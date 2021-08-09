using ApplicationLogger;
using GeneralOrderCore;
using System;
using System.Data.Entity;
using System.Linq;

namespace DatabaseEntity
{
    /// <summary>
    /// Class representing the whole document format, style
    /// </summary>
    public class GeneralOrderImportHeaderDC
    {

        public static void GenOrderProcessDate(int id)
        {
            //Coonect to the database
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    //Search for the record
                    var generalOrderImportHeaders = dbConnect.GeneralOrderImportHeaders.Where(r => r.GeneralOrderImportHeaderID == id).FirstOrDefault();

                    //Update the record if it exists, else create one
                    if (generalOrderImportHeaders != null)
                    {
                        generalOrderImportHeaders.GeneralOrderProcesseddatetime = DateTime.Now;
                        dbConnect.Entry(generalOrderImportHeaders).State = EntityState.Modified;
                        dbConnect.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GeneralOrderImportHeaderDC.GenOrderProcessDate -- {e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GeneralOrderImportHeaderDC.GenOrderProcessDate",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);

                }
            }
        }

        /// <summary>
        /// Removes the Import header, portfolio, acts and comment records from the database
        /// </summary>
        /// <returns></returns>
        public static bool DiscardGenOrderImport()
        {
            using (var dbConnect = new EntityDBM())
            {
                using (var dbContextTransaction = dbConnect.Database.BeginTransaction())
                {
                    try
                    {
                        int GeneralOrderActModelsC = 0;
                        int generalOrderPortfolioModelC = 0;
                        int GeneralOrderActCommentModelsC = 0;
                        //  Get all the record details from the other tables relating to this record and delete them
                        var generalOrderImportHeaders = dbConnect.GeneralOrderImportHeaders.Where(r => r.GeneralOrderProcesseddatetime == null).FirstOrDefault();

                        if (generalOrderImportHeaders != null)
                        {
                            //Get the Portfolio Model  data
                            var generalOrderPortfolioModel = dbConnect.GeneralOrderPortfolioModels.Where(r => r.GeneralOrderImportHeaderID == generalOrderImportHeaders.GeneralOrderImportHeaderID).ToList();
                            generalOrderPortfolioModelC = generalOrderPortfolioModel.Count();
                            // foreach (var resultActCommentPara in generalOrderPortfolioModel)
                            for (int i = generalOrderPortfolioModelC; 0 < i; i--)
                            {
                                //Get the Acts
                                //foreach (var resultGeneralOrderActModels in resultActCommentPara[i].GeneralOrderActModels)
                                GeneralOrderActModelsC = generalOrderPortfolioModel.ElementAt(i - 1).GeneralOrderActModels.Count();
                                for (int t = GeneralOrderActModelsC; 0 < t; t--)
                                {
                                    //Get the comments
                                    GeneralOrderActCommentModelsC = generalOrderPortfolioModel.ElementAt(i - 1).GeneralOrderActModels.ElementAt(t - 1).GeneralOrderActCommentModels.Count();
                                    for (int m = GeneralOrderActCommentModelsC; 0 < m; m--)
                                    {
                                        var selectedRec = generalOrderPortfolioModel.ElementAt(i - 1).GeneralOrderActModels.ElementAt(t - 1).GeneralOrderActCommentModels.ElementAt(m - 1).ParagraphModelID;
                                        //Get the comments and delete
                                        dbConnect.GeneralOrderActCommentModels.Remove(generalOrderPortfolioModel.ElementAt(i - 1).GeneralOrderActModels.ElementAt(t - 1).GeneralOrderActCommentModels.ElementAt(m - 1));
                                        //dbConnect.Entry(resultGeneralOrderActCommentsModels).State = EntityState.Deleted;
                                        dbConnect.SaveChanges();
                                        // Get the Portfolio Model and delete                                   
                                        var paragraphCommentsModel = dbConnect.ParagraphModels.Where(r => r.ParagraphModelID == selectedRec).ToList();
                                        //  foreach (var resultPara in paragraphCommentsModel)
                                        GeneralOrderActModelsC = paragraphCommentsModel.Count();
                                        for (int n = GeneralOrderActModelsC; 0 < n; n--)
                                        {
                                            dbConnect.ParagraphModels.Remove(paragraphCommentsModel.ElementAt(n - 1));
                                            // dbConnect.Entry(resultPara).State = EntityState.Deleted;
                                            dbConnect.SaveChanges();
                                        }
                                    }
                                    var selectedRec1 = generalOrderPortfolioModel.ElementAt(i - 1).GeneralOrderActModels.ElementAt(t - 1).ParagraphModelID;

                                    //now delete the Acts
                                    dbConnect.GeneralOrderActModels.Remove(generalOrderPortfolioModel.ElementAt(i - 1).GeneralOrderActModels.ElementAt(t - 1));
                                    // dbConnect.Entry(resultGeneralOrderActModels).State = EntityState.Deleted;
                                    dbConnect.SaveChanges();

                                    // Get the Portfolio Model and delete
                                    var paragraphModel = dbConnect.ParagraphModels.Where(r => r.ParagraphModelID == selectedRec1).ToList();
                                    //foreach (var resultPara in paragraphModel)
                                    GeneralOrderActModelsC = paragraphModel.Count();
                                    for (int o = GeneralOrderActModelsC; 0 < o; o--)
                                    {
                                        dbConnect.ParagraphModels.Remove(paragraphModel.ElementAt(o - 1));
                                        //dbConnect.Entry(resultPara).State = EntityState.Deleted;
                                        dbConnect.SaveChanges();
                                    }
                                }

                                //now delete the portfolio models
                                dbConnect.GeneralOrderPortfolioModels.Remove(generalOrderPortfolioModel.ElementAt(i - 1));
                                // dbConnect.Entry(resultActCommentPara).State = EntityState.Deleted;
                                dbConnect.SaveChanges();

                                // Get the Portfolio Model and delete
                                var selectedRec2 = generalOrderPortfolioModel.ElementAt(i - 1).ParagraphModelID;
                                var potfolioModel = dbConnect.ParagraphModels.Where(r => r.ParagraphModelID == selectedRec2).ToList();
                                //foreach (var resultPara in potfolioModel)\
                                GeneralOrderActModelsC = potfolioModel.Count();
                                for (int o = GeneralOrderActModelsC; 0 < o; o--)
                                {
                                    dbConnect.ParagraphModels.Remove(potfolioModel.ElementAt(o - 1));
                                    //  dbConnect.Entry(resultPara).State = EntityState.Deleted;
                                    dbConnect.SaveChanges();
                                }
                            }
                            //now delete the Import Header
                            dbConnect.GeneralOrderImportHeaders.Remove(generalOrderImportHeaders);
                            //  dbConnect.Entry(generalOrderImportHeaders).State = EntityState.Deleted;
                            dbConnect.SaveChanges();

                        }

                        //  var test = dbConnect.GeneralOrderImportHeaders.Where(r => r.GeneralOrderProcesseddatetime == null).FirstOrDefault();
                        //  dbContextTransaction.Rollback(); //testing only
                        dbContextTransaction.Commit();
                        /////

                        //foreach (var resultActCommentPara in generalOrderPortfolioModel)
                        //{
                        //    //Get the Acts
                        //    foreach (var resultGeneralOrderActModels in resultActCommentPara.GeneralOrderActModels)
                        //    {
                        //        //Get the comments
                        //        foreach (var resultGeneralOrderActCommentsModels in resultGeneralOrderActModels.GeneralOrderActCommentModels)
                        //        {
                        //            //Get the comments and delete
                        //            dbConnect.GeneralOrderActCommentModels.Remove(resultGeneralOrderActCommentsModels);
                        //            //dbConnect.Entry(resultGeneralOrderActCommentsModels).State = EntityState.Deleted;
                        //            dbConnect.SaveChanges();
                        //            // Get the Portfolio Model and delete
                        //            var paragraphCommentsModel = dbConnect.ParagraphModels.Where(r => r.ParagraphModelID == resultGeneralOrderActCommentsModels.ParagraphModelID).ToList();
                        //            foreach (var resultPara in paragraphCommentsModel)
                        //            {
                        //                dbConnect.ParagraphModels.Remove(resultPara);
                        //                // dbConnect.Entry(resultPara).State = EntityState.Deleted;
                        //                dbConnect.SaveChanges();
                        //            }
                        //        }

                        //        //now delete the Acts
                        //        dbConnect.GeneralOrderActModels.Remove(resultGeneralOrderActModels);
                        //        // dbConnect.Entry(resultGeneralOrderActModels).State = EntityState.Deleted;
                        //        dbConnect.SaveChanges();

                        //        // Get the Portfolio Model and delete
                        //        var paragraphModel = dbConnect.ParagraphModels.Where(r => r.ParagraphModelID == resultGeneralOrderActModels.ParagraphModelID).ToList();
                        //        foreach (var resultPara in paragraphModel)
                        //        {
                        //            dbConnect.ParagraphModels.Remove(resultPara);
                        //            //dbConnect.Entry(resultPara).State = EntityState.Deleted;
                        //            dbConnect.SaveChanges();
                        //        }
                        //    }

                        //    //now delete the portfolio models
                        //    dbConnect.GeneralOrderPortfolioModels.Remove(resultActCommentPara);
                        //    // dbConnect.Entry(resultActCommentPara).State = EntityState.Deleted;
                        //    dbConnect.SaveChanges();

                        //    // Get the Portfolio Model and delete
                        //    var potfolioModel = dbConnect.ParagraphModels.Where(r => r.ParagraphModelID == resultActCommentPara.ParagraphModelID).ToList();
                        //    foreach (var resultPara in potfolioModel)
                        //    {
                        //        dbConnect.ParagraphModels.Remove(resultPara);
                        //        //  dbConnect.Entry(resultPara).State = EntityState.Deleted;
                        //        dbConnect.SaveChanges();
                        //    }
                        //}

                        ////////
                        //  dbContextTransaction.Commit();


                        //  var generalOrderImportHeaders = dbConnect.GeneralOrderImportHeaders.Where(r => r.GeneralOrderProcesseddatetime == null)
                        //.GroupJoin(
                        //    dbConnect.GeneralOrderPortfolioModels,
                        //    goih => goih.GeneralOrderImportHeaderID,
                        //    gopm => gopm.GeneralOrderImportHeaderID,
                        //    (goih, gopm) => new
                        //    {
                        //        goih,
                        //        gopm1 = gopm.GroupJoin(dbConnect.GeneralOrderActModels,
                        //          p1 => p1.GeneralOrderPortfolioModelID,
                        //          p2 => p2.GeneralOrderPortfolioModelID,
                        //          (p1, p2) => new
                        //          {
                        //              p1,
                        //              p2a = p2.GroupJoin(dbConnect.GeneralOrderActCommentModels,
                        //            pt1 => pt1.GeneralOrderActModelID,
                        //            pt2 => pt2.GeneralOrderActModelID,
                        //            (pt1, pt2) => new { pt1, pt2 })
                        //          })
                        //    })
                        //      .ToList();

                        //if (generalOrderImportHeaders != null)
                        //{
                        //    //Get the header data
                        //    foreach (var resultgeneralOrderImportHeaders in generalOrderImportHeaders)
                        //    {

                        //        foreach (var resultActCommentPara in resultgeneralOrderImportHeaders.gopm1)
                        //        {
                        //            //Acts
                        //            foreach (var resultGeneralOrderActModels in resultActCommentPara.p1.GeneralOrderActModels)
                        //            {
                        //                //comments
                        //                foreach (var resultGeneralOrderActCommentsModels in resultGeneralOrderActModels.GeneralOrderActCommentModels)
                        //                {
                        //                    // Get the Portfolio Model and delete
                        //                    var paragraphCommentsModel = dbConnect.ParagraphModels.Where(r => r.ParagraphModelID == resultGeneralOrderActCommentsModels.ParagraphModelID).ToList();
                        //                    foreach (var resultPara in paragraphCommentsModel)
                        //                    {
                        //                        dbConnect.Entry(resultPara).State = EntityState.Deleted;
                        //                        dbConnect.SaveChanges();
                        //                    }
                        //                    //Get the comments and delete
                        //                    dbConnect.Entry(resultGeneralOrderActCommentsModels).State = EntityState.Deleted;
                        //                    dbConnect.SaveChanges();
                        //                }
                        //            }

                        //            // Get the Portfolio Model and delete
                        //            var paragraphModel = dbConnect.ParagraphModels.Where(r => r.ParagraphModelID == resultActCommentPara.p1.ParagraphModelID).ToList();
                        //            foreach (var resultPara in paragraphModel)
                        //            {
                        //                dbConnect.Entry(resultPara).State = EntityState.Deleted;
                        //                dbConnect.SaveChanges();
                        //            }

                        //            //now delete the Acts
                        //            dbConnect.Entry(resultActCommentPara).State = EntityState.Deleted;
                        //            dbConnect.SaveChanges();
                        //        }

                        //        //now delete the Import Header
                        //        dbConnect.Entry(resultgeneralOrderImportHeaders).State = EntityState.Deleted;
                        //        dbConnect.SaveChanges();
                        //    }
                        //}



                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GeneralOrderImportHeaderDC.DiscardGenOrderImport -- { e.Message} -- Contact IT support.");
                        WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                        {
                            AdditionalUserInfo = "Exception",
                            LogDetail = e.Message,
                            LogDetail_Additional = "GeneralOrderImportHeaderDC.DiscardGenOrderImport",
                            LogTime = DateTime.Now,
                            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                            Workstation = System.Environment.MachineName
                        }, "GODatabaseEntity", false);

                    }
                }
            }
            return true;
        }

