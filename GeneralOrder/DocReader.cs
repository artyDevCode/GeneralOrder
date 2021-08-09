
///NEWER VERSION STILL NOT FINISHED
using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace GeneralOrder
{
    public class DocReader
    {
        #region publicProperties
        public static ActTitleViewModel activitySearch { get; set; }
        public static PortfolioViewModel portfolioSearch { get; set; }
        public static byte gOrderActTitlebulletASCI { get; set; } = 0;
        public static GeneralOrderMetadataViewModel portfolioListError { get; set; }
        private bool _importFileIsRunning { get; set; }
        public bool importFileIsRunning { get; set; }
        private static GeneralOrderMetadataViewModel resMeta { get; set; }
        #endregion


        /// <summary>
        /// Read a Word document to extract a range of paragraphs with style. This then gets saved to an object <see cref="GeneralOrderMetadataViewModel"/>  and gets passed to other classes 
        /// that either save the data into a Database or recreates the document.
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <param name="headingText"></param>
        /// <param name="headingStyle"></param>
        public static async Task<Tuple<bool, GeneralOrderMetadataViewModel>> WordDocReader(string fileLocation) //, string headingText, string headingStyle)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;
            object path = fileLocation;
            object readOnly = true;
            bool isError = false;
            Microsoft.Office.Interop.Word.Document docs = new Microsoft.Office.Interop.Word.Document();
            try
            {
                docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);

                //Comments switch statement that includes a range
                portfolioListError = new GeneralOrderMetadataViewModel();
                portfolioListError.GOModel = new List<GeneralOrderPortfolioViewModel>();
                List<GeneralOrderActTitleViewModel> actTitleListError = new List<GeneralOrderActTitleViewModel>();

                resMeta = new GeneralOrderMetadataViewModel();

                //Create 
                List<GeneralOrderPortfolioViewModel> portfolioList = new List<GeneralOrderPortfolioViewModel>();
                List<GeneralOrderActTitleViewModel> actTitleList = new List<GeneralOrderActTitleViewModel>();
                List<GeneralOrderActCommentViewModel> actCommentList = new List<GeneralOrderActCommentViewModel>();

                activitySearch = new ActTitleViewModel();
                portfolioSearch = new PortfolioViewModel();

                int paraCount = docs.Paragraphs.Count;
                List result = new List();

                // Set the space between the markers and list content to 25 DIP.
                result.MarkerOffset = 25;
                // Use uppercase Roman numerals.
                result.MarkerStyle = System.Windows.TextMarkerStyle.UpperRoman;

                //Temporary variables used for populating the lists
                var innerXML = string.Empty;
                var innerTEXT = string.Empty;
                string gOrderPortfolioName = string.Empty;
                string gOrderActTitle = string.Empty;
                string gOrderActComment = string.Empty;
               
                var extraString = string.Empty;
                var docTitle = string.Empty;

                int i = 0; //, k = 0, m = 0;
                var BulletString = string.Empty;
                short bulletASCIByte = 0;
                var paragraphText = string.Empty;

                //Temporary style storage for Act Title
                string gOrderActTitlebullet = string.Empty;

                //Temporary style storage for Portfolio Name
                string gOrderPortfolioNamebullet = string.Empty;

                bool hasExcept = false;
                
                //Setup base word settings, page margin etc,
                resMeta.IndentationSpacing = Convert.ToBoolean(docs.Styles["Normal"].NoSpaceBetweenParagraphsOfSameStyle);
                resMeta.marginLeft = docs.PageSetup.LeftMargin;
                resMeta.marginRight = docs.PageSetup.RightMargin;
                resMeta.pageFooterMargin = docs.PageSetup.FooterDistance;
                resMeta.pageHeaderMargin = docs.PageSetup.HeaderDistance;
                resMeta.defaultTabStop = docs.DefaultTabStop;
                resMeta.OriginalXML = docs.WordOpenXML;
                resMeta.orderFileName = fileLocation;
                //Word Document paragraph count

                GeneralOrderPortfolioViewModel previousPortfolioRes = new GeneralOrderPortfolioViewModel();
                GeneralOrderActTitleViewModel previousActTitleRes = new GeneralOrderActTitleViewModel();
                

                //This will store only the errors to be printed as a simple word document. Tgis gives the user the ability to quickly retreive the errors without creating the whole General Order
                List<string> errorListOnly = new List<string>();

                //Loop through each paragraph in the word document
                for (i = 1; i < paraCount + 1; i++)
                {
                    //Trim unnessesary characters
                    var DocPara = docs.Paragraphs[i].Range.Text.ToString().Trim('\r', '\t', '\f');

                    //Check to see if the paragraph is not just a carriage return in the document
                    if (DocPara.Length > 0)
                    {
                        //check to see if its at the end of the document, then finalize the res array
                        if (DocPara.Contains("This General Order is effective on")  //full General Order
                                                      || DocPara.Contains("This Order takes effect on"))  //Supplementary General Order
                        {
                           
                            //get the previous act title and get the ild number
                            //Add LAST act title to the array once the paragraph contains "this general order is effetive..."
                            previousActTitleRes.genOrderActComment = actCommentList;
                            if (previousActTitleRes.ildNumber != 0)
                                actTitleList.Add(previousActTitleRes);
                            actTitleListError.Add(previousActTitleRes);


                            //Once the comments are added to res1, then add res1 to res array
                            previousPortfolioRes.genOrderActTitle = actTitleList;
                            if (previousPortfolioRes.generalOrderPortfolioID != 0)
                                portfolioList.Add(previousPortfolioRes);

                            portfolioListError.GOModel.Add(previousPortfolioRes);
                            break;
                        }

                        //Check the left indent to verify if its a comment
                        switch (docs.Paragraphs[i].Range.ParagraphFormat.LeftIndent + docs.Paragraphs[i].Range.ParagraphFormat.FirstLineIndent)
                        {
                            case float m when (m >= 0 && m < 10):
                                bulletASCIByte = 996;
                                break;
                            case float m when (m > 9 && m < 23):
                                bulletASCIByte = 997;
                                break;
                            case float m when (m > 22 && m < 55):
                                bulletASCIByte = 998;
                                break;
                            case float m when (m > 54 && m < 90):
                                bulletASCIByte = 999;
                                break;
                        }


                        //check to see if its a portfolio, act or comment be looking at the indentation
                        switch (docs.Paragraphs[i].Range.ParagraphFormat.LeftIndent + docs.Paragraphs[i].Range.ParagraphFormat.FirstLineIndent)
                        {
                            //Portfolio or Act or portfolio comment
                            case float n when (n == 0):
                                //If its BOLD, then its a portfolio
                                if (docs.Paragraphs[i].Range.ParagraphFormat.Alignment == Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft && docs.Paragraphs[i].Range.Bold == -1)
                                {
                                    //Check to see if the portfolio is valid (errors can occur due to mispelling etc)
                                    portfolioSearch = DocPara.Replace("Minister for ", "").Trim().GetPortfolioInfo();
                                    //check to see if theres a valid department/portfolio relationship
                                    var port = GetPortfolios.GetPortfolioInformation(portfolioSearch.PortfolioId);
                                    
                                    //If its null, then the portfolio could not be found in the database
                                    if (portfolioSearch == null || port.Count == 0)
                                    {
                                        //Log the error, add it to the errorListOnly List and set the flag isError to true so that the system does not processs the Import but gives the user the ability to fix the document
                                        isError = true;
                                        portfolioSearch = new PortfolioViewModel()
                                        {
                                            PortfolioId = 0,
                                            PortfolioName = "Portfolio Error - " + DocPara,
                                            PrefixWithMinisterFor = DocPara.Contains("Minister For")
                                        };
                                        errorListOnly.Add(portfolioSearch.PortfolioName);
                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Error : Cannot find Portfolio {DocPara} in the Database  ");
                                        });
                                    }

                                    //Save the last entry of the acts before starting the new portfolio. 
                                    if (previousActTitleRes.generalOrderActTitle != null) //(previousAct.ActTitle != null)
                                    {
                                        //Add Act Title to the array
                                        previousActTitleRes.genOrderActComment = actCommentList;
                                        if (previousActTitleRes.ildNumber != 0)
                                            actTitleList.Add(previousActTitleRes);
                                        actTitleListError.Add(previousActTitleRes);
                                        actCommentList = new List<GeneralOrderActCommentViewModel>();
                                    }


                                    //if its a new portfolio, save the records to it, but only if portfolio != string.empty.
                                    if (previousPortfolioRes.generalOrderPortfolioName != portfolioSearch.PortfolioName && previousPortfolioRes.generalOrderPortfolioName != null) //(previousPortfolio.PortfolioName != portfolioSearch.PortfolioName && previousPortfolio.PortfolioName != null)
                                    {
                                        previousPortfolioRes.genOrderActTitle = actTitleList;

                                        if (previousPortfolioRes.generalOrderPortfolioID != 0)
                                            portfolioList.Add(previousPortfolioRes);
                                        portfolioListError.GOModel.Add(previousPortfolioRes);
                                        actTitleList = new List<GeneralOrderActTitleViewModel>();
                                    }

                                    //If its a portfolio, then start saving it into memory
                                    if (previousPortfolioRes.generalOrderPortfolioName != portfolioSearch.PortfolioName) //(previousPortfolio.PortfolioName != portfolioSearch.PortfolioName)
                                    {

                                        previousPortfolioRes = new GeneralOrderPortfolioViewModel
                                        {
                                            generalOrderPortfolioID = portfolioSearch.PortfolioId,
                                            generalOrderPortfolioName = portfolioSearch.PortfolioName,
                                            generalOrderPortfolioParagraphMeta = new DocViewModel
                                            {
                                                bulletChar = docs.Paragraphs[i].Range.ListFormat.ListString,
                                                fontBold = Convert.ToBoolean(docs.Paragraphs[i].Range.Bold),
                                                listSymbolFont = "none",
                                                pageBreakBefore = Convert.ToBoolean(docs.Paragraphs[i].Range.ParagraphFormat.PageBreakBefore),
                                                tabStopPosition = docs.Paragraphs[i].TabStops[1].Position,
                                                IndentationLeft = docs.Paragraphs[i].Range.ParagraphFormat.LeftIndent,
                                                bulletASCI = 0,
                                                IndentationRight = docs.Paragraphs[i].Range.ParagraphFormat.RightIndent,
                                                TabHangingIndent = 1,
                                                paragraphAlignment = (byte)docs.Paragraphs[i].Range.ParagraphFormat.Alignment
                                            },
                                            genOrderActTitle = new List<GeneralOrderActTitleViewModel>(actTitleList)
                                        };


                                    }
                                    previousActTitleRes = new GeneralOrderActTitleViewModel();
                                }
                                // previousAct = new ActTitleViewModel();

                                else
                                //If DocPara is an Act then Strip of the ie. "Act 2001" text and date
                                if (System.Text.RegularExpressions.Regex.IsMatch(DocPara, @"(.*?)(Act)+\s+[0-9]{4}"))// || System.Text.RegularExpressions.Regex.IsMatch(DocPara, @"(.*?)(Act)+\s+[0-9]{4}") || System.Text.RegularExpressions.Regex.IsMatch(DocPara, @"(.*?)(Act)+\s+[0-9]{4} - Except:"))
                                {
                                    //check to see if this Act exists, replace the appostrphe as its a special character not recognised in the database string search
                                    if (DocPara.Contains("’"))
                                        DocPara = DocPara.Replace("’", "'");
                                    //Check to see if its a valid Act title. Use regular expression to check to see if it has the nessesary format
                                    activitySearch = System.Text.RegularExpressions.Regex.Match(DocPara, @"(.*?)(Act)+\s+[0-9]{4}").ToString().GetActTitleInfo();

                                    //If it cannot find the Act, due to mispelling etc, add it to the errorListOnly and log it
                                    if (activitySearch == null)
                                    {
                                        isError = true;
                                        activitySearch = new ActTitleViewModel()
                                        {
                                            ActNumber = 0,
                                            ActTitle = "Act Error - " + DocPara,
                                            ActTitleILDNumber = 0
                                        };
                                        errorListOnly.Add(activitySearch.ActTitle);

                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Error : Cannot find Act Title {DocPara} in the Database  ");
                                        });
                                    }

                                    //If its a new act title paragraph, store it in the previousActTitleRes List
                                    if (activitySearch.ActTitle != previousActTitleRes.generalOrderActTitle && previousActTitleRes.generalOrderActTitle != null) //(activitySearch.ActTitle != previousAct.ActTitle && previousAct.ActTitle != null)
                                    {
                                        //Add Act Title to the array
                                        previousActTitleRes.genOrderActComment = actCommentList;
                                        if (previousActTitleRes.ildNumber != 0)
                                            actTitleList.Add(previousActTitleRes);
                                        actTitleListError.Add(previousActTitleRes);
                                        actCommentList = new List<GeneralOrderActCommentViewModel>();
                                    }

                                    //If its a new act, not the previous, then create a new object
                                    if (activitySearch.ActTitle != previousActTitleRes.generalOrderActTitle) // (activitySearch.ActTitle != previousAct.ActTitle)
                                    {
                                        hasExcept = false;
                                        if (DocPara.Contains("Except"))
                                            hasExcept = true;
                                        
                                        previousActTitleRes = new GeneralOrderActTitleViewModel
                                        {
                                            //  generalOrderActTitleModelID = gOrderActTitleId,
                                            generalOrderActTitle = activitySearch.ActTitle,
                                            ildNumber = activitySearch.ActTitleILDNumber,
                                            isExcept = hasExcept,
                                            generalOrderActTitleParagraphMeta = new DocViewModel
                                            {
                                                bulletChar = docs.Paragraphs[i].Range.ListFormat.ListString,
                                                fontBold = Convert.ToBoolean(docs.Paragraphs[i].Range.Bold),
                                                listSymbolFont = "none",
                                                pageBreakBefore = Convert.ToBoolean(docs.Paragraphs[i].Range.ParagraphFormat.PageBreakBefore),
                                                tabStopPosition = docs.Paragraphs[i].TabStops[1].Position,
                                                bulletASCI = bulletASCIByte, // (byte)docs.Paragraphs[i].Range.ListFormat.ListString
                                                IndentationLeft = docs.Paragraphs[i].Range.ParagraphFormat.LeftIndent,
                                                IndentationRight = docs.Paragraphs[i].Range.ParagraphFormat.RightIndent,
                                                indentationBy = 0,
                                                TabHangingIndent = 1,
                                                paragraphAlignment = (byte)docs.Paragraphs[i].Range.ParagraphFormat.Alignment

                                            },
                                            genOrderActComment = new List<GeneralOrderActCommentViewModel>(actCommentList)
                                        };
                                    }
                                }
                                else
                                {
                                    //If the first porfolio has already been read. We check this so we can ignore the first page of the General Order
                                    if (previousPortfolioRes.generalOrderPortfolioName != null) //(previousPortfolio.PortfolioName != null)
                                    {
                                        //this will pick up Act Comments in LeftIndent == 0 that are already past the first page of text so it will be valid comment ie
                                        //(The Act is otherwise administered by the Minister for Education, the Minister for Families and Children and the Minister for Training and Skills)
                                        //Acts at the portfolio leftindent will start with "(".
                                        if (DocPara.Substring(0, 1).Contains("("))
                                        {

                                            actCommentList.Add(new GeneralOrderActCommentViewModel
                                            {
                                                generalOrderActComment = DocPara, // BulletString + DocPara,
                                                generalOrderActCommentParagraphMeta = new DocViewModel
                                                {
                                                    bulletChar = docs.Paragraphs[i].Range.ListFormat.ListString,
                                                    fontBold = Convert.ToBoolean(docs.Paragraphs[i].Range.Bold),
                                                    pageBreakBefore = Convert.ToBoolean(docs.Paragraphs[i].Range.ParagraphFormat.PageBreakBefore),
                                                    tabStopPosition = docs.Paragraphs[i].TabStops[1].Position,
                                                    IndentationLeft = docs.Paragraphs[i].Range.ParagraphFormat.LeftIndent,
                                                    bulletASCI = bulletASCIByte,
                                                    IndentationRight = docs.Paragraphs[i].Range.ParagraphFormat.RightIndent,
                                                    indentationBy = docs.Paragraphs[i].Range.ParagraphFormat.FirstLineIndent,
                                                    TabHangingIndent = 1,  // Convert.ToInt16(docs.Paragraphs[i].Range.ParagraphFormat.FirstLineIndent == 0 ? 0 : 1),
                                                    paragraphAlignment = (byte)docs.Paragraphs[i].Range.ParagraphFormat.Alignment,
                                                    listSymbolFont = docs.Paragraphs[i].Range.ListFormat.ListTemplate != null ? docs.Paragraphs[i].Range.ListFormat.ListTemplate.ListLevels[docs.Paragraphs[i].Range.ListFormat.ListLevelNumber].Font.Name : "none"
                                                }
                                            });
                                        }
                                        else
                                        {
                                            //Check if its a Appropriation or supply act or other. These are acts that dont have 'Act 2xxx' appended to them ie 'Legal Profession Uniform Law (Victoria)'
                                            isError = true;
                                            activitySearch = new ActTitleViewModel()
                                            {
                                                ActNumber = 0,
                                                ActTitle = "Act Error - " + DocPara,
                                                ActTitleILDNumber = 0
                                            };
                                            errorListOnly.Add(activitySearch.ActTitle);

                                            //Dont add the error to the list that way we can upload all the correct portfolios/acts and report on the ones that didnt go through
                                            //previousActTitleRes = new GeneralOrderActTitleViewModel
                                            //{
                                            //    generalOrderActTitle = activitySearch.ActTitle,
                                            //    ildNumber = activitySearch.ActTitleILDNumber,
                                            //    isExcept = hasExcept,
                                            //    generalOrderActTitleParagraphMeta = new DocViewModel
                                            //    {
                                            //        bulletChar = docs.Paragraphs[i].Range.ListFormat.ListString,
                                            //        fontBold = Convert.ToBoolean(docs.Paragraphs[i].Range.Bold),
                                            //        listSymbolFont = "none",
                                            //        pageBreakBefore = Convert.ToBoolean(docs.Paragraphs[i].Range.ParagraphFormat.PageBreakBefore),
                                            //        tabStopPosition = docs.Paragraphs[i].TabStops[1].Position,
                                            //        bulletASCI = bulletASCIByte, // (byte)docs.Paragraphs[i].Range.ListFormat.ListString
                                            //        IndentationLeft = docs.Paragraphs[i].Range.ParagraphFormat.LeftIndent,
                                            //        IndentationRight = docs.Paragraphs[i].Range.ParagraphFormat.RightIndent,
                                            //        indentationBy = 0,
                                            //        TabHangingIndent = 1,
                                            //        paragraphAlignment = (byte)docs.Paragraphs[i].Range.ParagraphFormat.Alignment

                                            //    },
                                            //    genOrderActComment = new List<GeneralOrderActCommentViewModel>(actCommentList)
                                            //};
                                        }
                                    }
                                }
                                break;
                            //Acts and Comments
                            case float a when (a > 0):

                                if (activitySearch.ActNumber != 0)
                                {
                                    BulletString = docs.Paragraphs[i].Range.ListFormat.ListString;


                                    for (int b = 0; b < BulletString.Trim().Length; b++)
                                    {
                                        bulletASCIByte = (byte)BulletString[b];
                                    }

                                    //Add the Act Comments to the array
                                    //Here we make sure that we switch around the value of FirstLineIndent to LeftIndent as per above statement

                                    actCommentList.Add(new GeneralOrderActCommentViewModel
                                    {

                                        //NOTE: Selection.ParagraphFormat.TabHangingIndent (1) will show "HangingIndent" while
                                        // Selection.ParagraphFormat.TabHangingIndent (-1) will show "FirstLine" while not calling
                                        // Selection.ParagraphFormat.TabHangingIndent will shoe "None"
                                        generalOrderActComment = DocPara, // BulletString + DocPara,
                                        generalOrderActCommentParagraphMeta = new DocViewModel
                                        {
                                            bulletChar = docs.Paragraphs[i].Range.ListFormat.ListString,
                                            fontBold = Convert.ToBoolean(docs.Paragraphs[i].Range.Bold),
                                            pageBreakBefore = Convert.ToBoolean(docs.Paragraphs[i].Range.ParagraphFormat.PageBreakBefore),
                                            tabStopPosition = docs.Paragraphs[i].TabStops[1].Position,
                                            //subtract the leftindent by the negative firstlineindent to give a true representation of leftindent
                                            IndentationLeft = docs.Paragraphs[i].Range.ParagraphFormat.LeftIndent,
                                            bulletASCI = bulletASCIByte, // (byte)docs.Paragraphs[i].Range.ListFormat.ListString
                                            IndentationRight = docs.Paragraphs[i].Range.ParagraphFormat.RightIndent,
                                            //FirstLineIndent and  Indentation are added/subtracted to give the tab spacing along with setting hanging indent
                                            indentationBy = 0, //docs.Paragraphs[m].Range.ParagraphFormat.FirstLineIndent,
                                            TabHangingIndent = Convert.ToInt16(docs.Paragraphs[i].Range.ParagraphFormat.FirstLineIndent == 0 ? 0 : 1),
                                            paragraphAlignment = (byte)docs.Paragraphs[i].Range.ParagraphFormat.Alignment,
                                            listSymbolFont = docs.Paragraphs[i].Range.ListFormat.ListTemplate != null ? docs.Paragraphs[i].Range.ListFormat.ListTemplate.ListLevels[docs.Paragraphs[i].Range.ListFormat.ListLevelNumber].Font.Name : "none"
                                        }
                                    });
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                docs.Close(false);
                word.Quit(false);
                //Add res object to resMeta.GOModel object
                resMeta.GOModel = portfolioList;

                if (errorListOnly.Count > 0)
                {
                    if (MessageBoxHelper.DisplayMessageBox("General Order Error", "Errors were found with the General Order document your trying to import.\nThe system will now generate a report.\nPress 'YES' to print a quick error list.\n OR 'NO' to exit"))
                        await SelectGenerateErrorList(errorListOnly);
                    if (MessageBoxHelper.DisplayMessageBox("General Order", "Do you wish to continue to import the correct Portfolios and Acts?\nPress 'YES' to continue or 'NO' to exit."))
                        isError = false;
                    else
                        isError = true;
                }

                return Tuple.Create(isError, resMeta);
            }
            catch (Exception e)
            {
                docs.Close(false);
                word.Quit(false);
                MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- DocReader.WordDocReader -- { e.Message} -- Contact IT support.");
                return null;
            }
        }



        /// <summary>
        /// This handles printing of the Error Report for Acts and Portfolio that are not found due to the document having spelling errors ect
        /// </summary>
        /// <param name="errorListOnly"></param>
        /// <returns></returns>
        private async static Task SelectGenerateErrorList(List<string> errorListOnly)
        {
            string WordDocErrListFilename = string.Empty;
            Application.Current.Dispatcher.Invoke(() =>
            {
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- Selected Creating ERROR LIST document ");
            });
            await Task.Run(() => WordDocErrListFilename = Reporting.CreateOriginalDoc.ErrorListDocCreator(errorListOnly));
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (WordDocErrListFilename != string.Empty)
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished creating ERROR LIST document - {WordDocErrListFilename}");
                else
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancelled creating ERROR LIST document - {WordDocErrListFilename}");
            });
        }



        /// <summary>
        /// This handles the printing of reports that reside in WindowViewModel.instance.resMeta. This gives the user the ability to re generate the report based on the model
        /// </summary>
        /// <returns></returns>
        private async static Task SelectGenerateNewGO()
        {
            string WordDocCreatorFilename = string.Empty;
            Application.Current.Dispatcher.Invoke(() =>
            {
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Selected Creating ERROR GO document ");
            });
            await Task.Run(() => WordDocCreatorFilename = Reporting.CreateOriginalDoc.WordDocCreator(portfolioListError));
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (WordDocCreatorFilename != string.Empty)
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished creating ERROR {(WindowViewModel.instance.resMeta.orderfull ? "Full" : "Partial")} GO document - {WordDocCreatorFilename}");
                else
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancelled creating ERROR {(WindowViewModel.instance.resMeta.orderfull ? "Full" : "Partial")} GO document - {WordDocCreatorFilename}");

            });
        }
    }
}

