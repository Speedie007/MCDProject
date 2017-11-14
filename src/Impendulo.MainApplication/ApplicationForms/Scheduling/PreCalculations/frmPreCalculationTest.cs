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
using Impendulo.Common.ScheduleAvailablityAlgorithm;

using Impendulo.Common.Enum;
using Impendulo.Common;

namespace Impendulo.Deployment.Scheduling
{
    public partial class frmPreCalculationTest : Form
    {
        public frmPreCalculationTest()
        {
            InitializeComponent();
        }

        private void frmPreCalculationTest_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(2017, 07, 19);
            //dt = dt.AddDays(-30);
            //CourseAvailablePeriodByFacilitator xxx = new CourseAvailablePeriodByFacilitator(SearchTimeAhead.OneMonth,dt.Date,4);
            CourseAvailablePeriod xx = new CourseAvailablePeriod(dt, new DateTime(2017, 08, 01), 4, "Orientation", 2,
            EnumPreLoadType.PreloadingOnSite
            , new List<EnumDayOfWeeks> {
                EnumDayOfWeeks.Monday,
                EnumDayOfWeeks.Tuesday,
                EnumDayOfWeeks.Wednesday,
                EnumDayOfWeeks.Thursday,
                EnumDayOfWeeks.Friday
            },
             null,
             null,
             null,
             2004);
            txtOutput.Text += "Booked Facilitator Dates:\n\n";
            foreach (FacilitatorPeriod aa in xx.FacilitatorBookedPeriods)
            {
                txtOutput.Text += "Facilitator ID : " + aa.FacillitatorID.ToString() + " Facilitator Name  " + aa.FacilitatorName + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            }
            txtOutput.Text += "\nAvailalble Facilitator Dates:\n\n";
            foreach (FacilitatorPeriod aa in xx.FacilitatorAvailablePeriods)
            {
                txtOutput.Text += "Facilitator ID : " + aa.FacillitatorID.ToString() + " Facilitator Name  " + aa.FacilitatorName + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            }

            txtOutput.Text += "\n Booked OnSiteVenue Dates:\n\n";
            foreach (OnSiteVenuePeriod aa in xx.OnSiteVenueBookedPeriods)
            {
                txtOutput.Text += "Venue ID : " + aa.VenueID.ToString() + " Venue Name  " + aa.VenueName + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            }


            txtOutput.Text += "\n Available OnSite Venue Dates:\n\n";
            foreach (OnSiteVenuePeriod aa in xx.OnSiteVenueAvailablePeriods)
            {
                txtOutput.Text += "Venue ID : " + aa.VenueID.ToString() + " Venue Name  " + aa.VenueName + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            }

            txtOutput.Text += "\n Booked OffSiteVenue Dates:\n\n";
            foreach (OffSiteVenuePeriod aa in xx.OffSiteVenueBookedPeriods)
            {
                txtOutput.Text += "Venue ID : " + aa.VenueID.ToString() + " Venue Name  " + aa.VenueName + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            }


            txtOutput.Text += "\n Available OffSite Venue Dates:\n\n";
            foreach (OffSiteVenuePeriod aa in xx.OffSiteVenueAvailablePeriods)
            {
                txtOutput.Text += "Venue ID : " + aa.VenueID.ToString() + " Venue Name  " + aa.VenueName + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            }

            //txtOutput.Text += "\n Available Dates that Each Ailitator Can Be Scheduled For a Course:\n\n";
            //foreach (AvailableCoursePeriodByFacilitatorResultSet aa in xx.ListOfAvailableCoursePeriodByFacilitator)
            //{
            //    txtOutput.Text += "Facilitator ID : " + aa.FacilitatorID.ToString() + " Facilitator Name  " + aa.FacilitatorName + "Course ID " + aa.CourseID + " Course Name " + aa.CourseName + " - StartDate: " + aa.CourseStartDate + " - EndDate: " + aa.CourseEndDate + "\n";
            //}
            txtOutput.Text += "\nFINAL OUTPUT DATA SET: Available Facilitator Dates For Course Filtered by - FACILITATOR, Course : \n\n";
            foreach (AvailableCoursePeriodByFacilitatorResultSet ACPBFRS in xx.ListOfAvailableCoursePeriodByFacilitator)
            {
                txtOutput.Text += "Facilitator ID : " + ACPBFRS.FacilitatorID.ToString() + " Facilitator Name  " + ACPBFRS.FacilitatorName + "\nCourse ID " + ACPBFRS.CourseID + " Course Name " + ACPBFRS.CourseName + "\nStartDate: " + ACPBFRS.CourseStartDate + "\nEndDate: " + ACPBFRS.CourseEndDate + "\n-------------------------------------\n";
            }

