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

namespace Impendulo.Development.Enrollment.ViewEnrollmentHistory
{
    public partial class frmEnrollmentViewHistory : MetroFramework.Forms.MetroForm
    {
        public int SelectedEnrollmentID { get; private set; }
        public frmEnrollmentViewHistory(int EnrollmentID)
        {
            this.SelectedEnrollmentID = EnrollmentID;
            InitializeComponent();
        }

        private void frmEnrollmentViewHistory_Load(object sender, EventArgs e)
        {
            refreshHistory();
        }

        private void refreshHistory()
        {
            populateHistory();
        }
        private void populateHistory()
        {

            using (var Dbconnection = new MCDEntities())
            {
                gETAllCourseEnrollmentHistoryByEnrollmentID_ResultBindingSource.DataSource = new Common.CustomSortableBindingList<GETAllCourseEnrollmentHistoryByEnrollmentID_Result>( Dbconnection.GETAllCourseEnrollmentHistoryByEnrollmentID(SelectedEnrollmentID).ToList());
            };
        }
    }
}
