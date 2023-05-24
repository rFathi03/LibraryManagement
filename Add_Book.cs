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

namespace Library_system
{
    public partial class Add_Book : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-MUVOJ1E\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");
        public Add_Book()
        {
            InitializeComponent();
        }

        private void Add_Book_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

        }

        private void Add_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO BOOK (ISBN, TITLE, PUBLICATIONYEAR) " +
                "VALUES (@ISBN, @TITLE, @BOOK_date); " +
                "INSERT INTO BOOKCOPY (BOOKCOPYID, ISBN, AVAILABILITY, DAMAGED, EDITION) " +
                "VALUES (@BOOK_ID, @ISBN, 1, @DAMAGED, @EDITION); " +
                "INSERT INTO IS_OF_KIND (ISBN, CATEGORYID) VALUES (@ISBN, @CATEGORYID); " +
                "INSERT INTO WRITTEN_BY (ISBN, AUTHORID) VALUES (@ISBN, @AUTHORID);";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@BOOK_ID", int.Parse(bookcopyTextbox.Text));
            cmd.Parameters.AddWithValue("@AUTHORID", AuthoridTextbox.Text);
            cmd.Parameters.AddWithValue("@ISBN", int.Parse(IsbnTextBox.Text));
            cmd.Parameters.AddWithValue("@CATEGORYID", float.Parse(categoryidtextbox.Text));
            cmd.Parameters.AddWithValue("@TITLE", TitletextBox.Text);
            cmd.Parameters.AddWithValue("@BOOK_date", publicationyeartextbox.Text);
            cmd.Parameters.AddWithValue("@EDITION", int.Parse(edition.Text));
            cmd.Parameters.AddWithValue("@DAMAGED", damaged.Checked ? 1 : 0);

            cmd.ExecuteNonQuery();

            conn.Close();

            bookcopyTextbox.Text = "";
            AuthoridTextbox.Text = "";
            IsbnTextBox.Text = "";
            publicationyeartextbox.Text = "";
            categoryidtextbox.Text = "";
            TitletextBox.Text = "";
            edition.Text = "";
            damaged.Checked = false;

            MessageBox.Show("Book Added successfully");
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Update book
            string sql = "UPDATE BOOK SET TITLE = @TITLE, PUBLICATIONYEAR = @BOOK_date " +
                "WHERE ISBN = @ISBN";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@TITLE", TitletextBox.Text);
            cmd.Parameters.AddWithValue("@BOOK_date", publicationyeartextbox.Text);
            cmd.Parameters.AddWithValue("@ISBN", int.Parse(IsbnTextBox.Text));

            cmd.ExecuteNonQuery();

            MessageBox.Show("Book updated successfully");
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bookcopyTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AuthoridTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void IsbnTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void TitletextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void categoryidtextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void publicationyeartextbox_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void edition_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
