﻿using System;
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
using System.Data.Entity;
using Impendulo.Deployment.ContactDetails;
using Impendulo.Deployment.Contacts;

namespace Impendulo.Deployment.Enquiry
{
    public partial class frmSelectIndividualContact : MetroForm
    {
        public frmSelectIndividualContact()
        {
            InitializeComponent();
        }

        private string _FirstName = "";
        private string _LastName = "";
        private string _EmailAddress = "";
        private string _CellNumbers = "";

        private Individual _CurrentSelectedIndividual;
        public Individual SelectedIndividual
        {
            get
            {
                if (_CurrentSelectedIndividual == null)
                {
                    _CurrentSelectedIndividual = new Individual();
                }
                return _CurrentSelectedIndividual;
            }
            set { _CurrentSelectedIndividual = value; }
        }

        private void frmSelectIndividualContact_Load(object sender, EventArgs e)
        {
            if (SelectedIndividual.IndividualID != 0)
            {
                txtFirstName.Text = SelectedIndividual.IndividualFirstName;
                txtLastName.Text = SelectedIndividual.IndividualLastname;
                setSearchControls();
            }
            else
            {

            }
            LoadSuggestions();
            this.refreshContacts();
            // this.refreshContactDetails();
        }

        private void LoadSuggestions()
        {

            AutoCompleteStringCollection StudentFirstNames = new AutoCompleteStringCollection();
            AutoCompleteStringCollection StudentLastNames = new AutoCompleteStringCollection();
            AutoCompleteStringCollection EmailAddresses = new AutoCompleteStringCollection();
            AutoCompleteStringCollection CellNumbers = new AutoCompleteStringCollection();

            List<Individual> x = new List<Individual>();
            using (var Dbconnection = new MCDEntities())
            {
                x = (from a in Dbconnection.Individuals
                     select a)
                     .Except(from a in Dbconnection.Facilitators
                             select a.Individual)
                                .Except(from a in Dbconnection.Employees
                                        select a.Individual)
                                        .Except(from a in Dbconnection.Assessors
                                                select a.Individual)
                                                    .Except(from a in Dbconnection.Students
                                                            select a.Individual)
                                              .Include(a => a.ContactDetails)
                                                    .Include(a => a.ContactDetails.Select(b => b.LookupContactType))
                     .ToList<Individual>();


            };
            foreach (Individual stud in x)
            {
                StudentFirstNames.Add(stud.IndividualFirstName);
            }

            txtFirstName.AutoCompleteCustomSource = StudentFirstNames;
            txtFirstName.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtFirstName.AutoCompleteSource = AutoCompleteSource.CustomSource;

            foreach (Individual stud in x)
            {
                StudentLastNames.Add(stud.IndividualLastname);
            }

            txtLastName.AutoCompleteCustomSource = StudentLastNames;
            txtLastName.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtLastName.AutoCompleteSource = AutoCompleteSource.CustomSource;

            foreach (Individual stud in x)
            {
                foreach (ContactDetail CD in stud.ContactDetails.Where(a => a.ContactTypeID == (int)Common.Enum.EnumContactTypes.Email_Address))
                {
                    EmailAddresses.Add(CD.ContactDetailValue.ToString());
                }

            }

            txtEmailAddresas.AutoCompleteCustomSource = EmailAddresses;
            txtEmailAddresas.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtEmailAddresas.AutoCompleteSource = AutoCompleteSource.CustomSource;

            foreach (Individual stud in x)
            {
                foreach (ContactDetail CD in stud.ContactDetails.Where(a => a.ContactTypeID == (int)Common.Enum.EnumContactTypes.Cell_Number))
                {
                    CellNumbers.Add(CD.ContactDetailValue.ToString());
                }

            }

            txtxCellNumbers.AutoCompleteCustomSource = CellNumbers;
            txtxCellNumbers.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtxCellNumbers.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void setSearchControls()
        {
            _FirstName = this.txtFirstName.Text.ToString();
            _LastName = this.txtLastName.Text.ToString();
            _EmailAddress = this.txtEmailAddresas.Text.ToString();
            _CellNumbers = this.txtxCellNumbers.Text.ToString();
            this.refreshContacts();
        }
        private void refreshContacts()
        {
            this.populateContacts();
        }

        private void refreshContactDetails()
        {
            if (individualBindingSource.Count > 0)
            {
                populateContactDetails();
            }
            else
            {
                contactDetailBindingSource.DataSource = null;
            }
        }

        private void populateContactDetails()
        {
            Individual CurrentIndividual = (Individual)individualBindingSource.Current;
            contactDetailBindingSource.DataSource = (CurrentIndividual.ContactDetails).ToList<ContactDetail>();
            if (contactDetailBindingSource.Count > 0)
            {
                this.btnSelectContact.Enabled = true;
            }
            else
            {
                this.btnSelectContact.Enabled = false;
            }
        }

        private void populateContacts()
        {
            using (var Dbconnection = new MCDEntities())
            {
                //List<Individual> AllIndividuals = (from a in Dbconnection.Individuals
                //                                      .Include("ContactDetails")
                //                                      .Include("ContactDetails.LookupContactType")
                //                                   where a.IndividualFirstName.Contains(_FirstName) && a.IndividualLastname.Contains(_LastName)
                //                                   select a)
                //                                       .Except(from a in Dbconnection.Facilitators
                //                                               select a.Individual)
                //                                               .Except(from a in Dbconnection.Employees
                //                                                       select a.Individual)
                //                                                       .Except(from a in Dbconnection.Assessors
                //                                                               select a.Individual)
                //                                                                       //.Except(from a in Dbconnection.Students
                //                                                                       //        select a.Individual)
                //                                                                       .Except(from a in Dbconnection.Individuals
                //                                                                               where a.Companies.Count > 0
                //                                                                               select a)
                //                                               .ToList<Individual>();

                List<Individual> AllIndividuals = (from a in Dbconnection.Individuals
                                                   from b in a.ContactDetails
                                                   where
                                                        a.IndividualFirstName.Contains(_FirstName)
                                                        && a.IndividualLastname.Contains(_LastName)
                                                        && b.ContactDetailValue.Contains(_EmailAddress)
                                                        && b.ContactDetailValue.Contains(_CellNumbers)
                                                   orderby a.IndividualFirstName
                                                   select a)
                                                    .Except(from a in Dbconnection.Facilitators
                                                            select a.Individual)
                                                        .Except(from a in Dbconnection.Employees
                                                                select a.Individual)
                                                                .Except(from a in Dbconnection.Assessors
                                                                        select a.Individual)
                                                                         .Except(from a in Dbconnection.Students
                                                                                 select a.Individual)
                                                    .Include(a => a.ContactDetails)
                                                    .Include(a => a.ContactDetails.Select(b => b.LookupContactType))
                                                    .Take<Individual>(50)
                                                    .ToList<Individual>();

                individualBindingSource.DataSource = AllIndividuals;
                if (individualBindingSource.Count > 0)
                {
                    btnAddContact.Visible = false;
                }
                else
                {
                    btnAddContact.Visible = true;
                }
            };
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.setSearchControls();
            this.refreshContactDetails();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmailAddresas.Clear();
            txtxCellNumbers.Clear();
            this.setSearchControls();
            this.refreshContactDetails();
        }

        private void individualBindingSource_PositionChanged(object sender, EventArgs e)
        {
            this.refreshContactDetails();
        }

        private void dgvContactInfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var ContactDetailObj = (ContactDetail)(row.DataBoundItem);
                    row.Cells[colContactType.Index].Value = ContactDetailObj.LookupContactType.ContactType;
                }
            }
        }

        private void btnAddContactInfo_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0))
            {
                Individual CurrentContact = (Individual)individualBindingSource.Current;

                frm.ShowDialog();
                if (frm.CurrentDetail != null)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.Individuals.Attach(CurrentContact);

                        Dbconnection.ContactDetails.Attach(frm.CurrentDetail);

                        CurrentContact.ContactDetails.Add(frm.CurrentDetail);

                        Dbconnection.SaveChanges();

                        Dbconnection.Entry(frm.CurrentDetail).Reference("LookupContactType").Load();
                    };
                    this.refreshContactDetails();
                }
            }
        }

        private void btnSelectContact_Click(object sender, EventArgs e)
        {
            if (individualBindingSource.Count > 0)
            {
                SelectedIndividual = (Individual)individualBindingSource.Current;
            }
            this.Close();
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            using (frmContactsAddUpdateContacts frm = new frmContactsAddUpdateContacts(0))
            {
                frm.IsStudent = true;
                frm.ShowDialog();
                refreshContacts();
            }
        }

        private void btnUpdateContactDetials_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0))
            {
                ContactDetail CurrentDetail = (ContactDetail)contactDetailBindingSource.Current;
                frm.CurrentDetail = CurrentDetail;
                frm.ContactDetailID = CurrentDetail.ContactDetailID;

                Individual CurrentContact = (Individual)individualBindingSource.Current;

                frm.ShowDialog();
                if (frm.CurrentDetail != null)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.ContactDetails.Attach(CurrentDetail);

                        Dbconnection.Entry(CurrentDetail).State = EntityState.Modified;

                        Dbconnection.SaveChanges();

                        Dbconnection.Entry(frm.CurrentDetail).Reference("LookupContactType").Load();
                    };
                    this.refreshContactDetails();
                }
            }
        }

        private void dgvContactsSearchResults_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    Individual IndividualObj = (Individual)(row.DataBoundItem);
                    row.Cells[colContactTitle.Index].Value = IndividualObj.LookupTitle.Title.ToString();
                }
            }
        }
    }
}
