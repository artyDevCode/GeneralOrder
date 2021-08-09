namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ScreenHelp")]
    public partial class ScreenHelp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScreenHelp()
        {
            ControlHelps = new HashSet<ControlHelp>();
        }

        public int ScreenHelpID { get; set; }

        [Required]
        [StringLength(255)]
        public string ScreenName { get; set; }

        public string ScreenHelpText { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlHelp> ControlHelps { get; set; }
    }
}
