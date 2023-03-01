using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDePharmacie
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            InitializeUI("UIMode");
        }

        //InitializeUI("UIMode");
        private void InitializeUI(string key) 
        {
            try
            {
                var uiMode = ConfigurationManager.AppSettings[key];
                if (uiMode=="light")
                {
                    btnChangeMode.Text = "Enable Dark Mode";
                    //this.ForeColor = Color.FromArgb(255, 255, 255);
                    this.BackColor = Color.White;//rgb(186, 220, 88)
                    ConfigurationManager.AppSettings[key] = "dark";
                }
                else
                {
                    btnChangeMode.Text = "Enable Light Mode";
                    //this.ForeColor = Color.FromArgb(255, 255, 255);
                    this.BackColor = Color.FromArgb(47, 54, 64);
                    ConfigurationManager.AppSettings[key] = "light";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //InitializeUI("UIMode");



        private void bntClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtAdminPassword.Text == "")
            {
                MessageBox.Show("Veuillez inserer le mot de passe de l'Admin :) ");
            }
            else if (txtAdminPassword.Text == "admin" || txtAdminPassword.Text == "Admin")
            {
                Dashboard Obj = new Dashboard();
                this.Hide();
                Obj.Show();
            }
            else
            {
                MessageBox.Show("Mot de passe incorrect :/ ");
                //txtAdminPassword.Text = "";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnChangeMode_Click(object sender, EventArgs e)
        {
            InitializeUI("UIMode");
        }
    }
}
