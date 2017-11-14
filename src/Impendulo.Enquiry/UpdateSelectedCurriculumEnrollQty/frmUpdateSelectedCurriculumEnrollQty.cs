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
using MetroFramework.Forms;

namespace Impendulo.Development.Enquiry
{
    public partial class frmUpdateSelectedCurriculumEnrollQty : MetroForm
    {

        public Data.Models.Enquiry CurrentlySelectedEnquiry { get; set; }
        public frmUpdateSelectedCurriculumEnrollQty()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUpdateSelectedCurriculumEnrollQty_Load(object sender, EventArgs e)
        {
            if (CurrentlySelectedEnquiry != null)
            {
                //if (this.nudQtyToEnroll.Minimum > )
                //{

                //}
                this.nudQtyToEnroll.Value = CurrentlySelectedEnquiry.EnrollmentQuanity;
            }
            else
            {
                this.nudQtyToEnroll.Value = 1;
            }
        }

        private void btnUpdateSelectedCurriculum_Click(object sender, EventArgs e)
        {
            CurrentlySelectedEnquiry.EnrollmentQuanity = (int)this.nudQtyToEnroll.Value;
            this.Close();
        }
    }
}
