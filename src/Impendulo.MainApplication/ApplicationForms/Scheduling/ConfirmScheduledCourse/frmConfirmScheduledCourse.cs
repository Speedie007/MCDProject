using Impendulo.Common.Enum;
using Impendulo.Data.Models;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Deployment.Scheduling.ConfirmScheduledCourse
{
    public partial class frmConfirmScheduledCourse : MetroFramework.Forms.MetroForm
    {

        #region Global Variables
        public int ScheduleID { get; private set; }
        public int EnrollmentID { get; private set; }
        private List<FilesToUpload> ListOfFilesToUpload { get; set; }
        private Boolean ConfirmAllScheduledCourses { get; set; }
        #endregion

        #region Internal DAtaStructure

        public class FilesToUpload
        {
            public int FileID { get; set; }
            public string FileName { get; set; }
            public string FileType { get; set; }
            public Data.Models.File FileToupload { get; set; }
            public int ScheduleConfirmationDocumentationTypeID { get; set; }


        }
        #endregion
        public frmConfirmScheduledCourse(int ScheduleID, int EnrollmentID, Boolean ConfirmAllScheduledCourses = false)
        {
            this.ConfirmAllScheduledCourses = ConfirmAllScheduledCourses;
            ListOfFilesToUpload = new List<FilesToUpload>();
            this.EnrollmentID = EnrollmentID;
            this.ScheduleID = ScheduleID;
            InitializeComponent();
        }

        private void frmConfirmScheduledCourse_Load(object sender, EventArgs e)
        {
            refreshEnrollmentDocumentTypes();
        }


        #region refresh Methods
        private void refreshEnrollmentDocumentTypes()
        {
            this.populateEnrollmentTypes();
        }
        private void refreshFilesToBeUploaded()
        {
            populateFilesToBeUploaded();
        }
        #endregion
        #region Populate Methods

        private void populateFilesToBeUploaded()
        {
            //List<FilesToUpload> UF = new List<FilesToUpload>();

            //foreach (FilesToUpload f in ListOfFilesToUpload)
            //{
            //    string sFileType = cboEnrollmentDocumentType.SelectedText;
            //    UF.Add(new FilesToUpload()
            //    {
            //        FileID = f.FileID,
            //        FileName = f.FileName,
            //        FileType = sFileType
            //    });
            //}
            FilesToUploadBindingSource.DataSource = new Common.CustomSortableBindingList<FilesToUpload>(ListOfFilesToUpload);
        }
        private void populateEnrollmentTypes()
        {
            using (var Dbconnection = new MCDEntities())
            {
                lookupScheduleConfirmationDocumentationTypeBindingSource.DataSource = (from a in Dbconnection.LookupScheduleConfirmationDocumentationTypes
                                                                                       select a).ToList<LookupScheduleConfirmationDocumentationType>();
            };
        }
        #endregion



        private void cboEnrollmentDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnConfirmScheduledCourseModule_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        int ScheduleConfirmationDocumentationType = Convert.ToInt32(cboEnrollmentDocumentType.SelectedValue);
                        if (ConfirmAllScheduledCourses)
                        {
                            foreach (FilesToUpload f in ListOfFilesToUpload)
                            {
                                Dbconnection.Files.Add(f.FileToupload);
                                Dbconnection.SaveChanges();
                            }
                            List<Schedule> AllScheduledCourse = Dbconnection.Schedules.Where(a => a.EnrollmentID == this.EnrollmentID).ToList<Schedule>();
                            foreach (Schedule s in AllScheduledCourse)
                            {
                                foreach (FilesToUpload f in ListOfFilesToUpload)
                                {
                                    ScheduleConfirmationDocumentation ED = new ScheduleConfirmationDocumentation()
                                    {
                                        ScheduleID = s.ScheduleID,
                                        ScheduleConfirmationDocumentationTypeID = f.ScheduleConfirmationDocumentationTypeID,
                                        ImageID = f.FileToupload.FileID
                                    };
                                    Dbconnection.ScheduleConfirmationDocumentations.Add(ED);
                                }
                                s.ScheduleStatusID = (int)EnumScheduleStatuses.Confirmed;
                            }
                            
                        }
                        else
                        {
                            /*First Upload Each of the Documents
                            * ********************************/
                            foreach (FilesToUpload f in ListOfFilesToUpload)
                            {
                                Dbconnection.Files.Add(f.FileToupload);
                                Dbconnection.SaveChanges();
                                ScheduleConfirmationDocumentation ED = new ScheduleConfirmationDocumentation()
                                {
                                    ScheduleID = this.ScheduleID,
                                    ScheduleConfirmationDocumentationTypeID = f.ScheduleConfirmationDocumentationTypeID,
                                    ImageID = f.FileToupload.FileID
                                };
                                Dbconnection.ScheduleConfirmationDocumentations.Add(ED);
                            }
                            Dbconnection.Schedules.Where(a => a.ScheduleID == this.ScheduleID).FirstOrDefault().ScheduleStatusID = (int)EnumScheduleStatuses.Confirmed;
                        }
                        ///*First Upload Each of the Documents
                        // * ********************************/
                        //foreach (FilesToUpload f in ListOfFilesToUpload)
                        //{
                        //    Dbconnection.Files.Add(f.FileToupload);
                        //    Dbconnection.SaveChanges();
                        //    ScheduleConfirmationDocumentation ED = new ScheduleConfirmationDocumentation()
                        //    {
                        //        ScheduleID = this.ScheduleID,
                        //        ScheduleConfirmationDocumentationTypeID = f.ScheduleConfirmationDocumentationTypeID,
                        //        ImageID = f.FileToupload.FileID
                        //    };
                        //    Dbconnection.ScheduleConfirmationDocumentations.Add(ED);
                        //}


                        //Dbconnection.Schedules.Where(a => a.ScheduleID == this.ScheduleID).FirstOrDefault().ScheduleStatusID = (int)EnumScheduleStatuses.Confirmed;

                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();

                        //commit transaction
                        dbTran.Commit();
                        MetroMessageBox.Show(this, "Confirmation Processed Successfully!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex is DbEntityValidationException)
                        {
                            foreach (DbEntityValidationResult entityErr in ((DbEntityValidationException)ex).EntityValidationErrors)
                            {
                                foreach (DbValidationError error in entityErr.ValidationErrors)
                                {
                                    MessageBox.Show(error.ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        //Rollback transaction if exception occurs
                        dbTran.Rollback();
                    }
                }
            };

        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            //ListOfFilesToUpload =
            foreach (Data.Models.File f in Common.FileHandeling.FileHandeling.UploadFile(
            UseMultipleFileSelect: true,
             AutomaicallyAddFileToDatabase: false,
              ImagesOnly: false))
            {
                ListOfFilesToUpload.Add(new FilesToUpload()
                {
                    FileName = f.FileName,
                    FileToupload = f,
                    ScheduleConfirmationDocumentationTypeID = Convert.ToInt32(cboEnrollmentDocumentType.SelectedValue)
                });
            }
            refreshFilesToBeUploaded();
        }

        private void btnREmoveUploadedFile_Click(object sender, EventArgs e)
        {
            if (FilesToUploadBindingSource.Count > 0)
            {
                ListOfFilesToUpload.RemoveAt(FilesToUploadBindingSource.Position);
            }
            refreshFilesToBeUploaded();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
