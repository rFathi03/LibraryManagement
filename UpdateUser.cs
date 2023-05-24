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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_system
{
    public partial class UpdateUser : Form
    {
        public UpdateUser()
        {
            InitializeComponent();
        }

        private void LoadUserData()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");

            // Retrieve userid, first name, last name from user
            string userQuery = "SELECT * FROM [USER]";
            SqlDataAdapter userAdapter = new SqlDataAdapter(userQuery, sqlConnection);
            DataTable userDataTable = new DataTable();
            userAdapter.Fill(userDataTable);
            dataGridView1.DataSource = userDataTable;

            sqlConnection.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // first name
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // last name
        }

        private void phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //add
            string firstName = firstname.Text;
            string lastName = lastname.Text;
            string phoneNumber = phone.Text;
            string password = passwordbox.Text;
            string emailAddress = email.Text;

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // Retrieve the biggest USERID value from the User table
                command.CommandText = "SELECT MAX(USERID) FROM [User]";
                int maxUserID = Convert.ToInt32(command.ExecuteScalar());
                maxUserID++;

                // Check if the provided userID exists
                command.CommandText = "SELECT COUNT(*) FROM [User] WHERE USERID = @UserID";
                command.Parameters.AddWithValue("@UserID", maxUserID);
                int userCount = Convert.ToInt32(command.ExecuteScalar());

                if (userCount == 0)
                {
                    MessageBox.Show("User does not exist. Please enter a valid userID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update the existing user
                command.CommandText = "UPDATE [User] SET FIRSTNAME = @FirstName, LASTNAME = @LastName, EMAILADDRESS = @EmailAddress, PASSWORD = @Password, PHONENUMBER = @PhoneNumber WHERE USERID = @UserID";
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@EmailAddress", emailAddress);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.ExecuteNonQuery();
            }

            MessageBox.Show("User updated successfully!");
            LoadUserData();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            // Update user
            int myuserid = int.Parse(userID.Text);
            string firstName = firstname.Text;
            string lastName = lastname.Text;
            string phoneNumber = phone.Text;
            string password = passwordbox.Text;
            string emailAddress = email.Text;

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                // Retrieve the biggest USERID value from the User table

                // Check if the provided userID exists
                command.CommandText = "SELECT COUNT(*) FROM [User] WHERE USERID = @UserID";
                command.Parameters.AddWithValue("@UserID", myuserid);
                int userCount = Convert.ToInt32(command.ExecuteScalar());

                if (userCount == 0)
                {
                    MessageBox.Show("User does not exist. Please enter a valid userID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update the existing user
                command.CommandText = "UPDATE [User] SET FIRSTNAME = @FirstName, LASTNAME = @LastName, EMAILADDRESS = @EmailAddress, PASSWORD = @Password, PHONENUMBER = @PhoneNumber WHERE USERID = @UserID";
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@EmailAddress", emailAddress);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.ExecuteNonQuery();
            }

            MessageBox.Show("User updated successfully!");
            LoadUserData();
        }

        private void userID_TextChanged(object sender, EventArgs e)
        {

        }

        private void UpdateUser_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

