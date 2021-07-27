using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1.personel
{
    public partial class personelGiris : Form
    {
        MySqlConnection bağlanti = new MySqlConnection("server=localhost; database=hali;uid=root;pwd=tepe");
        public personelGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string k_ad = textBox1.Text;
            string sifre = textBox2.Text;

            string giris = "select * from personel where kullanici = @kullanici and sifre = @sifre";
            MySqlCommand komut = new MySqlCommand(giris, bağlanti);
            komut.Parameters.AddWithValue("@kullanici", k_ad);
            komut.Parameters.AddWithValue("@sifre", sifre);
            bağlanti.Open();
            MySqlDataReader okuma = komut.ExecuteReader();
            if (okuma.Read())
            {
                haliSatisigiriss w = new haliSatisigiriss();
                w.Show();
                this.Hide();
            }
        }
    }
}
