using ApplicationLogger;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseEntity
{
    public class ActAdministrationDC
    {


        public static int ActAdministrationPendingImportCheckExistingCurrent(ActAdministration res)
        {
            var actAdministrationCurrent = new ActAdministration();
            var actAdministrationPending = new ActAdministration();

            using (var dbConnect = new EntityDBM())
            {
                //Check to see if there is a current record
                actAdministrationCurrent = dbConnect.ActAdministrations.Where(r => r.DepartmentPortfolioID == res.DepartmentPortfolioID
               && r.IsCurrent == true && r.ILDNumber == res.ILDNumber && r.EndDate == null).FirstOrDefault(); //existing record from the DB 

                //If there is a record in current list, change it to deleted
                if (actAdministrationCurrent != null) //If the ActAdministrationID != null, that means that a record exists in the DB. Modify the record as Deleted and set an EndDate
                {
                    actAdministrationCurrent.PendingEditType = (short)RecordPendingEditType.Delete;
                    actAdministrationCurrent.PendingEditID = 0;
                    dbConnect.Entry(actAdministrationCurrent).State = EntityState.Modified;
                    dbConnect.SaveChanges(); //Save the changes of the details   
                }

                //Check if there is already and existing non current record
                actAdministrationPending = dbConnect.ActAdministrations.Where(r => r.DepartmentPortfolioID == res.DepartmentPortfolioID
               && r.IsCurrent == false && r.ILDNumber == res.ILDNumber && r.EndDate == null).FirstOrDefault(); //existing record from the DB 

                //If there is a non current record, insert EndDate value
                if (actAdministrationPending != null) //If the ActAdministrationID != null, that means that a record exists in the DB. Modify the record as Deleted and set an EndDate
                {
                    actAdministrationPending.PendingEditType = (short)RecordPendingEditType.Delete;
                    actAdministrationPending.PendingEditID = 0;
                    actAdministrationPending.EndDate = DateTime.Now;
                    dbConnect.Entry(actAdministrationPending).State = EntityState.Modified;
                    dbConnect.SaveChanges(); //Save the changes of the details   

                }

                // Create a new non current record with the act ID of the current record set as delete.Handle if actAdministrationCurrent == null
                actAdministrationPending = dbConnect.ActAdministrations.Add(new ActAdministration
                {
                    IsCurrent = res.IsCurrent,
                    IsExcept = res.IsExcept,
                    StartDate = res.StartDate,
                    PendingEditID = actAdministrationCurrent != null ? actAdministrationCurrent.ActAdministrationID : 0,
                    EndDate = null,
                    PendingEditType = (short)(actAdministrationCurrent != null ? RecordPendingEditType.Add : RecordPendingEditType.Add),
                    OnHold = res.OnHold,
                    DepartmentPortfolioID = res.DepartmentPortfolioID,
                    ILDNumber = res.ILDNumber
                });
                dbConnect.SaveChanges();


            }
            return actAdministrationPending.ActAdministrationID;
        }


        //public static void SetActAdministrationToPending()
        //{
        //    //This will remove all pending records from the pending list . This only occurs if a new Full General Order is uploaded into ActAdministration
        //    using (var dbConnect = new EntityDBM())
        //    {
        //        var actAdministration = dbConnect.ActAdministrations.Where(r => r.EndDate == null && r.IsCurrent == false).ToList(); //existing record from the DB 

        //        foreach (var result in actAdministration)
        //        {
        //            result.PendingEditType = (short)RecordPendingEditType.Delete;
        //            result.EndDate = DateTime.Now;
        //            dbConnect.SaveChanges();
        //        }

        //    }
        //}


        /// <summary>
        /// Delete the ActAdministration PENDING records
        /// </summary>
        /// <returns></returns>
        public static bool DiscardPendingActAdministration()
        {
            List<ActAdministration> actAdministrationList = new List<ActAdministration>();
            List<ActTitleViewModel> list = new List<ActTitleViewModel>();
            var actAdministrationPending = new ActAdministration();
            using (var dbConnect = new EntityDBM())
            {
                using (var dbContextTransaction = dbConnect.Database.BeginTransaction())
                {
                    try
                    {
                        actAdministrationList = dbConnect.ActAdministrations.Where(r => r.IsCurrent == false && r.EndDate == null).ToList(); //existing record from the DB 

                        foreach (ActAdministration actAdministrationCurrent in actAdministrationList)
                        {
                            //Comments must be deleted in this seperate request. Wont work with just calling the ActAdministration record
                            var actAdminComment = dbConnect.ActAdminComments.Where(r => r.ActAdministrationID == actAdministrationCurrent.ActAdministrationID).ToList();
                            if (actAdminComment != null)
                            {
                                foreach (var resultActComment in actAdminComment)
                                {
                                    dbConnect.Entry(resultActComment).State = EntityState.Deleted;
                                }
                            }

                            //Now delete the ActAdministration records
                            dbConnect.Entry(actAdministrationCurrent).State = EntityState.Deleted;                          
                        }

                        //Modify the existing current records to show PendingEditType = none, not delete if there were current records
                        actAdministrationList = dbConnect.ActAdministrations.Where(r => r.IsCurrent == true && r.EndDate == null && r.PendingEditType == (short)RecordPendingEditType.Delete).ToList(); //existing record from the DB 
                        foreach (ActAdministration actAdministrationCurrent in actAdministrationList)
                        {
                            //Comments must be deleted in this seperate request. Wont work with just calling the ActAdministration record
                            actAdministrationCurrent.PendingEditType = (short)RecordPendingEditType.None;
                            actAdministrationCurrent.PendingEditID = 0;
                            dbConnect.Entry(actAdministrationCurrent).State = EntityState.Modified;
                        }
                        // dbContextTransaction.Rollback(); //testing only
                        dbConnect.SaveChanges(); //Save the changes of the details   
                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- ActAdministrationDC.DiscardPendingActAdministration -- { e.Message} -- Contact IT support.");
                        WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                        {
                            AdditionalUserInfo = "Exception",
                            LogDetail = e.Message,
                            LogDetail_Additional = "ActAdministrationDC.DiscardPendingActAdministration",
                            LogTime = DateTime.Now,
                            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                            Workstation = System.Environment.MachineName
                        }, "GODatabaseEntity", false);
                        return false;
                    }
                }
            }
        }
    }


}

