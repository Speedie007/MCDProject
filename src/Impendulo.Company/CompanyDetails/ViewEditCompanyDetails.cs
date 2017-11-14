using Impendulo.Common;
using Impendulo.Common.Enum;
using Impendulo.Data.Models;
using Impendulo.Data.Models.DataGridViewStructures.Company.Addresses;
using Impendulo.Data.Models.DataGridViewStructures.Company.ContactDetails;
using Impendulo.Data.Models.DataGridViewStructures.Company.Contacts;
using Impendulo.Development.Addresses;
using Impendulo.Development.ContactDetails;
using Impendulo.Development.Contacts;
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

namespace Impendulo.Development.Company.CompanyDetails
{
    public partial class ViewEditCompanyDetails : MetroFramework.Forms.MetroForm
    {

        #region Global Variables

        public Data.Models.Company CurrentlySelectedCompany { get; private set; }
        private int CompanyID { get; set; }
        #region User and Role
        public Employee CurrentEmployeeLoggedIn { get; set; }
        #endregion
        #endregion
        public ViewEditCompanyDetails(int CompanyID)
        {
            this.CompanyID = CompanyID;
            InitializeComponent();
        }

        private void ViewEditCompanyDetails_Load(object sender, EventArgs e)
        {
            refreshCompanyDetails();
        }

        #region Refresh Methods
        private void refreshCompanyDetails()
        {
            populateCompanyDetails();
        }
        private void refreshCompanyContacts()
        {
            this.populateCompanyContacts();
        }
        private void refreshCompanyContactsDetails()
        {

            using (var Dbconnection = new MCDEntities())
            {
                CompanyContacts SelectedConact = (CompanyContacts)companyContactsBindingSource.Current;
                companyContactsDetailsBindingSource.DataSource = new CustomSortableBindingList<CompanyContactDetails>(
                     (from a in CurrentlySelectedCompany.Individuals
                      from b in a.ContactDetails
                      where a.IndividualID == SelectedConact.IndividualID
                      select new CompanyContactDetails()
                      {
                          ContactDetailID = b.ContactDetailID,
                          ContactDetailValue = b.ContactDetailValue,
                          ContactType = b.LookupContactType.ContactType,
                          ContactTypeID = b.ContactTypeID

                      }
                    ).ToList<CompanyContactDetails>());
            };
        }
        private void refreshCompanyContactDetails()
        {
            this.populateCompanyContactDetails();
        }
        private void refreshCompanyAddresses()
        {


            companyAddressesBindingSource.DataSource = new CustomSortableBindingList<CompanyAddresses>((from a in CurrentlySelectedCompany.Addresses
                                                                                                        select new CompanyAddresses()
                                                                                                        {
                                                                                                            AddressID = a.AddressID,
                                                                                                            AddressType = a.LookupAddressType.AddressType,
                                                                                                            AreaCode = a.AddressAreaCode,
                                                                                                            Country = a.LookupCountry.CountryName,
                                                                                                            Description = a.AddressDescription,
                                                                                                            IsDefault = a.AddressIsDefault,
                                                                                                            LineOne = a.AddressLineOne,
                                                                                                            LineTwo = a.AddressLineTwo,
                                                                                                            ModifiedDate = a.AddressModifiedDate,
                                                                                                            Province = a.LookupProvince.Province,
                                                                                                            Suburb = a.AddressSuburb,
                                                                                                            Town = a.AddressTown
                                                                                                        }).ToList<CompanyAddresses>());





        }
        #endregion

