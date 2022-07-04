using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace VTYS_PROJE
{
    public partial class frmSiparis : Form
    {
        public frmSiparis()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port =2351; Database = VTYS_PROJE; " +
           "user ID = postgres ; password = 2351");

        private void btnListele_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from siparis order by siparis_no ASC", baglanti);
            NpgsqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr["siparis_no"].ToString();
                ekle.SubItems.Add(dr["musteri_id"].ToString());
                ekle.SubItems.Add(dr["urun_id"].ToString());
                ekle.SubItems.Add(dr["satici_id"].ToString());
                ekle.SubItems.Add(dr["siparis_tarihi"].ToString());
                ekle.SubItems.Add(dr["siparis_adedi"].ToString());
                ekle.SubItems.Add(dr["toplam_tutar"].ToString());

                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into siparis(siparis_no,musteri_id,urun_id,satici_id,siparis_adedi) VALUES (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtSiparisNo.Text));
            komut.Parameters.AddWithValue("@p2", int.Parse(txtMusteriID.Text));
            komut.Parameters.AddWithValue("@p3", int.Parse(txtUrunID.Text));
            komut.Parameters.AddWithValue("@p4", int.Parse(txtSaticiID.Text));
            komut.Parameters.AddWithValue("@p5", int.Parse(txtSiparisAdedi.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("siparis başarıyla eklendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from siparis where siparis_no =@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtSiparisNo.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Siparis başarıyla silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update siparis set siparis_adedi=@p1, where siparis_no=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", txtSiparisAdedi.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(txtSiparisNo.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Siparis başarıyla güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmSiparis_Load(object sender, EventArgs e)
        {

        }
    }
}
