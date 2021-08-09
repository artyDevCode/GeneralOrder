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
    public class GOImportFiles
    {
        /// <summary>
        /// goMeta represents the JSON string send via the client
        /// </summary>
        /// <param name="goMeta"></param>
        /// <returns></returns>
        public static bool GenOrderUpdateImportFiles(string goMeta, bool onHold)
        {
            //result represents a deserialized object of JSON
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralOrderMetadataViewModel>(goMeta);
            var actAdministration = new List<ActAdministration>();
            var actAdminComment = new ActAdminComment();
            var discardResult = false;
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    //if (result.orderfull)
                    //{
                    //    discardResult = ActAdministrationDC.DiscardPendingActAdministration();
                    //}
                    //else
                    //    discardResult = true; 

                    //If its a full General Order, then all Pending records must be deleted and current records that were set to PendingEditType = 'Delete' must be reset to PendingEditType = 'Delete'
                    discardResult = result.orderfull ? ActAdministrationDC.DiscardPendingActAdministration() : true; // need to set this to true always if the result.orderfull is PARTIAL, therefore no need to Discard pending

                    if (discardResult)
                    {
                        foreach (var resultGOModel in result.GOModel)
                        {
                            foreach (var resultActTitle in resultGOModel.genOrderActTitle)
                            {
                                var departmentPortfolioIDs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DepartmentViewModel>>(resultActTitle.DepartmentPortfolioIDs);

                                var res = new ActAdministration
                                {
                                    IsCurrent = false,
                                    OnHold = onHold,
                                    IsExcept = resultActTitle.isExcept, // resultActTitle.generalOrderActTitle.Contains("Except") ? true : false,
                                    StartDate = resultActTitle.EffectiveDate != null ? (DateTime)resultActTitle.EffectiveDate : DateTime.Now, // effDate,
                                    EndDate = null,
                                    PendingEditType = (short)RecordPendingEditType.None, //0
                                    DepartmentPortfolioID = departmentPortfolioIDs.Count > 0 ? Convert.ToInt32(departmentPortfolioIDs[0].DepartmentPortfolioID) : 0, //Get the department ID selected by the user in the UI
                                    ILDNumber = resultActTitle.ildNumber
                                };
                                //Add to DB

                                var actAdministrationId = ActAdministrationDC.ActAdministrationPendingImportCheckExistingCurrent(res);

                                if (resultActTitle.genOrderActComment.Count > 0)
                                {
                                    var comm = dbConnect.ActAdminComments.Where(r => r.ActAdministrationID == actAdministrationId).ToList();
                                    if (comm.Count > 0)
                                    {
                                        dbConnect.ActAdminComments.RemoveRange(comm);
                                        dbConnect.SaveChanges();
                                    }
                                }
                                foreach (var resultActComment in resultActTitle.genOrderActComment)
                                {
                                    actAdminComment = dbConnect.ActAdminComments.Add(new ActAdminComment
                                    {
                                        ActAdminComment1 = resultActComment.generalOrderActComment,
                                        ActAdministrationID = actAdministrationId,
                                        AlignmentType = resultActComment.generalOrderActCommentParagraphMeta.paragraphAlignment,
                                        BulletASCII = resultActComment.generalOrderActCommentParagraphMeta.bulletASCI,
                                        FontBold = resultActComment.generalOrderActCommentParagraphMeta.fontBold,
                                        BulletChar = resultActComment.generalOrderActCommentParagraphMeta.bulletChar,
                                        IndentationBy = resultActComment.generalOrderActCommentParagraphMeta.indentationBy,
                                        IndentationLeft = resultActComment.generalOrderActCommentParagraphMeta.IndentationLeft,
                                        IndentationRight = resultActComment.generalOrderActCommentParagraphMeta.IndentationRight,
                                        ListSymbolFont = resultActComment.generalOrderActCommentParagraphMeta.listSymbolFont,
                                        PageBreakBefore = resultActComment.generalOrderActCommentParagraphMeta.pageBreakBefore,
                                        TabHangingIndent = resultActComment.generalOrderActCommentParagraphMeta.TabHangingIndent,
                                        TabStopPosition = resultActComment.generalOrderActCommentParagraphMeta.tabStopPosition
                                    });

                                    dbConnect.SaveChanges(); //Save the changes of the details                          
                                }

                            }
                        }
                        return true;
                    }
                    else
                    {
                        MessageBoxHelper.CatchExceptionMessageBox("Error has occured -- GOImportFiles.GenOrderUpdateImportFiles() -- Contact IT support.");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GOImportFiles.GenOrderUpdateImportFiles -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GOImportFiles.GenOrderUpdateImportFiles",
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
