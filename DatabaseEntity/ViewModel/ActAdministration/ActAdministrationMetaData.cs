using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    public class ActAdministrationViewModel
    {
        public int ActAdministrationID { get; set; }
        public int ILDNumber { get; set; }
        public int PortfolioID { get; set; }
        public bool IsCurrent { get; set; }
        public bool OnHold { get; set; }
        public int PendingEditID { get; set; }
        public bool IsExcept { get; set; }
        public short PendingEditType { get; set; }
        public string ActAdministrationComment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<ActAdminCommentViewModel> ActCommentModel { get; set; }
    }


    public  class ActAdminCommentViewModel
    {
        public int ActAdminCommentId { get; set; }
        public bool FontBold { get; set; }
        public string BulletChar { get; set; }
        public short BulletASCII { get; set; }
        public double TabStopPosition { get; set; }
        public bool PageBreakBefore { get; set; }
        public string ListSymbolFont { get; set; }
        public double IndentationLeft { get; set; }
        public double IndentationRight { get; set; }
        public double IndentationBy { get; set; }
        public short TabHangingIndent { get; set; }
        public byte AlignmentType { get; set; }
        public string ActAdministrationComment { get; set; }
    }

}
