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
    public partial class halıRandevu : Form
    {
        MySqlConnection bağlantı = new MySqlConnection("server=localhost;database=hali;uid=root;pwd=tepe");
        int randevid= 0;
        public halıRandevu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string zaman_ekle = "insert into randevu (tarihi, gunu) values(@tarihi,@gunu)";
            MySqlCommand komut = new MySqlCommand(zaman_ekle, bağlantı);
            komut.Parameters.AddWithValue("@tarihi", textBox1.Text);
            komut.Parameters.AddWithValue("@gunu", textBox2.Text);

            bağlantı.Open();
            komut.ExecuteNonQuery();
            bağlantı.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            button2_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string randevuu = "select * from randevu";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(randevuu, bağlantı);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            bağlantı.Open();
            dataGridView1.DataSource = tabloGoster;
            bağlantı.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string seans_sil = "delete from randevu where r_id = @r_id";
            MySqlCommand komut = new MySqlCommand(seans_sil, bağlantı);
            komut.Parameters.AddWithValue("@r_id", randevid);
            bağlantı.Open();
            komut.ExecuteNonQuery();
            bağlantı.Close();
            button2_Click(sender, e);
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            randevid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string seansGuncelle = "update randevu set tarihi=@tarihi, gunu = @gunu where r_id = @r_id";
            MySqlCommand komut = new MySqlCommand(seansGuncelle, bağlantı);
            komut.Parameters.AddWithValue("@r_id", randevid);
            komut.Parameters.AddWithValue("@tarihi", textBox1.Text);
            komut.Parameters.AddWithValue("@gunu", textBox2.Text);

            bağlantı.Open();
            komut.ExecuteNonQuery();
            bağlantı.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            button2_Click(sender, e);
        }
    }
}