        #region Populate Methods
        private void populateCompanyContacts()
        {
            using (var Dbconnection = new MCDEntities())
            {
                companyContactsBindingSource.DataSource = new CustomSortableBindingList<CompanyContacts>(
                    (from a in CurrentlySelectedCompany.Individuals
                     select new CompanyContacts()
                     {
                         IndividualID = a.IndividualID,
                         FulName = a.FullName,
                         Title = a.LookupTitle.Title
                     }).ToList<CompanyContacts>()
                    );
            };
        }
        private void populateCompanyContactDetails()
        {
            if (CurrentlySelectedCompany.ContactDetails.Count > 0)
            {
                //CompanyContactDetails CD = (CompanyContactDetails)companyContactDetailsBindingSource.Current;
                //ContactDetail CDFromCache = CurrentlySelectedCompany.ContactDetails.Where(a => a.ContactDetailID == CD.ContactDetailID).First();
                companyContactDetailsBindingSource.DataSource = new CustomSortableBindingList<CompanyContactDetails>((from a in CurrentlySelectedCompany.ContactDetails
                                                                                                                      select new CompanyContactDetails()
                                                                                                                      {
                                                                                                                          ContactDetailID = a.ContactDetailID,
                                                                                                                          ContactDetailValue = a.ContactDetailValue,
                                                                                                                          ContactType = a.LookupContactType.ContactType,
                                                                                                                          ContactTypeID = a.ContactTypeID
                                                                                                                      }));
            }
            else
            {
                companyContactDetailsBindingSource.DataSource = null;

            }

        }
        private void populateCompanyDetails()
        {

            using (var Dbconnection = new MCDEntities())
            {
                CurrentlySelectedCompany = (from a in Dbconnection.Companies
                                            where a.CompanyID == this.CompanyID
                                            select a)

                                            .Include(a => a.ContactDetails)
                                            .Include(a => a.ContactDetails.Select(b => b.LookupContactType))
                                            .Include(a => a.Addresses)
                                            .Include(a => a.Addresses.Select(b => b.LookupAddressType))
                                            .Include(a => a.Addresses.Select(b => b.LookupCountry))
                                            .Include(a => a.Addresses.Select(b => b.LookupProvince))
                                            .Include(a => a.Individuals)
                                            .Include(a => a.Individuals.Select(b => b.ContactDetails))
                                             .Include(a => a.Individuals.Select(b => b.LookupTitle))
                                            .Include(a => a.Individuals.Select(b => b.ContactDetails.Select(c => c.LookupContactType)))
                                            .FirstOrDefault<Data.Models.Company>();

            };
            if (CurrentlySelectedCompany != null)
            {
                this.txtCompanyName.Text = CurrentlySelectedCompany.CompanyName;
                this.txtSiCode.Text = CurrentlySelectedCompany.CompanySicCode;
                this.txtSETANumber.Text = CurrentlySelectedCompany.CompanySETANumber;
                this.txtSARSLevyReg.Text = CurrentlySelectedCompany.CompanySARSLevyRegistrationNumber;


                this.refreshCompanyContactDetails();
                this.refreshCompanyContacts();
                this.refreshCompanyContactsDetails();
                this.refreshCompanyAddresses();
            }

        }
        #endregion

        private void btnPicSearchForCompany_Click(object sender, EventArgs e)
        {
            using (frmCompanySearch frm = new frmCompanySearch())
            {
                frm.ShowDialog();
                if (frm.CurrentCompany != null)
                {
                    this.CompanyID = frm.CurrentCompany.CompanyID;
                    this.refreshCompanyDetails();
                    //this.refreshCompanyContacts();
                    //refreshCompanyContactsDetails();
                    refreshCompanyContactDetails();
                }

            }
        }

