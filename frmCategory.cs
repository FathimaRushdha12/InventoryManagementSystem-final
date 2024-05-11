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

namespace InventoryManagementSystem
{
    public partial class frmCategory : Form
    {
        static string connectionstring = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionstring);
        public frmCategory()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmDashboard frmDashboard = new FrmDashboard();
            frmDashboard.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CategoryId=txtCategoryId.Text;
            string CategoryName=txtCategoryName.Text;
            if (string.IsNullOrEmpty(CategoryId) || string.IsNullOrEmpty(CategoryName))
            {
                MessageBox.Show("Enter Details");

            }
            conn.Open();

            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "Select Count(*) from [Category] where CategoryID=@CategoryId AND CategoryName=@CategoryName ";
            sqlCommand.Parameters.AddWithValue("@CategoryId", CategoryId);
            sqlCommand.Parameters.AddWithValue("@CategoryName", CategoryName);

            int count = (int)sqlCommand.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Entered Data Already Exist");
            }
            else
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Category values(@CategoryId,@CategoryName)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                }

        }
    }
}
