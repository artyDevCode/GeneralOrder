namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Minister")]
    public partial class Minister
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Minister()
        {
            MinisterPortfolios = new HashSet<MinisterPortfolio>();
        }

        public int MinisterID { get; set; }

        public int ParliamentMemberPartyHouseID { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MinisterPortfolio> MinisterPortfolios { get; set; }

        public virtual ParliamentMemberPartyHouse ParliamentMemberPartyHouse { get; set; }
    }
}
