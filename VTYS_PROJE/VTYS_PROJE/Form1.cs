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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port =2351; Database = VTYS_PROJE; " +
            "user ID = postgres ; password = 2351");
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from urun order by urun_id asc", baglanti);
            NpgsqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr["urun_id"].ToString();
                ekle.SubItems.Add(dr["marka_id"].ToString());
                ekle.SubItems.Add(dr["urun_adi"].ToString());
                ekle.SubItems.Add(dr["stok_miktari"].ToString());
                ekle.SubItems.Add(dr["urun_fiyati"].ToString());
                ekle.SubItems.Add(dr["kategori_id"].ToString());

                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into urun(urun_id,marka_id,urun_adi,stok_miktari,urun_fiyati,kategori_id) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txturunID.Text));
            komut.Parameters.AddWithValue("@p2", int.Parse(txtmarkaADİ.Text));
            komut.Parameters.AddWithValue("@p3", txturunADİ.Text);
            komut.Parameters.AddWithValue("@p4", int.Parse(txtstokMİKTARİ.Text));
            komut.Parameters.AddWithValue("@p5", int.Parse(txturunFİYATİ.Text));
            komut.Parameters.AddWithValue("@p6", int.Parse(txtkategoriID.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün başarıyla eklendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from urun where urun_id =@p1",baglanti);
            komut.Parameters.AddWithValue("@p1",int.Parse(txturunID.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün başarıyla silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update urun set urun_adi=@p1,stok_miktari=@p2,urun_fiyati=@p3 where urun_id=@p4",baglanti);
            komut.Parameters.AddWithValue("@p1", txturunADİ.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(txtstokMİKTARİ.Text));
            komut.Parameters.AddWithValue("@p3", int.Parse(txturunFİYATİ.Text));
            komut.Parameters.AddWithValue("@p4", int.Parse(txturunID.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün başarıyla güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from urun where urun_adi like '%" + txtAra.Text + "%'", baglanti);
            NpgsqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {   
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr["urun_id"].ToString();
                ekle.SubItems.Add(dr["marka_id"].ToString());
                ekle.SubItems.Add(dr["urun_adi"].ToString());
                ekle.SubItems.Add(dr["stok_miktari"].ToString());
                ekle.SubItems.Add(dr["urun_fiyati"].ToString());
                ekle.SubItems.Add(dr["kategori_id"].ToString());

                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }
    }
}
