namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepartmentPortfolio")]
    public partial class DepartmentPortfolio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepartmentPortfolio()
        {
            ActAdministrations = new HashSet<ActAdministration>();
        }

        public int DepartmentID { get; set; }

        public int PortfolioID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentPortfolioID { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActAdministration> ActAdministrations { get; set; }

        public virtual Department Department { get; set; }

        public virtual Portfolio Portfolio { get; set; }
    }
}
