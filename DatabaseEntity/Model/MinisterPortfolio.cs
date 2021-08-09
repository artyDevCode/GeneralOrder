namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MinisterPortfolio")]
    public partial class MinisterPortfolio
    {
        public int MinisterID { get; set; }

        public int PortfolioID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MinisterPortfolioID { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public virtual Minister Minister { get; set; }

        public virtual Portfolio Portfolio { get; set; }
    }
}
