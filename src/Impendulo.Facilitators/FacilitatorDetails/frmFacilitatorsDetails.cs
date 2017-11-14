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
using Impendulo.Development.Addresses;
using Impendulo.Development.ContactDetails;

namespace Impendulo.Development.Facilitator
{
    public partial class frmAddUpdateFacilitatorDetails : Form
    {
        public Data.Models.Facilitator CurrentFacilitator { get; set; }
        public int FacilitatorID { get; set; }
        public frmAddUpdateFacilitatorDetails()
        {
            FacilitatorID = 0;
            InitializeComponent();
        }

        private void refreshFacilitatorDetils()
        {
            populateFacilitatorDetails();
        }
        private void refreshFacilitatorAddressDetails()
        {
            if (facilitatorBindingSource.Count > 0)
            {
                this.populateFacilitatorAddressDetails();
            }
        }
        private void refreshFacilitatorContactDetails()
        {
            if (facilitatorBindingSource.Count > 0)
            {
                this.populateFacilitatorContactDetails();
            }
        }


        private void frmAddUpdateFacilitatorDetails_Load(object sender, EventArgs e)
        {

            setFilterCriteria();
            refreshFacilitatorDetils();
            refreshFacilitatorAddressDetails();
            refreshFacilitatorContactDetails();
            
        }

        private void disableDetailPanels()
        {
            gbDetails.Enabled = false;
            dgContactDetails.Enabled = false;
            gbAddressDetails.Enabled = false;
        }
        private void enableDetailPanels()
        {
            gbDetails.Enabled = true;
            dgContactDetails.Enabled = true;
            gbAddressDetails.Enabled = true;
        }

        #region Facilitator Detials

        #region Facilitator Details Populate Methods
        private void populateFacilitatorDetails()
        {
            using (var Dbconnection = new MCDEntities())
            {
                List<Data.Models.Facilitator> AllFacilitators = (from a in Dbconnection.Facilitators
                                                    .Include("Individual.LookupTitle")
                                                        .Include("Individual.Addresses")
                                                        .Include("Individual.ContactDetails")
                                                        .Include("Individual.ContactDetails.LookupContactType")
                                                        .Include("Individual.Addresses.LookupAddressType")
                                                        .Include("Individual.Addresses.LookupProvince")
                                                        .Include("Individual.Addresses.LookupCountry")

                                                     where a.Individual.IndividualFirstName.Contains(this.SearchFacilitatorName)
                                                     || a.Individual.IndividualSecondName.Contains(this.SearchFacilitatorName)
                                                     || a.Individual.IndividualLastname.Contains(this.SearchFacilitatorName)

                                                     orderby a.Individual.IndividualFirstName
                                                     select a).ToList<Data.Models.Facilitator>();
                if (AllFacilitators.Count > 0)
                {
                    this.facilitatorBindingSource.DataSource = AllFacilitators;
                    this.enableDetailPanels();
                }
                else
                {
                    this.facilitatorBindingSource.Clear();
                    this.addressesBindingSource.Clear();
                    this.contactDetailBindingSource.Clear();
                    this.disableDetailPanels();
                }


            };
        }
        #endregion

        #region Facilitator Details Logical Methods

        #endregion

        #region Facilitator Details Control Events
        private void dgvFacilitators_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void btnAddNewFacilitator_Click(object sender, EventArgs e)
        {
            frmAddUpdateFacilitator frm = new frmAddUpdateFacilitator();
            frm.CurrentFaclilitator = new Data.Models.Facilitator();

            frm.ShowDialog();

            this.refreshFacilitatorDetils();
        }

