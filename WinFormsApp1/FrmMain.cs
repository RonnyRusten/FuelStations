using DrivstoffappenStations.Models;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace WinFormsApp1
{
    public partial class FrmMain : Form
    {
        private IEnumerable<FuelStation>? _allStations;
        public FrmMain()
        {
            InitializeComponent();
        }

        private async void btnGetStations_Click(object sender, EventArgs e)
        {
            btnGetStations.Enabled = false;
            await GetStations();
            btnGetStations.Enabled = true;
        }

        private async Task GetStations()
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


            const string xClientId = "com.raskebiler.drivstoff.appen.ios";
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(tokenShifted));
                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("x2"));
                }

                var md5Hash = sb.ToString().ToLower();

                _allStations = await GetFuelStations("https://api.drivstoffappen.no/api/v1/stations?stationTypeId=1&includeDeleted=true&includePending=true", md5Hash, xClientId);

                var plusPos = tokenResponse.ExpiresAt?.IndexOf("+") ?? 0;
                var expires = tokenResponse.ExpiresAt?.Substring(0, plusPos).Replace("T", " ");
                var tokenEnd = Convert.ToDateTime(expires).AddHours(1);
                txtApiKey.Text = $"{md5Hash} Denne varer til: {tokenEnd:g}";
            }


            dgvFuelStations.DataSource = _allStations;
            dgvFuelStations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvFuelStations.Columns[1].Visible = false;
            dgvFuelStations.Columns[2].Visible = false;
            dgvFuelStations.Columns[3].Visible = false;
            dgvFuelStations.Columns[8].Visible = true;
            dgvFuelStations.Columns[9].Visible = true;

            lblStationCount.Text = $"Antall: {_allStations.Count()}";
        }

        private static async Task<Authorization?> GetApiToken(string url)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var authorization = await response.Content.ReadFromJsonAsync<Authorization>();

            return authorization;
        }

        private static async Task<IEnumerable<FuelStation>?> GetFuelStations(string url, string apiKey, string clientId)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            client.DefaultRequestHeaders.Add("X-CLIENT-ID", clientId);

            var response = await client.GetAsync(url);
            var stations = await response.Content.ReadFromJsonAsync<List<FuelStation>>();
            return stations;
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
    }
}
