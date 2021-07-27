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
    public partial class haliSatisigiriss : Form
    {
        MySqlConnection baglanti = new MySqlConnection("server=localhost; database=hali;uid=root;pwd=tepe");
        string hali_adi = "";
        string yer_ad = "";
        int hali_id = 0;
        int yer_no = 0;
        int randevu_id = 0;
        public haliSatisigiriss()
        {
            InitializeComponent();
            halii();
        }
        
        public void halii()
        {
            string halilariGetir = "select h_adı,h_modeli,h_uretim_yili,h_fiyat,h_id from hali";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(halilariGetir, baglanti);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            baglanti.Open();
            dataGridView1.DataSource = tabloGoster;
            dataGridView1.Columns[4].Visible = false;
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            hali_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            string hali_adi = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

            string salonlar = "select distinct(yer_adi),yer_kapasitesi,yer.yer_no from yer,hali,hry where hry.h_id = " +
                             "hali.h_id and hry.yer_no=yer.yer_no and h_adı ='" + hali_adi + "'";

            MySqlDataAdapter adaptor = new MySqlDataAdapter(salonlar, baglanti);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            baglanti.Open();
            dataGridView2.DataSource = tabloGoster;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            yer_no = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                if (dataGridView2.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.White;
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

            string yer_ad = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            string hali_adi = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string seans = "select tarihi,gunu,randevu.r_id from hali,yer,randevu,hry where hali.h_id = hry.h_id " +
                              "and yer.yer_no = hry.yer_no and randevu.r_id = hry.r_id and h_adı = '" + hali_adi + "' and yer_adi = '" + yer_ad + "'";

            MySqlDataAdapter adaptor = new MySqlDataAdapter(seans, baglanti);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            baglanti.Open();
            dataGridView3.DataSource = tabloGoster;
            dataGridView3.Columns[3].Visible = false;
            baglanti.Close();
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            randevu_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            haliSatis b = new haliSatis(hali_id, yer_no, randevu_id);
            b.Show();
            this.Hide();
        }
    }
}
