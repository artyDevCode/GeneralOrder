using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace GeneralOrderUpdateDetails
{
    public static class HelperFlowDocument
    {
        public static void LoggerFlowDocParser(this FlowDocument loggerFlowDoc, string text)
        {
            if (text == null)
                text = "No Helper text";

            var par = new System.Windows.Documents.Paragraph();
            //par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
            //par.Padding = new Thickness(0, 0, 0, 0);
            //par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
            //par.TextAlignment = TextAlignment.Left; 
            //par.FontFamily = new System.Windows.Media.FontFamily("Courier");
            //par.FontSize = 10;
            //var run = new Run(text);
            //par.Inlines.Add(run);
           // RichTextBoxHelper.DisplayTextRTBHelper(loggerFlowDoc, text, WindowViewModel.instance.UIFontSize);
            par = RichTextBoxHelper.DisplayTextRTBHelper(loggerFlowDoc, text, WindowViewModel.instance.UIFontSize);
            if (loggerFlowDoc.Blocks.FirstBlock == null)
                loggerFlowDoc.Blocks.Add(par);
            else
                loggerFlowDoc.Blocks.InsertBefore(loggerFlowDoc.Blocks.FirstBlock, par);
        }

        public static void WikiFlowDocParser(this FlowDocument wikiFlowDoc, string text)
        {
            if (text == null)
                text = "No Helper text";

            byte[] byteArray = Encoding.ASCII.GetBytes(text);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                //  wikiFlowDoc = new FlowDocument();
                TextRange tr = new TextRange(wikiFlowDoc.ContentStart, wikiFlowDoc.ContentEnd);
                tr.Load(ms, DataFormats.Rtf);
            }
        }

    }
}
