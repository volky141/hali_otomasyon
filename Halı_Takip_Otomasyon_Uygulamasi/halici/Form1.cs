using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;


namespace halici
{
    public partial class Form1 : Form
    {

        public static class GlobalSettings
        {
            public static bool IsLoggedIn = false;
        }
        // Veritabanı bağlantı dizesi
        private readonly string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=hali.accdb";

        // Sayfalama ile ilgili değişkenler
        private int currentPage = 1;
        private int recordsPerPage = 1; // Her sayfada kaç kayıt gösterilecek
        private int totalRecords;       // Toplam kayıt sayısı
        private int totalPages;         // Toplam sayfa sayısı

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Eğer zaten giriş yapılmışsa, login panelini tekrar göstermeyin.
            if (!GlobalSettings.IsLoggedIn)
            {
                this.Hide();
                using (Admin adminForm = new Admin())
                {
                    if (adminForm.ShowDialog() == DialogResult.OK)
                    {
                        GlobalSettings.IsLoggedIn = true;
                        this.Show();

                        // Toplam kayıt sayısını al ve ilk sayfayı yükle
                        GetTotalRecords();
                        currentPage = 1;
                        LoadData(currentPage);
                        GeneratePageButtons();

                        // ComboBoxları doldur
                        PopulateDurumComboBox();
                        PopulateStatusComboBox();
                        PopulateKargoServisiComboBox();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            else
            {
                // Giriş zaten yapılmışsa, sadece gerekli verileri yükleyin
                this.Show();
                GetTotalRecords();
                currentPage = 1;
                LoadData(currentPage);
                PopulateDurumComboBox();
                PopulateStatusComboBox();
                PopulateKargoServisiComboBox();
            }
        }

        #region Sayfalama (Önceki/Sonraki Sayfa)

        // Veritabanındaki toplam kayıt sayısını hesaplar ve totalPages'i günceller.
        private void GetTotalRecords()
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM [siparis-ekranı]";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        totalRecords = (int)cmd.ExecuteScalar();
                        totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Toplam kayıt sayısı alınırken hata oluştu: " + ex.Message,
                                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // İlgili sayfadaki kayıtları DataGridView'e yükler.
        private void LoadData(int page)
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    conn.Open();
                    int startRecord = (page - 1) * recordsPerPage;

                    string query;
                    // İlk sayfadaysak, doğrudan TOP x kayıt
                    if (startRecord == 0)
                    {
                        query = $"SELECT TOP {recordsPerPage} * FROM [siparis-ekranı] ORDER BY [siparis-id] desc";
                    }
                    else
                    {
                        // Önceki sayfalarda gösterilen kayıtları hariç tutarak TOP x kayıt
                        query = $@"
                            SELECT TOP {recordsPerPage} * 
                            FROM [siparis-ekranı]
                            WHERE [siparis-id] NOT IN
                            (
                                SELECT TOP {startRecord} [siparis-id] 
                                FROM [siparis-ekranı]
                                ORDER BY [siparis-id] desc
                            )
                            ORDER BY [siparis-id] desc";
                    }

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        dataGridView1.DataSource = dt;
                    }

                    UpdateNavigationButtons();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri yüklenirken hata oluştu: " + ex.Message,
                                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Önceki/Sonraki butonlarının aktiflik durumunu günceller.
        private void UpdateNavigationButtons()
        {
            // button3 = Önceki, button4 = Sonraki
            button3.Enabled = (currentPage > 1);
            button4.Enabled = (currentPage < totalPages);
        }

        // Önceki sayfa butonuna tıklandığında çalışır.
        private void button3_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData(currentPage);
                GeneratePageButtons();
            }
        }

