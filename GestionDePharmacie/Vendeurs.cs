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
    public partial class Vendeurs : Form
    {
        public Vendeurs()
        {
            InitializeComponent();
            ShowSeller();
            InitializeUI("UIMode");
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\source\repos\GestionDePharmacie\GestionDePharmacie\PharmacieDB.mdf;Integrated Security=True");

        private void bntClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ShowSeller()
        {
            Con.Open();
            string Query = "Select * from VendeurTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVVendeurs.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            txtNomDuVendeur.Text = "";
            txtAddresse.Text = "";
            txtTelephone.Text = "";
            txtGenre.SelectedIndex = 0;
            txtPassword.Text = "";
            Key = 0;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void goDashboard_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void btnProducteurs_Click(object sender, EventArgs e)
        {
            Producteur Obj = new Producteur();
            Obj.Show();
            this.Hide();
        }

        private void goProducteur_Click(object sender, EventArgs e)
        {
            Producteur Obj = new Producteur();
            Obj.Show();
            this.Hide();
        }

        private void btnMédicaments_Click(object sender, EventArgs e)
        {
            Medicaments Obj = new Medicaments();
            Obj.Show();
            this.Hide();
        }

        private void goMédicaments_Click(object sender, EventArgs e)
        {
            Medicaments Obj = new Medicaments();
            Obj.Show();
            this.Hide();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            Clients Obj = new Clients();
            Obj.Show();
            this.Hide();
        }

        private void goClients_Click(object sender, EventArgs e)
        {
            Clients Obj = new Clients();
            Obj.Show();
            this.Hide();
        }

        private void btnVentes_Click(object sender, EventArgs e)
        {
            Ventes Obj = new Ventes();
            Obj.Show();
            this.Hide();
        }

        private void goVentes_Click(object sender, EventArgs e)
        {
            Ventes Obj = new Ventes();
            Obj.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void goLogout_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (txtNomDuVendeur.Text == "" || txtAddresse.Text == "" || txtTelephone.Text == "" || txtGenre.SelectedIndex == -1 || txtPassword.Text == "")
            {
                MessageBox.Show("Information incomplete !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into VendeurTbl(nom_vendeur,addresse_vendeur,tel_vendeur,dob_vendeur,genre_vendeur,password_vendeur)values(@SN,@SA,@SMN,@SD,@SG,@SP)", Con);
                    cmd.Parameters.AddWithValue("@SN", txtNomDuVendeur.Text);
                    cmd.Parameters.AddWithValue("@SA", txtAddresse.Text);
                    cmd.Parameters.AddWithValue("@SMN", txtTelephone.Text);
                    cmd.Parameters.AddWithValue("@SD", txtDateNaissance.Value.Date);
                    cmd.Parameters.AddWithValue("@SG", txtGenre.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SP", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vendeur ajouté avec succes !");
                    Con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (txtNomDuVendeur.Text == "" || txtAddresse.Text == "" || txtTelephone.Text == "" || txtGenre.SelectedIndex == -1 || txtPassword.Text == "")
            {
                MessageBox.Show("Information incomplete !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update vendeurTbl Set nom_vendeur=@SN,addresse_vendeur=@SA,tel_vendeur=@SMN,dob_vendeur=@SD,genre_vendeur=@SG,password_vendeur=@SP where Id_vendeur=@SKey", Con);
                    cmd.Parameters.AddWithValue("@SN", txtNomDuVendeur.Text);
                    cmd.Parameters.AddWithValue("@SA", txtAddresse.Text);
                    cmd.Parameters.AddWithValue("@SMN", txtTelephone.Text);
                    cmd.Parameters.AddWithValue("@SD", txtDateNaissance.Value.Date);
                    cmd.Parameters.AddWithValue("@SG", txtGenre.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SP", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vendeur modifié avec succes !");
                    Con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Veuillez selectionner un Vendeur !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from VendeurTbl where Id_vendeur=@SKey", Con);
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vendeur Bien supprimé !");
                    Con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DGVVendeurs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNomDuVendeur.Text = DGVVendeurs.SelectedRows[0].Cells[1].Value.ToString();
            txtAddresse.Text = DGVVendeurs.SelectedRows[0].Cells[2].Value.ToString();
            txtTelephone.Text = DGVVendeurs.SelectedRows[0].Cells[3].Value.ToString();
            txtDateNaissance.Text = DGVVendeurs.SelectedRows[0].Cells[4].Value.ToString();
            txtGenre.SelectedItem = DGVVendeurs.SelectedRows[0].Cells[5].Value.ToString();
            txtPassword.Text = DGVVendeurs.SelectedRows[0].Cells[6].Value.ToString();
            if (txtNomDuVendeur.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DGVVendeurs.SelectedRows[0].Cells[0].Value.ToString());
            }
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
                    panel1.BackColor = Color.White;
                    label1.ForeColor = Color.LimeGreen;
                }
                else
                {
                    btnChangeMode.Text = "Enable Light Mode";
                    //this.ForeColor = Color.FromArgb(255, 255, 255);
                    this.BackColor = Color.FromArgb(113, 128, 147);
                    ConfigurationManager.AppSettings[key] = "light";
                    label1.ForeColor = Color.LimeGreen;
                    panel1.BackColor = Color.DarkSlateGray;
                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //InitializeUI("UIMode");
        //
        //
        private void btnChangeMode_Click(object sender, EventArgs e)
        {
            InitializeUI("UIMode");
        }
    }
}
