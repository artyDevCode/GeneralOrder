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
    public class GOActCommentModelDC
    {

        /// <summary>
        /// Insert Act Comment data into DB
        /// </summary>
        /// <param name="res"></param>
        /// <param name="actTitleID"></param>
        /// <returns></returns>
        public static int GOCommentImportDel(List<GeneralOrderActCommentViewModel> result, int actTitleID)
        {

            var generalOrderActCommentModelsList = new List<GeneralOrderActCommentModel>();
            var generalOrderActCommentModels = new GeneralOrderActCommentModel();
            using (var dbConnect = new EntityDBM())
            {
                using (var dbContextTransaction = dbConnect.Database.BeginTransaction())
                {
                    try
                    {
                        generalOrderActCommentModelsList = dbConnect.GeneralOrderActCommentModels
                      .Where(r => r.GeneralOrderActModelID == actTitleID)
                      .ToList();

                        int paraID = 0;

                        //Delete the flow doc Comments
                        if (generalOrderActCommentModelsList != null)
                        {
                            dbConnect.GeneralOrderActCommentModels.RemoveRange(generalOrderActCommentModelsList);
                            //dbConnect.Entry(generalOrderActCommentModelsList).State = EntityState.Deleted;
                            dbConnect.SaveChanges(); //Save the changes of the details
                        }

                        foreach (var res in result)
                        {
                            paraID = ParagraphModelDC.GenOrderParagraphImport(res.generalOrderActCommentParagraphMeta, "GeneralOrderActComment", 0);

                            //Add the new flowDoc comments
                            generalOrderActCommentModels = dbConnect.GeneralOrderActCommentModels.Add(new GeneralOrderActCommentModel
                            {
                                GeneralOrderActComment = res.generalOrderActComment,
                                ParagraphModelID = paraID,
                                GeneralOrderActModelID = actTitleID
                            });
                            generalOrderActCommentModelsList.Add(generalOrderActCommentModels);
                        }
                        dbConnect.SaveChanges(); //Save the changes of the details
                        dbContextTransaction.Commit();

                        return generalOrderActCommentModels.GeneralOrderActCommentModelID;
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GOActCommentModelDC.GOCommentImportDel -- { e.Message} -- Contact IT support.");
                        WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                        {
                            AdditionalUserInfo = "Exception",
                            LogDetail = e.Message,
                            LogDetail_Additional = "GOActCommentModelDC.GOCommentImportDel",
                            LogTime = DateTime.Now,
                            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                            Workstation = System.Environment.MachineName
                        }, "GODatabaseEntity", false);
                        return 0;
                    }
                }
                
            }
        }

        /// <summary>
        /// Insert Act Comment data into DB
        /// </summary>
        /// <param name="res"></param>
        /// <param name="actTitleID"></param>
        /// <returns></returns>
        public static int GOCommentImport(GeneralOrderActCommentViewModel res, int actTitleID)
        {

            var generalOrderActCommentModels = new GeneralOrderActCommentModel();

            using (var dbConnect = new EntityDBM())
            {

               // Find if theres already a record in the DB
                generalOrderActCommentModels = dbConnect.GeneralOrderActCommentModels
                    .Where(r => r.GeneralOrderActCommentModelID == res.generalOrderActCommentModelID && r.GeneralOrderActModelID == actTitleID)
                    .FirstOrDefault();

                int paraID = 0;

                if (generalOrderActCommentModels != null)
                {
                    //Insert/modify paragraph style 
                    paraID = ParagraphModelDC.GenOrderParagraphImport(res.generalOrderActCommentParagraphMeta, "GeneralOrderActComment", generalOrderActCommentModels.ParagraphModelID);

                    generalOrderActCommentModels.GeneralOrderActComment = res.generalOrderActComment;
                    generalOrderActCommentModels.ParagraphModelID = paraID; 
                    generalOrderActCommentModels.GeneralOrderActModelID = actTitleID;
                    dbConnect.Entry(generalOrderActCommentModels).State = EntityState.Deleted;
                }
                else
                {
                    paraID = ParagraphModelDC.GenOrderParagraphImport(res.generalOrderActCommentParagraphMeta, "GeneralOrderActComment", 0);

                    generalOrderActCommentModels = dbConnect.GeneralOrderActCommentModels.Add(new GeneralOrderActCommentModel
                    {
                        GeneralOrderActComment = res.generalOrderActComment,
                        ParagraphModelID = paraID,
                        GeneralOrderActModelID = actTitleID
                    });
                }

                dbConnect.SaveChanges(); //Save the changes of the details
                return generalOrderActCommentModels.GeneralOrderActCommentModelID;
            }
        }
    }
}
