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
    public partial class View_Books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MUVOJ1E\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");
        SqlDataAdapter Da;
        DataTable Dt = new DataTable();
        SqlCommandBuilder cmd;
        public View_Books()
        {
            InitializeComponent();
            string bookQuery = "SELECT b.*, bc.*, c.NAME AS Category, CAST(a.FIRSTNAME AS varchar) + ' ' + CAST(a.LASTNAME AS varchar) AS Author, bc.AVAILABILITY " +
    "FROM BOOK AS b " +
    "JOIN IS_OF_KIND AS iok ON b.ISBN = iok.ISBN " +
    "JOIN CATEGORY AS c ON iok.CATEGORYID = c.CATEGORYID " +
    "JOIN WRITTEN_BY AS wb ON b.ISBN = wb.ISBN " +
    "JOIN AUTHOR AS a ON wb.AUTHORID = a.AUTHORID " +
    "JOIN BOOKCOPY AS bc ON b.ISBN = bc.ISBN ";
            Da = new SqlDataAdapter(bookQuery, con);
            Da.Fill(Dt);
            dataGridView1.DataSource = Dt;
        }

        private void View_Books_Load(object sender, EventArgs e)
        {



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // add BOOK with new values entered into the data grid
            Add_Book a23 = new Add_Book();
            a23.Show();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            // update book information here with any changes occuring in the data grid
            cmd = new SqlCommandBuilder(Da);
            Da.Update(Dt);
            MessageBox.Show("Update Successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Search_By_Id()
        {
            con.Open();
            string query = "select * from BOOK where ISBN = '" + textBox1.Text + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void searchBtn_Click(object sender, EventArgs e)
        {
            Search_By_Id();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "Select * from BOOK";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}