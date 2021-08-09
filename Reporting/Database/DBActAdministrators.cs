using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntity;
using GeneralOrderCore;

namespace Reporting
{
    public class DBActAdministrators
    {
        public static List<Reporting.ReportModel> createReportObjectActAdministratorsReport(List<ActAdministrationViewModel> AllRecordsList)
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




            var reportList = new List<Reporting.ReportModel>();

            foreach (var res1 in res)
            {
                var report = new Reporting.ReportModel();

                List<PortfolioInfoViewModel> port = GetDepartments.GetDepartmentInformation(res1.portfolioID); //GetPortfolios.GetPortfolioInformation(actAdm.PortfolioID);
                report.portfolioID = res1.portfolioID;
                report.portfolio = port[0].PortfolioName;
                report.department = port[0].Department;
                report.deptCode = port[0].DeptCode;
                report.departmentPortfolioID = port[0].DepartmentPortfolioId;
                report.generalFieldsActViewModel = new List<Reporting.ReportModelAct>();

                foreach (var actAdm in res1.ActList)
                {
                    var reportAct = new Reporting.ReportModelAct();
                    if (actAdm.ILDNumber > 0) //Check to make sure the record has an ILD number. If the record is corrupt, it may not have one
                    {
                        ActTitleViewModel act = actAdm.ILDNumber.GetActInformation();
                        reportAct.generalFieldsCommentViewModel = new List<Reporting.ReportModelComments>();


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
                            var generalFieldsCommentViewModel = new Reporting.ReportModelComments();
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


        public static List<Reporting.ActAdministeredReportModelILD> createReportObjectAdministeredActs(List<ActAdministrationViewModel> AllRecordsList)
        {
            var allActs = new ObservableCollection<GeneralFieldsViewlModel>();

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

            var reportList = new List<Reporting.ActAdministeredReportModelILD>();

            foreach (var res1 in res)
            {
                var report = new Reporting.ActAdministeredReportModelILD();
                report.generalFieldsActViewModel = new List<Reporting.ActAdministeredReportModel>();

                foreach (var actAdm in res1.ActList)
                {
                    var reportAct = new Reporting.ActAdministeredReportModel();
                    if (actAdm.ILDNumber > 0) //Check to make sure the record has an ILD number. If the record is corrupt, it may not have one
                    {
                        ActTitleViewModel act = actAdm.ILDNumber.GetActInformation();
                        List<PortfolioInfoViewModel> port = GetDepartments.GetDepartmentInformation(actAdm.PortfolioID); //GetPortfolios.GetPortfolioInformation(actAdm.PortfolioID);

                        reportAct.portfolioID = actAdm.PortfolioID;
                        reportAct.portfolio = port[0].PortfolioName;
                        reportAct.generalFieldsCommentViewModel = new List<Reporting.CommentsAdministeredActsReportModel>();
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
                            var generalFieldsCommentViewModel = new Reporting.CommentsAdministeredActsReportModel();
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

            return reportList;
        }

    }
}
