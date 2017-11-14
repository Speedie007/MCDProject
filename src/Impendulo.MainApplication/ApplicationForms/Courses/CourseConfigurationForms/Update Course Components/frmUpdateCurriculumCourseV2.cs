﻿using Impendulo.Data.Models;
using Impendulo.Data.Models.Enum;
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

namespace Impendulo.Deployment.Courses
{
    public partial class frmUpdateCurriculumCourseV2 : MetroFramework.Forms.MetroForm
    {
        public int CurriculumCourseID { get; set; }
        public frmUpdateCurriculumCourseV2()
        {
            CurriculumCourseID = 0;
            InitializeComponent();
        }

        private void frmUpdateCurriculumCourseV2_Load(object sender, EventArgs e)
        {
            this.populateCurriculumCourse();
        }

        private void populateCurriculumCourse()
        {

            using (var Dbconnection = new MCDEntities())
            {
                CurriculumCourse cc = (from a in Dbconnection.CurriculumCourses
                                       where a.CurriculumCourseID == this.CurriculumCourseID
                                       select a).FirstOrDefault<CurriculumCourse>();

                txtCourseCost.Text = Convert.ToInt32(cc.Cost).ToString();
                nudCourseDuration.Value = cc.Duration;
                nudCourseMaximumAllowed.Value = cc.CurriculumCourseMinimumMaximum.CurriculumCourseMaximum;
                nudCourseMinimumAllowed.Value = cc.CurriculumCourseMinimumMaximum.CurriculumCourseMinimum;
                txtCourseCourseCode.Text = cc.CurricullumCourseCode.CurricullumCourseCodeValue;
                txtCurrentCostCode.Text = cc.CostCode;
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

            using (var Dbconnection = new MCDEntities())
            {

            };
        }

        private void btnUpdateCurriculumCourse_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        CurriculumCourse courseObj = (from a in Dbconnection.CurriculumCourses
                                                      where a.CurriculumCourseID == this.CurriculumCourseID
                                                      select a).FirstOrDefault<CurriculumCourse>();

                        courseObj.Cost = Convert.ToDecimal(txtCourseCost.Text);
                        courseObj.Duration = Convert.ToInt32(nudCourseDuration.Value);
                        courseObj.CurriculumCourseMinimumMaximum.CurriculumCourseMaximum = Convert.ToInt32(nudCourseMaximumAllowed.Value);
                        courseObj.CurriculumCourseMinimumMaximum.CurriculumCourseMinimum = Convert.ToInt32(nudCourseMinimumAllowed.Value);
                        courseObj.CurricullumCourseCode.CurricullumCourseCodeValue = txtCourseCourseCode.Text;
                        courseObj.CostCode = txtCurrentCostCode.Text;
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

        private void nudStartHours_ValueChanged(object sender, EventArgs e)
        {
            if (nudStartHours.Value > nudEndHours.Value)
                nudEndHours.Value = nudStartHours.Value;
        }

        private void nudStartMin_ValueChanged(object sender, EventArgs e)
        {
            if (nudStartMin.Value > nudEndMin.Value)
                nudEndMin.Value = nudStartMin.Value;
        }

        private void nudEndHours_ValueChanged(object sender, EventArgs e)
        {
            if (nudEndHours.Value < nudStartHours.Value)
                nudStartHours.Value = nudEndHours.Value;
        }

        private void nudEndMin_ValueChanged(object sender, EventArgs e)
        {
            if (nudEndMin.Value < nudStartMin.Value)
                nudStartMin.Value = nudEndMin.Value;
        }
        private void btnLinkDayAvailableToSchedule_Click(object sender, EventArgs e)
        {
            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations

                        DateTime dtStart = new DateTime(2000, 01, 1, Convert.ToInt32(nudStartHours.Value), Convert.ToInt32(nudStartMin.Value), 0);
                        DateTime dtEnd = new DateTime(2000, 01, 1, Convert.ToInt32(nudEndHours.Value), Convert.ToInt32(nudEndMin.Value), 0);
                        LookupDayOfWeek newLookupDayOfWeek = (LookupDayOfWeek)availableCurriculumCourseDayCanBeScheduledBindingSource.Current;

                        CurriculumCourseDayCanBeScheduled newCurriculumCourseDayCanBeScheduled = new CurriculumCourseDayCanBeScheduled()
                        {
                            CurriculumCourseID = this.CurriculumCourseID,
                            StartTime = dtStart.TimeOfDay,
                            EndTime = dtEnd.TimeOfDay,
                            DayOfWeekID = newLookupDayOfWeek.DayOfWeekID,
                            ObjectState = EntityObjectState.Added
                        };
                        Dbconnection.CurriculumCourseDayCanBeScheduleds.Add(newCurriculumCourseDayCanBeScheduled);
                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();

                        //commit transaction
                        dbTran.Commit();
                        this.populateAvaiabledays();
                        this.populateLinkedDays();
                        this.nudStartHours.Value = 8;
                        this.nudStartMin.Value = 0;
                        this.nudEndHours.Value = 16;
                        this.nudEndMin.Value = 0;
                        if (availableCurriculumCourseDayCanBeScheduledBindingSource.Count == 0)
                        {
                            btnLinkDayAvailableToSchedule.Enabled = false;
                        }

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

        private void dgvLinkedDayCourseCanBeScheduled_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var CurriculumCourseDayCanBeScheduledObj = (CurriculumCourseDayCanBeScheduled)(row.DataBoundItem);


                    row.Cells[colDay.Index].Value = CurriculumCourseDayCanBeScheduledObj.LookupDayOfWeek.DayOfWeek.ToString();

                }
            }
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedIndex)
            {

                case 1:
                    this.populateAvaiabledays();
                    this.populateLinkedDays();
                    break;
            }

        }

        #region Populate Methods

        private void populateAvaiabledays()
        {
            using (var Dbconnection = new MCDEntities())
            {
                availableCurriculumCourseDayCanBeScheduledBindingSource.DataSource =
                    (from a in Dbconnection.LookupDayOfWeeks
                     select a).Except(from a in Dbconnection.CurriculumCourseDayCanBeScheduleds
                                      where a.CurriculumCourse.CurriculumCourseID == CurriculumCourseID
                                      select a.LookupDayOfWeek).ToList<LookupDayOfWeek>();
            };
        }
        private void populateLinkedDays()
        {
            using (var Dbconnection = new MCDEntities())
            {
                linkedCcurriculumCourseDayCanBeScheduledBindingSource.DataSource =
                    (from a in Dbconnection.CurriculumCourseDayCanBeScheduleds
                     orderby a.DayOfWeekID
                     where a.CurriculumCourse.CurriculumCourseID == CurriculumCourseID
                     select a).ToList<CurriculumCourseDayCanBeScheduled>();
            };
        }
        #endregion



        private void btnRemoveLinkedDaysToSchedule_Click(object sender, EventArgs e)
        {
            CurriculumCourseDayCanBeScheduled CurriculumCourseDayCanBeScheduledObj = (CurriculumCourseDayCanBeScheduled)(dgvLinkedDayCourseCanBeScheduled.Rows[dgvLinkedDayCourseCanBeScheduled.CurrentRow.Index].DataBoundItem);
            CurriculumCourseDayCanBeScheduledObj.ObjectState = EntityObjectState.Deleted;

            using (var Dbconnection = new MCDEntities())
            {
                Dbconnection.Entry(CurriculumCourseDayCanBeScheduledObj).State = EntityState.Deleted;
                Dbconnection.SaveChanges();

                this.populateAvaiabledays();
                this.populateLinkedDays();

                this.btnLinkDayAvailableToSchedule.Enabled = true;
            };
        }
    }
}
