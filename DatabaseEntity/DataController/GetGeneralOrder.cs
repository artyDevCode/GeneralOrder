using ApplicationLogger;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseEntity
{
    public class GetGeneralOrder
    {
        /// <summary>
        /// Read the GeneralOrderImportHeader table and check to see if the record has been processed
        /// </summary>
        /// <returns></returns>
        public static GeneralOrderImportHeader CheckGeneralOrderProcessedDate()
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    return dbConnect.GeneralOrderImportHeaders.Where(r => r.GeneralOrderProcesseddatetime == null).FirstOrDefault();                
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetGeneralOrder.CheckGeneralOrderProcessedDate -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetGeneralOrder.CheckGeneralOrderProcessedDate",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new GeneralOrderImportHeader(); //empty model
                }
            }

        }

        /// <summary>
        /// Create an object to recreate a document ie SelectBtnGenerateActAdminReport
        /// </summary>
        /// <param name="ildNumber"></param>
        /// <returns></returns>
        public static List<ActAdministrationViewModel> GetActAdministrationDoc(int ildNumber = 0)
        {
            List<ActAdministrationViewModel> ActListResult = new List<ActAdministrationViewModel>();

            dynamic ActList = null;
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    //If the ildNumber = 0, then get all records
                    if (ildNumber != 0)
                    {
                        ActList = dbConnect.ActAdministrations
                            .GroupJoin(
                                dbConnect.ActAdminComments.OrderBy(s => s.ActAdminCommentID),
                                goih => goih.ActAdministrationID,
                                gopm => gopm.ActAdministrationID,
                                (goih, gopm) => new
                                {
                                    goih,
                                    gopm
                                }
                            )
                            .Where(r => r.goih.EndDate == null && r.goih.ILDNumber == ildNumber)
                            .ToList();
                    }
                    else
                    {
                        ActList = dbConnect.ActAdministrations
                            .GroupJoin(
                                dbConnect.ActAdminComments.OrderBy(s => s.ActAdminCommentID),
                                goih => goih.ActAdministrationID,
                                gopm => gopm.ActAdministrationID,
                                (goih, gopm) => new
                                { goih, gopm }
                            )
                    .Where(r => r.goih.EndDate == null)
                    .ToList();
                    }

                    if (ActList.Count > 0 || ActList != null)
                    {
                        foreach (var actResult in ActList)
                        {
                            ActAdministrationViewModel aam = new ActAdministrationViewModel
                            {
                                ILDNumber = actResult.goih.ILDNumber,
                                ActAdministrationID = actResult.goih.ActAdministrationID,
                                PortfolioID = actResult.goih.DepartmentPortfolioID,
                                IsCurrent = actResult.goih.IsCurrent,
                                OnHold = actResult.goih.OnHold,
                                PendingEditID = actResult.goih.PendingEditID,
                                IsExcept = actResult.goih.IsExcept,
                                PendingEditType = actResult.goih.PendingEditType,
                                StartDate = actResult.goih.StartDate,
                                EndDate = actResult.goih.EndDate,
                            };
                            aam.ActCommentModel = new List<ActAdminCommentViewModel>();

                            foreach (var comResult in actResult.goih.ActAdminComments)
                            {
                                ActAdminCommentViewModel acm = new ActAdminCommentViewModel
                                {
                                    ActAdminCommentId = comResult.ActAdminCommentID,
                                    FontBold = comResult.FontBold,
                                    BulletChar = comResult.BulletChar,
                                    BulletASCII = comResult.BulletASCII,
                                    TabStopPosition = comResult.TabStopPosition,
                                    PageBreakBefore = comResult.PageBreakBefore,
                                    ListSymbolFont = comResult.ListSymbolFont,
                                    IndentationLeft = comResult.IndentationLeft,
                                    IndentationRight = comResult.IndentationRight,
                                    IndentationBy = comResult.IndentationBy,
                                    TabHangingIndent = comResult.TabHangingIndent,
                                    AlignmentType = comResult.AlignmentType,
                                    ActAdministrationComment = comResult.ActAdminComment1
                                };

                                aam.ActCommentModel.Add(acm);
                            }

                            ActListResult.Add(aam);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetGeneralOrder.GetActAdministrationDoc -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetGeneralOrder.GetActAdministrationDoc",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new List<ActAdministrationViewModel>(); //empty list
                }

            }
            return ActListResult;
        }

        // Cast method - thanks to type inference when calling methods it 
        // is possible to cast object to type without knowing the type name
        //private static T Cast<T>(object obj, T type)
        //{
        //    return (T)obj;
        //}

        public class ActILDList
        {
            public int ildNumber { get; set; }
        }
        public static List<ActILDList> GetActAdministrationILDList()
        {

            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    var ActList = dbConnect.ActAdministrations
                         .Where(r => r.EndDate == null)
                         .Select(r => new ActILDList
                         {
                             ildNumber = r.ILDNumber
                         })
                         .ToList();
                    return ActList;

                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetGeneralOrder.GetActAdministrationILDList -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetGeneralOrder.GetActAdministrationILDList",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return new List<ActILDList>(); //empty list
                }

            }
        }

        /// <summary>
        /// Gets the document from the database tables and creates a new model  <see cref="GeneralOrderMetadataViewModel"/>
        /// Standardised can be set to true or false. If set to False, it will create the Document object with the original style / Format from ParagraphModel table.
        /// If set to True, it will create the document object using the standardised style/format from the ParagraphsModelType table
        /// </summary>
        /// <param name="standardised"></param>
        /// <returns></returns>
        public static GeneralOrderMetadataViewModel GetGeneralOrderDoc(bool standardised = false)
        {
            using (var dbConnect = new EntityDBM())
            {
                try
                {
                    /// Lambda GroupJoin. First it reads the lowest level table, the Import headers that contain page level style/format
                    /// It will groupjoin the  <see cref="GeneralOrderPortfolioModels"/>  table using GeneralOrderImportHeaderID keys
                    /// Using this record, it will groupjoin to all the other tables automatically except ParagraphModel and ParagraphModelType
                    /// Using the ParagraphModelID, groupjoin will retrieve the database information from <see cref="ParagraphModels"/> and <see cref="ParagraphModelTypes"/>
                    /// ParagraphModel contains the original document style/format while ParagrapModelType contains the standardised style/format
                    var myList1 = dbConnect.GeneralOrderImportHeaders.Where(r => r.GeneralOrderProcesseddatetime == null)
                         .GroupJoin(
                             dbConnect.GeneralOrderPortfolioModels,
                             goih => goih.GeneralOrderImportHeaderID,
                             gopm => gopm.GeneralOrderImportHeaderID,
                             (goih, gopm) => new
                             {
                                 goih,
                                 gopm1 = gopm.GroupJoin(dbConnect.ParagraphModels,
                                   p1 => p1.ParagraphModelID,
                                   p2 => p2.ParagraphModelID,
                                   (p1, p2) => new
                                   {
                                       p1,
                                       p2a = p2.GroupJoin(dbConnect.ParagraphModelTypes,
                                     pt1 => pt1.ParagraphModelTypeID,
                                     pt2 => pt2.ParagraphModelTypeID,
                                     (pt1, pt2) => new { pt1, pt2 })
                                   })
                             })
                             //  .Where(r => r.goih.GeneralOrderProcesseddatetime == null)
                             .ToList();

                    if (myList1.Count() == 0)
                        return null;// new GeneralOrderMetadataViewModel(); 


                    ////Create an empty object
                    List<GeneralOrderMetadataViewModel> GOMVMList = new List<GeneralOrderMetadataViewModel>();
                    GeneralOrderPortfolioViewModel GOP = null;
                    GeneralOrderActTitleViewModel GOAT = null;
                    GeneralOrderActCommentViewModel GOAC = null;




                    // Loop through the object containing the database data
                    foreach (var res in myList1)
                    {
                        GeneralOrderMetadataViewModel GOMVM = new GeneralOrderMetadataViewModel();
                        GOMVM.GOModel = new List<GeneralOrderPortfolioViewModel>();

                        GOMVM.ImportHeaderID = res.goih.GeneralOrderImportHeaderID;
                        GOMVM.orderfull = res.goih.GeneralOrderFull;
                        GOMVM.orderFileName = res.goih.GeneralOrderImportFile;
                        GOMVM.IndentationSpacing = res.goih.IndentationSpacing;
                        GOMVM.marginLeft = res.goih.MarginLeft;
                        GOMVM.marginRight = res.goih.MarginRight;
                        GOMVM.OriginalXML = res.goih.OriginalXML;
                        GOMVM.pageFooterMargin = res.goih.PageFooterMargin;
                        GOMVM.pageHeaderMargin = res.goih.PageHeaderMargin;
                        GOMVM.defaultTabStop = res.goih.DefaultTabStop;
                        GOMVM.orderEffective = res.goih.GeneralOrderEffectiveDate;
                        //reset the object
                        GOMVM.GOModel = new List<GeneralOrderPortfolioViewModel>();
                        foreach (var resGOPM in res.goih.GeneralOrderPortfolioModels)
                        {
                            GOP = new GeneralOrderPortfolioViewModel();
                            GOP.generalOrderPortfolioModelID = (int)resGOPM.GeneralOrderPortfolioModelID;
                            GOP.generalOrderPortfolioID = (int)resGOPM.GeneralOrderPortfolioID;
                            GOP.generalOrderPortfolioName = resGOPM.GeneralOrderPortfolioName;
                            //If standardised, use ParagraphModelType
                            if (standardised)
                            {
                                GOP.generalOrderPortfolioParagraphMeta = new DocViewModel
                                {
                                    fontBold = resGOPM.ParagraphModel.ParagraphModelType.FontBold,
                                    bulletChar = resGOPM.ParagraphModel.ParagraphModelType.BulletChar,
                                    bulletASCI = resGOPM.ParagraphModel.ParagraphModelType.BulletASCII,
                                    indentationBy = (float)resGOPM.ParagraphModel.ParagraphModelType.IndentationBy,
                                    IndentationLeft = (float)resGOPM.ParagraphModel.ParagraphModelType.IndentationLeft,
                                    IndentationRight = (float)resGOPM.ParagraphModel.ParagraphModelType.IndentationRight,
                                    TabHangingIndent = resGOPM.ParagraphModel.ParagraphModelType.TabHangingIndent,
                                    listSymbolFont = resGOPM.ParagraphModel.ParagraphModelType.ListSymbolFont,
                                    pageBreakBefore = resGOPM.ParagraphModel.ParagraphModelType.PageBreakBefore,
                                    paragraphAlignment = resGOPM.ParagraphModel.ParagraphModelType.AlignmentType,
                                    tabStopPosition = (float)resGOPM.ParagraphModel.ParagraphModelType.TabStopPosition
                                };
                            }
                            else
                            {
                                //Not standardised, use ParagraphModel
                                GOP.generalOrderPortfolioParagraphMeta = new DocViewModel
                                {
                                    fontBold = resGOPM.ParagraphModel.FontBold,
                                    bulletChar = resGOPM.ParagraphModel.BulletChar,
                                    bulletASCI = resGOPM.ParagraphModel.BulletASCII,
                                    indentationBy = (float)resGOPM.ParagraphModel.IndentationBy,
                                    IndentationLeft = (float)resGOPM.ParagraphModel.IndentationLeft,
                                    IndentationRight = (float)resGOPM.ParagraphModel.IndentationRight,
                                    TabHangingIndent = resGOPM.ParagraphModel.TabHangingIndent,
                                    listSymbolFont = resGOPM.ParagraphModel.ListSymbolFont,
                                    pageBreakBefore = resGOPM.ParagraphModel.PageBreakBefore,
                                    paragraphAlignment = resGOPM.ParagraphModel.AlignmentType,
                                    tabStopPosition = (float)resGOPM.ParagraphModel.TabStopPosition
                                };
                            }
                            //reset the object
                            GOP.genOrderActTitle = new List<GeneralOrderActTitleViewModel>();
                            //Loop through the general order act title
                            foreach (var resGOAT in resGOPM.GeneralOrderActModels)
                            {
                                GOAT = new GeneralOrderActTitleViewModel();
                                GOAT.DepartmentPortfolioIDs = resGOAT.DepartmentPortfolioIDs;
                                GOAT.generalOrderActTitleModelID = resGOAT.GeneralOrderActModelID;
                                GOAT.generalOrderActTitle = resGOAT.GeneralOrderActTitle;
                                GOAT.EffectiveDate = resGOAT.EffectiveDate; // res.goih.GeneralOrderEffectiveDate;
                                GOAT.ildNumber = (int)resGOAT.ILDNumber;
                                GOAT.isExcept = resGOAT.IsExcept;
                                GOAT.generalOrderPortfolioModelID = resGOAT.GeneralOrderPortfolioModelID;
                                //If standardised, use ParagraphModelType
                                if (standardised)
                                {
                                    GOAT.generalOrderActTitleParagraphMeta = new DocViewModel
                                    {
                                        fontBold = resGOAT.ParagraphModel.ParagraphModelType.FontBold,
                                        bulletChar = resGOAT.ParagraphModel.ParagraphModelType.BulletChar,
                                        bulletASCI = resGOAT.ParagraphModel.ParagraphModelType.BulletASCII,
                                        indentationBy = (float)resGOAT.ParagraphModel.ParagraphModelType.IndentationBy,
                                        IndentationLeft = (float)resGOAT.ParagraphModel.ParagraphModelType.IndentationLeft,
                                        IndentationRight = (float)resGOAT.ParagraphModel.ParagraphModelType.IndentationRight,
                                        TabHangingIndent = resGOAT.ParagraphModel.ParagraphModelType.TabHangingIndent,
                                        listSymbolFont = resGOAT.ParagraphModel.ParagraphModelType.ListSymbolFont,
                                        pageBreakBefore = resGOAT.ParagraphModel.ParagraphModelType.PageBreakBefore,
                                        paragraphAlignment = resGOAT.ParagraphModel.ParagraphModelType.AlignmentType,
                                        tabStopPosition = (float)resGOAT.ParagraphModel.ParagraphModelType.TabStopPosition
                                    };
                                }
                                else
                                {
                                    //Not standardised, use ParagraphModel
                                    GOAT.generalOrderActTitleParagraphMeta = new DocViewModel
                                    {
                                        fontBold = resGOAT.ParagraphModel.FontBold,
                                        bulletChar = resGOAT.ParagraphModel.BulletChar,
                                        bulletASCI = resGOAT.ParagraphModel.BulletASCII,
                                        indentationBy = (float)resGOAT.ParagraphModel.IndentationBy,
                                        IndentationLeft = (float)resGOAT.ParagraphModel.IndentationLeft,
                                        IndentationRight = (float)resGOAT.ParagraphModel.IndentationRight,
                                        TabHangingIndent = resGOAT.ParagraphModel.TabHangingIndent,
                                        listSymbolFont = resGOAT.ParagraphModel.ListSymbolFont,
                                        pageBreakBefore = resGOAT.ParagraphModel.PageBreakBefore,
                                        paragraphAlignment = resGOAT.ParagraphModel.AlignmentType,
                                        tabStopPosition = (float)resGOAT.ParagraphModel.TabStopPosition
                                    };
                                }
                                //Reset the object
                                GOAT.genOrderActComment = new List<GeneralOrderActCommentViewModel>();
                                //Loop through the general order Act Comment
                                foreach (var resGOAC in resGOAT.GeneralOrderActCommentModels)
                                {
                                    GOAC = new GeneralOrderActCommentViewModel();
                                    GOAC.generalOrderActCommentModelID = resGOAC.GeneralOrderActCommentModelID;
                                    GOAC.generalOrderActComment = resGOAC.GeneralOrderActComment;
                                    GOAC.generalOrderActTitleModelID = resGOAC.GeneralOrderActModelID;
                                    //If standardised, use ParagraphModelType
                                    if (standardised)
                                    {
                                        GOAC.generalOrderActCommentParagraphMeta = new DocViewModel
                                        {
                                            fontBold = resGOAC.ParagraphModel.ParagraphModelType.FontBold,
                                            bulletChar = ((char)resGOAC.ParagraphModel.ParagraphModelType.BulletASCII).ToString(),// resGOAC.ParagraphModel.BulletChar,
                                            bulletASCI = resGOAC.ParagraphModel.ParagraphModelType.BulletASCII,
                                            indentationBy = (float)resGOAC.ParagraphModel.ParagraphModelType.IndentationBy,
                                            IndentationLeft = (float)resGOAC.ParagraphModel.ParagraphModelType.IndentationLeft,
                                            IndentationRight = (float)resGOAC.ParagraphModel.ParagraphModelType.IndentationRight,
                                            TabHangingIndent = resGOAC.ParagraphModel.ParagraphModelType.TabHangingIndent,
                                            listSymbolFont = resGOAC.ParagraphModel.ParagraphModelType.ListSymbolFont,
                                            pageBreakBefore = resGOAC.ParagraphModel.ParagraphModelType.PageBreakBefore,
                                            paragraphAlignment = resGOAC.ParagraphModel.ParagraphModelType.AlignmentType,
                                            tabStopPosition = (float)resGOAC.ParagraphModel.ParagraphModelType.TabStopPosition
                                        };
                                    }
                                    else
                                    {
                                        //Not standardised, use ParagraphModel
                                        GOAC.generalOrderActCommentParagraphMeta = new DocViewModel
                                        {
                                            fontBold = resGOAC.ParagraphModel.FontBold,
                                            bulletChar = ((char)resGOAC.ParagraphModel.BulletASCII).ToString(),// resGOAC.ParagraphModel.BulletChar,
                                            bulletASCI = resGOAC.ParagraphModel.BulletASCII,
                                            indentationBy = (float)resGOAC.ParagraphModel.IndentationBy,
                                            IndentationLeft = (float)resGOAC.ParagraphModel.IndentationLeft,
                                            IndentationRight = (float)resGOAC.ParagraphModel.IndentationRight,
                                            TabHangingIndent = resGOAC.ParagraphModel.TabHangingIndent,
                                            listSymbolFont = resGOAC.ParagraphModel.ListSymbolFont,
                                            pageBreakBefore = resGOAC.ParagraphModel.PageBreakBefore,
                                            paragraphAlignment = resGOAC.ParagraphModel.AlignmentType,
                                            tabStopPosition = (float)resGOAC.ParagraphModel.TabStopPosition
                                        };
                                    }
                                    GOAT.genOrderActComment.Add(GOAC);
                                }
                                GOP.genOrderActTitle.Add(GOAT);
                            }
                            GOMVM.GOModel.Add(GOP);
                        }
                        GOMVMList.Add(GOMVM);
                    }


                    return GOMVMList[0];
                }

                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GetGeneralOrder.GetGeneralOrderDoc -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "GetGeneralOrder.GetGeneralOrderDoc",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GODatabaseEntity", false);
                    return null; // new List<GeneralOrderMetadataViewModel>();
                }


            }
        }
    }
}
