using Impendulo.Data.Models;
using Impendulo.Development.Enrollment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Development.Enrollment
{
    public partial class frmEnrollmentMenu : Form
    {
        private Employee CurrentEmployeeLoggedIn = null;
        public frmEnrollmentMenu()
        {
            InitializeComponent();
            using (var Dbconnection = new MCDEntities())
            {
                //Simulates the current Employee that is logged in.
                CurrentEmployeeLoggedIn = (from a in Dbconnection.Employees
                                           select a).FirstOrDefault<Employee>();
                //loads Departments in the drop down
                lookupDepartmentBindingSource.DataSource = (from a in Dbconnection.LookupDepartments
                                                            orderby a.DepartmentName
                                                            select a).ToList<LookupDepartment>();
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmEnrollmentProgress frm = new frmEnrollmentProgress(Convert.ToInt32(txtEnrollmentIDForInProgreesEnrollment.Text));

            Employee CurrentEmployeeLoggedIn = null;

            //Passes the employee object of the currentlly loggin
            frm.CurrentEmployeeLoggedIn = CurrentEmployeeLoggedIn;
            //Sets the parameters for the for to load the correct enrollments
            frm.CurrentEquiryID = Convert.ToInt32(txtEquriyIDForInprogressEnrollment.Text);
            frm.CurrentSelectedDepartment = (Common.Enum.EnumDepartments)(cboDepartmentsForInProgressEnrollment.SelectedValue);

            frm.ShowDialog();

        }

        private void frmEnrollmentMenu_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //frmScheduleApprenticeship frm = new frmScheduleApprenticeship();
            //frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmEnrollmentException frm = new frmEnrollmentException();
            frm.CurrentEmployeeLoggedIn = CurrentEmployeeLoggedIn;

            using (var Dbconnection = new MCDEntities())
            {
                frm.SelectedCurriculumCourseEnrollment = (from a in Dbconnection.CurriculumCourseEnrollments
                                                          orderby a.EnrollmentID descending
                                                          select a).FirstOrDefault<CurriculumCourseEnrollment>();
                frm.ShowDialog();
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            using (frmEnrollmentProgress frm = new frmEnrollmentProgress(0))
            {
                frm.CurrentEmployeeLoggedIn = CurrentEmployeeLoggedIn;
                frm.ShowDialog();
            }
        }
    }
}
