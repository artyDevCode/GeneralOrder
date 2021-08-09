using ApplicationLogger;
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

namespace GeneralOrderUpdateDetails
{

    /// <summary>
    /// Add a new record to the GO
    /// </summary>
    public class GOActAdminAddVM : BaseViewModel
    {
        #region properties
        private string _windowTitle { get; set; } = "Act Administration - Add Pending Record";
        public string windowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged("windowTitle");
            }
        }
        private List<PortfolioViewModel> _portfolioList { get; set; }
        public List<PortfolioViewModel> portfolioList
        {
            get { return _portfolioList; }
            set
            {
                _portfolioList = value;
                OnPropertyChanged("portfolioList");
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
        private List<DepartmentViewModel> _deptList { get; set; }
        public List<DepartmentViewModel> deptList
        {
            get { return _deptList; }
            set
            {
                _deptList = value;
                OnPropertyChanged("deptList");
            }
        }

        private DateTime _effectiveDatePicker { get; set; } = DateTime.Now;
        public DateTime effectiveDatePicker
        {
            get { return _effectiveDatePicker; }
            set
            {
                if (value != _effectiveDatePicker)
                {
                    _effectiveDatePicker = value;
                    OnPropertyChanged("effectiveDatePicker");
                    SelectGetSelectedDate();
                }
            }
        }
        private BindableRichTextBox _ActAdminRichTextBox { get; set; } //needed to add the binding in code behind as you cannot bind x:name
        public BindableRichTextBox ActAdminRichTextBox
        {
            get { return _ActAdminRichTextBox; }
            set
            {
                _ActAdminRichTextBox = value;
                OnPropertyChanged("ActAdminRichTextBox");
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
        private int _selectedPortfolioIndex { get; set; }
        public int selectedPortfolioIndex
        {
            get { return _selectedPortfolioIndex; }
            set
            {
                _selectedPortfolioIndex = value;
                OnPropertyChanged("selectedPortfolioIndex");
            }
        }
        private int _portfolioSelectedIndex { get; set; }
        public int portfolioSelectedIndex
        {
            get { return _portfolioSelectedIndex; }
            set
            {
                _portfolioSelectedIndex = value;
                OnPropertyChanged("portfolioSelectedIndex");
            }
        }
        private int _actSelectedIndex { get; set; }
        public int actSelectedIndex
        {
            get { return _actSelectedIndex; }
            set
            {
                _actSelectedIndex = value;
                OnPropertyChanged("actSelectedIndex");
            }
        }
        private int _deptSelectedIndex { get; set; }
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

        private string _getIsExcept { get; set; } = string.Empty;
        public string getIsExcept
        {
            get { return _getIsExcept; }
            set
            {
                _getIsExcept = value;
                OnPropertyChanged("getIsExcept");
            }
        }

        private bool _CheckBoxIsExceptEnabled { get; set; } = false;
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
        private List<PortfolioInfoViewModel> _port { get; set; }
        public List<PortfolioInfoViewModel> port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged("port");
            }
        }
        private System.Windows.Documents.Paragraph _par { get; set; }
        public System.Windows.Documents.Paragraph par
        {
            get { return _par; }
            set
            {
                _par = value;
                OnPropertyChanged("par");
            }
        }


        public string generalOrderPadding { get; set; } = WindowViewModel.instance.generalOrderPadding;
        public int actAdminRichTextBoxHeight { get; set; }
        public System.Windows.Documents.List list1;

        #endregion


        #region applicationWindow
        public int UIFontSize { get; set; } = WindowViewModel.instance.UIFontSize;
        public int UIWidth { get; set; } = WindowViewModel.instance.UIWidth;
        public int UIHeight { get; set; } = WindowViewModel.instance.GOActAdminAddHeight;
        #endregion


        #region commands

        public ICommand cbChecked { get; set; }
        public ICommand btnBackAndSave { get; set; }
        public ICommand btnCancel { get; set; }
        public ICommand insertTextRTB { get; set; }
        public ICommand insertTextRTBblock { get; set; }
        public ICommand deleteTextRTBblock { get; set; }
        public ICommand selectPortfolioCB { get; set; }
        public ICommand selectDeptCB { get; set; }
        public ICommand selectActCB { get; set; }
        public ICommand getPortfolioWikiInformation { get; set; }
        public ICommand getActWikiInformation { get; set; }
        public ICommand getDepartmentWikiInformation { get; set; }
        public ICommand getCommentsWikiInformation { get; set; }
        public ICommand getEffectiveDateWikiInformation { get; set; }
        public ICommand getBackAndSaveBtnWikiInformation { get; set; }
        public ICommand getCancelBtnWikiInformation { get; set; }
        public ICommand getApplicationWikiInformation { get; set; }

        #endregion

        /// <summary>
        /// Constructor. Sets the Richtextbox size based on WindowViewModel size
        /// </summary>
        public GOActAdminAddVM()
        {
            actAdminRichTextBoxHeight = (UIHeight / 6) * 4;
            BindableRichTextBox.UIFontSize = UIFontSize;

            //Set the relay commands
            wikiFlowDoc = new FlowDocument();
            cBSelectedPortfolioValue = new PortfolioViewModel();
            cBSelectedActValue = new ActTitleViewModel();
            cBSelectedDeptValue = new DepartmentViewModel();
            cbChecked = new RelayCommand(SelectCBChecked);
            getPortfolioWikiInformation = new RelayCommand(SelectGetPortfolioWikiInformation);
            getActWikiInformation = new RelayCommand(SelectGetActWikiInformation);
            getDepartmentWikiInformation = new RelayCommand(SelectGetDepartmentWikiInformation);
            getCommentsWikiInformation = new RelayCommand(SelectGetCommentsWikiInformation);
            getEffectiveDateWikiInformation = new RelayCommand(SelectGetEffectiveDateWikiInformation);
            getBackAndSaveBtnWikiInformation = new RelayCommand(SelectGetBackAndSaveBtnWikiInformation);
            getCancelBtnWikiInformation = new RelayCommand(SelectGetCancelBtnWikiInformation);
            getApplicationWikiInformation = new RelayCommand(SelectGetApplicationWikiInformation);
            btnCancel = new RelayCommand(selectBtnCancel);
            btnBackAndSave = new RelayCommand(selectbtnBackAndSave);
            insertTextRTB = new RelayCommand(selectInsertTextRTB);
            insertTextRTBblock = new RelayCommand(SelectInsertTextRTBblock);
            deleteTextRTBblock = new RelayCommand(SelectDeleteTextRTBblock);
            selectActCB = new RelayCommand(SelectActContents);
            selectDeptCB = new RelayCommand(SelectDeptContents);
            selectPortfolioCB = new RelayCommand(SelectPortfolioContents);


            getData();
            portfolioSelectedIndex = 0;

            //Get all the helper results for this Application Window. This will display the Helper info in the Wiki Window
            helperResults = DatabaseEntity.ApplicationPage.goActAdminAdd.GetHelpInfo();

            flowDoc = new FlowDocument();
            List<PortfolioInfoViewModel> port = new List<PortfolioInfoViewModel>();
            WindowViewModel.instance.selectedRecord.isExcept = false;

            //Set the default layout of the RTB
            var par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, "", UIFontSize);
            RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, 0, 1, UIFontSize);

        }


        /// <summary>
        /// Set the UI check box to toggle between 'IsExcept' true or false
        /// </summary>
        /// <param name="obj"></param>
        private void SelectCBChecked(object obj)
        {
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Is Except = {CheckBoxIsExcept}");
            WindowViewModel.instance.selectedRecord.isExcept = CheckBoxIsExcept;
            BindableRichTextBox.UIHasChanged = false;
        }

        #region Wiki
        //This adds information about the action just undertaken and reports it in the Application Logger window
        private void SelectGetSelectedDate()
        {
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Selected Date: {effectiveDatePicker.Date} ");
        }

        private void SelectGetApplicationWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ScreenHelpText);
        }


        private void SelectGetCancelBtnWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "CancelBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetBackAndSaveBtnWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "BackAndSaveBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }


        private void SelectGetEffectiveDateWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "EffectiveDateBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetCommentsWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "CommentsRTB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetDepartmentWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "DepartmentComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetActWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "ActComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetPortfolioWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "PortfolioComboBox").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        #endregion

        /// <summary>
        /// When a user selects and Act from the drop down
        /// </summary>
        private void SelectActContents()
        {
            // Get the Act information via the extention method GetActTitleInfo() using ActTitleViewModel model
            WindowViewModel.instance.selectedRecord.ildNumber = cBSelectedActValue.ActTitle.GetActTitleInfo().ActTitleILDNumber;
            WindowViewModel.instance.selectedRecord.actTitle = cBSelectedActValue.ActTitle;
            portfolioSelectedIndex = 0;
            deptSelectedIndex = 0;
        }

        /// <summary>
        /// When a user selects a portfolio from the drop down
        /// </summary>
        private async void SelectPortfolioContents()
        {
            if (cBSelectedPortfolioValue != null) //check if its null. This method gets called when initializing ie.  portfolioList = new ObservableCollection<PortfolioViewModel>();
            {
                if (cBSelectedPortfolioValue.PortfolioId > 0) //Check to make sure that there is a valid value
                {
                    await RunCommandAsync(() => ImportFileIsRunning, async () =>
                    {
                        spinnerBool = ImportFileIsRunning;

                        //Return a PortfolioInfoViewModel model of department and portfolio values
                        port = await Task.Run(() => GetPortfolios.GetPortfolioInformation(cBSelectedPortfolioValue.PortfolioId));

                        //Check to see if there are any results. This can only happen on an empty Database
                        if (port != null)
                        {
                            //Initialize WindowViewModel.instance.selectedRecord with some default values
                            var res = port.Select(r => new GeneralFieldsViewlModel
                            {
                                department = r.Department,
                                deptCode = r.DeptCode,
                                portfolioID = r.PortfolioId,
                                portfolio = r.PortfolioName,
                                departmentPortfolioID = r.DepartmentPortfolioId
                            }).FirstOrDefault();

                            //Set the global WindowViewModel.instance.selectedRecord values to be used throught the application
                            WindowViewModel.instance.selectedRecord.department = res.department;
                            WindowViewModel.instance.selectedRecord.deptCode = res.deptCode;
                            WindowViewModel.instance.selectedRecord.portfolio = res.portfolio;
                            WindowViewModel.instance.selectedRecord.portfolioID = res.portfolioID;
                            WindowViewModel.instance.selectedRecord.departmentPortfolioID = res.departmentPortfolioID;
                            //populate department list drop down
                            deptList = port.Select(r => new DepartmentViewModel
                            {
                                Department = r.Department,
                                DepartmentId = r.DepartmentId,
                                DepartmentPortfolioID = r.DepartmentPortfolioId
                            }).ToList();
                            cBSelectedDeptValue = deptList.FirstOrDefault();
                        }
                        else
                            //If the port == null, then set tghe deptlist as a new object to avoid the application from crashing
                            deptList = new List<DepartmentViewModel>();
                    });
                    spinnerBool = ImportFileIsRunning;
                }
            }
        }

        /// <summary>
        /// When a user select a department from the drop down
        /// </summary>
        private void SelectDeptContents()
        {
            if (cBSelectedDeptValue != null) //check if its null. This method gets called when initializing ie.  portfolioList = new ObservableCollection<PortfolioViewModel>();
            {
                var res = GetDepartments.GetDepartmentInformation(cBSelectedDeptValue.DepartmentPortfolioID);
                WindowViewModel.instance.selectedRecord.deptCode = res[0].DeptCode;
                WindowViewModel.instance.selectedRecord.department = res[0].Department;
                WindowViewModel.instance.selectedRecord.portfolioID = res[0].PortfolioId;
                WindowViewModel.instance.selectedRecord.departmentPortfolioID = res[0].DepartmentPortfolioId;
            }
        }



        /// <summary>
        /// Get the data from the database to dispolay
        /// </summary>
        private async void getData()
        {
            await RunCommandAsync(() => ImportFileIsRunning, async () =>
            {
                spinnerBool = ImportFileIsRunning;
                portfolioList = new List<PortfolioViewModel>();
                actList = new ObservableCollection<ActTitleViewModel>();
                //Get the portfolios
                portfolioList = GetPortfolios.GetPortfolio();
                //Get the Act Titles
                await Task.Run(() => actList = GetActTitles.GetActTitle());

            });
            spinnerBool = ImportFileIsRunning;
            WindowViewModel.instance.selectedRecord.portfolioID = cBSelectedPortfolioValue.PortfolioId = portfolioList.FirstOrDefault().PortfolioId; // currentSelectedPortfolio.generalOrderPortfolioID;
            WindowViewModel.instance.selectedRecord.portfolio = portfolioList.FirstOrDefault().PortfolioName;
            portfolioSelectedIndex = 0;

            var res = actList.Where(r => r.ActTitleILDNumber == WindowViewModel.instance.selectedRecord.ildNumber).FirstOrDefault();
            actSelectedIndex = actList.IndexOf(res); // (r => r.ActTitleILDNumber == WindowViewModel.instance.selectedRecord.ildNumber);

        }

        /// <summary>
        /// When a user click the back and save button
        /// </summary>
        /// <param name="obj"></param>
        private void selectbtnBackAndSave(object obj)
        {
            SelectSave();
        }


        /// <summary>
        /// Saves the contents of the RichTextBox as well as the drop down values to the Database.
        /// Also manipulates the records in the RTB with appropriate colours based on deleted or edited records
        /// </summary>
        private void SelectSave()
        {
            if (DatePickerHelper.isDateValid)
            {
                //Make sure that there are valid values
                if (WindowViewModel.instance.selectedRecord.departmentPortfolioID != 0
                   || WindowViewModel.instance.selectedRecord.ildNumber != 0)
                {
                    //Write to the application logger window
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Saving a new GO record");

                    //Writing logger information into the Database
                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "GOActAdminAdd",
                        LogDetail = "Added new GO record",
                        LogDetail_Additional = "User created a new GO record by selecting the 'Add New' button from the main window GOActAdminDetailsUpdate",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GeneralOrderUpdateDetails", false);


                    //Check to see if the record has an equivalent in current List and mark as delete
                    var currRecordExists = GOActAdminDetailsUpdateVM.instance.currentAdminListBox // GOActAdminDetailsUpdateVM.currentAdminListBox
                       .Where(r => r.departmentPortfolioID == WindowViewModel.instance.selectedRecord.departmentPortfolioID
                           && r.ildNumber == WindowViewModel.instance.selectedRecord.ildNumber).FirstOrDefault();

                    if (currRecordExists != null)
                    {
                        currRecordExists.isCurrent = true;
                        currRecordExists.isExcept = CheckBoxIsExcept;
                        currRecordExists.endDate = null; // DateTime.Now;
                        currRecordExists.pendingEditID = currRecordExists.actAdministrationID; //0
                        currRecordExists.pendingEditType = (short)RecordPendingEditType.Delete;
                        currRecordExists.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(currRecordExists.pendingEditType)}{currRecordExists.actAdministrationID}";  //"D";
                        currRecordExists.foregroundColour = "Red";
                        currRecordExists.backgroundColour = "Pink";
                        if (currRecordExists.generalFieldsCommentViewModel.Count > 0)
                            if (currRecordExists.generalFieldsCommentViewModel[0].actAdministrationComment != string.Empty)
                            {
                                GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct(currRecordExists);
                            }
                    }




                    //Check to see if there is another records with the same Portfolio, Act title and Department
                    var recordExists = GOActAdminDetailsUpdateVM.instance.pendingAdminListBox //GOActAdminDetailsUpdateVM.pendingAdminListBox
                        .Where(r => r.departmentPortfolioID == WindowViewModel.instance.selectedRecord.departmentPortfolioID
                            && r.ildNumber == WindowViewModel.instance.selectedRecord.ildNumber
                           && r.isCurrent == false
                           && r.endDate == null).FirstOrDefault();

                    List<GeneralFieldsViewlModel> newComments = new List<GeneralFieldsViewlModel>();
                    GeneralFieldsViewlModel newComment = new GeneralFieldsViewlModel();
                    var meta = new DocViewModel();
                    WindowViewModel.instance.selectedRecord.isCurrent = false; //= RadioOnHold == "NO" ? true : false;   //!onHold;

                    if (recordExists != null)
                    {
                        GOUpdateDetailsImportedFiles.GODeletePendingActs(recordExists.actAdministrationID);
                        GOActAdminDetailsUpdateVM.instance.pendingAdminListBox.Remove(recordExists);
                        recordExists.actAdministrationID = WindowViewModel.instance.selectedRecord.actAdministrationID;
                        recordExists.actTitle = WindowViewModel.instance.selectedRecord.actTitle;
                        recordExists.pendingEditType = (short)RecordPendingEditType.Add;
                        recordExists.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(recordExists.pendingEditType)}{(currRecordExists != null ? currRecordExists.actAdministrationID : 0)}"; //"A";
                        recordExists.department = WindowViewModel.instance.selectedRecord.department;
                        recordExists.deptCode = WindowViewModel.instance.selectedRecord.deptCode;
                        recordExists.endDate = WindowViewModel.instance.selectedRecord.endDate;
                        recordExists.departmentPortfolioID = WindowViewModel.instance.selectedRecord.departmentPortfolioID;
                        recordExists.flowDocument = flowDoc;
                        recordExists.ildNumber = WindowViewModel.instance.selectedRecord.ildNumber;
                        recordExists.isCurrent = WindowViewModel.instance.selectedRecord.isCurrent;
                        recordExists.isExcept = CheckBoxIsExcept; // WindowViewModel.instance.selectedRecord.isExcept;
                        recordExists.pendingEditID = currRecordExists != null ? currRecordExists.actAdministrationID : 0;
                        recordExists.portfolio = WindowViewModel.instance.selectedRecord.portfolio;
                        recordExists.portfolioID = WindowViewModel.instance.selectedRecord.portfolioID;
                        recordExists.startDate = effectiveDatePicker.Date;
                        recordExists.generalFieldsCommentViewModel = flowDoc.LoopThroughFlowDoc(recordExists.actAdministrationID);
                    }
                    else
                    {
                        recordExists = new NewAdminDetails
                        {
                            actAdministrationID = WindowViewModel.instance.selectedRecord.actAdministrationID,
                            actTitle = WindowViewModel.instance.selectedRecord.actTitle,
                            pendingEditType = (short)RecordPendingEditType.Add, // WindowViewModel.instance.selectedRecord.pendingEditType,
                            change = "A" + (currRecordExists != null ? currRecordExists.actAdministrationID : 0).ToString(),  //recordSelected.change,
                            department = WindowViewModel.instance.selectedRecord.department,
                            deptCode = WindowViewModel.instance.selectedRecord.deptCode,
                            endDate = WindowViewModel.instance.selectedRecord.endDate,
                            departmentPortfolioID = WindowViewModel.instance.selectedRecord.departmentPortfolioID,
                            flowDocument = flowDoc,
                            ildNumber = WindowViewModel.instance.selectedRecord.ildNumber,
                            isCurrent = WindowViewModel.instance.selectedRecord.isCurrent,
                            isExcept = CheckBoxIsExcept, // WindowViewModel.instance.selectedRecord.isExcept,
                            pendingEditID = 0, //currRecordExists != null ? currRecordExists.actAdministrationID : 0,
                            portfolio = WindowViewModel.instance.selectedRecord.portfolio,
                            portfolioID = WindowViewModel.instance.selectedRecord.portfolioID,
                            startDate = effectiveDatePicker.Date, //WindowViewModel.instance.selectedRecord.startDate, 
                            generalFieldsCommentViewModel = flowDoc.LoopThroughFlowDoc(WindowViewModel.instance.selectedRecord.actAdministrationID) // new ObservableCollection<GeneralFieldsCommentViewModel>()
                        };
                    }
                    if (recordExists.generalFieldsCommentViewModel.Count > 0)
                        if (recordExists.generalFieldsCommentViewModel[0].actAdministrationComment != string.Empty)
                        {
                            //Upload to database
                            var ActID = GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct(recordExists);
                            recordExists.actAdministrationID = recordExists.actAdministrationID == 0 ? ActID : recordExists.actAdministrationID;

                            GOActAdminDetailsUpdateVM.instance.pendingAdminListBox.Add(recordExists);
                        }
                    WindowViewModel.instance.CurrentPage = ApplicationPage.goDetailsUpdate;

                }
                else
                    MessageBoxHelper.CatchExceptionMessageBox("You cannot add a new record since there is information missing");
            }
            else
            {
                MessageBoxHelper.CatchExceptionMessageBox("Effective Date is incorrect");
            }
        }


        private void selectBtnCancel(object obj)
        {
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Add new record cancelled");

            WindowViewModel.instance.CurrentPage = ApplicationPage.goDetailsUpdate;
        }

        /// <summary>
        /// This method is used for enabling or disabling Is Except check box
        /// </summary>
        private void SelectDeleteTextRTBblock()
        {
            //Get the contents of the flow doc
            TextRange tr = new TextRange(flowDoc.ContentStart, flowDoc.ContentEnd);
            CheckBoxIsExceptEnabled = tr.Text.Replace("\t\r\n", string.Empty).Length > 0 ? true : false;
            CheckBoxIsExcept = false;
        }

        private void SelectInsertTextRTBblock()
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

            TextPointer caretPos = ActAdminRichTextBox.CaretPosition;
            Block flowBlock = ActAdminRichTextBox.Document.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();
            if (flowBlock != null)
                flowDoc.Blocks.InsertAfter(flowBlock, list1);
            else
                flowDoc.Blocks.Add(list1);

            ActAdminRichTextBox.CaretPosition = list1.ContentStart;

            selectInsertTextRTB();
        }

        /// <summary>
        /// When a user tabs inside the RichTextBox
        /// </summary>
        private void selectInsertTextRTB()
        {
            CheckBoxIsExceptEnabled = true;
            //If its a blank RichTextBox and you press tab, create a new block
            if (ActAdminRichTextBox.Document.Blocks.Count() == 0)
            {
                list1 = new System.Windows.Documents.List();
                var par = new System.Windows.Documents.Paragraph();
                par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
                par.Padding = new Thickness(0, 0, 0, 0);

                par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                par.TextAlignment = TextAlignment.Left;
                par.FontFamily = new System.Windows.Media.FontFamily("Courier");
                par.FontSize = UIFontSize;
                list1.Margin = new Thickness(99, 0, 0, 0);
                list1.MarkerStyle = TextMarkerStyle.None;
                list1.ListItems.Add(new ListItem(par));
                flowDoc.Blocks.Add(list1);
            }
            ActAdminRichTextBox.InsertTextRTB(UIFontSize);
        }


    }

}

