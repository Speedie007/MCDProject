using Impendulo.Data.Models;
using Impendulo.Deployment.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Development.Files.EnquirySwitch.ToPrivate
{
    public partial class SwitchToPrivate : MetroFramework.Forms.MetroForm
    {
        #region Global Variables
        private Individual CurrentlySelectedIndividual { get; set; }
        private Data.Models.Company CurrentlySelectedCompany { get; set; }
        private Boolean MustSwitchFromStudentToCompany { get; set; }
        #endregion
        public SwitchToPrivate(Data.Models.Company CurrentlySelectedCompany, Individual CurrentlySelectedIndividual, Boolean MustSwitchFromStudentToCompany = true)
        {
            this.MustSwitchFromStudentToCompany = MustSwitchFromStudentToCompany;
            this.CurrentlySelectedIndividual = CurrentlySelectedIndividual;
            this.CurrentlySelectedCompany = CurrentlySelectedCompany;
            InitializeComponent();
        }

        private void SwitchToPrivate_Load(object sender, EventArgs e)
        {

        }

       

        
       
        private void refreshStudent(int StudentID)
        {
            populateStudent();
        }
        private void populateStudent()
        {
            using (var Dbconnection = new MCDEntities())
            {
                this.CurrentlySelectedIndividual = (from a in Dbconnection.Individuals
                                                    where a.IndividualID == CurrentlySelectedIndividual.IndividualID
                                                    select a)
                                                    .Include(a => a.Student)
                                                    .Include(a => a.ContactDetails)
                                                    .Include(a => a.ContactDetails.Select(b => b.LookupContactType))
                                                    .FirstOrDefault<Individual>();

                /*Verfiy to determine if Student Progress Files Exists
                *****************************************************/
                int StudentProgressFileID = Common.Verifiction.OfProgressFiles.VerifyStudentProgressFile(CurrentlySelectedIndividual.IndividualID);

                //CurrentlySelectedProgressFile = (from a in Dbconnection.ProgressFiles
                //                                 where a.ProgressFileID == StudentProgressFileID
                //                                 select a)
                //                                 .Include(a => a.StudentProgressFile)
                //                                 .FirstOrDefault<ProgressFile>();
            };
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {

        }
    }
}