        private void btnAddCompanyAddress_Click(object sender, EventArgs e)
        {
            frmAddUpdateAddresses frm = new frmAddUpdateAddresses(0);

            frm.ShowDialog();
            if (frm.CurrentAddress != null)
            {
                if (frm.CurrentAddress.AddressID != 0)
                {
                    if (frm.CurrentAddress.AddressIsDefault)
                    {
                        resetAdressDefaults();
                    }
                    else if (CurrentlySelectedCompany.Addresses.Count == 0)
                    {
                        frm.CurrentAddress.AddressIsDefault = true;
                    }
                    using (var Dbconnection = new MCDEntities())
                    {
                        using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                        {
                            try
                            {
                                //CRUD Operations
                                CompanyAddresses CurrentCompanyAddress = ((CompanyAddresses)companyAddressesBindingSource.Current);

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
                                Data.Models.Company C = new Data.Models.Company { CompanyID = CurrentlySelectedCompany.CompanyID };
                                // 2
                                Dbconnection.Companies.Add(C);
                                // 3
                                Dbconnection.Companies.Attach(C);

                                // 1
                                Address A = new Address { AddressID = frm.CurrentAddress.AddressID };
                                // 2
                                Dbconnection.Addresses.Add(A);
                                // 3
                                Dbconnection.Addresses.Attach(A);

                                // like previous method add instance to navigation property
                                C.Addresses.Add(A);




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

                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.Addresses.Attach(frm.CurrentAddress);
                        Dbconnection.Entry(frm.CurrentAddress).Reference(a => a.LookupAddressType).Load();
                        Dbconnection.Entry(frm.CurrentAddress).Reference(a => a.LookupProvince).Load();
                        Dbconnection.Entry(frm.CurrentAddress).Reference(a => a.LookupCountry).Load();
                        CurrentlySelectedCompany.Addresses.Add(frm.CurrentAddress);
                    };
                    refreshCompanyAddresses();
                }

            }
        }
        private void resetAdressDefaults(int AddressID = 0)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        CompanyAddresses CurrentCompanyAddress = ((CompanyAddresses)companyAddressesBindingSource.Current);
                        Address CurrentAddress = (Address)CurrentlySelectedCompany.Addresses.Where(a => a.AddressID == CurrentCompanyAddress.AddressID).FirstOrDefault();
                        // Dbconnection.Companies.Attach(CurrentlySelectedCompany);
                        foreach (Address Add in CurrentlySelectedCompany.Addresses)
                        {
                            if (AddressID != Add.AddressID)
                            {
                                Add.AddressIsDefault = false;
                                var AddressToupdate = Dbconnection.Addresses.FirstOrDefault(s => s.AddressID == Add.AddressID);
                                AddressToupdate.AddressIsDefault = false;

                            }

                        }


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

        }
        private void btnRemoveStudentAddress_Click(object sender, EventArgs e)
        {
            Boolean bWasDefault = false;

            if (companyAddressesBindingSource.Count > 0)
            {
                CompanyAddresses CurrentCompanyAddress = ((CompanyAddresses)companyAddressesBindingSource.Current);
                Address CurrentAddress = (Address)CurrentlySelectedCompany.Addresses.Where(a => a.AddressID == CurrentCompanyAddress.AddressID).FirstOrDefault();

                if (CurrentAddress.AddressIsDefault)
                {
                    bWasDefault = true;
                }
                using (frmAddUpdateAddresses frm = new frmAddUpdateAddresses(CurrentCompanyAddress.AddressID, CurrentAddress))
                {

                    using (var Dbconnection = new MCDEntities())
                    {
                        using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                        {
                            try
                            {
                                //CRUD Operations
                                // return one instance each entity by primary key
                                var company = Dbconnection.Companies.FirstOrDefault(p => p.CompanyID == CurrentlySelectedCompany.CompanyID);
                                var AddressToRemove = Dbconnection.Addresses.FirstOrDefault(s => s.AddressID == CurrentCompanyAddress.AddressID);

                                // call Remove method from navigation property for any instance
                                // supplier.Product.Remove(product);
                                // also works
                                company.Addresses.Remove(AddressToRemove);

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
                    CurrentlySelectedCompany.Addresses.Remove(CurrentlySelectedCompany.Addresses.Where(a => a.AddressID == CurrentAddress.AddressID).First());
                    if (CurrentlySelectedCompany.Addresses.Count > 0)
                    {
                        if (bWasDefault)
                        {
                            var CurrentlySelectedToUpdate = CurrentlySelectedCompany.Addresses.First();
                            using (var Dbconnection = new MCDEntities())
                            {

                                var x = Dbconnection.Addresses.Where(a => a.AddressID == CurrentlySelectedToUpdate.AddressID).First();
                                x.AddressIsDefault = true;
                                Dbconnection.Entry(x).State = EntityState.Modified;
                                Dbconnection.SaveChanges();
                            };
                            CurrentlySelectedToUpdate.AddressIsDefault = true;
                        }

                    }

                    refreshCompanyAddresses();

                }
            }



        }

        private void btnUpdateAddress_Click(object sender, EventArgs e)
        {
            if (companyAddressesBindingSource.Count > 0)
            {


                CompanyAddresses CurrentCompanyAddress = ((CompanyAddresses)companyAddressesBindingSource.Current);
                Address CurrentAddress = (Address)CurrentlySelectedCompany.Addresses.Where(a => a.AddressID == CurrentCompanyAddress.AddressID).FirstOrDefault();
                using (frmAddUpdateAddresses frm = new frmAddUpdateAddresses(CurrentCompanyAddress.AddressID, CurrentAddress))
                {
                    frm.ShowDialog();
                    if (frm.CurrentAddress.AddressIsDefault)
                    {
                        resetAdressDefaults(CurrentAddress.AddressID);
                    }
                    else if (CurrentlySelectedCompany.Addresses.Count == 1)
                    {

                        using (var Dbconnection = new MCDEntities())
                        {
                            CurrentAddress.AddressIsDefault = true;
                            Address Add = Dbconnection.Addresses.Where(a => a.AddressID == CurrentAddress.AddressID).FirstOrDefault();
                            Add.AddressIsDefault = true;
                            //Dbconnection.Entry(Add).State = EntityState.Modified;
                            Dbconnection.SaveChanges();
                        };

                    }
                    refreshCompanyAddresses();

                }
            }
        }

        private void btnFilterContacts_Click(object sender, EventArgs e)
        {

        }

        private void btnRefreshContactsearch_Click(object sender, EventArgs e)
        {

        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            //CompanyContacts CC = (CompanyContacts)companyContactsBindingSource.Current;
            //Individual CCFromCache = CurrentlySelectedCompany.Individuals.Where(a => a.IndividualID == CC.IndividualID).First();

            using (frmContactsAddUpdateContacts frm = new frmContactsAddUpdateContacts(0))
            {
                frm.ShowDialog();
                if (frm.CurrentContact != null)
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
                                Data.Models.Company C = new Data.Models.Company { CompanyID = CurrentlySelectedCompany.CompanyID };
                                // 2
                                Dbconnection.Companies.Add(C);
                                // 3
                                Dbconnection.Companies.Attach(C);

                                // 1
                                Individual I = new Individual { IndividualID = frm.CurrentContact.IndividualID };
                                // 2
                                Dbconnection.Individuals.Add(I);
                                // 3
                                Dbconnection.Individuals.Attach(I);

                                // like previous method add instance to navigation property
                                C.Individuals.Add(I);

                                ////saves all above operations within one transaction
                                Dbconnection.SaveChanges();

                                //commit transaction
                                dbTran.Commit();
                                // Dbconnection.Entry(I).Collection(a => a.ContactDetails).Load();

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

                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.Individuals.Attach(frm.CurrentContact);
                        Dbconnection.Entry(frm.CurrentContact).Reference(a => a.LookupTitle).Load();
                        CurrentlySelectedCompany.Individuals.Add(frm.CurrentContact);
                        this.refreshCompanyContacts();
                    };



                }
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (companyContactsBindingSource.Count > 0)
            {
                using (var Dbconnection = new MCDEntities())
                {
                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            //CRUD Operations
                            CompanyContacts CC = (CompanyContacts)companyContactsBindingSource.Current;
                            Individual CCFromCache = CurrentlySelectedCompany.Individuals.Where(a => a.IndividualID == CC.IndividualID).First();

                            // return one instance each entity by primary key
                            var C = Dbconnection.Companies.FirstOrDefault(a => a.CompanyID == CurrentlySelectedCompany.CompanyID);
                            var I = Dbconnection.Individuals.FirstOrDefault(a => a.IndividualID == CC.IndividualID);

                            // call Remove method from navigation property for any instance
                            C.Individuals.Remove(I);

                            ////saves all above operations within one transaction
                            Dbconnection.SaveChanges();

                            //commit transaction
                            dbTran.Commit();
                            CurrentlySelectedCompany.Individuals.Remove(CCFromCache);
                            this.refreshCompanyContacts();
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


        }

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            if (companyContactsBindingSource.Count > 0)
            {
                CompanyContacts CC = (CompanyContacts)companyContactsBindingSource.Current;
                Individual CCFromCache = CurrentlySelectedCompany.Individuals.Where(a => a.IndividualID == CC.IndividualID).First();

                using (frmContactsAddUpdateContacts frm = new frmContactsAddUpdateContacts(CC.IndividualID, CCFromCache))
                {
                    frm.ShowDialog();
                    this.refreshCompanyContacts();
                }
            }
        }

        private void btnAddContactInfo_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0))
            {
                frm.ShowDialog();
                if (frm.CurrentDetail != null)
                {
                    CompanyContacts CC = (CompanyContacts)companyContactsBindingSource.Current;
                    Individual CCFromCache = CurrentlySelectedCompany.Individuals.Where(a => a.IndividualID == CC.IndividualID).First();

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
                                Individual I = new Individual { IndividualID = CC.IndividualID };
                                // 2
                                Dbconnection.Individuals.Add(I);
                                // 3
                                Dbconnection.Individuals.Attach(I);

                                // 1
                                ContactDetail cd = new ContactDetail { ContactDetailID = frm.CurrentDetail.ContactDetailID };
                                // 2
                                Dbconnection.ContactDetails.Add(cd);
                                // 3
                                Dbconnection.ContactDetails.Attach(cd);

                                // like previous method add instance to navigation property
                                I.ContactDetails.Add(cd);
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

                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.ContactDetails.Attach(frm.CurrentDetail);
                        Dbconnection.Entry(frm.CurrentDetail).Reference(a => a.LookupContactType).Load();
                    };
                    CCFromCache.ContactDetails.Add(frm.CurrentDetail);
                    refreshCompanyContactsDetails();
                }
            }
        }

