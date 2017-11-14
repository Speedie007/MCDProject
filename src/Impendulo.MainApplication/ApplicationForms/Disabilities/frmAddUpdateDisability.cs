using Impendulo.Data.Models;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Deployment.Disablity
{
    public partial class frmAddUpdateDisability : MetroForm
    {

        private int StudentID { get; set; }
        private int DisablityID { get; set; }

        public StudentDisability CurrentStudentDisablity { get; set; }
        public frmAddUpdateDisability(int StudentID, int iDisabilityID)
        {
            this.StudentID = StudentID;
            this.DisablityID = iDisabilityID;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrePopulateRadioControls();
            refreshDisabilities();
        }

        #region Refresh Methods

        private void refreshDisabilities()
        {
            this.populateDisablities();
        }
        #endregion
        #region Populate Methods

        private void populateDisablities()
        {
            if (this.DisablityID == 0)
            {
                ((RadioButton)flowLayoutPanelDisablitiyCategory.Controls[0]).Checked = true;
                this.btnUpdateStudentDisablity.Visible = false;
                this.btnAddDisability.Visible = true;

            }
            else
            {
                this.btnUpdateStudentDisablity.Visible = true;
                this.btnAddDisability.Visible = false;
                foreach (Control con in flowLayoutPanelDisablitiyCategory.Controls)
                {
                    if (con is RadioButton)
                    {
                        if (Convert.ToInt32(((RadioButton)con).Tag) == this.DisablityID)
                        {
                            ((RadioButton)con).Checked = true;
                            // gbDisablitiyOptions.Enabled = false;
                            txtStudentDisablityNotes.Text = CurrentStudentDisablity.StudentDisabilityNotes;
                        }
                        else
                        {
                            ((RadioButton)con).Visible = false;
                        }
                    }
                }
            }
        }
        #endregion

        #region perpopulate Methods
        private void PrePopulateRadioControls()
        {

            using (var Dbconnection = new MCDEntities())
            {
                List<LookupDisability> LD = new List<LookupDisability>();
                if (this.DisablityID == 0)
                {
                    LD = (from a in Dbconnection.LookupDisabilities
                          select a).Except((from b in Dbconnection.StudentDisabilities
                                            where b.StudentID == this.StudentID
                                            select b.LookupDisability)).ToList<LookupDisability>();
                }
                else
                {
                    LD = Dbconnection.StudentDisabilities
                    .Where(a => a.DisabilityID == this.DisablityID && a.StudentID == this.StudentID)
                    .Include(a => a.LookupDisability).Select(a => a.LookupDisability)
                    .ToList<LookupDisability>();
                }

                foreach (LookupDisability item in LD)
                {
                    RadioButton Rad = new RadioButton();
                    Rad.Tag = item.DisabilityID;
                    Rad.Text = item.Disability;
                    int tempID = DisablityID;
                    Rad.CheckedChanged += Rad_CheckedChanged;
                    flowLayoutPanelDisablitiyCategory.Controls.Add(Rad);
                    DisablityID = tempID;
                }
            };

           
        }

        private void Rad_CheckedChanged(object sender, EventArgs e)
        {
            this.DisablityID = Convert.ToInt32(((RadioButton)sender).Tag);
        }

        private void perpoulateDisablities()
        {
            flowLayoutPanelDisablitiyCategory.Controls.Clear();
            List<LookupDisability> LDL = new List<LookupDisability>();
            using (var Dbconnection = new MCDEntities())
            {
                if (this.DisablityID == 0)
                {
                    LDL = (from a in Dbconnection.LookupDisabilities
                           select a).Except((from b in Dbconnection.StudentDisabilities
                                             where b.StudentID == this.StudentID
                                             select b.LookupDisability)).ToList<LookupDisability>();
                    txtStudentDisablityNotes.Clear();

                }
                else
                {
                    LDL =
                        (from a in Dbconnection.LookupDisabilities
                         select a).ToList<LookupDisability>();
                    if (CurrentStudentDisablity == null)
                    {
                        CurrentStudentDisablity = Dbconnection.StudentDisabilities
                        .Where(a => a.DisabilityID == this.DisablityID && a.StudentID == this.StudentID)
                        .Include(a => a.LookupDisability)
                        .FirstOrDefault<StudentDisability>();
                    }
                    txtStudentDisablityNotes.Text = CurrentStudentDisablity.StudentDisabilityNotes;
                }

                Boolean isFirstItem = true;
                foreach (LookupDisability LD in LDL)
                {
                    RadioButton Rad = new RadioButton();


                    if (DisablityID == 0)
                    {
                        if (isFirstItem)
                        {
                            Rad.Checked = true;
                            isFirstItem = false;
                        }
                        Rad.Click += Rad_Click;
                        DisablityID = LD.DisabilityID;

                        this.btnAddDisability.Visible = true;
                        this.btnUpdateStudentDisablity.Visible = false;
                    }
                    else
                    {
                        if (DisablityID == LD.DisabilityID)
                        {
                            Rad.Click += Rad_Click;
                            Rad.Checked = true;
                            Rad.Tag = LD.DisabilityID;
                            Rad.Text = LD.Disability;
                            flowLayoutPanelDisablitiyCategory.Controls.Add(Rad);
                            DisablityID = LD.DisabilityID;
                            this.btnAddDisability.Visible = false;
                            this.btnUpdateStudentDisablity.Visible = true;
                        }

                    }


                }
                foreach (Control Con in flowLayoutPanelDisablitiyCategory.Controls)
                {
                    if (Con is RadioButton)
                    {
                        if (((RadioButton)Con).Checked)
                        {
                            this.DisablityID = Convert.ToInt32(((RadioButton)Con).Tag);
                        }
                    }
                }
            };
        }

        private void Rad_Click(object sender, EventArgs e)
        {
            this.DisablityID = Convert.ToInt32(((RadioButton)sender).Tag);
        }




        #endregion

        #region Refresh Methods


        #endregion

        #region Populate Methods

        #endregion

        private void btnCancelAddingStudentDisablity_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddUpdateStudentDisablity_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        if (CurrentStudentDisablity != null)
                        {

                            Dbconnection.StudentDisabilities.Attach(CurrentStudentDisablity);
                            CurrentStudentDisablity.StudentDisabilityNotes = this.txtStudentDisablityNotes.Text;
                            Dbconnection.Entry(CurrentStudentDisablity).State = EntityState.Modified;
                        }

                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();

                        //commit transaction
                        dbTran.Commit();
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

        private void btnAddDisability_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        CurrentStudentDisablity = new StudentDisability()
                        {
                            DisabilityID = this.DisablityID,
                            StudentID = this.StudentID,
                            StudentDisabilityNotes = this.txtStudentDisablityNotes.Text
                        };
                        Dbconnection.StudentDisabilities.Add(CurrentStudentDisablity);


                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();
                       
                        //commit transaction
                        dbTran.Commit();
                        Dbconnection.Entry(CurrentStudentDisablity).Reference(a => a.LookupDisability).Load();
                        
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
                    finally{
                        this.Close();
                    }
                }
            };

        }


    }
}
