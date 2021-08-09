using ApplicationLogger;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;


namespace GeneralOrderCore
{
    public static class RichTextBoxHelper
    {
       
        /// <summary>
        /// When a user presses the TAB button it will get the carrot position of the cursor in the flowdoc
        /// </summary>
        /// <param name="RichTextBox"></param>
        public static void InsertTextRTB(this BindableRichTextBox RichTextBox, int UIFontSize)
        {
            try
            {
                //FlowDocument flowDoc = RichTextBox.Document;
                //TextPointer caretPos = RichTextBox.CaretPosition;
                //Block flowBlock = flowDoc.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();

                //List list1 = new List();
                //System.Windows.Documents.Paragraph par = new System.Windows.Documents.Paragraph();



                //if (flowBlock == null)
                //{

                //    flowBlock = flowDoc.Blocks.LastBlock;
                //}

                ////check to see weather the flowblock is a List . If its not,get the paragraph, add it to the list and then add it to the flowdoc. then remove the original paragraph
                //if (flowBlock is System.Windows.Documents.Paragraph)
                //{
                //    list1.Margin = new Thickness(1, 0, 0, 0);
                //    list1.MarkerStyle = TextMarkerStyle.None;
                //    list1.ListItems.Add(new ListItem((System.Windows.Documents.Paragraph)flowBlock)); //Add the existing paragraph to a List item
                //    if (flowBlock.NextBlock != null)
                //        flowDoc.Blocks.InsertAfter(flowBlock, list1);
                //    else
                //    {
                //  //      flowDoc.Blocks.Remove(flowBlock); //Once list is inserted, remove the original paragraph item
                //        flowDoc.Blocks.Add(list1);//  flowDoc.Blocks.InsertBefore(flowBlock, list1); //Insert the list item before the original paragraph
                //    }

                //    caretPos = list1.ContentStart;
                //    RichTextBox.CaretPosition = caretPos;
                //    flowBlock = flowDoc.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == 0).FirstOrDefault();

                //}

               

                //flow.InsertTextRTBHelper(); //(flowDoc, list1, flowBlock); //send in the flowdoc and get it modified
                //RichTextBox.Document = flow.flowD;
                List list1 = null;
                TextRange textRange = null;
                FlowDocument flowDoc = RichTextBox.Document;
                TextPointer caretPos = RichTextBox.CaretPosition;
                Block flowBlock = RichTextBox.Document.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();
                if ((System.Windows.Documents.List)flowBlock != null)
                {
                    list1 = ((System.Windows.Documents.List)flowBlock).ListItems.Select(r => r.List).FirstOrDefault();
                    textRange = new TextRange(
                            caretPos.Paragraph.ContentStart,
                            caretPos.Paragraph.ContentEnd
                    );

                    if (!textRange.Text.Contains("\t"))   //this means that user has backspaced to the edge of the richtextbox. this will be out of the list scope
                    {
                        list1.Margin = new Thickness(0, 0, 0, 0);
                        list1.MarkerStyle = TextMarkerStyle.None;
                    }
                    
                    if (flowDoc.Blocks.Count() == 1)
                    {
                        textRange = new TextRange(
                             flowDoc.ContentStart,
                             flowDoc.ContentEnd
                        );

                        if (textRange.Text == "\r\n")
                        {
                            flowDoc = new FlowDocument();
                        }
                    }
                    InsertTextRTBHelper(flowDoc, list1, flowBlock, UIFontSize);
                }
                try
                {
                    RichTextBox.CaretPosition = list1.ContentEnd;
                }

                catch { }

            }
            catch (Exception e)
            {
                MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- RichTextBoxHelper.InsertTextRTB -- { e.Message} -- Contact IT support.");
                WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                {
                    AdditionalUserInfo = "Exception",
                    LogDetail = e.Message,
                    LogDetail_Additional = "RichTextBoxHelper.InsertTextRTB",
                    LogTime = DateTime.Now,
                    UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    Workstation = System.Environment.MachineName
                }, "GeneralOrderCore", false);

            }
        }


