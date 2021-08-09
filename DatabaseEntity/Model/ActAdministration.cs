namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActAdministration")]
    public partial class ActAdministration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActAdministration()
        {
            ActAdminComments = new HashSet<ActAdminComment>();
        }

        public int ActAdministrationID { get; set; }

        public int ILDNumber { get; set; }

        public int DepartmentPortfolioID { get; set; }

        public bool IsCurrent { get; set; }
        public bool OnHold { get; set; }
        public int PendingEditID { get; set; }
        public bool IsExcept { get; set; }
        public short PendingEditType { get; set; }

        [Column(TypeName = "ntext")]
        public string ActAdministrationComment { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActAdminComment> ActAdminComments { get; set; }

        public virtual DepartmentPortfolio DepartmentPortfolio { get; set; }

        public virtual ILD_CurrentDocuments ILD_CurrentDocuments { get; set; }
    }
}