        /// <summary>
        /// goMeta represents the JSON string send via the client
        /// </summary>
        /// <param name="goMeta"></param>
        /// <returns></returns>
        public static bool GenOrderImport(string goMeta, bool fullGO, DateTime GOFileInputDatePicker)
        {

            //result represents a deserialized object of JSON
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralOrderMetadataViewModel>(goMeta);
            var generalOrderImportHeaders = new GeneralOrderImportHeader();

            //Coonect to the database
            using (var dbConnect = new EntityDBM())
            {
                //Check to make sure paragraphTtype has values in DB
                var count = dbConnect.ParagraphModelTypes.Count();
                if (count < 9)
                {

                    var res = dbConnect.ParagraphModelTypes.Add(
                    new ParagraphModelType
                    {
                        ParagraphModelTypeName = "GeneralOrderPortfolio",
                        FontBold = true,
                        BulletChar = "",
                        BulletASCII = 0,
                        TabStopPosition = 36, //1.27, Cenimeters and points
                        PageBreakBefore = true,
                        ListSymbolFont = "none",
                        IndentationLeft = 0,
                        IndentationRight = 0,
                        TabHangingIndent = 0,
                        IndentationBy = 0,
                        AlignmentType = 0
                    });
                    res = dbConnect.ParagraphModelTypes.Add(
                    new ParagraphModelType
                    {
                        ParagraphModelTypeName = "GeneralOrderActTitle",
                        FontBold = false,
                        BulletChar = "",
                        BulletASCII = 0,
                        TabStopPosition = 36, //1.27,
                        PageBreakBefore = false,
                        ListSymbolFont = "none",
                        IndentationLeft = 0,
                        IndentationRight = 0,
                        TabHangingIndent = 0,
                        IndentationBy = 0,
                        AlignmentType = 0

                    });
                    res = dbConnect.ParagraphModelTypes.Add(
                     new ParagraphModelType
                     {
                         ParagraphModelTypeName = "GeneralOrderActComment_1",
                         FontBold = false,
                         BulletChar = "",
                         BulletASCII = 183,
                         TabStopPosition = 39.4, //1.39,
                         PageBreakBefore = false,
                         ListSymbolFont = "symbol",
                         IndentationLeft = 21.2, //0.75,
                         IndentationRight = 0,
                         TabHangingIndent = 1,
                         IndentationBy = -18, //50.7, - 0.63,
                         AlignmentType = 0

                     });
                    res = dbConnect.ParagraphModelTypes.Add(

                        new ParagraphModelType
                        {
                            ParagraphModelTypeName = "GeneralOrderActComment_2",
                            FontBold = false,
                            BulletChar = "",
                            BulletASCII = 111,
                            TabStopPosition = 72, //2.54,
                            PageBreakBefore = false,
                            ListSymbolFont = "Courier New",
                            IndentationLeft = 53.8, //1.9,
                            IndentationRight = 0,
                            TabHangingIndent = 1,
                            IndentationBy = -18, //53.8,  - 0.63,
                            AlignmentType = 0

                        });
                    res = dbConnect.ParagraphModelTypes.Add(
                        new ParagraphModelType
                        {
                            ParagraphModelTypeName = "GeneralOrderActComment_3",
                            FontBold = false,
                            BulletChar = "",
                            BulletASCII = 167,
                            TabStopPosition = 108, // 3.81,
                            PageBreakBefore = false,
                            ListSymbolFont = "Wingdings",
                            IndentationLeft = 89.8, //3.17,
                            IndentationRight = 0,
                            TabHangingIndent = 1,
                            IndentationBy = -18, //18.1, - 0.63,
                            AlignmentType = 0

                        });
                    res = dbConnect.ParagraphModelTypes.Add(
                        new ParagraphModelType
                        {
                            ParagraphModelTypeName = "GeneralOrderActComment_NoBullet",
                            FontBold = false,
                            BulletChar = "",
                            BulletASCII = 996,
                            TabStopPosition = 36, //1.27,
                            PageBreakBefore = false,
                            ListSymbolFont = "none",
                            IndentationLeft = 1, //50, - 1.75,
                            IndentationRight = 0,
                            TabHangingIndent = 0,
                            IndentationBy = -18,
                            AlignmentType = 0
                        });

                    res = dbConnect.ParagraphModelTypes.Add(
                      new ParagraphModelType
                      {
                          ParagraphModelTypeName = "GeneralOrderActComment_FirstNoBullet",
                          FontBold = false,
                          BulletChar = "",
                          BulletASCII = 997,
                          TabStopPosition = 36, //1.27,
                          PageBreakBefore = false,
                          ListSymbolFont = "none",
                          IndentationLeft = 10, //50, - 1.75,
                          IndentationRight = 0,
                          TabHangingIndent = 0,
                          IndentationBy = -18,
                          AlignmentType = 0
                      });

                    res = dbConnect.ParagraphModelTypes.Add(
                       new ParagraphModelType
                       {
                           ParagraphModelTypeName = "GeneralOrderActComment_SecondNoBullet",
                           FontBold = false,
                           BulletChar = "",
                           BulletASCII = 998,
                           TabStopPosition = 36, //1.27,
                           PageBreakBefore = false,
                           ListSymbolFont = "none",
                           IndentationLeft = 50, //50, - 1.75,
                           IndentationRight = 0,
                           TabHangingIndent = 0,
                           IndentationBy = -18,
                           AlignmentType = 0
                       });

                    res = dbConnect.ParagraphModelTypes.Add(
                       new ParagraphModelType
                       {
                           ParagraphModelTypeName = "GeneralOrderActComment_ThirdNoBullet",
                           FontBold = false,
                           BulletChar = "",
                           BulletASCII = 999,
                           TabStopPosition = 36, //1.27,
                           PageBreakBefore = false,
                           ListSymbolFont = "none",
                           IndentationLeft = 78, //50, - 1.75,
                           IndentationRight = 0,
                           TabHangingIndent = 0,
                           IndentationBy = -18,
                           AlignmentType = 0
                       });
                    var EffectedRows = dbConnect.SaveChanges();

                }



                if (fullGO)
                {
                    generalOrderImportHeaders = dbConnect.GeneralOrderImportHeaders.Where(r => r.GeneralOrderProcesseddatetime == null).FirstOrDefault();
                    // generalOrderImportHeaders = dbConnect.GeneralOrderImportHeaders.Where(r => r.GeneralOrderFull == true).FirstOrDefault();
                    //Update the record if it exists, else create one
                    if (generalOrderImportHeaders != null)
                    {
                        // GenOrderProcessDate(generalOrderImportHeaders.GeneralOrderImportHeaderID);
                        DiscardGenOrderImport(); //Discard the unprocessed record before uploading a new one.                     
                    }
                    //Create a new record
                    generalOrderImportHeaders = dbConnect.GeneralOrderImportHeaders.Add(new GeneralOrderImportHeader
                    {
                        GeneralOrderEffectiveDate = GOFileInputDatePicker, // result.orderEffective,
                        GeneralOrderFull = fullGO, // result.orderfull,
                        GeneralOrderImportDatetime = DateTime.Now,
                        GeneralOrderImportFile = result.orderFileName,
                        GeneralOrderProcesseddatetime = null, // DateTime.MinValue,
                        OriginalXML = result.OriginalXML,
                        ModifiedXML = result.OriginalXML,
                        MarginLeft = result.marginLeft,
                        MarginRight = result.marginRight,
                        DefaultTabStop = result.defaultTabStop,
                        PageHeaderMargin = result.pageHeaderMargin,
                        PageFooterMargin = result.pageFooterMargin,
                        IndentationSpacing = result.IndentationSpacing
                    });
                    dbConnect.SaveChanges();
                }
                else
                {
                    generalOrderImportHeaders = dbConnect.GeneralOrderImportHeaders.Add(new GeneralOrderImportHeader
                    {
                        GeneralOrderEffectiveDate = GOFileInputDatePicker, // result.orderEffective,
                        GeneralOrderFull = fullGO, // result.orderfull,
                        GeneralOrderImportDatetime = DateTime.Now,
                        GeneralOrderImportFile = result.orderFileName,
                        GeneralOrderProcesseddatetime = null, // DateTime.MinValue,
                        OriginalXML = result.OriginalXML,
                        ModifiedXML = result.OriginalXML,
                        MarginLeft = result.marginLeft,
                        MarginRight = result.marginRight,
                        DefaultTabStop = result.defaultTabStop,
                        PageHeaderMargin = result.pageHeaderMargin,
                        PageFooterMargin = result.pageFooterMargin,
                        IndentationSpacing = result.IndentationSpacing
                    });


                    //Save the GeneralOrderImportHeader data and it will return effected rows. The ID will be inserted into the array generalOrderImportHeaders.GeneralOrderImportHeaderID
                    var EffectedRows = dbConnect.SaveChanges();
                }


                using (var dbContextTransaction = dbConnect.Database.BeginTransaction())
                {
                    try
                    {
                        //Loop through all the portfolio data
                        foreach (var resultGOModel in result.GOModel)
                        {
                            //Insert portfolio into DB and get ID
                            var actPortfolioID = GeneralOrderPortfolioModelDC.GenOrderPortfolioImport(resultGOModel, generalOrderImportHeaders.GeneralOrderImportHeaderID);

                            //for each portfolio, get the Act Titles 
                            foreach (var resultActTitle in resultGOModel.genOrderActTitle)
                            {
                                //Insert Act Title into the db and get the ID
                                // var resultDepartmentId = Newtonsoft.Json.JsonConvert.DeserializeObject<DepartmentViewModel>(resultActTitle.DepartmentPortfolioIDs);

                                var actTitleID = GeneralOrderActTitleModelDC.GenOrderActTitleImport(resultActTitle, actPortfolioID, resultGOModel.generalOrderPortfolioID, GOFileInputDatePicker);
                                //For each Act Title, get the Act Comments
                                foreach (var resultActComment in resultActTitle.genOrderActComment)
                                {
                                    //Insert Act Comments into DB
                                    var actCommentModelID = GOActCommentModelDC.GOCommentImport(resultActComment, actTitleID);

                                    // If its a new record, get the ID from the database and set it to the Model in memory to keep it in sync
                                    if (resultActComment.generalOrderActCommentModelID == 0)
                                    {
                                        resultActComment.generalOrderActCommentModelID = actCommentModelID;
                                    }
                                }
                            }
                        }

                        dbContextTransaction.Commit();
                        return true;

                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GeneralOrderImportHeaderDC.GenOrderImport -- { e.Message} -- Contact IT support.");
                        WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                        {
                            AdditionalUserInfo = "Exception",
                            LogDetail = e.Message,
                            LogDetail_Additional = "GeneralOrderImportHeaderDC.GenOrderImport",
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

