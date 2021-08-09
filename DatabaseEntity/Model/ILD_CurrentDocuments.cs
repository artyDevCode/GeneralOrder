namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ILD_CurrentDocuments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ILD_CurrentDocuments()
        {
            ActAdministrations = new HashSet<ActAdministration>();
        }

        [Key]
        public int ILDNumber { get; set; }

        [Required]
        [StringLength(4)]
        public string DocType { get; set; }

        [Required]
        [StringLength(255)]
        public string DocTitle { get; set; }

        public int ActYear { get; set; }

        public int ActNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string BillType { get; set; }

        [Required]
        [StringLength(50)]
        public string CurrentStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActAdministration> ActAdministrations { get; set; }
    }
}
