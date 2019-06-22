using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Siguria_Projekti1._0

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void runQuery()
        {
            string query = "";
            if (txtBinar.Text != "" && txtPartia.Text != "")
            {

                string teksti = txtPartia.Text.Replace("\\", "\\\\");
                teksti = teksti.Replace("'", "''");
                string tekstiEnkriptuar = txtBinar.Text.Replace("\\", "\\\\");
                tekstiEnkriptuar = tekstiEnkriptuar.Replace("'", "''");
                query = "insert into onetimepad(`partia`,`parulla`) values " +
                    "('" + teksti + "','" + tekstiEnkriptuar + "')";
            }
            else
            {
                MessageBox.Show("Ju lutem mbushni te dhenat e kerkuara!");
                return;
            }

            string Conn = "datasource=127.0.0.1;port=3306;username=root;password=root;database=dbonetimepad;Sslmode=none";

            MySqlConnection databaseConnection = new MySqlConnection(Conn);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                MessageBox.Show("Query succesful");
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Query error" + ex.Message);
            }
        }

        Random rand = new Random();
        public const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public char[] generateKey(int size)
        {
            char[] key = new char[size];
            for(int i =0;i<key.Length;i++)
            {
                key[i] = chars[rand.Next(chars.Length)];
            }
            return key;
        }



        private void btnGjuaje_Click(object sender, EventArgs e)
        {
            try
            { 
                string fjalia = txtTexti.Text;
                int size = txtTexti.Text.Length;
                char[] key = generateKey(size);
                String celesi = new String(key);
                txtBinar.Text = enkripto(fjalia, celesi);
                string textiDekriptuar = enkripto(txtBinar.Text, celesi);
                txtTextiDekriptuar.Text = textiDekriptuar;
                while (textiDekriptuar != fjalia)
                {
                    key = generateKey(size);
                    celesi = new String(key);
                    txtBinar.Text = enkripto(fjalia, celesi);
                    textiDekriptuar = enkripto(txtBinar.Text, celesi);
                    txtTextiDekriptuar.Text = textiDekriptuar;
                }
                txtCelesi.Text = celesi;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string enkripto(string plaintexti,string celesi)
        {
            
            StringBuilder objekti = new StringBuilder(plaintexti);
            for(int i=0;i<plaintexti.Length;i++)
            {
                objekti[i] = (char)(plaintexti[i] ^ celesi[i]);

            }
                return objekti.ToString();
            
            

        }
        
        private void btnShto_Click(object sender, EventArgs e)
        {
            
            runQuery();
            txtTexti.Text = "";
            txtBinar.Text = "";
            txtPartia.Text = "";
            txtTextiDekriptuar.Text = "";
            txtCelesi.Text = "";
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void parullatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 objFrm = new Form2();
            objFrm.Show();
        }

        private void viewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }
    }
}
