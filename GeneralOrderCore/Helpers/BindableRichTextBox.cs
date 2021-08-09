using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;


namespace GeneralOrderCore
{
    //public class BindableRichTextBox1 : BaseAttachedProperty<BindableRichTextBox1, FlowDocument>
    //{
    //    public override void OnDocumentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    //    {
    //        SenderProperty = sender;
    //        RichTextBox rtb = (RichTextBox)sender;
    //        rtb.Document = (FlowDocument)e.NewValue;
    //    }

    //    public static DependencyObject SenderProperty;
    //    public new FlowDocument Document
    //    {
    //        get
    //        {
    //            return (FlowDocument)SenderProperty.GetValue(DocumentProperty);
    //            // return (FlowDocument)GetDocument(DocumentProperty);
    //        }

    //        set
    //        {
    //            SenderProperty.SetValue(DocumentProperty, value);
    //            // SetDocument(DocumentProperty, value);
    //        }
    //    }


    //}

   


    /// <summary>
    /// this class controls the RICHTEXTBOX activity.
    /// </summary>
    public class BindableRichTextBox : RichTextBox
    {
        //this executes after the character has been deleted
        //protected override void OnKeyUp(KeyEventArgs e)
        //{
        //    UIHasChanged = true;
        //    base.OnKeyUp(e);
        //    if (e.Key == Key.Back)
        //    {
        //        try
        //        {
        //            e.Handled = false;
        //            // List list1 = null;
        //            TextPointer caretPos = this.CaretPosition;
        //            Block curBlock = this.Document.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();

        //            if (caretPos.IsAtLineStartPosition)
        //            {
        //                UIHasChanged = false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GeneralOrderCore.BindableRichTextBox -- { e.Message} -- Contact IT support.", ex.ToString()));                
        //        }
        //    }
        //}

