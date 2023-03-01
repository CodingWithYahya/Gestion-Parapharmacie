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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountMedicine();
            CountCustomer();
            CountSeller();
            
            GetBestCustomer();
            GetBestSeller(); 
            InitializeUI("UIMode");
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\source\repos\GestionDePharmacie\GestionDePharmacie\PharmacieDB.mdf;Integrated Security=True");
        int Key = 0;


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void goLogout_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
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

        private void CountMedicine()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from MedicamentTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LblMédicaments.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCustomer()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ClientTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LblClients.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountSeller()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from VendeurTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LblVendeurs.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        /*private void SumAmount()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(montant_facture) from FactureTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LblMontantDeVente.Text = "Dhs " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        
        private void SumAmountBySellers()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(montant_facture) from FactureTbl where nom_vendeur='" + txtVentesParVendeur.SelectedValue.ToString() + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LblVentesParVendeur.Text = "Dhs " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        
        
        private void GetSeller()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select nom_vendeur from VendeurTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("nom_vendeur", typeof(string));
            dt.Load(Rdr);
            txtVentesParVendeur.ValueMember = "nom_vendeur";
            txtVentesParVendeur.DataSource = dt;
            Con.Close();
        }*/
        private void GetBestCustomer()
        {
            try
            {
                Con.Open();
                string InnerQuery = "select Max(montant_facture) from FactureTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, Con);
                sda1.Fill(dt1);
                string Query = "select nom_client from FactureTbl where montant_facture = '" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                LblMeilleurClient.Text = dt.Rows[0][0].ToString();
                Con.Close();

            }
            catch (Exception Ex)
            {
                Con.Close();
            }

        }
        private void GetBestSeller()
        {
            try
            {
                Con.Open();
                string InnerQuery = "select Max(montant_facture) from FactureTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, Con);
                sda1.Fill(dt1);
                string Query = "select nom_vendeur from FactureTbl where montant_facture = '" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                LblVendeur.Text = dt.Rows[0][0].ToString();
                Con.Close();

            }
            catch (Exception Ex)
            {
                Con.Close();
            }

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void txtVentesParVendeur_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //SumAmountBySellers();
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
        //panel1.BackColor = Color.White;
        //panel1.BackColor = Color.DarkSlateGray;
        private void btnChangeMode_Click(object sender, EventArgs e)
        {
            InitializeUI("UIMode");
        }

        private void LblMontantDeVente_Click(object sender, EventArgs e)
        {

        }
    }
}
