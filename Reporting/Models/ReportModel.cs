using System;
using System.Collections.Generic;


namespace Reporting
{
    public class ReportModel
    {
        public string portfolio { get; set; }
        public bool PrefixWithMinisterFor { get; set; }
        public int departmentPortfolioID { get; set; }
        public string department { get; set; }
        public int portfolioID { get; set; }
        public string deptCode { get; set; }
        public List<ReportModelAct> generalFieldsActViewModel { get; set; }
    }


    public class ReportModelAct
    {

        public string foregroundColour { get; set; }
        public string backgroundColour { get; set; }
        public string actTitle { get; set; }
        public string change { get; set; }
        public int actAdministrationID { get; set; }
        public int ildNumber { get; set; }
        public bool isCurrent { get; set; }
        public bool onHold { get; set; }
        public int pendingEditID { get; set; }
        public bool isExcept { get; set; }
        public short pendingEditType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
       // public FlowDocument flowDocument { get; set; }

        public List<ReportModelComments> generalFieldsCommentViewModel { get; set; }
    }


    public class ReportModelComments
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
        public double tabStopPosition { get; set; }
        public byte alignmentType { get; set; }
        public string actAdministrationComment { get; set; }
    }





    ///Administered Acts Model for reporting
    public class ActAdministeredReportModelILD
    {
        public string ildNumber { get; set; }
        public List<ActAdministeredReportModel> generalFieldsActViewModel { get; set; }
    }

    public class ActAdministeredReportModel
    {

        public string foregroundColour { get; set; }
        public string backgroundColour { get; set; }
        public string actTitle { get; set; }
        public string change { get; set; }
        public int actAdministrationID { get; set; }
        public int ildNumber { get; set; }
        public bool isCurrent { get; set; }
        public bool onHold { get; set; }
        public int pendingEditID { get; set; }
        public bool isExcept { get; set; }
        public short pendingEditType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int portfolioID { get; set; }
        public string portfolio { get; set; }
        public List<CommentsAdministeredActsReportModel> generalFieldsCommentViewModel { get; set; }
    }


    public class CommentsAdministeredActsReportModel
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
        public double tabStopPosition { get; set; }
        public byte alignmentType { get; set; }
        public string actAdministrationComment { get; set; }
    }
}
