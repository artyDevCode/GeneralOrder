namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("House")]
    public partial class House
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public House()
        {
            ParliamentMemberPartyHouses = new HashSet<ParliamentMemberPartyHouse>();
        }

        public int HouseID { get; set; }

        [Required]
        [StringLength(2)]
        public string HouseCode { get; set; }

        [Required]
        [StringLength(50)]
        public string HouseName { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParliamentMemberPartyHouse> ParliamentMemberPartyHouses { get; set; }
    }
}