        private void btnRemoveStudentClientInfo_Click(object sender, EventArgs e)
        {
            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        CompanyContacts CC = (CompanyContacts)companyContactsBindingSource.Current;
                        Individual CCFromCache = CurrentlySelectedCompany.Individuals.Where(a => a.IndividualID == CC.IndividualID).First();

                        CompanyContactDetails CD = (CompanyContactDetails)companyContactsDetailsBindingSource.Current;
                        ContactDetail CDFromCache = CCFromCache.ContactDetails.Where(a => a.ContactDetailID == CD.ContactDetailID).First();
                        Dbconnection.ContactDetails.Attach(CDFromCache);
                        Dbconnection.ContactDetails.Remove(CDFromCache);
                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();

                        //commit transaction
                        dbTran.Commit();

                        CCFromCache.ContactDetails.Remove(CDFromCache);
                        this.refreshCompanyContactsDetails();
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

        private void btnUpdateContactDetials_Click(object sender, EventArgs e)
        {
            if (this.companyContactsBindingSource.Count > 0)
            {
                CompanyContacts CC = (CompanyContacts)companyContactsBindingSource.Current;
                Individual CCFromCache = CurrentlySelectedCompany.Individuals.Where(a => a.IndividualID == CC.IndividualID).First();

                CompanyContactDetails CD = (CompanyContactDetails)companyContactsDetailsBindingSource.Current;
                ContactDetail CDFromCache = CCFromCache.ContactDetails.Where(a => a.ContactDetailID == CD.ContactDetailID).First();
                using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(CDFromCache.ContactDetailID))
                {
                    frm.CurrentDetail = CDFromCache;
                    frm.ShowDialog();

                    if (frm.CurrentDetail != null)
                    {
                        CDFromCache.ContactDetailValue = frm.CurrentDetail.ContactDetailValue;
                    }
                    refreshCompanyContactsDetails();
                }
            }
        }

