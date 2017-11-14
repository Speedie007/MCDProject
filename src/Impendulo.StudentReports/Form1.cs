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
using System.Reflection;
using Impendulo.Common.Reports;
using Impendulo.Common.ReportDataSources;
using Impendulo.Common.ReportModels;

namespace Impendulo.StudentReports.Development
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                var f = (from a in Dbconnection.Students
                         select a).ToList<Student>();
            };
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            using (var Dbconnection = new MCDEntities())
            {


                Impendulo.Common.Reports.AllEnrollments rpt = new Impendulo.Common.Reports.AllEnrollments();
                List<GetAllEnrollmentsCompanyAndStudent_Result> ds = Dbconnection.GetAllEnrollmentsCompanyAndStudent().ToList<GetAllEnrollmentsCompanyAndStudent_Result>();

                List<Impendulo.Common.ReportModels.Enrollments> x = new List<Enrollments>();

                //ds.ForEach(GetAllEnrollmentsCompanyAndStudent_Result GAECASR in )
                foreach (GetAllEnrollmentsCompanyAndStudent_Result GAECASR in ds)
                {
                    x.Add(new Impendulo.Common.ReportModels.Enrollments()
                    {
                        DepartmentName = GAECASR.DepartmentName,
                        AmountEnrolled = (int)GAECASR.AmountEnrolled,
                        Client = GAECASR.Client,
                        CourseLocation = GAECASR.CourseLocation,
                        CourseName = GAECASR.CourseName,
                        CurriculumName = GAECASR.CurriculumName,
                        ScheduleCompletionDate = GAECASR.ScheduleCompletionDate,
                        ScheduleStartDate = GAECASR.ScheduleStartDate
                    });
                }
                rpt.SetDataSource(x);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.RefreshReport();
            };
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            using (var Dbconnection = new MCDEntities())
            {


                Impendulo.Common.Reports.AllEnrollments rpt = new Impendulo.Common.Reports.AllEnrollments();

                List<GetAllEnrollmentsPerCompany_Result> ds = Dbconnection.GetAllEnrollmentsPerCompany(Common.Verifiction.OfProgressFiles.VerifyCompanyProgressFile(2004)).ToList<GetAllEnrollmentsPerCompany_Result>();

                List<Impendulo.Common.ReportModels.Enrollments> x = new List<Enrollments>();

                //ds.ForEach(GetAllEnrollmentsCompanyAndStudent_Result GAECASR in )
                foreach (GetAllEnrollmentsPerCompany_Result GAECASR in ds)
                {
                    int xx = 0;
                    if (GAECASR.ScheduleStartDate.Equals("Not Yet Secheduled"))
                    {
                        xx = 0;
                    }
                    else
                    {
                        xx = 1;
                    }
                    x.Add(new Impendulo.Common.ReportModels.Enrollments()
                    {
                        DepartmentName = GAECASR.DepartmentName,
                        AmountEnrolled = (int)GAECASR.AmountEnrolled,
                        // Client = GAECASR.Client,
                        DeterminIfScheduledOrNot = xx,
                        CourseLocation = GAECASR.CourseLocation,
                        CourseName = GAECASR.CourseName,
                        CurriculumName = GAECASR.CurriculumName,
                        ScheduleCompletionDate = GAECASR.ScheduleCompletionDate,
                        ScheduleStartDate = GAECASR.ScheduleStartDate
                    });
                }
                rpt.SetDataSource(x);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.RefreshReport();
            };
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities()) {

                var x = (from a in Dbconnection.Schedules
                         from b in a.Enrollment.CurriculumCourseEnrollments
                         where a.ScheduleStartDate > DateTime.Now

                         select new
                         {

                         });
            };
        }
    }
}
