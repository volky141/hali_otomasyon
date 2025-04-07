using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace halici
{
    public partial class Siparis_Olustur : Form
    {
        // Veritabanı bağlantı dizesi
        private readonly string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=hali.accdb";
        public Siparis_Olustur()
        {
            InitializeComponent();
        }


        #region Üst Butonlar
        private void Siparisler_Click(object sender, EventArgs e)
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

        private void Envanter_Click(object sender, EventArgs e)
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

        private void Personeller_Click(object sender, EventArgs e)
        {

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
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO [siparis-olustur] 
                        (
                            [adi],
                            [soyadi],
                            [telefon],
                            [hali-adet],
                            [hali-turu],
                            [musteri-notu],
                            [adres],
                            [hali-boyut],
                            [alim-tarihi],
                            [kargo-servisi],
                            [fiyat],
                            [teslim-tarihi]
                        )
                        VALUES
                        (
                            @adi,
                            @soyadi,
                            @telefon,
                            @haliAdet,
                            @haliTuru,
                            @musteriNotu,
                            @adres,
                            @haliBoyut,
                            @alimTarihi,
                            @kargoServisi,
                            @fiyat,
                            @teslimTarihi
                        )";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        // Formunuzdaki kontrol isimlerini buradaki parametrelere uygun şekilde düzenleyin
                        cmd.Parameters.AddWithValue("@adi", textBox_Adi.Text);
                        cmd.Parameters.AddWithValue("@soyadi", textBox_Soyadi.Text);
                        cmd.Parameters.AddWithValue("@telefon", textBox_Tel.Text);
                        cmd.Parameters.AddWithValue("@haliAdet", Convert.ToInt32(textBox_Adet.Text));
                        cmd.Parameters.AddWithValue("@haliTuru", textBox_Tur.Text);
                        cmd.Parameters.AddWithValue("@musteriNotu", textBox_Not.Text);
                        cmd.Parameters.AddWithValue("@adres", textBox_Adres.Text);
                        cmd.Parameters.AddWithValue("@haliBoyut", textBox_Boyut.Text);
                        cmd.Parameters.AddWithValue("@alimTarihi", dateTimePickerAlimTarihi.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@kargoServisi", comboBoxKargoServisi.Text);
                        cmd.Parameters.AddWithValue("@fiyat", textBox_fiyat.Text);
                        cmd.Parameters.AddWithValue("@teslimTarihi", dateTimePickerTeslimTarihi.Value.ToString("yyyy-MM-dd"));

                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Sipariş başarıyla oluşturuldu!",
                                            "Bilgi",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            // İsteğe bağlı: Form alanlarını temizleme
                            ResetForm();
                        }
                        else
                        {
                            MessageBox.Show("Sipariş oluşturulamadı!",
                                            "Hata",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sipariş oluşturulurken hata oluştu: " + ex.Message,
                                "Hata",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        // Form alanlarını temizleyen yardımcı metot (opsiyonel)
        private void ResetForm()
        {
            textBox_Adi.Clear();
            textBox_Soyadi.Clear();
            textBox_Tel.Clear();
            textBox_Adet.Clear();
            textBox_Tur.Clear();
            textBox_Not.Clear();
            textBox_Adres.Clear();
            textBox_Boyut.Clear();
            textBox_fiyat.Clear();
            dateTimePickerTeslimTarihi.Value = DateTime.Now;
            comboBoxKargoServisi.SelectedIndex = -1;
            dateTimePickerAlimTarihi.Value = DateTime.Now;
        }

        private void Siparis_Olustur_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'haliDataSet.Siparişler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.siparişlerTableAdapter.Fill(this.haliDataSet.Siparişler);

        }

        private void dateTimePickerTeslimTarihi_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerTeslimTarihi.Value = dateTimePickerAlimTarihi.Value.AddDays(4);
        }

        private void textBox_fiyat_TextChanged(object sender, EventArgs e)
        {

        }
    }


}

