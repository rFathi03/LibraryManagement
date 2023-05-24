using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class SignUpUser : Form
    {
        public SignUpUser()
        {
            InitializeComponent();
        }
        private void LoadUserData()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");

            // Retrieve userid, first name, last name from user
            string userQuery = "SELECT USERID, FIRSTNAME, LASTNAME FROM [USER]";
            SqlDataAdapter userAdapter = new SqlDataAdapter(userQuery, sqlConnection);
            DataTable userDataTable = new DataTable();
            userAdapter.Fill(userDataTable);
            dataGridView1.DataSource = userDataTable;

            // Retrieve userid, first name, last name from user
            string adminQuery = "SELECT ADMINID, FIRSTNAME, LASTNAME FROM [ADMIN]";
            SqlDataAdapter adminAdapter = new SqlDataAdapter(adminQuery, sqlConnection);
            DataTable adminDataTable = new DataTable();
            adminAdapter.Fill(adminDataTable);
            dataGridView2.DataSource = adminDataTable;

            sqlConnection.Close();
        }


        private void SignUpUser_Load(object sender, EventArgs e)
        {
            LoadUserData();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Insert i.e sign up user
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            // Retrieve the biggest USERID value from the User table
            sqlCommand.CommandText = "SELECT MAX(USERID) FROM [User]";
            int maxUserID = Convert.ToInt32(sqlCommand.ExecuteScalar());

            // Increment the maxUserID for the new user
            int newUserID = maxUserID + 1;

            // Insert the new user with the incremented USERID
            sqlCommand.CommandText = "INSERT INTO [User] (USERID, FIRSTNAME, LASTNAME, EMAILADDRESS, PASSWORD, PHONENUMBER) VALUES (@UserID, @FirstName, @LastName, @EmailAddress, @Password, @PhoneNumber)";
            sqlCommand.Parameters.AddWithValue("@UserID", newUserID);
            sqlCommand.Parameters.AddWithValue("@FirstName", textBox1.Text);
            sqlCommand.Parameters.AddWithValue("@LastName", textBox2.Text);
            sqlCommand.Parameters.AddWithValue("@EmailAddress", textBox3.Text);
            sqlCommand.Parameters.AddWithValue("@Password", textBox4.Text);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", textBox5.Text);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            // Show success message or update UI as needed
            MessageBox.Show("User is signed up successfully!");
            LoadUserData();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // displays the user table
        }

        int adminID = 16;

        private void button3_Click(object sender, EventArgs e)
        {
            // sign up admin
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            // Retrieve the biggest USERID value from the Admin table
            sqlCommand.CommandText = "SELECT MAX(ADMINID) FROM [Admin]";
            int maxAdminID = Convert.ToInt32(sqlCommand.ExecuteScalar());

            // Increment the maxAdminID for the new admin
            int newAdminID = maxAdminID + 1;

            // Insert the new admin with the incremented ADMINID
            sqlCommand.CommandText = "INSERT INTO Admin (ADMINID, FIRSTNAME, LASTNAME, EMAILADDRESS, PASSWORD, PHONENUMBER) VALUES (@UserID, @FirstName, @LastName, @EmailAddress, @Password, @PhoneNumber)";
            sqlCommand.Parameters.AddWithValue("@UserID", newAdminID);
            sqlCommand.Parameters.AddWithValue("@FirstName", textBox1.Text);
            sqlCommand.Parameters.AddWithValue("@LastName", textBox2.Text);
            sqlCommand.Parameters.AddWithValue("@EmailAddress", textBox3.Text);
            sqlCommand.Parameters.AddWithValue("@Password", textBox4.Text);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", textBox5.Text);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            // Show success message or update UI as needed
            MessageBox.Show("Admin is signed up successfully!");
            LoadUserData();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // show users first name, last name, and userid here from our database
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // show admins first name, last name, and userid here from our database
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
