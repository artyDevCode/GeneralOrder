using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

//Original code that prompts the user to select an Act Tilte (GOImportFileSelectAct.xaml) if it cannot find one
/// <summary>
/// NOT USED AS IT NOW WRITES UP A REPORT IN DOCREADER.CS
/// </summary>
namespace GeneralOrder
{
    public class DocumentReader
    {
        public static ActTitleViewModel activitySearch { get; set; }
        public static byte gOrderActTitlebulletASCI { get; set; } = 0;
        /// <summary>
        /// Read a Word document to extract a range of paragraphs with style. This then gets saved to an object <see cref="GeneralOrderMetadataViewModel"/>  and gets passed to other classes 
        /// that either save the data into a Database or recreates the document.
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <param name="headingText"></param>
        /// <param name="headingStyle"></param>
        public static GeneralOrderMetadataViewModel WordDocReader(string fileLocation) //, string headingText, string headingStyle)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;
            object path = fileLocation;
            object readOnly = true;
            Microsoft.Office.Interop.Word.Document docs = null;
            try
            {

                docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                // docs.Activate();
                //Comments switch statement that includes a range
                #region properties
                GeneralOrderMetadataViewModel resMeta = new GeneralOrderMetadataViewModel();

                List<GeneralOrderPortfolioViewModel> res = new List<GeneralOrderPortfolioViewModel>();
                List<GeneralOrderActTitleViewModel> res1 = new List<GeneralOrderActTitleViewModel>();
                List<GeneralOrderActCommentViewModel> res2 = new List<GeneralOrderActCommentViewModel>();
              
                activitySearch = new ActTitleViewModel();


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
                int gOrderPortfolioNameId = 1;
                int gOrderActTitleId = 1;

                var extraString = string.Empty;
                var docTitle = string.Empty;
                bool finished = false; //sets a flag when reading the xml document is finished    

                int i = 0, k = 0, m = 0;
                var BulletString = string.Empty;
                short bulletASCIByte = 0;
                var paragraphText = string.Empty;

                //Temporary style storage for Act Title
                string gOrderActTitlebullet = string.Empty;
                bool gOrderActTitlebold = false;
                bool gOrderActTitlepageBreakBefore = false;
                float gOrderActTitletextPosition = 0;
                float gOrderActTitleleftIndent = 0;

                float gOrderActTitleLeftIndent = 0;
                float gOrderActTitleRightIndent = 0;
                float gOrderActTitleIndentationBy = 0;
                short gOrderActTitleTabHangingIndent = 1;
                byte gOrderActTitleParagraphAlignment = 0;

                //Temporary style storage for Portfolio Name
                string gOrderPortfolioNamebullet = string.Empty;
                bool gOrderPortfolioNamebold = false;
                bool gOrderPortfolioNamepageBreakBefore = false;
                float gOrderPortfolioNametextPosition = 0;
                byte gOrderPortfolioNamebulletASCI = 0;
                float gOrderPortfolioNameIndentationLeft = 0;
                float gOrderPortfolioNameIndentationRight = 0;
                float gOrderPortfolioNameIndentationBy = 0;
                short gOrderPortfolioNameTabHangingIndent = 1;
                byte gOrderPortfolioNameParagraphAlignment = 0;
                bool hasExcept = false;
                #endregion

                //Setup base word settings, page margin etc,
                #region metadata
                resMeta.IndentationSpacing = Convert.ToBoolean(docs.Styles["Normal"].NoSpaceBetweenParagraphsOfSameStyle);
                resMeta.marginLeft = docs.PageSetup.LeftMargin;
                resMeta.marginRight = docs.PageSetup.RightMargin;
                resMeta.pageFooterMargin = docs.PageSetup.FooterDistance;
                resMeta.pageHeaderMargin = docs.PageSetup.HeaderDistance;
                resMeta.defaultTabStop = docs.DefaultTabStop;
                resMeta.OriginalXML = docs.WordOpenXML;
                resMeta.orderFileName = fileLocation;
                #endregion

                //Word Document paragraph count
                for (i = 1; i < paraCount + 1; i++)
                {
                    var DocPara = docs.Paragraphs[i].Range.Text.ToString().Trim('\r', '\t', '\f');
                    if (DocPara.Length > 0)
                    {
                        //loop through getting the portfolio
                        //var portfolioSearch = resultPortfolio.Find(r => r.PortfolioName == DocPara.Replace("Minister for ", "").Trim());
                        var portfolioSearch = DocPara.Replace("Minister for ", "").Trim().GetPortfolioInfo();

                        gOrderPortfolioName = gOrderActComment = gOrderActTitle = "";

                        if (portfolioSearch != null)
                        {
                            gOrderPortfolioNameId = portfolioSearch.PortfolioId;
                            gOrderPortfolioName = DocPara;

                            gOrderPortfolioNamebullet = docs.Paragraphs[i].Range.ListFormat.ListString;
                            gOrderPortfolioNamebold = Convert.ToBoolean(docs.Paragraphs[i].Range.Bold);
                            gOrderPortfolioNamepageBreakBefore = Convert.ToBoolean(docs.Paragraphs[i].Range.ParagraphFormat.PageBreakBefore);
                            gOrderPortfolioNametextPosition = docs.Paragraphs[i].TabStops[1].Position;
                            gOrderPortfolioNamebulletASCI = 0;
                            gOrderPortfolioNameParagraphAlignment = (byte)docs.Paragraphs[i].Range.ParagraphFormat.Alignment; // Convert.ToInt32(docs.Paragraphs[i].Range.ParagraphFormat.Alignment);
                            gOrderPortfolioNameIndentationLeft = docs.Paragraphs[i].Range.ParagraphFormat.LeftIndent;
                            gOrderPortfolioNameIndentationRight = docs.Paragraphs[i].Range.ParagraphFormat.RightIndent;
                            gOrderPortfolioNameIndentationBy = 0; //docs.Paragraphs[i].Range.ParagraphFormat.FirstLineIndent;
                            gOrderPortfolioNameTabHangingIndent = 1; //HangingIndent // Convert.ToInt16(gOrderPortfolioNameIndentationBy == 0 ? -1 : 1); 

                            //get the Portfolio details and save the meta                 
                            for (k = i + 1; k < paraCount + 1; k++)
                            {
                                DocPara = docs.Paragraphs[k].Range.Text.ToString().Trim('\r', '\t');

                                if (DocPara.Length > 0)
                                {
                                    //Check to make sure that the new paragraph in the loop is not a portfolioi
                                    // portfolioSearch = resultPortfolio.Find(r => r.PortfolioName == DocPara.Replace("Minister for ", "").Trim());                                                    
                                    portfolioSearch = DocPara.Replace("Minister for ", "").Trim().GetPortfolioInfo();

                                    hasExcept = false;
                                    if (DocPara.Contains("Except"))
                                        hasExcept = true;


                                    //If DocPara is an Act then Strip of the ie. "Act 2001" text and date
                                    innerTEXT = System.Text.RegularExpressions.Regex.Match(DocPara, @"(.*?)(Act)+\s+[0-9]{4}").ToString();

                                    ////The apostrophe from the document is different from c#. Change it.

                                    activitySearch = ValidateAct(innerTEXT, docs.Paragraphs[k].Range.ParagraphFormat.LeftIndent);
                                    //if (activitySearch != null)
                                    //    docs.Paragraphs[k].Range.Text = activitySearch.ActTitle + " –\r"; //reset just in case it was incorrect in the actual document

                                    if (activitySearch != null && portfolioSearch == null)
                                    {
                                        //Get Act metadata
                                        gOrderActTitle = activitySearch.ActTitle;  //DocPara; 

                                        //  gOrderActTitleId = activitySearch.ActTitleILDNumber;
                                        gOrderActTitlebullet = docs.Paragraphs[k].Range.ListFormat.ListString;
                                        gOrderActTitlebold = Convert.ToBoolean(docs.Paragraphs[k].Range.Bold);
                                        gOrderActTitlepageBreakBefore = Convert.ToBoolean(docs.Paragraphs[k].Range.ParagraphFormat.PageBreakBefore);
                                        gOrderActTitletextPosition = docs.Paragraphs[k].TabStops[1].Position;
                                        gOrderActTitlebulletASCI = 0;
                                        gOrderActTitleLeftIndent = docs.Paragraphs[k].Range.ParagraphFormat.LeftIndent;
                                        gOrderActTitleRightIndent = docs.Paragraphs[k].Range.ParagraphFormat.RightIndent;
                                        //This is set to 0 as Word caculates the tab space using the TabHangingIndent
                                        gOrderActTitleIndentationBy = 0; // docs.Paragraphs[k].Range.ParagraphFormat.FirstLineIndent;
                                        gOrderActTitleParagraphAlignment = (byte)docs.Paragraphs[k].Range.ParagraphFormat.Alignment;
                                        gOrderActTitleTabHangingIndent = 1; //HangingIndent // Convert.ToInt16(gOrderActTitleIndentationBy == 0 ? -1 : 1);

                                        //get the act comments and save the meta
                                        for (m = k + 1; m < paraCount + 1; m++)
                                        {
                                            DocPara = docs.Paragraphs[m].Range.Text.ToString().Trim('\r', '\t', '\f');
                                            if (DocPara.Length > 0)
                                            {
                                                //Check to make sure that the new paragraph in the loop is not a portfolio
                                                //portfolioSearch = resultPortfolio.Find(r => r.PortfolioName == DocPara.Replace("Minister for ", "").Trim());
                                                portfolioSearch = DocPara.Replace("Minister for ", "").Trim().GetPortfolioInfo();


                                                //If DocPara is an Act then Strip of the ie. "Act 2001" text and date
                                                innerTEXT = System.Text.RegularExpressions.Regex.Match(DocPara, @"(.*?)(Act)+\s+[0-9]{4}").ToString();


                                                //Check to see if its an Act
                                                //The apostrtophe from the document is different from c#. Change it.  Check to see if its an Act
                                                //Need to check the value of the left spacing to deteremine which non bullet ASCII it requires as all bulleted comments and including the act title has comments
                                                activitySearch = ValidateAct(innerTEXT, docs.Paragraphs[m].Range.ParagraphFormat.LeftIndent); //This is where it should give the user the chance to chage it at the docs.Paragraphs[m].Range.Text level
                                                if (activitySearch != null)
                                                {

                                                    var oldParagraphFormat = docs.Paragraphs[m].Range.ParagraphFormat.Duplicate;
                                                    docs.Paragraphs[m].Range.Delete();
                                                    docs.Paragraphs[m].Range.Text = activitySearch.ActTitle;
                                                    docs.Paragraphs[m].Range.ParagraphFormat = oldParagraphFormat;

                                                    /////////////////////////

                                                    //var oldParagraphFormat = docs.Paragraphs[m].Range.ParagraphFormat.Duplicate;
                                                    //var rngRange = docs.Range(docs.Paragraphs[m].Range.Start, docs.Paragraphs[m].Range.End);
                                                    //rngRange.Delete();
                                                    //rngRange.InsertBefore(activitySearch.ActTitle);
                                                    //rngRange.ParagraphFormat = oldParagraphFormat;

                                                    ///////////////////////////

                                                    //Microsoft.Office.Interop.Word.Range range = docs.Paragraphs[m].Range;
                                                    //var oldParagraphFormat = docs.Paragraphs[m].Range.ParagraphFormat.Duplicate;

                                                    //range.Text = activitySearch.ActTitle;
                                                    //range.ParagraphFormat = oldParagraphFormat;

                                                    ///////////////////////

                                                    //Microsoft.Office.Interop.Word.Range rngPara = docs.Paragraphs[m].Range;
                                                    //object unitCharacter = Microsoft.Office.Interop.Word.WdUnits.wdCharacter;
                                                    //object backOne = -1;
                                                    //rngPara.MoveEnd(ref unitCharacter, ref backOne);
                                                    //rngPara.Text = null;
                                                    //rngPara.Text = activitySearch.ActTitle;

                                                    //////////////////////

                                                    //docs.Paragraphs[m].Range.Text = activitySearch.ActTitle + "-\r"; //reset just in case it was incorrect in the actual document
                                                    //docs.Paragraphs[m].Range.ParagraphFormat.LeftIndent = 0; //this is set back to 0 because for some reason, as soon as theActTitle is set, it changes leftIndent to 36
                                                }
                                                //If both Act and Portfolio are null, it must be an Act comment
                                                if (activitySearch == null && portfolioSearch == null)
                                                {
                                                    //This checks to see if the Act Comments contains the effective date clause. If it does, save the date and break out of the loop.
                                                    if (DocPara.Contains("This General Order is effective on")  //full General Order
                                                        || DocPara.Contains("This Order takes effect on"))  //Supplementary General Order
                                                    {
                                                        var effDate = System.Text.RegularExpressions.Regex.Match(DocPara, @"\d{2}\s+.{3}\s+[0-9]{4}");

                                                        finished = true;
                                                        activitySearch = ValidateAct(gOrderActTitle, docs.Paragraphs[m].Range.ParagraphFormat.LeftIndent);

                                                        //get the previous act title and get the ild number
                                                        //Add LAST act title to the array once the paragraph contains "this general order is effetive..."
                                                        res1.Add(new GeneralOrderActTitleViewModel
                                                        {
                                                            // generalOrderActTitleModelID = gOrderActTitleId,
                                                            generalOrderActTitle = activitySearch.ActTitle,
                                                            ildNumber = activitySearch.ActTitleILDNumber,
                                                            isExcept = hasExcept,
                                                            generalOrderActTitleParagraphMeta = new DocViewModel
                                                            {
                                                                bulletChar = gOrderActTitlebullet,
                                                                fontBold = gOrderActTitlebold,
                                                                pageBreakBefore = gOrderActTitlepageBreakBefore,
                                                                tabStopPosition = gOrderActTitletextPosition,
                                                                bulletASCI = gOrderActTitlebulletASCI,
                                                                IndentationLeft = gOrderActTitleleftIndent,
                                                                IndentationRight = gOrderActTitleRightIndent,
                                                                indentationBy = gOrderActTitleIndentationBy,
                                                                TabHangingIndent = gOrderActTitleTabHangingIndent,
                                                                paragraphAlignment = gOrderActTitleParagraphAlignment,
                                                                listSymbolFont = "none"
                                                            },
                                                            genOrderActComment = new List<GeneralOrderActCommentViewModel>(res2)
                                                        });

                                                        break;
                                                    }
                                                    //Its an Act Comment
                                                    BulletString = docs.Paragraphs[m].Range.ListFormat.ListString;

                                                    //Need to check the value of the left spacing to deteremine which non bullet ASCII it requires as all bulleted comments and including the act title has comments
                                                    if (BulletString == "")
                                                    {
                                                        switch (docs.Paragraphs[m].Range.ParagraphFormat.LeftIndent + docs.Paragraphs[m].Range.ParagraphFormat.FirstLineIndent)
                                                        {
                                                            case float n when (n >= 0 && n < 10):
                                                                bulletASCIByte = 996;
                                                                break;
                                                            case float n when (n > 9 && n < 23):
                                                                bulletASCIByte = 997;
                                                                break;
                                                            case float n when (n > 22 && n < 55):
                                                                bulletASCIByte = 998;
                                                                break;
                                                            case float n when (n > 54 && n < 90):
                                                                bulletASCIByte = 999;
                                                                break;
                                                        }
                                                    }
                                                    //  bulletASCIByte = 999; 


                                                    for (int b = 0; b < BulletString.Trim().Length; b++)
                                                    {
                                                        bulletASCIByte = (byte)BulletString[b];
                                                    }

                                                    //Add the Act Comments to the array
                                                    res2.Add(new GeneralOrderActCommentViewModel
                                                    {
                                                        //NOTE: Selection.ParagraphFormat.TabHangingIndent (1) will show "HangingIndent" while
                                                        // Selection.ParagraphFormat.TabHangingIndent (-1) will show "FirstLine" while not calling
                                                        // Selection.ParagraphFormat.TabHangingIndent will shoe "None"
                                                        generalOrderActComment = DocPara, // BulletString + DocPara,
                                                        generalOrderActCommentParagraphMeta = new DocViewModel
                                                        {
                                                            bulletChar = docs.Paragraphs[m].Range.ListFormat.ListString,
                                                            fontBold = Convert.ToBoolean(docs.Paragraphs[m].Range.Bold),
                                                            pageBreakBefore = Convert.ToBoolean(docs.Paragraphs[m].Range.ParagraphFormat.PageBreakBefore),
                                                            tabStopPosition = docs.Paragraphs[m].TabStops[1].Position,
                                                            //subtract the leftindent by the negative firstlineindent to give a true representation of leftindent
                                                            IndentationLeft = docs.Paragraphs[m].Range.ParagraphFormat.LeftIndent + docs.Paragraphs[m].Range.ParagraphFormat.FirstLineIndent,
                                                            bulletASCI = bulletASCIByte, // (byte)docs.Paragraphs[i].Range.ListFormat.ListString
                                                            IndentationRight = docs.Paragraphs[m].Range.ParagraphFormat.RightIndent,
                                                            //FirstLineIndent and  Indentation are added/subtracted to give the tab spacing along with setting hanging indent
                                                            indentationBy = 0, //docs.Paragraphs[m].Range.ParagraphFormat.FirstLineIndent,
                                                            TabHangingIndent = Convert.ToInt16(docs.Paragraphs[m].Range.ParagraphFormat.FirstLineIndent == 0 ? 0 : 1),
                                                            //paragraphAlignment = Convert.ToInt32(docs.Paragraphs[m].Range.ParagraphFormat.Alignment),
                                                            paragraphAlignment = (byte)docs.Paragraphs[m].Range.ParagraphFormat.Alignment,
                                                            listSymbolFont = docs.Paragraphs[m].Range.ListFormat.ListTemplate != null ? docs.Paragraphs[m].Range.ListFormat.ListTemplate.ListLevels[docs.Paragraphs[m].Range.ListFormat.ListLevelNumber].Font.Name : "none"
                                                        }
                                                    });
                                                }
                                                else
                                                {
                                                    //Its not a comment, so must be the end of the last comment paragraph so save it to its related Act
                                                    //get the previous act title and get the ild number
                                                    //The apostrtophe from the document is different from c#. Change it.
                                                    if (gOrderActTitle.Length > 0)
                                                    {
                                                        activitySearch = ValidateAct(gOrderActTitle, 0);


                                                        //Add Act Title to the array

                                                        res1.Add(new GeneralOrderActTitleViewModel
                                                        {
                                                            //  generalOrderActTitleModelID = gOrderActTitleId,
                                                            generalOrderActTitle = activitySearch.ActTitle,
                                                            ildNumber = activitySearch.ActTitleILDNumber,
                                                            isExcept = hasExcept,
                                                            generalOrderActTitleParagraphMeta = new DocViewModel
                                                            {
                                                                bulletChar = gOrderActTitlebullet,
                                                                fontBold = gOrderActTitlebold,
                                                                listSymbolFont = "none",
                                                                pageBreakBefore = gOrderActTitlepageBreakBefore,
                                                                tabStopPosition = gOrderActTitletextPosition,
                                                                bulletASCI = gOrderActTitlebulletASCI, // (byte)docs.Paragraphs[i].Range.ListFormat.ListString
                                                                IndentationLeft = gOrderActTitleleftIndent,
                                                                IndentationRight = gOrderActTitleRightIndent,
                                                                indentationBy = gOrderActTitleIndentationBy,
                                                                TabHangingIndent = gOrderActTitleTabHangingIndent,
                                                                paragraphAlignment = gOrderActTitleParagraphAlignment

                                                            },
                                                            genOrderActComment = new List<GeneralOrderActCommentViewModel>(res2)
                                                        });
                                                        res2 = new List<GeneralOrderActCommentViewModel>();
                                                    }
                                                    m--;
                                                    k = m;
                                                    break;

                                                }
                                            }
                                        }

                                    }
                                    if (portfolioSearch != null || finished)
                                    {
                                        res.Add(new GeneralOrderPortfolioViewModel
                                        {
                                            generalOrderPortfolioID = gOrderPortfolioNameId,
                                            generalOrderPortfolioName = gOrderPortfolioName,
                                            generalOrderPortfolioParagraphMeta = new DocViewModel
                                            {
                                                bulletChar = gOrderPortfolioNamebullet,
                                                fontBold = gOrderPortfolioNamebold,
                                                listSymbolFont = "none",
                                                pageBreakBefore = gOrderPortfolioNamepageBreakBefore,
                                                tabStopPosition = gOrderPortfolioNametextPosition,
                                                IndentationLeft = gOrderPortfolioNameIndentationLeft,
                                                bulletASCI = gOrderPortfolioNamebulletASCI, // (byte)docs.Paragraphs[i].Range.ListFormat.ListString
                                                IndentationRight = gOrderPortfolioNameIndentationRight,
                                                indentationBy = gOrderPortfolioNameIndentationBy,
                                                TabHangingIndent = gOrderActTitleTabHangingIndent,
                                                paragraphAlignment = (byte)gOrderPortfolioNameParagraphAlignment
                                            },
                                            genOrderActTitle = new List<GeneralOrderActTitleViewModel>(res1)
                                        });
                                        res1 = new List<GeneralOrderActTitleViewModel>();
                                        k--;
                                        break;
                                    }


                                }
                                if (finished)
                                {
                                    break;
                                }
                            }

                            i = k;


                        }
                        if (finished)
                        {
                            break;
                        }
                    }
                }
                docs.Close(false);
                word.Quit(false);
                //Add res object to resMeta.GOModel object
                resMeta.GOModel = res;

                return resMeta;
            }