        private void btnUpdateFacilitator_Click(object sender, EventArgs e)
        {
            frmAddUpdateFacilitator frm = new frmAddUpdateFacilitator();
            frm.CurrentFaclilitator = (Data.Models.Facilitator)facilitatorBindingSource.Current;

            frm.ShowDialog();

            int currentFacilitatorID = ((Data.Models.Facilitator)facilitatorBindingSource.Current).FacilitatorID;

            this.refreshFacilitatorDetils();

            int iCurrentIndex = 0;
            foreach (Data.Models.Facilitator FacilitatorObj in facilitatorBindingSource.List)
            {
                if (FacilitatorObj.FacilitatorID == currentFacilitatorID)
                {
                    facilitatorBindingSource.Position = iCurrentIndex;
                }
                iCurrentIndex++;
            }
        }
        private void dgvFacilitators_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var FacilitatorObj = (Data.Models.Facilitator)(row.DataBoundItem);
                    row.Cells[FacilitatorName.Index].Value = FacilitatorObj.Individual.IndividualFirstName.ToString() + " " + FacilitatorObj.Individual.IndividualSecondName.ToString() + " " + FacilitatorObj.Individual.IndividualLastname.ToString();
                }
            }
        }
        #endregion
        #endregion

        #region Facilitator Address Details

        #region Facilitator Address Details Populate Methods
        private void populateFacilitatorAddressDetails()
        {
            Data.Models.Facilitator CurrentFacilitator = (Data.Models.Facilitator)this.facilitatorBindingSource.Current;
            addressesBindingSource.DataSource = (from a in CurrentFacilitator.Individual.Addresses
                                                 select a).ToList<Address>();
        }

        #endregion

        #region Facilitator Address Details Controls Event Methods
        private void dgvFacilitatorAddresses_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var AddressObj = (Data.Models.Address)(row.DataBoundItem);
                    row.Cells[AddressType.Index].Value = AddressObj.LookupAddressType.AddressType.ToString();
                    row.Cells[Province.Index].Value = AddressObj.LookupProvince.Province.ToString();
                    row.Cells[Country.Index].Value = AddressObj.LookupCountry.CountryName.ToString();
                }
            }
        }
        private void dgvFacilitatorAddresses_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void btnUpdateAddress_Click(object sender, EventArgs e)
        {
            Address FacilitatorAddressToBeUpdated = (Address)addressesBindingSource.Current;
            frmAddUpdateAddresses frm = new frmAddUpdateAddresses(FacilitatorAddressToBeUpdated.AddressID);
           
            frm.CurrentAddress = FacilitatorAddressToBeUpdated;
            frm.ShowDialog();
            refreshFacilitatorAddressDetails();
        }
        private void btnAddFacilitatorAddress_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateAddresses frm = new frmAddUpdateAddresses(0))
            {
                frm.ShowDialog();
                if (frm.CurrentAddress != null)
                {
                    if (frm.CurrentAddress.AddressID != 0)
                    {
                        using (var Dbconnection = new MCDEntities())
                        {
                            Data.Models.Facilitator CurrentFacilitator = ((Data.Models.Facilitator)facilitatorBindingSource.Current);

                            Dbconnection.Facilitators.Attach(CurrentFacilitator);
                            Dbconnection.Addresses.Attach(frm.CurrentAddress);

                            CurrentFacilitator.Individual.Addresses.Add(frm.CurrentAddress);

                            Dbconnection.SaveChanges();

                            //Dbconnection.Entry(CurrentFacilitator).Reference(a => a.Individual.Addresses).Load();
                            //                Dbconnection.Entry(CurrentFacilitator).Reference(a => a.LookupProvince).Load();
                            ////                    Dbconnection.Entry(CurrentFacilitator).Reference(a => a.LookupCountry).Load();
                        };
                        refreshFacilitatorAddressDetails();
                    }
                }
            };
          
            

        }
        #endregion
        #endregion

        #region Facilitator Contact Details

        #region Facilitator Contact Details Populate Methods
        private void populateFacilitatorContactDetails()
        {
            Data.Models.Facilitator CurrentEmpolyeeObj = (Data.Models.Facilitator)facilitatorBindingSource.Current;

            List<ContactDetail> result = (from a in CurrentEmpolyeeObj.Individual.ContactDetails
                                          select a).ToList<ContactDetail>();
            contactDetailsBindingSource.DataSource = result;
        }
        #endregion

        #region Facilitator Conact Details Controls Events
        private void contactDetailDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var ContactDetailObj = (ContactDetail)(row.DataBoundItem);
                    row.Cells[FacilitatorName.Index].Value = ContactDetailObj.LookupContactType.ContactType.ToString();
                }
            }
        }

        private void btnAddContactInfo_Click(object sender, EventArgs e)
        {
            Data.Models.Facilitator CurrentFacilitatorObj = (Data.Models.Facilitator)facilitatorBindingSource.Current;
            frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0);
            frm.ShowDialog();
            if (frm.CurrentDetail != null)
            {
                using (var Dbconnection = new MCDEntities())
                {
                    Dbconnection.Facilitators.Attach(CurrentFacilitatorObj);

                    Dbconnection.ContactDetails.Attach(frm.CurrentDetail);

                    CurrentFacilitatorObj.Individual.ContactDetails.Add(frm.CurrentDetail);

                    Dbconnection.SaveChanges();

                    Dbconnection.Entry(frm.CurrentDetail).Reference("LookupContactType").Load();
                };
                this.refreshFacilitatorContactDetails();
            }
        }

        private void btnUpdateContactDetials_Click(object sender, EventArgs e)
        {
            frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(((ContactDetail)contactDetailsBindingSource.Current).ContactDetailID);
            frm.CurrentDetail = (ContactDetail)contactDetailsBindingSource.Current;
            frm.ShowDialog();
            if (frm.CurrentDetail != null)
            {
                using (var Dbconnection = new MCDEntities())
                {
                    Dbconnection.ContactDetails.Attach(frm.CurrentDetail);
                    Dbconnection.Entry(frm.CurrentDetail).State = System.Data.Entity.EntityState.Modified;
                    Dbconnection.SaveChanges();
                };

                this.refreshFacilitatorContactDetails();
            }
        }
        private void contactDetailDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }






        #endregion

        #endregion

        private void dgvFacilitators_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void facilitatorBindingSource_PositionChanged(object sender, EventArgs e)
        {
            refreshFacilitatorAddressDetails();
            refreshFacilitatorContactDetails();
        }

        private void btnRemoveFacilitator_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (var Dbconnection = new MCDEntities())
                {


                };
            }
        }

        private void btnFilterFacilitators_Click(object sender, EventArgs e)
        {
            this.setFilterCriteria();
            this.refreshFacilitatorDetils();
            this.refreshFacilitatorAddressDetails();
            this.refreshFacilitatorContactDetails();
        }
        private string SearchFacilitatorName { get; set; }
        private void setFilterCriteria()
        {
            SearchFacilitatorName = txtFacilitatorFilterCriteria.Text.ToLower().ToString();
        }
        private void resetFilterCriteria()
        {
            SearchFacilitatorName = "";
        }
        private void btnRefreshFacilitatorSearch_Click(object sender, EventArgs e)
        {
            resetFilterCriteria();
            this.refreshFacilitatorDetils();
            this.refreshFacilitatorAddressDetails();
            this.refreshFacilitatorContactDetails();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSetFacilitatorCourses_Click(object sender, EventArgs e)
        {
            frmFacilitatorAssociatedCourses frm = new frmFacilitatorAssociatedCourses();
            frm.currentFacilitator = (Data.Models.Facilitator)facilitatorBindingSource.Current;
            frm.ShowDialog();
        }
    }
}
