using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDePharmacie
{
    public partial class Login : Form
    {
        //string parametres = "SERVER=127.0.0.1; DATABASE=emsi_pharmacie; UID=root; PASSWORD=";
        //private MySqlConnection maconnexion;
        //DataTable dataTable = new DataTable();
        //int currRowIndex;

        public Login()
        {
            InitializeComponent();
            InitializeUI("UIMode");
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\source\repos\GestionDePharmacie\GestionDePharmacie\PharmacieDB.mdf;Integrated Security=True");
        

        private void bntClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public static string User;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Enter Username and Password to Login");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from VendeurTbl where nom_vendeur='" + txtUsername.Text + "' and password_vendeur='" + txtPassword.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    User = txtUsername.Text;
                    Ventes Obj = new Ventes();
                    Obj.Show();
                    this.Hide();
                    Con.Close();
                }
                else
                {
                    MessageBox.Show("You have entered wrong Username and Password");
                }
                Con.Close();
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Admin Obj = new Admin();
            this.Hide();
            Obj.Show();
        }

        //InitializeUI("UIMode");
        private void InitializeUI(string key)
        {
            try
            {
                var uiMode = ConfigurationManager.AppSettings[key];
                if (uiMode == "light")
                {
                    btnChangeMode.Text = "Enable Dark Mode";
                    //this.ForeColor = Color.FromArgb(255, 255, 255);
                    this.BackColor = Color.FromArgb(245, 246, 250);
                    ConfigurationManager.AppSettings[key] = "dark";

                    //label1.ForeColor = Color.White;
                    
                }
                else
                {
                    btnChangeMode.Text = "Enable Light Mode";
                    //this.ForeColor = Color.FromArgb(255, 255, 255);
                    this.BackColor = Color.FromArgb(113, 128, 147);
                    ConfigurationManager.AppSettings[key] = "light";
                    //label1.ForeColor = Color.LimeGreen;
                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //InitializeUI("UIMode");
        private void btnChangeMode_Click(object sender, EventArgs e)
        {
            InitializeUI("UIMode");
        }
    }
}