        /// <summary>
        /// Based of margin left, everytime TAB button is pressed it will determinine what is the next position of the cursor. 
        /// It will also insert a bullet character if required based on the position of the left margin
        /// </summary>
        /// <param name="flowDoc">Flow document passed in by the richtextbox</param>
        /// <param name="list1">Lists within the flowdoc that contain the paragraphs</param>
        /// <param name="flowBlock">FlowBlocks are blocks within the flowdoc that contain lists and paragraphs</param>
        private static void InsertTextRTBHelper(FlowDocument flowDoc, List list1, Block flowBlock, int UIFontSize) 
        {
            if (flowDoc.Blocks.FirstBlock == null) 
            {
                list1 = new System.Windows.Documents.List();               
                list1.Margin = new Thickness(0, 0, 0, 0);
            }
            list1.FontSize = UIFontSize;
            switch (list1.Margin.Left)
            {
                case (double)0:
                    var par = new System.Windows.Documents.Paragraph();
                    par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
                    par.Padding = new Thickness(0, 0, 0, 0);

                    par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                    par.TextAlignment = TextAlignment.Left;
                    par.FontFamily = new System.Windows.Media.FontFamily("Courier");
                    par.FontSize = UIFontSize; // 10;
                    list1.Margin = new Thickness(1, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.None;
                    list1.ListItems.Add(new ListItem(par));
                    if (flowBlock.NextBlock != null)
                        flowDoc.Blocks.InsertAfter(flowBlock, list1);
                    else
                        flowDoc.Blocks.Add(list1);
                    
                    break;
                case (double)1: ////Comment for first bullet
                    list1.Margin = new Thickness(10, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.None;
                    break;
                case (double)10: //First bullet
                    list1.Margin = new Thickness(21.2, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.Disc;
                    break;
                case (double)21.2: //Comment for second level bullet
                    list1.Margin = new Thickness(50, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.None;
                    break;
                case (double)50://Second bullet
                    list1.Margin = new Thickness(54, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.Circle;
                    list1.FontSize = UIFontSize - 4;
                    break;
                case (double)54: //Comment for third bullet
                    list1.Margin = new Thickness(78, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.None;
                    break;
                case (double)78:
                    list1.Margin = new Thickness(89.9, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.Box;
                    break;
                default:
                    list1.Margin = new Thickness(1, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.None;
                    break;
            }
         
        }



        /// <summary>
        /// The richtextbox template style is applied
        /// </summary>
        /// <param name="flowDoc"></param>
        /// <param name="list1"></param>
        /// <param name="rec"></param>
        public static System.Windows.Documents.Paragraph DisplayTextRTBHelper( FlowDocument flowDoc, string rec, int fontSize = 10)
        {
            System.Windows.Documents.Paragraph par = new System.Windows.Documents.Paragraph();

            par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
            par.Padding = new Thickness(0, 0, 0, 0);

            par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
            par.TextAlignment = TextAlignment.Left; 

            par.FontFamily = new System.Windows.Media.FontFamily("Courier");
            par.FontSize = fontSize;
            Run run = new Run(rec);
            par.Inlines.Add(run);

            return par;
           // FlowDocBulletType(flowDoc, list1, par, rec.bulletASCII, rec.indentationLeft);
            
        }


        /// <summary>
        /// Adds a List to the flowdoc that contains a bullet style
        /// </summary>
        /// <param name="flowDoc"></param>
        /// <param name="list1"></param>
        /// <param name="par"></param>
        /// <param name="bulletASCII"></param>
        /// <param name="indentationLeft"></param>
        public static void FlowDocBulletType( FlowDocument flowDoc, List list1, Paragraph par, short bulletASCII, double indentationLeft, int UIFontSiz)
        {
            list1 = new System.Windows.Documents.List();
            list1.FontSize = UIFontSiz;
            //  list1.FontSize = 10;
            switch (bulletASCII)
            {
                case 183:
                    list1.Margin = new Thickness(indentationLeft, 0, 0, 0);                
                    list1.MarkerStyle = TextMarkerStyle.Disc;
                    list1.ListItems.Add(new ListItem(par)); 
                    flowDoc.Blocks.Add(list1);
                    break;
                case 111:
                    list1.Margin = new Thickness(indentationLeft, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.Circle;
                    list1.FontSize = UIFontSiz - 4; //font size of circle is much bigger thn disc and box 
                    list1.ListItems.Add(new ListItem(par));   
                    flowDoc.Blocks.Add(list1);
                    break;
                case 167:
                    list1.Margin = new Thickness(indentationLeft, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.Box;
                    list1.ListItems.Add(new ListItem(par));                          
                    flowDoc.Blocks.Add(list1);
                    break;
                default:
                    list1.Margin = new Thickness(indentationLeft, 0, 0, 0);
                    list1.MarkerStyle = TextMarkerStyle.None;
                    list1.ListItems.Add(new ListItem(par));
                    flowDoc.Blocks.Add(list1);
                    break;
            }
        }

        //public static void DeleteTextRTBblock(this BindableRichTextBox RichTextBox)
        //{
        //    try
        //    {

        //       TextPointer caretPos = RichTextBox.CaretPosition;
        //        Block curBlock = RichTextBox.Document.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();

        //        // var textSpacing = list1.Margin.Left < 20 ? 1 : 2; //check to see if the list has a bullet point. If so, remove the first 2 items ie. bullet and '\t'. If it has no bullet, just remove 1 item ie "\t"

        //        if (caretPos.IsAtLineStartPosition)
        //        {
        //            List list1 = ((System.Windows.Documents.List)curBlock).ListItems.Select(r => r.List).FirstOrDefault();
        //            list1.FontSize = 10;
        //            var val = Convert.ToDouble(list1.Margin.Left.ToString("F2")); //trim remaining decimal places and only leave one ie 21.2
        //            switch (val)
        //            {
        //                case (double)10:
        //                    list1.Margin = new Thickness(1, 0, 0, 0);
        //                    list1.MarkerStyle = TextMarkerStyle.None;
        //                    break;
        //                case (double)21.2:
        //                    list1.Margin = new Thickness(10, 0, 0, 0);
        //                    list1.MarkerStyle = TextMarkerStyle.None;
        //                    break;
        //                case (double)50:
        //                    list1.Margin = new Thickness(21.2, 0, 0, 0);
        //                    list1.MarkerStyle = TextMarkerStyle.Disc;
        //                    break;
        //                case (double)54:
        //                    list1.Margin = new Thickness(50, 0, 0, 0);
        //                    list1.MarkerStyle = TextMarkerStyle.None;
        //                    break;
        //                case (double)78:
        //                    list1.Margin = new Thickness(54, 0, 0, 0);
        //                    list1.MarkerStyle = TextMarkerStyle.Circle;
        //                    list1.FontSize = 6;
        //                    break;
        //                case (double)89.9:
        //                    list1.Margin = new Thickness(78, 0, 0, 0);
        //                    list1.MarkerStyle = TextMarkerStyle.None;
        //                    break;
        //                default:
        //                    if (curBlock.PreviousBlock != null) //Check to see if theres a previous list item so we can default the cursor there
        //                    {
        //                        RichTextBox.CaretPosition = curBlock.PreviousBlock.ContentEnd;
        //                        RichTextBox.Document.Blocks.Remove(list1); //Remove the item that got deleted from the list
        //                    }
        //                    else
        //                    {
        //                        if (curBlock.NextBlock != null) //If the previous list item does not exist (deleting the top most list item), then default to the bottom list item
        //                        {
        //                            RichTextBox.CaretPosition = curBlock.NextBlock.ContentEnd;
        //                            RichTextBox.Document.Blocks.Remove(list1);  //Remove the item that got deleted from the list
        //                        }
        //                        else     //If there is no bottom item, then just reset the current list item to a brand new one
        //                        {
        //                            list1.Margin = new Thickness(1, 0, 0, 0);
        //                            list1.MarkerStyle = TextMarkerStyle.None;
        //                        }
        //                    }
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            //caretPos = caretPos.GetPositionAtOffset(0, LogicalDirection.Forward);
        //            //var test = caretPos.GetTextInRun(LogicalDirection.Backward);

        //            //if (caretPos.GetTextInRun(LogicalDirection.Backward).Count() == 0)
        //            //{
        //            //    //var res = caretPos.GetTextInRun(LogicalDirection.Backward);
        //            //    caretPos.InsertTextInRun(" ");
        //            //}


        //            //the whole rich text box text
        //            //TextRange txt = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);


        //            //This removes a highlighted area of text
        //            TextSelection ts = RichTextBox.Selection;
        //            if (ts.Text.Count() > 0)
        //                ts.Text = string.Empty;
        //            else
        //            {

        //                // (new System.Linq.SystemCore_EnumerableDebugView<System.Windows.Documents.ListItem>(((System.Windows.Documents.List)(new System.Linq.SystemCore_EnumerableDebugView<System.Windows.Documents.Block>(RichTextBox.Document.Blocks).Items[1])).ListItems).Items[0]).TextRange.Text
        //                // ((List)curBlock).ListItems.ElementAt(0).ContentStart.DeleteTextInRun(-1);

        //             //   caretPos = RichTextBox.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
        //              caretPos.DeleteTextInRun(-1);
        //                ////////////////////////////////////////////////////////
        //                // caretPos.DeleteTextInRun(-1);

        //                //caretPos = RichTextBox.CaretPosition;
        //                //TextRange txtCurrBlock = new TextRange(curBlock.ContentStart, curBlock.ContentEnd);
        //                //var pos = curBlock.ContentStart.GetOffsetToPosition(caretPos);
        //                //var bulletASCIByte = (byte)txtCurrBlock.Text[0];
        //                //string sb = string.Empty;
        //                //if (bulletASCIByte == 34 || bulletASCIByte == 203 || bulletASCIByte == 160)
        //                //    sb = txtCurrBlock.Text.Remove(0, 2);
        //                //else
        //                //    sb = txtCurrBlock.Text.Trim('\t'); // Remove(0, 2);
        //                //txtCurrBlock.Text = sb.Remove(pos, 1);


        //                //////////////////////////////////


        //                //var bulletASCIByte = (byte)txtCurrBlock.Text[0];
        //                //string sb = string.Empty;
        //                //if (bulletASCIByte == 34 || bulletASCIByte == 203 || bulletASCIByte == 160)
        //                //    sb = txtCurrBlock.Text.Remove(0, 2);
        //                //else
        //                //    sb = txtCurrBlock.Text.Trim('\t'); // Remove(0, 2);

        //                //var currBlockIndex = ((List)curBlock).StartIndex;
        //                //var par = new System.Windows.Documents.Paragraph();
        //                //par.Margin = new Thickness(0, 0, 0, 0);
        //                //par.Padding = new Thickness(0, 0, 0, 0);
        //                //par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
        //                //par.TextAlignment = TextAlignment.Left;
        //                //par.FontFamily = new System.Windows.Media.FontFamily("Courier");
        //                //par.FontSize = 10;
        //                //Run run = new Run(sb);
        //                //par.Inlines.Add(run);
        //                //var list1 = new System.Windows.Documents.List();
        //                //list1.Margin = curBlock.Margin;// new Thickness(10, 0, 0, 0);
        //                //list1.MarkerStyle = ((List)curBlock).MarkerStyle;
        //                //list1.ListItems.Add(new ListItem(par));

        //                //RichTextBox.Document.Blocks.InsertAfter(curBlock, list1);
        //                //RichTextBox.Document.Blocks.Remove(curBlock);
        //                //RichTextBox.CaretPosition = RichTextBox.CaretPosition.GetPositionAtOffset(pos - 1);


        //                ////////////////////////////////////////////////


        //                //TextRange txtCurrBlock = new TextRange(curBlock.ContentStart, curBlock.ContentEnd);
        //                //var bulletASCIByte = (byte)txtCurrBlock.Text[0];
        //                //string sb = string.Empty;
        //                //if (bulletASCIByte == 34 || bulletASCIByte == 203 || bulletASCIByte == 160)
        //                //    sb = txtCurrBlock.Text.Remove(0, 2);
        //                //else
        //                //    sb = txtCurrBlock.Text.Trim('\t'); // Remove(0, 2);

        //                //caretPos = RichTextBox.CaretPosition;
        //                //var pos = curBlock.ContentStart.GetOffsetToPosition(caretPos);
        //                //txtCurrBlock.Text = sb.Remove(pos - 4, 1);
        //                //RichTextBox.CaretPosition = curBlock.ContentStart.GetPositionAtOffset(pos - 1);

        //                /////////////////////////////////////////////////




        //                //Creating a new block paragraph run
        //                //var list1 = new System.Windows.Documents.List();
        //                //var par = new System.Windows.Documents.Paragraph();
        //                //par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
        //                //par.Padding = new Thickness(0, 0, 0, 0);
        //                //par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
        //                //par.TextAlignment = TextAlignment.Left; //comm.generalOrderActCommentParagraphMeta.IndentationLeft < 0 ? TextAlignment.Left : TextAlignment.Right;
        //                //par.FontFamily = new System.Windows.Media.FontFamily("Courier");
        //                //par.FontSize = 10;
        //                //TextRange txtCurrBlock = new TextRange(curBlock.ContentStart, curBlock.ContentEnd);
        //                //var textfromblock = txtCurrBlock.Text;
        //                //Run run = new Run(textfromblock);
        //                //par.Inlines.Add(run);

        //                //list1.Margin = new Thickness(99, 0, 0, 0);
        //                //list1.MarkerStyle = TextMarkerStyle.None;
        //                //list1.ListItems.Add(new ListItem(par));

        //                //RichTextBox.Document.Blocks.InsertAfter(curBlock, list1);
        //                //RichTextBox.Document.Blocks.Remove(curBlock);
        //                //// Set the TextPointer 6 displacement backward
        //                //caretPos = caretPos.GetPositionAtOffset(50, LogicalDirection.Backward);

        //                //// Specify the new caret position to RichTextBox
        //                //RichTextBox.CaretPosition = caretPos;


        //                //////////////////////////////////////////////////



        //                //Override the run
        //                //var par1 = curBlock.t
        //                //Run run = new Run(textfromblock);
        //                //par1.Inlines.Add(run);

        //                //TextRange txtCurrBlock = new TextRange(curBlock.ContentStart, curBlock.ContentEnd);
        //                //var textfromblock = txtCurrBlock.Text;
        //                //Paragraph curParagraph = curBlock as Paragraph;
        //                //TextPointer carePointer = RichTextBox.CaretPosition;
        //                //Run newRun = new Run(textfromblock);
        //                //curParagraph.Inlines.Add(newRun);
        //                //RichTextBox.CaretPosition = newRun.ElementStart;


        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- RichTextBoxHelper.DeleteTextRTBblock -- { e.Message} -- Contact IT support.");
        //        WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
        //        {
        //            AdditionalUserInfo = "Exception",
        //            LogDetail = e.Message,
        //            LogDetail_Additional = "RichTextBoxHelper.DeleteTextRTBblock",
        //            LogTime = DateTime.Now,
        //            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
        //            Workstation = System.Environment.MachineName
        //        }, "GeneralOrderCore", false);

        //    }
        //}

        //public static void InsertTextRTBDocument(this BindableRichTextBox RichTextBox)
        //{
        //    try
        //    {
        //        TextPointer caretPos = RichTextBox.CaretPosition;
        //        Block curBlock = RichTextBox.Document.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();

        //        caretPos.InsertTextInRun("a");

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- RichTextBoxHelper.DeleteTextRTBblock -- { e.Message} -- Contact IT support.");
        //        WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
        //        {
        //            AdditionalUserInfo = "Exception",
        //            LogDetail = e.Message,
        //            LogDetail_Additional = "RichTextBoxHelper.DeleteTextRTBblock",
        //            LogTime = DateTime.Now,
        //            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
        //            Workstation = System.Environment.MachineName
        //        }, "GeneralOrderCore", false);

        //    }
        //}


        /// <summary>
        /// The richtextbox template style is applied
        /// </summary>
        /// <param name="flowDoc"></param>
        /// <param name="list1"></param>
        /// <param name="rec"></param>
        //public static void DisplayTextRTBHelper(FlowDocument flowDoc, List list1, GeneralFieldsCommentViewModel rec)
        //{
        //    System.Windows.Documents.Paragraph par = new System.Windows.Documents.Paragraph();

        //    list1 = new System.Windows.Documents.List();

        //    par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
        //    par.Padding = new Thickness(0, 0, 0, 0);

        //    par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
        //    //  par.FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
        //    par.TextAlignment = TextAlignment.Left; //comm.generalOrderActCommentParagraphMeta.IndentationLeft < 0 ? TextAlignment.Left : TextAlignment.Right;

        //    par.FontFamily = new System.Windows.Media.FontFamily("Courier");
        //    par.FontSize = 10;
        //    Run run = new Run(rec.actAdministrationComment);
        //    par.Inlines.Add(run);

        //    switch (rec.bulletASCII)
        //    {
        //        case 183:
        //            list1.Margin = new Thickness(rec.indentationLeft, 0, 0, 0);
        //            list1.MarkerStyle = TextMarkerStyle.Disc;
        //            list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));
        //            flowDoc.Blocks.Add(list1);
        //            break;
        //        case 111:
        //            list1.Margin = new Thickness(rec.indentationLeft, 0, 0, 0);
        //            list1.MarkerStyle = TextMarkerStyle.Circle;
        //            list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));     
        //            flowDoc.Blocks.Add(list1);
        //            break;
        //        case 167:
        //            list1.Margin = new Thickness(rec.indentationLeft, 0, 0, 0);
        //            list1.MarkerStyle = TextMarkerStyle.Box;
        //            list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));                            
        //            flowDoc.Blocks.Add(list1);
        //            break;
        //        default:
        //            list1.Margin = new Thickness(rec.indentationLeft, 0, 0, 0);
        //            list1.MarkerStyle = TextMarkerStyle.None;
        //            list1.ListItems.Add(new ListItem(par));
        //            flowDoc.Blocks.Add(list1);
        //            break;
        //    }
        //}
    }
}
