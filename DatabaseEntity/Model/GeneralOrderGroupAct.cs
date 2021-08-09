namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralOrderGroupAct")]
    public partial class GeneralOrderGroupAct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralOrderGroupAct()
        {
            GeneralOrderGroupActLists = new HashSet<GeneralOrderGroupActList>();
        }

        public int GeneralOrderGroupActID { get; set; }

        [Required]
        [StringLength(255)]
        public string GeneralOrderGroupActTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralOrderGroupActList> GeneralOrderGroupActLists { get; set; }
    }
}
