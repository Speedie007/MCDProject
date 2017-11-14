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

namespace Impendulo.Development.Scheduling.SubForms
{
    public partial class frmSchedulingDurationAdjustment : MetroFramework.Forms.MetroForm
    {

        #region Gloabl Variables
        public int CurrentDuration { get; private set; }
        public CurriculumCourseEnrollment CurrentCourseEnrollment { get; private set; }
        #endregion
        public frmSchedulingDurationAdjustment(CurriculumCourseEnrollment CurrentCourseEnrollment)
        {
            this.CurrentCourseEnrollment = CurrentCourseEnrollment;
            InitializeComponent();
        }

        private void frmSchedulingDurationAdjustment_Load(object sender, EventArgs e)
        {
            txtCurrentDuration.Text = CurrentCourseEnrollment.CurriculumCourse.Duration.ToString();
            nudNewDuration.Value = CurrentCourseEnrollment.CurriculumCourse.Duration;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CurrentCourseEnrollment.CurriculumCourse.Duration = (int)nudNewDuration.Value;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
