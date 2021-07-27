using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace WindowsFormsApplication1
{

    public partial class yoneticiGiris : Form
    {
        MySqlConnection bağlanti = new MySqlConnection("server=localhost;database=hali;uid=root;pwd=tepe");
        public yoneticiGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullanici_ad = textBox1.Text;
            string sifre = textBox2.Text;
            string baglan = "select * from yonetici where kullanici_ad = @kullanici_ad and sifre = @sifre";
            MySqlCommand komut = new MySqlCommand(baglan, bağlanti);
            komut.Parameters.AddWithValue("@kullanici_ad", kullanici_ad);
            komut.Parameters.AddWithValue("@sifre", sifre);
            bağlanti.Open();
            MySqlDataReader okuma = komut.ExecuteReader();
            if (okuma.Read())
            {
                yöneticigirisyaptı yeni = new yöneticigirisyaptı();
                yeni.Show();
                this.Hide();

            }
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void yoneticiGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
