using ApplicationLogger;
using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Reporting
{
    public class CreateGeneralOrderDoc
    {

        /// <summary>
        /// WordDocCreator creates a General Order document from the database <see cref="ReportModel"/>
        /// resMeta represents the model
        /// </summary>
        /// <param name="resMeta"></param>
        public static async System.Threading.Tasks.Task<string> WordDocCreator(List<ReportModel> resMeta, string fileName)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = fileName; // "GeneralOrder"; // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            dlg.Filter = "Word Documents|*.doc;*.docx|Excel Worksheets|*.xls|PowerPoint Presentations|*.ppt" +
             "|Office Files|*.doc;*.docx;*.xls;*.ppt" +
             "|All Files|*.*";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                ///Create the word document based on the res Obj
                try
                {
                    await Task.Run(() =>
                    {
                        //Create an instance for word app
                        Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                        //  //Set status for word application is to be visible or not.
                        winword.Visible = false;
                        winword.Options.SavePropertiesPrompt = false;
                        //  //Create a missing variable for missing value
                        object missing = System.Reflection.Missing.Value;

                        //  //Create a new document
                        Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                        int level = 0;
                        //Set the page style
                        document.Styles["Normal"].NoSpaceBetweenParagraphsOfSameStyle = false;
                        document.PageSetup.LeftMargin = 90;
                        document.PageSetup.RightMargin = 90;
                        document.PageSetup.HeaderDistance = 36;
                        document.PageSetup.FooterDistance = 36;
                        document.DefaultTabStop = 36;

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
                        foreach (var paraRes in resMeta)
                        {
                            //Get the style for the portfolio paragraph
                            para1.Range.ParagraphFormat.SpaceAfter = 0;
                            para1.Range.ParagraphFormat.RightIndent = 0;
                            para1.Range.ParagraphFormat.FirstLineIndent = 0;
                            para1.Range.ParagraphFormat.TabHangingIndent(1);
                            para1.Range.ListFormat.RemoveNumbers(Microsoft.Office.Interop.Word.WdNumberType.wdNumberParagraph);
                            para1.Range.Text = paraRes.portfolio; // paraRes.PrefixWithMinisterFor ? "Minister for " + paraRes.portfolio : paraRes.portfolio;

                            //para1.Range.Text = paraRes.portfolio != "Attorney-General" ? $"Minister for { e.Message}", paraRes.portfolio) : paraRes.portfolio;
                            para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            para1.Range.Bold = -1;
                            para1.Range.ParagraphFormat.LeftIndent = 0;
                            para1.Range.ParagraphFormat.PageBreakBefore = -1;
                            para1.Range.ParagraphFormat.TabStops.ClearAll();
                            para1.Range.ParagraphFormat.TabStops.Add(36);
                            para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                            //Insert the page into the Word Paragraph
                            para1.Range.InsertParagraphAfter();
                            //Add a page break
                            para1.Range.ParagraphFormat.PageBreakBefore = 0;
                            para1.Range.InsertParagraph();


                            //    //Get the style for the Act Title

                            foreach (var paraAct in paraRes.generalFieldsActViewModel)
                            {
                                para1.Range.ListFormat.RemoveNumbers(Microsoft.Office.Interop.Word.WdNumberType.wdNumberParagraph);
                                para1.Range.Text = paraAct.isExcept ? paraAct.actTitle + " - Except:" : paraAct.generalFieldsCommentViewModel.Count > 0 ? paraAct.actTitle + " - " : paraAct.actTitle; // paraAct.actTitle;
                                para1.Range.Bold = 0;
                                para1.Range.ParagraphFormat.LeftIndent = 0;
                                para1.Range.ParagraphFormat.PageBreakBefore = 0;
                                para1.Range.ParagraphFormat.TabStops.ClearAll();
                                para1.Range.ParagraphFormat.TabStops.Add(36);

                                para1.Range.ParagraphFormat.SpaceAfter = 0;
                                para1.Range.ParagraphFormat.RightIndent = 0;
                                para1.Range.ParagraphFormat.FirstLineIndent = 0;
                                para1.Range.ParagraphFormat.TabHangingIndent(0);
                                para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                                //Insert the Word Paragraph
                                para1.Range.InsertParagraphAfter();

                                int commCount = 0;
                                //Loop through the Act Comments
                                foreach (var actcommentRes in paraAct.generalFieldsCommentViewModel)
                                {
                                    commCount++;

                                    //para1.Range.Bold = actcommentRes.fontBold == false ? 0 : -1;
                                    //para1.Range.ParagraphFormat.PageBreakBefore = actcommentRes.pageBreakBefore == false ? 0 : -1;

                                    //para1.Range.ListFormat.ApplyListTemplateWithLevel(
                                    //        winword.ListGalleries[Microsoft.Office.Interop.Word.WdListGalleryType.wdBulletGallery].ListTemplates[1],
                                    //        false,
                                    //        Microsoft.Office.Interop.Word.WdListApplyTo.wdListApplyToSelection,
                                    //        Microsoft.Office.Interop.Word.WdDefaultListBehavior.wdWord10ListBehavior);

                                    //switch (actcommentRes.alignmentType)
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
                                    switch (actcommentRes.bulletASCII)
                                    {
                                        case 183: //Default solid round
                                        case 111: //Default hollow round
                                        case 167: //Default solid square


                                            Microsoft.Office.Interop.Word.ListTemplate template = para1.Range.Application.ListGalleries[Microsoft.Office.Interop.Word.WdListGalleryType.wdBulletGallery].ListTemplates[1];
                                            template.ListLevels[1].NumberStyle = Microsoft.Office.Interop.Word.WdListNumberStyle.wdListNumberStyleBullet;
                                            para1.Range.ListFormat.ApplyListTemplateWithLevel(
                                                    template,
                                                    false,
                                                    Microsoft.Office.Interop.Word.WdListApplyTo.wdListApplyToSelection,
                                                    Microsoft.Office.Interop.Word.WdDefaultListBehavior.wdWord10ListBehavior);

                                            if (actcommentRes.bulletASCII == 183)
                                            {
                                                para1.Range.ListFormat.ListTemplate.ListLevels[1].NumberFormat = Convert.ToChar(61623).ToString();  //char.ConvertFromUtf32(61623);
                                            }
                                            if (actcommentRes.bulletASCII == 111)
                                            {
                                                para1.Range.ListFormat.ListTemplate.ListLevels[1].NumberFormat = Convert.ToChar(9675).ToString(); // "o";
                                            }
                                            if (actcommentRes.bulletASCII == 167)
                                            {
                                                para1.Range.ListFormat.ListTemplate.ListLevels[1].NumberFormat = Convert.ToChar(61607).ToString();  //char.ConvertFromUtf32(61607);
                                            }
                                            para1.Range.ParagraphFormat.TabStops.ClearAll();
                                            para1.Range.ParagraphFormat.TabStops.Add((float)actcommentRes.tabStopPosition);
                                            para1.Range.ParagraphFormat.LeftIndent = (float)actcommentRes.indentationLeft;
                                            para1.Range.ParagraphFormat.RightIndent = (float)actcommentRes.indentationRight;
                                            para1.Range.ParagraphFormat.FirstLineIndent = (float)actcommentRes.indentationBy;
                                            para1.Range.ParagraphFormat.TabHangingIndent(actcommentRes.tabHangingIndent);
                                            //para1.Range.ListFormat.ListTemplate.ListLevels[1].NumberFormat = actcommentRes.bulletChar;
                                            para1.Range.ListFormat.ListTemplate.ListLevels[1].Font.Name = actcommentRes.listSymbolFont; //Symbol
                                            para1.Range.Text = actcommentRes.actAdministrationComment;

                                            level = para1.Range.ListFormat.ListLevelNumber;
                                            para1.Range.InsertParagraphAfter();
                                            break;
                                        default: //This is used for comments that dont have a bullet. These are generally at the end of the Act and are enclosed in brackets
                                            if (para1.Range.ListFormat != null)
                                                para1.Range.ListFormat.RemoveNumbers();
                                            para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                            para1.Range.ParagraphFormat.TabStops.ClearAll();
                                            para1.Range.ParagraphFormat.TabStops.Add((float)actcommentRes.tabStopPosition);
                                            para1.Range.ParagraphFormat.LeftIndent = (float)actcommentRes.indentationLeft;
                                            para1.Range.ParagraphFormat.RightIndent = (float)actcommentRes.indentationRight;
                                            para1.Range.ParagraphFormat.FirstLineIndent = (float)actcommentRes.indentationBy;
                                            para1.Range.ParagraphFormat.TabHangingIndent(actcommentRes.tabHangingIndent);

                                            //  para1.Range.ParagraphFormat.SpaceAfter = 0;
                                            para1.Range.Text = actcommentRes.actAdministrationComment;
                                            para1.Range.InsertParagraphAfter();
                                            //para1.Range.ListFormat.ApplyBulletDefault("None");
                                            //para1.Range.ParagraphFormat.LeftIndent = (float)actcommentRes.indentationLeft;
                                            //para1.Range.ParagraphFormat.RightIndent = (float)actcommentRes.indentationRight;
                                            //para1.Range.ParagraphFormat.FirstLineIndent = (float)actcommentRes.indentationBy;
                                            //para1.Range.ParagraphFormat.TabHangingIndent(actcommentRes.tabHangingIndent);

                                            //para1.Range.ParagraphFormat.SpaceAfter = 0;
                                            //para1.Range.Text = actcommentRes.actAdministrationComment;
                                            //para1.Range.InsertParagraphAfter();
                                            break;

                                    }


                                    //Insert a blank paragraph after the last Comment.
                                    if (commCount == paraAct.generalFieldsCommentViewModel.Count)
                                    {
                                        para1.Range.ListFormat.RemoveNumbers(Microsoft.Office.Interop.Word.WdNumberType.wdNumberParagraph);
                                        para1.Range.InsertParagraphAfter();
                                    }
                                }
                            }
                        }
                        //Add the end part of the document
                        para1.Range.ParagraphFormat.PageBreakBefore = -1;
                        para1.Range.InsertParagraphAfter();

                        para1.Range.Text = $"This General Order is effective on {DateTime.Now.ToString("dd MMM yyyy")}"; // resMeta[0]..ToString("dd/MM/yyyy"));
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

                        // Save document
                        object filename = dlg.FileName;
                        document.SaveAs(ref filename);
                        // winword.Options.SavePropertiesPrompt = true;

                        //Release the Word COM object
                        ((Microsoft.Office.Interop.Word._Document)document).Close(ref missing, ref missing, ref missing);
                        ((Microsoft.Office.Interop.Word._Application)winword).Quit(ref missing, ref missing, ref missing);
                    });
                }

                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- CreateGeneralOrderDoc.WordDocCreator -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "CreateGeneralOrderDoc.WordDocCreator",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GeneralOrderUpdateDetails", false);
                }

            }
            else
                dlg.FileName = string.Empty;

            return dlg.FileName;
        }
    }
}
