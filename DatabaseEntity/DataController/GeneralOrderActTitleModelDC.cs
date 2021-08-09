using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    public class GeneralOrderActTitleModelDC
    {
        /// <summary>
        /// Insert Act Title data into the DB based on a portfolio
        /// </summary>
        /// <param name="res"></param>
        /// <param name="portfolioID"></param>
        /// <returns></returns>
        public static int GenOrderActTitleImport(GeneralOrderActTitleViewModel res, int portfolioID, int portfolioId, DateTime GOFileInputDatePicker)
        {
            var generalOrderActModels = new GeneralOrderActModel();
            dynamic resPortfolioResult = null;
            using (var dbConnect = new EntityDBM())
            {
                //Check and see if theres an existing record based on Portfolio ID
                generalOrderActModels =  dbConnect.GeneralOrderActModels.Where(r => r.GeneralOrderPortfolioModelID == portfolioID && r.GeneralOrderActModelID == res.generalOrderActTitleModelID).FirstOrDefault();
                if (res.DepartmentPortfolioIDs == null)
                {
                    List<PortfolioInfoViewModel> port = GetPortfolios.GetPortfolioInformation(portfolioId); // GetDepartments.GetDepartmentInformation(departmentId);
                    resPortfolioResult = Newtonsoft.Json.JsonConvert.SerializeObject(port.Select(r => new DepartmentViewModel
                    {
                        DepartmentId = r.DepartmentId,
                        Department = r.Department,
                        DepartmentPortfolioID = r.DepartmentPortfolioId
                    }));
                }
                else
                    resPortfolioResult = res.DepartmentPortfolioIDs;
                int paraID = 0;
                //Modify or insert data into Act Title DB tabl
                if (generalOrderActModels != null)
                {
                    //Insert/Modify Paragraph Style
                    generalOrderActModels.EffectiveDate = res.EffectiveDate;
                    generalOrderActModels.ILDNumber = res.ildNumber;
                    generalOrderActModels.IsExcept = res.isExcept;
                    generalOrderActModels.GeneralOrderActTitle = res.generalOrderActTitle;
                    generalOrderActModels.DepartmentPortfolioIDs = resPortfolioResult;   
                    dbConnect.Entry(generalOrderActModels).State = EntityState.Modified;
                }
                else
                {
                    paraID =  ParagraphModelDC.GenOrderParagraphImport(res.generalOrderActTitleParagraphMeta, "GeneralOrderActTitle", 0);

                    generalOrderActModels = dbConnect.GeneralOrderActModels.Add(new GeneralOrderActModel
                    {
                        EffectiveDate = GOFileInputDatePicker, 
                        GeneralOrderActTitle = res.generalOrderActTitle,
                        ILDNumber = res.ildNumber, 
                        IsExcept = res.isExcept,
                        GeneralOrderPortfolioModelID = portfolioID,
                        ParagraphModelID = paraID,
                        DepartmentPortfolioIDs = resPortfolioResult

                    });
                }

                dbConnect.SaveChanges(); //Save the changes of the details
                                        
            }
            return generalOrderActModels.GeneralOrderActModelID;
        }

    }
}
