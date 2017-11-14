using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Deployment.Enquiry.NewEnquiry.AddNewEnquiry
{
    public partial class frmAddNewEnquiry : MetroForm
    {

        public List<Data.Models.Curriculum> CurrentlySelectedCurriculum { get; set; }
        public Data.Models.Curriculum CurrentlySelectedCurriculumFromForm = null;
        public int ProgressFileID { get; set; }

        public List<Data.Models.Enquiry> EL = new List<Data.Models.Enquiry>();
        public frmAddNewEnquiry(int ProgressFileID)
        {
            CurrentlySelectedCurriculum = new List<Data.Models.Curriculum>();
            this.ProgressFileID = ProgressFileID;
            InitializeComponent();
        }

        private void frmAddNewEnquiry_Load(object sender, EventArgs e)
        {

        }

        private void btnResetSelection_Click(object sender, EventArgs e)
        {

        }

        private void btnAddSelectedCurriculum_Click(object sender, EventArgs e)
        {
            Data.Models.Enquiry NewEnquiry = new Data.Models.Enquiry()
            {
                ProgressFileID = this.ProgressFileID,
                EnquiryDate = DateTime.Now,
                EnquiryStatusID = (int)Common.Enum.EnumEnquiryStatuses.New,
                InitialConsultationComplete = false,
                InitialCurriculumEnquiryDocumentationSent = false,
                CurriculumID = CurrentlySelectedCurriculumFromForm.CurriculumID,
                LastUpdated = DateTime.Now,
                EnrollmentQuanity = (int)nudQtyToEnroll.Value,
            };
            CurrentlySelectedCurriculum.Add(CurrentlySelectedCurriculumFromForm);

            EL.Add(NewEnquiry);


            this.resetCurriculumSelectionControls();
            this.refreshSelectedEnqiuries();
        }

        private void refreshSelectedEnqiuries()
        {
            enquiryBindingSource.Clear();
            enquiryBindingSource.DataSource = EL.ToList();
            enquiryBindingSource.ResetBindings(true);
        }
        private void btnSelectCurricullum_Click(object sender, EventArgs e)
        {
            using (frmSelectCourseCurriculumForClientEnquiry frm = new frmSelectCourseCurriculumForClientEnquiry())
            {
                frm.SelectedCurriculum = CurrentlySelectedCurriculum;
                frm.ShowDialog();
                if (frm.currentCurriculum != null)
                {
                    // this.CurrentSelectedCurriculum = frm.currentCurriculum;
                    CurrentlySelectedCurriculumFromForm = frm.currentCurriculum;
                    this.txtSelectedCourseCurriculum.Text = frm.currentCurriculum.CurriculumName;
                    this.enableAddSelectedCurriculumControls();
                }
                else
                {
                    this.resetCurriculumSelectionControls();
                    this.disableAddSElectedCurriculumControls();
                }
            };
        }

        private void btnUpdateQty_Click(object sender, EventArgs e)
        {
            if (enquiryBindingSource.Count > 0)
            {
                frmUpdateSelectedCurriculumEnrollQty frm = new frmUpdateSelectedCurriculumEnrollQty();
                frm.CurrentlySelectedEnquiry = (Data.Models.Enquiry)this.enquiryBindingSource.Current;
                frm.ShowDialog();
                this.refreshSelectedEnqiuries();
            }
        }

        private void enableAddSelectedCurriculumControls()
        {
            btnAddSelectedCurriculum.Enabled = true;
            this.btnAddSelectedCurriculum.Enabled = true;
        }
        private void disableAddSElectedCurriculumControls()
        {
            btnAddSelectedCurriculum.Enabled = false;
            this.btnAddSelectedCurriculum.Enabled = false;
        }
        private void resetCurriculumSelectionControls()
        {
            this.txtSelectedCourseCurriculum.Clear();
            nudQtyToEnroll.Value = 1;
            this.CurrentlySelectedCurriculumFromForm = null;
            this.btnAddSelectedCurriculum.Enabled = false;
        }

        private void dgvSelectedCurrriculum_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnResetSelection_Click_1(object sender, EventArgs e)
        {
            this.resetCurriculumSelectionControls();
        }

        private void dgvSelectedCurrriculum_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var EnquiryObj = (Data.Models.Enquiry)(row.DataBoundItem);
                    Data.Models.Curriculum CCC = (from a in CurrentlySelectedCurriculum
                                                  where a.CurriculumID == EnquiryObj.CurriculumID
                                                  select a).FirstOrDefault();
                    row.Cells[colCurriculum.Index].Value = CCC.CurriculumName.ToString();
                    row.Cells[colDepartment.Index].Value = CCC.LookupDepartment.DepartmentName.ToString();
                }
            }
        }


    }
}
