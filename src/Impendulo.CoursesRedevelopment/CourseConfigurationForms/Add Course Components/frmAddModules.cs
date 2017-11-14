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

namespace Impendulo.Development.Courses
{
    public partial class frmAddModules : MetroFramework.Forms.MetroForm
    {
        public int DepartmentID { get; set; }
        public int CurriculumCourseID { get; set; }
        public frmAddModules()
        {
            InitializeComponent();
        }

        private void setAddAndRemoveButtons()
        {
            if (bindingSourceAvailableModules.Count == 0) { btnLinkModule.Enabled = false; } else { btnLinkModule.Enabled = true; };
            if (bindingSourceLinkedModules.Count == 0) { btnRemoveModule.Enabled = false; } else { btnRemoveModule.Enabled = true; };
        }

        public int TrainingDepartmentCourseEnrollmentTypeMetaDataID { get; set; }

        private void btnLinkModule_Click(object sender, EventArgs e)
        {
            using (var DbConnection = new MCDEntities())
            {
                CurriculumCourseModule TDCETM = new CurriculumCourseModule()
                {
                    CurriculumCourseID = this.CurriculumCourseID,
                    ModuleID = Convert.ToInt32(lstAvailableModules.SelectedValue)
                };
                DbConnection.CurriculumCourseModules.Add(TDCETM);
                DbConnection.SaveChanges();
            };
            this.populateAvailableModules();
            this.populateLinkedModules();
        }
        private void btnRemoveModule_Click(object sender, EventArgs e)
        {
            using (var DbConnection = new MCDEntities())
            {
                int ID = Convert.ToInt32(lstLinkedModules.SelectedValue);
                CurriculumCourseModule LinkedModuleToRemove = (from a in DbConnection.CurriculumCourseModules
                                                               where a.CurriculumCourseID == this.CurriculumCourseID
                                                                        && a.ModuleID == ID
                                                               select a).FirstOrDefault<CurriculumCourseModule>();

                DbConnection.Entry(LinkedModuleToRemove).State = System.Data.Entity.EntityState.Deleted;
                DbConnection.SaveChanges();
            };
            this.populateAvailableModules();
            this.populateLinkedModules();
        }



        #region "Populate Methods"

        private void populateAvailableModules()
        {
            using (var DbConnection = new MCDEntities())
            {
                int _DepartmentID = Convert.ToInt32(cboDepartments.SelectedValue);
                List<Module> AvaliableModules = (from a in DbConnection.Modules
                                                 where a.DepartmentID == _DepartmentID
                                                 select a).Except(from a in DbConnection.Modules
                                                                  from b in a.CurriculumCourseModules
                                                                  where b.CurriculumCourseID == this.CurriculumCourseID
                                                                  select a).ToList<Module>();

                this.bindingSourceAvailableModules.DataSource = (from a in AvaliableModules
                                                                 where a.ModuleName.ToLower().Contains(this.txtModuleFilterCriteria.Text.ToLower())
                                                                 orderby a.ModuleName
                                                                 select a).ToList<Module>();
            };
            this.setAddAndRemoveButtons();
        }
        private void populateLinkedModules()
        {
            using (var DbConnection = new MCDEntities())
            {
                List<Module> LinkedModules = (from a in DbConnection.Modules
                                              from b in a.CurriculumCourseModules
                                              where b.CurriculumCourseID == this.CurriculumCourseID
                                              select a).ToList<Module>();

                this.bindingSourceLinkedModules.DataSource = (from a in LinkedModules
                                                              orderby a.ModuleName
                                                              select a).ToList<Module>();
            };
            this.setAddAndRemoveButtons();
        }
        private void populateDepartments()
        {
            using (var Dbconnection = new MCDEntities())
            {
                departmentBindingSource.DataSource = (from a in Dbconnection.LookupDepartments
                                                      orderby a.DepartmentName
                                                      select a).ToList<LookupDepartment>();
            };
            cboDepartments.SelectedValue = DepartmentID;
        }
        #endregion

        private void frmAddTrainingDepartmentCourseEnrollmentTypeModules_Load(object sender, EventArgs e)
        {
            this.populateDepartments();
            this.populateAvailableModules();
            this.populateLinkedModules();



        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        Module mod = new Module()
                        {
                            ModuleName = txtModuleBindingNavigator.Text.ToString(),
                            DepartmentID = Convert.ToInt32(cboDepartments.SelectedValue)
                        };
                        Dbconnection.Modules.Add(mod);
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

           
            this.txtModuleBindingNavigator.Text = "";
            this.populateAvailableModules();
        }

        private void btnFilterModule_Click(object sender, EventArgs e)
        {
            this.populateAvailableModules();
        }

        private void tsbtnRefresModuleSearch_Click(object sender, EventArgs e)
        {
            this.resetModuleSearchFilter();
        }
        private void resetModuleSearchFilter()
        {
            this.txtModuleFilterCriteria.Text = "";
            this.populateAvailableModules();
        }

        private void cboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.resetModuleSearchFilter();
            this.populateAvailableModules();
        }


    }
}


