using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    public class GeneralOrderPortfolioModelDC
    {
        /// <summary>
        /// Insert Portfolio Data into DB. Send the ImportHeaderID and the res.goModel
        /// </summary>
        /// <param name="res"></param>
        /// <param name="ImportHeaderID"></param>
        /// <returns></returns>
        public static int GenOrderPortfolioImport(GeneralOrderPortfolioViewModel res, int ImportHeaderID)
        {                          
            var generalOrderPortfolioModels = new GeneralOrderPortfolioModel();
            //Coonect to DB
            using (var dbConnect = new EntityDBM())
            {
                //General Order Portfolio Model
                generalOrderPortfolioModels =  dbConnect.GeneralOrderPortfolioModels
                    .Where(r => r.GeneralOrderPortfolioModelID == res.generalOrderPortfolioModelID && r.GeneralOrderImportHeaderID == ImportHeaderID)
                    .FirstOrDefault();

                //Examples
                //Act
               // generalOrderActModels = dbConnect.GeneralOrderActModels.Where(r => r.GeneralOrderPortfolioModelID == portfolioID && r.GeneralOrderActModelID == res.generalOrderActTitleID).FirstOrDefault();


                //Comments
                //  generalOrderActCommentModels = dbConnect.GeneralOrderActCommentModels
                //     .Where(r => r.GeneralOrderActCommentModelID == res.generalOrderActCommentID && r.GeneralOrderActModelID == actTitleID)
                //     .FirstOrDefault();
                //End Examples

                

                int paraID = 0;
                //Modify existing or insert portfolio data into DB including the Paragraph Meta and ImportHeaderID
                if (generalOrderPortfolioModels != null)
                {
                    //Insert Metadata of the portfolio paragraph and get ID
                    paraID = ParagraphModelDC.GenOrderParagraphImport(res.generalOrderPortfolioParagraphMeta, "GeneralOrderPortfolio", generalOrderPortfolioModels.ParagraphModelID);

                    generalOrderPortfolioModels.GeneralOrderPortfolioName = res.generalOrderPortfolioName;
                    generalOrderPortfolioModels.GeneralOrderPortfolioID = res.generalOrderPortfolioID; 
                    generalOrderPortfolioModels.ParagraphModelID = paraID; 
                    generalOrderPortfolioModels.GeneralOrderImportHeaderID = ImportHeaderID;
                    dbConnect.Entry(generalOrderPortfolioModels).State = EntityState.Modified;

                }
                else
                {
                    paraID = ParagraphModelDC.GenOrderParagraphImport(res.generalOrderPortfolioParagraphMeta, "GeneralOrderPortfolio", 0);

                    //Create a new record in DB
                    generalOrderPortfolioModels = dbConnect.GeneralOrderPortfolioModels.Add(new GeneralOrderPortfolioModel
                    {
                        GeneralOrderImportHeaderID = ImportHeaderID,  
                        GeneralOrderPortfolioName = res.generalOrderPortfolioName,
                        ParagraphModelID = paraID,
                        GeneralOrderPortfolioID = res.generalOrderPortfolioID
                    });
                }

                dbConnect.SaveChanges(); //Save the changes of the details
                //TEST
              //  var testResult1 = dbConnect.GeneralOrderPortfolioModels.Select(r => r.GeneralOrderPortfolioName);
                //End TEST
                //Return the ID
                
            }
            return generalOrderPortfolioModels.GeneralOrderPortfolioModelID;
        }
    }
}
