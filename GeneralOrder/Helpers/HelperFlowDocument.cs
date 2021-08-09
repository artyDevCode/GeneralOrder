using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace GeneralOrder
{
    /// <summary>
    /// WIKI helper 
    /// </summary>
    public static class HelperFlowDocument
    {
        /// <summary>
        /// Adds the text to the application logger flowdoc 
        /// </summary>
        /// <param name="loggerFlowDoc">Logger flowdoc that belongs to the main application window</param>
        /// <param name="text">Text send by the application calling this method. The text derives from the Database</param>
        public static void LoggerFlowDocParser(this FlowDocument loggerFlowDoc, string text)
        {
            if (text == null)
                text = "No Helper text";

            var par = new System.Windows.Documents.Paragraph();
            par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
            par.Padding = new Thickness(0, 0, 0, 0);

            par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
            par.TextAlignment = TextAlignment.Left;
            par.FontFamily = new System.Windows.Media.FontFamily("Courier");
            par.FontSize = WindowViewModel.instance.UIFontSize; // 10;
            var run = new Run(text);
            par.Inlines.Add(run);



            if (loggerFlowDoc.Blocks.FirstBlock == null)
                loggerFlowDoc.Blocks.Add(par);
            else
                loggerFlowDoc.Blocks.InsertBefore(loggerFlowDoc.Blocks.FirstBlock, par);
            
        }

        /// <summary>
        /// Adds the text to the WIKI flowdoc
        /// </summary>
        /// <param name="wikiFlowDoc">Wiki flowdoc that belongs to the calling application. The calls come from the current window</param>
        /// <param name="text">Text send by the application calling this method. The text derives from the Database</param>
        public static void WikiFlowDocParser(this FlowDocument wikiFlowDoc, string text)
        {
            if (text == null)
                text = "No Helper text";

            byte[] byteArray = Encoding.ASCII.GetBytes(text);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                TextRange tr = new TextRange(wikiFlowDoc.ContentStart, wikiFlowDoc.ContentEnd);
                tr.Load(ms, DataFormats.Rtf);
            }
        }

    }
}
