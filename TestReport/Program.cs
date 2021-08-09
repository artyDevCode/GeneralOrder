
using DatabaseEntity;
using GeneralOrderCore;
using GeneralOrderUpdateDetails;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TestReport
{
    public class Program
    {
        static void Main(string[] args)
        {



            var allILDrecs = GetGeneralOrder.GetActAdministrationDoc();
            var res = createReportObjectActAdministratorsReport(allILDrecs.Where(r => r.IsCurrent == true).ToList());
            if (res.Count > 0)
            {
                ActAdministratorsReportClass.CreateActAdministratorsReport(res, $"GO-AllActs-CurrentCommentsOnly-{DateTime.Now.ToString("dd-MM-yyyy hh-mm tt")}");
            }

            //var allILDrecs = GetGeneralOrder.GetActAdministrationDoc();
            //var actRes = createReportObjectAdministeredActs(allILDrecs.Where(r => r.IsCurrent == false).ToList()); //TESTING USING FALSE---- IsCurrent should be == true
            //if (actRes.Count > 0)
            //{
            //    AdministeredActsReportClass.ActAdministeredReportCreator(actRes, $"GO-AllActs-CurrentCommentsOnly-{0}", DateTime.Now.ToString("dd-MM-yyyy hh-mm tt")));
            //}
        }



        // Administered Acts
        private static List<ReportModel> createReportObjectActAdministratorsReport(List<ActAdministrationViewModel> AllRecordsList)
        {
            var allActs = new ObservableCollection<GeneralFieldsViewlModel>();

            var res = AllRecordsList.GroupBy(r => r.PortfolioID).Select(d => new
            {
                portfolioID = d.Key,
                ActList = d.Select(s => new
                {
                    ActAdministrationComment = s.ActAdministrationComment,
                    ActAdministrationID = s.ActAdministrationID,
                    ActCommentModel = s.ActCommentModel,
                    EndDate = s.EndDate,
                    ILDNumber = s.ILDNumber,
                    IsCurrent = s.IsCurrent,
                    IsExcept = s.IsExcept,
                    OnHold = s.OnHold,
                    PendingEditID = s.PendingEditID,
                    PendingEditType = s.PendingEditType,
                    PortfolioID = s.PortfolioID,
                    StartDate = s.StartDate,
                })
                .ToList()
            })
            .ToList();




            var reportList = new List<ReportModel>();

            foreach (var res1 in res)
            {
                var report = new ReportModel();

                List<PortfolioInfoViewModel> port = GetDepartments.GetDepartmentInformation(res1.portfolioID); //GetPortfolios.GetPortfolioInformation(actAdm.PortfolioID);
                report.portfolioID = res1.portfolioID;
                report.portfolio = port[0].PortfolioName;
                report.department = port[0].Department;
                report.deptCode = port[0].DeptCode;
                report.departmentPortfolioID = port[0].DepartmentPortfolioId;
                report.generalFieldsActViewModel = new List<ReportModelAct>();

                foreach (var actAdm in res1.ActList)
                {
                    var reportAct = new ReportModelAct();
                    if (actAdm.ILDNumber > 0) //Check to make sure the record has an ILD number. If the record is corrupt, it may not have one
                    {
                        ActTitleViewModel act = actAdm.ILDNumber.GetActInformation();
                        reportAct.generalFieldsCommentViewModel = new List<ReportModelComments>();


                        reportAct.actAdministrationID = actAdm.ActAdministrationID;
                        reportAct.ildNumber = actAdm.ILDNumber;
                        reportAct.isCurrent = actAdm.IsCurrent;
                        reportAct.pendingEditID = actAdm.PendingEditID;
                        reportAct.isExcept = actAdm.IsExcept;
                        reportAct.pendingEditType = actAdm.PendingEditType;
                        reportAct.startDate = actAdm.StartDate;
                        reportAct.endDate = actAdm.EndDate;
                        reportAct.actTitle = act.ActTitle;
                        reportAct.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(actAdm.PendingEditType)}{actAdm.PendingEditID}";  //  currRecordExists != null ? "A" : string.Empty;
                        reportAct.foregroundColour = actAdm.PendingEditType == 3 ? "Red" : string.Empty;
                        reportAct.backgroundColour = actAdm.PendingEditType == 3 ? "Pink" : string.Empty;


                        foreach (var com in actAdm.ActCommentModel)
                        {
                            var generalFieldsCommentViewModel = new ReportModelComments();
                            generalFieldsCommentViewModel.actAdministrationId = actAdm.ActAdministrationID;
                            generalFieldsCommentViewModel.actAdministrationComment = com.ActAdministrationComment;
                            generalFieldsCommentViewModel.actAdminCommentId = com.ActAdminCommentId;
                            generalFieldsCommentViewModel.fontBold = com.FontBold;
                            generalFieldsCommentViewModel.bulletChar = com.BulletChar;
                            generalFieldsCommentViewModel.bulletASCII = com.BulletASCII;
                            generalFieldsCommentViewModel.pageBreakBefore = com.PageBreakBefore;
                            generalFieldsCommentViewModel.listSymbolFont = com.ListSymbolFont;
                            generalFieldsCommentViewModel.indentationLeft = com.IndentationLeft;
                            generalFieldsCommentViewModel.indentationRight = com.IndentationRight;
                            generalFieldsCommentViewModel.indentationBy = com.IndentationBy;
                            generalFieldsCommentViewModel.tabHangingIndent = com.TabHangingIndent;
                            generalFieldsCommentViewModel.tabStopPosition = com.TabStopPosition;
                            generalFieldsCommentViewModel.alignmentType = com.AlignmentType;

                            reportAct.generalFieldsCommentViewModel.Add(generalFieldsCommentViewModel);
                        }
                        report.generalFieldsActViewModel.Add(reportAct);
                    }
                }

                reportList.Add(report);
            }


            //var result = reportList.OrderBy(r => r.department).ToList();
            //reportList.Clear();
            //reportList = result;
            var orederResult = reportList.OrderBy(r => r.portfolio).ToList();
            var idx = orederResult.FindIndex(x => x.portfolio == "Attorney-General");
            if (idx > -1)
            {
                var item = orederResult[idx];
                orederResult.RemoveAt(idx);
                orederResult.Insert(0, item);
            }

            return orederResult;
        }





        private static List<ActAdministeredReportModelILD> createReportObjectAdministeredActs(List<ActAdministrationViewModel> AllRecordsList)
        {
            var allActs = new ObservableCollection<GeneralFieldsViewlModel>();

            //var res = AllRecordsList.Select(s => new
            //{
            //    ActAdministrationComment = s.ActAdministrationComment,
            //    ActAdministrationID = s.ActAdministrationID,
            //    ActCommentModel = s.ActCommentModel,
            //    EndDate = s.EndDate,
            //    ILDNumber = s.ILDNumber,
            //    IsCurrent = s.IsCurrent,
            //    IsExcept = s.IsExcept,
            //    OnHold = s.OnHold,
            //    PendingEditID = s.PendingEditID,
            //    PendingEditType = s.PendingEditType,
            //    PortfolioID = s.PortfolioID,
            //    StartDate = s.StartDate,
            //})
            // .OrderBy(r => r.ILDNumber)
            // .ToList();

            var res = AllRecordsList.GroupBy(r => r.ILDNumber).Select(d => new
            {
                ildNumber = d.Key,
                ActList = d.Select(s => new
                {
                    ActAdministrationComment = s.ActAdministrationComment,
                    ActAdministrationID = s.ActAdministrationID,
                    ActCommentModel = s.ActCommentModel,
                    EndDate = s.EndDate,
                    ILDNumber = s.ILDNumber,
                    IsCurrent = s.IsCurrent,
                    IsExcept = s.IsExcept,
                    OnHold = s.OnHold,
                    PendingEditID = s.PendingEditID,
                    PendingEditType = s.PendingEditType,
                    PortfolioID = s.PortfolioID,
                    StartDate = s.StartDate,
                })
               .ToList()
            })
           .ToList();

            var reportList = new List<ActAdministeredReportModelILD>();

            foreach (var res1 in res)
            {
                var report = new ActAdministeredReportModelILD();
                report.generalFieldsActViewModel = new List<ActAdministeredReportModel>();

                foreach (var actAdm in res1.ActList)
                {
                    var reportAct = new ActAdministeredReportModel();
                    if (actAdm.ILDNumber > 0) //Check to make sure the record has an ILD number. If the record is corrupt, it may not have one
                    {
                        ActTitleViewModel act = actAdm.ILDNumber.GetActInformation();
                        List<PortfolioInfoViewModel> port = GetDepartments.GetDepartmentInformation(actAdm.PortfolioID); //GetPortfolios.GetPortfolioInformation(actAdm.PortfolioID);

                        reportAct.portfolioID = actAdm.PortfolioID;
                        reportAct.portfolio = port[0].PortfolioName;                       
                        reportAct.generalFieldsCommentViewModel = new List<CommentsAdministeredActsReportModel>();                       
                        reportAct.actAdministrationID = actAdm.ActAdministrationID;
                        reportAct.ildNumber = res1.ildNumber; // actAdm.ILDNumber;
                        reportAct.isCurrent = actAdm.IsCurrent;
                        reportAct.pendingEditID = actAdm.PendingEditID;
                        reportAct.isExcept = actAdm.IsExcept;
                        reportAct.pendingEditType = actAdm.PendingEditType;
                        reportAct.startDate = actAdm.StartDate;
                        reportAct.endDate = actAdm.EndDate;
                        reportAct.actTitle = act.ActTitle;
                        reportAct.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(actAdm.PendingEditType)}{actAdm.PendingEditID}";  //  currRecordExists != null ? "A" : string.Empty;
                        reportAct.foregroundColour = actAdm.PendingEditType == 3 ? "Red" : string.Empty;
                        reportAct.backgroundColour = actAdm.PendingEditType == 3 ? "Pink" : string.Empty;


                        foreach (var com in actAdm.ActCommentModel)
                        {
                            var generalFieldsCommentViewModel = new CommentsAdministeredActsReportModel();
                            generalFieldsCommentViewModel.actAdministrationId = actAdm.ActAdministrationID;
                            generalFieldsCommentViewModel.actAdministrationComment = com.ActAdministrationComment;
                            generalFieldsCommentViewModel.actAdminCommentId = com.ActAdminCommentId;
                            generalFieldsCommentViewModel.fontBold = com.FontBold;
                            generalFieldsCommentViewModel.bulletChar = com.BulletChar;
                            generalFieldsCommentViewModel.bulletASCII = com.BulletASCII;
                            generalFieldsCommentViewModel.pageBreakBefore = com.PageBreakBefore;
                            generalFieldsCommentViewModel.listSymbolFont = com.ListSymbolFont;
                            generalFieldsCommentViewModel.indentationLeft = com.IndentationLeft;
                            generalFieldsCommentViewModel.indentationRight = com.IndentationRight;
                            generalFieldsCommentViewModel.indentationBy = com.IndentationBy;
                            generalFieldsCommentViewModel.tabHangingIndent = com.TabHangingIndent;
                            generalFieldsCommentViewModel.tabStopPosition = com.TabStopPosition;
                            generalFieldsCommentViewModel.alignmentType = com.AlignmentType;

                            reportAct.generalFieldsCommentViewModel.Add(generalFieldsCommentViewModel);
                        }
                        report.generalFieldsActViewModel.Add(reportAct);
                    }
                }
                report.ildNumber = res1.ildNumber.ToString();
                reportList.Add(report);
            }


            //var result = reportList.OrderBy(r => r.department).ToList();
            //reportList.Clear();
            //reportList = result;
            //var orederResult = reportList.OrderBy(r => r.).ToList();
            //var idx = orederResult.FindIndex(x => x.portfolio == "Attorney-General");
            //if (idx > -1)
            //{
            //    var item = orederResult[idx];
            //    orederResult.RemoveAt(idx);
            //    orederResult.Insert(0, item);
            //}

            return reportList;
        }



        //private static void CreateTable()
        //{
        //    object oMissing = System.Reflection.Missing.Value;
        //    object oEndOfDoc = "\\endofdoc";
        //    Microsoft.Office.Interop.Word._Application objWord;
        //    Microsoft.Office.Interop.Word._Document objDoc;
        //    objWord = new Microsoft.Office.Interop.Word.Application();
        //    objWord.Visible = true;
        //    objDoc = objWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);


        //    //Add header into the document
        //    foreach (Microsoft.Office.Interop.Word.Section section in objDoc.Sections)
        //    {
        //        //Get the header range and add the header details.
        //        Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        //        headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
        //        headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        //        headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
        //        headerRange.Font.Size = 10;
        //        headerRange.Text = "Acts Administrators Report header";
        //    }

        //    //Add the footers into the document
        //    foreach (Microsoft.Office.Interop.Word.Section wordSection in objDoc.Sections)
        //    {
        //        //Get the footer range and add the footer details.
        //        Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        //        footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
        //        footerRange.Font.Size = 10;
        //        footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        //        footerRange.Text = "Footer text goes here";
        //    }




        //    //  //Create a new document
        //    //   Microsoft.Office.Interop.Word.Document objDoc = objWord.Documents.Add(ref missing, ref missing, ref missing, ref missing);
        //    objDoc.Styles["Normal"].NoSpaceBetweenParagraphsOfSameStyle = false;
        //    objDoc.PageSetup.LeftMargin = 90;
        //    objDoc.PageSetup.RightMargin = 90;
        //    objDoc.PageSetup.HeaderDistance = 36;
        //    objDoc.PageSetup.FooterDistance = 36;
        //    objDoc.DefaultTabStop = 36;

        //    //adding text to document
        //    objDoc.Content.SetRange(0, 0);
        //    objDoc.Styles["Normal"].Font.Name = "Times New Roman";
        //    //Add paragraph with Normal style
        //    Microsoft.Office.Interop.Word.Paragraph para1 = objDoc.Content.Paragraphs.Add(ref oMissing);
        //    object styleHeading1 = "Normal"; // "Heading 1";
        //    para1.Range.set_Style(ref styleHeading1);
        //    para1.TabStops.ClearAll();
        //    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        //    para1.Range.ParagraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
        //    para1.Range.ParagraphFormat.SpaceAfter = 0;
        //    objWord.Selection.TypeParagraph();
        //    objWord.Selection.TypeParagraph();


        //    //Adding first page title and text as per original General Order document.
        //    para1.Range.Text = "Acts Administrators Report";
        //    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        //    para1.Range.Bold = -1;
        //    para1.Range.ParagraphFormat.LeftIndent = 0;
        //    para1.Range.ParagraphFormat.PageBreakBefore = 0;
        //    para1.Range.ParagraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
        //    para1.Range.ParagraphFormat.SpaceAfter = 0;
        //    para1.Range.InsertParagraphAfter();
        //    para1.Range.InsertParagraph();

        //    //Date
        //    para1.Range.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //    para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
        //    para1.Range.Bold = 0;
        //    para1.Range.InsertParagraphAfter();
        //    para1.Range.InsertParagraph();







        //    int i = 0;
        //    int j = 0;
        //    Microsoft.Office.Interop.Word.Table objTable;
        //    Microsoft.Office.Interop.Word.Range wrdRng = objDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
        //    //  wrdRng = objDoc.Sections[1].Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

        //    objTable = objDoc.Tables.Add(wrdRng, 4, 5, ref oMissing, ref oMissing);
        //    objTable.Range.ParagraphFormat.SpaceAfter = 7;

        //    objTable.Range.Font.Name = "Arial";
        //    objTable.Range.Font.Size = 8;
        //    objTable.Rows[1].Range.Font.Bold = 1;

        //    string strText;
        //    objTable.Columns[1].SetWidth(objWord.Application.CentimetersToPoints(3.864f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
        //    objTable.Columns[2].SetWidth(objWord.Application.CentimetersToPoints(1.005f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
        //    objTable.Columns[3].SetWidth(objWord.Application.CentimetersToPoints(1.534f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
        //    objTable.Columns[4].SetWidth(objWord.Application.CentimetersToPoints(5.372f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
        //    objTable.Columns[5].SetWidth(objWord.Application.CentimetersToPoints(5.504f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);

        //    strText = "Minister";
        //    objTable.Cell(1, 1).Range.Text = strText;
        //    strText = "Year";
        //    objTable.Cell(1, 2).Range.Text = strText;
        //    strText = "Act Number";
        //    objTable.Cell(1, 3).Range.Text = strText;
        //    strText = "Title";
        //    objTable.Cell(1, 4).Range.Text = strText;
        //    strText = "Comments";
        //    objTable.Cell(1, 5).Range.Text = strText;

        //    objTable.Range.Font.Name = "Times New Roman";
        //    objTable.Range.Font.Size = 8;
        //    for (i = 2; i <= 4; i++)  //rows
        //    {
        //        strText = "Attorney-General";
        //        objTable.Cell(i, 1).Range.Text = strText;
        //        strText = "1958";
        //        objTable.Cell(i, 2).Range.Text = strText;
        //        strText = "6188";
        //        objTable.Cell(i, 3).Range.Text = strText;
        //        strText = "Acts Enumeration and Revision Act 1958";
        //        objTable.Cell(i, 4).Range.Text = strText;
        //        strText = "Except: In so far as it relates to the functions of the Registrar - General and the management of the Office of the Registrar - General(in so far as it relates to those matters, the Act is administere by the Minister for Planning) ";
        //        objTable.Cell(i, 5).Range.Text = strText;
        //    }
        // //for (j = 1; j <= 4; j++) //columns
        // //{
        // //    strText = "Attorney-General";
        // //    objTable.Cell(i, j).Range.Text = strText;
        // //}

        // //objTable.Rows[1].Range.Font.Italic = 1;


        // //  objTable.Borders.Shadow = true;
        // ((Microsoft.Office.Interop.Word._Document)objDoc).Close(ref oMissing, ref oMissing, ref oMissing);
        //    ((Microsoft.Office.Interop.Word._Application)wrdRng).Quit(ref oMissing, ref oMissing, ref oMissing);
        //    // objDoc.Close();
        //}
        //public static void ReportTest(string fileName)
        //{
        //    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        //    dlg.FileName = fileName; // "GeneralOrder"; // Default file name
        //    dlg.DefaultExt = ".docx"; // Default file extension
        //    dlg.Filter = "Word Documents|*.doc;*.docx|Excel Worksheets|*.xls|PowerPoint Presentations|*.ppt" +
        //     "|Office Files|*.doc;*.docx;*.xls;*.ppt" +
        //     "|All Files|*.*";

        //    // Show save file dialog box
        //    Nullable<bool> result = dlg.ShowDialog();
        //    if (result == true)
        //    {
        //        CreateTable();
        //    }
        //}
    }
}
