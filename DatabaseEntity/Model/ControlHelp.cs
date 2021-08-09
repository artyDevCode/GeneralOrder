namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ControlHelp")]
    public partial class ControlHelp
    {
        public int ControlHelpID { get; set; }

        [Required]
        [StringLength(255)]
        public string ControlName { get; set; }

        [StringLength(512)]
        public string ControlHelpText { get; set; }

        public int? ScreenHelpID { get; set; }

        public virtual ScreenHelp ScreenHelp { get; set; }
    }
}
