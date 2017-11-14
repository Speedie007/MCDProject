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
using Impendulo.Common.Enum;
using System.Data.Entity;
using MetroFramework.Forms;

namespace Impendulo.Deployment.Enquiry
{
    /*
     * How to data bind and create chart control with entity framework
     * https://www.codeproject.com/Articles/221931/Entity-Framework-in-WinForms
     * *****************************************************************/
    public partial class frmWorkbanchEnquiries : MetroForm
    {
        public frmWorkbanchEnquiries()
        {
            InitializeComponent();
        }

        private void frmWorkbanchEnquiries_Load(object sender, EventArgs e)
        {
            /*set date parameters*/
            DateTime Todaydate = DateTime.Now;
            Todaydate.Month.ToString("D");
            lbCurrentDateTime.Text = Todaydate.ToShortDateString();
            dtpFrom.Value = new DateTime(Todaydate.Year, Todaydate.Month, 1);
            dtpTo.Value = new DateTime(Todaydate.Year, Todaydate.Month, 1).AddMonths(1).AddDays(-1);



            this.enquiriesByMonth(dtpFrom.Value, dtpTo.Value, EnumDepartments.Apprenticeship);

            rbEnquiryByMonth.Checked = true;

            //make chart2 invisible
            chart2.Visible = false;


        }

