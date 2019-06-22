using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Siguria_Projekti1._0
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        private void runQuery()
        {
            string query = "";
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
            Dictionary<string, string> column;


            if (txtPartia.Text != "" )
            {

                string teksti = txtPartia.Text.Replace("\\", "\\\\");
                teksti = teksti.Replace("'", "''");
                query = "select * from onetimepad where Partia='" + teksti + "'";
            }
            else
            {
                MessageBox.Show("Ju lutem mbushni te dhenat!");
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
                if (myReader.HasRows)
                {
                  
                    while (myReader.Read())
                    {
                        column = new Dictionary<string, string>();

                        column["ID"] = myReader["ID"].ToString();
                        column["Partia"] = myReader["Partia"].ToString();
                        column["Parulla"] = myReader["Parulla"].ToString();
                        rows.Add(column);
                    }
                    myReader.Close();
                }

                txtParulla.Text = "";
                
                    foreach (Dictionary<string, string> column1 in rows)
                {
                    txtParulla.Text = txtParulla.Text + column1["ID"] + "\t";
                    txtParulla.Text = txtParulla.Text + column1["Partia"] + "\t\t";
                    txtParulla.Text = txtParulla.Text + column1["Parulla"] + "\t";
                    txtParulla.Text = txtParulla.Text + System.Environment.NewLine;
                }
                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Query error" + ex.Message);
            }
        }

        private void btnShiko_Click(object sender, EventArgs e)
        {
            runQuery();
        }
        public string dekripto(string ciphertexti, string celesi)
        {

            StringBuilder objekti = new StringBuilder(ciphertexti);
            for (int i = 0; i < ciphertexti.Length; i++)
            {
                objekti[i] = (char)(ciphertexti[i] ^ celesi[i]);

            }
            return objekti.ToString();



        }

        private void btnDekripto_Click(object sender, EventArgs e)
        {
            if(txtId.Text != "" && txtCelesi.Text != "")
            {
                try
                {
                    string teksti = txtId.Text.Replace("\\", "\\\\");
                    teksti = teksti.Replace("'", "''");
                    string query = "select parulla from onetimepad where id='" + teksti + "'";
                    string Conn = "datasource=127.0.0.1;port=3306;username=root;password=root;database=dbonetimepad;Sslmode=none";

                    MySqlConnection databaseConnection = new MySqlConnection(Conn);

                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    commandDatabase.CommandTimeout = 60;
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    string parulla = "";
                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            parulla = myReader.GetString(0);
                        }
                    }
                    string celesi = txtCelesi.Text;
                    string parullaDekriptuar = dekripto(parulla, celesi);
                    txt.Text = parullaDekriptuar;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ju lutem mbushni te dhenat e kerkuara!");
            }
        }
        
    }
}
