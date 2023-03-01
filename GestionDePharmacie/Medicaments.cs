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
    public partial class Medicaments : Form
    {
        public Medicaments()
        {
            InitializeComponent();
            ShowMedicine();
            GetManufecturer();
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

        private void ShowMedicine()
        {
            Con.Open();
            string Query = "Select * from MedicamentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVMédicaments.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            txtNom.Text = "";
            txtType.SelectedIndex = 0;
            txtQuantite.Text = "";
            txtPrice.Text = "";
            txtNomDuProducteur.Text = "";
            Key = 0;
        }
        private void GetManufecturer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select Id_producteur from ProducteurTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("id_producteur", typeof(int));
            dt.Load(Rdr);
            txtProducteurId.ValueMember = "id_producteur";
            txtProducteurId.DataSource = dt;
            Con.Close();
        }
        private void GetManufecturerName()
        {
            Con.Open();
            string Query = "Select * from ProducteurTbl where id_producteur='" + txtProducteurId.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtNomDuProducteur.Text = dr["nom_producteur"].ToString();
            }
            Con.Close();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (txtNom.Text == "" || txtType.SelectedIndex == -1 || txtQuantite.Text == "" || txtPrice.Text == "" || txtProducteurId.SelectedIndex == -1 || txtNomDuProducteur.Text == "")
            {
                MessageBox.Show("Information insuffisante");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into MedicamentTbl(nom_medicament,type_medicament,quantite_medicament,prix_medicament,id_producteur,nom_producteur)values(@MN,@MT,@MQ,@MP,@MMI,@MM)", Con);
                    cmd.Parameters.AddWithValue("@MN", txtNom.Text);
                    cmd.Parameters.AddWithValue("@MT", txtType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", txtQuantite.Text);
                    cmd.Parameters.AddWithValue("@MP", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@MMI", txtProducteurId.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MM", txtNomDuProducteur.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicament ajoute avec succes !");
                    Con.Close();
                    ShowMedicine();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void txtNomDuProducteur_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetManufecturerName();
        }

        private void DGVMédicaments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtNom.Text = DGVMédicaments.SelectedRows[0].Cells[1].Value.ToString();
            txtType.SelectedItem = DGVMédicaments.SelectedRows[0].Cells[2].Value.ToString();
            txtQuantite.Text = DGVMédicaments.SelectedRows[0].Cells[3].Value.ToString();
            txtPrice.Text = DGVMédicaments.SelectedRows[0].Cells[4].Value.ToString();
            txtProducteurId.SelectedValue = DGVMédicaments.SelectedRows[0].Cells[5].Value.ToString();
            txtNomDuProducteur.Text = DGVMédicaments.SelectedRows[0].Cells[6].Value.ToString();
            if (txtNom.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DGVMédicaments.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Selectionner le medicament !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from MedicamentTbl where Id_medicament=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicament supprimé avec succes !");
                    Con.Close();
                    ShowMedicine();
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
            if (txtNom.Text == "" || txtType.SelectedIndex == -1 || txtQuantite.Text == "" || txtPrice.Text == "" || txtProducteurId.SelectedIndex == -1 || txtNomDuProducteur.Text == "")
            {
                MessageBox.Show("Information insuffisante");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update MedicamentTbl Set nom_medicament=@MN,type_medicament=@MT,quantite_medicament=@MQ,prix_medicament=@MP,id_producteur=@MMI,nom_producteur=@MM where Id_medicament=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MN", txtNom.Text); 
                    cmd.Parameters.AddWithValue("@MT", txtType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", txtQuantite.Text);
                    cmd.Parameters.AddWithValue("@MP", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@MMI", txtProducteurId.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MM", txtNomDuProducteur.Text);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicament modifié avec succes !");
                    Con.Close();
                    ShowMedicine();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void txtProducteurId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetManufecturerName();
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
        //
        //
        private void btnChangeMode_Click(object sender, EventArgs e)
        {
            InitializeUI("UIMode");
        }
    }
}
