namespace WinFormsApp1
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnGetStations = new Button();
            dgvFuelStations = new DataGridView();
            txtSearch = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtApiKey = new TextBox();
            lblStationCount = new Label();
            dgvPrices = new DataGridView();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvFuelStations).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPrices).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnGetStations
            // 
            btnGetStations.Location = new Point(12, 12);
            btnGetStations.Name = "btnGetStations";
            btnGetStations.Size = new Size(104, 23);
            btnGetStations.TabIndex = 1;
            btnGetStations.Text = "Hent data";
            btnGetStations.UseVisualStyleBackColor = true;
            btnGetStations.Click += btnGetStations_Click;
            // 
            // dgvFuelStations
            // 
            dgvFuelStations.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvFuelStations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFuelStations.Location = new Point(12, 41);
            dgvFuelStations.MultiSelect = false;
            dgvFuelStations.Name = "dgvFuelStations";
            dgvFuelStations.RowHeadersVisible = false;
            dgvFuelStations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFuelStations.Size = new Size(800, 585);
            dgvFuelStations.TabIndex = 2;
            dgvFuelStations.Click += dgvFuelStations_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(154, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(304, 23);
            txtSearch.TabIndex = 3;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(122, 16);
            label1.Name = "label1";
            label1.Size = new Size(26, 15);
            label1.TabIndex = 4;
            label1.Text = "Søk";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(464, 16);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 5;
            label2.Text = "API-Key";
            // 
            // txtApiKey
            // 
            txtApiKey.Location = new Point(519, 12);
            txtApiKey.Name = "txtApiKey";
            txtApiKey.ReadOnly = true;
            txtApiKey.Size = new Size(534, 23);
            txtApiKey.TabIndex = 6;
            // 
            // lblStationCount
            // 
            lblStationCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStationCount.AutoSize = true;
            lblStationCount.Location = new Point(12, 629);
            lblStationCount.Name = "lblStationCount";
            lblStationCount.Size = new Size(50, 15);
            lblStationCount.TabIndex = 7;
            lblStationCount.Text = "Antall: 0";
            // 
            // dgvPrices
            // 
            dgvPrices.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPrices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPrices.Location = new Point(818, 41);
            dgvPrices.Name = "dgvPrices";
            dgvPrices.RowHeadersVisible = false;
            dgvPrices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrices.Size = new Size(562, 312);
            dgvPrices.TabIndex = 8;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(818, 359);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1392, 653);
            Controls.Add(pictureBox1);
            Controls.Add(dgvPrices);
            Controls.Add(lblStationCount);
            Controls.Add(txtApiKey);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtSearch);
            Controls.Add(dgvFuelStations);
            Controls.Add(btnGetStations);
            Name = "FrmMain";
            Text = "Drivstoffappen - Stasjoner";
            ((System.ComponentModel.ISupportInitialize)dgvFuelStations).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPrices).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button btnGetStations;
        private DataGridView dgvFuelStations;
        private TextBox txtSearch;
        private Label label1;
        private Label label2;
        private TextBox txtApiKey;
        private Label lblStationCount;
        private DataGridView dgvPrices;
        private PictureBox pictureBox1;
    }
}
