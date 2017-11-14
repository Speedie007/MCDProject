using Impendulo.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Impendulo.Development.Enrollment.ViewEnrollmentHistory;

namespace Impendulo.Development.Enquiry
{
    public partial class frmEnrollmentSelectionForEquiry : MetroForm
    {

        public Data.Models.Enquiry CurrentlySelectedEnquiry { get; private set; }

        public int SelectedEnrollmentID { get; set; }
        public Employee CurrentEmployeeLoggedIn { get; set; }

        private List<Data.Models.Enrollment> AllEnrollments { get; set; }
        public frmEnrollmentSelectionForEquiry(Data.Models.Enquiry EnquiryObj)
        {
            this.CurrentlySelectedEnquiry = EnquiryObj;
            InitializeComponent();
            //this.CurrentlySelectedCurriculumEnquiry = CurrentlySelectedCurriculumEnquiry;
            SelectedEnrollmentID = 0;
        }

        private void frmEnrollmentSelectionForEquiry_Load(object sender, EventArgs e)
        {
            populateEquiryEnrollments();
        }

        private void populateEquiryEnrollments()
        {

            using (var Dbconnection = new MCDEntities())
            {
                AllEnrollments = (from a in Dbconnection.Enrollments
                                  where a.EnquiryID == CurrentlySelectedEnquiry.EnquiryID
                                  where a.EnrolmentParentID == 0
                                 && a.CurriculumID == CurrentlySelectedEnquiry.CurriculumID
                                  //Filter Section
                                  select a)
                                .Include(a => a.StudentEnrollment.Student)
                                .Include(a => a.StudentEnrollment.Student.Individual)
                                .Include(a => a.Curriculum)
                                .Include(a => a.LookupEnrollmentProgressState)
                                .ToList<Data.Models.Enrollment>();

                enrollmentBindingSource.DataSource = AllEnrollments;
            };
        }

        private void enrollmentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    SelectedEnrollmentID = ((Data.Models.Enrollment)enrollmentBindingSource.Current).EnrollmentID;
                    this.Close();
                    break;

                case 1:
                    using (frmEnrollmentViewHistory frm = new frmEnrollmentViewHistory(((Data.Models.Enrollment)enrollmentBindingSource.Current).EnrollmentID))
                    {
                        frm.ShowDialog();
                    }
                    break;
                default:
                    break;
            }
        }

        private void enrollmentDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {

                    Data.Models.Enrollment EnrollmentObj = (Data.Models.Enrollment)(row.DataBoundItem);

                    row.Cells[colStudentIDNumber1.Index].Value = EnrollmentObj.StudentEnrollment.Student.StudentIDNumber.ToString();
                    row.Cells[colFirstName1.Index].Value = EnrollmentObj.StudentEnrollment.Student.Individual.IndividualFirstName.ToString();
                    row.Cells[colLastName1.Index].Value = EnrollmentObj.StudentEnrollment.Student.Individual.IndividualLastname.ToString();
                    //row.Cells[colStatus.Index].Value = EnrollmentObj.LookupEnrollmentProgressState.EnrollmentProgressCurrentState.ToString();
                }
            }
        }

        private void btnIDNumberFilter_Click(object sender, EventArgs e)
        {
            enrollmentBindingSource.DataSource = AllEnrollments.Where(a => a.StudentEnrollment.Student.StudentIDNumber.ToLower().Contains(txtIDNumberFilter.Text.ToLower())).ToList();
        }

        private void btnFirstNameFilter_Click(object sender, EventArgs e)
        {
            enrollmentBindingSource.DataSource = AllEnrollments.Where(a => a.StudentEnrollment.Student.Individual.IndividualFirstName.ToLower().Contains(txtFirstNameFilter.Text.ToLower())).ToList();
        }

        private void btnLastNameFilter_Click(object sender, EventArgs e)
        {
            enrollmentBindingSource.DataSource = AllEnrollments.Where(a => a.StudentEnrollment.Student.Individual.IndividualLastname.ToLower().Contains(txtLastNameFilter.Text.ToLower())).ToList();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            this.txtLastNameFilter.Clear();
            this.txtFirstNameFilter.Clear();
            this.txtIDNumberFilter.Clear();
            enrollmentBindingSource.DataSource = AllEnrollments.ToList();
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    SelectedEnrollmentID = ((Data.Models.Enrollment)enrollmentBindingSource.Current).EnrollmentID;
                    this.Close();
                    break;

                case 1:
                    using (frmEnrollmentViewHistory frm = new frmEnrollmentViewHistory(((Data.Models.Enrollment)enrollmentBindingSource.Current).EnrollmentID))
                    {
                        frm.ShowDialog();
                    }
                    break;
                default:
                    break;
            }
        }

        private void metroGrid1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {

                    Data.Models.Enrollment EnrollmentObj = (Data.Models.Enrollment)(row.DataBoundItem);

                    row.Cells[colStudentIDNumber1.Index].Value = EnrollmentObj.StudentEnrollment.Student.StudentIDNumber.ToString();
                    row.Cells[colFirstName1.Index].Value = EnrollmentObj.StudentEnrollment.Student.Individual.IndividualFirstName.ToString();
                    row.Cells[colLastName1.Index].Value = EnrollmentObj.StudentEnrollment.Student.Individual.IndividualLastname.ToString();
                    //row.Cells[colStatus.Index].Value = EnrollmentObj.LookupEnrollmentProgressState.EnrollmentProgressCurrentState.ToString();
                }
            }
        }
    }
}

