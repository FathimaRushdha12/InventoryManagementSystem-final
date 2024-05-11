using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmBrand : Form
    {
      
        static string connectionstring = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionstring);
        public frmBrand()
        {
            InitializeComponent();
        }
        public EventHandler DisplayButton;
        private void button4_Click(object sender, EventArgs e)
        {
            FrmDashboard frmDashboard = new FrmDashboard();
            frmDashboard.Show();
            //try
            //{
            //    conn.Open(); // Connection is opened
            //    SqlCommand sqlCommand = conn.CreateCommand();
            //    sqlCommand.CommandType = CommandType.Text;

            //    // Search for students whose ID or FullName matches the input text
            //    sqlCommand.CommandText = "SELECT * FROM Brand WHERE Student_ID LIKE @SearchText OR FullName LIKE @SearchText";
            //    sqlCommand.Parameters.AddWithValue("@SearchText", "%" + txtSearch.Text + "%"); // Use '%' to match any characters before and after the search text
            //    //sqlCommand.ExecuteNonQuery();

            //    DataTable dataTable = new DataTable();//store the query results
            //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);//bridge between datatable and database
            //    sqlDataAdapter.Fill(dataTable);//populate the results

            //    if (dataTable.Rows.Count > 0)
            //    {
            //        // Display the search results in the DataGridView
            //        dgvDispaly.DataSource = dataTable;
   
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
            //finally
            //{
            //    conn.Close(); // Connection is closed
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBrand_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string brandid=txtBrandId.Text;
            string brandname = txtBrandName.Text;
            if (string.IsNullOrEmpty(brandid) || string.IsNullOrEmpty(brandname))
            {
                MessageBox.Show("Enter Correct Details");
            }

            conn.Open();

            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "Select Count(*) from [Brand] where BrandID=@BrandId AND BrandName=@BrandName ";
            sqlCommand.Parameters.AddWithValue("@BrandId", brandid);
            sqlCommand.Parameters.AddWithValue("@BrandName", brandname);

            int count = (int)sqlCommand.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Entered Data Already Exist");
            }
            else
                try
                {

                    sqlCommand.CommandText = "Insert into [Brand]  values(@BrandId,@BrandName)";
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("BrandId", brandid);
                    sqlCommand.Parameters.AddWithValue("BrandName", brandname);
                    int rowsAffected = sqlCommand.ExecuteNonQuery();//return the count of row affected
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Brand Added");

                    }
                    else
                    {
                        MessageBox.Show("Adding Brand Unsuccessful.");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);

                }
                finally
                {
                    conn.Close();


                }

        }

        private void btnDeleteBrand_Click(object sender, EventArgs e)
        {
            try
            {
                string brandid = txtBrandId.Text;
                string brandname = txtBrandName.Text;
                conn.Open();
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Delete from [Brand] where BrandId=@BrandId";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@BrandId", brandid);
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Brand  deleted successfully");

                }
                else
                {
                    MessageBox.Show("Unsuccessful!EnterDetails");
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                conn.Close();

            }
        }

        private void btnUpdateBrand_Click(object sender, EventArgs e)
        {
            string brandid = txtBrandId.Text;
            string brandname = txtBrandName.Text;

            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update Brand SET BrandName=@BrandName where BrandId=@BrandId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@BrandId", brandid);
                cmd.Parameters.AddWithValue("@BrandName", brandname);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Brand Updated Successfully");

                }
                else
                {
                    MessageBox.Show("Update Failed!");

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                //Connection is closed
                conn.Close();
            }

        }
    }
    
}

