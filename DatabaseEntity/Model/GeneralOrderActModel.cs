namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralOrderActModel")]
    public partial class GeneralOrderActModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralOrderActModel()
        {
            GeneralOrderActCommentModels = new HashSet<GeneralOrderActCommentModel>();
        }

        public int GeneralOrderActModelID { get; set; }

        [Required]
        [StringLength(255)]
        public string GeneralOrderActTitle { get; set; }

        public int? ILDNumber { get; set; }

        public int GeneralOrderPortfolioModelID { get; set; }

        public int ParagraphModelID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EffectiveDate { get; set; }

        public string DepartmentPortfolioIDs { get; set; }
        public bool IsExcept { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralOrderActCommentModel> GeneralOrderActCommentModels { get; set; }

        public virtual GeneralOrderPortfolioModel GeneralOrderPortfolioModel { get; set; }

        public virtual ParagraphModel ParagraphModel { get; set; }
    }
}
