namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActAdminComment")]
    public partial class ActAdminComment
    {
        public int ActAdminCommentID { get; set; }

        [Column("ActAdminComment")]
        [Required]
        public string ActAdminComment1 { get; set; }

        public bool FontBold { get; set; }

        [StringLength(1)]
        public string BulletChar { get; set; }

        public short BulletASCII { get; set; }

        public double TabStopPosition { get; set; }

        public bool PageBreakBefore { get; set; }

        [StringLength(50)]
        public string ListSymbolFont { get; set; }

        public double IndentationLeft { get; set; }

        public double IndentationRight { get; set; }

        public double IndentationBy { get; set; }

        public short TabHangingIndent { get; set; }

        public byte AlignmentType { get; set; }

        public int ActAdministrationID { get; set; }

        public virtual ActAdministration ActAdministration { get; set; }
    }
}
