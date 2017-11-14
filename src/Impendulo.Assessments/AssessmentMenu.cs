using Impendulo.Developments.Assessments.ApprenticeshipAssessments;
using Impendulo.Developments.Assessments.ApprenticeshipAssessments.AddNewReport;
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

namespace Impendulo.Developments.Assessments
{
    public partial class AssessmentMenu : MetroForm
    {
        public AssessmentMenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            using (frmApprenticeshipAssessmentReport frm = new frmApprenticeshipAssessmentReport())
            {
                frm.ShowDialog();
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            using (frmAddNewApprenticeshipAssessmentReport frm = new frmAddNewApprenticeshipAssessmentReport(null))
            {
                frm.ShowDialog();
            }
        }
    }
}
