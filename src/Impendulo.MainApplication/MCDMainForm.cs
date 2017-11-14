using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Impendulo.Data.Models;
using Impendulo.Common.AppSettings;
using Impendulo.Common;
using Impendulo.Common.EntityFrameWorkHelper;
using Impendulo.Common.SqlHelper;
using Impendulo.Deployment.Facilitator;
using Impendulo.Deployment.MCDEmployees;
using Impendulo.Deployment.Enquiry;
using Impendulo.Deployment.Students;
using Impendulo.Deployment.Company;
using Impendulo.SMTPModule.Development;

using Impendulo.Deployment.DatabaseSettings;

using Impendulo.MessageTemplates.Deployment;
using Impendulo.Deployment.Courses;
using Impendulo.Deployment.Scheduling.ScheduleSummary;


using Impendulo.Deployment.Company.CompanyDetails;
using Impendulo.Deployment.Venues.AddEditVenues;
using Impendulo.Deployment.Enquiry.NewEnquiry;
using Inpendulo.Development.Files.Files.CompanyFiles;
using Impendulo.Deployment.Reports.Reports.Schedules;
using Impendulo.Deployment.Scheduling.CourseScheduleSections;
using Impendulo.Deployments.Reports.Reports.Schedules;

namespace Impendulo.MainApplication
{
    public partial class MCDMainForm : Form
    {
        public MCDMainForm()
        {
            InitializeComponent();
        }

        public Employee CurrentEmployeeLoggedIn
        {
            get;
            set;
        }
        public SystemAdministrator SystemAdminLogin { get; set; }
        private Boolean _IsLogOut = true;
        public Boolean IsLogOut
        {
            get { return _IsLogOut; }
            set { _IsLogOut = value; }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //RibbonButton btn = new RibbonButton("here");
            //ribbonPanel2.Items.Add(btn);
            //btn.Canvas.Refresh();
        }

        //private void AddStudentRribbonButton_Click(object sender, EventArgs e)
        //{
        //    foreach (Form f in this.MdiChildren)
        //    {
        //        if (f.GetType() == typeof(Impendulo.ViewCurrentCourseSchedules.ViewCurrentCourseSchedules))
        //        {
        //            f.Activate();
        //            return;
        //        }
        //    }

        //    Form form = new temp.Form1();
        //    form.MdiParent = this;
        //    form.Show();

        //}

        private void ExitRibbonOrbMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure," + Environment.NewLine + "Make Sure All Work Is Saved!", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (DialogResult.Yes == result)
            {
                this.IsLogOut = false;
                this.Close();
            }

        }

        private void ribbon1_Click(object sender, EventArgs e)
        {

        }

        private void ribbtnConfigureEngApprenticeshipCourses_Click(object sender, EventArgs e)
        {


        }

