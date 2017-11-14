
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
using MetroFramework.Forms;
using Impendulo.Development.Company;
using Impendulo.Development.ContactDetails;
using Impendulo.Development.Contacts;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace Impendulo.Development.Enquiry

{
    public partial class frmSelectCompanyContact : MetroForm
    {
        public Data.Models.Company CurrentCompany { get; set; }
        public Individual SelectedIndividual { get; set; }

        public Boolean IsCanceled { get; set; }

        private string SerachCriteriaCompanyName { get; set; }
        private string SerachCriteriaContactName { get; set; }
        public frmSelectCompanyContact()
        {
            this.IsCanceled = false;
            this.CurrentCompany = new Data.Models.Company();
            InitializeComponent();
        }

        private void frmSelectCompanyContact_Load(object sender, EventArgs e)
        {
            setContactNameSearchCriteria();
            if (CurrentCompany != null)
            {
                txtCompanyName.Text = CurrentCompany.CompanyName;

                this.refreshCompanyContacts();
                this.refreshCompanyContactDetails();

                int SelectedIndividualIndex = 0;
                if (SelectedIndividual != null)
                {
                    foreach (Individual x in individualBindingSource.List)
                    {
                        if (x.IndividualID == SelectedIndividual.IndividualID)
                        {
                            individualBindingSource.Position = SelectedIndividualIndex;
                        }
                        SelectedIndividualIndex++;
                    }
                }

                this.setDisplayControlsToEnabled();
            }
            else
            {
                this.setDisplayControlsToDisabled();


            }


        }

        private void setDisplayControlsToEnabled()
        {
            this.txtCompanyName.Text = CurrentCompany.CompanyName;
            this.gbContacts.Enabled = true;
            this.btnSelectContact.Enabled = true;
        }

        private void setDisplayControlsToDisabled()
        {
            this.btnSelectContact.Enabled = false;
            this.gbContacts.Enabled = false;
            this.txtCompanyName.Clear();
        }
        private void refreshCompanyContacts()
        {
            if (CurrentCompany != null)
            {
                this.populateCompanyContacts();
                if (individualBindingSource.Count > 0)
                {
                    this.btnSelectContact.Enabled = true;
                }
                else
                {
                    this.btnSelectContact.Enabled = false;
                }
            }


        }
        private void refreshCompanyContactDetails()
        {
            int _IndividualID = 0;
            if (individualBindingSource.Count > 0 && CurrentCompany != null)
            {
                //_IndividualID = ((Individual)individualBindingSource.Current).IndividualID;
                this.populateCompanyContactDetails();
            }
            else
            {
                this.contactDetailBindingSource.DataSource = null;
            }
        }




        private void populateCompanyContactDetails()
        {
            int _IndividualID = ((Individual)individualBindingSource.Current).IndividualID;

            using (var Dbconnection = new MCDEntities())
            {
                this.contactDetailBindingSource.DataSource = (from a in Dbconnection.Individuals
                                                              from b in a.ContactDetails
                                                              where a.IndividualID == _IndividualID
                                                              select b).ToList<ContactDetail>();
            };

        }
        private void populateCompanyContacts()
        {

            using (var Dbconnection = new MCDEntities())
            {
                this.individualBindingSource.DataSource = (from a in Dbconnection.Individuals
                                                           from b in a.Companies
                                                           where b.CompanyID == CurrentCompany.CompanyID
                                                           //&&
                                                           //a.IndividualFirstName.ToLower().Contains(SerachCriteriaContactName)
                                                           //   || a.IndividualSecondName.ToLower().Contains(SerachCriteriaContactName) ||
                                                           //   a.IndividualLastname.ToLower().Contains(SerachCriteriaContactName)
                                                           select a)
                                                           .Include(a => a.ContactDetails)
                                                           .Include(a => a.ContactDetails.Select(b => b.LookupContactType))
                                                           .Include(a => a.LookupTitle)
                                                           .ToList<Individual>();
            };

        }

        private void btnSelectCompany_Click(object sender, EventArgs e)
        {
            frmCompanySearch frm = new frmCompanySearch();

            frm.ShowDialog();
            CurrentCompany = frm.CurrentCompany;
            if (CurrentCompany != null)
            {
                this.setDisplayControlsToEnabled();
            }
            else
            {
                this.setDisplayControlsToDisabled();
                this.individualBindingSource.Clear();
            }
            this.refreshCompanyContacts();
            this.refreshCompanyContactDetails();
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {

            frmContactsAddUpdateContacts frm = new frmContactsAddUpdateContacts(0);
            frm.ShowDialog();
            //Data.Models.Company CompanyObj = (Data.Models.Company)companyBindingSource.Current;
            if (frm.CurrentContact != null)
            {
                if (frm.CurrentContact.IndividualID != 0)
                {

                    using (var Dbconnection = new MCDEntities())
                    {
                        using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                        {
                            try
                            {
                                //CRUD Operations
                                /*
			                        * this steps follow to both entities
			                        * 
			                        * 1 - create instance of entity with relative primary key
			                        * 
			                        * 2 - add instance to context
			                        * 
			                        * 3 - attach instance to context
			                    */

                                // 1
                                Data.Models.Company Comp = new Data.Models.Company()
                                {
                                    CompanyID = CurrentCompany.CompanyID

                                };
                                // 2
                                Dbconnection.Companies.Add(Comp);
                                // 3
                                Dbconnection.Companies.Attach(Comp);

                                // 1
                                Individual Individ = new Individual { IndividualID = frm.CurrentContact.IndividualID };
                                // 2
                                Dbconnection.Individuals.Add(Individ);
                                // 3
                                Dbconnection.Individuals.Attach(Individ);

                                // like previous method add instance to navigation property
                                Comp.Individuals.Add(Individ);

                                // call SaveChanges
                                Dbconnection.SaveChanges();

                                ////saves all above operations within one transaction
                                Dbconnection.SaveChanges();

                                //commit transaction
                                dbTran.Commit();

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


                    //using (var Dbconnection = new MCDEntities())
                    //{
                    //    Dbconnection.Individuals.Attach(frm.CurrentContact);
                    //    Dbconnection.Entry(frm.CurrentContact).Collection(a => a.ContactDetails).Load();
                    //};


                }
            }
            // CompanyObj.Individuals.Add(NewContact);
            this.refreshCompanyContacts();

        }

        private void individualBindingSource_PositionChanged(object sender, EventArgs e)
        {
            this.refreshCompanyContactDetails();
        }

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {


            Individual IndividualObj = (Individual)individualBindingSource.Current;

            //Data.Models.Company CompanyObj = (Data.Models.Company)companyBindingSource.Current;

            Individual IndividualToUpdate = null;
            foreach (Individual CurrentIndividualObj in CurrentCompany.Individuals)
            {
                System.Type type = IndividualObj.GetType();
                //if (CurrentIndividualObj.IndividualID == (int)type.GetProperty("IndividualID").GetValue(IndividualObj, null))
                if (CurrentIndividualObj.IndividualID == IndividualObj.IndividualID)
                {
                    IndividualToUpdate = CurrentIndividualObj;
                }
            }
            if (IndividualToUpdate != null)
            {
                using (frmContactsAddUpdateContacts frm = new frmContactsAddUpdateContacts(IndividualToUpdate.IndividualID))
                {
                    frm.CurrentContact = IndividualToUpdate;
                    frm.ShowDialog();
                    this.refreshCompanyContacts();
                };

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsCanceled = true;
            this.Close();
        }

        private void btnSelectContact_Click(object sender, EventArgs e)
        {
            SelectedIndividual = (Individual)individualBindingSource.Current;

            this.Close();
        }

        private void btnAddContactInfo_Click(object sender, EventArgs e)
        {
            Individual CurrentContact = (Individual)individualBindingSource.Current;
            frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0);
            frm.ShowDialog();
            if (frm.CurrentDetail != null)
            {

                using (var Dbconnection = new MCDEntities())
                {
                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            Individual CurrentIndividual = (Individual)individualBindingSource.Current;
                            //CRUD Operations
                            /*
                                * this steps follow to both entities
                                * 
                                * 1 - create instance of entity with relative primary key
                                * 
                                * 2 - add instance to context
                                * 
                                * 3 - attach instance to context
                            */

                            // 1
                            ContactDetail CD = new ContactDetail()
                            {
                                ContactDetailID = frm.CurrentDetail.ContactDetailID

                            };
                            // 2
                            Dbconnection.ContactDetails.Add(CD);
                            // 3
                            Dbconnection.ContactDetails.Attach(CD);

                            // 1
                            Individual Individ = new Individual { IndividualID = CurrentIndividual.IndividualID };
                            // 2
                            Dbconnection.Individuals.Add(Individ);
                            // 3
                            Dbconnection.Individuals.Attach(Individ);

                            // like previous method add instance to navigation property
                            Individ.ContactDetails.Add(CD);

                            // call SaveChanges
                            Dbconnection.SaveChanges();
                            ////saves all above operations within one transaction

                            //commit transaction
                            dbTran.Commit();
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



                this.refreshCompanyContactDetails();
            }
        }

        private void btnUpdateContactDetials_Click(object sender, EventArgs e)
        {
            frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(((ContactDetail)contactDetailBindingSource.Current).ContactDetailID);
            frm.CurrentDetail = (ContactDetail)contactDetailBindingSource.Current;
            frm.ShowDialog();
            if (frm.CurrentDetail != null)
            {
                using (var Dbconnection = new MCDEntities())
                {
                    Dbconnection.ContactDetails.Attach(frm.CurrentDetail);
                    Dbconnection.Entry(frm.CurrentDetail).State = System.Data.Entity.EntityState.Modified;
                    Dbconnection.SaveChanges();
                };

                this.refreshCompanyContactDetails();
            }
        }

        private void dgvCompanyContacts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var IndividualObj = (Individual)(row.DataBoundItem);
                    //var lookupAddressTypeObj = AddressObj.LookupAddressType.AddressType;

                    row.Cells[colContactTitle.Index].Value = IndividualObj.LookupTitle.Title.ToString();


                    //row.Cells[colContactFullName.Index].Value = IndividualObj.IndividualFirstName + " " + IndividualObj.IndividualSecondName + " " + IndividualObj.IndividualLastname.ToString();

                }
            }
        }

        private void dgvStudentContactInfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var ContactDetailObj = (ContactDetail)(row.DataBoundItem);
                    //var lookupAddressTypeObj = AddressObj.LookupAddressType.AddressType;

                    row.Cells[colContactType.Index].Value = ContactDetailObj.LookupContactType.ContactType.ToString();

                }
            }
        }
        private void setContactNameSearchCriteria()
        {
            SerachCriteriaContactName = txtContactsFilterCriteria.Text.ToLower().ToString();
        }

        private void btnFilterContacts_Click(object sender, EventArgs e)
        {
            this.setContactNameSearchCriteria();
            this.refreshCompanyContacts();
            this.refreshCompanyContactDetails();
        }

        private void btnRefreshContactsearch_Click(object sender, EventArgs e)
        {
            this.txtContactsFilterCriteria.Clear();
            this.setContactNameSearchCriteria();
            this.refreshCompanyContacts();
            this.refreshCompanyContactDetails();
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            frmAddCompany frm = new frmAddCompany();
            frm.ShowDialog();

        }

    }
}
