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
    public partial class genel : Form
    {
        int halı_no = 0;
        int yer_no = 0;
        int[] randevu;
        int sayi = 0;        
        string halı_ad = "";
        string yer_ad = "";
        string randevutar = "";


        MySqlConnection baglanti = new MySqlConnection("server=localhost;database=hali;uid=root;pwd=tepe");
        public genel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string halıı = "select * from yer";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(halıı, baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adaptor.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string yer = "select * from randevu";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(yer, baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adaptor.Fill(tablo);
            dataGridView3.DataSource = tablo;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string randevu = "select * from hali";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(randevu, baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adaptor.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            halı_no = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                if (dataGridView2.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.White;
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            yer_no = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Red)
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            else
                dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

            randevu = new int[100];
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Red)
                    randevu[i] = Convert.ToInt32(dataGridView3.Rows[i].Cells[0].Value);
                sayi = dataGridView3.Rows.Count;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sayi; i++)
                if (randevu[i] > 0)
                {
                    string fss_ekle = "insert into hry(h_id,r_id,yer_no) values(@h_id,@r_id,@yer_no)";
                    MySqlCommand komut = new MySqlCommand(fss_ekle, baglanti);
                    komut.Parameters.AddWithValue("@h_id", halı_no);
                    komut.Parameters.AddWithValue("@yer_no", yer_no);
                    komut.Parameters.AddWithValue("@r_id", randevu[i]);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    button5_Click(sender, e);
                }
            Array.Clear(randevu, 0, 100);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string filmGoster = "select h_adı,yer_adi,tarihi from hali,yer,randevu,hry where hali.h_id = hry.h_id and yer.yer_no=hry.yer_no and randevu.r_id = hry.r_id;";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(filmGoster, baglanti);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            baglanti.Open();
            dataGridView4.DataSource = tabloGoster;
            baglanti.Close();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                if (dataGridView4.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    dataGridView4.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            halı_ad = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();
            yer_ad = dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString();
            randevutar = dataGridView4.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int hry_sayi = 0;
            string fss_id = "select hry_id from hry,hali,yer,randevu where hry.h_id = hali.h_id and yer.yer_no = hry.yer_no and hry.r_id =randevu.r_id and h_adı ='" + halı_ad + "' and yer_adi = '" + yer_ad + "' and tarihi ='" + randevutar + "'";
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand(fss_id, baglanti);
            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                hry_sayi = Convert.ToInt32(okuma[0]);
            }
            baglanti.Close();
            string sil = "delete from hry where hry_id = " + hry_sayi;
            baglanti.Open();
            MySqlCommand komutt = new MySqlCommand(sil, baglanti);
            komutt.ExecuteNonQuery();
            baglanti.Close();
            button5_Click(sender, e);
        }
    }
}
