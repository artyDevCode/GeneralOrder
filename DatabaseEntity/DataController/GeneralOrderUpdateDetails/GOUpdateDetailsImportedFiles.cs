using ApplicationLogger;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseEntity
{
    public class ActAdminModel
    {
        public int portfolioId { get; set; }
        public int? ildNumber { get; set; }
        public string comments { get; set; }
    }
    public class GOUpdateDetailsImportedFiles
    {
        /// <summary>
        /// Delete comments using an Act ID
        /// </summary>
        /// <param name="actAdminId">The ID of the Act</param>
        /// <returns>Return true of false based on success</returns>
        public static bool GOUpdateActAdminCommentsDelete(int actAdminId)
        {
            // res is the record sent that has all the information required to save to the database. Currently, the record is removed from the database.
            var actAdminComment = new List<ActAdminComment>();

            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    actAdminComment = dbConnect.ActAdminComments.Where(r => r.ActAdministrationID == actAdminId).ToList();
                    if (actAdminComment != null)
                    {
                        foreach (var res in actAdminComment)
                        {
                            dbConnect.Entry(res).State = EntityState.Deleted;
                            dbConnect.SaveChanges();
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GOUpdateDetailsImportedFiles.GOUpdateActAdminCommentsDelete -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GOUpdateDetailsImportedFiles.GOUpdateActAdminCommentsDelete",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes all pending records from the database.
        /// </summary>
        /// <param name="res">The actAdministrationID</param>
        /// <returns></returns>
        public static bool GODeletePendingActs(int res)
        {
            // res is the record sent that has all the information required to save to the database. Currently, the record is removed from the database.
            var actAdministration = new ActAdministration();
            var actAdminComment = new List<ActAdminComment>();

            using (var dbConnect = new EntityDBM())
            {
                using (var dbContextTransaction = dbConnect.Database.BeginTransaction())
                {
                    try
                    {
                        actAdministration = dbConnect.ActAdministrations.Where(r => r.ActAdministrationID == res).FirstOrDefault();
                        if (actAdministration != null)
                        {

                            dbConnect.Entry(actAdministration).State = EntityState.Deleted;
                            // dbConnect.SaveChanges(); //Save the changes of the details   
                        }

                        //Comments must be deleted in this seperate request. Wont work with just calling the ActAdministration record
                        actAdminComment = dbConnect.ActAdminComments.Where(r => r.ActAdministrationID == res).ToList();
                        if (actAdminComment != null)
                        {
                            foreach (var resultActComment in actAdminComment)
                            {
                                dbConnect.Entry(resultActComment).State = EntityState.Deleted;
                                // dbConnect.SaveChanges(); //Save the changes of the details   
                            }
                        }
                        dbConnect.SaveChanges(); //Save the changes of the details   
                        dbContextTransaction.Commit();

                        return true;
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GOUpdateDetailsImportedFiles.GOUpdateActAdminCommentsDelete -- {e.Message} -- Contact IT support.");
                        WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                        {
                            AdditionalUserInfo = "Exception",
                            LogDetail = e.Message,
                            LogDetail_Additional = "GOUpdateDetailsImportedFiles.GOUpdateActAdminCommentsDelete",
                            LogTime = DateTime.Now,
                            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                            Workstation = System.Environment.MachineName
                        }, "GODatabaseEntity", false);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// When user ckick back and save, saves a new ActAdministration record or updates an existing one
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static int GOUpdatePartialRecordAct(GeneralFieldsViewlModel res)
        {
            var actAdministration = new ActAdministration();
            var actAdminComment = new ActAdminComment();
            using (var dbConnect = new EntityDBM())
            {
                try
                {

                    //Find if the record already exists in the Database
                    actAdministration = dbConnect.ActAdministrations.Where(r => r.ActAdministrationID == res.actAdministrationID).FirstOrDefault();
                    if (actAdministration != null)
                    {
                        actAdministration.IsCurrent = res.isCurrent;
                        actAdministration.OnHold = res.onHold;
                        actAdministration.IsExcept = res.isExcept;
                        actAdministration.StartDate = res.startDate;
                        actAdministration.EndDate = res.endDate;
                        actAdministration.PendingEditType = res.pendingEditType; //0
                        actAdministration.PendingEditID = res.pendingEditID;
                        actAdministration.DepartmentPortfolioID = res.departmentPortfolioID;
                        actAdministration.ILDNumber = res.ildNumber;
                        dbConnect.Entry(actAdministration).State = EntityState.Modified;
                        dbConnect.SaveChanges(); //Save the changes of the details   


                    }
                    else
                    {   //It does not exists in the DB, so create a new one
                        actAdministration = dbConnect.ActAdministrations.Add(new ActAdministration
                        {
                            IsCurrent = res.isCurrent,
                            IsExcept = res.isExcept,
                            StartDate = res.startDate,
                            EndDate = res.endDate,
                            PendingEditID = res.pendingEditID,
                            OnHold = res.onHold,
                            PendingEditType = res.pendingEditType,
                            DepartmentPortfolioID = res.departmentPortfolioID,
                            ILDNumber = res.ildNumber,

                        });
                        dbConnect.SaveChanges(); //Save the changes of the details   

                    }
                    //return the new or existsing ID
                    return actAdministration.ActAdministrationID;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured --   GOUpdateDetailsImportedFiles.GOUpdatePartialRecordAct -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GOUpdateDetailsImportedFiles.GOUpdatePartialRecordAct",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return 0;
                }
            }
        }

        /// <summary>
        /// This method allows for the UI GOActAdminAdd and GOActAdminDetailsUpdate to update the record in the DB (before it was just managed in memory)
        /// </summary>
        /// <param name="result"></param>
        public static int GOUpdateFullRecordAct(GeneralFieldsViewlModel res)
        {
            //result represents a deserialized object of JSON
            var actAdministration = new ActAdministration();
            var actAdminComment = new ActAdminComment();
            using (var dbConnect = new EntityDBM())
            {
                try
                {


                    actAdministration = dbConnect.ActAdministrations.Where(r => r.ActAdministrationID == res.actAdministrationID).FirstOrDefault();
                    if (actAdministration != null)
                    {
                        actAdministration.IsCurrent = res.isCurrent; 
                        actAdministration.OnHold = res.onHold;
                        actAdministration.IsExcept = res.isExcept;
                        actAdministration.StartDate = res.startDate;
                        actAdministration.EndDate = res.endDate;
                        actAdministration.PendingEditType = res.pendingEditType; //0
                        actAdministration.PendingEditID = res.pendingEditID;
                        actAdministration.DepartmentPortfolioID = res.departmentPortfolioID;
                        actAdministration.ILDNumber = res.ildNumber;
                        dbConnect.Entry(actAdministration).State = EntityState.Modified;
                        dbConnect.SaveChanges(); //Save the changes of the details   


                    }
                    else
                    {
                        actAdministration = dbConnect.ActAdministrations.Add(new ActAdministration
                        {
                            //  ActAdministrationID = res.actAdministrationID,   DB will a
                            IsCurrent = res.isCurrent,
                            IsExcept = res.isExcept,
                            StartDate = res.startDate,
                            EndDate = res.endDate,
                            PendingEditID = res.pendingEditID,
                            OnHold = res.onHold,
                            PendingEditType = res.pendingEditType,
                            DepartmentPortfolioID = res.departmentPortfolioID,
                            ILDNumber = res.ildNumber,

                        });
                        dbConnect.SaveChanges(); //Save the changes of the details   

                    }


                    //Delete the comments before adding the new ones
                    if (res.generalFieldsCommentViewModel.Count > 0)
                    {
                        // var comm = resultActTitle.genOrderActComment.Where(r => r.generalOrderActTitleModelID == resultActTitle.generalOrderActTitleModelID).ToList();
                        var comm = dbConnect.ActAdminComments.Where(r => r.ActAdministrationID == res.actAdministrationID).ToList();
                        if (comm.Count > 0)
                        {
                            dbConnect.ActAdminComments.RemoveRange(comm);
                            dbConnect.SaveChanges();
                        }
                    }
                    foreach (var resultActComment in res.generalFieldsCommentViewModel)
                    {
                        if (resultActComment.actAdministrationComment != string.Empty)
                        {
                            actAdminComment = dbConnect.ActAdminComments.Add(new ActAdminComment
                            {
                                ActAdminComment1 = resultActComment.actAdministrationComment,
                                ActAdministrationID = actAdministration.ActAdministrationID, //the ID is automatically passed to this var when the database saves a new record
                                AlignmentType = resultActComment.alignmentType,
                                BulletASCII = resultActComment.bulletASCII,
                                FontBold = resultActComment.fontBold,
                                BulletChar = resultActComment.bulletChar,
                                IndentationBy = resultActComment.indentationBy,
                                IndentationLeft = resultActComment.indentationLeft,
                                IndentationRight = resultActComment.indentationRight,
                                ListSymbolFont = resultActComment.listSymbolFont,
                                PageBreakBefore = resultActComment.pageBreakBefore,
                                TabHangingIndent = resultActComment.tabHangingIndent,
                                TabStopPosition = resultActComment.tabStopPosition
                            });
                            dbConnect.SaveChanges(); //Save the changes of the details   
                        }
                    }
                    //Return the new or existsing ID
                    return actAdministration.ActAdministrationID;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured --  GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return 0;
                }
            }
        }



        /// <summary>
        /// Updates / Inserts Pending Acts. 
        /// </summary>
        /// <param name="adminListBox"></param>
        // public static void GenOrderActAdminUpdateFiles<T>(T currentAdminListBox) where T : ObservableCollection<GeneralFieldsViewlModel> //, IDisposable
        public static void GOActAdminUpdatePendingFiles(List<ActAdministrationViewModel> adminListBox)  //, IDisposable
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    foreach (var res in adminListBox)
                    {

                        var actAdministration = new ActAdministration();

                        if (res.ActAdministrationID != 0) //If its an existing DB record
                            actAdministration = dbConnect.ActAdministrations.Where(r => r.ActAdministrationID == res.ActAdministrationID).FirstOrDefault(); //existing record from the DB 
                        else  //If its an ADDED new record based of a CURRENT record
                            actAdministration = dbConnect.ActAdministrations.Where(r => r.ActAdministrationID == res.PendingEditID).FirstOrDefault(); //new record created with back and save using the pending ID
                        //If a record has been flagged for DELETION, it will skip the ActAdministrationNonCurrentImport and ActAdminCommentImport
                        if (res.PendingEditType != (short)RecordPendingEditType.Delete)
                        {
                            //Send the record to ActAdministrationNonCurrentImport to process


                            //If there is a record in current list, there will be one in the DB. Set the EndDate and isCurrent
                            if (actAdministration != null) //If the ActAdministrationID != null, that means that a record exists in the DB. Modify the record as Deleted and set an EndDate
                            {

                                actAdministration.IsCurrent = true;
                                actAdministration.PendingEditType = (short)RecordPendingEditType.None;
                                dbConnect.Entry(actAdministration).State = EntityState.Modified;
                                dbConnect.SaveChanges();

                            }

                            else
                            {

                                //Reset the variable
                                actAdministration = null;


                                actAdministration = dbConnect.ActAdministrations.Add(new ActAdministration
                                {
                                    ActAdministrationID = res.ActAdministrationID,
                                    IsCurrent = true, //res.isCurrent,
                                    IsExcept = res.IsExcept,
                                    StartDate = res.StartDate,
                                    EndDate = res.EndDate,
                                    PendingEditID = res.PendingEditID,
                                    OnHold = res.OnHold,
                                    PendingEditType = res.PendingEditType,
                                    DepartmentPortfolioID = res.PortfolioID,
                                    ILDNumber = res.ILDNumber,
                                    ActAdminComments = res.ActCommentModel.Select(r => new ActAdminComment
                                    {
                                        ActAdminComment1 = r.ActAdministrationComment,
                                        ActAdministrationID = res.ActAdministrationID,
                                        AlignmentType = r.AlignmentType,
                                        BulletASCII = r.BulletASCII,
                                        FontBold = r.FontBold,
                                        BulletChar = r.BulletChar,
                                        IndentationBy = r.IndentationBy,
                                        IndentationLeft = r.IndentationLeft,
                                        IndentationRight = r.IndentationRight,
                                        ListSymbolFont = r.ListSymbolFont,
                                        PageBreakBefore = r.PageBreakBefore,
                                        TabHangingIndent = r.TabHangingIndent,
                                        TabStopPosition = r.TabStopPosition
                                    }).ToList()
                                });
                                dbConnect.SaveChanges();

                            }
                        }
                        else
                        {
                            //Rec has been flagged as delete, so modify appropriate fields including EndDate with Date stamp
                            actAdministration.PendingEditType = (short)RecordPendingEditType.Delete;
                            actAdministration.PendingEditID = 0;
                            actAdministration.DepartmentPortfolioID = res.PortfolioID;
                            actAdministration.IsCurrent = false;
                            actAdministration.EndDate = DateTime.Now;
                            actAdministration.OnHold = false;
                            dbConnect.Entry(actAdministration).State = EntityState.Modified;
                            dbConnect.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured --  GOUpdateDetailsImportedFiles.GOActAdminUpdatePendingFiles -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GOUpdateDetailsImportedFiles.GOActAdminUpdatePendingFiles",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                }
            }
        }



        /// <summary>
        /// Recieves a list of records from the CURRENT list box but only searches for deleted records. When it finds a record that is Flagged as Delete, it will set the date in the EndDate field
        /// </summary>
        /// <param name="adminListBox"></param>
        public static async void GOActAdminDeleteCurrentFiles(List<ActAdministrationViewModel> adminListBox)  //, IDisposable
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    foreach (var res in adminListBox)
                    {
                        ActAdministration actAdministration = new ActAdministration();

                        //If record has the deleted flag
                        if (res.PendingEditType == (short)RecordPendingEditType.Delete && res.IsCurrent)
                        {
                            actAdministration = await dbConnect.ActAdministrations.Where(r => r.ActAdministrationID == res.ActAdministrationID).FirstOrDefaultAsync();
                            var pendingRec = adminListBox.Where(r => r.PendingEditID == res.ActAdministrationID).FirstOrDefault();

                            //If there is a record in current list, there will be one in the DB. Set the EndDate and isCurrent
                            if (actAdministration != null) 
                            {
                                actAdministration.PendingEditType = (short)RecordPendingEditType.Delete;
                                actAdministration.DepartmentPortfolioID = res.PortfolioID;
                                actAdministration.IsCurrent = false;
                                actAdministration.EndDate = pendingRec != null ? pendingRec.StartDate : DateTime.Now;        //Setting Date will prevent the UI from displaying this record again       
                                actAdministration.OnHold = false;
                                dbConnect.Entry(actAdministration).State = EntityState.Modified;
                                dbConnect.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured --  GOUpdateDetailsImportedFiles.GOActAdminDeleteCurrentFiles -- {e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GOUpdateDetailsImportedFiles.GOActAdminDeleteCurrentFiles",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                }
            }
        }
    }
}