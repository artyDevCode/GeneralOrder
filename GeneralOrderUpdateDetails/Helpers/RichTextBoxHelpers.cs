using DatabaseEntity;
using GeneralOrderCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace GeneralOrderUpdateDetails
{
    public static class RichTextBoxHelpers
    {
        public static ObservableCollection<GeneralFieldsCommentViewModel> LoopThroughFlowDoc(this FlowDocument flowDoc, int actAdminID)
        {
            var meta = new DocViewModel();
            ObservableCollection<GeneralFieldsCommentViewModel> array = new ObservableCollection<GeneralFieldsCommentViewModel>();

            //delete all the records in the ActAdminComments that relate to actAdminId so new comments can be created
            GOUpdateDetailsImportedFiles.GOUpdateActAdminCommentsDelete(actAdminID);

            foreach (var aa in flowDoc.Blocks)
            {
                TextRange blockRange = new TextRange(aa.ContentStart, aa.ContentEnd);
                var bulletASCIByte = (byte)blockRange.Text[0];
                meta = null;

                switch (bulletASCIByte)
                {
                    case 34: //this is WPF ascii code for the bullet since it only has a small set to choose from
                        meta =  ParagraphModelDC.GetStandardizedParagraphMeta(183, "GeneralOrderActComment");
                        break;
                    case 203:
                        meta =  ParagraphModelDC.GetStandardizedParagraphMeta(111, "GeneralOrderActComment");
                        break;
                    case 160:
                        meta =  ParagraphModelDC.GetStandardizedParagraphMeta(167, "GeneralOrderActComment");
                        break;
                    default:
                        //Since there are no bullets, use the leftindent to see what paragraph info to save
                        switch (aa.Margin.Left)
                        {
                            //case 1:
                            //    meta =  ParagraphModelDC.GetStandardizedParagraphMeta(996, "GeneralOrderActComment");
                            //    break;
                            //case 10:
                            //    meta =  ParagraphModelDC.GetStandardizedParagraphMeta(997, "GeneralOrderActComment");
                            //    break;
                            //case 50:
                            //    meta =  ParagraphModelDC.GetStandardizedParagraphMeta(998, "GeneralOrderActComment");
                            //    break;
                            //default: //case 78:
                            //    meta =   ParagraphModelDC.GetStandardizedParagraphMeta(999, "GeneralOrderActComment");
                            //    break;
                            case double m when (m >= 0 && m < 10):
                                meta = ParagraphModelDC.GetStandardizedParagraphMeta(996, "GeneralOrderActComment");
                                break;
                            case double m when (m > 9 && m < 23):
                                meta = ParagraphModelDC.GetStandardizedParagraphMeta(997, "GeneralOrderActComment");
                                break;
                            case double m when (m > 22 && m < 55):
                                meta = ParagraphModelDC.GetStandardizedParagraphMeta(998, "GeneralOrderActComment");
                                break;
                            default:     //case double m when (m > 54 && m < 90):
                                meta = ParagraphModelDC.GetStandardizedParagraphMeta(999, "GeneralOrderActComment");
                                break;
                        }
                        break;
                }


                var com = string.Empty;
                if (bulletASCIByte == 34 || bulletASCIByte == 203 || bulletASCIByte == 160)
                    com = blockRange.Text.Remove(0, 2);
                else
                    com = blockRange.Text.Trim('\t');

              

                array.Add(
                    new GeneralFieldsCommentViewModel
                    {
                        actAdministrationId = actAdminID, //creted by db
                        actAdministrationComment = com, //blockRange.Text.Remove(0, 2),
                        alignmentType = meta.paragraphAlignment,
                        bulletASCII = meta.bulletASCI,
                        bulletChar = meta.bulletChar,
                        fontBold = meta.fontBold,
                        indentationBy = meta.indentationBy,
                        indentationLeft = meta.IndentationLeft,
                        indentationRight = meta.IndentationRight,
                        listSymbolFont = meta.listSymbolFont,
                        pageBreakBefore = meta.pageBreakBefore,
                        tabHangingIndent = meta.TabHangingIndent,
                        tabStopPosition = meta.tabStopPosition
                       // actAdminCommentId = actAdminCommentID,                       
                    }
                );

            }
            return array;
        }

    }
}
