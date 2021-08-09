using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace GeneralOrder
{

    /// <summary>
    /// This class will handle records from the General Order that have more than one department associated with it. The user will select the department
    /// </summary>
    public class GODepartmentForActVM : BaseViewModel
    {
        public static GOImportFileDM Instance => new GOImportFileDM();

        #region commands
        //Set up the commands
        public ICommand selectActCB { get; set; }
        public ICommand selectDeptCB { get; set; }
        public ICommand insertTextRTBblock { get; set; }
        public ICommand insertTextRTB { get; set; }
        public ICommand deleteTextRTBblock { get; set; }

        public ICommand selectPortfolioCB { get; set; }
        public ICommand getPortfolioWikiInformation { get; set; }
        public ICommand getActWikiInformation { get; set; }
        public ICommand getDepartmentWikiInformation { get; set; }
        public ICommand getCommentsWikiInformation { get; set; }
        public ICommand getBackAndSaveWikiInformation { get; set; }
        public ICommand getCloseWikiInformation { get; set; }
        public ICommand getApplicationWikiInformation { get; set; }
        //Buttons
        public ICommand BackAndSave { get; set; }
        public ICommand Close { get; set; }

        #endregion

        #region properties
        /// <summary>
        /// Rich text box: needed to add the binding in code behind as you cannot bind x:name
        /// </summary>
        private BindableRichTextBox _departmentForActRichTextBox { get; set; }
        public BindableRichTextBox departmentForActRichTextBox
        {
            get { return _departmentForActRichTextBox; }
            set
            {
                _departmentForActRichTextBox = value;
                OnPropertyChanged("departmentForActRichTextBox");
            }
        }

        /// <summary>
        /// Rich Text Box flowDoc
        /// </summary>
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

        /// <summary>
        /// Portfolio List 
        /// </summary>
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

        /// <summary>
        /// Act List
        /// </summary>
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

        /// <summary>
        /// Department List
        /// </summary>
        private ObservableCollection<DepartmentViewModel> _deptList { get; set; }
        public ObservableCollection<DepartmentViewModel> deptList
        {
            get { return _deptList; }
            set
            {
                _deptList = value;
                OnPropertyChanged("deptList");
            }
        }
        private int _deptSelectedIndex { get; set; } = -1;
        public int deptSelectedIndex
        {
            get { return _deptSelectedIndex; }
            set
            {
                _deptSelectedIndex = value;
                OnPropertyChanged("deptSelectedIndex");
            }
        }
        private DepartmentViewModel _cBSelectedDeptValue { get; set; }
        public DepartmentViewModel cBSelectedDeptValue
        {
            get { return _cBSelectedDeptValue; }
            set
            {
                _cBSelectedDeptValue = value;
                OnPropertyChanged("cBSelectedDeptValue");
            }
        }


        private string _resPortfolioResult { get; set; }
        public string resPortfolioResult
        {
            get { return _resPortfolioResult; }
            set
            {
                _resPortfolioResult = value;
                OnPropertyChanged("resPortfolioResult");
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

        /// <summary>
        /// Wiki flowdoc binding
        /// </summary>
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

        private string previousAct { get; set; }
        private string previousPortfolio { get; set; }

        #endregion

        #region UISize
        public int UIFontSize { get; set; } = WindowViewModel.instance.UIFontSize;
        public int UIWidth { get; set; } = WindowViewModel.instance.UIWidth;
        public int UIHeight { get; set; } = WindowViewModel.instance.GODepartmentForActHeight;
        public string generalOrderMargin { get; set; } = WindowViewModel.instance.generalOrderMargin;
        public string generalOrderPadding { get; set; } = WindowViewModel.instance.generalOrderPadding;
        public int departmentForActRichTextBoxeight { get; set; }
        #endregion
        /// <summary>
        /// Binding for the flowdoc List 
        /// </summary>
        public System.Windows.Documents.List list1;

        /// <summary>
        /// Constructor, called only once
        /// </summary>
        public GODepartmentForActVM()
        {
            GODepartmentForActVMStart();
        }

        /// <summary>
        /// Called from the constructor
        /// </summary>
        public void GODepartmentForActVMStart()
        {
            departmentForActRichTextBoxeight = (UIHeight / 6) * 4;
            BindableRichTextBox.UIFontSize = UIFontSize;
            //set up the relays
            wikiFlowDoc = new FlowDocument();
            cBSelectedPortfolioValue = new PortfolioViewModel();
            cBSelectedDeptValue = new DepartmentViewModel();
            cBSelectedActValue = new ActTitleViewModel();

            #region relaycommands
            selectDeptCB = new RelayCommand(SelectDeptContents);
            selectActCB = new RelayCommand(SelectActContents);
            selectPortfolioCB = new RelayCommand(SelectPortfolioContents);
            getPortfolioWikiInformation = new RelayCommand(SelectGetPortfolioWikiInformation);
            getActWikiInformation = new RelayCommand(SelectGetActWikiInformation);
            getDepartmentWikiInformation = new RelayCommand(SelectGetDepartmentWikiInformation);
            getCommentsWikiInformation = new RelayCommand(SelectGetCommentsWikiInformation);
            getBackAndSaveWikiInformation = new RelayCommand(SelectGetBackAndSaveWikiInformation);
            getCloseWikiInformation = new RelayCommand(SelectGetCloseWikiInformation);
            getApplicationWikiInformation = new RelayCommand(SelectGetApplicationWikiInformation);

            //initialize current datas storage
            GeneralOrderPortfolioViewModel currentSelectedPortfolio = new GeneralOrderPortfolioViewModel();
            GeneralOrderActTitleViewModel currentSelectedAct = new GeneralOrderActTitleViewModel();

            deleteTextRTBblock = new RelayCommand(SelectDeleteTextRTBblock);
            insertTextRTBblock = new RelayCommand(SelectInsertTextRTBblock);
            insertTextRTB = new RelayCommand(SelectInsertTextRTB);

            //Buttons
            BackAndSave = new RelayCommand(SelectBackAndSave);
            Close = new RelayCommand(SelectClose);
            #endregion


            //Rich Text Box flow document and sizing
            flowDoc = new FlowDocument();
            flowDoc.PageHeight = Double.NaN;
            flowDoc.PageWidth = Double.NaN;

            //Get the data and display in UI
            PushDataToUI();
            //Get all the WIKI helper information for the current selected window/page ie goDepartmentForAct
            helperResults = DatabaseEntity.ApplicationPage.goDepartmentForAct.GetHelpInfo();

        }

        #region dropdown
        /// <summary>
        /// User selected a Portfolio from the drop down list
        /// </summary>
        private void SelectPortfolioContents()
        {
            if (BindableRichTextBox.UIHasChanged)
            {
                PromptSaveRecToDB();
            }
            if (cBSelectedPortfolioValue != null) //check if its null. This method gets called when initializing ie.  portfolioList = new ObservableCollection<PortfolioViewModel>();
            {
                flowDoc = new FlowDocument();
                actList = new ObservableCollection<ActTitleViewModel>();
                currentSelectedPortfolio = WindowViewModel.instance.resMeta.GOModel.Find(r => r.generalOrderPortfolioID == cBSelectedPortfolioValue.PortfolioId);
                //Based on the selected portfolio, get the corresponding Acts
                foreach (var at in currentSelectedPortfolio.genOrderActTitle)
                {
                    List<DepartmentViewModel> departmentsPortfolioIDsCount = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DepartmentViewModel>>(at.DepartmentPortfolioIDs);

                    //DepartmentViewModel test = departmentsPortfolioIDsCount.FirstOrDefault();
                    if (departmentsPortfolioIDsCount.Count > 1)
                    {
                        //populate Act List
                        actList.Add(new ActTitleViewModel
                        {
                            ActTitle = at.generalOrderActTitle, //$"{0}{1}", at.generalOrderActTitle, at.isExcept ? " - Except:" : string.Empty),
                            ActTitleILDNumber = at.generalOrderActTitleModelID
                        });
                    }
                }
                actSelectedIndex = 0;
                cBSelectedActValue.ActTitle = actList[actSelectedIndex].ActTitle;
                cBSelectedActValue.ActTitleILDNumber = actList[actSelectedIndex].ActTitleILDNumber;
                cBSelectedActValue.ActNumber = actList[actSelectedIndex].ActNumber;
                previousPortfolio = cBSelectedPortfolioValue.PortfolioName;
            }
        }

        /// <summary>
        /// User selected Department from the drop down list
        /// </summary>
        private void SelectDeptContents()
        {
            if (deptSelectedIndex > 0)
            {
                BindableRichTextBox.UIHasChanged = true;
                resPortfolioResult = Newtonsoft.Json.JsonConvert.SerializeObject(new List<DepartmentViewModel>
            {
                new DepartmentViewModel
                {
                    DepartmentId = cBSelectedDeptValue.DepartmentId,
                    Department = cBSelectedDeptValue.Department,
                    DepartmentPortfolioID = cBSelectedDeptValue.DepartmentPortfolioID
                }
            });

                currentSelectedAct = currentSelectedPortfolio.genOrderActTitle.Find(e => e.generalOrderActTitleModelID == cBSelectedActValue.ActTitleILDNumber);

                currentSelectedAct.DepartmentPortfolioIDs = resPortfolioResult;
                previousPortfolio = cBSelectedPortfolioValue.PortfolioName;
            }
            else
            {
                if (BindableRichTextBox.UIHasChanged)
                {
                    BindableRichTextBox.UIHasChanged = false;
                    MessageBoxHelper.CatchExceptionMessageBox("Please select a Department");
                }
            }
        }

        /// <summary>
        /// User selected and Act from the drop down list
        /// </summary>
        private void SelectActContents()
        {
            if (BindableRichTextBox.UIHasChanged)
            {
                PromptSaveRecToDB();
            }
            if (actList.Count < 1)
                return;

            flowDoc = new FlowDocument();
            System.Windows.Documents.Paragraph par;
            System.Windows.Style style = new System.Windows.Style(typeof(System.Windows.Documents.Paragraph));
            currentSelectedAct = currentSelectedPortfolio.genOrderActTitle.Find(e => e.generalOrderActTitleModelID == cBSelectedActValue.ActTitleILDNumber);
            List<DepartmentViewModel> departmentsPortfolioIDsCount = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DepartmentViewModel>>(currentSelectedAct.DepartmentPortfolioIDs);
            if (departmentsPortfolioIDsCount.Count == 1)
            {
                deptSelectedIndex = deptList.Where(r => r.DepartmentPortfolioID == departmentsPortfolioIDsCount[0].DepartmentPortfolioID).Count() + 1;
            }
            else
                deptSelectedIndex = 0;

            //The RichTextBox will be populated with teh Act comments
            foreach (var comm in currentSelectedAct.genOrderActComment)
            {
                par = new System.Windows.Documents.Paragraph();
                par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, comm.generalOrderActComment, UIFontSize);

                RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, comm.generalOrderActCommentParagraphMeta.bulletASCI, comm.generalOrderActCommentParagraphMeta.IndentationLeft, UIFontSize);
            }
            previousAct = cBSelectedActValue.ActTitle;
          
            BindableRichTextBox.UIHasChanged = false;       
        }
        #endregion
        /// <summary>
        /// Close the screen but prompot user to save the current record if there were changes made
        /// </summary>
        private void SelectClose()
        {
            if (BindableRichTextBox.UIHasChanged)
            {
                PromptSaveRecToDB();
            }
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Closeed");
          
            WindowViewModel.instance.CurrentPage = ApplicationPage.goFileDetailsUpload;
        }

        /// <summary>
        /// User clicked the Back and Save button
        /// </summary>
        private void SelectBackAndSave()
        {
            if (BindableRichTextBox.UIHasChanged)
            {
                PromptSaveRecToDB();
            }
            WindowViewModel.instance.CurrentPage = ApplicationPage.goFileDetailsUpload;
        }


        /// <summary>
        /// Prompts the user to save the current record before displaying the newly selected record
        /// </summary>
        private void PromptSaveRecToDB()
        {
            if (deptSelectedIndex > 0)
            {
                if (MessageBoxHelper.DisplayMessageBox("Warning", "There has been changes made.\n Do you want to save changes?"))
                {
                    //Write to the application logger window
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Changes saved to Act {previousAct} and Porfolio {previousPortfolio}");

                    RunCommandSync(() => ImportFileIsRunning, () =>
                    {
                        spinnerBool = ImportFileIsRunning;
                         //Write to the application logger window
                         SelectSaveToMemoryArray();
                        BackAndSaveToDB();
                        BindableRichTextBox.UIHasChanged = false;

                    });
                    spinnerBool = ImportFileIsRunning;
                }
                BindableRichTextBox.UIHasChanged = false;
            }
            else
            {
                BindableRichTextBox.UIHasChanged = false;
                MessageBoxHelper.CatchExceptionMessageBox("Please select a Department");
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

        }

        /// <summary>
        /// This grabs the currentSelectedPortfolio record that is currently displayed in the UI and saves it back to the Database. 
        /// It does not use WindowViewModel.instance.resMeta.GOModel as it will then save all the records 'Portfolios' that is in the dropdown 
        /// </summary>
        private void BackAndSaveToDB()
        {
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Saving Acts: { WindowViewModel.instance.resMeta.orderFileName}, File Type: {(WindowViewModel.instance.resMeta.orderfull ? "Full" : "Partial")}, Effective Date: {WindowViewModel.instance.resMeta.orderEffective}");

            ////Insert the selected Act record into the db 

            var actTitleID = 0;
            actTitleID = GeneralOrderActTitleModelDC.GenOrderActTitleImport(currentSelectedAct, currentSelectedAct.generalOrderPortfolioModelID, currentSelectedPortfolio.generalOrderPortfolioID, currentSelectedAct.EffectiveDate != null ? (DateTime)currentSelectedAct.EffectiveDate : WindowViewModel.instance.resMeta.orderEffective);
            int actCommentModelID = 0;
            if (currentSelectedAct.genOrderActComment.Count > 0)
                actCommentModelID = GOActCommentModelDC.GOCommentImportDel(currentSelectedAct.genOrderActComment, actTitleID);

           
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- Finished saving Acts: {WindowViewModel.instance.resMeta.orderFileName}, File Type: {(WindowViewModel.instance.resMeta.orderfull ? "Full" : "Partial")}, Effective Date: {WindowViewModel.instance.resMeta.orderEffective}");
        }


        /// <summary>
        /// Populates the UI with data from the global WindowViewModel.instance.resMeta.GOModel
        /// </summary>deptList
        private void PushDataToUI()
        {
            #region Insert data into comboboxes
            portfolioList = new ObservableCollection<PortfolioViewModel>();
            actList = new ObservableCollection<ActTitleViewModel>();
            deptList = new ObservableCollection<DepartmentViewModel>();
            bool addPortfolioToList = false;
            deptList.Add(new DepartmentViewModel
            {
                Department = "-- Select Department --",
                DepartmentId = 0,
                DepartmentPortfolioID = 0
            });

            foreach (var p in WindowViewModel.instance.resMeta.GOModel)
            {
                foreach (var at in p.genOrderActTitle)
                {
                    //parse the JSON from DepartmentsPortfolioIDs
                    List<DepartmentViewModel> departmentsPortfolioIDs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DepartmentViewModel>>(at.DepartmentPortfolioIDs);
                    if (departmentsPortfolioIDs.Count > 1)
                    {
                        foreach (var d in departmentsPortfolioIDs)
                        {
                            var dept = deptList.FirstOrDefault(r => r.DepartmentId == d.DepartmentId);
                            if (dept == null)
                                deptList.Add(new DepartmentViewModel { Department = d.Department, DepartmentId = d.DepartmentId, DepartmentPortfolioID = d.DepartmentPortfolioID });


                        }
                        if (departmentsPortfolioIDs.Count > 1) //theres more than one department
                        {
                            addPortfolioToList = true;
                            actList.Add(new ActTitleViewModel
                            {
                                ActTitle = at.generalOrderActTitle, 
                                ActTitleILDNumber = at.generalOrderActTitleModelID
                            });

                        }
                    }
                }


                if (addPortfolioToList)
                {
                    //Add only the portfolios that have department conflicts
                    portfolioList.Add(new PortfolioViewModel { PortfolioName = p.generalOrderPortfolioName, PortfolioId = p.generalOrderPortfolioID });
                    addPortfolioToList = false;
                }
            }
           
            var res = portfolioList[0];
            currentSelectedPortfolio = WindowViewModel.instance.resMeta.GOModel.Find(w => w.generalOrderPortfolioID == res.PortfolioId);

            cBSelectedPortfolioValue.PortfolioId = currentSelectedPortfolio.generalOrderPortfolioID;
            cBSelectedActValue.ActTitleILDNumber = actList[0].ActTitleILDNumber;
            cBSelectedDeptValue.DepartmentId = deptList[0].DepartmentId;
            resPortfolioResult = Newtonsoft.Json.JsonConvert.SerializeObject(new List<DepartmentViewModel>
                {
                    new DepartmentViewModel
                    {
                        DepartmentId = deptList[0].DepartmentId,
                        Department = deptList[0].Department,
                        DepartmentPortfolioID = deptList[0].DepartmentPortfolioID
                    }
                });


            portfolioSelectedIndex = 0;
            actSelectedIndex = 0;
            deptSelectedIndex = 0;
            SelectPortfolioContents();
            SelectActContents();
            #endregion
        }



        /// <summary>
        /// Not used. The delete action is now done in BindableRichTextBox OnPreviewKeyDown
        /// </summary>
        private void SelectDeleteTextRTBblock()
        {
            //  departmentForActRichTextBox.DeleteTextRTBblock();
        }

        /// <summary>
        /// This is when a user presses the return key to create a new line in the RichTextBox
        /// </summary>
        private void SelectInsertTextRTBblock()
        {
            list1 = new System.Windows.Documents.List();
            var par = new System.Windows.Documents.Paragraph();
            par.Margin = new Thickness(0, 0, 0, 0);
            par.Padding = new Thickness(0, 0, 0, 0);

            par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
            par.TextAlignment = TextAlignment.Left;
            par.FontFamily = new System.Windows.Media.FontFamily("Courier");
            par.FontSize = UIFontSize; //10;

            list1.Margin = new Thickness(99, 0, 0, 0);
            list1.MarkerStyle = TextMarkerStyle.None;
            list1.ListItems.Add(new ListItem(par));


            TextPointer caretPos = departmentForActRichTextBox.CaretPosition;
            Block flowBlock = departmentForActRichTextBox.Document.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();
            if (flowBlock != null)
                flowDoc.Blocks.InsertAfter(flowBlock, list1);
            else
                flowDoc.Blocks.Add(list1);

            departmentForActRichTextBox.CaretPosition = list1.ContentStart;

            SelectInsertTextRTB();
        }

        /// <summary>
        /// When a user tabs inside the RichTextBox
        /// </summary>
        private void SelectInsertTextRTB()
        {
            if (departmentForActRichTextBox.Document.Blocks.Count() == 0)
            {
                list1 = new System.Windows.Documents.List();
                var par = new System.Windows.Documents.Paragraph();
                par.Margin = new Thickness(0, 0, 0, 0);
                par.Padding = new Thickness(0, 0, 0, 0);

                par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                par.TextAlignment = TextAlignment.Left;
                par.FontFamily = new System.Windows.Media.FontFamily("Courier");
                par.FontSize = UIFontSize; //10;
                list1.Margin = new Thickness(99, 0, 0, 0);
                list1.MarkerStyle = TextMarkerStyle.None;
                list1.ListItems.Add(new ListItem(par));
                flowDoc.Blocks.Add(list1);
            }
            departmentForActRichTextBox.InsertTextRTB(UIFontSize);
        }


        #region TEST
        /// TEST ONLY 
        /// <summary>
        /// This gets all the data and displays it as the original document in a RichTextBox for 
        /// </summary>
        private void displayData()
        {
            WindowViewModel.instance.resMeta = GetGeneralOrder.GetGeneralOrderDoc(false);
            portfolioList = new ObservableCollection<PortfolioViewModel>();
            actList = new ObservableCollection<ActTitleViewModel>();
            //NOTE THIS IS FOR DISPLAYING THE DOC IN THE UI
            ////parse the flow document
            Run run = null;
            Bold bold = null;


            //This handles the WPF richtextbox displaying the parsed document  ** PORTFOLIO
            System.Windows.Documents.Paragraph par;  // = new System.Windows.Documents.Paragraph();
            System.Windows.Style style = new System.Windows.Style(typeof(System.Windows.Documents.Paragraph));
            foreach (var d in WindowViewModel.instance.resMeta.GOModel)
            {
                par = new System.Windows.Documents.Paragraph();
                portfolioList.Add(new PortfolioViewModel { PortfolioName = d.generalOrderPortfolioName, PortfolioId = d.generalOrderPortfolioID });


                par.Margin = new Thickness(0, 0, 0, 10);
                par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                par.TextAlignment = TextAlignment.Left;
                par.TextIndent = d.generalOrderPortfolioParagraphMeta.indentationBy;
                par.FontFamily = new System.Windows.Media.FontFamily("Courier");
                par.FontSize = UIFontSize; // 10;
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
                    par.FontFamily = new System.Windows.Media.FontFamily("Courier");
                    par.TextAlignment = TextAlignment.Left;
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
            }
        }
        #endregion

        #region wiki
        private void SelectGetApplicationWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ScreenHelpText);
        }

        private void SelectGetCloseWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "CloseBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetBackAndSaveWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "BackAndSaveBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetCommentsWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "CommentsComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetDepartmentWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "DepartmentComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetActWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "ActComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetPortfolioWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "PortfolioComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }
        #endregion
    }
}
