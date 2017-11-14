using Impendulo.Development.Scheduling.ConfirmScheduledCourse;
using Impendulo.Development.Scheduling.CourseScheduleSections;
using Impendulo.Development.Scheduling.ScheduleSummary;
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


namespace Impendulo.Development.Scheduling
{
    public partial class frmMenu : MetroForm
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //using (frmScheduleCurriculumCourses frm = new frmScheduleCurriculumCourses())
            //{
            //    frm.ShowDialog();
            //}
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            using (frmPreCalculationTest frm = new frmPreCalculationTest())
            {
                frm.ShowDialog();
            }

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            using (frmScheduleSummary frm = new frmScheduleSummary())
            {
                frm.ShowDialog();
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            using (frmConfirmScheduledCourse frm = new frmConfirmScheduledCourse(4089, 11135))
            {
                frm.ShowDialog();
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            using (frmCourseScheduleSections frm = new frmCourseScheduleSections())
            {
                frm.ShowDialog();
            }
        }
    }
}
