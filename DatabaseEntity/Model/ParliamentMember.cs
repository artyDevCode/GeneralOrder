namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParliamentMember")]
    public partial class ParliamentMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParliamentMember()
        {
            ParliamentMemberPartyHouses = new HashSet<ParliamentMemberPartyHouse>();
        }

        public int ParliamentMemberID { get; set; }

        [Required]
        [StringLength(20)]
        public string MemberTitle { get; set; }

        [Required]
        [StringLength(80)]
        public string MemberFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string MemberLastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParliamentMemberPartyHouse> ParliamentMemberPartyHouses { get; set; }
    }
}
