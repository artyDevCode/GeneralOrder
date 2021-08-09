using ApplicationLogger;
using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{

    public static class GetPortfolios
    {
        /// <summary>
        /// Gets a list of all current Portfolio names.
        /// </summary>
        /// <returns></returns>
        public static List<PortfolioViewModel> GetPortfolio()
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    List<PortfolioViewModel> res = new List<PortfolioViewModel>();



                    res = dbConnect.Portfolios
                        .Where(r => r.EndDate == null)
                        .Select(r => new PortfolioViewModel
                        {
                            PortfolioId = r.PortfolioID,
                            PortfolioName = r.PortfolioName
                        })
                    .OrderBy(r => r.PortfolioName)
                    .ToList();


                    res.Insert(0, new PortfolioViewModel
                    {
                        PortfolioId = 0,
                        PortfolioName = "-- Select Portfolio --"
                    });

                    return res;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetPortfolios.GetPortfolio -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetPortfolios.GetPortfolio",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new List<PortfolioViewModel>();
                }
            }
        }


        public static PortfolioViewModel GetPortfolioInfo(this string value)
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    PortfolioViewModel res = dbConnect.Portfolios.Select(r => new PortfolioViewModel
                    {
                        PortfolioId = r.PortfolioID,
                        PortfolioName = r.PortfolioName,
                        PrefixWithMinisterFor = r.PrefixWithMinisterFor
                    }).OrderBy(r => r.PortfolioName)
                    .FirstOrDefault(r => r.PortfolioName == value);

                    return res;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetPortfolios.GetPortfolioInfo -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetPortfolios.GetPortfolio",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new PortfolioViewModel();
                }
            }
        }

        //Returns a list PortfolioInfoViewModel model of portfolio and department values
        public static List<PortfolioInfoViewModel> GetPortfolioInformation(int portfolioID)
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    //using portfolio id
                    var resPortfolio = dbConnect.Portfolios
                        .GroupJoin(
                            dbConnect.DepartmentPortfolios,
                            goih => goih.PortfolioID,
                            gopm => gopm.PortfolioID,
                            (goih, gopm) => new
                            {
                                goih,
                                gopm1 = gopm.GroupJoin(dbConnect.Departments,
                                  p1 => p1.DepartmentID,
                                  p2 => p2.DepartmentID,
                                  (p1, p2) => new PortfolioInfoViewModel
                                  {
                                      DepartmentId = p1.Department.DepartmentID,
                                      Department = p1.Department.DepartmentName,
                                      PrefixWithMinisterFor = p1.Portfolio.PrefixWithMinisterFor,
                                      DeptCode = p1.Department.DepCode,
                                      PortfolioId = p1.PortfolioID,
                                      DepartmentPortfolioId = p1.DepartmentPortfolioID,
                                      PortfolioName = p1.Portfolio.PortfolioName
                                  })
                            }).Select(r => r.gopm1.Where(e => e.PortfolioId == portfolioID)).ToList();
                    var res = resPortfolio.FirstOrDefault(d => d.Count() > 0);
                    if (res != null)
                        return res.ToList();
                    else
                        return new List<PortfolioInfoViewModel>();

                  //  return resPortfolio.FirstOrDefault(d => d.Count() > 0).ToList();
                  
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error -- GetPortfolios.GetPortfolioInformation -- { e.Message} --  PotfolioID({portfolioID}). -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetPortfolios.GetPortfolio",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new List<PortfolioInfoViewModel>();
                }
            }
        }
    }
}
