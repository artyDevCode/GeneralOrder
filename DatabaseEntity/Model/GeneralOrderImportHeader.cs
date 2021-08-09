namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralOrderImportHeader")]
    public partial class GeneralOrderImportHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralOrderImportHeader()
        {
            GeneralOrderPortfolioModels = new HashSet<GeneralOrderPortfolioModel>();
        }

        public int GeneralOrderImportHeaderID { get; set; }

        [Required]
        public string GeneralOrderImportFile { get; set; }

        [Column(TypeName = "date")]
        public DateTime GeneralOrderEffectiveDate { get; set; }

        public bool GeneralOrderFull { get; set; }

        [Required]
        public string OriginalXML { get; set; }

        [Required]
        public string ModifiedXML { get; set; }

        public DateTime GeneralOrderImportDatetime { get; set; }

        public DateTime? GeneralOrderProcesseddatetime { get; set; }

        public double MarginLeft { get; set; }

        public double MarginRight { get; set; }

        public double DefaultTabStop { get; set; }

        public double PageHeaderMargin { get; set; }

        public double PageFooterMargin { get; set; }

        public bool IndentationSpacing { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralOrderPortfolioModel> GeneralOrderPortfolioModels { get; set; }
    }
}
