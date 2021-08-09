using ApplicationLogger;
using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;

namespace GeneralOrder
{   ///*********************************************************************************************************************************
    /// <summary>
    /// NOT USING THIS ONE **************************************************************************************
    /// </summary>
    /// *********************************************************************************************************************************
    public static class WordParser
    {
        public static GeneralOrderMetadataViewModel loadWordML(this FlowDocument flowDoc, string fileName) //, IEnumerable<string> headingText, IEnumerable<string> headingStyle)
        {
            object miss = System.Reflection.Missing.Value;
            //object path = fileLocation;
            object readOnly = true;
            string gOrderPortfolioName = string.Empty;
            string gOrderActTitle = string.Empty;
            string gOrderActComment = string.Empty;
            int gOrderPortfolioNameId = 1;
            int gOrderActTitleId = 1;
            DateTime gOrderEffective = DateTime.MinValue;
            bool gOrderfull = false;

            int i = 0, k = 0;
            GeneralOrderMetadataViewModel resMeta = new GeneralOrderMetadataViewModel();
            List<GeneralOrderPortfolioViewModel> res = new List<GeneralOrderPortfolioViewModel>();
            List<GeneralOrderActTitleViewModel> res1 = new List<GeneralOrderActTitleViewModel>();

            //Get the portfolios from ILD.NET database
            var resultPortfolio = GetPortfolios.GetPortfolio();
          //  var resultActTitle = GetActTitles.GetActTitle();

            XElement wordDoc = null;
            XElement wordDocStyle = null;


            try
            {
                Package package = Package.Open(fileName);
                Uri documentUri = new Uri("/word/document.xml", UriKind.Relative);
                PackagePart documentPart = package.GetPart(documentUri);
                wordDoc = XElement.Load(new StreamReader(documentPart.GetStream()));

                Uri documentUriStyle = new Uri("/word/styles.xml", UriKind.Relative);
                PackagePart documentPartStyle = package.GetPart(documentUriStyle);
                wordDocStyle = XElement.Load(new StreamReader(documentPartStyle.GetStream()));



            }
            catch (Exception e)
            {
                flowDoc.Blocks.Add(new Paragraph(new Run("File not found or not in correct format (Word 2007)")));
                MessageBoxHelper.CatchExceptionMessageBox($"Error has occured while processing the document. Contact IT support. -- { e.Message}");
                WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                {
                    AdditionalUserInfo = "Exception",
                    LogDetail = e.Message,
                    LogDetail_Additional = "WordParser.WordDocCreator",
                    LogTime = DateTime.Now,
                    UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    Workstation = System.Environment.MachineName
                }, "GeneralOrder", false);
                return new GeneralOrderMetadataViewModel(); //return an empty view model
            }

            XNamespace w = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

            var paragraphs = wordDoc.Descendants(w + "p").Select(r => r).ToList();
            var styles = wordDocStyle.Descendants(w + "style").Select(r => r).ToList();
            var fonts = wordDocStyle.Descendants(w + "rFonts").Select(r => r).ToList();

            //Initialize strings
            var innerXML = string.Empty;
            var innerTEXT = string.Empty;
            // var outerXML = string.Empty;
            var extraString = string.Empty;
            var docTitle = string.Empty;
            bool finished = false; //sets a flag when reading the xml document is finished           
          
