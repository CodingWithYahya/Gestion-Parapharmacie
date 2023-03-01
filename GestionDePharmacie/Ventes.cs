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
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace GestionDePharmacie
{
    public partial class Ventes : Form
    {
        public Ventes()
        {
            //label11.Enabled=false;
            InitializeComponent();
            ShowMedicine();
            ShowFacture();
            //ShowBill();
            GetCustomer();
            LblSellerName.Text = Login.User;
            InitializeUI("UIMode");
        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\source\repos\GestionDePharmacie\GestionDePharmacie\PharmacieDB.mdf;Integrated Security=True");
        int Key = 0;

        private void bntClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select Id_client from ClientTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id_client", typeof(int));
            dt.Load(Rdr);
            txtIdDuClient.ValueMember = "Id_client";
            txtIdDuClient.DataSource = dt;
            Con.Close();
        }
        private void GetCustomerName()
        {
            Con.Open();
            string Query = "Select * from ClientTbl where Id_client='" + txtIdDuClient.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtNomDuClient.Text = dr["nom_client"].ToString();
            }
            Con.Close();
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void UpdateQnty()
        {
            string Stock;
            Stock = DGVMedicaments.SelectedRows[0].Cells[3].Value.ToString();
            
            try
            {
                int NewQnty = Convert.ToInt32(Stock) - Convert.ToInt32(txtQuantite.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update MedicamentTbl Set quantite_medicament=@MQ where Id_medicament=@MKey", Con);
                cmd.Parameters.AddWithValue("@MQ", NewQnty);
                cmd.Parameters.AddWithValue("@MKey", Key);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Medicament modifie avec succes !");
                Con.Close();
                ShowMedicine();
                //Reset();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }
        private void ShowFacture()
        {
            Con.Open();
            string Query = "Select * from FactureTbl where nom_vendeur='" + LblSellerName.Text + "'and Id_client='" + txtIdDuClient.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVFacture.DataSource = ds.Tables[0];
            Con.Close();
            //ShowFacture();
        }


        /*
        private void ShowBill()
        {
            Con.Open();
            string Query = "Select * from FactureTbl where nom_vendeur='" + LblSellerName.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVTransactions.DataSource = ds.Tables[0];
            Con.Close();
        }
        */
        private void ShowMedicine()
        {
            Con.Open();
            string Query = "Select * from MedicamentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVMedicaments.DataSource = ds.Tables[0];
            Con.Close();
        }
        
       
        //= Convert.ToInt32(txtQuantite.Text);

        private void txtIdDuClient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustomerName();
        }

        int MedId, MedPrice, MedQty, MedTot;

        private void btnImprimer_Click(object sender, EventArgs e)
        {
            string r;
            r = DGVFacture.SelectedRows[0].Cells[0].Value.ToString();
            PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (PrintPreviewDialog.ShowDialog() == DialogResult.OK)
            {
                PrintDocument.Print();
            }
            //btnAjouterLaFacture_Click();

            Con.Open();
            SqlCommand cmd = new SqlCommand("Update FactureTbl Set etat_facture=@CN where Id_client=@CKey", Con);
            cmd.Parameters.AddWithValue("@CN", "Reglé !");
            cmd.Parameters.AddWithValue("@CKey", r);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Client modifie avec succes !");
            Con.Close();

        }

        string MedName;
        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string NomMedic;
            NomMedic = DGVMedicaments.SelectedRows[0].Cells[1].Value.ToString();

            e.Graphics.DrawString("EMSI PARAPHARMACY", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.LimeGreen, new Point(80));
            e.Graphics.DrawString("ID Médicament Prix Quantite Total", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.LimeGreen, new Point(26, 40));
            foreach (DataGridViewRow row in DGVFacture.Rows)
            {
                MedId = Convert.ToInt32(row.Cells[0].Value); //
                NomMedic = "" + row.Cells[1].Value;
                MedPrice = Convert.ToInt32(row.Cells[2].Value);
                //MedQty = Convert.ToInt32(row.Cells[3].Value);
                //MedTot = Convert.ToInt32(row.Cells[4].Value);
                e.Graphics.DrawString("" + MedId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, Pos));
                e.Graphics.DrawString("" + NomMedic, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, Pos));
                e.Graphics.DrawString("" + MedPrice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(128, Pos));
                e.Graphics.DrawString("" + MedQty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, Pos));
                e.Graphics.DrawString("" + MedTot, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, Pos));
                Pos = Pos + 20;
            }
            e.Graphics.DrawString("Le Total en Dhs :" + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(50, Pos + 50));
            e.Graphics.DrawString("******   EMSI  PARAPHARMACY   *****", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Black, new Point(10, Pos + 85));
            //DGVFacture.Rows.Clear();
            DGVFacture.Refresh();
            Pos = 100;
            GrdTotal = 0;
            n = 0;
        }

        private void DGVMedicaments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //label11.Enabled = true;
            txtMedicaments.Text = DGVMedicaments.SelectedRows[0].Cells[1].Value.ToString();
            //txtType.SelectedItem = DGVMedicaments.SelectedRows[0].Cells[2].Value.ToString();
            txtQuantite.Text = DGVMedicaments.SelectedRows[0].Cells[3].Value.ToString();
            //Stock = Convert.ToInt32(DGVMedicaments.SelectedRows[0].Cells[2].Value.ToString());
            txtPrix.Text = DGVMedicaments.SelectedRows[0].Cells[4].Value.ToString();
            //txt.SelectedValue = DGVMedicaments.SelectedRows[0].Cells[5].Value.ToString();
            //txtManufecturerName.Text = DGVMedicaments.SelectedRows[0].Cells[6].Value.ToString();
            if (txtMedicaments.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DGVMedicaments.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Selectionner la vente !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from FactureTbl where Id_facture=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicament supprimé avec succes !");
                    Con.Close();
                    ShowMedicine();
                    //Reset();
                    ShowFacture();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void LblTotal_Click(object sender, EventArgs e)
        {

        }
        private void AfficherFact()
        {
            Con.Open();
            string Query = "Select * from FactureTbl where nom_vendeur='" + LblSellerName.Text + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                LblSellerName.Text = dr["nom_vendeur"].ToString();
            }
            Con.Close();

            /*Con.Open();
            string Query = "Select * from FactureTbl where nom_vendeur='" + LblSellerName.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVFacture.DataSource = ds.Tables[0];
            Con.Close();*/
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AfficherFact();
        }

        private void DGVFacture_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
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

                    label1.ForeColor = Color.LimeGreen;
                    panel1.BackColor = Color.White;
                    ConfigurationManager.AppSettings[key] = "dark";
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
        

        int Pos = 60;
        int n = 0, GrdTotal = 0;
        private void btnAjouterLaFacture_Click(object sender, EventArgs e)
        {
            string Stock;
            Stock = DGVMedicaments.SelectedRows[0].Cells[3].Value.ToString();
            string NomMedic;
            NomMedic = DGVMedicaments.SelectedRows[0].Cells[1].Value.ToString();
            string QteAct;
            QteAct = txtQuantite.Text;


            if (txtQuantite.Text == "" || Convert.ToInt32(txtQuantite.Text) > Convert.ToInt32(Stock))
            {
                MessageBox.Show("Enter Correct Quantity");
            }
            else
            {
                int total = Convert.ToInt32(txtQuantite.Text) * Convert.ToInt32(txtPrix.Text);
                /*DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(DGVFacture);
                newRow.Cells[0].Value = n+1;
                newRow.Cells[1].Value = txtMedicaments.Text;
                newRow.Cells[2].Value = txtQuantite.Text;
                newRow.Cells[3].Value = txtPrix.Text;
                newRow.Cells[4].Value = total;
                DGVFacture.Rows.Add(newRow);*/
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into FactureTbl(nom_vendeur,id_client,nom_client,quantite_facture,montant_facture,etat_facture)values(@CN,@CA,@CMN,@CD,@CG,@CG2)", Con);
                cmd.Parameters.AddWithValue("@CN", LblSellerName.Text);
                cmd.Parameters.AddWithValue("@CA", txtIdDuClient.Text);
                cmd.Parameters.AddWithValue("@CMN", txtNomDuClient.Text);
                cmd.Parameters.AddWithValue("@CD", QteAct);
                cmd.Parameters.AddWithValue("@CG", txtPrix.Text);
                cmd.Parameters.AddWithValue("@CG2", "Non-reglé !");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ajouté avec succes !");
                Con.Close();
                //ShowCustomer();
                //Reset();
                GrdTotal = GrdTotal + total;
                LblTotal.Text = "Dhs " + GrdTotal;
                n++;
                //ShowBill();
                ShowFacture();
                UpdateQnty();
            }
        }
    }
}


/*
 *                 if (Key == 0)
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
*/
