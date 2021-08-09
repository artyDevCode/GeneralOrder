namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralOrderGroupActList")]
    public partial class GeneralOrderGroupActList
    {
        public int GeneralOrderGroupActListID { get; set; }

        public int GeneralOrderGroupActID { get; set; }

        public int GeneralOrderGroupActList_ILDNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string GeneralOrderGroupActList_Title { get; set; }

        public virtual GeneralOrderGroupAct GeneralOrderGroupAct { get; set; }
    }
}
