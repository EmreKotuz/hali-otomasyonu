using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApplication1.yonetici
{
    public partial class ciro : Form
    {
        MySqlConnection bağlanti = new MySqlConnection("server=localhost;database=hali;uid=root;pwd=tepe");
        public ciro()
        {
            InitializeComponent();
            hali_ekle();

        }
        public void hali_ekle()
        {
            string hali = "select h_ad from hali";
            MySqlCommand komut = new MySqlCommand(hali, bağlanti);
            bağlanti.Open();
            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                comboBox1.Items.Add(Convert.ToString(okuma[0]));
            }
            bağlanti.Close();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int aylik = 0;
            string aylik_ciro = "select sum(fiyat)from halisatis where tarih > date_sub(curdate(),interval 1 month)";

            MySqlCommand komut = new MySqlCommand(aylik_ciro, bağlanti);
            bağlanti.Open();
            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                aylik = Convert.ToInt32(okuma[0]);
            }

            bağlanti.Close();
            textBox1.Text = aylik.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int yıllık = 0;
            string yıllık_ciro = "select sum(fiyat)from halisatis where tarih > date_sub(curdate(),interval 1 year)";

            MySqlCommand komut = new MySqlCommand(yıllık_ciro, bağlanti);
            bağlanti.Open();
            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                yıllık = Convert.ToInt32(okuma[0]);
            }

            bağlanti.Close();
            textBox2.Text = yıllık.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ciro = 0;
            string yıllık_ciro = "select sum(fiyat)from halisatis where tarih > @bir and tarih < @iki)";

            MySqlCommand komut = new MySqlCommand(yıllık_ciro, bağlanti);
            komut.Parameters.AddWithValue("@bir", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@iki", dateTimePicker2.Value);
            bağlanti.Open();
            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                ciro = Convert.ToInt32(okuma[0]);
            }

            bağlanti.Close();
            textBox3.Text = ciro.ToString();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ciro_yıllık = 0;
            string aylikciro = "select sum(fiyat)from halisatis,hali where hali.h_id=halisatis.h_id and h_ad =" +comboBox1.Text + "and tarih > date_sub(curdate(),interval 1  year)";

            bağlanti.Open();
            MySqlCommand komut = new MySqlCommand(aylikciro, bağlanti);

            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                ciro_yıllık = Convert.ToInt32(okuma[0]);
            }
            textBox4.Text = ciro_yıllık.ToString();
            bağlanti.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int ciro_aylık = 0;
            string aylikciro = "select sum(fiyat)from halisatis,hali where hali.h_id=halisatis.h_id and h_ad =" +comboBox1.Text + " and tarih > date_sub(curdate(),interval 1 month)";
            
            bağlanti.Open();
            MySqlCommand komut = new MySqlCommand(aylikciro, bağlanti);

            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                ciro_aylık = Convert.ToInt32(okuma[0]);
            }
            textBox4.Text = ciro_aylık.ToString();
            bağlanti.Close();
  
        }

        private void ciro_Load(object sender, EventArgs e)
        {

        }
    }
}
