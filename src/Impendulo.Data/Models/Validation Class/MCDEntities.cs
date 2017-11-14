using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace Impendulo.Data.Models
{


    public partial class MCDEntities : DbContext
    {

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
        {
            return base.ShouldValidateEntity(entityEntry);
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {

            #region Student details
            if (entityEntry.Entity is Student)
            {

                //if (entityEntry.CurrentValues.GetValue<string>("StudentIDNumber").Length == 0)
                //{
                //    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                //    list.Add(new System.Data.Entity.Validation.DbValidationError("Student IDNumber - Field", "(ID Number Invalid) - ID Number Can Not Be Blank."));

                //    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                //}

                //if (entityEntry.CurrentValues.GetValue<string>("StudentIDNumber").Length != 13)
                //{
                //    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                //    list.Add(new System.Data.Entity.Validation.DbValidationError("StudentIDNumber", "(ID Number Invalid) - ID Number Length Must Be 13 Charaters In Length"));

                //    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                //}

                if (entityEntry.CurrentValues.GetValue<int>("EthnicityID").Equals(6))
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("EthnicityID", "Ethnicity is not specified"));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }

                if (entityEntry.CurrentValues.GetValue<int>("QualificationLevelID").Equals(11))
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("QualificationLevelID", "Qualification Level is not specified"));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion

            #region Individual Student details
            if (entityEntry.Entity is Individual)
            {
                if (entityEntry.CurrentValues.GetValue<string>("IndividualFirstName") == "")
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("IndividualFirstName", "Student FirstName - Is a Required Field"));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }

                if (entityEntry.CurrentValues.GetValue<string>("IndividualLastname") == "")
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("IndividualLastname", "Student Lastname - Is a Required Field"));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion

            #region Student Address details
            if (entityEntry.Entity is Address)
            {
                if (entityEntry.CurrentValues.GetValue<string>("AddressLineOne").Length == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("AddressLineOne", "Address LineOne - Is a Required Field"));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }

                if (entityEntry.CurrentValues.GetValue<string>("AddressAreaCode").Length == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("AddressAreaCode", "Address AreaCode - Is a Required Field"));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion

            #region Course details
            if (entityEntry.Entity is Course)
            {
                if (entityEntry.CurrentValues.GetValue<string>("CourseName").Length == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("Course Name", "(Course Name Invalid) - Course Name Can Not Be Blank."));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion

            #region CurriculumCourse details
            if (entityEntry.Entity is CurriculumCourse)
            {
                if ((entityEntry.CurrentValues.GetValue<decimal>("Cost").ToString()).Length == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("Course Cost", "(Cost Invalid) - Cost Can Not Be Empty."));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }

                if (entityEntry.CurrentValues.GetValue<decimal>("Cost") == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("Course Cost", "(Cost Invalid) - Cost Can Not Be Zero."));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion

            #region Venue Details
            if (entityEntry.Entity is Venue)
            {
                if ((entityEntry.CurrentValues.GetValue<string>("VenueName").ToString()).Length == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("Venue Name", "(Venue Name Invalid) -  Name Can Not Be Empty."));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion

            #region Addresses
            if (entityEntry.Entity is Address)
            {
                if ((entityEntry.CurrentValues.GetValue<string>("AddressDescription").ToString()).Length == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("Address Description", "(Description Invalid) -  Name Can Not Be Blank."));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion

            #region Modules
            if (entityEntry.Entity is Module)
            {
                if ((entityEntry.CurrentValues.GetValue<string>("ModuleName").ToString()).Length == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("Module Name", "(Module Name Invalid) -  Module Name Can Not Be Blank."));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion
            #region Companies
            if (entityEntry.Entity is Company)
            {
                if ((entityEntry.CurrentValues.GetValue<string>("CompanyName").ToString()).Length == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("Company Name", "(Company Name Invalid) -  Company Name Can Not Be Blank."));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion

            #region Activities
            if (entityEntry.Entity is Activity)
            {
                if ((entityEntry.CurrentValues.GetValue<string>("ActivityCode").ToString()).Length == 0)
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();
                    list.Add(new System.Data.Entity.Validation.DbValidationError("Activity Code", "(Activity Code Invalid) -  Activity Code Can Not Be Blank."));

                    return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, list);
                }
            }
            #endregion

            return base.ValidateEntity(entityEntry, items);
        }
    }
}
