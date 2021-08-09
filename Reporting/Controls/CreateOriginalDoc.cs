using ApplicationLogger;
using DatabaseEntity;
using GeneralOrderCore;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;

namespace Reporting
{
    public class CreateOriginalDoc
    {

        public static string ErrorListDocCreator(List<string> errorList)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "GeneralOrder"; // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            dlg.Filter = "Word Documents|*.doc;*.docx|Excel Worksheets|*.xls|PowerPoint Presentations|*.ppt" +
             "|Office Files|*.doc;*.docx;*.xls;*.ppt" +
             "|All Files|*.*";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {

                try
                {
                    //Create an instance for word app
                    Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                    winword.Visible = false;
                    winword.Options.SavePropertiesPrompt = false;
                    //  //Create a missing variable for missing value
                    object missing = System.Reflection.Missing.Value;

                    //  //Create a new document
                    Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);


                    //adding text to document
                    document.Content.SetRange(0, 0);
                    document.Styles["Normal"].Font.Name = "Times New Roman";
                    //Add paragraph with Normal style
                    Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                    object styleHeading1 = "Normal"; // "Heading 1";
                    para1.Range.set_Style(ref styleHeading1);
                    para1.TabStops.ClearAll();
                    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    para1.Range.ParagraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
                    winword.Selection.TypeParagraph();
                    winword.Selection.TypeParagraph();

                    //Loop through the object to extract the Portfolio , Act Title and Act Comments
                    foreach (var errRecord in errorList)
                    {
                        para1.Range.Text = errRecord;
                        para1.Range.Bold = 0;
                        para1.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        para1.Range.ParagraphFormat.LeftIndent = 0;
                        para1.Range.ParagraphFormat.PageBreakBefore = 0;
                        para1.Range.ParagraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
                        para1.Range.ParagraphFormat.SpaceAfter = 0;
                        para1.Range.InsertParagraphAfter();
                        para1.Range.InsertParagraph();
                    }
                    // Save document
                    object filename = dlg.FileName;
                    document.SaveAs(ref filename);

                    //Release the Word COM object
                    ((Microsoft.Office.Interop.Word._Document)document).Close(ref missing, ref missing, ref missing);
                    ((Microsoft.Office.Interop.Word._Application)winword).Quit(ref missing, ref missing, ref missing);
                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured while creating the Error List document. Contact IT support. -- { e.Message}");
                }
            }
            else
                dlg.FileName = string.Empty;
            return dlg.FileName;
        }






        /// <summary>
        /// WordDocCreator creates a General Order document the the imported records that reside in memory object  <see cref="GeneralOrderMetadataViewModel"/>
        /// resMeta represents the model
        /// </summary>
        /// <param name="resMeta"></param>


        public static string WordDocCreator(DatabaseEntity.GeneralOrderMetadataViewModel resMeta)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "GeneralOrder"; // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            dlg.Filter = "Word Documents|*.doc;*.docx|Excel Worksheets|*.xls|PowerPoint Presentations|*.ppt" +
             "|Office Files|*.doc;*.docx;*.xls;*.ppt" +
             "|All Files|*.*";
            Microsoft.Office.Interop.Word.Paragraph para1 = null;
            string errResult = string.Empty;
            int commCount = 0;
            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                ///Create the word document based on the res Obj
                try
                {
                    //Create an instance for word app
                    Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                    //  //Set animation status for word application
                    //  winword.ShowAnimation = false;

                    //  //Set status for word application is to be visible or not.
                    winword.Visible = false;
                    winword.Options.SavePropertiesPrompt = false;
                    //  //Create a missing variable for missing value
                    object missing = System.Reflection.Missing.Value;

                    //  //Create a new document
                    Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    int level = 0;
                    //Set the page style
                    document.Styles["Normal"].NoSpaceBetweenParagraphsOfSameStyle = resMeta.IndentationSpacing;
                    document.PageSetup.LeftMargin = (float)resMeta.marginLeft;
                    document.PageSetup.RightMargin = (float)resMeta.marginRight;
                    document.PageSetup.HeaderDistance = (float)resMeta.pageHeaderMargin;
                    document.PageSetup.FooterDistance = (float)resMeta.pageFooterMargin;
                    document.DefaultTabStop = (float)resMeta.defaultTabStop;

                    //adding text to document
                    document.Content.SetRange(0, 0);
                    document.Styles["Normal"].Font.Name = "Times New Roman";
                    //Add paragraph with Normal style
                    para1 = document.Content.Paragraphs.Add(ref missing);
                    object styleHeading1 = "Normal"; // "Heading 1";
                    para1.Range.set_Style(ref styleHeading1);
                    para1.TabStops.ClearAll();
                    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    para1.Range.ParagraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
                    para1.Range.ParagraphFormat.SpaceAfter = 0;
                    winword.Selection.TypeParagraph();
                    winword.Selection.TypeParagraph();


                    //Adding first page title and text as per original General Order document.
                    para1.Range.Text = "ADMINISTRATION OF ACTS";
                    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    para1.Range.Bold = -1;
                    para1.Range.ParagraphFormat.LeftIndent = 0;
                    para1.Range.ParagraphFormat.PageBreakBefore = 0;
                    para1.Range.ParagraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
                    para1.Range.ParagraphFormat.SpaceAfter = 0;
                    para1.Range.InsertParagraphAfter();
                    para1.Range.InsertParagraph();

                    para1.Range.Text = "General Order";
                    para1.Range.Bold = 0;
                    para1.Range.InsertParagraphAfter();
                    para1.Range.InsertParagraph();

                    para1.Range.Text = "I, Daniel Andrews, Premier of Victoria, state that the following administrative arrangements for responsibility for the following Acts of Parliament, provisions of Acts and functions will operate in substitution of the arrangements in operation immediately before the date this Order takes effect:";
                    para1.Range.Bold = 0;
                    para1.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    para1.Range.InsertParagraphAfter();

                    //Loop through the object to extract the Portfolio , Act Title and Act Comments
                    foreach (var portfolioRes in resMeta.GOModel)
                    {
                        var portfolioSearch = portfolioRes.generalOrderPortfolioName.GetPortfolioInfo();
                        // portfolioRes.generalOrderPrefixWithMinisterFor = portfolioSearch.PrefixWithMinisterFor;

                        //Get the style for the portfolio paragraph
                        para1.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;
                        para1.Range.ParagraphFormat.SpaceAfter = 0;
                        para1.Range.ParagraphFormat.RightIndent = portfolioRes.generalOrderPortfolioParagraphMeta.IndentationRight;
                        para1.Range.ParagraphFormat.FirstLineIndent = portfolioRes.generalOrderPortfolioParagraphMeta.indentationBy;
                        para1.Range.ParagraphFormat.TabHangingIndent(portfolioRes.generalOrderPortfolioParagraphMeta.TabHangingIndent);
                        if (para1.Range.ListFormat != null)
                            para1.Range.ListFormat.RemoveNumbers(Microsoft.Office.Interop.Word.WdNumberType.wdNumberParagraph);
                        para1.Range.Text = portfolioSearch.PrefixWithMinisterFor ? "Minister for " + portfolioRes.generalOrderPortfolioName : portfolioRes.generalOrderPortfolioName;

                        if (portfolioRes.generalOrderPortfolioName.Contains("Error"))
                        {
                            para1.Range.Bold = -1;
                            para1.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorRed;
                        }
                        else
                        {
                            para1.Range.Bold = 0;
                            para1.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;
                        }
                        para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        para1.Range.Bold = portfolioRes.generalOrderPortfolioParagraphMeta.fontBold == false ? 0 : -1;
                        para1.Range.ParagraphFormat.LeftIndent = portfolioRes.generalOrderPortfolioParagraphMeta.IndentationLeft;
                        para1.Range.ParagraphFormat.PageBreakBefore = portfolioRes.generalOrderPortfolioParagraphMeta.pageBreakBefore == false ? 0 : -1;
                        para1.Range.ParagraphFormat.TabStops.ClearAll();
                        para1.Range.ParagraphFormat.TabStops.Add(portfolioRes.generalOrderPortfolioParagraphMeta.tabStopPosition);

                        //Set the paragraph alignment
                        switch (portfolioRes.generalOrderPortfolioParagraphMeta.paragraphAlignment)
                        {
                            case 0:
                                para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                break;
                            case 1:
                                para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                break;
                            case 2:
                                para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                break;
                            default:
                                para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                break;
                        }

                        //Insert the page into the Word Paragraph
                        para1.Range.InsertParagraphAfter();
                        //Add a page break
                        para1.Range.ParagraphFormat.PageBreakBefore = 0;
                        para1.Range.InsertParagraph();

                        //Loop through the Act Title
                        foreach (var actTitleRes in portfolioRes.genOrderActTitle)
                        {
                            errResult = actTitleRes.generalOrderActTitle;
                            //Get the style for the Act Title
                            para1.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;
                            if (para1.Range.ListFormat != null)
                                para1.Range.ListFormat.RemoveNumbers(Microsoft.Office.Interop.Word.WdNumberType.wdNumberParagraph);
                            para1.Range.Bold = actTitleRes.generalOrderActTitleParagraphMeta.fontBold == false ? 0 : -1;

                            para1.Range.Text = actTitleRes.isExcept ? actTitleRes.generalOrderActTitle + " - Except:" : (actTitleRes.genOrderActComment.Count > 0 ? actTitleRes.generalOrderActTitle + " - " : actTitleRes.generalOrderActTitle);
                            if (actTitleRes.generalOrderActTitle.Contains("Error"))
                            {
                                para1.Range.Bold = -1;
                                para1.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorRed;
                            }
                            else
                            {
                                para1.Range.Bold = 0;
                                para1.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;
                            }
                            para1.Range.ParagraphFormat.LeftIndent = actTitleRes.generalOrderActTitleParagraphMeta.IndentationLeft;
                            para1.Range.ParagraphFormat.PageBreakBefore = actTitleRes.generalOrderActTitleParagraphMeta.pageBreakBefore == false ? 0 : -1;
                            para1.Range.ParagraphFormat.TabStops.ClearAll();
                            para1.Range.ParagraphFormat.TabStops.Add(actTitleRes.generalOrderActTitleParagraphMeta.tabStopPosition);

                            para1.Range.ParagraphFormat.SpaceAfter = 0;
                            para1.Range.ParagraphFormat.RightIndent = actTitleRes.generalOrderActTitleParagraphMeta.IndentationRight;
                            para1.Range.ParagraphFormat.FirstLineIndent = actTitleRes.generalOrderActTitleParagraphMeta.indentationBy;
                            para1.Range.ParagraphFormat.TabHangingIndent(actTitleRes.generalOrderActTitleParagraphMeta.TabHangingIndent);

                            switch (actTitleRes.generalOrderActTitleParagraphMeta.paragraphAlignment)
                            {
                                case 0:
                                    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                    break;
                                case 1:
                                    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                    break;
                                case 2:
                                    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                    break;
                                default:
                                    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                    break;
                            }
                            //Insert the Word Paragraph
                            para1.Range.InsertParagraphAfter();

                            commCount = 0;
                            //Loop through the Act Comments
                            foreach (var actcommentRes in actTitleRes.genOrderActComment)
                            {
                                commCount++;

                                //para1.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;
                                //para1.Range.Bold = actcommentRes.generalOrderActCommentParagraphMeta.fontBold == false ? 0 : -1;
                                //para1.Range.ParagraphFormat.PageBreakBefore = actcommentRes.generalOrderActCommentParagraphMeta.pageBreakBefore == false ? 0 : -1;

                                //para1.Range.ListFormat.ApplyListTemplateWithLevel(
                                //        winword.ListGalleries[Microsoft.Office.Interop.Word.WdListGalleryType.wdBulletGallery].ListTemplates[1],
                                //        false,
                                //        Microsoft.Office.Interop.Word.WdListApplyTo.wdListApplyToSelection,
                                //        Microsoft.Office.Interop.Word.WdDefaultListBehavior.wdWord10ListBehavior);

                                //switch (actcommentRes.generalOrderActCommentParagraphMeta.paragraphAlignment)
                                //{
                                //    case 0:
                                //        para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                //        break;
                                //    case 1:
                                //        para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                //        break;
                                //    case 2:
                                //        para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                //        break;
                                //    default:
                                //        para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                //        break;
                                //}


                                //Get the correct bullet for the List and set the paragraph format
                                switch (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI)
                                {
                                    /*
                                     case 183: //Default solid round
                                     case 111: //Default hollow round
                                     case 167: //Default solid square
                                         string Name = string.Empty;
                                         if (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI == 183)
                                         {
                                             Name = "ReportBullet1";
                                         }
                                         if (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI == 111)
                                         {
                                             Name = "ReportBullet2";
                                         }
                                         if (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI == 167)
                                         {
                                             Name = "ReportBullet3";
                                         }


                                         var Type = Microsoft.Office.Interop.Word.WdStyleType.wdStyleTypeParagraph;

                                         bool hasStyle = false;
                                         foreach (Style res in document.Styles)
                                         {
                                             if (res.NameLocal == Name)
                                             {
                                                 hasStyle = true;
                                                 break;
                                             }
                                         }
                                         if (!hasStyle) //style doesnt exist
                                         {
                                             ListTemplate template = para1.Range.Application.ListGalleries[WdListGalleryType.wdBulletGallery].ListTemplates[1];
                                             template.ListLevels[1].NumberStyle = WdListNumberStyle.wdListNumberStyleBullet;

                                             if (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI == 183)
                                             {
                                                 template.ListLevels[1].NumberFormat = Convert.ToChar(61623).ToString();  //char.ConvertFromUtf32(61623);
                                             }
                                             if (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI == 111)
                                             {
                                                 template.ListLevels[1].NumberFormat = Convert.ToChar(9675).ToString(); // "o";
                                             }
                                             if (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI == 167)
                                             {
                                                 template.ListLevels[1].NumberFormat = Convert.ToChar(61607).ToString();  //char.ConvertFromUtf32(61607);
                                             }

                                             template.ListLevels[1].Font.Name = actcommentRes.generalOrderActCommentParagraphMeta.listSymbolFont; //Symbol


                                             winword.ActiveDocument.Styles.Add(Name, Type);

                                             winword.ActiveDocument.Styles[Name].ParagraphFormat.TabStops.ClearAll();
                                             winword.ActiveDocument.Styles[Name].ParagraphFormat.TabStops.Add(actcommentRes.generalOrderActCommentParagraphMeta.tabStopPosition);


                                             winword.ActiveDocument.Styles[Name].Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;
                                             winword.ActiveDocument.Styles[Name].Font.Bold = actcommentRes.generalOrderActCommentParagraphMeta.fontBold == false ? 0 : -1;
                                             winword.ActiveDocument.Styles[Name].ParagraphFormat.PageBreakBefore = actcommentRes.generalOrderActCommentParagraphMeta.pageBreakBefore == false ? 0 : -1;

                                             switch (actcommentRes.generalOrderActCommentParagraphMeta.paragraphAlignment)
                                             {
                                                 case 0:
                                                     winword.ActiveDocument.Styles[Name].ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                                     break;
                                                 case 1:
                                                     winword.ActiveDocument.Styles[Name].ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                                     break;
                                                 case 2:
                                                     winword.ActiveDocument.Styles[Name].ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                                     break;
                                                 default:
                                                     winword.ActiveDocument.Styles[Name].ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                                     break;
                                             }

                                             winword.ActiveDocument.Styles[Name].ParagraphFormat.TabHangingIndent(actcommentRes.generalOrderActCommentParagraphMeta.TabHangingIndent);
                                             winword.ActiveDocument.Styles[Name].ParagraphFormat.LeftIndent = actcommentRes.generalOrderActCommentParagraphMeta.IndentationLeft;
                                             winword.ActiveDocument.Styles[Name].ParagraphFormat.RightIndent = actcommentRes.generalOrderActCommentParagraphMeta.IndentationRight;
                                             winword.ActiveDocument.Styles[Name].ParagraphFormat.FirstLineIndent = actcommentRes.generalOrderActCommentParagraphMeta.indentationBy;
                                             winword.ActiveDocument.Styles[Name].Font.Name = "Times New Roman"; // actcommentRes.generalOrderActCommentParagraphMeta.listSymbolFont; //Symbol

                                             template.ListLevels[1].LinkedStyle = Name;

                                             winword.ActiveDocument.Styles[Name].LinkToListTemplate(template, 1);
                                            // para1.Range.ParagraphStyle.LinkToListTemplate(template, 1);

                                         }
                                         para1.Range.ParagraphFormat.set_Style(Name);
                                        // para1.Range.set_Style(Name);
                                         para1.Range.Text = actcommentRes.generalOrderActComment;

                                         para1.Range.InsertParagraphAfter();
                                         para1.Range.ListFormat.RemoveNumbers();

                                         break;
                                         */

                                    case 183: //Default solid round
                                    case 111: //Default hollow round
                                    case 167: //Default solid square

                                        ListTemplate template = para1.Range.Application.ListGalleries[WdListGalleryType.wdBulletGallery].ListTemplates[1];
                                        template.ListLevels[1].NumberStyle = WdListNumberStyle.wdListNumberStyleBullet;
                                        para1.Range.ListFormat.ApplyListTemplateWithLevel(
                                                template,
                                                false,
                                                Microsoft.Office.Interop.Word.WdListApplyTo.wdListApplyToSelection,
                                                Microsoft.Office.Interop.Word.WdDefaultListBehavior.wdWord10ListBehavior);

                                        // para1.Range.ListFormat.ApplyListTemplate(template, ref missing, ref missing, ref missing);
                                        if (para1.Range.ListFormat.ListTemplate == null) //Retry as sometimes it fails the first time. The reason is unknown
                                        {
                                            para1.Range.ListFormat.ApplyListTemplateWithLevel(
                                                    template,
                                                    false,
                                                    Microsoft.Office.Interop.Word.WdListApplyTo.wdListApplyToSelection,
                                                    Microsoft.Office.Interop.Word.WdDefaultListBehavior.wdWord10ListBehavior);
                                        }


                                        para1.Range.ListFormat.ListTemplate.ListLevels[1].Font.Name = actcommentRes.generalOrderActCommentParagraphMeta.listSymbolFont; //Symbol

                                        if (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI == 183)
                                        {
                                            para1.Range.ListFormat.ListTemplate.ListLevels[1].NumberFormat = Convert.ToChar(61623).ToString();  //char.ConvertFromUtf32(61623);
                                        }
                                        if (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI == 111)
                                        {
                                            para1.Range.ListFormat.ListTemplate.ListLevels[1].NumberFormat = Convert.ToChar(9675).ToString(); // "o";  (9702).ToString();
                                        }
                                        if (actcommentRes.generalOrderActCommentParagraphMeta.bulletASCI == 167)
                                        {
                                            para1.Range.ListFormat.ListTemplate.ListLevels[1].NumberFormat = Convert.ToChar(61607).ToString();  //char.ConvertFromUtf32(61607);
                                        }
                                        para1.Range.ParagraphFormat.TabStops.ClearAll();
                                        para1.Range.ParagraphFormat.TabStops.Add(actcommentRes.generalOrderActCommentParagraphMeta.tabStopPosition);
                                        para1.Range.ParagraphFormat.LeftIndent = actcommentRes.generalOrderActCommentParagraphMeta.IndentationLeft;
                                        para1.Range.ParagraphFormat.RightIndent = actcommentRes.generalOrderActCommentParagraphMeta.IndentationRight;
                                        para1.Range.ParagraphFormat.FirstLineIndent = actcommentRes.generalOrderActCommentParagraphMeta.indentationBy;
                                        para1.Range.ParagraphFormat.TabHangingIndent(actcommentRes.generalOrderActCommentParagraphMeta.TabHangingIndent);
                                        para1.Range.Text = actcommentRes.generalOrderActComment;

                                        // level = para1.Range.ListFormat.ListLevelNumber;
                                        para1.Range.InsertParagraphAfter();
                                        para1.Range.ListFormat.RemoveNumbers();
                                        break;

                                    default: //This is used for comments that dont have a bullet. These are generally at the end of the Act and are enclosed in brackets
                                        if (para1.Range.ListFormat != null)
                                            para1.Range.ListFormat.RemoveNumbers();
                                        para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                        para1.Range.ParagraphFormat.TabStops.ClearAll();
                                        para1.Range.ParagraphFormat.TabStops.Add(actcommentRes.generalOrderActCommentParagraphMeta.tabStopPosition);
                                        para1.Range.ParagraphFormat.LeftIndent = actcommentRes.generalOrderActCommentParagraphMeta.IndentationLeft;
                                        para1.Range.ParagraphFormat.RightIndent = actcommentRes.generalOrderActCommentParagraphMeta.IndentationRight;
                                        para1.Range.ParagraphFormat.FirstLineIndent = actcommentRes.generalOrderActCommentParagraphMeta.indentationBy;
                                        para1.Range.ParagraphFormat.TabHangingIndent(actcommentRes.generalOrderActCommentParagraphMeta.TabHangingIndent);

                                        //  para1.Range.ParagraphFormat.SpaceAfter = 0;
                                        para1.Range.Text = actcommentRes.generalOrderActComment;
                                        para1.Range.InsertParagraphAfter();
                                        break;

                                }


                                //Insert a blank paragraph after the last Comment.
                                if (commCount == actTitleRes.genOrderActComment.Count)
                                {
                                    if (para1.Range.ListFormat != null)
                                        para1.Range.ListFormat.RemoveNumbers(Microsoft.Office.Interop.Word.WdNumberType.wdNumberParagraph);
                                    para1.Range.InsertParagraphAfter();
                                }

                            }
                        }
                    }

                    //Add the end part of the document
                    para1.Range.ParagraphFormat.PageBreakBefore = -1;
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Text = $"This General Order is effective on {resMeta.orderEffective.ToString("dd MMM yyyy")}";
                    para1.Range.Bold = 0;
                    para1.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    para1.Range.ParagraphFormat.PageBreakBefore = 0;
                    para1.Range.InsertParagraphAfter();

                    para1.Range.InsertParagraph();
                    para1.Range.InsertParagraph();
                    para1.Range.InsertParagraph();
                    para1.Range.InsertParagraph();

                    para1.Range.Text = "The Hon Daniel Andrews MP";
                    para1.Range.Bold = 1;
                    para1.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Text = "Premier of Victoria";
                    para1.Range.Bold = 1;
                    para1.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    para1.Range.InsertParagraphAfter();


                    //test
                    //para1.Range.Text = "ADMINISTRATION OF ACTS";
                    //para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    //para1.Range.Bold = -1;
                    //para1.Range.ParagraphFormat.LeftIndent = 0;
                    //para1.Range.ParagraphFormat.PageBreakBefore = 0;
                    //para1.Range.ParagraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
                    //para1.Range.ParagraphFormat.SpaceAfter = 0;
                    //para1.Range.InsertParagraphAfter();
                    //para1.Range.InsertParagraph();

                    //para1.Range.Text = "General Order";
                    //para1.Range.Bold = 0;
                    //para1.Range.InsertParagraphAfter();
                    //para1.Range.InsertParagraph();

                    //para1.Range.Text = "I, Daniel Andrews, Premier of Victoria, state that the following administrative arrangements for responsibility for the following Acts of Parliament, provisions of Acts and functions will operate in substitution of the arrangements in operation immediately before the date this Order takes effect:";
                    //para1.Range.Bold = 0;
                    //para1.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    //para1.Range.InsertParagraphAfter();

                    //end test
                    //Save the document
                    // object filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\testOUT";

                    // Save document
                    object filename = dlg.FileName;
                    document.SaveAs(ref filename);
                    // winword.Options.SavePropertiesPrompt = true;

                    //  object filename = @"C:\Users\arty\Documents\Visual Studio 2017\Projects\General Order Projects\Word Docs\testOUT";

                    //Save word document


                    //Release the Word COM object
                    ((Microsoft.Office.Interop.Word._Document)document).Close(ref missing, ref missing, ref missing);
                    ((Microsoft.Office.Interop.Word._Application)winword).Quit(ref missing, ref missing, ref missing);
                }

                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured while creating the document at {errResult + ' ' + commCount}. Contact IT support. -- { e.Message}");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "CreateOriginalDoc.ErrorListDocCreator",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "Reporting", false);
                }
            }
            else
                dlg.FileName = string.Empty;

            return dlg.FileName;

            ///FLOWDOCUMENT
            //if (solidSquareBullet)
            //{
            //    if (newList)
            //    {
            //        ssb = new List();
            //        ssb.ListItems.Add(new ListItem(new Paragraph(new Run(WPFstring))));

            //        // result.ListItems.Add(new ListItem(new Paragraph(new Run(docs.Paragraphs[i].Range.Text.ToString()))));
            //        newList = false;
            //    }
            //    else
            //    {
            //        ssb.ListItems.Add(new ListItem(new Paragraph(new Run(WPFstring))));
            //    }
            //}
            //else
            //  if (hollowRoundbullet)
            //{
            //    if (newList)
            //    {
            //        hrb = new List();
            //        hrb.ListItems.Add(new ListItem(new Paragraph(new Run(WPFstring))));

            //        // result.ListItems.Add(new ListItem(new Paragraph(new Run(docs.Paragraphs[i].Range.Text.ToString()))));
            //        newList = false;
            //    }
            //    else
            //    {
            //        if (ssb != null && ssb.ListItems.Count() > 0)
            //        {

            //        }
            //        hrb.ListItems.Add(new ListItem(new Paragraph(new Run(WPFstring))));

            //    }
            //}
            //else
            //{

            //    if (hrb != null && hrb.ListItems.Count() > 0)
            //    {
            //        foreach (ListItem LI in hrb.ListItems)
            //        {
            //            result.ListItems.AddRange(hrb.ListItems);
            //            //result.ListItems.Add(new ListItem(new List(LI)));
            //            // result.ListItems.Add(LI);
            //        }
            //    }
            //    newList = true;
            //    if (solidRoundbullet)
            //        result.ListItems.Add(new ListItem(new Paragraph(new Run(WPFstring))));
            //    // result += "<ListItem>< Paragraph>" + docs.Paragraphs[i].Range.Text.ToString()  + "</Paragraph></ListItem>";
            //}





            //if (style != null && style.NameLocal.Equals(headingStyle))
            //{
            //    flag = false;
            //    if (docs.Paragraphs[i].Range.Text.ToString().TrimEnd('\r').ToUpper() == headingText.ToUpper())
            //    {
            //        ind++;
            //        flag = true;
            //    }
            //}
            //if (flag && ind >= 1)
            //    totaltext += " \r\n " + docs.Paragraphs[i].Range.Text.ToString();

            //}
            //if (totaltext == "") { totaltext = "No such data found!"; }
            //// richTextBox1.Text = totaltext;
            //docs.Close();
            //word.Quit();



        }
    }
}
