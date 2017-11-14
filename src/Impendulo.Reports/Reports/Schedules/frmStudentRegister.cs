using Impendulo.Common.ReportModels.Schedules.Registers;
using Impendulo.Common.Reports.Schedules.StudentRegister;
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

namespace Impendulo.Developments.Reports.Reports.Schedules
{
    public partial class frmStudentRegister : MetroFramework.Forms.MetroForm
    {
        public frmStudentRegister()
        {
            InitializeComponent();
        }

        private void frmStudentRegister_Load(object sender, EventArgs e)
        {
            CurrentlySelectedConfirmationType = ConfrimationSelection.SelectAll;
            CurrentlySelectedDatePeriod = DateTypeSelection.CurrentlMonthOnly;
            this.LoadReport();
        }

        private ConfrimationSelection CurrentlySelectedConfirmationType { get; set; }
        private DateTypeSelection CurrentlySelectedDatePeriod { get; set; }
        private enum DateTypeSelection
        {
            CurrentlMonthOnly,
            CustomDatePeriod
        }
        private enum ConfrimationSelection
        {
            SelectAll,
            SelectOnlyConfirmed,
            SeletOnlyNotYetCopnfirmed
        }

        private void LoadReport()
        {
            DateTime dtStartDate = DateTime.Now;
            DateTime dtEndDate = DateTime.Now;

            switch (CurrentlySelectedDatePeriod)
            {
                case DateTypeSelection.CurrentlMonthOnly:
                    dtStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dtEndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                    break;
                case DateTypeSelection.CustomDatePeriod:
                    //58,c5,cb,25,40,91
                    dtStartDate = metroDateTime1.Value.Date;
                    dtEndDate = metroDateTime2.Value.Date;
                    break;
            }
            List<GetStudentRegister_Result> ds = new List<GetStudentRegister_Result>();
            switch (CurrentlySelectedConfirmationType)
            {
                case ConfrimationSelection.SelectAll:

                    using (var Dbconnection = new MCDEntities())
                    {
                        ds = Dbconnection.GetStudentRegister(dtStartDate, dtEndDate).ToList<GetStudentRegister_Result>();
                    };


                    break;
                case ConfrimationSelection.SelectOnlyConfirmed:
                    using (var Dbconnection = new MCDEntities())
                    {
                        ds = Dbconnection.GetStudentRegister(dtStartDate, dtEndDate).Where(a => a.Confirmed == "Y").ToList<GetStudentRegister_Result>();
                    };

                    break;
                case ConfrimationSelection.SeletOnlyNotYetCopnfirmed:
                    using (var Dbconnection = new MCDEntities())
                    {
                        ds = Dbconnection.GetStudentRegister(dtStartDate, dtEndDate).Where(a => a.Confirmed == "N").ToList<GetStudentRegister_Result>();
                    };

                    break;
            }

            ScheduleStudentRegister rpt = new ScheduleStudentRegister();

            List<StudentRegister> x = new List<StudentRegister>();
            //ds.ForEach(GetAllEnrollmentsCompanyAndStudent_Result GAECASR in )
            foreach (GetStudentRegister_Result GSR in ds)
            {
                x.Add(new StudentRegister()
                {
                    Company = GSR.Company,
                    Confirmed = GSR.Confirmed,
                    CostCode = GSR.CostCode,
                    Course = GSR.Course,
                    Department = GSR.Department,
                    Duration = GSR.Duration,
                    EndDate = GSR.EndDate,
                    IDNumber = GSR.IDNumber,
                    StartDate = GSR.StartDate,
                    StudentName = GSR.StudentName,
                    CurriculumName = GSR.CurriculumName

                });
            }
            rpt.SetDataSource(x);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.RefreshReport();

        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {
            if (metroDateTime2.Value.Date > metroDateTime2.Value.Date)
            {
                metroDateTime2.Value = metroDateTime1.Value;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                LoadReport();

                //ScheduleStudentRegister rpt = new ScheduleStudentRegister();
                //List<GetStudentRegister_Result> ds = Dbconnection.GetStudentRegister(metroDateTime1.Value, metroDateTime2.Value).ToList<GetStudentRegister_Result>();

                //List<StudentRegister> x = new List<StudentRegister>();

                ////ds.ForEach(GetAllEnrollmentsCompanyAndStudent_Result GAECASR in )
                //foreach (GetStudentRegister_Result GSR in ds)
                //{
                //    x.Add(new StudentRegister()
                //    {
                //        Company = GSR.Company,
                //        Confirmed = GSR.Confirmed,
                //        CostCode = GSR.CostCode,
                //        Course = GSR.Course,
                //        Department = GSR.Department,
                //        Duration = GSR.Duration,
                //        EndDate = GSR.EndDate,
                //        IDNumber = GSR.IDNumber,
                //        StartDate = GSR.StartDate,
                //        StudentName = GSR.StudentName,
                //        CurriculumName = GSR.CurriculumName

                //    });
                //}
                //rpt.SetDataSource(x);
                //crystalReportViewer1.ReportSource = rpt;
                //crystalReportViewer1.RefreshReport();
            };
        }

        private void radSelectAllSchedules_CheckedChanged(object sender, EventArgs e)
        {
            CurrentlySelectedConfirmationType = ConfrimationSelection.SelectAll;
            LoadReport();
        }

        private void radSelectOnlyConfirmedSchedules_CheckedChanged(object sender, EventArgs e)
        {
            CurrentlySelectedConfirmationType = ConfrimationSelection.SelectOnlyConfirmed;
            LoadReport();
        }

        private void radSelectOnlyNotYetConfirmed_CheckedChanged(object sender, EventArgs e)
        {
            CurrentlySelectedConfirmationType = ConfrimationSelection.SeletOnlyNotYetCopnfirmed;
            LoadReport();
        }

        private void radCurrentMonthSelection_CheckedChanged(object sender, EventArgs e)
        {
            gbCustomDatePeriodSelector.Enabled = false;
            CurrentlySelectedDatePeriod = DateTypeSelection.CurrentlMonthOnly;
            LoadReport();
        }

        private void radCustomDateSelection_CheckedChanged(object sender, EventArgs e)
        {
            gbCustomDatePeriodSelector.Enabled = true;
            CurrentlySelectedDatePeriod = DateTypeSelection.CustomDatePeriod;
        }

        private void metroDateTime2_ValueChanged(object sender, EventArgs e)
        {
            if (metroDateTime2.Value.Date < metroDateTime1.Value.Date)
            {
                metroDateTime1.Value = metroDateTime2.Value;
            }
        }
    }

}

