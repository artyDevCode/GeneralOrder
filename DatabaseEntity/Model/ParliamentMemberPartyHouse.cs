namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParliamentMemberPartyHouse")]
    public partial class ParliamentMemberPartyHouse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParliamentMemberPartyHouse()
        {
            Ministers = new HashSet<Minister>();
        }

        public int ParliamentMemberPartyHouseID { get; set; }

        public int ParliamentID { get; set; }

        public int PartyID { get; set; }

        public int HouseID { get; set; }

        public int ParliamentMemberID { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public virtual House House { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Minister> Ministers { get; set; }

        public virtual Parliament Parliament { get; set; }

        public virtual ParliamentMember ParliamentMember { get; set; }

        public virtual Party Party { get; set; }
    }
}
