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

namespace Impendulo.StudentReports
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var Dbconnection = new MCDEntities())
            {
                Impendulo.Common.Reports.GeneralReports.AllCompanyEnrollments.AllCompanyEnrollments rpt = new Impendulo.Common.Reports.GeneralReports.AllCompanyEnrollments.AllCompanyEnrollments();
                List<GetAllEnrollmentsCompanyAndStudent_Result> ds = Dbconnection.GetAllEnrollmentsCompanyAndStudent().ToList<GetAllEnrollmentsCompanyAndStudent_Result>();

                List<Impendulo.Common.ReportModels.General.AllCompanyEnrollments.AllCompanyEnrollments> x = new List<Impendulo.Common.ReportModels.General.AllCompanyEnrollments.AllCompanyEnrollments>();

                //ds.ForEach(GetAllEnrollmentsCompanyAndStudent_Result GAECASR in )
                foreach (GetAllEnrollmentsCompanyAndStudent_Result GAECASR in ds)
                {
                    x.Add(new Impendulo.Common.ReportModels.General.AllCompanyEnrollments.AllCompanyEnrollments()
                    {
                        Location = GAECASR.CourseLocation,
                        DepartmentName = GAECASR.DepartmentName,
                        AmountEnrolled = (int)GAECASR.AmountEnrolled,
                        Client = GAECASR.Client,
                        ScheduledCompletionDate = GAECASR.ScheduleCompletionDate,
                        ScheduledStartDate =  GAECASR.ScheduleStartDate,
                        CourseName = GAECASR.CourseName,
                        CurriculumName = GAECASR.CurriculumName
                    });
                }
                rpt.SetDataSource(x);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.RefreshReport();
            };
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
