using ApplicationLogger;
using DatabaseEntity;
using GeneralOrderCore;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Reporting
{
    public class ActAdministrationReport
    {
        /// <summary>
        /// WordDocCreator creates a General Order document based on the original (reverse engineering) from an onject <see cref="GeneralOrderMetadataViewModel"/>
        /// resMeta represents the model
        /// </summary>
        /// <param name="resMeta"></param>

        public static async System.Threading.Tasks.Task<string> ActAdministrationReportCreator(List<Reporting.ReportModel> resMeta)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Act Administrators Report"; // "GeneralOrder"; // Default file name
            dlg.DefaultExt = ".docx"; // Default file extension
            dlg.Filter = "Word Documents|*.doc;*.docx|Excel Worksheets|*.xls|PowerPoint Presentations|*.ppt" +
             "|Office Files|*.doc;*.docx;*.xls;*.ppt" +
             "|Pdf Files|*.pdf" +
             "|All Files|*.*";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                try
                {
                    await System.Threading.Tasks.Task.Run(() =>
                    {
                        //Create an instance for word app
                        Microsoft.Office.Interop.Word.Application winword = new Application();

                        winword.Visible = false;

                        //Create a missing variable for missing value
                        object missing = System.Reflection.Missing.Value;

                        //Create a new document
                        Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                        document.PageSetup.RightMargin = 17.9f;
                        document.PageSetup.LeftMargin = 17.9f;
                        document.PageSetup.TopMargin = 17.9f;
                        document.PageSetup.BottomMargin = 17.9f;
                        document.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
                        winword.Options.SavePropertiesPrompt = false;

                        Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                        string strText;
                        //  Microsoft.Office.Interop.Word.Section section = document.Sections.Add();

                        //Add header into the document
                        foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                        {
                            //Get the header range and add the header details.
                            Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

                            headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                            headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            headerRange.Bold = -1;
                            headerRange.Font.Size = 10;
                            headerRange.Font.Name = "Arial";
                            headerRange.Text = "Acts Administration Report";
                            headerRange.Underline = WdUnderline.wdUnderlineSingle;
                            headerRange.InsertParagraphAfter();

                            para1.Range.ParagraphFormat.SpaceAfter = 0;
                            para1.Range.Font.Name = "Times New Roman";
                            para1.Range.Font.Size = 8;
                            para1.Range.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                            para1.Range.ParagraphFormat.SpaceAfter = 0;
                            para1.Range.InsertParagraphAfter();

                        }




                        // Add the footers into the document
                        foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                        {
                            var res = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers.Add();
                            res.Alignment = WdPageNumberAlignment.wdAlignPageNumberCenter;
                            Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                            footerRange.InlineShapes.AddHorizontalLineStandard();
                            footerRange.Font.Size = 10;
                            footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            // footerRange.Text = "Page "; // + para1.Range.get_Information(WdInformation.wdActiveEndPageNumber);
                        }


                        //get the count of the database records
                        int cnt = 0;
                        var listCnt = resMeta.Select(r => r.generalFieldsActViewModel.Count());
                        foreach (int cntTemp in listCnt)
                        {
                            cnt += cntTemp;
                        }


                        Microsoft.Office.Interop.Word.Table objTable = document.Tables.Add(para1.Range, cnt + 1, 5, ref missing, ref missing);
                        objTable.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthAuto;
                        objTable.AllowAutoFit = true;
                        objTable.Borders.Enable = 0;
                        objTable.Columns[1].SetWidth(winword.Application.CentimetersToPoints(3.864f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
                        objTable.Columns[2].SetWidth(winword.Application.CentimetersToPoints(1.005f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
                        objTable.Columns[3].SetWidth(winword.Application.CentimetersToPoints(1.534f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
                        objTable.Columns[4].SetWidth(winword.Application.CentimetersToPoints(5.372f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
                        if (document.PageSetup.Orientation == WdOrientation.wdOrientLandscape)
                            objTable.Columns[5].SetWidth(winword.Application.CentimetersToPoints(16.66f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
                        else
                            objTable.Columns[5].SetWidth(winword.Application.CentimetersToPoints(9.66f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);


                        int i = 1;
                        string previousPortfolio = "";
                        objTable.Range.Font.Name = "Times New Roman";
                        objTable.Range.Font.Size = 8;

                        //header
                        objTable.Range.Bold = -1;
                        strText = "Minister";
                        objTable.Rows[1].Cells[1].Range.Text = strText;
                        objTable.Rows[1].Cells[1].Range.Bold = 1;
                        strText = "Year";
                        objTable.Rows[1].Cells[2].Range.Text = strText;
                        strText = "Act Number";
                        objTable.Rows[1].Cells[3].Range.Text = strText;
                        strText = "Title";
                        objTable.Rows[1].Cells[4].Range.Text = strText;
                        strText = "Comments";
                        objTable.Rows[1].Cells[5].Range.Text = strText;
                        objTable.Rows[1].HeadingFormat = -1;
                        objTable.Rows[1].Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
                        objTable.Rows[1].Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;



                        objTable.Range.Bold = 0;
                        foreach (var paraRes in resMeta)
                        {
                            foreach (var paraAct in paraRes.generalFieldsActViewModel)
                            {
                                i++; // start from row 2. row 1 is for the heading
                                Row row = objTable.Rows[i];

                                if (paraRes.portfolio != previousPortfolio)
                                {
                                    var portfolioSearch = paraRes.portfolio.GetPortfolioInfo();
                                    row.Cells[1].Range.Bold = 1;
                                    row.Cells[1].Range.Text = portfolioSearch.PrefixWithMinisterFor ? $"Minister for {paraRes.portfolio}" : paraRes.portfolio;
                                    previousPortfolio = paraRes.portfolio;
                                    row.Cells[1].Range.ParagraphFormat.KeepWithNext = -1;
                                }
                                row.Cells[1].Range.Bold = 0;
                                row.Cells[2].Range.Text = paraAct.actTitle.Substring(paraAct.actTitle.Length - 4, 4);
                                row.Cells[3].Range.Text = paraAct.ildNumber.ToString();
                                row.Cells[4].Range.Text = paraAct.actTitle;

                                if (paraAct.isExcept)
                                {
                                    row.Cells[5].Range.Paragraphs[1].Range.Text = "Except:";
                                    row.Cells[5].Range.InsertParagraphAfter();
                                }

                                //Loop through the Act Comments

                                foreach (var actcommentRes in paraAct.generalFieldsCommentViewModel)
                                {
                                    //  commCount++;
                                    row.Cells[5].Range.Font.Name = "Times New Roman";
                                    row.Cells[5].Range.Font.Size = 8;
                                    row.Cells[5].Range.Bold = actcommentRes.fontBold == false ? 0 : -1;
                                    row.Cells[5].Range.ParagraphFormat.PageBreakBefore = actcommentRes.pageBreakBefore == false ? 0 : -1;

                                    var parCount = row.Cells[5].Range.Paragraphs.Count;

                                    row.Cells[5].Range.Paragraphs[parCount].Range.Text = actcommentRes.actAdministrationComment;

                                    switch (actcommentRes.alignmentType)
                                    {
                                        case 0:
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                            break;
                                        case 1:
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                            break;
                                        case 2:
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                            break;
                                        default:
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                            break;
                                    }

                                    //Get the correct bullet for the List and set the paragraph format
                                    switch (actcommentRes.bulletASCII)
                                    {
                                        case 183: //Default solid round
                                        case 111: //Default hollow round
                                        case 167: //Default solid square
                                            string Name = string.Empty;
                                            if (actcommentRes.bulletASCII == 183)
                                            {
                                                Name = "ReportBullet1";
                                            }

                                            if (actcommentRes.bulletASCII == 111)
                                            {
                                                Name = "ReportBullet2";
                                            }

                                            if (actcommentRes.bulletASCII == 167)
                                            {
                                                Name = "ReportBullet3";
                                            }


                                            var Type = WdStyleType.wdStyleTypeParagraph;

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
                                                ListTemplate template = row.Cells[5].Range.Paragraphs[parCount].Range.Application.ListGalleries[WdListGalleryType.wdBulletGallery].ListTemplates[1];
                                                template.ListLevels[1].NumberStyle = WdListNumberStyle.wdListNumberStyleBullet;

                                                if (actcommentRes.bulletASCII == 183)
                                                {
                                                    template.ListLevels[1].NumberFormat = Convert.ToChar(61623).ToString();  //char.ConvertFromUtf32(61623);
                                                }
                                                if (actcommentRes.bulletASCII == 111)
                                                {
                                                    template.ListLevels[1].NumberFormat = Convert.ToChar(9675).ToString(); // "o";
                                                }
                                                if (actcommentRes.bulletASCII == 167)
                                                {
                                                    template.ListLevels[1].NumberFormat = Convert.ToChar(61607).ToString();  //char.ConvertFromUtf32(61607);
                                                }

                                                //  template.ListLevels[1].NumberPosition = (float)actcommentRes.indentationLeft;
                                                //  template.ListLevels[1].Alignment = WdListLevelAlignment.wdListLevelAlignLeft;


                                                //  template.ListLevels[1].TextPosition = (float)actcommentRes.indentationLeft;
                                                template.ListLevels[1].TabPosition = 0;
                                                template.ListLevels[1].Font.Name = actcommentRes.listSymbolFont;

                                                winword.ActiveDocument.Styles.Add(Name, Type);

                                                //winword.ActiveDocument.Styles[Name].AutomaticallyUpdate = false;
                                                //winword.ActiveDocument.Styles[Name].set_BaseStyle("");
                                                //winword.ActiveDocument.Styles[Name].set_NextParagraphStyle("Normal");


                                                winword.ActiveDocument.Styles[Name].Font.Name = "Times New Roman";
                                                winword.ActiveDocument.Styles[Name].Font.Size = 8;
                                                //winword.ActiveDocument.Styles[Name].Font.Bold = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Italic = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Underline = WdUnderline.wdUnderlineNone;
                                                //winword.ActiveDocument.Styles[Name].Font.UnderlineColor = WdColor.wdColorAutomatic;
                                                //winword.ActiveDocument.Styles[Name].Font.StrikeThrough = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.DoubleStrikeThrough = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Outline = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Emboss = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Shadow = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Hidden = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.SmallCaps = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.AllCaps = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Color = WdColor.wdColorAutomatic;
                                                //winword.ActiveDocument.Styles[Name].Font.Engrave = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Superscript = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Subscript = 0;
                                                //winword.ActiveDocument.Styles[Name].Font.Kerning = 0;

                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.SpaceBefore = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.SpaceBeforeAuto = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.SpaceAfterAuto = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceMultiple;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.KeepWithNext = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.KeepTogether = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.NoLineNumber = 0;

                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.CharacterUnitLeftIndent = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.CharacterUnitRightIndent = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.CharacterUnitFirstLineIndent = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.LineUnitBefore = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.LineUnitAfter = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.MirrorIndents = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
                                                //winword.ActiveDocument.Styles[Name].NoSpaceBetweenParagraphsOfSameStyle = false;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.TabStops.ClearAll();
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.Shading.Texture = WdTextureIndex.wdTextureNone;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.Shading.ForegroundPatternColor = WdColor.wdColorAutomatic;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.Shading.BackgroundPatternColor = WdColor.wdColorAutomatic;

                                                winword.ActiveDocument.Styles[Name].ParagraphFormat.TabHangingIndent(actcommentRes.tabHangingIndent);
                                                winword.ActiveDocument.Styles[Name].ParagraphFormat.LeftIndent = (float)actcommentRes.indentationLeft;
                                                winword.ActiveDocument.Styles[Name].ParagraphFormat.RightIndent = (float)actcommentRes.indentationRight;
                                                winword.ActiveDocument.Styles[Name].ParagraphFormat.PageBreakBefore = actcommentRes.pageBreakBefore == true ? 1 : 0;
                                                winword.ActiveDocument.Styles[Name].ParagraphFormat.TabStops.Add((float)actcommentRes.tabStopPosition);
                                                winword.ActiveDocument.Styles[Name].ParagraphFormat.FirstLineIndent = (float)actcommentRes.indentationBy;

                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.SpaceAfter = 0;
                                                //winword.ActiveDocument.Styles[Name].ParagraphFormat.Borders.Shadow = false;
                                                //winword.ActiveDocument.Styles[Name].LanguageID = WdLanguageID.wdEnglishAUS;
                                                //winword.ActiveDocument.Styles[Name].NoProofing = 0;
                                                //winword.ActiveDocument.Styles[Name].Frame.Delete();


                                                template.ListLevels[1].LinkedStyle = Name;
                                                winword.ActiveDocument.Styles[Name].LinkToListTemplate(template, 1);
                                            }

                                            row.Cells[5].Range.Paragraphs[parCount].Range.set_Style(Name);
                                            row.Cells[5].Range.InsertParagraphAfter();
                                            row.Cells[5].Range.Paragraphs[parCount + 1].Range.ListFormat.RemoveNumbers();
                                            break;
                                        default: //This is used for comments that dont have a bullet. These are generally at the end of the Act and are enclosed in brackets   
                                            if (row.Cells[5].Range.Paragraphs[parCount].Range.ListFormat != null)
                                                row.Cells[5].Range.Paragraphs[parCount].Range.ListFormat.RemoveNumbers();
                                            // row.Cells[5].Range.Paragraphs[parCount].Range.ListFormat.ApplyBulletDefault("None");
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.TabHangingIndent(actcommentRes.tabHangingIndent);
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.LeftIndent = (float)actcommentRes.indentationLeft;
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.RightIndent = (float)actcommentRes.indentationRight;
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.FirstLineIndent = (float)actcommentRes.indentationBy;
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.TabStops[1].Position = (float)actcommentRes.tabStopPosition;
                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.PageBreakBefore = actcommentRes.pageBreakBefore == true ? 1 : 0;
                                            row.Cells[5].Range.Paragraphs[parCount].Range.Font.Name = actcommentRes.listSymbolFont;
                                            row.Cells[5].Range.Paragraphs[parCount].Indent();

                                            row.Cells[5].Range.Paragraphs[parCount].Range.ParagraphFormat.SpaceAfter = 0;
                                            row.Cells[5].Range.InsertParagraphAfter();
                                            break;

                                    }


                                    ////Insert a blank paragraph after the last Comment.
                                    //if (commCount == paraAct.generalFieldsCommentViewModel.Count)
                                    //{                               
                                    //    row.Cells[5].Range.Paragraphs.Add().Range.Text = "";
                                    //}


                                }
                            }
                        }

                        object filename = dlg.FileName;

                        if (Path.GetExtension(filename.ToString()) == ".pdf")
                        {
                            document.ExportAsFixedFormat(filename.ToString(), WdExportFormat.wdExportFormatPDF, false, WdExportOptimizeFor.wdExportOptimizeForPrint,
                                WdExportRange.wdExportAllDocument, 1, 1, WdExportItem.wdExportDocumentContent, true, true, WdExportCreateBookmarks.wdExportCreateNoBookmarks, true, true, false);
                        }
                        else
                        {
                            document.SaveAs(ref filename);
                        }

                         //Release the Word COM object
                         ((Microsoft.Office.Interop.Word._Document)document).Close(false, ref missing, ref missing);  //(ref missing, ref missing, ref missing);
                        ((Microsoft.Office.Interop.Word._Application)winword).Quit(ref missing, ref missing, ref missing);
                    });

                }
                catch (Exception e)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- ActAdministrationReport.ActAdministrationReportCreator -- { e.Message} -- Contact IT support.");
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "Exception",
                        LogDetail = e.Message,
                        LogDetail_Additional = "ActAdministrationReport.ActAdministrationReportCreator",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "Reporting", false);
                }
            }
            return dlg.FileName;
        }

    }
}