        /// <summary>
        ///retruns date into the future or in to be past by a set amount of days excluding Saturday and Sunday.
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <param name="AmountDaysToAdd"></param>
        /// <returns></returns>
        private void LoadItems(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {
            lblEquiyTotalEquiry.Text = getAllEnquiry(FromDate, Todate, aDepartment).Count.ToString();

            lblNewEnquiry.Text = GetNewEquiry(FromDate, Todate, aDepartment).Count.ToString();

            lblOverDueEnquiries.Text = GetOverDueEnquiry(FromDate, Todate, aDepartment).Count.ToString();

            lblCompanyEnquiry.Text = getCompanyEnquiry(FromDate, Todate, aDepartment).Count.ToString();

            lblPrivateEquiries.Text = getPrivateEnquiry(FromDate, Todate, aDepartment).Count.ToString();

        }
        /// <summary>
        ///  Get a list of equiry Object  -Amount of equiry for the period defined by the dates.
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="Todate"></param>
        /// <param name="aDepartment"></param>
        /// <returns>List of Equiry Objects</returns>
        private List<Data.Models.Enquiry> getAllEnquiry(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {
            List<Data.Models.Enquiry> Rtn = new List<Data.Models.Enquiry>();
            using (var Dbconnection = new MCDEntities())
            {
                //Rtn = (from a in Dbconnection.Enquiries
                //           /*Include Sections */
                //       from b in a.CurriculumEnquiries
                //           /* Where Sections */
                //       where
                //            /*Filters*/
                //            (a.EnquiryDate >= FromDate &&
                //             a.EnquiryDate <= Todate) &&
                //             //Sections
                //             b.Curriculum.DepartmentID == (int)aDepartment
                //       select a)
                //                         /*Aggregation*/
                //                         .ToList<Data.Models.Enquiry>();
            };
            return Rtn;
        }

        private List<Data.Models.Enquiry> getCompanyEnquiry(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {
            List<Data.Models.Enquiry> Rtn = new List<Data.Models.Enquiry>();
            using (var Dbconnection = new MCDEntities())
            {
                //company enquiries
                //lblCompanyEnquiry.Text
                Rtn = (from a in Dbconnection.Enquiries
                       where a.EnquiryDate >= FromDate && a.EnquiryDate <= Todate && a.Companies.Count > 0
                       select a).ToList<Data.Models.Enquiry>();
            }
            return Rtn;
        }

        private List<Data.Models.Enquiry> getPrivateEnquiry(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {
            List<Data.Models.Enquiry> Rtn = new List<Data.Models.Enquiry>();
            using (var Dbconnection = new MCDEntities())
            {
                //lblPrivateEquiries.Text 
                Rtn = (from a in Dbconnection.Enquiries
                       from b in a.Individuals
                       where a.EnquiryDate >= FromDate && a.EnquiryDate <= Todate && b.Companies.Count == 0
                       select a).ToList<Data.Models.Enquiry>();
            }

            return Rtn;
        }

        /// <summary>
        /// Get a list of equiry Object  - Only the new one for the period defined by the dates.
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="Todate"></param>
        /// <param name="aDepartment"></param>
        /// <returns>>List of Equiry Objects</returns>
        private List<Data.Models.Enquiry> GetNewEquiry(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {
            List<Data.Models.Enquiry> Rtn = new List<Data.Models.Enquiry>();
            using (var Dbconnection = new MCDEntities())
            {
                //Rtn = (from a in Dbconnection.Enquiries
                //       from b in a.CurriculumEnquiries
                //       where
                //       // a.InitialConsultationComplete == false &&
                //       b.LookupEnquiryStatus.EnquiryStatusID == (int)EnumEnquiryStatuses.New
                //       && a.EnquiryDate >= FromDate && a.EnquiryDate <= Todate &&
                //       b.Curriculum.DepartmentID == (int)aDepartment
                //       select a).ToList<Data.Models.Enquiry>();
            };

            return Rtn;
        }

        private List<Data.Models.Enquiry> GetOverDueEnquiry(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {
            List<Data.Models.Enquiry> Rtn = new List<Data.Models.Enquiry>();

            using (var Dbconnection = new MCDEntities())
            {
                //I created the CustomerDateTime static classs inside impendulo.Common
                DateTime queryDatetime = Impendulo.Common.CustomDateTime.getCustomDateTime(DateTime.Now, -4);
                //DateTime queryDatetime = getCustDateTime(DateTime.Now, -4);
                //Rtn = (from a in Dbconnection.Enquiries
                //       from b in a.CurriculumEnquiries
                //       where
                //       //Enquiriesw are deemed Over Due if not responded to with in 3 Working Days
                //       a.EnquiryDate <= queryDatetime &&
                //       b.EnquiryStatusID != (int)EnumEnquiryStatuses.Enquiry_Closed &&
                //       b.Curriculum.DepartmentID == (int)aDepartment
                //       select a).ToList<Data.Models.Enquiry>();
            };
            //Over due enquiries

            return Rtn;
        }
        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            //LoadItems(dtpFrom.Value, dtpTo.Value, EnumDepartments.Apprenticeship);
            //fillChart();
        }
        /// <summary>
        /// Date Added 24 May 2017
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApprenticeshipFilterSearch_Click(object sender, EventArgs e)
        {
            LoadItems(dtpFrom.Value, dtpTo.Value, EnumDepartments.Apprenticeship);
            fillChart(dtpFrom.Value, dtpTo.Value, EnumDepartments.Apprenticeship);
        }

        private void fillChart(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {
            using (var Dbconnection = new MCDEntities())
            {
                /*Notice that i have assigned the YValueMembers with the same name as the field that i created in the Anonymous class flied Name:
                 * match the numbers to referfence what i mean
                 * 1.1 YValueMembers =  AmountOfEnquiries is the same as the field name that i created in the anonymous class field name.
                 * 
                 * */
                chart1.Series["AllEnquiries"].YValueMembers = "AmountOfEnquiries";   //1.1 HERE MAtch the Class Field Name Below.
                chart1.Series["AllEnquiries"].XValueMember = "Date";                 //1.2 HERE MAtch the Class Field Name Below.

                //count enquiries made at a specific date
                //var enquiriesByDate = (from a in Dbconnection.Enquiries
                //                       from b in a.CurriculumEnquiries
                //                       where a.EnquiryID != 1 && a.EnquiryDate >= FromDate && a.EnquiryDate <= Todate && b.Curriculum.DepartmentID == (int)aDepartment

                //                       // where a.EnquiryID != 1 && a.EnquiryDate >= FromDate && a.EnquiryDate <= Todate && b.Curriculum.DepartmentID == (int)aDepartment

                //                       group a by a.EnquiryDate into b
                //                       select new
                //                       {
                //                           Date = b.Key,                            //1.1 - Same field name "Date" as above SEE 1.1 ABOVE( I made the field name up - the Fieldname is the same as above.)//1.1 - Same field name as above( I made the field name up - the Fieldname is the same as above.
                //                           AmountOfEnquiries = b.Distinct().Count() //1.2 - Same field name "AmountOfEnquiries" as above SEE 1.2 ABOVE( I made the field name up - the Fieldname is the same as above.)
                //                       });
                //filling the chart
                //enquiryBindingSource.DataSource = enquiriesByDate.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbNewEnquiryByMonth_CheckedChanged(object sender, EventArgs e)
        {
            this.NewEnquiryByMonth(dtpFrom.Value, dtpTo.Value, EnumDepartments.Apprenticeship);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAmountOfPrivateVSCompanyEnquiriesPerMonth_CheckedChanged(object sender, EventArgs e)
        {
            this.AmountOfPrivateVSCompanyEnquiriesPerMonth(dtpFrom.Value, dtpTo.Value, EnumDepartments.Apprenticeship);
        }

        private void rbEnquiryByMonth_CheckedChanged(object sender, EventArgs e)
        {
            this.enquiriesByMonth(dtpFrom.Value, dtpTo.Value, EnumDepartments.Apprenticeship);
        }

        private void enquiriesByMonth(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {
            if (rbEnquiryByMonth.Checked == true)
            {
                lblGraphTitle.Text = "ENQUIRY BY MONTH";
                this.fillChart(dtpFrom.Value, dtpTo.Value, EnumDepartments.Apprenticeship);
            }
        }

        private void NewEnquiryByMonth(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {

            if (rbNewEnquiryByMonth.Checked == true)
            {
                lblGraphTitle.Text = "NEW ENQUIRY BY MONTH";
                //Update the chart
                //chart1.BackColor = System.Drawing.Color.Gray;
                using (var Dbconnection = new MCDEntities())
                {
                    chart1.Series["AllEnquiries"].YValueMembers = "AmountOfEnquiries";   //1.1 HERE MAtch the Class Field Name Below.
                    chart1.Series["AllEnquiries"].XValueMember = "Date";                 //1.2 HERE MAtch the Class Field Name Below.

                    //count enquiries made at a specific date
                    //var enquiriesByDate = (from a in Dbconnection.Enquiries
                    //                       from b in a.CurriculumEnquiries
                    //                       where a.EnquiryID != 1 && a.EnquiryDate >= FromDate && a.EnquiryDate <= Todate && b.Curriculum.DepartmentID == (int)aDepartment
                    //                       && b.LookupEnquiryStatus.EnquiryStatusID == (int)EnumEnquiryStatuses.New
                    //                       group a by a.EnquiryDate into b
                    //                       select new
                    //                       {
                    //                           Date = b.Key,                            //1.1 - Same field name "Date" as above SEE 1.1 ABOVE( I made the field name up - the Fieldname is the same as above.)//1.1 - Same field name as above( I made the field name up - the Fieldname is the same as above.
                    //                           AmountOfEnquiries = b.Distinct().Count() //1.2 - Same field name "AmountOfEnquiries" as above SEE 1.2 ABOVE( I made the field name up - the Fieldname is the same as above.)
                    //                       });


                    //filling the chart
                    //enquiryBindingSource.DataSource = enquiriesByDate.ToList();
                }
            }
        }

        private void AmountOfPrivateVSCompanyEnquiriesPerMonth(DateTime FromDate, DateTime Todate, EnumDepartments aDepartment)
        {
            enquiryBindingSource1.Clear();
            chart2.Visible = true;

            chart2.Series["Company"].Points.Clear();
            chart2.Series["Private"].Points.Clear();
            //chart1.Visible = false;
            if (rbAmountOfPrivateVSCompanyEnquiriesPerMonth.Checked == true)
            {
                lblGraphTitle.Text = "AMOUNT OF PRIVATE VS COMPANY ENQUIRIES";
                //Update the chart

                //Join the enquiries table with the CompanyEnquiries table and EnquiryAssociatedContact table in order to count the number
                //of enquries made by company an the individual in a certain date. 
                using (var Dbconnection = new MCDEntities())
                {
                    var joinabc = from a in Dbconnection.Enquiries
                                      //from b in a.Companies
                                  from c in a.Individuals
                                      //join d in Dbconnection.Companies on b.CompanyID equals d.CompanyID
                                      //join e in Dbconnection.Individuals on c.IndividualID equals e.IndividualID
                                      //where a.EnquiryID == b.CompanyID
                                  group a by a.EnquiryDate into b
                                  select new
                                  {
                                      Date = b.Key,
                                      CompanyEnquiries = b.Where(ab => ab.Companies.Count > 0).Count(),
                                      PrivateEnquries = b.Select(ac => ac.Individuals.Where( ad => ad.Companies.Count == 0)).Count()
                                  };

                    enquiryBindingSource1.DataSource = joinabc.ToList();

                    chart2.Series["Company"].Points.AddXY("Date", "CompanyEnquiries");
                    chart2.Series["Private"].Points.AddXY("Date", "PrivateEnqurie");
                }


            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                    /*load queries*/
                    LoadItems(dtpFrom.Value, dtpTo.Value, EnumDepartments.Apprenticeship);
                    break;
                case 2:
                    //Console.WriteLine("Case 2");
                    break;
                default:
                    //Console.WriteLine("Default case");
                    break;
            }

        }
    }
}
