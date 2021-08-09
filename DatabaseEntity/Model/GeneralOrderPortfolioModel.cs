namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralOrderPortfolioModel")]
    public partial class GeneralOrderPortfolioModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralOrderPortfolioModel()
        {
            GeneralOrderActModels = new HashSet<GeneralOrderActModel>();
        }

        public int GeneralOrderPortfolioModelID { get; set; }

        public int GeneralOrderImportHeaderID { get; set; }

        [Required]
        [StringLength(100)]
        public string GeneralOrderPortfolioName { get; set; }

        public int? GeneralOrderPortfolioID { get; set; }

        public int ParagraphModelID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralOrderActModel> GeneralOrderActModels { get; set; }

        public virtual GeneralOrderImportHeader GeneralOrderImportHeader { get; set; }

        public virtual ParagraphModel ParagraphModel { get; set; }
    }
}
