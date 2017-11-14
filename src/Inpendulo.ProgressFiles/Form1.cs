using Impendulo.Data.Models;
using Impendulo.Development.Files.Files.CompanyFiles;
using Impendulo.Development.Files.Files.StudentFiles;
using Inpendulo.Development.Files;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inpendulo.ProgressFiles
{
    public partial class Form1 : Form
    {

        public Employee CurrentEmployeeLoggedIn { get; set; }
        public Form1()
        {

            using (var Dbconnection = new MCDEntities())
            {
                CurrentEmployeeLoggedIn = (from a in Dbconnection.Employees
                                           select a).FirstOrDefault();
            };
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //using (frmNewProgressFileV1 frm = new frmNewProgressFileV1(Impendulo.Common.Enum.EnumProgessFileTypes.Student))
            //{
            //    frm.ShowDialog();
            //}

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //using (frmNewProgressFileV1 frm = new frmNewProgressFileV1(Impendulo.Common.Enum.EnumProgessFileTypes.Company))
            //{
            //    frm.ShowDialog();
            //}
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            using (frmCompanyFiles frm = new frmCompanyFiles(CurrentEmployeeLoggedIn))
            {
                frm.ShowDialog();
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            using (frmStudentFiles frm = new frmStudentFiles(CurrentEmployeeLoggedIn))
            {
                frm.ShowDialog();
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            //using (Form2 frm = new Form2())
            //{
            //    frm.ShowDialog();
            //}
        }
    }
}
