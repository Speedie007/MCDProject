﻿using Impendulo.Common.Enum;
using Impendulo.Data.Models;
using MetroFramework.Forms;
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

namespace Impendulo.Development.Company

{
    public partial class frmAddCompany : MetroForm
    {
        public frmAddCompany()
        {
            InitializeComponent();
        }
        private Impendulo.Data.Models.Company _CurrentCompany;
        public Impendulo.Data.Models.Company CurrentCompany
        {
            get
            {
                if (_CurrentCompany == null)
                {
                    _CurrentCompany = new Data.Models.Company();
                    return _CurrentCompany;
                }
                return _CurrentCompany;
            }
            set { _CurrentCompany = value; }
        }

        private void frmCompanySearchV2_Load(object sender, EventArgs e)
        {

        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        CurrentCompany.CompanyName = this.txtComapnyName.Text.ToString();
                        CurrentCompany.CompanySETANumber = this.txtSARSLevyRegistration.Text.ToString();
                        CurrentCompany.CompanySicCode = this.txtSicCode.Text.ToString();
                        CurrentCompany.CompanySARSLevyRegistrationNumber = this.txtSARSLevyRegistration.Text.ToString();

                        //foreach (ContactDetail ConDetail in GetCompanyContactDetails())
                        //{
                        //    CurrentCompany.ContactDetails.Add(ConDetail);
                        //};
                        Dbconnection.Companies.Add(CurrentCompany);
                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();
                       
                        //commit transaction
                        dbTran.Commit();

                        Impendulo.Common.Verifiction.OfProgressFiles.VerifyCompanyProgressFile(CurrentCompany.CompanyID);
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
        private List<ContactDetail> GetCompanyContactDetails()
        {
            List<ContactDetail> AllCompanyContactDetails = new List<ContactDetail>();
            //Adds the Comapany Email adddress to the ContactDetails
            AllCompanyContactDetails.Add(new ContactDetail
            {
                ContactTypeID = (int)EnumContactTypes.Email_Address,
                ContactDetailValue = txtEmailAddress.Text.ToString()
            });
            //Adds the Comapany Office Number to the ContactDetails
            AllCompanyContactDetails.Add(new ContactDetail
            {
                ContactTypeID = (int)EnumContactTypes.Office_Number,
                ContactDetailValue = txtOfficeNumber.Text.ToString()
            });
            //Adds the Comapany Fax Number to the ContactDetails
            AllCompanyContactDetails.Add(new ContactDetail
            {
                ContactTypeID = (int)EnumContactTypes.Fax_Number,
                ContactDetailValue = txtFaxNumber.Text.ToString()
            });
            return AllCompanyContactDetails;
        }
    }
}