        /// <summary>
        /// Overrides the OnPreviewKeyDown. We capture the BACK key in order to control the format of the list within the RichTextBox.
        /// </summary>
        public static bool UIHasChanged { get; set; }
        public static int UIFontSize { get; set; }
        //This executes before the charater gets deleted
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            UIHasChanged = true;
            base.OnPreviewKeyDown(e); 
            //If back key is pressed, format the left margin of the paragraph
            if (e.Key == Key.Back)
            {
                try
                {
                    e.Handled = false;
                    List list1 = null;
                    TextPointer caretPos = this.CaretPosition;
                    Block curBlock = this.Document.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();

                    if (caretPos.IsAtLineStartPosition)
                    {
                        e.Handled = true;

                        if ((System.Windows.Documents.List)curBlock != null)
                        {
                            list1 = ((System.Windows.Documents.List)curBlock).ListItems.Select(r => r.List).FirstOrDefault();
                            list1.FontSize = UIFontSize;
                            var val = Convert.ToDouble(list1.Margin.Left.ToString("F2")); //trim remaining decimal places and only leave one ie 21.2
                            switch (val)
                            {
                                case (double)10:
                                    list1.Margin = new Thickness(1, 0, 0, 0);
                                    list1.MarkerStyle = TextMarkerStyle.None;
                                    break;
                                case (double)21.2:
                                    list1.Margin = new Thickness(10, 0, 0, 0);
                                    list1.MarkerStyle = TextMarkerStyle.None;
                                    break;
                                case (double)50:
                                    list1.Margin = new Thickness(21.2, 0, 0, 0);
                                    list1.MarkerStyle = TextMarkerStyle.Disc;
                                    break;
                                case (double)54:
                                    list1.Margin = new Thickness(50, 0, 0, 0);
                                    list1.MarkerStyle = TextMarkerStyle.None;
                                    break;
                                case (double)78:
                                    list1.Margin = new Thickness(54, 0, 0, 0);
                                    list1.MarkerStyle = TextMarkerStyle.Circle;
                                    list1.FontSize = UIFontSize - 4;
                                    break;
                                case (double)89.9:
                                    list1.Margin = new Thickness(78, 0, 0, 0);
                                    list1.MarkerStyle = TextMarkerStyle.None;
                                    break;
                                default:
                                    if (curBlock.PreviousBlock != null) //Check to see if theres a previous list item so we can default the cursor there
                                    {
                                        this.CaretPosition = curBlock.PreviousBlock.ContentEnd;
                                        this.Document.Blocks.Remove(list1); //Remove the item that got deleted from the list
                                    }
                                    else
                                    {
                                        if (curBlock.NextBlock != null) //If the previous list item does not exist (deleting the top most list item), then default to the bottom list item
                                        {
                                            this.CaretPosition = curBlock.NextBlock.ContentEnd;
                                            this.Document.Blocks.Remove(list1);  //Remove the item that got deleted from the list
                                        }
                                        else     //If there is no bottom item, then just reset the current list item to a brand new one
                                        {
                                            list1.Margin = new Thickness(1, 0, 0, 0);
                                            list1.MarkerStyle = TextMarkerStyle.None;
                                            //UIHasChanged = false;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {  //textrange should only have "\t" when there is no comment text.
                        TextRange txtCurrBlock = new TextRange(curBlock.ContentStart, curBlock.ContentEnd);
                        if (txtCurrBlock.Text.Length < 4)
                            UIHasChanged = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- GeneralOrderCore.BindableRichTextBox -- { ex.Message} -- Contact IT support.");
                   
                }
            }

        }

        #region dependencyObject
        /// <summary>
        /// This controls which UI the document property belongs to. If I dont have this, the various windoes/Controls that have a rich textbox will get confused causing an error
        /// </summary>

        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(FlowDocument),
            typeof(BindableRichTextBox), new FrameworkPropertyMetadata
            (null, new PropertyChangedCallback(OnDocumentChanged)));

        public new FlowDocument Document
        {
            get
            {
                return (FlowDocument)this.GetValue(DocumentProperty);
            }

            set
            {
                this.SetValue(DocumentProperty, value);
            }
        }

        public static void OnDocumentChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs args)
        {
            //RichTextBox rtb = (RichTextBox)obj;
            //rtb.Document = (FlowDocument)args.NewValue;
            var rtb = (RichTextBox)obj;
            if (args.NewValue != null)
            {

                var doc = (FlowDocument)args.NewValue;
                if (doc.Tag is RichTextBox)
                {
                    // clear belongs to another rtb.
                    ((RichTextBox)doc.Tag).Document = new FlowDocument();
                }

                doc.Tag = rtb;
                rtb.Document = doc;
            }
        }
        #endregion

    }




    //public class RichTextBoxHelper : DependencyObject
    //{
    //    public static string GetDocumentXaml(DependencyObject obj)
    //    {
    //        return (string)obj.GetValue(DocumentXamlProperty);
    //    }
    //    public static void SetDocumentXaml(DependencyObject obj, string value)
    //    {
    //        obj.SetValue(DocumentXamlProperty, value);
    //    }
    //    public static readonly DependencyProperty DocumentXamlProperty =
    //        DependencyProperty.RegisterAttached(
    //        "DocumentXaml",
    //        typeof(string),
    //        typeof(RichTextBoxHelper),
    //        new FrameworkPropertyMetadata
    //        {
    //            BindsTwoWayByDefault = true,
    //            PropertyChangedCallback = (obj, e) =>
    //            {
    //                var richTextBox = (RichTextBox)obj;

    //                // Parse the XAML to a document (or use XamlReader.Parse())
    //                var xaml = GetDocumentXaml(richTextBox);
    //                var doc = new FlowDocument();
    //                var range = new TextRange(doc.ContentStart, doc.ContentEnd);

    //                range.Load(new MemoryStream(Encoding.UTF8.GetBytes(xaml)), DataFormats.Xaml);

    //                // Set the document
    //                richTextBox.Document = doc;

    //                // When the document changes update the source
    //                range.Changed += (obj2, e2) =>
    //                      {
    //                          if (richTextBox.Document == doc)
    //                          {
    //                              MemoryStream buffer = new MemoryStream();
    //                              range.Save(buffer, DataFormats.Xaml);
    //                              SetDocumentXaml(richTextBox,
    //                          Encoding.UTF8.GetString(buffer.ToArray()));
    //                          }
    //                      };
    //            }
    //        });
    //}
}
