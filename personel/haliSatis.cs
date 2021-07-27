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
    public partial class haliSatis : Form
    {
        MySqlConnection baglanti = new MySqlConnection("server=localhost; database=hali;uid=root;pwd=tepe");

        int hali_id = 0;

        int yer_no = 0;

        int randevu_id = 0;
        int saatSayisi_no = 0;
        int yer_sayisi = 0;

        Button[] randevu_dizisi;

        int xkoordinat = 300;
        int yKoordinat = 50;
        public haliSatis()
        {
            InitializeComponent();
        }

         public haliSatis(int hali,int yer,int randevu)
        {
            InitializeComponent();
            hali_id = hali;
            yer_no = yer;
            randevu_id = randevu;
            toplamgun();
            satilanHali();
            saatKontrol();
        }

        public void saatKontrol()
        {
            string şimdikiSaat = DateTime.Now.ToString("HH:mm");

            string seansSaati = "";

            string Bilgi = "select gunu from randevu where r_id = " +randevu_id;
            baglanti.Open();

            MySqlCommand komut = new MySqlCommand(Bilgi, baglanti);
            MySqlDataReader okuma = komut.ExecuteReader();

            while (okuma.Read())
            {
                seansSaati = okuma[0].ToString();
            }

            baglanti.Close();

            TimeSpan şimdi = TimeSpan.Parse(şimdikiSaat);
            TimeSpan seans = TimeSpan.Parse(seansSaati);

            if (şimdi.CompareTo(seans) == 1)
            {
                MessageBox.Show("Şimdiki saat daha büyüktür");
                for (int i = 0; i < yer_sayisi; i++)
                {
                    randevu_dizisi[i].Enabled = false;
                    randevu_dizisi[i].BackColor = Color.Blue;
                }
                MessageBox.Show("Gün saati geçmiştir");
            }
            if (şimdi.CompareTo(seans) == -1)
                MessageBox.Show("Gün saati daha büyüktür");
            if (şimdi.CompareTo(seans) == 0)
                MessageBox.Show("Saatler eşittir");



        }

        public void satilanHali()
        {

            string satilanHali = "select r_id from halisatis where h_id = " + hali_id + " and yer_no = " + yer_no + " and r_id = " + randevu_id;

            baglanti.Open();

            MySqlCommand komut = new MySqlCommand(satilanHali, baglanti);
            MySqlDataReader okuma = komut.ExecuteReader();

            while (okuma.Read())
            {
                randevu_dizisi[Convert.ToInt32(okuma[0])].BackColor = Color.Blue;
                randevu_dizisi[Convert.ToInt32(okuma[0])].Enabled = false;
            }

            baglanti.Close();

        }


        public void toplamgun()
        {
            string k_sayı = "select count(yer_kapasitesi) from randevu where yer_no = " + yer_no;

            MySqlCommand komut = new MySqlCommand(k_sayı, baglanti);

            baglanti.Open();

            MySqlDataReader okuma = komut.ExecuteReader();

            while (okuma.Read())
            {
                yer_sayisi = Convert.ToInt32(okuma[0]);
            }

            baglanti.Close();

            gunn();
        }

        public void gunn()
        {
            randevu_dizisi = new Button[yer_sayisi];


            for (int i = 1; i < yer_sayisi; i++)
                randevu_dizisi[i] = new Button();
            int n = 1;
            while (n<yer_sayisi)
            {
                randevu_dizisi[n].Text = (n + 1).ToString();
                randevu_dizisi[n].BackColor = Color.Red;

            
                randevu_dizisi[n].Height = 30;
                randevu_dizisi[n].Width = 50;
                randevu_dizisi[n].Left = xkoordinat;
                randevu_dizisi[n].Top = yKoordinat;

                xkoordinat = xkoordinat + randevu_dizisi[n].Width+30;

                randevu_dizisi[n].Click += new EventHandler(gunRengi);


                Controls.Add(randevu_dizisi[n]);
                if (n % 5 == 0)
                {
                    xkoordinat = 300;
                    yKoordinat = yKoordinat + randevu_dizisi[n].Height + 10;
                }
                n++;
            }


        }

        public void gunRengi(object gönderici, EventArgs yakalayıcı)
        {

            //Bu for döngüsü ile daha önce sadece seçilen ama satılmayan
            //butonların renkleri ilk renklerine dönderiliyor
            for (int i = 0; i < yer_sayisi; i++)
                if (randevu_dizisi[i].BackColor == Color.Green)
                    randevu_dizisi[i].BackColor = Color.Red;

            //Bu for döngüsü ile en son seçilen butonun rengi sadece
            //değiştiriliyor
            for (int i = 0; i < yer_sayisi; i++)
                if (gönderici.Equals(randevu_dizisi[i]))
                {
                    randevu_dizisi[i].BackColor = Color.Green;
                    saatSayisi_no = i;
                }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string haliSatis = "insert into halisatis(hali_id,yer_no,randevu_id,yer_no,fiyat,tarih) values(@hali_id,@yer_no,@randevu_id,@yer_no,@fiyat,@tarih)";
            MySqlCommand komut = new MySqlCommand(haliSatis, baglanti);
            komut.Parameters.AddWithValue("@hali_id", hali_id);
            komut.Parameters.AddWithValue("@yer_no", yer_no);
            komut.Parameters.AddWithValue("@randevu_id", randevu_id);
            komut.Parameters.AddWithValue("@yer_no", saatSayisi_no);
            komut.Parameters.AddWithValue("@fiyat", Convert.ToInt32(label1.Text));
            komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

        }     

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date < DateTime.Now.Date)
            {
                for (int i = 1; i < yer_sayisi; i++)
                {
                    randevu_dizisi[i].Enabled = false;
                    randevu_dizisi[i].BackColor = Color.Blue;
                }
            }
            else if(dateTimePicker1.Value.Date >DateTime.Now.Date)
            {
                for (int i = 1;i < yer_sayisi; i++)
                {
                    randevu_dizisi[i].Enabled = false;
                    randevu_dizisi[i].BackColor = Color.Blue;
                }
            }
            else
            {
                for (int i = 1; i < yer_sayisi; i++)
                {
                    randevu_dizisi[i].Enabled = false;
                    randevu_dizisi[i].BackColor = Color.Red;
                }
                saatKontrol();
            }
        }

        private void haliSatis_Load(object sender, EventArgs e)
        {

        }

    }
}
