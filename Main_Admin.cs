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
    public partial class Main_Admin : Form
    {
        //private int childFormNumber = 0;

        public Main_Admin()
        {
            InitializeComponent();
        }

        //private void ShowNewForm(object sender, EventArgs e)
        //{
        //    Form childForm = new Form();
        //    childForm.MdiParent = this;
        //    childForm.Text = "Window " + childFormNumber++;
        //    childForm.Show();
        //}

        //private void OpenFile(object sender, EventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        //    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        //    {
        //        string FileName = openFileDialog.FileName;
        //    }
        //}

        //private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        //    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
        //    {
        //        string FileName = saveFileDialog.FileName;
        //    }
        //}







        private void Main_Admin_Load(object sender, EventArgs e)
        {

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            View_Books v = new View_Books();
            v.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Query_Form q = new Query_Form();
            //q.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Users us = new Users();
            us.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Delete d1 = new Delete();
            d1.Show();
        }
    }
}
