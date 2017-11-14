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
using System.Data.Entity.Validation;
using MetroFramework;
using MetroFramework.Forms;

namespace Impendulo.Deployment.Addresses
{
    public partial class frmAddUpdateAddresses : MetroForm
    {
        private int AddressID { get; set; }
        public Address CurrentAddress { get; set; }

        public Boolean IsSuccessfullyUpdated = false;
        public frmAddUpdateAddresses(int AddressID, Address CurrentAddress = null)
        {
            this.CurrentAddress = CurrentAddress;
            this.AddressID = AddressID;
            InitializeComponent();
        }

        private void frmAddresses_Load(object sender, EventArgs e)
        {

            this.populateAddressType();
            this.populateAddressProvinces();
            this.populateAddressCountries();
            if (AddressID != 0)
            {


                if (CurrentAddress != null)
                {
                    txtAddressDescription.Text = CurrentAddress.AddressDescription;
                    cboStudentAddressAddressType.SelectedValue = CurrentAddress.AddressTypeID;
                    cboStudentAddressCountry.SelectedValue = CurrentAddress.CountryID;
                    cboStudentAddressProvince.SelectedValue = CurrentAddress.ProvinceID;
                    chkStudnetAddressIsDefault.Checked = CurrentAddress.AddressIsDefault;
                    cboStudentAddressCountry.SelectedValue = CurrentAddress.CountryID;
                    cboStudentAddressProvince.SelectedValue = CurrentAddress.ProvinceID;
                    txtStudentAddressLineTwo.Text = CurrentAddress.AddressLineTwo;
                    txtStudentAddressLineOne.Text = CurrentAddress.AddressLineOne;
                    txtStudentAddressTown.Text = CurrentAddress.AddressTown;
                    txtStudentAddressSuburb.Text = CurrentAddress.AddressSuburb;
                    txtStudentAddressAreaCode.Text = CurrentAddress.AddressAreaCode;
                }
                else
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        Address AddressToUpdate = (from a in Dbconnection.Addresses
                                                   where a.AddressID == this.AddressID
                                                   select a).FirstOrDefault<Address>();

                        txtAddressDescription.Text = AddressToUpdate.AddressDescription;
                        cboStudentAddressAddressType.SelectedValue = AddressToUpdate.AddressTypeID;
                        cboStudentAddressCountry.SelectedValue = AddressToUpdate.CountryID;
                        cboStudentAddressProvince.SelectedValue = AddressToUpdate.ProvinceID;
                        chkStudnetAddressIsDefault.Checked = AddressToUpdate.AddressIsDefault;
                        cboStudentAddressCountry.SelectedValue = AddressToUpdate.CountryID;
                        cboStudentAddressProvince.SelectedValue = AddressToUpdate.ProvinceID;
                        txtStudentAddressLineTwo.Text = AddressToUpdate.AddressLineTwo;
                        txtStudentAddressLineOne.Text = AddressToUpdate.AddressLineOne;
                        txtStudentAddressTown.Text = AddressToUpdate.AddressTown;
                        txtStudentAddressSuburb.Text = AddressToUpdate.AddressSuburb;
                        txtStudentAddressAreaCode.Text = AddressToUpdate.AddressAreaCode;

                    };
                }
            };
            this.setAddUpdateButtons();
        }
        private void setAddUpdateButtons()
        {
            if (AddressID == 0)
            {
                this.btnAddAddress.Visible = true;
                this.btnUpdateAddress.Visible = false;
            }
            else
            {
                this.btnAddAddress.Visible = false;
                this.btnUpdateAddress.Visible = true;
            }
        }

        private void populateAddressType()
        {
            using (var Dbconnection = new MCDEntities())
            {
                this.lookupAddressTypeBindingSource.DataSource = (from a in Dbconnection.LookupAddressTypes
                                                                  select a).ToList<LookupAddressType>();
            };
        }
        private void populateAddressProvinces()
        {
            using (var Dbconnection = new MCDEntities())
            {
                this.lookupProvinceBindingSource.DataSource = (from a in Dbconnection.LookupProvinces
                                                               select a).ToList<LookupProvince>();
            };
        }
        private void populateAddressCountries()
        {
            using (var Dbconnection = new MCDEntities())
            {
                this.lookupCountryBindingSource.DataSource = (from a in Dbconnection.LookupCountries
                                                              select a).ToList<LookupCountry>();
            };
        }

        private void populateAddress()
        {
            using (var Dbconnection = new MCDEntities())
            {

            };
        }

        private void btnStudentAddressCancelAddUpdate_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStudentAddressAddUpdate_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        if (CurrentAddress != null)
                        {
                            Dbconnection.Addresses.Attach(CurrentAddress);

                            CurrentAddress.AddressDescription = this.txtAddressDescription.Text.ToString();
                            CurrentAddress.AddressTypeID = Convert.ToInt32(cboStudentAddressAddressType.SelectedValue);
                            CurrentAddress.AddressIsDefault = chkStudnetAddressIsDefault.Checked;
                            CurrentAddress.CountryID = Convert.ToInt32(cboStudentAddressCountry.SelectedValue);
                            CurrentAddress.ProvinceID = Convert.ToInt32(cboStudentAddressProvince.SelectedValue);
                            CurrentAddress.AddressLineTwo = txtStudentAddressLineTwo.Text.ToString();
                            CurrentAddress.AddressLineOne = txtStudentAddressLineOne.Text.ToString();
                            CurrentAddress.AddressTown = txtStudentAddressTown.Text.ToString();
                            CurrentAddress.AddressSuburb = txtStudentAddressSuburb.Text.ToString();
                            CurrentAddress.AddressAreaCode = txtStudentAddressAreaCode.Text.ToString();

                            Dbconnection.Entry(CurrentAddress).Reference(a => a.LookupAddressType).Load();
                            Dbconnection.Entry(CurrentAddress).Reference(a => a.LookupProvince).Load();
                            Dbconnection.Entry(CurrentAddress).Reference(a => a.LookupCountry).Load();

                            Dbconnection.Entry(CurrentAddress).State = System.Data.Entity.EntityState.Modified;


                        };
                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();

                        //commit transaction
                        dbTran.Commit();
                        IsSuccessfullyUpdated = true;
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
                                    MetroMessageBox.Show(this, error.ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //MessageBox.Show(error.ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        //Rollback transaction if exception occurs
                        dbTran.Rollback();
                    }
                }
            };


        }

        private void btnAddAddress_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        CurrentAddress = new Address
                        {
                            AddressDescription = this.txtAddressDescription.Text.ToString(),
                            AddressTypeID = Convert.ToInt32(cboStudentAddressAddressType.SelectedValue),
                            AddressIsDefault = chkStudnetAddressIsDefault.Checked,
                            CountryID = Convert.ToInt32(cboStudentAddressCountry.SelectedValue),
                            ProvinceID = Convert.ToInt32(cboStudentAddressProvince.SelectedValue),
                            AddressLineTwo = txtStudentAddressLineTwo.Text.ToString(),
                            AddressLineOne = txtStudentAddressLineOne.Text.ToString(),
                            AddressTown = txtStudentAddressTown.Text.ToString(),
                            AddressSuburb = txtStudentAddressSuburb.Text.ToString(),
                            AddressAreaCode = txtStudentAddressAreaCode.Text.ToString(),
                            AddressModifiedDate = DateTime.Now
                        };
                        Dbconnection.Addresses.Add(CurrentAddress);
                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();

                        //commit transaction
                        dbTran.Commit();
                        IsSuccessfullyUpdated = true;
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
                                    MetroMessageBox.Show(this, error.ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //MessageBox.Show(error.ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        //Rollback transaction if exception occurs
                        dbTran.Rollback();
                    }
                }
            };


        }
    }
}
