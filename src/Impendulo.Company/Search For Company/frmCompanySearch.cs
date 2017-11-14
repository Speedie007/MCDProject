using Impendulo.Data.Models;
using MetroFramework.Forms;
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

namespace Impendulo.Development.Company

{
    public partial class frmCompanySearch : MetroForm
    {

        private string SearchCompanyName { get; set; }
        private string SearchSicNumber { get; set; }
        private string SearchSARSRegNumber { get; set; }
        private string SearchSETANumber { get; set; }

        public Data.Models.Company CurrentCompany { get; set; }
        public frmCompanySearch()
        {
            InitializeComponent();
        }

        private void frmCompanySearchV2_Load(object sender, EventArgs e)
        {
            this.setSearchVariables();
            this.SearchForCompany();
            this.LoadSearchSuggestions();
        }
        private void LoadSearchSuggestions()
        {

            AutoCompleteStringCollection allowedCompanyNames = new AutoCompleteStringCollection();
            AutoCompleteStringCollection allowedSETANumbers = new AutoCompleteStringCollection();
            AutoCompleteStringCollection allowedSICNumbers = new AutoCompleteStringCollection();
            AutoCompleteStringCollection allowedSARSRegistrationNumbers = new AutoCompleteStringCollection();

            List<Data.Models.Company> x = new List<Data.Models.Company>();

            using (var Dbconnection = new MCDEntities())
            {
                x = (from a in Dbconnection.Companies
                     select a)
                     .ToList<Data.Models.Company>();


            };
            foreach (Data.Models.Company CompanyObj in x)
            {
                allowedCompanyNames.Add(CompanyObj.CompanyName.ToString());
                allowedSETANumbers.Add(CompanyObj.CompanySETANumber.ToString());
                allowedSICNumbers.Add(CompanyObj.CompanySicCode.ToString());
                allowedSARSRegistrationNumbers.Add(CompanyObj.CompanySARSLevyRegistrationNumber.ToString());
            }

            txtCompanyName.AutoCompleteCustomSource = allowedCompanyNames;
            txtCompanyName.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtCompanyName.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtSETANumber.AutoCompleteCustomSource = allowedSETANumbers;
            txtSETANumber.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtSETANumber.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtSicCode.AutoCompleteCustomSource = allowedSICNumbers;
            txtSicCode.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtSicCode.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtSARSRegNumber.AutoCompleteCustomSource = allowedSARSRegistrationNumbers;
            txtSARSRegNumber.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtSARSRegNumber.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.setSearchVariables();
            this.SearchForCompany();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.txtSicCode.Text = "";
            this.txtSETANumber.Text = "";
            this.txtSARSRegNumber.Text = "";
            this.txtCompanyName.Text = "";
            this.setSearchVariables();
            SearchForCompany();
        }
        private void setSearchVariables()
        {
            this.SearchCompanyName = this.txtCompanyName.Text;
            this.SearchSicNumber = this.txtSicCode.Text;
            this.SearchSARSRegNumber = txtSARSRegNumber.Text;
            this.SearchSETANumber = txtSETANumber.Text;

        }
        private void SearchForCompany()
        {

            using (var DbConnection = new MCDEntities())
            {
                List<Data.Models.Company> resultByFilter = (from a in DbConnection.Companies

                                                            where
                                                            a.CompanyName.Contains(this.SearchCompanyName) &&
                                                            a.CompanySARSLevyRegistrationNumber.Contains(this.SearchSARSRegNumber) &&
                                                            a.CompanySETANumber.Contains(this.SearchSETANumber) &&
                                                            a.CompanySicCode.Contains(SearchSicNumber)
                                                            orderby a.CompanyName
                                                            select a)
                                                            .Include(a => a.Individuals)
                                                            .Include(a => a.Individuals.Select(b => b.ContactDetails))
                                                            .Include(a => a.Individuals.Select(b => b.ContactDetails.Select(c => c.LookupContactType)))
                                                            .Include(a => a.Individuals.Select(b => b.LookupTitle))
                                                           .ToList<Data.Models.Company>();

                companyBindingSource.DataSource = resultByFilter;
                if (companyBindingSource.Count == 0)
                {
                    btnAddCompany.Visible = true;
                }
                else
                {
                    btnAddCompany.Visible = false;
                }
            };
        }

        private void dgvCompanySearchResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                CurrentCompany = (Data.Models.Company)(senderGrid.Rows[e.RowIndex].DataBoundItem);
                this.Close();
            }
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            using (frmAddCompany frm = new frmAddCompany())
            {
                frm.ShowDialog();
                SearchForCompany();
            }
        }
    }
}