            txtOutput.Text += "\nFINAL OUTPUT DATA SET: Available Course Filtered by - FACILITATOR, VENUE : \n\n";
            foreach (AvailableOnSitePeriods AOSP in xx.GetAllAvailableOnStiePeriods())
            {
                txtOutput.Text += "Facilitator ID : " + AOSP.FacilitatorID.ToString() + " Facilitator Name  " + AOSP.FacilitatorName + "\nCourse ID " + AOSP.CourseID + " Course Name " + AOSP.CourseName + "\nStartDate: " + AOSP.CourseStartDate + "\nEndDate: " + AOSP.CourseEndDate + "\nVenue ID : " + AOSP.VenueID.ToString() + " Venue Name  " + AOSP.VenueName + "\n-------------------------------------\n";
            }
            //SchedulingDateSearchAlgorithms ScheduleAlgorthim = new SchedulingDateSearchAlgorithms(
            //    CourseIDBookingFor: 4,
            //    InitialDateToStartFrom: dt.Date,
            //     AmountOfSearchTimeAhead: SearchTimeAhead.OneMonth,
            //     CurriculumCourseID: 4117,
            //     ScheduleLocation: Common.Enum.EnumScheduleLocations.Onsite
            //    );
            //List<AvailableOnSitePeriods> kkkkk =  xx.GetAllAvailableOnStiePeriodsV1();
            dataGridView1.DataSource = xx.GetAllAvailableOnStiePeriods()
                .OrderBy(a => a.FacilitatorName)
                //.ThenBy(a => a.CourseEndDate)
                .ThenBy(a => a.CourseStartDate).ToList();
            dataGridView2.DataSource = xx.GetAllAvailableOffSitePeriods()
                .OrderBy(a => a.FacilitatorName)
                //.ThenBy(a => a.CourseEndDate)
                .ThenBy(a => a.CourseStartDate).ToList();

            CourseAvailablePeriod ScheduleAlgorthim = new CourseAvailablePeriod(
               CourseSearchPeriodStartDate: Common.CustomDateTime.getCustomDateTime(
                                               CurrentDate: new DateTime(2017, 07, 01),
                                               AmountDaysToAdd: 2,

                                               DaysCanSchedule: new List<EnumDayOfWeeks> {
                                                EnumDayOfWeeks.Monday,
                                                EnumDayOfWeeks.Tuesday,
                                                EnumDayOfWeeks.Wednesday,
                                                EnumDayOfWeeks.Thursday }),

               CourseSearchPeriodEndDate: new DateTime(2017, 08, 01),
               CourseID: 4,
               CourseDescription: "Orientation",
                CourseDuration: 2,
               PreLoadingType: EnumPreLoadType.PreloadingOnSite,
                SetOfDaysOfWeekCourseCanBeScheulded: new List<EnumDayOfWeeks> {
                                                EnumDayOfWeeks.Monday,
                                                EnumDayOfWeeks.Tuesday,
                                                EnumDayOfWeeks.Wednesday,
                                                EnumDayOfWeeks.Thursday },
               OffSiteID: 2004);

            //availableOnSitePeriodsBindingSource.DataSource = ScheduleAlgorthim.GetAllAvailableOnStiePeriods();
            //dataGridView1.DataSource = aa.CourseByFacilitatorAvailableDateSets;
            //txtOutput.Text += "Booked Facilitator Dates:\n\n";
            //foreach (FacilitatorBookedDateSet aa in ScheduleAlgorthim.FacilitatorBookedDateSets)
            //{
            //    txtOutput.Text += aa.FacilitatorID.ToString() + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            //}
            //txtOutput.Text += "Available Facilitator Dates:\n\n";
            //foreach (FacilitatorAvailableDateSet aa in ScheduleAlgorthim.FacilitatorAvailableDateSets)
            //{
            //    txtOutput.Text += aa.FacilitatorID.ToString() + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            //}
            //txtOutput.Text += "Booked Venues Dates:\n\n";
            //foreach (VenueBookedDateSet aa in ScheduleAlgorthim.VenueBookedDateSets)
            //{
            //    txtOutput.Text += aa.VenueID.ToString() + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            //}
            //txtOutput.Text += "Available Venue Dates:\n\n";
            //foreach (VenueAvailableDateSet aa in ScheduleAlgorthim.VenueAvailableDateSets)
            //{
            //    txtOutput.Text += aa.VenueID.ToString() + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            //}

