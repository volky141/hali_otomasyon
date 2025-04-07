using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace halici
{
    public partial class StokTakipForm : Form
    {
        // Veritabanı bağlantı cümlesi
        string conString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=hali.accdb;";

        // Sayfalama için global değişkenler
        private int currentPage = 1;
        private int pageSize = 10;      // Her sayfada gösterilecek kayıt sayısı
        private int totalRecords = 0;
        private int totalPages = 0;

        public StokTakipForm()
        {
            InitializeComponent();

            // Form ve diğer kontroller için event bağlama
            this.Load += new EventHandler(StokTakipForm_Load);
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);

            // CRUD butonları için event bağlama
            btnSil.Click += new EventHandler(btnSil_Click);
            btnGuncelle.Click += new EventHandler(btnGuncelle_Click);
            btnOlustur.Click += new EventHandler(btnOlustur_Click);
            btnTemizle.Click += new EventHandler(btnTemizle_Click);

            // Sayfalama butonları için event bağlama (form tasarımınızda bu butonların bulunduğunu varsayıyoruz)
            button4.Click += new EventHandler(button4_Click);
            button3.Click += new EventHandler(button3_Click);
        }

        private void StokTakipForm_Load(object sender, EventArgs e)
        {
            // İlk sayfa ayarı
            currentPage = 1;

            // DataGridView ve ComboBox'ı doldur
            StokListele();
            KategoriListele();

            // İlk buton durumları: Temizle, Güncelle ve Sil pasif, Oluştur aktif
            btnOlustur.Enabled = true;
            btnTemizle.Enabled = false;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
        }

        // DataGridView'e envanter tablosunu, sayfalama mantığıyla yükler
        private void StokListele()
        {
            using (OleDbConnection con = new OleDbConnection(conString))
            {
                try
                {
                    con.Open();

                    // Toplam kayıt sayısını al
                    string countQuery = "SELECT COUNT(*) FROM envanter";
                    using (OleDbCommand countCmd = new OleDbCommand(countQuery, con))
                    {
                        totalRecords = (int)countCmd.ExecuteScalar();
                    }
                    totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                    string query = "";
                    if (currentPage == 1)
                    {
                        // İlk sayfa için basit TOP sorgusu
                        query = $"SELECT TOP {pageSize} * FROM envanter ORDER BY [urun-id]";
                    }
                    else
                    {
                        // Sonraki sayfalar için, önceki sayfalardaki kayıtları hariç tutan sorgu
                        int offset = pageSize * (currentPage - 1);
                        query = $"SELECT TOP {pageSize} * FROM envanter " +
                                $"WHERE [urun-id] NOT IN (SELECT TOP {offset} [urun-id] FROM envanter ORDER BY [urun-id]) " +
                                $"ORDER BY [urun-id]";
                    }

                    OleDbDataAdapter da = new OleDbDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    // Sayfalama bilgisini güncelle
                    UpdatePaginationStatus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        // Sayfa bilgisini lblPageStatus etiketine yazar
        private void UpdatePaginationStatus()
        {
            flowLayoutPanelPages.Text = $"Sayfa {currentPage} / {totalPages}";
        }

        // DISTINCT kategori isimlerini ComboBox'a ekler
        private void KategoriListele()
        {
            cmbKategori.Items.Clear();

            using (OleDbConnection con = new OleDbConnection(conString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT DISTINCT kategori FROM envanter WHERE kategori IS NOT NULL AND kategori <> ''";
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        using (OleDbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                cmbKategori.Items.Add(dr["kategori"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata (KategoriListele): " + ex.Message);
                }
            }
        }

        // DataGridView'de satır seçildiğinde bilgileri TextBox ve ComboBox'lara aktarır
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtUrunID.Text = row.Cells["urun-id"].Value.ToString();
                txtUrunAdi.Text = row.Cells["urunadı"].Value.ToString();
                cmbKategori.Text = row.Cells["kategori"].Value.ToString();
                txtStokMiktari.Text = row.Cells["stok-miktar"].Value.ToString();
                txtBirimFiyat.Text = row.Cells["birim-fiyat"].Value.ToString();

                // Seçim yapıldığında: Oluştur pasif, Temizle, Güncelle ve Sil aktif
                btnOlustur.Enabled = false;
                btnTemizle.Enabled = true;
                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;
            }
        }

        // Güncelle butonu
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUrunID.Text))
            {
                MessageBox.Show("Lütfen güncellenecek ürünü seçin.");
                return;
            }

            using (OleDbConnection con = new OleDbConnection(conString))
            {
                try
                {
                    con.Open();
                    string query = @"UPDATE envanter 
                                     SET [urunadı] = ?, [kategori] = ?, [stok-miktar] = ?, [birim-fiyat] = ?
                                     WHERE [urun-id] = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@urunadı", txtUrunAdi.Text);
                        cmd.Parameters.AddWithValue("@kategori", cmbKategori.Text);
                        cmd.Parameters.AddWithValue("@stok-miktar", txtStokMiktari.Text);
                        cmd.Parameters.AddWithValue("@birim-fiyat", txtBirimFiyat.Text);
                        cmd.Parameters.AddWithValue("@urunid", txtUrunID.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Ürün başarıyla güncellendi.");
                    StokListele();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata (Güncelle): " + ex.Message);
                }
            }
        }

        // Oluştur butonu (Yeni ürün ekleme)
        private void btnOlustur_Click(object sender, EventArgs e)
        {
            using (OleDbConnection con = new OleDbConnection(conString))
            {
                try
                {
                    con.Open();
                    string query = @"INSERT INTO envanter ([urunadı], [kategori], [stok-miktar], [birim-fiyat])
                                     VALUES (?, ?, ?, ?)";
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@urunadı", txtUrunAdi.Text);
                        cmd.Parameters.AddWithValue("@kategori", cmbKategori.Text);
                        cmd.Parameters.AddWithValue("@stok-miktar", txtStokMiktari.Text);
                        cmd.Parameters.AddWithValue("@birim-fiyat", txtBirimFiyat.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Yeni ürün başarıyla eklendi.");
                    StokListele();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata (Oluştur): " + ex.Message);
                }
            }
        }

        // Temizle butonu
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        // Sil butonu
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUrunID.Text))
            {
                MessageBox.Show("Lütfen silinecek ürünü seçin.");
                return;
            }

            DialogResult dr = MessageBox.Show("Seçili ürünü silmek istediğinize emin misiniz?",
                                              "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                using (OleDbConnection con = new OleDbConnection(conString))
                {
                    try
                    {
                        con.Open();
                        string query = "DELETE FROM envanter WHERE [urun-id] = ?";
                        using (OleDbCommand cmd = new OleDbCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@urunid", txtUrunID.Text);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Ürün başarıyla silindi.");
                        StokListele();
                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata (Sil): " + ex.Message);
                    }
                }
            }
        }

        // TextBox ve ComboBox'ları temizleyen ve buton durumlarını sıfırlayan metod
        private void ClearInputs()
        {
            txtUrunID.Clear();
            txtUrunAdi.Clear();
            cmbKategori.SelectedIndex = -1;
            txtStokMiktari.Clear();
            txtBirimFiyat.Clear();

            // Buton durumlarını sıfırla: Oluştur aktif, diğerleri pasif
            btnOlustur.Enabled = true;
            btnTemizle.Enabled = false;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
        }

        // Sipariş oluşturma ekranını açan metod
        private void Olustur_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (Siparis_Olustur sipolusturForm = new Siparis_Olustur())
            {
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

        #region Üst Butonlar
        private void Siparisler_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (Form1 form1Form = new Form1())
            {
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

        private void Envanter_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (StokTakipForm envanterForm = new StokTakipForm())
            {
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

        private void Personeller_Click(object sender, EventArgs e)
        {
            using (Personeller PersonellerForm = new Personeller())
            {
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
        #endregion

        // Sayfalama: Sonraki sayfa butonu
        private void button4_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                StokListele();
            }
        }

        // Sayfalama: Önceki sayfa butonu
        private void button3_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                StokListele();
            }
        }

        // (Opsiyonel) Sayfa bilgisi için özel çizim yapılabilir
        private void lblPageStatus_Paint(object sender, PaintEventArgs e)
        {
            // Eğer özel bir çizim yapmak isterseniz
        }

        #region Arama (textBox1)

        // textBox1 - Sipariş ID veya ürün adı araması. Boşsa tüm veriyi, doluysa filtreli veriyi gösterir.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                // Arama kutusu boşaldıysa mevcut sayfayı tekrar yükle
                StokListele();
            }
            else
            {
                // Arama metnine göre sipariş ID veya ürün adı filtrele
                SearchOrders(searchText);
            }
        }

        private void SearchOrders(string searchText)
        {
            DataTable orders = GetAllOrders();
            DataView dv = orders.DefaultView;

            // [urun-id] kolonu string'e çevrilerek ve [urunadı] kolonu da kontrol edilerek arama yapılır
            dv.RowFilter = $"Convert([urun-id], 'System.String') LIKE '%{searchText}%' OR [urunadı] LIKE '%{searchText}%'";
            dataGridView1.DataSource = dv.ToTable();
        }

        // Tüm ürünleri Access'ten getirir.
        private DataTable GetAllOrders()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (OleDbConnection connection = new OleDbConnection(conString))
                {
                    connection.Open();
                    string query = "SELECT * FROM [envanter]";
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
    }
}
