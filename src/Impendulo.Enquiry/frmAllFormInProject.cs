﻿

using Impendulo.Development.Enquiry;
using Impendulo.Development.Enquiry.NewEnquiry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Development.Enquiry
{
    public partial class frmAllFormInProject : Form
    {
        public frmAllFormInProject()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //frmClientEnquiryV2 frm = new frmClientEnquiryV2();
            //frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmEnquiryInitialConsultation frm = new frmEnquiryInitialConsultation(null);
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmInitailDocumentation frm = new frmInitailDocumentation();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmNewEnquiry frm = new frmNewEnquiry();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmSelectCompanyContact frm = new frmSelectCompanyContact();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmSelectIndividualContact frm = new frmSelectIndividualContact();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmSelectCourseCurriculumForClientEnquiry frm = new frmSelectCourseCurriculumForClientEnquiry();
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmEnquiryViewContactInformation frm = new frmEnquiryViewContactInformation();
            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmEquiryViewHistory frm = new frmEquiryViewHistory(0);
            frm.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            frmWorkbanchEnquiries frm = new frmWorkbanchEnquiries();
            frm.ShowDialog();
        }

        private void frmAllFormInProject_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (frmClientEnquiry frm = new frmClientEnquiry(0))
            {
                frm.ShowDialog();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (frmSearchForSelectedEquiry frm = new frmSearchForSelectedEquiry())
            {
                frm.ShowDialog();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (frmEquiryViewHistory frm = new frmEquiryViewHistory(7182))
            {
                frm.ShowDialog();
            }

            //using(Form1 frm = new Form1())
            //{
            //    frm.ShowDialog();
            //}
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frmNewEnquiryV2 frm = new frmNewEnquiryV2();
            frm.ShowDialog();
        }
        //
        //frmSelectCompanyContact
    }
}
