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

namespace Impendulo.Development.NextOfKin
{
    public partial class frmAddUpdateNextOfKin : MetroForm
    {
        private int IndividualID { get; set; }
        public Data.Models.NextOfKin CurrentNextOfKin { get; set; }
        public frmAddUpdateNextOfKin(int IndividualID)
        {
            this.IndividualID = IndividualID;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.populateTitles();
            this.populateRelationTypes();
            if (IndividualID != 0)
            {
                this.setControls();
            }
            this.setContactAddUpdateButtons();
        }

        private void setControls()
        {
            //create individual with properties set
            Individual ContactObj;
            using (var Dbconnection = new MCDEntities())
            {
                ContactObj = (from a in Dbconnection.Individuals
                              where a.IndividualID == IndividualID
                              select a)
                              .Include(a => a.NextOfKin)
                              .FirstOrDefault<Individual>();
            };
            this.cboIndividualTitle.SelectedValue = ContactObj.TitleID;
            this.txtFirstName.Text = ContactObj.IndividualFirstName;
            this.txtSecondName.Text = ContactObj.IndividualSecondName;
            this.txtLastName.Text = ContactObj.IndividualLastname;
            this.cboRelationType.SelectedValue = ContactObj.NextOfKin.TypeOfRelationID;
        }
        private void populateTitles()
        {
            using (var Dbconnection = new MCDEntities())
            {
                lookupTitleBindingSource.DataSource = (from a in Dbconnection.LookupTitles
                                                       select a).ToList<LookupTitle>();
            };
        }
        private void populateRelationTypes()
        {
            using (var Dbconnection = new MCDEntities())
            {
                lookupTypeOfRelationBindingSource.DataSource = (from a in Dbconnection.LookupTypeOfRelations
                                                                select a).ToList<LookupTypeOfRelation>();
            };
        }
        private void setContactAddUpdateButtons()
        {
            if (IndividualID == 0)
            {
                btnAddContact.Visible = true;
                btnUpdateContact.Visible = false;
            }
            else
            {
                btnAddContact.Visible = false;
                btnUpdateContact.Visible = true;
            }
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        CurrentNextOfKin = new Data.Models.NextOfKin
                        {
                            Individual = new Individual()
                            {
                                TitleID = Convert.ToInt32(cboIndividualTitle.SelectedValue),
                                IndividualFirstName = txtFirstName.Text.ToString(),
                                IndividualSecondName = txtSecondName.Text.ToString(),
                                IndividualLastname = txtLastName.Text.ToString()
                            },
                            TypeOfRelationID = Convert.ToInt32(this.cboRelationType.SelectedValue)
                        };
                        Dbconnection.NextOfKins.Add(CurrentNextOfKin);
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

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        Data.Models.NextOfKin IndividualToUpdate = (from a in Dbconnection.NextOfKins
                                                                    where a.IndividualID == IndividualID
                                                                    select a).FirstOrDefault<Data.Models.NextOfKin>();
                        IndividualToUpdate.Individual.TitleID = Convert.ToInt32(cboIndividualTitle.SelectedValue);
                        IndividualToUpdate.Individual.IndividualFirstName = txtFirstName.Text.ToString();
                        IndividualToUpdate.Individual.IndividualSecondName = txtSecondName.Text.ToString();
                        IndividualToUpdate.Individual.IndividualLastname = txtLastName.Text.ToString();
                        IndividualToUpdate.TypeOfRelationID = Convert.ToInt32(cboRelationType.SelectedValue);
                        Dbconnection.SaveChanges();
                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();
                        if (CurrentNextOfKin != null)
                        {
                            CurrentNextOfKin.Individual.TitleID = Convert.ToInt32(cboIndividualTitle.SelectedValue);
                            CurrentNextOfKin.Individual.IndividualFirstName = txtFirstName.Text.ToString();
                            CurrentNextOfKin.Individual.IndividualSecondName = txtSecondName.Text.ToString();
                            CurrentNextOfKin.Individual.IndividualLastname = txtLastName.Text.ToString();
                            CurrentNextOfKin.TypeOfRelationID = Convert.ToInt32(cboRelationType.SelectedValue);
                        }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
