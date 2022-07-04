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
    public partial class Form2 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port =2351; Database = VTYS_PROJE; " +
            "user ID = postgres ; password = 2351");
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from siparis order by siparis_no asc", baglanti);
            NpgsqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr["siparis_no"].ToString();
                ekle.SubItems.Add(dr["musteri_id"].ToString());
                ekle.SubItems.Add(dr["urun_adi"].ToString());
                ekle.SubItems.Add(dr["satici_id"].ToString());
                ekle.SubItems.Add(dr["Siparis_tarihi"].ToString());
                ekle.SubItems.Add(dr["Siparis_adedi"].ToString());
                ekle.SubItems.Add(dr["toplam_tutar"].ToString());

                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }
    }
}
