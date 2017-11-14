
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Development.Email
{
    public partial class frmEmailMenu : Form
    {
        public frmEmailMenu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //frmSelectEmailContacts frm = new frmSelectEmailContacts();
            //frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmEmailMessageV2 frm = new frmEmailMessageV2();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void frmEmailMenu_Load(object sender, EventArgs e)
        {

        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    Form1 frm = new Form1();
        //    frm.ShowDialog();
        //}
    }
}
