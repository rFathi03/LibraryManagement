using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WindowsFormsApp2;

namespace Library_system
{
    public partial class Login_Admin : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-MUVOJ1E\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");
        int count = 0;
        public Login_Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from dbo.ADMIN where ADMINID ='" + Ad_name.Text +"' and PASSWORD='"+ Ad_pass.Text +"'  ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            count = Convert.ToInt32(dt.Rows.Count.ToString());

            if(count == 0)
            {
                MessageBox.Show("username or password is not entered correctly. .");
            }
            else
            {
                // Retrieve the UserID from the DataTable
                int userID = Convert.ToInt32(dt.Rows[0]["ADMINID"]);

                // Assign the UserID to CurrentBorrowerID in UserSession
                UserSession.CurrentBorrowerID = userID;
                this.Hide();
                Main_Admin m = new Main_Admin();
                m.Show();
            }

        }


        private void Login_Admin_Load(object sender, EventArgs e)
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
