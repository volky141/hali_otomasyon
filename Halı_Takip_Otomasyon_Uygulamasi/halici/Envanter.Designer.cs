namespace halici
{
    partial class StokTakipForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.siparişlerTableAdapter1 = new halici.haliDataSetTableAdapters.SiparişlerTableAdapter();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.txtUrunAdi = new System.Windows.Forms.TextBox();
            this.txtUrunID = new System.Windows.Forms.TextBox();
            this.txtStokMiktari = new System.Windows.Forms.TextBox();
            this.txtBirimFiyat = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOlustur = new System.Windows.Forms.Button();
            this.haliDataSet1 = new halici.haliDataSet1();
            this.envanterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.envanterTableAdapter = new halici.haliDataSet1TableAdapters.envanterTableAdapter();
            this.btnTemizle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.Personeller = new System.Windows.Forms.Button();
            this.Olustur = new System.Windows.Forms.Button();
            this.Envanter = new System.Windows.Forms.Button();
            this.Siparisler = new System.Windows.Forms.Button();
            this.flowLayoutPanelPages = new System.Windows.Forms.FlowLayoutPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.haliDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.envanterBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(327, 100);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(761, 386);
            this.dataGridView1.TabIndex = 0;
            // 
            // siparişlerTableAdapter1
            // 
            this.siparişlerTableAdapter1.ClearBeforeFill = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 16);
            this.label8.TabIndex = 107;
            this.label8.Text = "Ürün Adı";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 16);
            this.label7.TabIndex = 106;
            this.label7.Text = "Ürün ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 104;
            this.label6.Text = "Stok Miktarı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 358);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 103;
            this.label2.Text = "Birim Fiyat";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 102;
            this.label4.Text = "Kategori";
            // 
            // cmbKategori
            // 
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Location = new System.Drawing.Point(54, 283);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(156, 24);
            this.cmbKategori.TabIndex = 101;
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Location = new System.Drawing.Point(12, 439);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(147, 23);
            this.btnGuncelle.TabIndex = 100;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = true;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // txtUrunAdi
            // 
            this.txtUrunAdi.Location = new System.Drawing.Point(53, 229);
            this.txtUrunAdi.Name = "txtUrunAdi";
            this.txtUrunAdi.Size = new System.Drawing.Size(157, 22);
            this.txtUrunAdi.TabIndex = 99;
            // 
            // txtUrunID
            // 
            this.txtUrunID.Location = new System.Drawing.Point(53, 176);
            this.txtUrunID.Name = "txtUrunID";
            this.txtUrunID.ReadOnly = true;
            this.txtUrunID.Size = new System.Drawing.Size(157, 22);
            this.txtUrunID.TabIndex = 98;
            // 
            // txtStokMiktari
            // 
            this.txtStokMiktari.Location = new System.Drawing.Point(53, 326);
            this.txtStokMiktari.Name = "txtStokMiktari";
            this.txtStokMiktari.Size = new System.Drawing.Size(157, 22);
            this.txtStokMiktari.TabIndex = 96;
            // 
            // txtBirimFiyat
            // 
            this.txtBirimFiyat.Location = new System.Drawing.Point(53, 377);
            this.txtBirimFiyat.Name = "txtBirimFiyat";
            this.txtBirimFiyat.Size = new System.Drawing.Size(157, 22);
            this.txtBirimFiyat.TabIndex = 95;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(479, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 111;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(327, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 110;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 109;
            this.label1.Text = "Ürün arama";
            // 
            // btnOlustur
            // 
            this.btnOlustur.Location = new System.Drawing.Point(178, 439);
            this.btnOlustur.Name = "btnOlustur";
            this.btnOlustur.Size = new System.Drawing.Size(129, 23);
            this.btnOlustur.TabIndex = 117;
            this.btnOlustur.Text = "Oluştur";
            this.btnOlustur.UseVisualStyleBackColor = true;
            this.btnOlustur.Click += new System.EventHandler(this.btnOlustur_Click);
            // 
            // haliDataSet1
            // 
            this.haliDataSet1.DataSetName = "haliDataSet1";
            this.haliDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // envanterBindingSource
            // 
            this.envanterBindingSource.DataMember = "envanter";
            this.envanterBindingSource.DataSource = this.haliDataSet1;
            // 
            // envanterTableAdapter
            // 
            this.envanterTableAdapter.ClearBeforeFill = true;
            // 
            // btnTemizle
            // 
            this.btnTemizle.Location = new System.Drawing.Point(12, 485);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(147, 23);
            this.btnTemizle.TabIndex = 118;
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.UseVisualStyleBackColor = true;
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(178, 484);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(129, 23);
            this.btnSil.TabIndex = 119;
            this.btnSil.Text = "Veriyi Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            // 
            // Personeller
            // 
            this.Personeller.Location = new System.Drawing.Point(1027, 36);
            this.Personeller.Name = "Personeller";
            this.Personeller.Size = new System.Drawing.Size(112, 36);
            this.Personeller.TabIndex = 123;
            this.Personeller.Text = "Personeller";
            this.Personeller.UseVisualStyleBackColor = true;
            this.Personeller.Click += new System.EventHandler(this.Personeller_Click);
            // 
            // Olustur
            // 
            this.Olustur.Location = new System.Drawing.Point(697, 36);
            this.Olustur.Name = "Olustur";
            this.Olustur.Size = new System.Drawing.Size(104, 36);
            this.Olustur.TabIndex = 120;
            this.Olustur.Text = "Sipariş Oluştur";
            this.Olustur.UseVisualStyleBackColor = true;
            this.Olustur.Click += new System.EventHandler(this.Olustur_Click);
            // 
            // Envanter
            // 
            this.Envanter.Location = new System.Drawing.Point(917, 36);
            this.Envanter.Name = "Envanter";
            this.Envanter.Size = new System.Drawing.Size(104, 36);
            this.Envanter.TabIndex = 121;
            this.Envanter.Text = "Envanter";
            this.Envanter.UseVisualStyleBackColor = true;
            this.Envanter.Click += new System.EventHandler(this.Envanter_Click);
            // 
            // Siparisler
            // 
            this.Siparisler.Location = new System.Drawing.Point(807, 36);
            this.Siparisler.Name = "Siparisler";
            this.Siparisler.Size = new System.Drawing.Size(104, 36);
            this.Siparisler.TabIndex = 122;
            this.Siparisler.Text = "Siparişler";
            this.Siparisler.UseVisualStyleBackColor = true;
            this.Siparisler.Click += new System.EventHandler(this.Siparisler_Click);
            // 
            // flowLayoutPanelPages
            // 
            this.flowLayoutPanelPages.Location = new System.Drawing.Point(340, 492);
            this.flowLayoutPanelPages.Name = "flowLayoutPanelPages";
            this.flowLayoutPanelPages.Size = new System.Drawing.Size(853, 49);
            this.flowLayoutPanelPages.TabIndex = 126;
           // this.flowLayoutPanelPages.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelPages_Paint);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(727, 549);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 125;
            this.button4.Text = "Sonraki";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(545, 549);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 124;
            this.button3.Text = "Önceki";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // StokTakipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1371, 750);
            this.Controls.Add(this.flowLayoutPanelPages);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Personeller);
            this.Controls.Add(this.Olustur);
            this.Controls.Add(this.Envanter);
            this.Controls.Add(this.Siparisler);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnTemizle);
            this.Controls.Add(this.btnOlustur);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbKategori);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.txtUrunAdi);
            this.Controls.Add(this.txtUrunID);
            this.Controls.Add(this.txtStokMiktari);
            this.Controls.Add(this.txtBirimFiyat);
            this.Controls.Add(this.dataGridView1);
            this.Name = "StokTakipForm";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.haliDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.envanterBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private haliDataSetTableAdapters.SiparişlerTableAdapter siparişlerTableAdapter1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.TextBox txtUrunAdi;
        private System.Windows.Forms.TextBox txtUrunID;
        private System.Windows.Forms.TextBox txtStokMiktari;
        private System.Windows.Forms.TextBox txtBirimFiyat;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOlustur;
        private haliDataSet1 haliDataSet1;
        private System.Windows.Forms.BindingSource envanterBindingSource;
        private haliDataSet1TableAdapters.envanterTableAdapter envanterTableAdapter;
        private System.Windows.Forms.Button btnTemizle;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button Personeller;
        private System.Windows.Forms.Button Olustur;
        private System.Windows.Forms.Button Envanter;
        private System.Windows.Forms.Button Siparisler;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPages;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
    }
}