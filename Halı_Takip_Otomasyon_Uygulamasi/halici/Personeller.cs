using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace halici
{
    public partial class Personeller : Form
    {
        public Personeller()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
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

        private void Personeller_Load(object sender, EventArgs e)
        {

        }
    }
}
