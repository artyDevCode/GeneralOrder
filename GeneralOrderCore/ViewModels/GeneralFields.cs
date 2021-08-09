using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GeneralOrderCore;
using System.Windows.Documents;
using System.Collections.ObjectModel;

namespace GeneralOrderCore
{
    public class GeneralFieldsViewlModel : BaseViewModel
    {
        public string foregroundColour { get; set; }
        public string backgroundColour { get; set; }
        public string isDelete { get; set; }
        public string department { get; set; }
        public string deptCode { get; set; }
        public string actTitle { get; set; }
        public string portfolio { get; set; }
        public int departmentPortfolioID { get; set; }
        public string change { get; set; }
        public int actAdministrationID { get; set; }
        public int ildNumber { get; set; }
        public int portfolioID { get; set; }
        public bool isCurrent { get; set; }
        public bool onHold { get; set; }
        public int pendingEditID { get; set; }
        public bool isExcept { get; set; }
        public short pendingEditType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public FlowDocument flowDocument { get; set; }
        public ObservableCollection<GeneralFieldsCommentViewModel> generalFieldsCommentViewModel { get; set; }
    }

    public class GeneralFieldsCommentViewModel
    { 
        public int actAdminCommentId { get; set; }
        public int actAdministrationId { get; set; }
        public bool fontBold { get; set; }
        public string bulletChar { get; set; }
        public short bulletASCII { get; set; }
        public bool pageBreakBefore { get; set; }
        public string listSymbolFont { get; set; }
        public double indentationLeft { get; set; }
        public double indentationRight { get; set; }
        public double indentationBy { get; set; }
        public short tabHangingIndent { get; set; }
        public double  tabStopPosition { get; set; }
        public byte alignmentType { get; set; }
        public string actAdministrationComment { get; set; }
    }

    public static class GeneralFieldsViewlModelCopy
    {
        public static GeneralFieldsViewlModel CopyGeneralFields(this GeneralFieldsViewlModel recCopySelected)
        {
            GeneralFieldsViewlModel recordSelected = new GeneralFieldsViewlModel
            {
                foregroundColour = recCopySelected.foregroundColour,
                backgroundColour = recCopySelected.backgroundColour,
                department = recCopySelected.department,
                deptCode = recCopySelected.deptCode,
                actTitle = recCopySelected.actTitle,
                portfolio = recCopySelected.portfolio,
                change = recCopySelected.change,
                actAdministrationID = recCopySelected.actAdministrationID,
                ildNumber = recCopySelected.ildNumber,
                portfolioID = recCopySelected.portfolioID,
                departmentPortfolioID = recCopySelected.departmentPortfolioID,
                isCurrent = recCopySelected.isCurrent,
                onHold = recCopySelected.onHold,
                pendingEditID = recCopySelected.pendingEditID,
                isExcept = recCopySelected.isExcept,
                pendingEditType = recCopySelected.pendingEditType,
                startDate = recCopySelected.startDate,
                endDate = recCopySelected.endDate,
                flowDocument = recCopySelected.flowDocument,
                generalFieldsCommentViewModel = new ObservableCollection<GeneralOrderCore.GeneralFieldsCommentViewModel>()
            };

            foreach (var r in recCopySelected.generalFieldsCommentViewModel)
            {
                recordSelected.generalFieldsCommentViewModel.Add(
                    new GeneralOrderCore.GeneralFieldsCommentViewModel
                    {
                        actAdminCommentId = r.actAdminCommentId,
                        fontBold = r.fontBold,
                        bulletChar = r.bulletChar,
                        bulletASCII = r.bulletASCII,
                        pageBreakBefore = r.pageBreakBefore,
                        listSymbolFont = r.listSymbolFont,
                        indentationLeft = r.indentationLeft,
                        indentationRight = r.indentationRight,
                        indentationBy = r.indentationBy,
                        tabHangingIndent = r.tabHangingIndent,
                        tabStopPosition = r.tabStopPosition,
                        alignmentType = r.alignmentType,
                        actAdministrationComment = r.actAdministrationComment
                    });
            }
            return recordSelected;
        }
    }

}
