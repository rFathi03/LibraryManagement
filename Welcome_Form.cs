using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace Library_system
{
    public partial class Welcome_Form : Form
    {
        public Welcome_Form()
        {
            InitializeComponent();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login_Admin ld = new Login_Admin();
            ld.Show();
        }

        private void loginToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login_Reader lr = new Login_Reader();
            lr.Show();
        }

        private void signUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void loginToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Login_Author la = new Login_Author();
            //la.Show();

        }

        private void signUpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void Welcome_Form_Load(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Welcome_Form_Load_1(object sender, EventArgs e)
        {

        }

        private void readerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void signUpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SignUpUser u12 = new SignUpUser();
            u12.Show();
        }

        private void signUpToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            SignUpUser u22 = new SignUpUser();
            u22.Show();
        }
    }
}
