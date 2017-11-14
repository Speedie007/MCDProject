using Impendulo.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Common.Verifiction
{
    public class OfProgressFiles
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>CompanyProgressFileID</returns>
        public static int VerifyCompanyProgressFile(int CompanyID)
        {
            using (var Dbconnection = new MCDEntities())
            {
                CompanyProgressFile CPF = Dbconnection.CompanyProgressFiles.Where(a => a.CompanyID == CompanyID).FirstOrDefault<CompanyProgressFile>();
                if (CPF == null)
                {
                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            //CRUD Operations
                            ProgressFile newProgressFile = new ProgressFile()
                            {
                                DateCreated = DateTime.Now,
                                ProgressFileTypeID = (int)Common.Enum.EnumProgessFileTypes.Company,
                                CompanyProgressFile = new CompanyProgressFile()
                                {
                                    CompanyID = CompanyID,
                                    DateLastUpdated = DateTime.Now
                                }
                            };
                            Dbconnection.ProgressFiles.Add(newProgressFile);

                            ////saves all above operations within one transaction
                            Dbconnection.SaveChanges();

                            //commit transaction
                            dbTran.Commit();
                            return newProgressFile.CompanyProgressFile.CompanyProgressFileID;
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
                }
                return CPF.CompanyProgressFileID;
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns>StudentProgressFileID</returns>
        public static int VerifyStudentProgressFile(int StudentID)
        {
            using (var Dbconnection = new MCDEntities())
            {
                StudentProgressFile SPF = Dbconnection.StudentProgressFiles.Where(a => a.StudentID == StudentID).FirstOrDefault<StudentProgressFile>();
                if (SPF == null)
                {
                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            //CRUD Operations
                            ProgressFile newProgressFile = new ProgressFile()
                            {
                                DateCreated = DateTime.Now,
                                ProgressFileTypeID = (int)Common.Enum.EnumProgessFileTypes.Company,
                                StudentProgressFile = new StudentProgressFile()
                                {
                                    StudentID = StudentID,
                                    DateLastUpdated = DateTime.Now
                                }
                            };
                            Dbconnection.ProgressFiles.Add(newProgressFile);

                            ////saves all above operations within one transaction
                            Dbconnection.SaveChanges();

                            //commit transaction
                            dbTran.Commit();
                            return newProgressFile.StudentProgressFile.StudentProgressFileID;
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
                }
                return SPF.StudentProgressFileID;
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="CompanyID"></param>
        /// <returns>CompanyStudentProgressFileID</returns>
        public static int VerifyCompanyStudentProgressFile(int StudentID, int CompanyID)
        {
            int _StudentProgressFileID = VerifyStudentProgressFile(StudentID);
            int _CompanyProgressFileID = VerifyCompanyProgressFile(CompanyID);
            using (var Dbconnection = new MCDEntities())
            {
                CompanyStudentProgressFile CSPF = Dbconnection.CompanyStudentProgressFiles.Where(a => a.StudentProgressFileID == _StudentProgressFileID && a.CompanyProgressFileID == _CompanyProgressFileID).FirstOrDefault<CompanyStudentProgressFile>();
                if (CSPF == null)
                {
                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            //CRUD Operations
                            CompanyStudentProgressFile newCompanyStudentProgressFile = new CompanyStudentProgressFile()
                            {
                                StudentProgressFileID = _StudentProgressFileID,
                                CompanyProgressFileID = _CompanyProgressFileID
                            };
                            Dbconnection.CompanyStudentProgressFiles.Add(newCompanyStudentProgressFile);

                            ////saves all above operations within one transaction
                            Dbconnection.SaveChanges();

                            //commit transaction
                            dbTran.Commit();
                            return newCompanyStudentProgressFile.CompanyStudentProgressFileID;
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
                }
                return CSPF.CompanyStudentProgressFileID;
            };
        }
    }
}
