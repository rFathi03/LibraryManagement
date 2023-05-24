

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
        public partial class BorrowMenu : Form
        {
        private string connectionString = "Data Source=DESKTOP-MUVOJ1E\\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True";
            public BorrowMenu()
            {
                InitializeComponent();
            }

        private void Main_User_Load(object sender, EventArgs e)
        {
            // Retrieve data from the Book table and bind to dataGridView1
            string bookQuery = "SELECT b.*, bc.*, c.NAME AS Category, CAST(a.FIRSTNAME AS varchar) + ' ' + CAST(a.LASTNAME AS varchar) AS Author, bc.AVAILABILITY " +
                               "FROM BOOK AS b " +
                               "JOIN IS_OF_KIND AS iok ON b.ISBN = iok.ISBN " +
                               "JOIN CATEGORY AS c ON iok.CATEGORYID = c.CATEGORYID " +
                               "JOIN WRITTEN_BY AS wb ON b.ISBN = wb.ISBN " +
                               "JOIN AUTHOR AS a ON wb.AUTHORID = a.AUTHORID " +
                               "JOIN BOOKCOPY AS bc ON b.ISBN = bc.ISBN " +
                               "WHERE bc.AVAILABILITY = 1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlDataAdapter bookAdapter = new SqlDataAdapter(bookQuery, connection))
                {
                    DataTable bookDataTable = new DataTable();
                    bookAdapter.Fill(bookDataTable);
                    dataGridView1.DataSource = bookDataTable;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Get the selected book's ISBN from the DataGridView
            string selectedISBN = dataGridView1.SelectedRows[0].Cells["ISBN"].Value.ToString();

            // Check if the book is available for borrowing
            string checkAvailabilityQuery = "SELECT * FROM BookCopy WHERE ISBN = @ISBN AND Availability = 1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(checkAvailabilityQuery, connection))
                {
                    command.Parameters.AddWithValue("@ISBN", selectedISBN);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Book is available, proceed with borrowing
                        reader.Close();

                        // Retrieve the selected book copy ID from the DataGridView
                        int selectedBookCopyID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["BookCopyID"].Value);

                        // Generate a new unique borrowing ID
                        int newBorrowingID = GetNewBorrowingID(connection);

                        // Insert a new borrowing record into the Borrowing table
                        string borrowBookQuery = "INSERT INTO Borrowing (BorrowingID, UserID, BookCopyID, BorrowingDate, RETURNDATE) " +
                                                    "VALUES (@BorrowingID, @UserID, @BookCopyID, @BorrowingDate, @RETURNDATE)";

                        // Assuming you have a variable to store the current user ID
                        int currentUserID = UserSession.CurrentBorrowerID;
                        DateTime borrowingDate = DateTime.Now;
                        DateTime returnDate = borrowingDate.AddDays(14);

                        using (SqlCommand borrowCommand = new SqlCommand(borrowBookQuery, connection))
                        {
                            borrowCommand.Parameters.AddWithValue("@BorrowingID", newBorrowingID);
                            borrowCommand.Parameters.AddWithValue("@UserID", currentUserID);
                            borrowCommand.Parameters.AddWithValue("@BookCopyID", selectedBookCopyID);
                            borrowCommand.Parameters.AddWithValue("@BorrowingDate", borrowingDate);
                            borrowCommand.Parameters.AddWithValue("@RETURNDATE", returnDate);
                            borrowCommand.ExecuteNonQuery();
                        }

                        // Update the availability status
                        string updateAvailabilityQuery = "UPDATE BookCopy SET Availability = 0 WHERE BookCopyID = @BookCopyID";
                        using (SqlCommand updateCommand = new SqlCommand(updateAvailabilityQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@BookCopyID", selectedBookCopyID);
                            updateCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Book borrowed successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Book is not available for borrowing.");
                    }

                }
            }
        }


        private int GetNewBorrowingID(SqlConnection connection)
        {
            string query = "SELECT MAX(BorrowingID) FROM Borrowing";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                var result = command.ExecuteScalar();
                int lastBorrowingID = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                return lastBorrowingID + 1;
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                // show book information here
            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchISBN = textBox1.Text.Trim();

            string searchQuery = "SELECT b.*, bc.*, c.NAME AS Category, CAST(a.FIRSTNAME AS varchar) + ' ' + CAST(a.LASTNAME AS varchar) AS Author, bc.AVAILABILITY " +
                                 "FROM BOOK AS b " +
                                 "JOIN IS_OF_KIND AS iok ON b.ISBN = iok.ISBN " +
                                 "JOIN CATEGORY AS c ON iok.CATEGORYID = c.CATEGORYID " +
                                 "JOIN WRITTEN_BY AS wb ON b.ISBN = wb.ISBN " +
                                 "JOIN AUTHOR AS a ON wb.AUTHORID = a.AUTHORID " +
                                 "JOIN BOOKCOPY AS bc ON b.ISBN = bc.ISBN " +
                                 "WHERE bc.AVAILABILITY = 1 AND b.ISBN LIKE @ISBN";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(searchQuery, connection);
                command.Parameters.AddWithValue("@ISBN", "%" + searchISBN + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string searchYear = textBox2.Text.Trim();

            string searchQuery = "SELECT b.*, bc.*, c.NAME AS Category, CAST(a.FIRSTNAME AS varchar) + ' ' + CAST(a.LASTNAME AS varchar) AS Author, bc.AVAILABILITY " +
                                 "FROM BOOK AS b " +
                                 "JOIN IS_OF_KIND AS iok ON b.ISBN = iok.ISBN " +
                                 "JOIN CATEGORY AS c ON iok.CATEGORYID = c.CATEGORYID " +
                                 "JOIN WRITTEN_BY AS wb ON b.ISBN = wb.ISBN " +
                                 "JOIN AUTHOR AS a ON wb.AUTHORID = a.AUTHORID " +
                                 "JOIN BOOKCOPY AS bc ON b.ISBN = bc.ISBN " +
                                 "WHERE bc.AVAILABILITY = 1 AND b.PublicationYear LIKE @PublicationYear";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(searchQuery, connection);
                command.Parameters.AddWithValue("@PublicationYear", "%" + searchYear + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }



        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string searchTitle = textBox3.Text.Trim();

            string searchQuery = "SELECT b.*, bc.*, c.NAME AS Category, CAST(a.FIRSTNAME AS varchar) + ' ' + CAST(a.LASTNAME AS varchar) AS Author, bc.AVAILABILITY " +
                                 "FROM BOOK AS b " +
                                 "JOIN IS_OF_KIND AS iok ON b.ISBN = iok.ISBN " +
                                 "JOIN CATEGORY AS c ON iok.CATEGORYID = c.CATEGORYID " +
                                 "JOIN WRITTEN_BY AS wb ON b.ISBN = wb.ISBN " +
                                 "JOIN AUTHOR AS a ON wb.AUTHORID = a.AUTHORID " +
                                 "JOIN BOOKCOPY AS bc ON b.ISBN = bc.ISBN " +
                                 "WHERE bc.AVAILABILITY = 1 AND b.Title LIKE @Title";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(searchQuery, connection);
                command.Parameters.AddWithValue("@Title", "%" + searchTitle + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }



        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string searchAuthor = textBox6.Text.Trim();

            string searchQuery = "SELECT b.*, bc.*, c.NAME AS Category, CAST(a.FIRSTNAME AS varchar) + ' ' + CAST(a.LASTNAME AS varchar) AS Author, bc.AVAILABILITY " +
                                 "FROM BOOK AS b " +
                                 "JOIN IS_OF_KIND AS iok ON b.ISBN = iok.ISBN " +
                                 "JOIN CATEGORY AS c ON iok.CATEGORYID = c.CATEGORYID " +
                                 "JOIN WRITTEN_BY AS wb ON b.ISBN = wb.ISBN " +
                                 "JOIN AUTHOR AS a ON wb.AUTHORID = a.AUTHORID " +
                                 "JOIN BOOKCOPY AS bc ON b.ISBN = bc.ISBN " +
                                 "WHERE bc.AVAILABILITY = 1 AND (a.FIRSTNAME LIKE @Author OR a.LASTNAME LIKE @Author)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(searchQuery, connection);
                command.Parameters.AddWithValue("@Author", "%" + searchAuthor + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }



        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string searchCategory = textBox7.Text.Trim();

            string searchQuery = "SELECT b.*, bc.*, c.NAME AS Category, CAST(a.FIRSTNAME AS varchar) + ' ' + CAST(a.LASTNAME AS varchar) AS Author, bc.AVAILABILITY " +
                                 "FROM BOOK AS b " +
                                 "JOIN IS_OF_KIND AS iok ON b.ISBN = iok.ISBN " +
                                 "JOIN CATEGORY AS c ON iok.CATEGORYID = c.CATEGORYID " +
                                 "JOIN WRITTEN_BY AS wb ON b.ISBN = wb.ISBN " +
                                 "JOIN AUTHOR AS a ON wb.AUTHORID = a.AUTHORID " +
                                 "JOIN BOOKCOPY AS bc ON b.ISBN = bc.ISBN " +
                                 "WHERE bc.AVAILABILITY = 1 AND c.NAME LIKE @Category";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(searchQuery, connection);
                command.Parameters.AddWithValue("@Category", "%" + searchCategory + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}