        private void btnUpdateCompanyInfo_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        Data.Models.Company UpdateCompany = (from a in Dbconnection.Companies

                                                             where a.CompanyID == CurrentlySelectedCompany.CompanyID
                                                             select a)

                                                     .FirstOrDefault<Data.Models.Company>();

                        UpdateCompany.CompanyName = txtCompanyName.Text;
                        UpdateCompany.CompanySETANumber = txtSETANumber.Text;
                        UpdateCompany.CompanySicCode = txtSiCode.Text;
                        UpdateCompany.CompanySARSLevyRegistrationNumber = txtSARSLevyReg.Text;
                        Dbconnection.Entry(UpdateCompany).State = EntityState.Modified;



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
        }

        private void btnAddCompmayContactDetail_Click(object sender, EventArgs e)
        {

            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0))
            {
                frm.ShowDialog();
                if (frm.CurrentDetail != null)
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
                                Data.Models.Company c = new Data.Models.Company { CompanyID = CurrentlySelectedCompany.CompanyID };
                                // 2
                                Dbconnection.Companies.Add(c);
                                // 3
                                Dbconnection.Companies.Attach(c);

                                // 1
                                ContactDetail cd = new ContactDetail { ContactDetailID = frm.CurrentDetail.ContactDetailID };
                                // 2
                                Dbconnection.ContactDetails.Add(cd);
                                // 3
                                Dbconnection.ContactDetails.Attach(cd);

                                // like previous method add instance to navigation property
                                c.ContactDetails.Add(cd);
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

                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.ContactDetails.Attach(frm.CurrentDetail);
                        Dbconnection.Entry(frm.CurrentDetail).Reference(a => a.LookupContactType).Load();
                    };
                    CurrentlySelectedCompany.ContactDetails.Add(frm.CurrentDetail);
                    refreshCompanyContactDetails();
                }
            }

        }

        private void btnRemoveCompmayContactDetail_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        CompanyContactDetails CD = (CompanyContactDetails)companyContactDetailsBindingSource.Current;
                        ContactDetail CDFromCache = CurrentlySelectedCompany.ContactDetails.Where(a => a.ContactDetailID == CD.ContactDetailID).First();
                        Dbconnection.ContactDetails.Attach(CDFromCache);
                        Dbconnection.ContactDetails.Remove(CDFromCache);
                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();

                        //commit transaction
                        dbTran.Commit();

                        this.CurrentlySelectedCompany.ContactDetails.Remove(CDFromCache);
                        this.refreshCompanyContactDetails();
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

        private void btnUpdateCompmayContactDetail_Click(object sender, EventArgs e)
        {
            if (this.companyContactDetailsBindingSource.Count > 0)
            {
                CompanyContactDetails CD = (CompanyContactDetails)companyContactDetailsBindingSource.Current;
                ContactDetail CDFromCache = CurrentlySelectedCompany.ContactDetails.Where(a => a.ContactDetailID == CD.ContactDetailID).First();

                using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(CD.ContactDetailID))
                {
                    frm.CurrentDetail = CDFromCache;
                    frm.ShowDialog();
                    if (frm.CurrentDetail != null)
                    {
                        CDFromCache.ContactDetailValue = frm.CurrentDetail.ContactDetailValue;
                    }
                    refreshCompanyContactDetails();
                }
            }
        }

        private void companyContactsBindingSource_PositionChanged(object sender, EventArgs e)
        {
            //CompanyContactDetails CD = (CompanyContactDetails)companyContactDetailsBindingSource.Current;
            //ContactDetail CDFromCache = CurrentlySelectedCompany.ContactDetails.Where(a => a.ContactDetailID == CD.ContactDetailID).First();
            //refreshCompanyContactDetails();
            this.refreshCompanyContactsDetails();
        }

        private void companyContactsDetailsBindingSource_PositionChanged(object sender, EventArgs e)
        {

        }

        private void metroGrid4_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
