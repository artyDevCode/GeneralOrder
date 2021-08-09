using ApplicationLogger;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    /// <summary>
    /// Enumeration of the screen IDs
    /// </summary>
    public enum ApplicationPage
    {
        goImportFile = 1,
        goFileDetailsUpload = 2,
        goDepartmentForAct = 3,
        goActAdminAdd = 4,
        goActAdminDetailsUpdate = 5,
        goActAdminIndividualDetails = 6
    }

    public enum RecordPendingEditType
    {
        None = 0,
        Add = 1,
       // Modify = 2,
        Delete = 3
    }


   
    public static class GetHelpInformation
    {
        /// <summary>
        /// Gets all the records from the helper table in the DB using the screen ID
        /// </summary>
        /// <param name="screenId">Id of the screen in view</param>
        /// <returns>return a ScreenHelpViewModel object containing all the information about the current screen</returns>
        public static ScreenHelpViewModel GetHelpInfo(this ApplicationPage screenId)
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    var res = dbConnect.ScreenHelps
                        .Select(r => new ScreenHelpViewModel
                        {
                            ScreenHelpID = r.ScreenHelpID,
                            ScreenName = r.ScreenName,
                            ScreenHelpText = r.ScreenHelpText,
                            ControlHelperVM = r.ControlHelps.Select(e => new ControlHelperViewModel
                            {
                                ControlHelpText = e.ControlHelpText,
                                ControlName = e.ControlName
                            }).ToList()
                        })
                        .Where(r => r.ScreenHelpID == (int)screenId).FirstOrDefault();
                       

                    return res;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetHelpInformation.GetHelpInfo -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetHelpInformation.GetHelpInfo",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new ScreenHelpViewModel();
                }
            }
        }
    }
}