        // Sonraki sayfa butonuna tıklandığında çalışır.
        private void button4_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData(currentPage); 
                GeneratePageButtons();
            }
        }

        #endregion

        #region Dinamik Sayfa Numarası Butonları

        // FlowLayoutPanel (flowLayoutPanelPages) içerisine dinamik olarak sayfa numarası butonlarını ekler.
        private void GeneratePageButtons()
        {
            int maxVisibleButtons = 6; // Gösterilecek maksimum sayfa butonu sayısı
            flowLayoutPanelPages.Controls.Clear();

            // Toplam sayfa sayısı, gösterilmek istenen buton sayısından küçükse
            if (totalPages < maxVisibleButtons)
                maxVisibleButtons = totalPages;

            // Mevcut sayfayı merkez alacak şekilde başlangıç sayfasını hesapla
            int startPage = currentPage - (maxVisibleButtons / 2);
            if (startPage < 1)
                startPage = 1;

            int endPage = startPage + maxVisibleButtons - 1;
            if (endPage > totalPages)
            {
                endPage = totalPages;
                startPage = endPage - maxVisibleButtons + 1;
                if (startPage < 1)
                    startPage = 1;
            }

            // Belirlenen aralıktaki sayfa butonlarını oluştur
            for (int i = startPage; i <= endPage; i++)
            {
                Button btnPage = new Button();
                btnPage.Text = i.ToString();
                btnPage.Width = 40;
                btnPage.Height = 30;
                btnPage.Tag = i;  // Butona sayfa numarasını sakla
                btnPage.Click += new EventHandler(PageButton_Click);

                // Aktif sayfayı vurgulamak için farklı bir renk kullanın
                if (i == currentPage)
                {
                    btnPage.BackColor = Color.LightBlue;
                }

                flowLayoutPanelPages.Controls.Add(btnPage);
            }
        }

        // Dinamik oluşturulan sayfa butonuna tıklama olayı.
        private void PageButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                currentPage = Convert.ToInt32(btn.Tag);
                LoadData(currentPage);
                GeneratePageButtons();
            }
        }

        #endregion

        #region ComboBox Doldurma ve Filtreleme

        /// comboBox1 - "Tümü" seçeneği içeren durum bilgileri.
        private void PopulateDurumComboBox()
        {
            try
            {
                comboBox1.Items.Clear();
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT durum FROM [siparis-ekranı]";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader["durum"].ToString());
                            }
                        }
                    }
                }
                // Başta "Tümü" seçeneği
                comboBox1.Items.Insert(0, "Tümü");
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ComboBox verileri yüklenirken hata oluştu: " + ex.Message,
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// comboBoxStatus - Sipariş durumu güncelleme combobox'ı.
        private void PopulateStatusComboBox()
        {
            try
            {
                comboBoxStatus.Items.Clear();
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT durum FROM [siparis-ekranı]";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBoxStatus.Items.Add(reader["durum"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Durum bilgileri yüklenirken hata oluştu: " + ex.Message,
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // comboBoxKargoServisi - Kargo servisi bilgileri.
        private void PopulateKargoServisiComboBox()
        {
            try
            {
                comboBoxKargoServisi.Items.Clear();
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT [kargo-servisi] FROM [siparis-ekranı]";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBoxKargoServisi.Items.Add(reader["kargo-servisi"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kargo servisi bilgileri yüklenirken hata oluştu: " + ex.Message,
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // comboBox1 (Tümü / durum) değiştiğinde filtreleme yapar.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDurum = comboBox1.SelectedItem.ToString();
            if (selectedDurum == "Tümü")
            {
                // Tümü seçildiyse sayfalamayı sıfırla
                currentPage = 1;
                GetTotalRecords();
                LoadData(currentPage);
                GeneratePageButtons();
            }
            else
            {
                // Belirli bir duruma göre filtrele
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT * FROM [siparis-ekranı] WHERE durum = @durum ORDER BY [siparis-id]";
                        using (OleDbDataAdapter da = new OleDbDataAdapter(query, conn))
                        {
                            da.SelectCommand.Parameters.AddWithValue("@durum", selectedDurum);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;

                            // Filtreliyken sayfalama devre dışı
                            button3.Enabled = false;
                            button4.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Filtreleme yapılırken hata oluştu: " + ex.Message,
                                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Arama (textBox1)

        // textBox1 - Sipariş ID araması. Boşsa tüm veriyi, doluysa filtreli veriyi gösterir.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                // Arama kutusu boşaldıysa mevcut sayfayı tekrar yükle
                LoadData(currentPage);
                GeneratePageButtons();
            }
            else
            {
                // Arama metnine göre siparis-id filtrele
                SearchOrders(searchText);
            }
        }

        private void SearchOrders(string searchText)
        {
            DataTable orders = GetAllOrders();
            DataView dv = orders.DefaultView;

            // siparis-id kolonunu string'e çevirerek arama
            dv.RowFilter = $"Convert([siparis-id], 'System.String') LIKE '%{searchText}%' OR [ad] LIKE '%{searchText}%'";
            dataGridView1.DataSource = dv.ToTable();
        }

        // Tüm siparişleri Access'ten getirir.
        private DataTable GetAllOrders()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connString))
                {
                    connection.Open();
                    string query = "SELECT * FROM [siparis-ekranı]";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Siparişler alınırken hata oluştu: " + ex.Message,
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        #endregion

        #region DataGridView Tıklama ve Güncelleme

        // DataGridView üzerinde bir hücreye tıklandığında, ilgili sipariş bilgilerini text/combobox alanlarına doldurur.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["siparis-id"].Value?.ToString();
                textBox3.Text = row.Cells["telefon"].Value?.ToString();
                comboBoxStatus.Text = row.Cells["durum"].Value?.ToString();
                textBox5.Text = row.Cells["adet"].Value?.ToString();
                textBox6.Text = row.Cells["ad"].Value?.ToString();
                textBox7.Text = row.Cells["soyadı"].Value?.ToString();
                textBox8.Text = row.Cells["adres"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["teslim-tarihi"].Value?.ToString(), out DateTime teslimTarihi))
                {
                    dateTimePickerTeslimTarihi.Value = teslimTarihi;
                }
                else
                {
                    dateTimePickerTeslimTarihi.Value = DateTime.Today;
                }

                comboBoxKargoServisi.Text = row.Cells["kargo-servisi"].Value?.ToString();

                // Güncelleme işlemi aktif, oluşturma pasif
                buttonCreate.Enabled = false;
                button5.Enabled = true;
            }
        }

        // Güncelleme işlemi (button5)
        private void button5_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                UPDATE [siparis-ekranı] SET 
                    [telefon] = @telefon, 
                    [durum] = @durum, 
                    [adet] = @adet, 
                    [ad] = @ad, 
                    [soyadı] = @soyad, 
                    [adres] = @adres, 
                    [teslim-tarihi] = @teslimTarihi, 
                    [kargo-servisi] = @kargoServisi
                WHERE [siparis-id] = @siparisId";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@telefon", textBox3.Text);
                        cmd.Parameters.AddWithValue("@durum", comboBoxStatus.Text);
                        cmd.Parameters.AddWithValue("@adet", textBox5.Text);
                        cmd.Parameters.AddWithValue("@ad", textBox6.Text);
                        cmd.Parameters.AddWithValue("@soyad", textBox7.Text);
                        cmd.Parameters.AddWithValue("@adres", textBox8.Text);
                        cmd.Parameters.AddWithValue("@teslimTarihi", dateTimePickerTeslimTarihi.Value);
                        cmd.Parameters.AddWithValue("@kargoServisi", comboBoxKargoServisi.Text);
                        cmd.Parameters.AddWithValue("@siparisId", textBox2.Text);

                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Güncelleme başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Güncel sayfayı tekrar yükle
                            LoadData(currentPage);

                            GeneratePageButtons();

                            // Formu sıfırla
                            ResetForm();
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme yapılmadı. Sipariş bulunamadı.",
                                            "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Güncelleme sırasında hata oluştu: " + ex.Message,
                                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        // Oluşturma işlemi (buttonCreate)
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    conn.Open();

                    string query = @"
        INSERT INTO [siparis-ekranı] 
        ([telefon], [durum], [adet], [ad], [soyadı], [adres], [teslim-tarihi], [kargo-servisi])
        VALUES 
        (@telefon, @durum, @adet, @ad, @soyad, @adres, @teslimTarihi, @kargoServisi)";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@telefon", textBox3.Text);
                        cmd.Parameters.AddWithValue("@durum", comboBoxStatus.Text);
                        cmd.Parameters.AddWithValue("@adet", Convert.ToInt32(textBox5.Text));
                        cmd.Parameters.AddWithValue("@ad", textBox6.Text);
                        cmd.Parameters.AddWithValue("@soyad", textBox7.Text);
                        cmd.Parameters.AddWithValue("@adres", textBox8.Text);
                        cmd.Parameters.AddWithValue("@teslimTarihi", dateTimePickerTeslimTarihi.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@kargoServisi", comboBoxKargoServisi.Text);

                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Yeni kayıtları göster
                            GetTotalRecords();
                            LoadData(currentPage);

                            GeneratePageButtons();


                            // Formu sıfırla
                            ResetForm();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt eklenemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt ekleme sırasında hata oluştu: " + ex.Message,
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Formu temizleyip butonları sıfırlayan metod
        private void ResetForm()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            comboBoxStatus.SelectedIndex = -1;
            comboBoxKargoServisi.SelectedIndex = -1;
            dateTimePickerTeslimTarihi.Value = DateTime.Today;

            // Oluşturma aktif, güncelleme pasif
            buttonCreate.Enabled = true;
            button5.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Şuanki formu gizle

            using (Form1 form1Form = new Form1())
            {
                // Eğer personel formu kapatıldığında OK dönerse ana formu tekrar göster
                if (form1Form.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide(); // Şuanki formu gizle

            using (StokTakipForm envanterForm = new StokTakipForm())
            {
                // Eğer personel formu kapatıldığında OK dönerse ana formu tekrar göster
                if (envanterForm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Şuanki formu gizle

            using (Siparis_Olustur sipolusturForm = new Siparis_Olustur())
            {
                // Eğer personel formu kapatıldığında OK dönerse ana formu tekrar göster
                if (sipolusturForm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide(); // Şuanki formu gizle

            using (Personeller PersonellerForm = new Personeller())
            {
                // Eğer personel formu kapatıldığında OK dönerse ana formu tekrar göster
                if (PersonellerForm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
