namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParagraphModelType")]
    public partial class ParagraphModelType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParagraphModelType()
        {
            ParagraphModels = new HashSet<ParagraphModel>();
        }

        public int ParagraphModelTypeID { get; set; }

        [Required]
        [StringLength(250)]
        public string ParagraphModelTypeName { get; set; }

        public bool FontBold { get; set; }

        [StringLength(1)]
        public string BulletChar { get; set; }

        public short BulletASCII { get; set; }

        public double? TabStopPosition { get; set; }

        public bool PageBreakBefore { get; set; }

        [StringLength(50)]
        public string ListSymbolFont { get; set; }

        public double IndentationLeft { get; set; }

        public double IndentationRight { get; set; }

        public double IndentationBy { get; set; }

        public short TabHangingIndent { get; set; }

        public byte AlignmentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParagraphModel> ParagraphModels { get; set; }
    }
}
