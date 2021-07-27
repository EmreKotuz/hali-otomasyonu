using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class yöneticigirisyaptı : Form
    {
        public yöneticigirisyaptı()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            halıekle n = new halıekle();
            n.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            yer_ekle nn = new yer_ekle();
            nn.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            halıRandevu nnn = new halıRandevu();
            nnn.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            genel n = new genel();
            n.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            personelekle n = new personelekle();
            n.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            yonetici.ciro n = new yonetici.ciro();
            n.Show();
            this.Hide();
        }
    }
}
