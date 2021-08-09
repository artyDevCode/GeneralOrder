using ApplicationLogger;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    public class GetDepartments
    {
        /// <summary>
        /// Get a list of department for the dropdown
        /// </summary>
        /// <returns></returns>
        public static List<DepartmentViewModel> GetDepartmentList()
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    List<DepartmentViewModel> res =  dbConnect.Departments
                        .Select(r => new DepartmentViewModel
                        {
                            Department = r.DepartmentName,
                            DepartmentId = r.DepartmentID
                        })
                        .OrderBy(r => r.Department)
                        .ToList();

                    return res;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetDepartments.GetDepartmentList -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetDepartments.GetDepartmentList",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new List<DepartmentViewModel>(); //empty list
                }
            }
        }

        /// <summary>
        /// Get the department information using the departmentPortfolioID
        /// </summary>
        /// <param name="departmentPortfolioID"></param>
        /// <returns></returns>
        public static List<PortfolioInfoViewModel> GetDepartmentInformation(int departmentPortfolioID)
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    //using portfolio id and Transperent Identifier to create an object
                    var resDepartment =  dbConnect.Portfolios
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
                                  // (p1, p2) => new { p1, p2 })
                                  (p1, p2) => new PortfolioInfoViewModel
                                  {
                                      DepartmentId = p1.Department.DepartmentID,
                                      Department = p1.Department.DepartmentName,
                                      PrefixWithMinisterFor = p1.Portfolio.PrefixWithMinisterFor,
                                      DeptCode = p1.Department.DepCode,
                                      PortfolioId = p1.PortfolioID,
                                      PortfolioName = p1.Portfolio.PortfolioName,
                                      DepartmentPortfolioId = p1.DepartmentPortfolioID
                                  })
                            })
                            .Select(r => r.gopm1.Where(e => e.DepartmentPortfolioId == departmentPortfolioID))
                            .FirstOrDefault(d => d.Count() > 0);

                    return resDepartment.ToList();
;
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetDepartments.GetDepartmentInformation -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetDepartments.GetDepartmentInformation",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new List<PortfolioInfoViewModel>(); //empty list
                }
            }
        }
    }
}
