using GeneralOrderCore;
using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace GeneralOrder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// A sample client that calls Database Entity to store the document into the database
    /// </summary>
    public partial class MainWindow : Window
    {    
        public MainWindow()
        {
            // WPF flowdocument richtextbox
            //Checks to see if there is an instance of the application running. If there is, show message and shut down otherwise start application
         
            int cnt = 0;          
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName.Contains("GeneralOrder")) // p.MainModule.FileName.EndsWith("GeneralOrder.exe", System.StringComparison.CurrentCultureIgnoreCase))
                    cnt++;
            }

            if (cnt > 1)
            {
                MessageBoxHelper.CatchExceptionMessageBox("This program is already running");
                Application.Current.Shutdown();
            }
            else
            {
                InitializeComponent();
                string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                System.Reflection.Assembly.LoadFile(exePath + "\\System.Windows.Interactivity.dll");
                System.Reflection.Assembly.LoadFile(exePath + "\\FontAwesome.WPF.dll");
                
                var culture = new CultureInfo("en-AU");
                FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));

                this.DataContext = WindowViewModel.instance = new WindowViewModel(this);
                //Write to the application logger window
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Started..");
            }
        }
        
        #region old way
        ////*******************************************************************
        /// ALL THE REST OF THE CODE HAS BEEN MOVED TO DOCUMENTVIEWMODEL TO CORRESPOND TO MVVM PRACTICES
        //public FlowDocument flowDoc { get; set; }
        //public MainWindow()
        //{
        //    //WPF flowdocument richtextbox
        //    InitializeComponent();


        //    this.DataContext = new GOImportFileVM();

        //    flowDoc = new FlowDocument();

        //    flowDoc.PageHeight = Double.NaN;
        //    flowDoc.PageWidth = Double.NaN;
        //    // Specify minimum page sizes.
        //    flowDoc.MinPageWidth = 680.0;
        //    flowDoc.MinPageHeight = 480.0;
        //    //Specify maximum page sizes.
        //    flowDoc.MaxPageWidth = 1024.0;
        //    flowDoc.MaxPageHeight = 768.0;

        //    DatabaseEntity.GeneralOrderMetadataViewModel data = null;
        //    //  data = flowDoc.loadWordML(@"C:\Users\arty\Documents\Visual Studio 2017\Projects\General Order Projects\Word Docs\testSMALL.docx");


        //    //test

        //    // data = DocReader.WordDocReader(@"C:\Users\arty\Documents\Visual Studio 2017\Projects\General Order Projects\Word Docs\testSMALL1.docx", "", "");


        //    //Convert to JSON for ease of use in sending objects to other DLL
        //    // var resMetaResult = Newtonsoft.Json.JsonConvert.SerializeObject(data);

        //    //Send the JSON object to DLL to import into DB
        //    // var passed = DatabaseEntity.GeneralOrderImportHeaderDC.GenOrderImport(resMetaResult);



        //    //Reads document from the database 
        //    var resMeta = GetGeneralOrder.GetGeneralOrderDoc(false);

        //    //Creates the word document exactly like the original
        //    CreateOriginalDoc.WordDocCreator(resMeta);


        //    //parse the flow document
        //    Run run = null;
        //    Bold bold = null;
        //    foreach (var d in resMeta.GOModel)
        //    {
        //        //This handles the WPF richtextbox displaying the parsed document  ** PORTFOLIO
        //        System.Windows.Documents.Paragraph par = new System.Windows.Documents.Paragraph();
        //        System.Windows.Style style = new System.Windows.Style(typeof(System.Windows.Documents.Paragraph));

        //        // style the paragraph
        //        //  par.LineHeight = 0;
        //        par.Margin = new Thickness(0, 0, 0, 10);
        //        //par.BreakPageBefore = true;
        //        par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
        //        par.FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
        //        par.TextAlignment = TextAlignment.Left; // d.generalOrderPortfolioParagraphMeta.IndentationLeft < 0 ? TextAlignment.Left : TextAlignment.Right;
        //        par.FontSize = 14;
        //        par.TextIndent = d.generalOrderPortfolioParagraphMeta.indentationBy;

        //        if (d.generalOrderPortfolioParagraphMeta.fontBold)
        //        {
        //            bold = new Bold(new Run(d.generalOrderPortfolioName));
        //            par.Inlines.Add(bold);
        //        }
        //        flowDoc.Blocks.Add(par);

        //        //gOrderPortfolioNameId = d.generalOrderPortfolioID;
        //        //gOrderPortfolioName = d.generalOrderPortfolioName;
        //        //gOrderPortfolioNamebullet = "";
        //        //gOrderPortfolioNamebold = Convert.ToBoolean(docs.Paragraphs[i].Range.Bold);
        //        //gOrderPortfolioNamepageBreakBefore = Convert.ToBoolean(docs.Paragraphs[i].Range.ParagraphFormat.PageBreakBefore);
        //        //gOrderPortfolioNametextPosition = docs.Paragraphs[i].TabStops[1].Position;
        //        //gOrderPortfolioNamebulletASCI = 0;
        //        //gOrderPortfolioNameParagraphAlignment = Convert.ToInt32(docs.Paragraphs[i].Range.ParagraphFormat.Alignment);
        //        //gOrderPortfolioNameIndentationLeft = docs.Paragraphs[i].Range.ParagraphFormat.IndentationLeft;
        //        //gOrderPortfolioNameIndentationRight = docs.Paragraphs[i].Range.ParagraphFormat.IndentationRight;
        //        //gOrderPortfolioNameIndentationBy = docs.Paragraphs[i].Range.ParagraphFormat.FirstLineIndent;

        //        foreach (var at in d.genOrderActTitle)
        //        {

        //            //This handles the WPF richtextbox displaying the parsed document  ** ACT TITLE
        //            par = new System.Windows.Documents.Paragraph();
        //            par.Margin = new Thickness(0, 0, 0, 0);
        //            // style the paragraph
        //            //  par.LineHeight = 0;
        //            par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
        //            par.FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
        //            par.TextAlignment = TextAlignment.Left; //at.generalOrderActTitleParagraphMeta.IndentationLeft < 0 ? TextAlignment.Left : TextAlignment.Right;
        //            par.FontSize = 14;

        //            // par.Margin.Left = at.generalOrderActTitleParagraphMeta.IndentationLeft;
        //            //BlockCollection MyBC = flowDoc.Blocks;
        //            //var curCaret = richTextBox1.CaretPosition;
        //            //var curBlock = richTextBox1.Document.Blocks.Where(x => x.ContentStart.CompareTo(curCaret) == -1 && x.ContentEnd.CompareTo(curCaret) == 1).FirstOrDefault();
        //            //curBlock.TextAlignment = TextAlignment.Right;

        //            par.TextIndent = at.generalOrderActTitleParagraphMeta.indentationBy;

        //            if (at.generalOrderActTitleParagraphMeta.fontBold)
        //            {
        //                bold = new Bold(new Run(at.generalOrderActTitle));
        //                par.Inlines.Add(bold);
        //            }

        //            run = new Run(at.generalOrderActTitle);
        //            par.Inlines.Add(run);
        //            flowDoc.Blocks.Add(par);


        //            //This handles the WPF richtextbox displaying the parsed document  ** ACT COMMENTS
        //          //  System.Windows.Documents.Section sec = new System.Windows.Documents.Section();

        //            foreach (var comm in at.genOrderActComment)
        //            {
        //                par = new System.Windows.Documents.Paragraph();
        //                System.Windows.Documents.List list1 = new System.Windows.Documents.List();

        //                //This will handle hanging indent by adding a section to the paragraph.
        //                //sec.Padding = new Thickness(0, 0, 0, 0);
        //                //sec.Margin = new Thickness(comm.generalOrderActCommentParagraphMeta.IndentationLeft, 0, 0, 0);

        //                //****  NOTE: Due to the limitations of Nested Lists, we could use paragraphs with special bullet characters but then another problem occurs
        //                //****     where the paragraph text does not hang from the first charecter but rather from the bullet. (USING SECTIONS). Must use a list
        //                //if (comm.generalOrderActCommentParagraphMeta.bulletASCI != 999)
        //                //{
        //                //    var font = new Run(((char)comm.generalOrderActCommentParagraphMeta.bulletASCI).ToString());
        //                //    font.FontFamily = new System.Windows.Media.FontFamily(comm.generalOrderActCommentParagraphMeta.listSymbolFont);
        //                //    par.Inlines.Add(font);
        //                //    var space = new Run("  ");
        //                //    par.Inlines.Add(space);
        //                //    // sec.Blocks.Add(par);
        //                //}
        //                //else
        //                //{
        //                //    var space = new Run("   ");
        //                //    par.Inlines.Add(space);
        //                //    // sec.Blocks.Add(par);
        //                //}
        //                //*********



        //                par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
        //                par.Padding = new Thickness(0, 0, 0, 0);

        //                // style the paragraph
        //                // par.LineHeight = 0;
        //                par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
        //                par.FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
        //                par.TextAlignment = TextAlignment.Left; //comm.generalOrderActCommentParagraphMeta.IndentationLeft < 0 ? TextAlignment.Left : TextAlignment.Right;
        //                par.FontSize = 14;
        //                //par.TextIndent = comm.generalOrderActCommentParagraphMeta.IndentationLeft;

        //                run = new Run(comm.generalOrderActComment);
        //                par.Inlines.Add(run);
        //                //sec.Blocks.Add(par);
        //               // flowDoc.Blocks.Add(sec);


        //                //NOTE: Due to the limitations of nested lists with a richtextbox, a list has to be created for every list item. This is because I cannot add a LIST with a List
        //                //    ie: List.ListItem.Add(List1) -- error programmatically. You can however create a nested list with XAML
        //                switch (comm.generalOrderActCommentParagraphMeta.bulletASCI)
        //                {
        //                    case 183:
        //                        list1.Margin = new Thickness(comm.generalOrderActCommentParagraphMeta.IndentationLeft, 0, 0, 0);
        //                        list1.MarkerStyle = TextMarkerStyle.Disc;
        //                        list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));
        //                        flowDoc.Blocks.Add(list1);
        //                        break;
        //                    case 111:
        //                        list1.Margin = new Thickness(comm.generalOrderActCommentParagraphMeta.IndentationLeft, 0, 0, 0);
        //                        list1.MarkerStyle = TextMarkerStyle.Circle;
        //                        list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));     
        //                        flowDoc.Blocks.Add(list1);
        //                        break;
        //                    case 167:
        //                        list1.Margin = new Thickness(comm.generalOrderActCommentParagraphMeta.IndentationLeft, 0, 0, 0);
        //                        list1.MarkerStyle = TextMarkerStyle.Box;
        //                        list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));                            
        //                        flowDoc.Blocks.Add(list1);
        //                        break;
        //                    default:
        //                        list1.Margin = new Thickness(comm.generalOrderActCommentParagraphMeta.IndentationLeft, 0, 0, 0);
        //                        list1.MarkerStyle = TextMarkerStyle.None;
        //                        list1.ListItems.Add(new ListItem(par));                                                            
        //                        flowDoc.Blocks.Add(list1);
        //                        break;
        //                }

        //                //if (comm.generalOrderActCommentParagraphMeta.fontBold)
        //                //{
        //                //    bold = new Bold(new Run(comm.generalOrderActComment));
        //                //    par.Inlines.Add(bold);
        //                //}

        //            }

        //        }

        //    }



        //    StringWriter wr_f1 = new StringWriter();
        //    XamlWriter.Save(flowDoc, wr_f1);
        //    richTextBox1.Document = XamlReader.Parse(wr_f1.ToString()) as FlowDocument;


        //    //Once completed parsing the Word Doc, push the data into a word template
        //    //pushDataIntoTemplate(GeneralOrderMetadata data);


        //}

        #endregion
    }
}