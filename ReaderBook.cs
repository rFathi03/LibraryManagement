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
    public partial class ReaderBook : Form
    {

    
         SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MUVOJ1E\SQLEXPRESS;Initial Catalog=LibraryManagement;Integrated Security=True");
    SqlDataAdapter Da;
    DataTable Dt = new DataTable();
    SqlCommandBuilder cmd;
    
        public ReaderBook()
        {
            InitializeComponent();
            Da = new SqlDataAdapter("select * from dbo.BOOK", con);
            Da.Fill(Dt);
            dataGridView1.DataSource = Dt;
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

        private void button1_Click(object sender, EventArgs e)
        {
            //ConfirmRea cr = new ConfirmRea();
            //cr.Show();
        }

        private void ReaderBook_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
