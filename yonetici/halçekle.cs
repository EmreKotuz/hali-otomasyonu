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
    public partial class halıekle : Form
    {
        MySqlConnection bağlanti = new MySqlConnection("server=localhost; database=hali;uid=root;pwd=tepe");
        public halıekle()
        {
            InitializeComponent();
        }
        string hali_ad = "";
        int hali_id = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            string filmEkle = "insert into hali (h_adı,h_modeli,h_uretim_yili,h_fiyat) values(@h_adı,@h_modeli,@h_uretim_yili,@h_fiyat)";
            MySqlCommand komut = new MySqlCommand(filmEkle, bağlanti);
            komut.Parameters.AddWithValue("@h_adı", textBox1.Text);
            komut.Parameters.AddWithValue("@h_modeli", textBox2.Text);
            komut.Parameters.AddWithValue("@h_uretim_yili", textBox3.Text);
            komut.Parameters.AddWithValue("@h_fiyat", textBox4.Text);

            bağlanti.Open();
            komut.ExecuteNonQuery();
            bağlanti.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            button2_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string halıyıGoster = "select * from hali";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(halıyıGoster, bağlanti);
            DataTable tabloo = new DataTable();
            adaptor.Fill(tabloo);
            bağlanti.Open();
            dataGridView1.DataSource = tabloo;
            bağlanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string hali_sill = "delete from hali where h_id = @h_id";
            MySqlCommand komut = new MySqlCommand(hali_sill, bağlanti);
            komut.Parameters.AddWithValue("@h_id", hali_id);
            bağlanti.Open();
            komut.ExecuteNonQuery();
            bağlanti.Close();
            button2_Click(sender, e);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            hali_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)

                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;

            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string gGuncelle = "update hali set h_adı = @h_adı, h_modeli = @h_modeli,h_uretim_yili =@h_uretim_yili where h_fiyat=@h_fiyat";
            MySqlCommand komut = new MySqlCommand(gGuncelle, bağlanti);
            komut.Parameters.AddWithValue("@h_adı", textBox1.Text);
            komut.Parameters.AddWithValue("@h_modeli", textBox2.Text);
            komut.Parameters.AddWithValue("@h_uretim_yili", textBox3.Text);
            komut.Parameters.AddWithValue("@h_fiyat", textBox4.Text);
            bağlanti.Open();
            komut.ExecuteNonQuery();
            bağlanti.Close();
            button2_Click(sender, e);
        }
        }
    }

