using Impendulo.Common.ReportModels.Schedules.SectionalViews;
using Impendulo.Common.Reports.Schedules.SectionalView;
using Impendulo.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Development.Reports.Reports.Schedules
{
    public partial class frmSectionalView : MetroFramework.Forms.MetroForm
    {
        private enumScheduleSectionialViewToDisplay SectioanlViewToDisplay { get; set; }
        public frmSectionalView(enumScheduleSectionialViewToDisplay SectioanlViewToDisplay)
        {
            this.SectioanlViewToDisplay = SectioanlViewToDisplay;
            InitializeComponent();
        }

        private void frmSectionalView_Load(object sender, EventArgs e)
        {
            loadReport();
        }

        private void loadReport()
        {


            rptScheduleSectionalView rpt = new rptScheduleSectionalView();

            List<SectionalView> x = new List<SectionalView>();

            //ds.ForEach(GetAllEnrollmentsCompanyAndStudent_Result GAECASR in )
            using (var Dbconnection = new MCDEntities())
            {
                switch (SectioanlViewToDisplay)
                {
                    case enumScheduleSectionialViewToDisplay.NotYetConfirmed:
                        foreach (REPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_Result GSR in Dbconnection.REPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules())
                        {
                            x.Add(new SectionalView()
                            {
                                AmountEnrolled = (int)GSR.AmountEnrolled,
                                CostCode = GSR.CostCode,
                                //CourseID = GSR.CourseID,
                                CourseName = GSR.CourseName,
                                //CurriculumCourseID = GSR.CurriculumCourseID,
                                CurriculumName = GSR.CurriculumName,
                                // DepartmentID = GSR.DepartmentID,
                                DepartmentName = GSR.DepartmentName,
                                EndDate = GSR.EndDate,
                                EnrollmentProgressCurrentState = GSR.EnrollmentProgressCurrentState,
                                //FacilitatorID = GSR.FacilitatorID,
                                FacilitatorName = GSR.FacilitatorName,
                                LookupEnrollmentProgressStateID = GSR.LookupEnrollmentProgressStateID,
                                LookupScheduleLocationID = GSR.LookupScheduleLocationID,
                                ReportHeading = "Enrollment Which Have Not Yet Been Scheduled.",
                                StartDate = GSR.StartDate,
                                //VenueID = (int)GSR.VenueID,
                                VenueName = GSR.VenueName
                            });
                        }

                        break;
                    case enumScheduleSectionialViewToDisplay.Confirmed_NotYetStarted:
                        foreach (REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet_Result GSR in Dbconnection.REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet())
                        {
                            x.Add(new SectionalView()
                            {
                               
                                AmountEnrolled = (int)GSR.AmountEnrolled,
                                CostCode = GSR.CostCode,
                                //CourseID = GSR.CourseID,
                                CourseName = GSR.CourseName,
                                //CurriculumCourseID = GSR.CurriculumCourseID,
                                CurriculumName = GSR.CurriculumName,
                                // DepartmentID = GSR.DepartmentID,
                                DepartmentName = GSR.DepartmentName,
                                EndDate = GSR.EndDate,
                                //EnrollmentProgressCurrentState = GSR.EnrollmentProgressCurrentState,
                                //FacilitatorID = GSR.FacilitatorID,
                                FacilitatorName = GSR.FacilitatorName,
                                //LookupEnrollmentProgressStateID = GSR.LookupEnrollmentProgressStateID,
                                LookupScheduleLocationID = GSR.LookupScheduleLocationID,
                                ReportHeading = "All Scheduled Courese which have not Yet Started",
                                StartDate = GSR.StartDate,
                                //VenueID = (int)GSR.VenueID,
                                VenueName = GSR.VenueName
                            });
                        }

                        break;
                    case enumScheduleSectionialViewToDisplay.Confirmed_InProgress:

                        foreach (REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress_Result GSR in Dbconnection.REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress())
                        {
                            x.Add(new SectionalView()
                            {
                                AmountEnrolled = (int)GSR.AmountEnrolled,
                                CostCode = GSR.CostCode,
                                //CourseID = GSR.CourseID,
                                CourseName = GSR.CourseName,
                                //CurriculumCourseID = GSR.CurriculumCourseID,
                                CurriculumName = GSR.CurriculumName,
                                // DepartmentID = GSR.DepartmentID,
                                DepartmentName = GSR.DepartmentName,
                                EndDate = GSR.EndDate,
                                // EnrollmentProgressCurrentState = GSR.EnrollmentProgressCurrentState,
                                //FacilitatorID = GSR.FacilitatorID,
                                FacilitatorName = GSR.FacilitatorName,
                                // LookupEnrollmentProgressStateID = GSR.LookupEnrollmentProgressStateID,
                                LookupScheduleLocationID = GSR.LookupScheduleLocationID,
                                ReportHeading = "Scheduled Courses Which Are Currently Inprogress",
                                StartDate = GSR.StartDate,
                                //VenueID = (int)GSR.VenueID,
                                VenueName = GSR.VenueName
                            });
                        }
                        break;
                    case enumScheduleSectionialViewToDisplay.Confirmed_REquireReports:
                        foreach (REPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_Result GSR in Dbconnection.REPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate())
                        {
                            x.Add(new SectionalView()
                            {
                                AmountEnrolled = (int)GSR.AmountEnrolled,
                                CostCode = GSR.CostCode,
                                //CourseID = GSR.CourseID,
                                CourseName = GSR.CourseName,
                                //CurriculumCourseID = GSR.CurriculumCourseID,
                                CurriculumName = GSR.CurriculumName,
                                // DepartmentID = GSR.DepartmentID,
                                DepartmentName = GSR.DepartmentName,
                                EndDate = GSR.EndDate,
                                // EnrollmentProgressCurrentState = GSR.EnrollmentProgressCurrentState,
                                //FacilitatorID = GSR.FacilitatorID,
                                FacilitatorName = GSR.FacilitatorName,
                                // LookupEnrollmentProgressStateID = GSR.LookupEnrollmentProgressStateID,
                                LookupScheduleLocationID = GSR.LookupScheduleLocationID,
                                ReportHeading = "Courses Which Require Reports Feed Back From Facilitator.",
                                StartDate = GSR.StartDate,
                                //VenueID = (int)GSR.VenueID,
                                VenueName = GSR.VenueName
                            });
                        }

                        break;
                    case enumScheduleSectionialViewToDisplay.Confirmed_Complete:

                        foreach (REPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_Result GSR in Dbconnection.REPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate())
                        {
                            x.Add(new SectionalView()
                            {
                                AmountEnrolled = (int)GSR.AmountEnrolled,
                                CostCode = GSR.CostCode,
                                //CourseID = GSR.CourseID,
                                CourseName = GSR.CourseName,
                                //CurriculumCourseID = GSR.CurriculumCourseID,
                                CurriculumName = GSR.CurriculumName,
                                // DepartmentID = GSR.DepartmentID,
                                DepartmentName = GSR.DepartmentName,
                                EndDate = GSR.EndDate,
                                // EnrollmentProgressCurrentState = GSR.EnrollmentProgressCurrentState,
                                //FacilitatorID = GSR.FacilitatorID,
                                FacilitatorName = GSR.FacilitatorName,
                                // LookupEnrollmentProgressStateID = GSR.LookupEnrollmentProgressStateID,
                                LookupScheduleLocationID = GSR.LookupScheduleLocationID,
                                ReportHeading = "Completed Course - Archieved.(History)",
                                StartDate = GSR.StartDate,
                                //VenueID = (int)GSR.VenueID,
                                VenueName = GSR.VenueName
                            });
                        }
                        break;
                }

            };
            rpt.SetDataSource(x);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.RefreshReport();
        }
    }
}
