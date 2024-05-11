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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace InventoryManagementSystem
{
    public partial class frmLogin : Form
    {
        static string connectionstring = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        SqlConnection conn=new SqlConnection(connectionstring);  
        public frmLogin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username=txtUsername.Text;
            string password = txtPassword.Text; 
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)){
                MessageBox.Show("Invalid Credentials!");

            }
            else if (AuthenticateUser(username, password))
            {
                FrmDashboard frmDashboard = new FrmDashboard();
                frmDashboard.Show();
            }
            else
            {
                MessageBox.Show("Wrong UserName or Password!");
            }

           

        }
        private bool AuthenticateUser(string username, string password)
        {
            bool isAuthenticated = false;

      
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE Username = @username AND Password = @password";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);//avoid injection attacks
                command.Parameters.AddWithValue("@password", password);

                conn.Open();
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    isAuthenticated = true;
                }
                conn.Close();
            }
            return isAuthenticated;
        }
        ////Only allow numeric digits to be entered as an Employee Id
        //private void txtEmpId_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        //    {

        //        e.Handled = true;
        //    }

        //}


        ////Only allow letters to be entered at Employee Name
        //private void txtEmpName_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
 
          


        