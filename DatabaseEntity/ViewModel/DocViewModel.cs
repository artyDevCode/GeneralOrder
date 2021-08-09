

namespace DatabaseEntity
{
    /// <summary>
    /// Paragraph Style Model for all paragraphs in the document. <see cref="GeneralOrderMetadataViewModel"/>
    /// </summary>
    public class DocViewModel
    {
        public bool fontBold { get; set; }
        public string bulletChar { get; set; }
        public short bulletASCI { get; set; }
        public float tabStopPosition { get; set; }
        public bool pageBreakBefore { get; set; }
        public string listSymbolFont { get; set; }
        public float IndentationLeft { get; set; }
        public float IndentationRight { get; set; }
        public float indentationBy { get; set; }
        public short TabHangingIndent { get; set; } 
        public byte paragraphAlignment { get; set; }
    }
}