            //txtOutput.Text += "Available Course By Facilitator Dates:\n\n";
            //foreach (CourseByFacilitatorAvailableDateSet aa in ScheduleAlgorthim.CourseByFacilitatorAvailableDateSets)
            //{
            //    txtOutput.Text += "FacilitatorID: " + aa.FacilitatorID.ToString() + "CurriculumCourseID: " + aa.CurriculumCourseID + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            //}

            //txtOutput.Text += "Available Course By Facilitator AND Venue Dates:\n\n";
            //foreach (CourseByFacilitatorAndVenueAvailableDateSet aa in ScheduleAlgorthim.CourseByFacilitatorAndVenueAvailableDateSets)
            //{
            //    txtOutput.Text += "FacilitatorID: " + aa.FacilitatorID.ToString() + " VenueID: " + aa.VenueID.ToString() + "CurriculumCourseID: " + aa.CurriculumCourseID + " - StartDate: " + aa.StartDate + " - EndDate: " + aa.EndDate + "\n";
            //}
        }
        //Orientateion is ID = 4
        private void GetAllFacilitatorForSelectedCourse()
        {

            using (var Dbconnection = new MCDEntities())
            {
                List<Impendulo.Data.Models.Facilitator> f = (from a in Dbconnection.Facilitators
                                       from b in a.FacilitatorAssociatedCourses
                                       where b.CourseID == 4
                                       select a).ToList<Impendulo.Data.Models.Facilitator>();


                List<object> s = new List<object>();

                foreach (Impendulo.Data.Models.Facilitator AvaiableFacilitaor in f)
                {

                    //var aa = (from a in Dbconnection.Schedules
                    //          where a.ScheduleStartDate >= dt.Date &&
                    //          a.FacilitatorID == AvaiableFacilitaor.FacilitatorID
                    //          select new
                    //          {
                    //              FacilitatorID = a.FacilitatorID,
                    //              ScheduleStartDate = a.ScheduleStartDate,
                    //              ScheduleCompletionDate = a.ScheduleCompletionDate

                    //          }).OrderBy(a => a.ScheduleStartDate).Distinct().ToList();
                    //foreach (var FaciliatorBookedDates in aa)
                    //{
                    //    FacilitatorBookedDateSet FBDS = new FacilitatorBookedDateSet(
                    //        FacilitatorID: FaciliatorBookedDates.FacilitatorID,
                    //         StartDate: FaciliatorBookedDates.ScheduleStartDate,
                    //         EndDate: FaciliatorBookedDates.ScheduleCompletionDate);
                    //    ScheduleAlgorthim.FacilitatorBookedDateSets.Add(FBDS);
                    //}


                }

                //string xrsd = "";
                //var t = (from a in Dbconnection.Schedules
                //         where a.FacilitatorID == AvaiableFacilitaor.FacilitatorID
                //         group a by a.ScheduleStartDate into g

                //         select new
                //         {

                //             CurrentFacilitatorID = AvaiableFacilitaor.FacilitatorID,
                //             CurrentFacilitatorName = AvaiableFacilitaor.Individual.FullName,
                //             StartDate = g.Key,
                //             GroupedScheduleEnrollments = g
                //         }).ToList();
                //if (t.Count > 0)
                //    s.Add(t);
                //List<Schedule> ss = (from a in Dbconnection.Schedules
                //                     where a.FacilitatorID == AvaiableFacilitaor.FacilitatorID
                //                     //orderby a.ScheduleStartDate
                //                     select a
                //                     ).ToList();
                //string xrsd = "";


                /*REquired
                 var xx = (from a in Dbconnection.Schedules
                           where a.ScheduleStartDate > dt
                           group a by a.ScheduleStartDate into cc
                           select cc).ToList();

                 var yy = (from a in Dbconnection.Schedules
                           where a.ScheduleStartDate > dt
                           select new
                           {
                               FacilitatorID = a.FacilitatorID,
                               ScheduleStartDate = a.ScheduleStartDate,
                               ScheduleCompletionDate = a.ScheduleCompletionDate

                           }).OrderBy(a => a.ScheduleStartDate).Distinct().ToList();
                 */

                //foreach (Facilitator AvaiableFacilitaor in f)
                //{
                //    var t = (from a in Dbconnection.Schedules
                //                 //where a.FacilitatorID == AvaiableFacilitaor.FacilitatorID
                //             group a by a.ScheduleStartDate into g

                //             select new { g.Key, GroupedScheduleEnrollments = g }).ToList();
                //}
            };
        }
    }
}
