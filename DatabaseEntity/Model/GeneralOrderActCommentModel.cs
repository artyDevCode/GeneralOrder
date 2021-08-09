namespace DatabaseEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralOrderActCommentModel")]
    public partial class GeneralOrderActCommentModel
    {
        public int ParagraphModelID { get; set; }

        public int GeneralOrderActModelID { get; set; }

        public int GeneralOrderActCommentModelID { get; set; }

        public string GeneralOrderActComment { get; set; }

        public virtual GeneralOrderActModel GeneralOrderActModel { get; set; }

        public virtual ParagraphModel ParagraphModel { get; set; }
    }
}
