using ApplicationLogger;
using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace GeneralOrder
{

    //UI where the user can upload the final GO 
    public class GOFileDetailsUploadVM : BaseViewModel
    {
        #region properties
        public static GOImportFileDM Instance => new GOImportFileDM();
        #region commands
        public ICommand cbChecked { get; set; }
        public ICommand getApplicationWikiInformation { get; set; }
        public ICommand getGOFileWikiInformation { get; set; }
        public ICommand getPortfolioWikiInformation { get; set; }
        public ICommand getActWikiInformation { get; set; }
        public ICommand getCommentsWikiInformation { get; set; }
        public ICommand getEffectiveDateWikiInformation { get; set; }
        public ICommand getRadioYesWikiInformation { get; set; }
        public ICommand getRadioNoWikiInformation { get; set; }
        public ICommand getBackBtnWikiInformation { get; set; }
        public ICommand getUploadDetailsBtnWikiInformation { get; set; }
        public ICommand getNewGOBtnWikiInformation { get; set; }
        public ICommand getClosekBtnWikiInformation { get; set; }
        public ICommand selectPortfolioCB { get; set; }
        public ICommand selectGeneralOrderCB { get; set; }
        public ICommand selectActCB { get; set; }
        public ICommand insertTextRTB { get; set; }
        public ICommand insertTextRTBblock { get; set; }
        // public ICommand isDirty { get; set; }
        public ICommand deleteTextRTBblock { get; set; }
        public ICommand pasteTextRTBblock { get; set; }
        //Buttons
        public ICommand BackAndSave { get; set; }
        public ICommand UploadDetails { get; set; }
        public ICommand GenerateNewGO { get; set; }
        public ICommand Close { get; set; }

        #endregion

        public System.Windows.Documents.List list1;
      
        private DateTime _effectiveDatePicker { get; set; }
        public DateTime effectiveDatePicker
        {
            get { return _effectiveDatePicker; }
            set
            {
                _effectiveDatePicker = value;
                OnPropertyChanged("effectiveDatePicker");
            }
        }

        private BindableRichTextBox _detailsUploadRichTextBox { get; set; } //needed to add the binding in code behind as you cannot bind x:name
        public BindableRichTextBox detailsUploadRichTextBox

        {
            get { return _detailsUploadRichTextBox; }
            set
            {
                _detailsUploadRichTextBox = value;
                OnPropertyChanged("detailsUploadRichTextBox");
            }
        }
        private FlowDocument _flowDoc { get; set; }
        public FlowDocument flowDoc
        {
            get { return _flowDoc; }
            set
            {
                _flowDoc = value;
                OnPropertyChanged("flowDoc");
            }
        }
        private FlowDocument _wikiFlowDoc { get; set; }
        public FlowDocument wikiFlowDoc
        {
            get { return _wikiFlowDoc; }
            set
            {
                _wikiFlowDoc = value;
                OnPropertyChanged("wikiFlowDoc");
            }
        }


        private int _portfolioSelectedIndex { get; set; } = -1;
        public int portfolioSelectedIndex
        {
            get { return _portfolioSelectedIndex; }
            set
            {
                _portfolioSelectedIndex = value;
                OnPropertyChanged("portfolioSelectedIndex");
            }
        }

        private bool _CheckBoxIsExceptEnabled { get; set; } = true;
        public bool CheckBoxIsExceptEnabled
        {
            get { return _CheckBoxIsExceptEnabled; }
            set
            {
                _CheckBoxIsExceptEnabled = value;
                OnPropertyChanged("CheckBoxIsExceptEnabled");
            }
        }

        private bool _CheckBoxIsExcept { get; set; } = false;
        public bool CheckBoxIsExcept
        {
            get { return _CheckBoxIsExcept; }
            set
            {
                _CheckBoxIsExcept = value;
                OnPropertyChanged("CheckBoxIsExcept");
            }
        }

        private int _generalOrderSelectedIndex { get; set; } = -1;
        public int generalOrderSelectedIndex
        {
            get { return _generalOrderSelectedIndex; }
            set
            {
                _generalOrderSelectedIndex = value;
                OnPropertyChanged("generalOrderSelectedIndex");
            }
        }
        private int _actSelectedIndex { get; set; } = -1;
        public int actSelectedIndex
        {
            get { return _actSelectedIndex; }
            set
            {
                _actSelectedIndex = value;
                OnPropertyChanged("actSelectedIndex");
            }
        }
        private bool _multipleDepartmentPerAct { get; set; } = false;
        public bool multipleDepartmentPerAct
        {
            get { return _multipleDepartmentPerAct; }
            set
            {
                _multipleDepartmentPerAct = value;
                OnPropertyChanged("multipleDepartmentPerAct");
            }
        }
        private string _selectedDate { get; set; }
        public string selectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("selectedDate");
            }
        }

        private string _generalOrderName { get; set; }
        public string generalOrderName
        {
            get { return _generalOrderName; }
            set
            {
                _generalOrderName = value;
                OnPropertyChanged("generalOrderName");
            }
        }
        
        private GeneralOrderViewModel _cBSelectedGeneralOrderValue { get; set; }
        public GeneralOrderViewModel cBSelectedGeneralOrderValue
        {
            get { return _cBSelectedGeneralOrderValue; }
            set
            {
                _cBSelectedGeneralOrderValue = value;
                OnPropertyChanged("cBSelectedGeneralOrderValue");
            }
        }


        private ObservableCollection<PortfolioViewModel> _portfolioList { get; set; }
        public ObservableCollection<PortfolioViewModel> portfolioList
        {
            get { return _portfolioList; }
            set
            {
                _portfolioList = value;
                OnPropertyChanged("portfolioList");
            }
        }
        private PortfolioViewModel _cBSelectedPortfolioValue { get; set; }
        public PortfolioViewModel cBSelectedPortfolioValue
        {
            get { return _cBSelectedPortfolioValue; }
            set
            {
                _cBSelectedPortfolioValue = value;
                OnPropertyChanged("cBSelectedPortfolioValue");
            }
        }

        /// <summary>
        ///Holds the current instance of a record from WindowViewModel.instance.resMeta.GOModel. See PushDataToUI
        /// </summary>
        private GeneralOrderPortfolioViewModel _currentSelectedPortfolio { get; set; }
        public GeneralOrderPortfolioViewModel currentSelectedPortfolio
        {
            get { return _currentSelectedPortfolio; }
            set
            {
                _currentSelectedPortfolio = value;
                OnPropertyChanged("currentSelectedPortfolio");
            }
        }
        private GeneralOrderActTitleViewModel _currentSelectedAct { get; set; }
        public GeneralOrderActTitleViewModel currentSelectedAct
        {
            get { return _currentSelectedAct; }
            set
            {
                _currentSelectedAct = value;
                OnPropertyChanged("currentSelectedAct");
            }
        }


        private ObservableCollection<ActTitleViewModel> _actList { get; set; }
        public ObservableCollection<ActTitleViewModel> actList
        {
            get { return _actList; }
            set
            {
                _actList = value;
                OnPropertyChanged("actList");
            }
        }

        private ActTitleViewModel _cBSelectedActValue { get; set; }
        public ActTitleViewModel cBSelectedActValue
        {
            get { return _cBSelectedActValue; }
            set
            {
                _cBSelectedActValue = value;
                OnPropertyChanged("cBSelectedActValue");
            }
        }


        /// <summary>
        /// <summary>
        /// A flag indicating if the SelectFileMethod command is running
        /// </summary>
        private bool _CloseIsRunning { get; set; }
        public bool CloseIsRunning
        {
            get { return _CloseIsRunning; }
            set
            {
                _CloseIsRunning = value;
                OnPropertyChanged("CloseIsRunning");
            }
        }

        private ScreenHelpViewModel _helperResults { get; set; }
        public ScreenHelpViewModel helperResults
        {
            get { return _helperResults; }
            set
            {
                _helperResults = value;
                OnPropertyChanged("helperResults");
            }
        }


        private bool _spinnerBool { get; set; } = false;
        public bool spinnerBool
        {
            get { return _spinnerBool; }
            set
            {
                _spinnerBool = value;
                OnPropertyChanged("spinnerBool");
            }
        }


        /// A flag indicating if the SelectFileMethod command is running
        /// </summary>
        private bool _ImportFileIsRunning { get; set; }
        public bool ImportFileIsRunning
        {
            get { return _ImportFileIsRunning; }
            set
            {
                _ImportFileIsRunning = value;
                OnPropertyChanged("ImportFileIsRunning");
            }
        }

        private string previousAct { get; set; }
        private string previousPortfolio { get; set; }
        #endregion


        #region UISize
        public int UIFontSize { get; set; } = WindowViewModel.instance.UIFontSize;
        public int UIWidth { get; set; } = WindowViewModel.instance.UIWidth;
        public int UIHeight { get; set; } = WindowViewModel.instance.GOFileDetailsUploadHeight;

        /// <summary>
        /// Sets the UI height and fontsize
        /// </summary>
        public int detailsUploadRichTextBoxHeight { get; set; }

        public string generalOrderMargin { get; set; } = WindowViewModel.instance.generalOrderMargin;
        public string generalOrderPadding { get; set; } = WindowViewModel.instance.generalOrderPadding;
        #endregion

        /// <summary>
        /// constructor
        /// </summary>
        public GOFileDetailsUploadVM()
        {
            GOFileDetailsUploadVMAStart();
        }


        /// <summary>
        /// Set up the relays
        /// </summary>
        public void GOFileDetailsUploadVMAStart()
        {

            BindableRichTextBox.UIFontSize = UIFontSize;

            cBSelectedPortfolioValue = new PortfolioViewModel();
            cBSelectedGeneralOrderValue = new GeneralOrderViewModel();
            detailsUploadRichTextBoxHeight = (UIHeight / 6) * 4;

            #region relaycommands
            cbChecked = new RelayCommand(SelectCBChecked);
            getGOFileWikiInformation = new RelayCommand(SelectGetGOFileWikiInformation);
            getPortfolioWikiInformation = new RelayCommand(SelectGetPortfolioWikiInformation);
            getActWikiInformation = new RelayCommand(SelectGetActWikiInformation);
            getCommentsWikiInformation = new RelayCommand(SelectGetCommentsWikiInformation);
            getEffectiveDateWikiInformation = new RelayCommand(SelectGetEffectiveDateWikiInformation);
            getRadioYesWikiInformation = new RelayCommand(SelectGetRadioYesWikiInformation);
            getRadioNoWikiInformation = new RelayCommand(SelectGetRadioNoWikiInformation);
            getBackBtnWikiInformation = new RelayCommand(SelectGetBackBtnWikiInformation);
            getUploadDetailsBtnWikiInformation = new RelayCommand(SelectGetUploadDetailsBtnWikiInformation);
            getNewGOBtnWikiInformation = new RelayCommand(SelectGetNewGOBtnWikiInformation);
            getClosekBtnWikiInformation = new RelayCommand(SelectGetClosekBtnWikiInformation);
            getApplicationWikiInformation = new RelayCommand(SelectGetApplicationWikiInformation);
            cBSelectedActValue = new ActTitleViewModel();
            selectGeneralOrderCB = new RelayCommand(SelectGeneralOrderContents);
            selectPortfolioCB = new RelayCommand(SelectPortfolioContents);
            selectActCB = new RelayCommand(SelectActContents);
            insertTextRTB = new RelayCommand(selectInsertTextRTB);
            insertTextRTBblock = new RelayCommand(SelectInsertTextRTBblock);
            deleteTextRTBblock = new RelayCommand(SelectDeleteTextRTBblock);
            #endregion

            //initialize current datas storage
            GeneralOrderPortfolioViewModel currentSelectedPortfolio = new GeneralOrderPortfolioViewModel();
            GeneralOrderActTitleViewModel currentSelectedAct = new GeneralOrderActTitleViewModel();

            //Buttons
            BackAndSave = new RelayCommand(SelectBackAndSave);
            //  Save = new RelayCommand(SelectSave);
            UploadDetails = new RelayCommand(SelectUploadDetails);
            GenerateNewGO = new RelayCommand(SelectGenerateNewGO);
            Close = new RelayCommand(SelectClose);
            helperResults = DatabaseEntity.ApplicationPage.goFileDetailsUpload.GetHelpInfo();
            flowDoc = new FlowDocument();

            flowDoc.PageHeight = Double.NaN;
            flowDoc.PageWidth = Double.NaN;

            wikiFlowDoc = new FlowDocument();

            PushDataToUI();
        }

        /// <summary>
        /// This method is used for enabling or disabling Is Except check box
        /// </summary>
        private void SelectCBChecked()
        {
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- Is Except = {CheckBoxIsExcept}");
            currentSelectedAct.isExcept = CheckBoxIsExcept;
            BindableRichTextBox.UIHasChanged = true;
        }

        /// <summary>
        /// Clear out the RichTextBox
        /// </summary>
        private void SelectDeleteTextRTBblock()
        {
            //Get the contents of the flow doc
            TextRange tr = new TextRange(flowDoc.ContentStart, flowDoc.ContentEnd);
            CheckBoxIsExceptEnabled = tr.Text.Replace("\t\r\n", string.Empty).Length > 0 ? true : false;
            if (!CheckBoxIsExceptEnabled)
            {
                CheckBoxIsExcept = false;
                currentSelectedAct.isExcept = CheckBoxIsExcept;
            }
        }

        /// <summary>
        /// This is when a user presses the return key to create a new line in the RichTextBox
        /// </summary>
        private void SelectInsertTextRTBblock()
        {
            list1 = new System.Windows.Documents.List();
            var par = new System.Windows.Documents.Paragraph();
            par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
            par.Padding = new Thickness(0, 0, 0, 0); // new Thickness(0, 0, 0, 0);

            par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
            par.TextAlignment = TextAlignment.Left;
            par.FontFamily = new System.Windows.Media.FontFamily("Courier");
            par.FontSize = UIFontSize; // 10;
            list1.Margin = new Thickness(99, 0, 0, 0);
            list1.MarkerStyle = TextMarkerStyle.None;
            list1.ListItems.Add(new ListItem(par));

            TextPointer caretPos = detailsUploadRichTextBox.CaretPosition;
            Block flowBlock = detailsUploadRichTextBox.Document.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();
            if (flowBlock != null)
                flowDoc.Blocks.InsertAfter(flowBlock, list1);
            else
                flowDoc.Blocks.Add(list1);

            detailsUploadRichTextBox.CaretPosition = list1.ContentStart;
            selectInsertTextRTB();
        }


        /// <summary>
        /// When a user tabs inside the RichTextBox
        /// </summary>
        private void selectInsertTextRTB()
        {
            CheckBoxIsExceptEnabled = true;
            if (detailsUploadRichTextBox.Document.Blocks.Count() == 0)
            {
                list1 = new System.Windows.Documents.List();
                var par = new System.Windows.Documents.Paragraph();
                par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
                par.Padding = new Thickness(0, 0, 0, 0);

                par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                par.TextAlignment = TextAlignment.Left;
                par.FontFamily = new System.Windows.Media.FontFamily("Courier");
                par.FontSize = UIFontSize; // 10;
                list1.Margin = new Thickness(99, 0, 0, 0);
                list1.MarkerStyle = TextMarkerStyle.None;
                list1.ListItems.Add(new ListItem(par));
                flowDoc.Blocks.Add(list1);
            }
            detailsUploadRichTextBox.InsertTextRTB(UIFontSize);

            /////////////////////////////////////////////////////////
            //if (detailsUploadRichTextBox.CaretPosition.Paragraph.Margin.Left == 0)
            //    list1.Margin = new Thickness(caretPos.Paragraph.Margin.Left, 0, 0, 0);


            //System.Windows.Documents.Paragraph par = new System.Windows.Documents.Paragraph();
            //Run run = null;

            //list1 = new System.Windows.Documents.List();

            //par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
            //par.Padding = new Thickness(0, 0, 0, 0);

            //par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
            //par.FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
            //par.TextAlignment = TextAlignment.Left; //comm.generalOrderActCommentParagraphMeta.IndentationLeft < 0 ? TextAlignment.Left : TextAlignment.Right;
            //                                        // par.FontSize = 14;
            //par.FontFamily = new System.Windows.Media.FontFamily("Courier");
            //par.FontSize = 10;
            //FileImportDetailsRTB = new BindableRichTextBox();
            ////  var cursor = FileImportDetailsRTB.CaretPosition;

            ////if (cursor.Paragraph.Margin.Left < 20)
            ////{
            ////    list1.Margin = new Thickness(21.3, 0, 0, 0);
            ////    list1.MarkerStyle = TextMarkerStyle.Disc;
            ////    list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));
            ////    flowDoc.Blocks.Add(list1);
            ////}

            //run = new Run("");
            //par.Inlines.Add(run);


            ////TextPointer caretPos = detailsUploadRichTextBox.CaretPosition;

            ////var curBlock = flowDoc.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).ToList(); //.FirstOrDefault();


            ////TextRange textRange = new TextRange(
            ////        // TextPointer to the start of content in the RichTextBox.
            ////        flowDoc.ContentStart,
            ////        // TextPointer to the end of content in the RichTextBox.
            ////        flowDoc.ContentEnd
            //// );
            ////var test = flowDoc.ContentStart.GetPositionAtOffset(0);

            //if (flowDoc.Blocks.FirstBlock == null)
            //{
            //    list1.Margin = new Thickness(21.3, 0, 0, 0);
            //    list1.MarkerStyle = TextMarkerStyle.Disc;
            //    list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));

            //    flowDoc.Blocks.Add(list1);
            //}
            //else
            //{
            //    // flowDoc.Blocks.Remove(flowDoc.Blocks.FirstBlock);
            //    switch (flowDoc.Blocks.LastBlock.Margin.Left)
            //    {
            //        case (double)0:
            //            list1.Margin = new Thickness(21.3, 0, 0, 0);
            //            list1.MarkerStyle = TextMarkerStyle.Disc;
            //            list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));
            //            //curBlock.Add(list1);
            //            flowDoc.Blocks.Add(list1);
            //            break;
            //        case (double)21.3:
            //            //  list1.ListItems.Remove(list1.ListItems.LastListItem);
            //            list1.Margin = new Thickness(54, 0, 0, 0);
            //            list1.MarkerStyle = TextMarkerStyle.Circle;
            //            list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActCom
            //            flowDoc.Blocks.Add(list1);
            //            //curBlock.Add(list1);
            //            break;
            //        case (double)54:
            //            list1.Margin = new Thickness(89.9, 0, 0, 0);
            //            list1.MarkerStyle = TextMarkerStyle.Box;
            //            list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));
            //            flowDoc.Blocks.Add(list1);
            //            //curBlock.Add(list1);
            //            break;
            //        default:
            //            list1.Margin = new Thickness(21.3, 0, 0, 0);
            //            list1.MarkerStyle = TextMarkerStyle.Disc;
            //            list1.ListItems.Add(new ListItem(par)); //new System.Windows.Documents.Paragraph(new Run(comm.generalOrderActComment))));                                                              
            //            flowDoc.Blocks.Add(list1);
            //            // curBlock.Add(list1);
            //            break;
            //    }
            //}


        }

        /// <summary>
        /// When a user closes the Upload VM window and the UI has changed, it will prompt the user to save changes before exiting
        /// </summary>
        private void SelectClose()
        {
            if (BindableRichTextBox.UIHasChanged)
            {
                PromptSaveRecToDB();
            }

            WindowViewModel.instance.CurrentPage = ApplicationPage.goImportFile;

        }

        /// <summary>
        /// This will create a document based on the data that was uploaded from the Database when selecting 'VIEW FILE'
        /// </summary>
        private async void SelectGenerateNewGO()
        {
            if (BindableRichTextBox.UIHasChanged)
            {
                PromptSaveRecToDB();
            }
            //The filename that was used to create the document will be returned to this string
            string WordDocCreatorFilename = string.Empty;
            //WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- Selected Creating GO document ", DateTime.Now));

            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Selected Creating GO document ");
            //save any changes before updating. This will make sure that the GeneralOrderImportHeader and related Tables are up to date
            spinnerBool = true;

            //Send the in memory object of the document to WordDocCreator to create a new copy of the document
            await Task.Run(() => WordDocCreatorFilename = Reporting.CreateOriginalDoc.WordDocCreator(WindowViewModel.instance.resMeta));
            //Write to the application logger window
            if (WordDocCreatorFilename != string.Empty)
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished creating {(WindowViewModel.instance.resMeta.orderfull ? "Full" : "Partial")} GO document - { WordDocCreatorFilename}");
            else
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancelled creating {(WindowViewModel.instance.resMeta.orderfull ? "Full" : "Partial")} GO document - { WordDocCreatorFilename}");

            spinnerBool = false;
        }

        /// <summary>
        /// This method will upload an already imported file
        /// </summary>
        private async void SelectUploadDetails()
        {
            //Check to make sure that all the portfolios/acts have only one department attached to it.
            if (multipleDepartmentPerAct)
            {
                //Write to the application logger window
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Multiple Departments detected for one or more Acts....");

                //Writing logger information into the Database
                WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                {
                    AdditionalUserInfo = "GOFileDetailsUpload",
                    LogDetail = "Get the Acts with multiple departments.",
                    LogDetail_Additional = "Records with multiple departments needs user interaction. Only one Department per Act ",
                    LogTime = DateTime.Now,
                    UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    Workstation = System.Environment.MachineName
                }, "GeneralOrder", false);
                if (BindableRichTextBox.UIHasChanged)
                {
                    PromptSaveRecToDB();
                }
                WindowViewModel.instance.CurrentPage = ApplicationPage.goDepartmentForAct;
                //Open a UI to handle the multiple Dept per Act
            }
            else
            {
                if (MessageBoxHelper.DisplayMessageBox("Upload", "Are you sure you want to upload this file?"))
                {
                    //Check to make sure that there are no changes in the UI before proceeding. If there is, save the changes
                    if (BindableRichTextBox.UIHasChanged)
                    {
                        PromptSaveRecToDB();
                    }

                    //Convert the model into JSON
                    var resMetaResult = Newtonsoft.Json.JsonConvert.SerializeObject(WindowViewModel.instance.resMeta);

                    bool result = false;
                    spinnerBool = true;
                    //Write to the application logger window
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Uploading {WindowViewModel.instance.resMeta.orderFileName}");

                    await Task.Run(() =>
                    {
                        //Upload the file object
                        result = GOImportFiles.GenOrderUpdateImportFiles(resMetaResult, false);
                        if (result) //If succeeded, then set the Processed Date flag in the database to the current date
                            GeneralOrderImportHeaderDC.GenOrderProcessDate(WindowViewModel.instance.resMeta.ImportHeaderID);
                    });
                    if (result)
                    {
                        //Write to the application logger window
                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished Uploading { WindowViewModel.instance.resMeta.orderFileName}");
                        //Writing logger information into the Database
                        WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                        {
                            AdditionalUserInfo = "GOFileDetailsUpload",
                            LogDetail = "Header ID = " + WindowViewModel.instance.resMeta.ImportHeaderID +
                                WindowViewModel.instance.resMeta.orderFileName + ", Effective Date = " + WindowViewModel.instance.resMeta.orderEffective + " ",
                            LogDetail_Additional = "Uploading Acts for selected GO file.",
                            LogTime = DateTime.Now,
                            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                            Workstation = System.Environment.MachineName
                        }, "GeneralOrder", false);
                    }
                    else
                        //Write to the application logger window
                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Upload Closed.");

                    ////////////
                    //if (promptSaveRecToDB())
                    //{
                    //    //Convert the model into JSON
                    //    var resMetaResult = Newtonsoft.Json.JsonConvert.SerializeObject(WindowViewModel.instance.resMeta);

                    //    bool result = false;
                    //    spinnerBool = true;
                    //    //Write to the application logger window
                    //    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- Uploading {1}", DateTime.Now, WindowViewModel.instance.resMeta.orderFileName));

                    //    await Task.Run(() =>
                    //    {
                    //        result = GOImportFiles.GenOrderUpdateImportFiles(resMetaResult, false);
                    //        if (result)
                    //            GeneralOrderImportHeaderDC.GenOrderProcessDate(WindowViewModel.instance.resMeta.ImportHeaderID);
                    //    });
                    //    if (result)
                    //    {
                    //        //Write to the application logger window
                    //        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- Finished Uploading {1}", DateTime.Now, WindowViewModel.instance.resMeta.orderFileName));
                    //        //Writing logger information into the Database
                    //        WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    //        {
                    //            AdditionalUserInfo = "GOFileDetailsUpload",
                    //            LogDetail = "Header ID = " + WindowViewModel.instance.resMeta.ImportHeaderID +
                    //                // ", Full GO = " + radioFullPartial + ", FileName = " + // Remove the onHold feature 10/01/2018
                    //                WindowViewModel.instance.resMeta.orderFileName + ", Effective Date = " + WindowViewModel.instance.resMeta.orderEffective + " ",
                    //            LogDetail_Additional = "Uploading Acts for selected GO file.",
                    //            LogTime = DateTime.Now,
                    //            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    //            Workstation = System.Environment.MachineName
                    //        }, "GeneralOrder", false);
                    //    }
                    //    else
                    //        //Write to the application logger window
                    //        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- Upload Closeled.", DateTime.Now));
                    //}
                }

                spinnerBool = false;
                WindowViewModel.instance.CurrentPage = ApplicationPage.goImportFile;
            }



        }

        /// <summary>
        /// This saves the comments and its format into the currentSelectedAct which contains the current selected data in the UI.
        /// WindowViewModel.instance.resMeta.GOModel contains all the records that is listed in the DB as current.
        /// currentSelectedPortfolio and currentSelectedAct has a pointer to WindowViewModel.instance.resMeta.GOModel 'selected' record in the UI
        /// </summary>
        private void SelectSaveToMemoryArray()
        {
            var newComments = new List<GeneralOrderActCommentViewModel>();
            GeneralOrderActCommentViewModel newComment;
            var meta = new DocViewModel();
            foreach (var aa in flowDoc.Blocks)
            {
                TextRange blockRange = new TextRange(aa.ContentStart, aa.ContentEnd);
                if (blockRange.Text != string.Empty)
                    if (blockRange.Text != "\t")
                    {
                        var bulletASCIByte = (byte)blockRange.Text[0];
                        meta = null;

                        switch (bulletASCIByte)
                        {
                            case 34: //this is WPF ascii code for the bullet since it only has a small set to choose from
                                meta = ParagraphModelDC.GetStandardizedParagraphMeta(183, "GeneralOrderActComment");
                                break;
                            case 203:
                                meta = ParagraphModelDC.GetStandardizedParagraphMeta(111, "GeneralOrderActComment");
                                break;
                            case 160:
                                meta = ParagraphModelDC.GetStandardizedParagraphMeta(167, "GeneralOrderActComment");
                                break;
                            default:
                                //Since there are no bullets, use the leftindent to see what paragraph info to save
                                switch (aa.Margin.Left)
                                {
                                    //case 1:
                                    //    meta = ParagraphModelDC.GetStandardizedParagraphMeta(996, "GeneralOrderActComment");
                                    //    break;
                                    //case 10:
                                    //    meta = ParagraphModelDC.GetStandardizedParagraphMeta(997, "GeneralOrderActComment");
                                    //    break;
                                    //case 50:
                                    //    meta = ParagraphModelDC.GetStandardizedParagraphMeta(998, "GeneralOrderActComment");
                                    //    break;
                                    //default: //case 78:
                                    //    meta = ParagraphModelDC.GetStandardizedParagraphMeta(999, "GeneralOrderActComment");
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

                        newComment = new GeneralOrderActCommentViewModel();
                        newComment.generalOrderActCommentParagraphMeta = new DocViewModel();
                        newComment.generalOrderActCommentParagraphMeta = meta;
                        if (bulletASCIByte == 34 || bulletASCIByte == 203 || bulletASCIByte == 160)
                            newComment.generalOrderActComment = blockRange.Text.Remove(0, 2);
                        else
                            newComment.generalOrderActComment = blockRange.Text.Trim('\t');
                        newComment.generalOrderActTitleModelID = currentSelectedAct.generalOrderActTitleModelID;
                        newComments.Add(newComment);
                    }
            }
            currentSelectedAct.genOrderActComment = newComments;
            currentSelectedAct.EffectiveDate = effectiveDatePicker.Date;

        }



        /// <summary>
        /// Prompts the user to save the current record before displaying the newly selected record
        /// </summary>
        private void PromptSaveRecToDB()
        {
            if (MessageBoxHelper.DisplayMessageBox("Warning", $"There has been changes made.\n Effective Date: { effectiveDatePicker.Date.ToString("dd/MM/yyyy")}\n Is Except: {currentSelectedAct.isExcept.ToString()}\n Do you want to save changes?"))
            {
                if (DatePickerHelper.isDateValid)
                {
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Changes saved to Act {previousAct} and Porfolio {previousPortfolio}");
                    RunCommandSync(() => ImportFileIsRunning, () =>
                    {
                        spinnerBool = true; 
                        //Write to the application logger window
                        SelectSaveToMemoryArray();
                        BackAndSaveToDB();
                        BindableRichTextBox.UIHasChanged = false;
                    });
                    spinnerBool = false; 
                }
                else
                {
                    MessageBoxHelper.CatchExceptionMessageBox("Effective Date is incorrect");
                    BindableRichTextBox.UIHasChanged = false;
                    //return false;
                }
            }
            else
                BindableRichTextBox.UIHasChanged = false;
            // return true;
        }


        /// <summary>
        /// This grabs the currentSelectedPortfolio record that is currently displayed in the UI and saves it back to the Database. 
        /// It does not use WindowViewModel.instance.resMeta.GOModel as it will then save all the records 'Portfolios' that is in the dropdown 
        /// </summary>
        private void BackAndSaveToDB()
        {
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Saving Acts: {WindowViewModel.instance.resMeta.orderFileName}, File Type: {(WindowViewModel.instance.resMeta.orderfull ? "Full" : "Partial")}, Effective Date: { WindowViewModel.instance.resMeta.orderEffective}");

          
            ////Insert the selected Act record into the db 
            var actTitleID = 0;
            actTitleID = GeneralOrderActTitleModelDC.GenOrderActTitleImport(currentSelectedAct, currentSelectedAct.generalOrderPortfolioModelID, currentSelectedPortfolio.generalOrderPortfolioID, currentSelectedAct.EffectiveDate != null ? (DateTime)currentSelectedAct.EffectiveDate : WindowViewModel.instance.resMeta.orderEffective);
            int actCommentModelID = 0;
            if (currentSelectedAct.genOrderActComment.Count > 0)
                actCommentModelID = GOActCommentModelDC.GOCommentImportDel(currentSelectedAct.genOrderActComment, actTitleID);

            //foreach (var resultActTitle in currentSelectedPortfolio.genOrderActTitle)
            //{
            //    //Insert Act Title into the db and get the ID
            //    var actTitleID = 0;
            //    actTitleID = GeneralOrderActTitleModelDC.GenOrderActTitleImport(resultActTitle, currentSelectedPortfolio.generalOrderPortfolioModelID, currentSelectedPortfolio.generalOrderPortfolioID, resultActTitle.EffectiveDate != null ? (DateTime)resultActTitle.EffectiveDate : WindowViewModel.instance.resMeta.orderEffective);
            //    int actCommentModelID = 0;
            //    if (resultActTitle.genOrderActComment.Count > 0)
            //        actCommentModelID = GOActCommentModelDC.GOCommentImportDel(resultActTitle.genOrderActComment, actTitleID);
            //}
            // });
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished saving Acts: {WindowViewModel.instance.resMeta.orderFileName}, File Type: {(WindowViewModel.instance.resMeta.orderfull ? "Full" : "Partial")}, Effective Date: {WindowViewModel.instance.resMeta.orderEffective}");
        }

        private void SelectBackAndSave()
        {
            if (BindableRichTextBox.UIHasChanged)
            {
                PromptSaveRecToDB();
            }
        }

        /// <summary>
        /// Selecting an Act in the combobox
        /// </summary>
        private void SelectActContents()
        {
            spinnerBool = true;
            if (actList.Count < 1)
                return;
            if (BindableRichTextBox.UIHasChanged)
            {
                PromptSaveRecToDB();
            }

            RunCommandSync(() => ImportFileIsRunning, () =>
           {
               spinnerBool = ImportFileIsRunning;

               flowDoc = new FlowDocument();
               System.Windows.Documents.Paragraph par; 
                System.Windows.Style style = new System.Windows.Style(typeof(System.Windows.Documents.Paragraph));
                currentSelectedAct = currentSelectedPortfolio.genOrderActTitle.Find(e => e.generalOrderActTitleModelID == cBSelectedActValue.ActTitleILDNumber);
               effectiveDatePicker = currentSelectedAct.EffectiveDate != null ? (DateTime)currentSelectedAct.EffectiveDate : DateTime.Now; 
                if (currentSelectedAct.genOrderActComment.FirstOrDefault() != null)
               {
                   foreach (var comm in currentSelectedAct.genOrderActComment)
                   {
                       par = new System.Windows.Documents.Paragraph();
                       par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, comm.generalOrderActComment, UIFontSize);

                       RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, comm.generalOrderActCommentParagraphMeta.bulletASCI, comm.generalOrderActCommentParagraphMeta.IndentationLeft, UIFontSize);
                   }
                   CheckBoxIsExcept = currentSelectedAct.isExcept;
                   CheckBoxIsExceptEnabled = true;
               }
               else
               {  //if no comments, default to a blank
                    par = new System.Windows.Documents.Paragraph();
                   par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, "", UIFontSize);
                   RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, 0, 1, UIFontSize);
                   CheckBoxIsExcept = false;
                   CheckBoxIsExceptEnabled = false;
               }
               spinnerBool = false;
               previousAct = cBSelectedActValue.ActTitle;

           });
            spinnerBool = ImportFileIsRunning;

            //if (promptSaveRecToDB())
            //{
            //    flowDoc = new FlowDocument();
            //    System.Windows.Documents.Paragraph par; // = new System.Windows.Documents.Paragraph();
            //    System.Windows.Style style = new System.Windows.Style(typeof(System.Windows.Documents.Paragraph));
            //    //  currentSelectedPortfolio = WindowViewModel.instance.resMeta.GOModel.Find(r => r.generalOrderPortfolioID == cBSelectedPortfolioValue.PortfolioId);
            //    currentSelectedAct = currentSelectedPortfolio.genOrderActTitle.Find(e => e.generalOrderActTitleModelID == cBSelectedActValue.ActTitleILDNumber);
            //    effectiveDatePicker = currentSelectedAct.EffectiveDate != null ? (DateTime)currentSelectedAct.EffectiveDate : DateTime.Now; // WindowViewModel.instance.resMeta.orderEffective;
            //    if (currentSelectedAct.genOrderActComment.FirstOrDefault() != null)
            //    {
            //        foreach (var comm in currentSelectedAct.genOrderActComment)
            //        {
            //            par = new System.Windows.Documents.Paragraph();
            //            par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, comm.generalOrderActComment, UIFontSize);

            //            RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, comm.generalOrderActCommentParagraphMeta.bulletASCI, comm.generalOrderActCommentParagraphMeta.IndentationLeft, UIFontSize);
            //        }
            //    }
            //    else
            //    {  //if no comments, default to a blank
            //        par = new System.Windows.Documents.Paragraph();
            //        par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, "", UIFontSize);
            //        RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, 0, 1, UIFontSize);
            //    }
            //    spinnerBool = false;
            //    previousAct = cBSelectedActValue.ActTitle;
            //    CheckBoxIsExcept = currentSelectedAct.isExcept;
            //    getIsExcept = currentSelectedAct.isExcept ? " - Except:" : string.Empty;
            //    //var res = actList.Where(r => r.ActTitleILDNumber == currentSelectedAct.ildNumber).FirstOrDefault();
            //    //actSelectedIndex = actList.IndexOf(res); // (r => r.ActTitleILDNumber == WindowViewModel.instance.selectedRecord.ildNumber);
            //}
        }

        /// <summary>
        /// Select a portfolio in the combobox
        /// </summary>
        private void SelectPortfolioContents()
        {
            if (cBSelectedPortfolioValue != null) //check if its null. This method gets called when initializing ie.  portfolioList = new ObservableCollection<PortfolioViewModel>();
            {
                if (BindableRichTextBox.UIHasChanged)
                {
                    PromptSaveRecToDB();
                }
                actList = new ObservableCollection<ActTitleViewModel>();
                currentSelectedPortfolio = WindowViewModel.instance.resMeta.GOModel.Find(r => r.generalOrderPortfolioID == cBSelectedPortfolioValue.PortfolioId);

                foreach (var at in currentSelectedPortfolio.genOrderActTitle)
                {
                    //populate Act List
                    actList.Add(new ActTitleViewModel
                    {
                        ActTitle = at.generalOrderActTitle, 
                        ActTitleILDNumber = at.generalOrderActTitleModelID
                    });
                }

                actSelectedIndex = 0;
                //default to first item in ActList combobox
                cBSelectedActValue.ActTitle = actList[actSelectedIndex].ActTitle;
                cBSelectedActValue.ActTitleILDNumber = actList[actSelectedIndex].ActTitleILDNumber;
                cBSelectedActValue.ActNumber = actList[actSelectedIndex].ActNumber;
                previousPortfolio = cBSelectedPortfolioValue.PortfolioName;
            }
        }

        private void SelectGeneralOrderContents()
        {
            portfolioList = new ObservableCollection<PortfolioViewModel>();
            // WindowViewModel.instance.resMeta = WindowViewModel.instance.resMetaList[generalOrderSelectedIndex];
            foreach (var p in WindowViewModel.instance.resMeta.GOModel)
            {
                //populate Act List
                portfolioList.Add(new PortfolioViewModel { PortfolioId = p.generalOrderPortfolioID, PortfolioName = p.generalOrderPortfolioName });
                CheckDepartmentToAct(p.generalOrderPortfolioID); //Check the department to act relashionship in the first default portfolio
            }
            portfolioSelectedIndex = 0;
        }

        private void CheckDepartmentToAct(int portfolioID)
        {
            multipleDepartmentPerAct = false;
            //When selecting a GO, theck all the document to see if there are issues with any of the portfolios/act. This way, the user does not need to select every portfolio/act to verify
            var d = WindowViewModel.instance.resMeta.GOModel.Find(r => r.generalOrderPortfolioID == portfolioID);
            foreach (var at in d.genOrderActTitle)
            {
                if (at.DepartmentPortfolioIDs != null)
                {
                    //parse the JSON from DepartmentsPortfolioIDs
                    List<DepartmentViewModel> departmentsPortfolioIDs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DepartmentViewModel>>(at.DepartmentPortfolioIDs);
                    if (departmentsPortfolioIDs.Count > 1) //theres more than one department
                    {
                        //Bring up another screen to show the user the relationships
                        multipleDepartmentPerAct = true;
                    }

                }
            }
        }

        private void PushDataToUI()
        {

            spinnerBool = true;
            #region Insert data into comboboxes
            portfolioList = new ObservableCollection<PortfolioViewModel>();
            actList = new ObservableCollection<ActTitleViewModel>();

            generalOrderName = Path.GetFileName(WindowViewModel.instance.resMeta.orderFileName); //Path.GetFileName(WindowViewModel.instance.resMetaList[0].orderFileName);
            generalOrderSelectedIndex = 0;
            cBSelectedGeneralOrderValue.GeneralOrderId = generalOrderSelectedIndex;
            //  WindowViewModel.instance.resMeta = WindowViewModel.instance.resMetaList[generalOrderSelectedIndex];

            foreach (var d in WindowViewModel.instance.resMeta.GOModel)
            {
                portfolioList.Add(new PortfolioViewModel { PortfolioName = d.generalOrderPortfolioName, PortfolioId = d.generalOrderPortfolioID });
                CheckDepartmentToAct(d.generalOrderPortfolioID); //Check the department to act relashionship in the first default portfolio
                                                                 ////This handles the WPF richtextbox displaying the parsed document  ** PORTFOLIO         
            }
            //Get the first Portfolio and get the Acts
            var res = portfolioList[0];
            currentSelectedPortfolio = WindowViewModel.instance.resMeta.GOModel.Find(w => w.generalOrderPortfolioID == res.PortfolioId);

            foreach (var at in currentSelectedPortfolio.genOrderActTitle)
            {
                //populate Act List
                actList.Add(new ActTitleViewModel
                {
                    ActTitle = at.generalOrderActTitle, //$"{0}{1}", at.generalOrderActTitle, at.isExcept ? " - Except:" : string.Empty),
                    ActTitleILDNumber = at.generalOrderActTitleModelID
                });
            }
            cBSelectedPortfolioValue.PortfolioId = currentSelectedPortfolio.generalOrderPortfolioID;
            cBSelectedActValue.ActTitleILDNumber = currentSelectedPortfolio.genOrderActTitle[0].generalOrderActTitleModelID;

            portfolioSelectedIndex = 0;
            actSelectedIndex = 0;

            spinnerBool = false;
            previousPortfolio = res.PortfolioName;
            previousAct = actList[0].ActTitle;
            #endregion

            SelectPortfolioContents();
            SelectActContents();
        }



        #region TEST
        /// TEST ONLY 
        /// <summary>
        /// This gets all the data and displays it as the original document in a RichTextBox
        /// </summary>
        private void displayData()
        {
            WindowViewModel.instance.resMeta = GetGeneralOrder.GetGeneralOrderDoc(false);
            // WindowViewModel.instance.resMeta = WindowViewModel.instance.resMetaList[0];
            portfolioList = new ObservableCollection<PortfolioViewModel>();
            actList = new ObservableCollection<ActTitleViewModel>();
            //NOTE THIS IS FOR DISPLAYING THE DOC IN THE UI
            ////parse the flow document
            Run run = null;
            Bold bold = null;

            //This handles the WPF richtextbox displaying the parsed document  ** PORTFOLIO
            System.Windows.Documents.Paragraph par; // = new System.Windows.Documents.Paragraph();
            System.Windows.Style style = new System.Windows.Style(typeof(System.Windows.Documents.Paragraph));
            foreach (var d in WindowViewModel.instance.resMeta.GOModel)
            {
                par = new System.Windows.Documents.Paragraph();
                portfolioList.Add(new PortfolioViewModel { PortfolioName = d.generalOrderPortfolioName, PortfolioId = d.generalOrderPortfolioID });

                // style the paragraph
                par.Margin = new Thickness(0, 0, 0, 10);
                par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                par.TextAlignment = TextAlignment.Left;
                par.FontFamily = new System.Windows.Media.FontFamily("Courier");
                par.FontSize = UIFontSize; //10;
                par.TextIndent = d.generalOrderPortfolioParagraphMeta.indentationBy;

                if (d.generalOrderPortfolioParagraphMeta.fontBold)
                {
                    bold = new Bold(new Run(d.generalOrderPortfolioName));
                    par.Inlines.Add(bold);
                }
                flowDoc.Blocks.Add(par);

                foreach (var at in d.genOrderActTitle)
                {
                    //populate Act List
                    actList.Add(new ActTitleViewModel { ActTitle = at.generalOrderActTitle, ActTitleILDNumber = at.generalOrderActTitleModelID });

                    //This handles the WPF richtextbox displaying the parsed document  ** ACT TITLE
                    par = new System.Windows.Documents.Paragraph();
                    par.Margin = new Thickness(0, 0, 0, 0);

                    par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                    par.TextAlignment = TextAlignment.Left;
                    par.FontFamily = new System.Windows.Media.FontFamily("Courier");
                    par.FontSize = UIFontSize; //10;
                    par.TextIndent = at.generalOrderActTitleParagraphMeta.indentationBy;

                    if (at.generalOrderActTitleParagraphMeta.fontBold)
                    {
                        bold = new Bold(new Run(at.generalOrderActTitle));
                        par.Inlines.Add(bold);
                    }

                    run = new Run(at.generalOrderActTitle);
                    par.Inlines.Add(run);
                    flowDoc.Blocks.Add(par);

                    foreach (var comm in at.genOrderActComment)
                    {
                        par = new System.Windows.Documents.Paragraph();
                        par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, comm.generalOrderActComment, UIFontSize);
                        //NOTE: Due to the limitations of nested lists with a richtextbox, a list has to be created for every list item. This is because I cannot add a LIST with a List
                        //    ie: List.ListItem.Add(List1) -- error programmatically. You can however create a nested list with XAML
                        RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, comm.generalOrderActCommentParagraphMeta.bulletASCI, comm.generalOrderActCommentParagraphMeta.IndentationLeft, UIFontSize);
                    }
                }
                portfolioSelectedIndex = 0;
            }
        }
        #endregion
        #region wiki
        private void SelectGetApplicationWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ScreenHelpText);
        }

        private void SelectGetClosekBtnWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "CloseBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetNewGOBtnWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "NewGOBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetUploadDetailsBtnWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "UploadDetailsBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetBackBtnWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "BackBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetRadioNoWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "RadioNoBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetRadioYesWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "RadioYesBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetEffectiveDateWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "EffectiveDateBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetCommentsWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "CommentsRTB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetActWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "ActComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetPortfolioWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "PortfolioComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetGOFileWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "GOFileComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        #endregion

    }
}
