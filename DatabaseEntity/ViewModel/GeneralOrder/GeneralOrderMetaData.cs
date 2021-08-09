using System;
using System.Collections.Generic;


namespace DatabaseEntity
{
    /// <summary>
    /// Overall Structure of General Order Word document. This includes document based style
    /// </summary>
    public class GeneralOrderMetadataViewModel
    {
        public int ImportHeaderID { get; set; }
        public DateTime orderEffective { get; set; }
        public bool orderfull { get; set; }
        public string orderFileName { get; set; }
        public string OriginalXML { get; set; }
        public double marginLeft { get; set; }
        public double marginRight { get; set; }
        public double defaultTabStop { get; set; }
        public double pageHeaderMargin { get; set; }
        public double pageFooterMargin { get; set; }
        public bool IndentationSpacing { get; set; }
        //GOModel will contain all the paragraphs of the document
        public List<GeneralOrderPortfolioViewModel> GOModel { get; set; }
    }
    /// <summary>
    /// General Order view model based on Portfolios and its content. This includes paragraph style 
    /// </summary>
    public class GeneralOrderPortfolioViewModel
    {
        public int generalOrderPortfolioModelID { get; set; }
        public int generalOrderPortfolioID { get; set; }
        public string generalOrderPortfolioName { get; set; }
        //public bool generalOrderPrefixWithMinisterFor { get; set; }

        public DocViewModel generalOrderPortfolioParagraphMeta { get; set; }
        public List<GeneralOrderActTitleViewModel> genOrderActTitle { get; set; }
    }

    /// <summary>
    /// General Order view model for Act Title. This includes paragraph style 
    /// </summary>
    public class GeneralOrderActTitleViewModel
    {
        public int generalOrderActTitleModelID { get; set; }
        public int generalOrderPortfolioModelID { get; set; }
        public string generalOrderActTitle { get; set; }
        public int ildNumber { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string DepartmentPortfolioIDs { get; set; }
        public bool isExcept { get; set; }
        public DocViewModel generalOrderActTitleParagraphMeta { get; set; }
        public List<GeneralOrderActCommentViewModel> genOrderActComment { get; set; }
    }

    /// <summary>
    /// General Order view model for Act Comments. This includes paragraph style 
    /// </summary>
    public class GeneralOrderActCommentViewModel
    {
        public int generalOrderActCommentModelID { get; set; }
        public int generalOrderActTitleModelID { get; set; }
        public DocViewModel generalOrderActCommentParagraphMeta { get; set; }
        public string generalOrderActComment { get; set; }
    }
}

