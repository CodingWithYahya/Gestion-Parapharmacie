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
    public partial class Producteur : Form
    {
        public Producteur()
        {
            InitializeComponent();
            ShowManufecturer();
            InitializeUI("UIMode");
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\source\repos\GestionDePharmacie\GestionDePharmacie\PharmacieDB.mdf;Integrated Security=True");

        private void ShowManufecturer()
        {
            Con.Open();
            string Query = "Select * from ProducteurTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVProducteurs.DataSource = ds.Tables[0];
            Con.Close();
        }
        int Key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNomDuProducteur.Text = DGVProducteurs.SelectedRows[0].Cells[1].Value.ToString();
            txtAddresse.Text = DGVProducteurs.SelectedRows[0].Cells[2].Value.ToString();
            txtTelephone.Text = DGVProducteurs.SelectedRows[0].Cells[3].Value.ToString();
            txtDate.Text = DGVProducteurs.SelectedRows[0].Cells[4].Value.ToString();
            if (txtNomDuProducteur.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DGVProducteurs.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

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

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (txtNomDuProducteur.Text == "" || txtAddresse.Text == "" || txtTelephone.Text == "")
            {
                MessageBox.Show(" Information insuffisante");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ProducteurTbl(nom_producteur,addresse_producteur,tel_producteur,date_producteur)values(@MN,@MA,@MMN,@MD)", Con);
                    cmd.Parameters.AddWithValue("@MN", txtNomDuProducteur.Text);
                    cmd.Parameters.AddWithValue("@MA", txtAddresse.Text);
                    cmd.Parameters.AddWithValue("@MMN", txtTelephone.Text);
                    cmd.Parameters.AddWithValue("@MD", txtDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producteur ajoute avec succes !");
                    Con.Close();
                    ShowManufecturer();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Reset()
        {
            txtNomDuProducteur.Text = "";
            txtAddresse.Text = "";
            txtTelephone.Text = "";
            Key = 0;
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (txtNomDuProducteur.Text == "" || txtAddresse.Text == "" || txtTelephone.Text == "")
            {
                MessageBox.Show("Information insuffisante ! ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProducteurTbl Set nom_producteur=@MN,addresse_producteur=@MA,tel_producteur=@MMN,date_producteur=@MD where Id_producteur=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MN", txtNomDuProducteur.Text);
                    cmd.Parameters.AddWithValue("@MA", txtAddresse.Text);
                    cmd.Parameters.AddWithValue("@MMN", txtTelephone.Text);
                    cmd.Parameters.AddWithValue("@MD", txtDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producteur modifie avec succes !");
                    Con.Close();
                    ShowManufecturer();
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
                MessageBox.Show("Selectionner le producteur");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ProducteurTbl where Id_producteur=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producteur supprime avec succes ! ");
                    Con.Close();
                    ShowManufecturer();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        //
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
                    label1.ForeColor = Color.LimeGreen;
                    panel1.BackColor = Color.White;
                    //panel1.BackColor = Color.DarkSlateGray;

                }
                else
                {
                    btnChangeMode.Text = "Enable Light Mode";
                    //this.ForeColor = Color.FromArgb(255, 255, 255);
                    this.BackColor = Color.FromArgb(113, 128, 147);
                    ConfigurationManager.AppSettings[key] = "light";
                    label1.ForeColor = Color.LimeGreen;
                    //panel1.BackColor = Color.White;
                    panel1.BackColor = Color.DarkSlateGray;

                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //
        private void btnChangeMode_Click(object sender, EventArgs e)
        {
            InitializeUI("UIMode");
        }
    }
}
