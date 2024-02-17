using DrivstoffappenStations.Models;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace WinFormsApp1
{
    public partial class FrmMain : Form
    {
        private IEnumerable<FuelStationViewModel>? _allStations;
        private IEnumerable<FuelStation>? _fuelStations;
        private IEnumerable<FuelBrand> _fuelBrands;

        public FrmMain()
        {
            InitializeComponent();
        }

        private async void btnGetStations_Click(object sender, EventArgs e)
        {
            btnGetStations.Enabled = false;
            await GetData();
            btnGetStations.Enabled = true;
        }

        private async Task GetData()
        {
            var authorization = await GetAuthorization();

            var plusPos = authorization.ExpiresAt?.IndexOf("+") ?? 0;
            var expires = authorization.ExpiresAt?.Substring(0, plusPos).Replace("T", " ");
            var tokenEnd = Convert.ToDateTime(expires).AddHours(1);
            txtApiKey.Text = $"{authorization.ApiKey} Denne varer til: {tokenEnd:g}";

            _fuelStations = await GetFuelStations(authorization.ApiKey);
            _fuelBrands = await GetFuelBrands(authorization.ApiKey);
            _allStations = await GetFuelStationList(authorization.ApiKey);

            dgvFuelStations.DataSource = _allStations;
            dgvFuelStations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvFuelStations.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFuelStations.Columns[4].Visible = false;
            dgvFuelStations.Columns[5].Visible = false;
            dgvFuelStations.Columns[6].Visible = false;
            dgvFuelStations.Columns[7].Visible = false;

            lblStationCount.Text = $"Antall: {_allStations.Count()}";
        }

        private async Task<Authorization> GetAuthorization()
        {
            var tokenResponse = await GetApiToken("https://api.drivstoffappen.no/api/v1/authorization-sessions");
            var petrolDataToken = tokenResponse.Token;

            var tokenBytes = Encoding.UTF8.GetBytes(petrolDataToken);
            var first = tokenBytes[0];
            var rest = new byte[tokenBytes.Length - 1];
            Array.Copy(tokenBytes, 1, rest, 0, rest.Length);
            var tokenBytesShifted = new byte[tokenBytes.Length];
            Array.Copy(rest, 0, tokenBytesShifted, 0, rest.Length);
            tokenBytesShifted[^1] = first;
            var tokenShifted = Encoding.UTF8.GetString(tokenBytesShifted);

            using var md5 = MD5.Create();
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(tokenShifted));
            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("x2"));
            }

            var md5Hash = sb.ToString().ToLower();
            tokenResponse.ApiKey = md5Hash;

            return tokenResponse;
        }

        private static async Task<Authorization?> GetApiToken(string url)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var authorization = await response.Content.ReadFromJsonAsync<Authorization>();

            return authorization;
        }

        private async Task<List<FuelStationViewModel>> GetFuelStationList(string apiKey)
        {
            var list = new List<FuelStationViewModel>();
            foreach (var station in _fuelStations)
            {
                list.Add(new FuelStationViewModel
                {
                    Id = station.Id,
                    Brand = _fuelBrands.Single(b => b.Id == station.BrandId).Name,
                    CreatedAt = station.CreatedAt,
                    Latitude = station.Latitude,
                    Longitude = station.Longitude,
                    Location = station.Location,
                    Name = station.Name,
                    UpdatedAt = station.UpdatedAt
                });
            }

            return list;
        }

        private static async Task<IEnumerable<FuelStation>?> GetFuelStations(string apiKey)
        {
            var url = "https://api.drivstoffappen.no/api/v1/stations?stationTypeId=1&countryId=1";
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            client.DefaultRequestHeaders.Add("X-CLIENT-ID", "com.raskebiler.drivstoff.appen.ios");

            var response = await client.GetAsync(url);
            var stations = await response.Content.ReadFromJsonAsync<List<FuelStation>>();
            return stations;
        }

        private static async Task<List<FuelBrand>?> GetFuelBrands(string apiKey)
        {
            var brandsUrl = "https://api.drivstoffappen.no/api/v1/brands";
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            client.DefaultRequestHeaders.Add("X-CLIENT-ID", "com.raskebiler.drivstoff.appen.ios");

            var response = await client.GetAsync(brandsUrl);
            var brands = await response.Content.ReadFromJsonAsync<List<FuelBrand>>();
            return brands;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_allStations == null)
            {
                return;
            }

            if (txtSearch.Text.Length > 0)
            {
                var filtered = _allStations.Where(s => s.Name.ToLower().Contains(txtSearch.Text.ToLower()) | s.Location.ToLower().Contains(txtSearch.Text.ToLower()));
                dgvFuelStations.DataSource = filtered.ToList();
                lblStationCount.Text = $"Antall: {filtered.Count()}";
            }
            else
            {
                dgvFuelStations.DataSource = _allStations;
                lblStationCount.Text = $"Antall: {_allStations.Count()}";
            }
        }

        private void dgvFuelStations_Click(object sender, EventArgs e)
        {
            var fuelStationVM = (FuelStationViewModel)dgvFuelStations.SelectedRows[0].DataBoundItem;
            var fuelStation = _fuelStations.Single(s => s.Id == fuelStationVM.Id);
            var brand = _fuelBrands.Single(b => b.Id == fuelStation.BrandId);
            var prices = fuelStation.Prices;
            pictureBox1.ImageLocation = brand.PictureUrl;
            dgvPrices.DataSource = prices;
            dgvPrices.Columns[0].Visible = false;
            dgvPrices.Columns[1].Visible = false;
            dgvPrices.Columns[2].Visible = false;
            dgvPrices.Columns[4].Visible = false;
            dgvPrices.Columns[5].Visible = false;
            dgvPrices.Columns[6].Visible = false;
        }
    }
}