            catch (Exception e)
            {
                MessageBoxHelper.CatchExceptionMessageBox(e.Message);
                return null;
            }
        }

        private static ActTitleViewModel ValidateAct(string value, float leftIndentation)
        {

            activitySearch = null;
            if (value.Contains("’"))
            {
                activitySearch = value.Replace("’", "'").GetActTitleInfo();
            }
            else
            {
                //Check to see if its an Act
                if (gOrderActTitlebulletASCI == 0 && value != "" && leftIndentation == 0)
                {
                    activitySearch = System.Text.RegularExpressions.Regex.Match(value, @"(.*?)(Act)+\s+[0-9]{4}").ToString().GetActTitleInfo();

                    if (activitySearch == null)
                    {
                        //  Application.Current.Dispatcher.Invoke
                        //(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
                        Application.Current.Dispatcher.Invoke(() =>
                          {

                              var dialog = new GOImportFileSelectAct();

                              GOImportFileSelectActVM.instance.verifyActText = value;
                              GOImportFileSelectActVM.instance.StartActWindow();
                              dialog.Topmost = true;
                              dialog.WindowState = WindowState.Normal;
                              WindowViewModel.instance.mWindow.Topmost = false;
                              dialog.Owner = WindowViewModel.instance.mWindow;
                              dialog.AllowsTransparency = false;
                              // dialog.Focus();
                              // dialog.Activate();
                              dialog.ShowDialog();

                              dialog.Close();
                              activitySearch = new ActTitleViewModel();

                              //change this as it prechecks the Act so it does display the popup twice
                              //docs.Paragraphs[m].Range.Text = GOImportFileSelectActVM.instance.selectText.ActTitle; //test
                              activitySearch.ActTitle = GOImportFileSelectActVM.instance.selectText.ActTitle;
                              activitySearch.ActNumber = GOImportFileSelectActVM.instance.selectText.ActNumber;
                              activitySearch.ActTitleILDNumber = GOImportFileSelectActVM.instance.selectText.ActTitleILDNumber;
                              WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Error with Act Title:  {value} - Replaced with Act Title {activitySearch.ActTitle} " );

                          });

                    }
                }
            }
            return activitySearch;
        }

    }
}