            //Loop through the elements in the xml
            for (i = k; i < paragraphs.Count; i++)
            {
                innerXML = paragraphs[i].ToString();
                innerTEXT = paragraphs[i].Value;
                // outerXML = p.Element.outerXML;
                
                //Get the Portfolio name
                //   if (resultPortfolio.Exists(e => e.PortfolioName == innerTEXT.Replace("Minister for ", "")) && gOrderPortfolioName != innerTEXT)
                var portfolioSearch = resultPortfolio.Find(r => r.PortfolioName == innerTEXT.Replace("Minister for ", "").Trim());
                //Check if its a portfolio name exists AND if its not one that is already being processed.
                if (portfolioSearch != null && gOrderPortfolioName != innerTEXT)
                {
                    gOrderPortfolioNameId = portfolioSearch.PortfolioId;
                    gOrderPortfolioName = innerTEXT;
                    i++;
               
                    //Get the Act Title
                    for (k = i; k < paragraphs.Count; k++)
                    {
                        // var portfolioSearch = resultPortfolio.Find(r => r.PortfolioName == elemList[k].InnerText.Replace("Minister for ", "").Trim());
                        //It has found a portfolio by remove the "Minister for" string
                        if (resultPortfolio.Exists(r => r.PortfolioName == paragraphs[k].Value.Replace("Minister for ", "").Trim()))
                        {
                            //res.Add(new GeneralOrderModel
                            //{
                            //    generalOrderPortfolioID = gOrderPortfolioNameId,
                            //    generalOrderPortfolioName = portfolioSearch.PortfolioName, //gOrderPortfolioName,
                            //    genOrderActTitle = new List<GeneralOrderActTitleModel>(res1)
                            //});

                            gOrderPortfolioName = gOrderActComment = gOrderActTitle = "";

                            res1 = new List<GeneralOrderActTitleViewModel>();

                            k--;
                            i = k;
                            break;
                        }
                        innerXML = paragraphs[k].ToString();
                        //  innerTEXT = elemList[k].InnerText;
                        //outerXML = elemList[k].OuterXml;
                        var check1 = System.Text.RegularExpressions.Regex.IsMatch(paragraphs[k].Value, @"(.*?)(Act)+\s+[0-9]{4}\s+[\–]+\s+(Except:)");
                        var check2 = System.Text.RegularExpressions.Regex.IsMatch(paragraphs[k].Value, @"(.*?)(Act)+\s+[0-9]{4}\s+[\–]");

                        if (check1)
                            extraString = "Except: ";
                        else
                            extraString = string.Empty;

                        //Strip out any characters after the Year to compare ie : Act 2006
                        innerTEXT = System.Text.RegularExpressions.Regex.Match(paragraphs[k].Value, @"(.*?)(Act)+\s+[0-9]{4}").ToString();
                      //  var activitySearch = resultActTitle.Find(r => r.ActTitle == innerTEXT);
                        var activitySearch = innerTEXT.GetActTitleInfo(); //Extension method

                        if (activitySearch != null) //resultActTitle.Exists(r => r.ActTitle == innerTEXT))
                                                    // if (ActTitle.Contains(innerTEXT))
                        {

                           
                            gOrderActTitle = innerTEXT;
                            gOrderActTitleId = activitySearch.ActTitleILDNumber;
                            innerXML = paragraphs[k].ToString();
                            innerTEXT = paragraphs[k].Value;
                            // outerXML = elemList[k].OuterXml;
                            k++;
                            
                            //Get the Acts Comments
                            if (check2) //Check top see if the Act has comments
                            {
                                //  while (!resultActTitle.Exists(r => r.ActTitle == System.Text.RegularExpressions.Regex.Match(paragraphs[k].Value, @"(.*?)(Act)+\s+[0-9]{4}").ToString()))
                                while (System.Text.RegularExpressions.Regex.Match(paragraphs[k].Value, @"(.*?)(Act)+\s+[0-9]{4}").ToString().GetActTitleInfo() == null)
                                {
                                    //Store the different elements of the XML paragraph
                                    innerXML = paragraphs[k].ToString();
                                    innerTEXT = paragraphs[k].Value;
                                    //  outerXML = elemList[k].OuterXml;
                                    //This check is to make sure that we have not reached a portfolio. This can be due to the LAST Act Title not existing in the database
                                    //When this condition is met, break out of the loop.
                                    if (resultPortfolio.Exists(r => r.PortfolioName == innerTEXT.Replace("Minister for ", "").Trim()))
                                    {
                                        finished = true;
                                        break;
                                    }
                                    //This checks to see if the Act Comments contains the effective date clause. If it does, save the date and break out of the loop.
                                    if (innerTEXT.Contains("This General Order is effective on"))
                                    {
                                        var effDate = System.Text.RegularExpressions.Regex.Match(paragraphs[k].Value, @"\d{2}\s+.{3}\s+[0-9]{4}");
                                        gOrderEffective = Convert.ToDateTime(effDate.Value);
                                        gOrderfull = true;
                                        finished = true;
                                        break;
                                    }
                                  
                                    k++;

                                    gOrderActComment +=  innerTEXT; //innerXML

                                    //If its the last row in file, exit loop
                                    if (k >= paragraphs.Count - 1)
                                    {
                                        finished = true;
                                        break;
                                    }
                                  
                                }

                                //Add the comments to the Act title
                                if (extraString.Length > 0)
                                {
                                    gOrderActComment = AddExceptToString(gOrderActComment);
                                    extraString = string.Empty;
                                }
                                res1.Add(new GeneralOrderActTitleViewModel
                                {
                                    generalOrderActTitleModelID = gOrderActTitleId,
                                    generalOrderActTitle = gOrderActTitle,
                                    isExcept = extraString.Length > 0 ? true : false,
                                    genOrderActComment = new List<GeneralOrderActCommentViewModel>
                                {
                                    new GeneralOrderActCommentViewModel {
                                     generalOrderActComment = gOrderActComment
                                    }
                                }
                                });

                                //If its the last row in file, save last res1
                                if (finished)
                                {
                                  
                                    res.Add(new GeneralOrderPortfolioViewModel
                                    {
                                        generalOrderPortfolioID = gOrderPortfolioNameId,
                                        generalOrderPortfolioName = gOrderPortfolioName,
                                        genOrderActTitle = new List<GeneralOrderActTitleViewModel>(res1)
                                    });
                                }

                                gOrderActComment = gOrderActTitle = string.Empty;
                            }
                            k--;
                            i = k;
                        }
                        //Act title wqith no comments

                        if (gOrderActTitle != "")
                        {
                            if (extraString.Length > 0)
                            {
                                gOrderActComment = AddExceptToString(gOrderActComment);
                                extraString = string.Empty;
                            }

                            res1.Add(new GeneralOrderActTitleViewModel
                            {
                                generalOrderActTitleModelID = gOrderActTitleId,
                                generalOrderActTitle = gOrderActTitle,
                                isExcept = extraString.Length > 0 ? true : false,
                                genOrderActComment = new List<GeneralOrderActCommentViewModel>
                                {
                                    new GeneralOrderActCommentViewModel {
                                    generalOrderActComment = gOrderActComment
                                    }
                                }
                            });
                        }
                        gOrderActComment = gOrderActTitle = string.Empty;
                    }
                }
            }

            //Create the root level object to be serialized
            resMeta = new GeneralOrderMetadataViewModel
            {

                orderEffective = gOrderEffective,
                orderfull = gOrderfull,
                GOModel = res,
                OriginalXML = wordDoc.ToString(),
                orderFileName = fileName
            };

            #region Insert Into Database
            //Convert to JSON for ease of use in sending objects to other DLL
           // var result = Newtonsoft.Json.JsonConvert.SerializeObject(resMeta);
            //Send the JSON object to DLL to import into DB
            // GeneralOrderHeaderImportDB.GenOrderImport(result);           
            #endregion

            return resMeta;

        }

        #region Add 'Except: ' To String


        /// <summary>
        /// Add the 'Except: ' string to the first occurance of the Act Comment paragraph <w:t>
        /// </summary>
        /// <param name="gOrderActComment"></param>
        /// <returns></returns>
        private static string AddExceptToString(string gOrderActComment)
        {
            var startText = 0;

            startText = gOrderActComment.IndexOf("<w:t xml:space=\"preserve\">");
            gOrderActComment = gOrderActComment.Insert(startText + "<w:t xml:space=\"preserve\">".Length, "Except: ");

            return gOrderActComment;
        }

        #endregion
    }
}
