namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralOrderImporFileAttrib")]
    public partial class GeneralOrderImporFileAttrib
    {
        public int GeneralOrderImporFileAttribID { get; set; }

        public double MarginLeft { get; set; }

        public double MarginRight { get; set; }

        public double DefaultTabStop { get; set; }

        public double PageHeaderMargin { get; set; }

        public double PageFooterMargin { get; set; }

        public bool IndentationSpacing { get; set; }
    }
}