        private void ribbonPanel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ResetTransform();
        }

        private void ribbon1_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.Refresh();
        }

        //private void rbtnAdministerCourses_Click(object sender, EventArgs e)
        //{
        //    foreach (Form f in this.MdiChildren)
        //    {
        //        if (f.GetType() == typeof(CourseAdministrationV6cs))
        //        {
        //            f.Activate();
        //            return;
        //        }
        //    }

        //    CourseAdministrationV6cs frm = new CourseAdministrationV6cs();
        //    //frm._CourseType = ((RibbonButton)sender).Tag.ToString();
        //    frm.MdiParent = this;
        //    frm.Show();
        //}

        private void btnoinr_Click(object sender, EventArgs e)
        {

        }

        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Helllo");
        }

        private void rbtnNeweditStudnetEnrollment_Click(object sender, EventArgs e)
        {
            //foreach (Form f in this.MdiChildren)
            //{
            //    if (f.GetType() == typeof(frmStudentCourseEnrollment))
            //    {
            //        f.Activate();
            //        f.WindowState = FormWindowState.Maximized;
            //        return;
            //    }
            //}

            //frmStudentCourseEnrollment frm = new frmStudentCourseEnrollment();
            ////frm._CourseType = ((RibbonButton)sender).Tag.ToString();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void rbtnCourseAdministration_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmCourseConfiguration))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            frmCourseConfiguration frm = new frmCourseConfiguration();

            frm.MdiParent = this;
            frm.Show();
        }

        private void btnEmailSettings_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmEmailConfiguration))
                {
                    f.Activate();
                    return;
                }
            }

            frmEmailConfiguration frm = new frmEmailConfiguration();

            frm.MdiParent = this;
            frm.Show();
        }

        private void btnDatabaseSettings_CanvasChanged(object sender, EventArgs e)
        {

        }

        private void btnDatabaseSettings_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmDynamicallySetConnectionString))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            frmDynamicallySetConnectionString frm = new frmDynamicallySetConnectionString();

            frm.MdiParent = this;
            frm.Show();
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(ViewEditCompanyDetails))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            ViewEditCompanyDetails frm = new ViewEditCompanyDetails(0);

            frm.MdiParent = this;
            frm.Show();
        }

        private void btnAdminStudent_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmStudentSearchForStudent))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            using (frmStudentSearchForStudent frm = new frmStudentSearchForStudent(true))
            {
                //frm.MdiParent = this;
                frm.ShowDialog();
                using (frmStudentAddUpdate frmInner = new frmStudentAddUpdate(frm.CurrentSelectedStudent.StudentID))
                {

                    frmInner.ShowDialog();
                }
            };

            //frm.frmParentForm = this;


        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmAddUpdateEmployeeDetails))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            frmAddUpdateEmployeeDetails frm = new frmAddUpdateEmployeeDetails();
            // frm.frmParentForm = this;

            frm.MdiParent = this;
            frm.Show();
        }

        private void btnFacilitators_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmAddUpdateFacilitatorDetails))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            frmAddUpdateFacilitatorDetails frm = new frmAddUpdateFacilitatorDetails();
            // frm.frmParentForm = this;

            frm.MdiParent = this;
            frm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEquiryList_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmClientEnquiry))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            frmClientEnquiry frm = new frmClientEnquiry(0);

            frm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;

            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
            frm.WindowState = FormWindowState.Maximized;
        }

        private void btnEditMessageTeplates_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmMessageTemplates))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            frmMessageTemplates frm = new frmMessageTemplates();

            frm.MdiParent = this;
            frm.Show();
        }

        private void btnEnquiyNew_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmNewEnquiryV2))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            using (frmNewEnquiryV2 frm = new frmNewEnquiryV2())
            {
                frm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;

                //frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();
            };



        }

        private void btnAddEditVenues_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmVenueAssociatedCourses))
                {
                    f.Activate();
                    // f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            using (frmVenueAssociatedCourses frm = new frmVenueAssociatedCourses())
            {
                //frm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;

                //frm.MdiParent = this;
                //frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();
            };

        }

        private void btnCurrentSchedule_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmScheduleSummary))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            using (frmScheduleSummary frm = new frmScheduleSummary())
            {
                /* frm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;*/

                //frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();
            };

            //foreach (Form f in this.MdiChildren)
            //{
            //    if (f.GetType() == typeof(frmMessageTemplates))
            //    {
            //        f.Activate();
            //        f.WindowState = FormWindowState.Maximized;
            //        return;
            //    }
            //}

            //frmMessageTemplates frm = new frmMessageTemplates();

            ////frm.MdiParent = this;
            //frm.Show();
        }

        private void btnCompanyFiles_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmCompanyFiles))
                {
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            frmCompanyFiles frm = new frmCompanyFiles(this.CurrentEmployeeLoggedIn);

            //frm.MdiParent = this;
            frm.ShowDialog();



        }

        private void btnAllCourseOverView_Click(object sender, EventArgs e)
        {
            //frmSectionalView
            using (frmCourseScheduleSections frm = new frmCourseScheduleSections())
            {
                frm.ShowDialog();
            }
        }

        private void btnStudentRegister_Click(object sender, EventArgs e)
        {
            using (frmStudentRegister frm = new frmStudentRegister())
            {
                frm.ShowDialog();
            }
        }
    }
}

