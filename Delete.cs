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
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();
        }

        private void Delete_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");

            // Retrieve data from the Book table and bind to dataGridView1
            string bookQuery = "SELECT * FROM BookCopy";
            SqlDataAdapter bookAdapter = new SqlDataAdapter(bookQuery, sqlConnection);
            DataTable bookDataTable = new DataTable();
            bookAdapter.Fill(bookDataTable);
            dataGridView1.DataSource = bookDataTable;

            // Retrieve data from the Borrower table and bind to dataGridView2
            string borrowerQuery = "SELECT * FROM Borrowing";
            SqlDataAdapter borrowerAdapter = new SqlDataAdapter(borrowerQuery, sqlConnection);
            DataTable borrowerDataTable = new DataTable();
            borrowerAdapter.Fill(borrowerDataTable);
            dataGridView2.DataSource = borrowerDataTable;

            sqlConnection.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // Delete rows from BookCopy where Condition is 'Damaged'
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            sqlCommand.CommandText = "DELETE FROM BookCopy WHERE DAMAGED = '1'";
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            // Show success message or update UI as needed
            MessageBox.Show("Damaged book copies have been deleted.");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // insert code that will make damaged books show
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // insert code that will show overdue users
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete rows from Borrower where DueDate is in the past
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            sqlCommand.CommandText = "DELETE FROM Borrower WHERE DATEDIFF(day, DueDate, GETDATE()) > 0";
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            // Show success message or update UI as needed
            MessageBox.Show("Overdue borrower records have been deleted.");

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
