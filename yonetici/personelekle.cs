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
    public partial class personelekle : Form
    {
        MySqlConnection baglanti = new MySqlConnection("server = localhost;database=hali;uid=root;pwd=tepe");
        public personelekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string personelEkle = "insert into personel (p_ad, p_soyad, kullanici, sifre) values(@p_ad, @p_soyad, @kullanici, @sifre)";
            MySqlCommand komut = new MySqlCommand(personelEkle, baglanti);
            komut.Parameters.AddWithValue("@p_ad", textBox1.Text);
            komut.Parameters.AddWithValue("@p_soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@kullanici", textBox3.Text);
            komut.Parameters.AddWithValue("@sifre", textBox4.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}
