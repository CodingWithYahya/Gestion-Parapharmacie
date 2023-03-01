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
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
            ShowCustomer();
            InitializeUI("UIMode");
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\source\repos\GestionDePharmacie\GestionDePharmacie\PharmacieDB.mdf;Integrated Security=True");
        int Key = 0;

        private void bntClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void btnVendeurs_Click(object sender, EventArgs e)
        {
            Vendeurs Obj = new Vendeurs();
            Obj.Show();
            this.Hide();
        }

        private void goVendeurs_Click(object sender, EventArgs e)
        {
            Vendeurs Obj = new Vendeurs();
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

        private void ShowCustomer()
        {
            Con.Open();
            string Query = "Select * from ClientTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVClients.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            txtNomDuClient.Text = "";
            txtAddresse.Text = "";
            txtTelephone.Text = "";
            txtGenre.SelectedIndex = 0;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (txtNomDuClient.Text == "" || txtAddresse.Text == "" || txtTelephone.Text == "" || txtGenre.SelectedIndex == -1)
            {
                MessageBox.Show("Information insuffisante");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ClientTbl(nom_client,addresse_client,tel_client,dob_client,genre_client)values(@CN,@CA,@CMN,@CD,@CG)", Con);
                    cmd.Parameters.AddWithValue("@CN", txtNomDuClient.Text);
                    cmd.Parameters.AddWithValue("@CA", txtAddresse.Text);
                    cmd.Parameters.AddWithValue("@CMN", txtTelephone.Text);
                    cmd.Parameters.AddWithValue("@CD", txtDateNaissance.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", txtGenre.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Client ajoute avec succes !");
                    Con.Close();
                    ShowCustomer();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (txtNomDuClient.Text == "" || txtAddresse.Text == "" || txtTelephone.Text == "" || txtGenre.SelectedIndex == -1)
            {
                MessageBox.Show("Information insuffisante");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ClientTbl Set nom_client=@CN,addresse_client=@CA,tel_client=@CMN,dob_client=@CD,genre_client=@CG where Id_client=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CN", txtNomDuClient.Text);
                    cmd.Parameters.AddWithValue("@CA", txtAddresse.Text);
                    cmd.Parameters.AddWithValue("@CMN", txtTelephone.Text);
                    cmd.Parameters.AddWithValue("@CD", txtDateNaissance.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", txtGenre.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Client modifie avec succes !");
                    Con.Close();
                    ShowCustomer();
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
                MessageBox.Show("Select the Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ClientTbl where Id_client=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Client supprime avec succes !");
                    Con.Close();
                    ShowCustomer();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void DGVClients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNomDuClient.Text = DGVClients.SelectedRows[0].Cells[1].Value.ToString();
            txtAddresse.Text = DGVClients.SelectedRows[0].Cells[2].Value.ToString();
            txtTelephone.Text = DGVClients.SelectedRows[0].Cells[3].Value.ToString();
            txtDateNaissance.Text = DGVClients.SelectedRows[0].Cells[4].Value.ToString();
            txtGenre.SelectedItem = DGVClients.SelectedRows[0].Cells[5].Value.ToString();
            if (txtNomDuClient.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DGVClients.SelectedRows[0].Cells[0].Value.ToString());
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
