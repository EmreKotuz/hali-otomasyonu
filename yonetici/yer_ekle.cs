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
    public partial class yer_ekle : Form
    {
        MySqlConnection bağlantii = new MySqlConnection("server=localhost;database=hali;uid=root;pwd=tepe");
        public yer_ekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string salon_ekle = "insert into yer values(@yer_no,@yer_adi,@yer_kapasitesi)";
            MySqlCommand komut = new MySqlCommand(salon_ekle, bağlantii);
            komut.Parameters.AddWithValue("@yer_no", textBox1.Text);
            komut.Parameters.AddWithValue("@yer_adi", textBox2.Text);
            komut.Parameters.AddWithValue("@yer_kapasitesi", textBox3.Text);
            bağlantii.Open();
            komut.ExecuteNonQuery();
            bağlantii.Close();

            int haliKapasite = Convert.ToInt32(textBox3.Text);
            int haliYerr = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < haliKapasite; i++)
            {
                string koltukEkle = "insert into haliYer(hyer_no,hyer_durumu,yer_no) values(@hyer_no,@hyer_durumu,@yer_no)";
                MySqlCommand komutKoltuk = new MySqlCommand(koltukEkle, bağlantii);
                komutKoltuk.Parameters.AddWithValue("@hyer_no", i);
                komutKoltuk.Parameters.AddWithValue("@hyer_durumu", 0);
                komutKoltuk.Parameters.AddWithValue("@yer_no", haliYerr);
                bağlantii.Open();
                komutKoltuk.ExecuteNonQuery();
                bağlantii.Close();
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button2_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string yerGoster = "select * from yer";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(yerGoster, bağlantii);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            bağlantii.Open();
            dataGridView1.DataSource = tabloGoster;
            bağlantii.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string yer_sil = "delete from yer where yer_no = @yer_no";
            MySqlCommand komut = new MySqlCommand(yer_sil, bağlantii);
            komut.Parameters.AddWithValue("@yer_no", textBox1.Text);
            bağlantii.Open();
            komut.ExecuteNonQuery();
            bağlantii.Close();
            button2_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string gGuncelle = "update yer set yer_adi = @yer_adi, yer_kapasitesi = @yer_kapasitesi where yer_no = @yer_no";
            MySqlCommand komut = new MySqlCommand(gGuncelle, bağlantii);
            komut.Parameters.AddWithValue("@yer_no",textBox1.Text);
            komut.Parameters.AddWithValue("@yer_adi",textBox2.Text);
            komut.Parameters.AddWithValue("@yer_kapasitesi",textBox3.Text);
            bağlantii.Open();
            komut.ExecuteNonQuery();
            bağlantii.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button2_Click(sender, e);
        }
    }
}
