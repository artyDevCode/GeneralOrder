using ApplicationLogger;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    public static class GetActTitles
    {
        /// <summary>
        /// Gets a list of all the current Act Titles for the drop down
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ActTitleViewModel> GetActTitle()
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    List<ActTitleViewModel> res = dbConnect.ILD_CurrentDocuments
                         .Where(e => e.CurrentStatus == "Current")
                        .Select(r => new ActTitleViewModel
                        {
                            ActTitleILDNumber = r.ILDNumber,
                            ActTitle = r.DocTitle,
                            ActNumber = r.ActNumber
                        })
                        .OrderBy(r => r.ActTitle)
                        .ToList();

                    return new ObservableCollection<ActTitleViewModel>(res);
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetActTitles.GetActTitle -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetActTitles.GetActTitle",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new ObservableCollection<ActTitleViewModel>(); //empty list
                }
            }
        }

        /// <summary>
        /// Get a list Acts using the ILD number from a list
        /// </summary>
        /// <param name="ildNumberList">List of ILD list</param>
        /// <returns></returns>
        public static List<ActTitleViewModel> GetActTitleFromExistingData(IEnumerable<GetGeneralOrder.ActILDList> ildNumberList)
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    List<ActTitleViewModel> res = dbConnect.ILD_CurrentDocuments
                         .Where(e => e.CurrentStatus == "Current")
                         .Select(r => new ActTitleViewModel
                         {
                             ActTitleILDNumber = r.ILDNumber,
                             ActTitle = r.DocTitle,
                             ActNumber = r.ActNumber
                         })
                        .ToList();

                    var result = ildNumberList
                      .Join(
                          res,
                          goih => goih.ildNumber,
                          gopm => gopm.ActTitleILDNumber,
                          (goih, gopm) => new ActTitleViewModel
                          {
                              ActTitleILDNumber = gopm.ActTitleILDNumber,
                              ActTitle = gopm.ActTitle,
                              ActNumber = gopm.ActNumber
                          })
                            .GroupBy(r => r.ActTitleILDNumber).Select(d => d.First())
                            .OrderBy(r => r.ActTitle)
                            .ToList();


                    //var result = res

                    //  .Join(
                    //      ildNumberList,
                    //      goih => goih.ActTitleILDNumber,
                    //      gopm => gopm.ildNumber,
                    //      (goih, gopm) => new ActTitleViewModel
                    //      {
                    //          ActTitleILDNumber = goih.ActTitleILDNumber,
                    //          ActTitle = goih.ActTitle,
                    //          ActNumber = goih.ActNumber
                    //      })
                    //        .OrderBy(r => r.ActTitle)                          
                    //        .ToList();

                    //.ToListAsync();


                    //var res = await dbConnect.ILD_CurrentDocuments
                    //  .GroupJoin(
                    //      ildNumberList,
                    //      goih => goih.ILDNumber,
                    //      gopm => gopm.ildNumber,
                    //      (goih, gopm) => new ActTitleViewModel
                    //      {
                    //          ActTitleILDNumber = goih.ILDNumber,
                    //          ActTitle = goih.DocTitle,
                    //          ActNumber = goih.ActNumber
                    //      })
                    //        .OrderBy(r => r.ActTitle)
                    //        .ToListAsync();



                    //foreach (var ildNumber in ildNumberList)
                    //{
                    //    List<ActTitleViewModel> res = await dbConnect.ILD_CurrentDocuments
                    //         .Where(e => e.CurrentStatus == "Current" && e.ILDNumber == ildNumber)
                    //        .Select(r => new ActTitleViewModel
                    //        {
                    //            ActTitleILDNumber = r.ILDNumber,
                    //            ActTitle = r.DocTitle,
                    //            ActNumber = r.ActNumber
                    //        })
                    //        .OrderBy(r => r.ActTitle)
                    //        .ToListAsync();
                    //}
                    return result;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetActTitles.GetActTitleFromExistingData -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetActTitles.GetActTitleFromExistingData",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new List<ActTitleViewModel>(); //empty list
                }
            }
        }

        /// <summary>
        /// Get the Act Object using the Act Title 
        /// </summary>
        /// <param name="value">Text value comments</param>
        /// <returns></returns>
        public static ActTitleViewModel GetActTitleInfo(this string value)
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    var res = dbConnect.ILD_CurrentDocuments.Select(r => new ActTitleViewModel
                    {
                        ActTitleILDNumber = r.ILDNumber,
                        ActTitle = r.DocTitle,
                        ActNumber = r.ActNumber
                    })
                    .FirstOrDefault(r => r.ActTitle == value);

                    return res;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetActTitles.GetActTitleInfo -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetActTitles.GetActTitleInfo",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new ActTitleViewModel(); //empty model
                }
            }
        }

        /// <summary>
        /// Get the Act information  using the ILD Number
        /// </summary>
        /// <param name="ildNumber">ILD number</param>
        /// <returns></returns>
        public static ActTitleViewModel GetActInformation(this int ildNumber)
        {
            //EntityDBM dbConnect = EntityDBM.Create("data source=ocpc-dev-sq01;initial catalog=ILD.NET;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            using (var dbConnect = new EntityDBM())
            {
                try
                {

                    var res = dbConnect.ILD_CurrentDocuments.Select(r => new ActTitleViewModel { ActTitleILDNumber = r.ILDNumber, ActTitle = r.DocTitle, ActNumber = r.ActNumber })
                        .Where(r => r.ActTitleILDNumber == ildNumber)
                        .FirstOrDefault();
                    return res;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetActTitles.GetActInformation -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetActTitles.GetActInformation",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new ActTitleViewModel();
                }
            }
        }


        /// <summary>
        /// Get the Act Object using the Act Title 
        /// </summary>
        /// <param name="value">Text value comments</param>
        /// <returns></returns>
        public static bool VerifyAppropriationSupplyAct(this string value)
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    var res = dbConnect.GeneralOrderGroupActs
                         .Where(e => e.GeneralOrderGroupActTitle == value).FirstOrDefault();

                    if (res != null)
                        return true;
                    else
                        return false;

                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetActTitles.VerifyAppropriationSupplyAct -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetActTitles.VerifyAppropriationSupplyAct",
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
