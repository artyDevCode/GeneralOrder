using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntity;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using ApplicationLogger;
using System.Globalization;
using Reporting;

namespace GeneralOrderUpdateDetails
{
    /// <summary>
    /// This is the entry point from ILD where the user can update the GO
    /// </summary>
    public class GOActAdminDetailsUpdateVM : BaseViewModel
    {
        public static GOActAdminDetailsUpdateVM instance;

        #region properties
        private ObservableCollection<OrigAdminDetails> _currentAdminListBox { get; set; }
        public ObservableCollection<OrigAdminDetails> currentAdminListBox
        {
            get { return _currentAdminListBox; }
            set
            {
                _currentAdminListBox = value;
                OnPropertyChanged("currentAdminListBox");
            }
        }
        private ObservableCollection<NewAdminDetails> _pendingAdminListBox { get; set; }
        public ObservableCollection<NewAdminDetails> pendingAdminListBox
        {
            get { return _pendingAdminListBox; }
            set
            {
                _pendingAdminListBox = value;
                OnPropertyChanged("pendingAdminListBox");
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
      
        private string _RadioActReport { get; set; } = "2";
        public string RadioActReport
        {
            get { return _RadioActReport; }
            set
            {
                _RadioActReport = value;
                OnPropertyChanged("RadioActReport");
            }
        }
        private string _RadioUploadAct { get; set; } = "2";
        public string RadioUploadAct
        {
            get { return _RadioUploadAct; }
            set
            {
                _RadioUploadAct = value;
                OnPropertyChanged("RadioUploadAct");
            }
        }
        private string _RadioActAdminReport { get; set; } = "1";
        public string RadioActAdminReport
        {
            get { return _RadioActAdminReport; }
            set
            {
                _RadioActAdminReport = value;
                OnPropertyChanged("RadioActAdminReport");
            }
        }


        private string _RadioCommentsReport { get; set; } = "2";
        public string RadioCommentsReport
        {
            get { return _RadioCommentsReport; }
            set
            {
                _RadioCommentsReport = value;
                OnPropertyChanged("RadioCommentsReport");
            }
        }
        private string _generalOrderILD { get; set; }

        public string generalOrderILD
        {
            get { return _generalOrderILD; }
            set
            {
                _generalOrderILD = value;
                OnPropertyChanged("generalOrderILD");
            }
        }
        private ListViewItemComments _CurrentDialogPage { get; set; }
        public ListViewItemComments CurrentDialogPage
        {
            get { return _CurrentDialogPage; }
            set
            {
                _CurrentDialogPage = value;
                OnPropertyChanged("CurrentDialogPage");
            }
        }
        private List<ActTitleViewModel> _actList { get; set; }
        public List<ActTitleViewModel> actList
        {
            get { return _actList; }
            set
            {
                _actList = value;
                OnPropertyChanged("actList");
            }
        }
        public int _actSelectedIndex { get; set; }
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
        public ICommand selectActCB { get; set; }

        // public string generalOrderActName { get; set; }
        private string _generalOrderActNumber { get; set; }
        public string generalOrderActNumber
        {
            get { return _generalOrderActNumber; }
            set
            {
                _generalOrderActNumber = value;
                OnPropertyChanged("generalOrderActNumber");
            }
        }
        public System.Windows.Documents.List list1;
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
        //public FlowDocument loggerFlowDoc { get; set; }
        //public List<ActAdministrationViewModel> _selectedRecord { get; set; }  //List of records from AllRecordsList but only of an ILD Number ie 9650
        //public List<ActAdministrationViewModel> selectedRecord
        //{
        //    get { return _selectedRecord; }
        //    set
        //    {
        //        _selectedRecord = value;
        //        OnPropertyChanged("selectedRecord");
        //    }
        //}
        private List<ActAdministrationViewModel> _AllRecordsList { get; set; } //List of all records in ActAdministration
        public List<ActAdministrationViewModel> AllRecordsList
        {
            get { return _AllRecordsList; }
            set
            {
                _AllRecordsList = value;
                OnPropertyChanged("AllRecordsList");
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

        #endregion

        #region commands
        public ICommand editSelectedRow { get; set; }
        public ICommand deleteSelectedRow { get; set; }
        public ICommand btnAddNew { get; set; }
        public ICommand btnGenerateActReport { get; set; }
        public ICommand btnGenerateActAdminReport { get; set; }
        public ICommand btnCancel { get; set; }
        public ICommand DisplayMessageCommand { get; set; }
        public ICommand btnUploadDetails { get; set; }
        public ICommand getApplicationWikiInformation { get; set; }
        public ICommand getPrintBtnWikiInformation { get; set; }
        public ICommand getPrintCurrentRBWikiInformation { get; set; }
        public ICommand getPrintNewRBWikiInformation { get; set; }
        public ICommand getPrintThisActRBWikiInformation { get; set; }
        public ICommand getPrintAllRBWikiInformation { get; set; }
        public ICommand getOnHoldYESRBWikiInformation { get; set; }
        public ICommand getOnHoldNORBWikiInformation { get; set; }
        public ICommand getUploadBtnWikiInformation { get; set; }
        public ICommand getUploadAllActsRBWikiInformation { get; set; }
        public ICommand getUploadThisActRBWikiInformation { get; set; }
        public ICommand getBackBtnWikiInformation { get; set; }
        public ICommand getCancelBtnWikiInformation { get; set; }
        public ICommand getAddNewBtnWikiInformation { get; set; }
        public ICommand btnDiscardPending { get; set; }

        #endregion


        #region UIResizing
        public int UIFontSize { get; set; } = WindowViewModel.instance.UIFontSize;
        public int UIWidth { get; set; } = WindowViewModel.instance.UIWidth;
        public int UIHeight { get; set; } = WindowViewModel.instance.GOAdminDetailsUpdateHeight;
        public int GOListBoxHeight { get; set; }
        public int GOListBoxWidth { get; set; }
        public int GOListViewHeight { get; set; }
        public string generalOrderPadding { get; set; } = WindowViewModel.instance.generalOrderPadding;
        #endregion


        //Contructor
        public GOActAdminDetailsUpdateVM()
        {
            //Set the size and font of the UI
            BindableRichTextBox.UIFontSize = UIFontSize;
            GOListBoxWidth = ((800 * Convert.ToInt32(UIFontSize + "0")) / 110) - 200;
            GOListBoxHeight = (480 * Convert.ToInt32(UIFontSize + "0")) / 110;
            GOListViewHeight = (210 * Convert.ToInt32(UIFontSize + "0")) / 110;

            #region relaycommands
            selectActCB = new RelayCommand(SelectActContents);
            getApplicationWikiInformation = new RelayCommand(SelectGetApplicationWikiInformation);
            getPrintBtnWikiInformation = new RelayCommand(SelectGetPrintBtnWikiInformation);
            getPrintCurrentRBWikiInformation = new RelayCommand(SelectGetPrintCurrentRBWikiInformation);
            getPrintNewRBWikiInformation = new RelayCommand(SelectGetPrintNewRBWikiInformation);
            getPrintThisActRBWikiInformation = new RelayCommand(SelectGetPrintThisActRBWikiInformation);
            getPrintAllRBWikiInformation = new RelayCommand(SelectGetPrintAllRBWikiInformation);
            getOnHoldYESRBWikiInformation = new RelayCommand(SelectGetOnHoldYESRBWikiInformation);
            getOnHoldNORBWikiInformation = new RelayCommand(SelectGetOnHoldNORBWikiInformation);
            getUploadBtnWikiInformation = new RelayCommand(SelectGetUploadBtnWikiInformation);
            getUploadAllActsRBWikiInformation = new RelayCommand(SelectGetUploadAllActsRBWikiInformation);
            getUploadThisActRBWikiInformation = new RelayCommand(SelectGetUploadThisActRBWikiInformation);
            getBackBtnWikiInformation = new RelayCommand(SelectGetBackBtnWikiInformation);
            getCancelBtnWikiInformation = new RelayCommand(SelectGetCancelBtnWikiInformation);
            getAddNewBtnWikiInformation = new RelayCommand(SelectGetAddNewBtnWikiInformation);
            btnGenerateActReport = new RelayCommand(SelectBtnGenerateActReport);
            btnGenerateActAdminReport = new RelayCommand(SelectBtnGenerateActAdminReport);
            btnUploadDetails = new RelayCommand(SelectBtnUploadDetails);
            editSelectedRow = new RelayCommand(getEditSelectedRow);
            deleteSelectedRow = new RelayCommand(getDeleteSelectedRow);
            btnAddNew = new RelayCommand(selectBtnAddNew);
            btnCancel = new RelayCommand(selectBtnCancel);
            btnDiscardPending = new RelayCommand(selectBtnDiscardPending);

            DisplayMessageCommand = new RelayCommand(DisplayMessage); // (p => DisplayMessage());
            #endregion

            //Return the helper info for this application
            helperResults = DatabaseEntity.ApplicationPage.goActAdminDetailsUpdate.GetHelpInfo();

            cBSelectedActValue = new ActTitleViewModel();
            wikiFlowDoc = new FlowDocument();

            if (actList == null)
                actList = new List<ActTitleViewModel>();  //Create an instance of the COMBO BOX array
            var res = GetGeneralOrder.GetActAdministrationILDList();  //Get the GO list
            actList = GetActTitles.GetActTitleFromExistingData(res);  //get all the acts for the combo box

            //set the first list item as the ILD number sent through
            var actL = actList.Where(r => r.ActTitleILDNumber == WindowViewModel.instance.selectedRecord.ildNumber).FirstOrDefault();

            if (actL != null)
            {
                actSelectedIndex = actList.IndexOf(actL);
                cBSelectedActValue = actList[actSelectedIndex];
                generalOrderILD = cBSelectedActValue.ActTitleILDNumber.ToString(); // actL.ActTitleILDNumber.ToString(); //Set the UI with the ILD number
            }
            else
            { //Handle the UI if there is NO records in the DB
                actSelectedIndex = 0;
                cBSelectedActValue = actList.Count > 0 ? actList.FirstOrDefault() : new ActTitleViewModel(); //[actSelectedIndex];
                generalOrderILD = cBSelectedActValue.ActTitleILDNumber.ToString(); // actL.ActTitleILDNumber.ToString(); //Set the UI with the ILD number
                WindowViewModel.instance.selectedRecord.ildNumber = cBSelectedActValue.ActTitleILDNumber;
            }
            
        }

        /// <summary>
        /// When an Act is selected from the combo box, it will trigger this mehod to get all the records that have the ILD number
        /// </summary>
        /// <returns></returns>
        public bool GetAllGORecordsByILD()
        {

            
            //Get all the records from ActAdministration to get the ild numbers and get the act titles from it.
            GOActAdminDetailsUpdateVM.instance.currentAdminListBox = new ObservableCollection<OrigAdminDetails>();
            GOActAdminDetailsUpdateVM.instance.pendingAdminListBox = new ObservableCollection<NewAdminDetails>();

            //Get all the Records from the DB based on the ILD Number
            AllRecordsList = GetGeneralOrder.GetActAdministrationDoc(WindowViewModel.instance.selectedRecord.ildNumber);

            if (AllRecordsList.Count() > 0)
            {
                // selectedRecord = AllRecordsList; //.Where(r => r.ILDNumber == WindowViewModel.ildNumber).ToList(); //Set the selected record to "SelectedRecord"
                //UI components
                generalOrderILD = cBSelectedActValue.ActTitleILDNumber.ToString(); // actL.ActTitleILDNumber.ToString(); //Set the UI with the ILD number
                generalOrderActNumber = cBSelectedActValue.ActNumber.ToString() + "/" + cBSelectedActValue.ActTitle.Substring(cBSelectedActValue.ActTitle.Length - 4, 4); //Set the UI effective date of the selected Act      

                GOActAdminDetailsUpdate(AllRecordsList); //Populate the Current and non-current lists in the UI
                DisplayMessage(GOActAdminDetailsUpdateVM.instance.currentAdminListBox == null ? (GeneralFieldsViewlModel)GOActAdminDetailsUpdateVM.instance.pendingAdminListBox.FirstOrDefault() : (GeneralFieldsViewlModel)GOActAdminDetailsUpdateVM.instance.currentAdminListBox.FirstOrDefault());
                TidyUI();
            }

            return AllRecordsList.Count() > 0;
        }


        /// <summary>
        /// UI Act Combo box. Once an item is selected, its values are copied over to WindowViewModel.instance.selectedRecord static var
        /// </summary>
        /// <param name="obj"></param>
        private void SelectActContents(object obj)
        {

            if (cBSelectedActValue != null)
            {
                WindowViewModel.instance.selectedRecord.ildNumber = cBSelectedActValue.ActTitleILDNumber;
                WindowViewModel.instance.selectedRecord.actTitle = cBSelectedActValue.ActTitle; // + (WindowViewModel.instance.selectedRecord.isExcept ? " - Except:" : "")             

                GetAllGORecordsByILD();
                //Write to the application logger window
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} -- Selected Act { cBSelectedActValue.ActTitle} - Act Number {cBSelectedActValue.ActNumber.ToString() + "/" + cBSelectedActValue.ActTitle.Substring(cBSelectedActValue.ActTitle.Length - 4, 4)}");

            }
            //The Combo box UI color is rearraged as items are added and removed from the lists
            TidyUI();
        }

        /// <summary>
        /// Generate the Act administration reports
        /// </summary>
        private async void SelectBtnGenerateActAdminReport()
        {
            List<ActAdministrationViewModel> allILDrecs = new List<ActAdministrationViewModel>();
            string fileName = string.Empty;
            switch (RadioActAdminReport)
            {
                case "1":

                    await RunCommandAsync(() => ImportFileIsRunning, async () =>
                    {
                        spinnerBool = ImportFileIsRunning;
                        await Task.Run(() => allILDrecs = GetGeneralOrder.GetActAdministrationDoc());
                        List<Reporting.ReportModel> res = DBActAdministrators.createReportObjectActAdministratorsReport(allILDrecs.Where(r => r.IsCurrent == true).ToList());
                        if (res.Count > 0)
                        {
                            //Write to the application logger window
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Started printing 'Act Administrators Report'");
                            fileName = await ActAdministrationReport.ActAdministrationReportCreator(res);
                            if (fileName != string.Empty)
                                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished printing {fileName} ");
                            else
                                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancelled printing {fileName} ");                            
                        }
                        else
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- No records for 'Act Administrators Report'");
                    });
                    spinnerBool = ImportFileIsRunning;
                    break;
                case "2":
                    await RunCommandAsync(() => ImportFileIsRunning, async () =>
                    {
                        spinnerBool = ImportFileIsRunning;
                        await Task.Run(() => allILDrecs = GetGeneralOrder.GetActAdministrationDoc());
                        List<ActAdministeredReportModelILD> actRes = DBActAdministrators.createReportObjectAdministeredActs(allILDrecs.Where(r => r.IsCurrent == true).ToList());
                        if (actRes.Count > 0)
                        {
                            //Write to the application logger window
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- Started printing 'Administered Acts Report'");
                            fileName = await AdministeredActsReport.AdministeredActsReportCreator(actRes);
                            if (fileName != string.Empty)
                                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- Finished printing {fileName} ");
                            else
                                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- Cancelled printing {fileName} ");
                        }
                        else
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- No records for 'Administered Acts Report'");
                    });
                    spinnerBool = ImportFileIsRunning;
                    break;
                default:
                    break;
            }

        }


        //Generate reports of the GO. There are 4 options
        private async void SelectBtnGenerateActReport()
        {
            var res = new List<Reporting.ReportModel>();
            List<ActAdministrationViewModel> allILDrecs = new List<ActAdministrationViewModel>();
            string fileName = string.Empty;
            switch (RadioActReport)
            {

                case "1": // "All Act": 
                    switch (RadioCommentsReport)
                    {
                        case "1": // "With New Comments": 
                                  //using portfolio id
                            var yesno = MessageBoxHelper.DisplayMessageBox("Warning", "Printing this report may take up to 10 minutes. Would you like to continue?");
                            if (yesno)
                            {
                                await RunCommandAsync(() => ImportFileIsRunning, async () =>
                                {
                                    spinnerBool = ImportFileIsRunning;
                                    await Task.Run(() => allILDrecs = GetGeneralOrder.GetActAdministrationDoc());
                                    res = DBReportObject.CreateReportObject(allILDrecs.Where(r => r.PendingEditType != (short)RecordPendingEditType.Delete).ToList()); // && r.EndDate == null).ToList());
                                    if (res.Count > 0)
                                    {
                                        //Write to the application logger window
                                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Started printing 'All Acts / New Comments' "); //Act: {cBSelectedActValue.ActTitle}");
                                        fileName = await CreateGeneralOrderDoc.WordDocCreator(res, $"GO-AllActs-WithNewComments-{DateTime.Now.ToString("dd-MM-yyyy hh-mm tt")}");
                                        if (fileName != string.Empty)
                                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished printing 'All Acts / New Comments'  -- File Name: {fileName}");
                                        else
                                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancelled printing 'All Acts / New Comments'  -- File Name: {fileName}");
                                    }
                                    else
                                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- No records 'All Acts / New Comments' "); // Act: {cBSelectedActValue.ActTitle}");
                                });
                                spinnerBool = ImportFileIsRunning;
                            }
                            break;
                        case "2": // "Current Comments": 
                            yesno = MessageBoxHelper.DisplayMessageBox("Warning", "Printing this report may take up to 10 minutes. Would you like to continue?");
                            if (yesno)
                            {
                                await RunCommandAsync(() => ImportFileIsRunning, async () =>
                            {
                                spinnerBool = ImportFileIsRunning;
                                await Task.Run(() => allILDrecs = GetGeneralOrder.GetActAdministrationDoc());
                                res = DBReportObject.CreateReportObject(allILDrecs.Where(r => r.IsCurrent == true).ToList()); // && r.PendingEditType != (short)RecordPendingEditType.Delete && r.EndDate == null).ToList());
                                if (res.Count > 0)
                                {
                                    //Write to the application logger window
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Started printing 'All Acts / Current Comments' Act: {cBSelectedActValue.ActTitle}");
                                    fileName = await CreateGeneralOrderDoc.WordDocCreator(res, $"GO-AllActs-CurrentCommentsOnly-{DateTime.Now.ToString("dd-MM-yyyy hh-mm tt")}");
                                    if (fileName != string.Empty)
                                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished printing 'All Acts / Current Comments' Act: {cBSelectedActValue.ActTitle} -- File Name: {fileName}");
                                    else
                                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancelled printing 'All Acts / Current Comments' Act: {cBSelectedActValue.ActTitle} -- File Name: {fileName}");
                                }
                                else
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- No records 'All Acts / Current Comments' Act: {cBSelectedActValue.ActTitle}");
                            });
                                spinnerBool = ImportFileIsRunning;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case "2": // "This Act": 
                    switch (RadioCommentsReport)
                    {
                        case "1": // "New Comments": 
                            await RunCommandAsync(() => ImportFileIsRunning, async () =>
                            {
                                spinnerBool = ImportFileIsRunning;
                                await Task.Run(() => allILDrecs = GetGeneralOrder.GetActAdministrationDoc(WindowViewModel.instance.selectedRecord.ildNumber));
                                res = DBReportObject.CreateReportObject(allILDrecs.Where(r => r.PendingEditType != (short)RecordPendingEditType.Delete && r.ILDNumber == WindowViewModel.instance.selectedRecord.ildNumber).ToList());
                                if (res.Count > 0)
                                {
                                    //Write to the application logger window
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Started printing 'This Act / New Comments' Act: {cBSelectedActValue.ActTitle}");
                                    fileName = await CreateGeneralOrderDoc.WordDocCreator(res, $"GO-AThisAct-WithNewComments-{ DateTime.Now.ToString("dd-MM-yyyy hh-mm tt")}");
                                    if (fileName != string.Empty)
                                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished printing 'This Act / New Comments' Act: {cBSelectedActValue.ActTitle} -- File Name: {fileName}");
                                    else
                                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancelled printing 'This Act / New Comments' Act: {cBSelectedActValue.ActTitle} -- File Name: {fileName}");

                                }
                                else
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- No records 'This Act / New Comments' Act: {cBSelectedActValue.ActTitle}");
                            });
                            spinnerBool = ImportFileIsRunning;

                            break;
                        case "2": // "Current Comments": 
                                  //Current records are copied to a print object
                            await RunCommandAsync(() => ImportFileIsRunning, async () =>
                            {
                                spinnerBool = ImportFileIsRunning;
                                await Task.Run(() => allILDrecs = GetGeneralOrder.GetActAdministrationDoc(WindowViewModel.instance.selectedRecord.ildNumber));
                                res = DBReportObject.CreateReportObject(allILDrecs.Where(r => r.IsCurrent == true && r.ILDNumber == WindowViewModel.instance.selectedRecord.ildNumber).ToList());
                                if (res.Count > 0)
                                {
                                    //Write to the application logger window
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Started printing 'This Act / Current Comments'  Act: {cBSelectedActValue.ActTitle}");
                                    fileName = await CreateGeneralOrderDoc.WordDocCreator(res, $"GO-ThisAct-CurrentCommentsOnly-{DateTime.Now.ToString("dd-MM-yyyy hh-mm tt")}");
                                    if (fileName != string.Empty)
                                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished printing 'This Act / Current Comments' Act: {cBSelectedActValue.ActTitle} -- File Name: {fileName}");
                                    else
                                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancelled printing 'This Act / Current Comments' Act: {cBSelectedActValue.ActTitle} -- File Name: {fileName}");
                                }
                                else
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- No records 'This Act / Current Comments' Act: {cBSelectedActValue.ActTitle}");
                            });
                            spinnerBool = ImportFileIsRunning;

                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;


            }
        }

        private void selectBtnCancel(object obj)
        {
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancelled....");
            WindowViewModel.instance.WindowClose();
        }




        /// <summary>
        /// This will discard all the pending records that reside in the lower listbox
        /// </summary>
        /// <param name="obj"></param>
        private async void selectBtnDiscardPending(object obj)
        {
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- 'Discard Pending' records");

            if (MessageBoxHelper.DisplayMessageBox("Verify", "Are you sure you want to delete pending records?"))
            {
                bool res = false;
                await RunCommandAsync(() => ImportFileIsRunning, async () =>
                    {
                        spinnerBool = ImportFileIsRunning;
                        await Task.Run(() => res = ActAdministrationDC.DiscardPendingActAdministration());
                        if (res)
                             GOActAdminDetailsUpdateVM.instance.pendingAdminListBox.Clear();
                    });
                
                if (res)
                {
                    var ildListRes = GetGeneralOrder.GetActAdministrationILDList();
                    actList = GetActTitles.GetActTitleFromExistingData(ildListRes);
                    var actL = actList.Where(r => r.ActTitleILDNumber == WindowViewModel.instance.selectedRecord.ildNumber).FirstOrDefault();

                    if (actL != null)
                    {
                        actSelectedIndex = actList.IndexOf(actL);
                        cBSelectedActValue = actList[actSelectedIndex];
                        generalOrderILD = cBSelectedActValue.ActTitleILDNumber.ToString(); // actL.ActTitleILDNumber.ToString(); //Set the UI with the ILD number
                    }
                    TidyUI();
                    generalOrderILD = string.Empty;
                    generalOrderActNumber = string.Empty;
                    spinnerBool = ImportFileIsRunning;

                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Discard Pending records successfully.");

                    WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                    {
                        AdditionalUserInfo = "GOActAdminDetailsUpdate",
                        LogDetail = "Discard Pending records",
                        LogDetail_Additional = "",
                        LogTime = DateTime.Now,
                        UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                        Workstation = System.Environment.MachineName
                    }, "GeneralOrderUpdateDetails", false);
                }
                else
                {
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- ERROR occured with 'Discard Pending' records. See DB log 'ActAdministrationDC.DiscardPendingActAdministration'");

                }
            }
            else
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- CANCELLED 'Discard Pending' records");


        }


        /// <summary>
        /// Upload 'All Acts' or 'Current Acts' that are in the pending state 'Non Current List'.
        /// When uploading, GOActAdminDeleteCurrentFiles and GOActAdminUpdateNonCurrentFiles methods are executed. These will changed the state of the Acts to current
        /// </summary>
        /// <param name="obj"></param>
        private async void SelectBtnUploadDetails(object obj)
        {
            if (MessageBoxHelper.DisplayMessageBox("Upload", "Are you sure you want to update pending records?"))
            {
                ////Writing logger information into the Database
                WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                {
                    AdditionalUserInfo = "GOActAdminDetailsUpdate",
                    LogDetail = "Update current selected Act",
                    LogDetail_Additional = $"General Order ILD: {generalOrderILD} General Order Act Name: {cBSelectedActValue.ActTitle} Act Number: {generalOrderActNumber}",
                    // "General Order ILD: " + generalOrderILD + ", General Order Act Name: " + generalOrderActName + ", generalOrderEffectiveDate: " + generalOrderEffectiveDate,
                    LogTime = DateTime.Now,
                    UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    Workstation = System.Environment.MachineName
                }, "GeneralOrderUpdateDetails", false);

                //Use await async calls to maintain UI in an unlocked state
                await RunCommandAsync(() => ImportFileIsRunning, async () =>
                {
                    spinnerBool = ImportFileIsRunning;
                    //Check Radio Button for upload 'This Act' or 'All Acts'

                    switch (RadioUploadAct)
                    {
                        case "1": // 'All Acts' retrieved from the DB
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Uploading 'All Acts' records");
                            List<ActAdministrationViewModel> allILDrecs = new List<ActAdministrationViewModel>();

                            await Task.Run(() => allILDrecs = GetGeneralOrder.GetActAdministrationDoc()); //get all the records from the DB
                            if (allILDrecs.Count > 0)  //This will send records to GenOrderActAdminDeleteCurrentFiles to flag 'delete' to Current records
                            {
                                await Task.Run(() => GOUpdateDetailsImportedFiles.GOActAdminDeleteCurrentFiles(allILDrecs.Where(r => r.IsCurrent == true).ToList()));
                                await Task.Run(() => GOUpdateDetailsImportedFiles.GOActAdminUpdatePendingFiles(allILDrecs.Where(r => r.IsCurrent == false &&
                                                        r.PendingEditType != (short)RecordPendingEditType.Delete).ToList()));   //This will send records to GenOrderActAdminUpdatePendingFiles to process in DB
                                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished Uploading 'All Acts' records");
                            }
                            else
                                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- No records to upload");
                            break;
                        case "2": // 'This Act' in the UI being displayed
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Uploading 'This Act' General Order ILD: {generalOrderILD} General Order Act Name: {cBSelectedActValue.ActTitle} Act Number: {generalOrderActNumber}");

                            if (GOActAdminDetailsUpdateVM.instance.currentAdminListBox.Count > 0)  //This will send records to GenOrderActAdminDeleteCurrentFiles to flag 'delete' to Current records
                                await Task.Run(() => GOUpdateDetailsImportedFiles.GOActAdminDeleteCurrentFiles(AllRecordsList));

                            if (GOActAdminDetailsUpdateVM.instance.pendingAdminListBox.Count > 0)  //This will send records to GenOrderActAdminUpdatePendingFiles to process in DB
                                await Task.Run(() => GOUpdateDetailsImportedFiles.GOActAdminUpdatePendingFiles(AllRecordsList.Where(r => r.IsCurrent == false).ToList()));
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished uploading 'This Act' General Order ILD: {generalOrderILD} General Order Act Name: {cBSelectedActValue.ActTitle} Act Number: {generalOrderActNumber}");
                            break;
                        default:
                            break;
                    }
                });

                GetAllGORecordsByILD();

                TidyUI();
                spinnerBool = ImportFileIsRunning;
            }
        }

        private void DisplayMessage(object obj)
        {
            var rec = obj as GeneralFieldsViewlModel;
            if (rec != null)
            {
                generalOrderILD = rec.ildNumber.ToString(); // "11716";
                cBSelectedActValue.ActTitle = rec.actTitle; // "Transport Integration Act 2010";
                                                            //  generalOrderActNumber = rec.ActNumber.ToString(); // rec.effectiveDate;
                                                            //CurrentDialogPage.Show();
            }
        }

        /// <summary>
        /// Add new button will open up a window which allows the user to enter a new act with comments
        /// </summary>
        /// <param name="obj"></param>
        private void selectBtnAddNew(object obj)
        {
            //Write to logger
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Add a new record");
            //Initialize global variable
            WindowViewModel.instance.selectedRecord = new GeneralFieldsViewlModel();
            WindowViewModel.instance.selectedRecordIndex = -1;
            // WindowViewModel.ildNumber = cBSelectedActValue.ActTitleILDNumber;
            WindowViewModel.instance.selectedRecord.ildNumber = cBSelectedActValue.ActTitleILDNumber;
            WindowViewModel.instance.selectedRecord.actTitle = cBSelectedActValue.ActTitle;
            //Go to the Add Acts page
            WindowViewModel.instance.CurrentPage = ApplicationPage.GOActAdminAdd;
        }

        /// <summary>
        /// Edit icon in each row of the current and non current list will open a window wihich will have the contents of the record inserted.
        /// User is then able to modify the record/comments and save the changes
        /// If the selected record is a current record and changes are saved, it will create a new pending non current record and mark the current selected record as Delete
        /// If the selected record is a pending non current record and changes are saved, it will override the non current selected record 
        /// </summary>
        /// <param name="selectedItem"></param>
        private void getEditSelectedRow(object selectedItem)
        {

            var res = selectedItem.GetType().GetProperty("DataContext");

            //If its a current record
            if (selectedItem.ToString().Contains("OrigAdminDetails"))
            {
                WindowViewModel.instance.selectedRecordIndex = GOActAdminDetailsUpdateVM.instance.currentAdminListBox.IndexOf(res.GetValue(selectedItem) as OrigAdminDetails);
                WindowViewModel.instance.selectedRecord = res.GetValue(selectedItem) as OrigAdminDetails;
                WindowViewModel.instance.isCurrentList = true;

                //Write to logger
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} -- EDIT FROM CURRENT LIST-- Act: {WindowViewModel.instance.selectedRecord.actTitle} Department: {WindowViewModel.instance.selectedRecord.department}  Portfolio: {WindowViewModel.instance.selectedRecord.portfolio}");
            }
            else
            {
                //If its a Pending non current record
                WindowViewModel.instance.selectedRecordIndex = GOActAdminDetailsUpdateVM.instance.pendingAdminListBox.IndexOf(res.GetValue(selectedItem) as NewAdminDetails);
                WindowViewModel.instance.selectedRecord = res.GetValue(selectedItem) as NewAdminDetails;
                WindowViewModel.instance.isCurrentList = false;

                //Write to logger
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} -- EDIT FROM PENDING LIST-- Act: {WindowViewModel.instance.selectedRecord.actTitle} Department: {WindowViewModel.instance.selectedRecord.department} Portfolio: {WindowViewModel.instance.selectedRecord.portfolio}");
            }

            if (WindowViewModel.instance.selectedRecord.isExcept)
                WindowViewModel.instance.selectedRecord.portfolio = WindowViewModel.instance.selectedRecord.portfolio.Replace(" - Except:", string.Empty);
            if (WindowViewModel.instance.selectedRecord.generalFieldsCommentViewModel.Count > 0)
                WindowViewModel.instance.selectedRecord.portfolio = WindowViewModel.instance.selectedRecord.portfolio.Remove(WindowViewModel.instance.selectedRecord.portfolio.Length - 2);

            WindowViewModel.instance.CurrentPage = ApplicationPage.goIndividualDetails;

            GetAllGORecordsByILD();

            TidyUI();
        }

        /// <summary>
        /// Delete rows from CURRENT and NON CURRENT Lists in the UI.
        /// If the deleted record is from the Current list, it will mark the record as delete and color it red. It will only get updated to the delete status when the upload button is pressed.
        /// The user can opt to undelete this record.
        /// </summary>
        /// <param name="selectedItem"></param>
        private void getDeleteSelectedRow(object selectedItem)
        {

            var res = selectedItem.GetType().GetProperty("DataContext");
            var selectedRec = res.GetValue(selectedItem) as GeneralFieldsViewlModel;


            //Check if its the CURRENT list 
            if (selectedRec.isCurrent) // (selectedItem.ToString().Contains("OrigAdminDetails"))
            {
                if (MessageBoxHelper.DisplayMessageBox("Delete", selectedRec.pendingEditType != 3 ? "Are you sure you want to delete this record?" : "This will remove any pending records. Are you sure you want to Undelete this record?"))
                {
                    // var selectedRec = res.GetValue(selectedItem) as OrigAdminDetails;
                    var pendingRecordExists = GOActAdminDetailsUpdateVM.instance.pendingAdminListBox
                            .Where(r => r.departmentPortfolioID == selectedRec.departmentPortfolioID
                                && r.ildNumber == selectedRec.ildNumber).FirstOrDefault();
                    switch (selectedRec.pendingEditType) // selectedRec.change.Take(1).FirstOrDefault())
                    {
                        case (short)RecordPendingEditType.Delete: // 'D': //Already has been deleted but revert back
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} -- Undelete Current record --- Act: {selectedRec.actTitle} Department: {selectedRec.department} Portfolio: {selectedRec.portfolio}");

                            selectedRec.isDelete = "Visible";
                            selectedRec.isCurrent = true;
                            selectedRec.endDate = null;
                            selectedRec.pendingEditID = 0;
                            selectedRec.pendingEditType = (short)RecordPendingEditType.None;
                            selectedRec.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(selectedRec.pendingEditType)}";
                            selectedRec.foregroundColour = string.Empty;
                            selectedRec.backgroundColour = string.Empty;
                            GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct(selectedRec);

                            //find the equivalent record in Non Current list and delete it when the current record has been Un-deleted

                            if (pendingRecordExists != null)
                            {
                                if (GOUpdateDetailsImportedFiles.GODeletePendingActs(pendingRecordExists.actAdministrationID))
                                {
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} -- Deleted duplicate record from Pending List --- Act: {pendingRecordExists.actTitle} Department: {pendingRecordExists.department} Portfolio: {pendingRecordExists.portfolio}");
                                }
                            }
                            break;

                        default: //Flag a record to be DELETED
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} -- Tag current record as DELETED from current list --- Act: {selectedRec.actTitle} Department: {selectedRec.department}  Portfolio: {selectedRec.portfolio}");
                            selectedRec.isDelete = "Hidden";

                            selectedRec.endDate = null;
                            selectedRec.pendingEditID = selectedRec.actAdministrationID; // 0;
                            selectedRec.pendingEditType = (short)RecordPendingEditType.Delete;
                            selectedRec.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(selectedRec.pendingEditType)}{selectedRec.actAdministrationID}";  //"D" + selectedRec.actAdministrationID;
                            selectedRec.foregroundColour = "Red";
                            selectedRec.backgroundColour = "Pink";
                            GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct(selectedRec);


                            //If Current record is to be deleted, then change the non current record to a pendingtypeid = 0. 
                            if (pendingRecordExists != null)
                            {
                                pendingRecordExists.endDate = null;
                                pendingRecordExists.pendingEditType = (short)RecordPendingEditType.None;
                                pendingRecordExists.pendingEditID = 0;
                                pendingRecordExists.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(selectedRec.pendingEditType)}"; //string.Empty;
                                pendingRecordExists.foregroundColour = string.Empty;
                                pendingRecordExists.backgroundColour = string.Empty;
                                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} -- Tag pending record as active --- Act: {pendingRecordExists.actTitle} Department: {pendingRecordExists.department}  Portfolio: {pendingRecordExists.portfolio}");
                            }
                            break;
                    }
                }
            }
            else
            {
                //Check if a NON CURRENT record is deleted. The application does not remove the record from the list as then we cannot flag it as deleted in the DB.
                if (MessageBoxHelper.DisplayMessageBox("Delete", "Are you sure you want to delete this record?"))
                {

                    switch (selectedRec.pendingEditType) // selectedRec.change.Take(1).FirstOrDefault())
                    {
                        //THERE IS NO UNDELETE SINCE NON CURRENT RECORDS SHOULD BE REMOVED IMMEDIATLY
                        //case (short)RecordPendingEditType.Delete:

                        //    if (currentRec != null) //If the Non Current rec is deleted, then undelete the Current rec if exists
                        //    {
                        //        currentRec.endDate = null;
                        //        currentRec.pendingEditType = (short)RecordPendingEditType.Delete;
                        //        currentRec.pendingEditID = 0;
                        //        currentRec.change = $"{0}{1}", PendingEditTypeHelper.GetPendingEditTypeText(selectedRec.pendingEditType), string.Empty); //string.Empty;
                        //        currentRec.foregroundColour = "Red";
                        //        currentRec.backgroundColour = "Pink";
                        //        GOUpdateImportedFiles.GenOrderUpdateActAdministrationRecord(currentRec);
                        //    }
                        //    //  notCurrentAdminListBox.Remove(selectedRec); //Delete the copy
                        //    //Delete record in DB. Set the non Current record to Undelete
                        //    selectedRec.endDate = null;
                        //    selectedRec.pendingEditType = (short)RecordPendingEditType.None;
                        //    selectedRec.pendingEditID = 0;
                        //    selectedRec.change = $"{0}{1}", PendingEditTypeHelper.GetPendingEditTypeText(selectedRec.pendingEditType), string.Empty); //string.Empty;
                        //    selectedRec.foregroundColour = string.Empty;
                        //    selectedRec.backgroundColour = string.Empty;
                        //    GOUpdateImportedFiles.GenOrderUpdateActAdministrationRecord(selectedRec);
                        //    break;
                        default: //Flag a record to be DELETED

                            var currentRec = GOActAdminDetailsUpdateVM.instance.currentAdminListBox
                                        .Where(r => r.departmentPortfolioID == selectedRec.departmentPortfolioID
                                             && r.ildNumber == selectedRec.ildNumber).FirstOrDefault();  //Check to see if there is a record in current list meaning that it was copied
                            //Delete the actual record using endDate so theres always a way to delete a current record if theres one
                            selectedRec.endDate = DateTime.Now;
                            selectedRec.pendingEditID = currentRec != null ? currentRec.pendingEditID : 0;
                            selectedRec.pendingEditType = (short)RecordPendingEditType.Delete;
                            selectedRec.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(selectedRec.pendingEditType)}{ selectedRec.actAdministrationID}";  //"D" + selectedRec.actAdministrationID;
                            selectedRec.foregroundColour = string.Empty;  //"Red";
                            selectedRec.backgroundColour = string.Empty;  //"Pink";

                            if (GOUpdateDetailsImportedFiles.GODeletePendingActs(selectedRec.actAdministrationID))
                            {
                                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} -- Record deleted from pending list --- Act: {selectedRec.actTitle} Department: {selectedRec.department}  Portfolio: {selectedRec.portfolio}");


                                if (currentRec != null) //If the pending rec is deleted, then undelete the Current rec if exists
                                {
                                    currentRec.endDate = null;
                                    currentRec.pendingEditType = (short)RecordPendingEditType.None;
                                    currentRec.pendingEditID = 0;
                                    currentRec.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(selectedRec.pendingEditType)}"; //string.Empty;
                                    currentRec.foregroundColour = string.Empty;
                                    currentRec.backgroundColour = string.Empty;
                                    GOUpdateDetailsImportedFiles.GOUpdatePartialRecordAct(currentRec);
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} -- Tag pending current record as active --- Act: {currentRec.actTitle} Department: {currentRec.department}  Portfolio: {currentRec.portfolio}");
                                }

                            }
                            break;
                    }
                }

            }
            GetAllGORecordsByILD();

            TidyUI();

        }


        /*
          private void getDeleteSelectedRow(object selectedItem)
        {
            var res = selectedItem.GetType().GetProperty("DataContext");
            //Check if its the CURRENT list 
            if (selectedItem.ToString().Contains("OrigAdminDetails"))
            {
                OrigAdminDetails selectedRec = res.GetValue(selectedItem) as OrigAdminDetails;

                switch (selectedRec.pendingEditType) // selectedRec.change.Take(1).FirstOrDefault())
                {
                    //this will be from the noncurrent list box. Records are added as NEW or COPIES of the current rec
                    //case (short)RecordPendingEditType.Add: //'A':
                    //    int ind = Convert.ToInt16(selectedRec.change.Remove(0, 1));
                    //    selectedRec.pendingEditType = (short)RecordPendingEditType.None;
                    //    notCurrentAdminListBox[ind].change = selectedRec.actAdministrationID.ToString();
                    //    break;

                    case (short)RecordPendingEditType.Delete: // 'D': //Already has been deleted but revert back
                        selectedRec.isCurrent = true;
                        selectedRec.onHold = false;
                        selectedRec.endDate = null;
                        selectedRec.pendingEditType = (short)RecordPendingEditType.Add;
                        selectedRec.change = selectedRec.actAdministrationID.ToString();
                        //Reset the colours
                        selectedRec.foregroundColour = string.Empty;
                        selectedRec.backgroundColour = string.Empty;
                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"UNDELETED FROM CURRENT LIST --- Act: { e.Message} Department: {1} Portfolio: {2}", selectedRec.actTitle,
                             selectedRec.department, selectedRec.portfolio));
                        //See if there is a copied record and if it exists, change its OnHold status
                        var nonCurrentRecordSelected = notCurrentAdminListBox.Where(r => r.change.Remove(0, 1) == selectedRec.actAdministrationID.ToString()).ToList();
                        if (nonCurrentRecordSelected.Count > 0)
                            foreach (var ncrs in nonCurrentRecordSelected)
                            {
                                ncrs.onHold = false; // true;
                            }
                        break;

                    default: //Flag a record to be DELETED
                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"DELETED FROM CURRENT LIST --- TAG RED  Act: { e.Message} Department: {1}  Portfolio: {2}", selectedRec.actTitle,
                             selectedRec.department, selectedRec.portfolio));
                        selectedRec.isCurrent = false;
                        selectedRec.onHold = false; // true;
                        selectedRec.endDate = DateTime.Now;
                        selectedRec.pendingEditType = (short)RecordPendingEditType.Delete;
                        selectedRec.change = "D" + selectedRec.actAdministrationID;
                        selectedRec.foregroundColour = "Red";
                        selectedRec.backgroundColour = "Pink";
                        break;
                }

            }
            else
            {
                //Check if a NON CURRENT record is deleted. The application does not remove the record from the list as then we cannot flag it as deleted in the DB.
                NewAdminDetails selectedRec = res.GetValue(selectedItem) as NewAdminDetails;

                if (selectedRec.actAdministrationID == 0)  //its a newly created record and has no ties to any other records
                {
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"DELETED FROM NON CURRENT LIST --- Act: { e.Message} Department: {1}  Portfolio: {2}", selectedRec.actTitle,
                               selectedRec.department, selectedRec.portfolio));
                    var currentRec = currentAdminListBox.Where(r => r.actAdministrationID == selectedRec.pendingEditID).FirstOrDefault(); //Check to see if there is a record in current list meaning that it was copied
                    if (currentRec != null) //Reset the copied CURRENT record before deleting the non Current copy
                    {
                        currentRec.endDate = null;
                        currentRec.pendingEditType = (short)RecordPendingEditType.None;
                        currentRec.foregroundColour = string.Empty;
                        currentRec.backgroundColour = string.Empty;
                    }
                    notCurrentAdminListBox.Remove(selectedRec); //Delete the copy
                }
                else
                {

                    switch (selectedRec.pendingEditType) // selectedRec.change.Take(1).FirstOrDefault())
                    {
                        case (short)RecordPendingEditType.Delete: // 'D': //Already has been deleted but revert back
                            selectedRec.endDate = null;
                            selectedRec.pendingEditType = (short)RecordPendingEditType.None;
                            selectedRec.foregroundColour = string.Empty;
                            selectedRec.backgroundColour = string.Empty;
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"UNDELETED FROM NON CURRENT LIST --- Act: { e.Message} Department: {1}  Portfolio: {2}", selectedRec.actTitle,
                              selectedRec.department, selectedRec.portfolio));
                            break;

                        default:  //Flag a record to be deleted.      
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"DELETED FROM NON CURRENT LIST --- TAG RED Act: { e.Message} Department: {1}  Portfolio: {2}", selectedRec.actTitle,
                               selectedRec.department, selectedRec.portfolio));
                            selectedRec.endDate = DateTime.Now;
                            selectedRec.pendingEditType = (short)RecordPendingEditType.Delete;
                            selectedRec.change = "D" + selectedRec.actAdministrationID;
                            selectedRec.foregroundColour = "Red";
                            selectedRec.backgroundColour = "Pink";
                            break;
                    }


                    //Check if theres only one notCurrent rec of a current one left : There could be multiple onhold copies of one Current record
                    var recordSelected = notCurrentAdminListBox.Where(r => r.actAdministrationID == selectedRec.pendingEditID && r != selectedRec).FirstOrDefault();

                    if (notCurrentAdminListBox.Where(r => r.pendingEditID == selectedRec.pendingEditID).Count() == 0)
                    {
                        //Check if there is a record associated with it in Current list
                        if (recordSelected != null)
                        {
                            recordSelected.isCurrent = true;
                            recordSelected.onHold = false;
                            recordSelected.endDate = null;
                            recordSelected.foregroundColour = string.Empty;
                            recordSelected.backgroundColour = string.Empty;
                            recordSelected.change = recordSelected.actAdministrationID.ToString();
                            recordSelected.pendingEditType = (short)RecordPendingEditType.None;
                            recordSelected.pendingEditID = 0;
                        }
                    }
                    else
                    {
                        //If there is more than one copy of the Current List record, then check to see if any of them are NOT onHold. If there are none, then remove the "D" from the current list rec
                        var data = notCurrentAdminListBox.Where(r => r.pendingEditID == selectedRec.pendingEditID).Select(r => r.onHold == false);
                        if (data == null)
                        {
                            recordSelected.isCurrent = true;
                            recordSelected.onHold = false;
                            recordSelected.endDate = null;
                            recordSelected.foregroundColour = string.Empty;
                            recordSelected.backgroundColour = string.Empty;
                            recordSelected.change = recordSelected.actAdministrationID.ToString();
                            recordSelected.pendingEditType = (short)RecordPendingEditType.None;
                            recordSelected.pendingEditID = 0;
                        }
                    }
                }
            }

            TidyUILists();
        }
        */

        /// <summary>
        /// The Combo box UI color is rearraged as items are added and removed from the lists
        /// It will loop both current and non current list box and apply the foreground and background colours
        /// </summary>
        public void TidyUI()
        {
            int i = 0;
            if (GOActAdminDetailsUpdateVM.instance.currentAdminListBox != null)
            {
                foreach (var res in GOActAdminDetailsUpdateVM.instance.currentAdminListBox)
                {
                    i++;
                    if (res.foregroundColour != "Red")
                    {
                        res.backgroundColour = i % 2 != 0 ? "#eaf0fb" : "White";
                        res.foregroundColour = "#4682B4"; // i % 2 != 0 ? "White" : "#4682B4";
                    }
                }
            }

            if (GOActAdminDetailsUpdateVM.instance.pendingAdminListBox != null)
            {
                foreach (var res in GOActAdminDetailsUpdateVM.instance.pendingAdminListBox)
                {
                    i++;
                    if (res.foregroundColour != "Red")
                    {
                        res.backgroundColour = i % 2 != 0 ? "#ebfbea" : "White";
                        res.foregroundColour = "#008080"; //i % 2 != 0 ? "White" : "#008080";
                    }
                }
            }
        }


        /// <summary>
        /// Get the selected record and populate the UI Current and Non Current ListBoxes with appropriate UI settings
        /// </summary>
        /// <param name="selectedRecord"></param>
        private void GOActAdminDetailsUpdate(List<ActAdministrationViewModel> selectedRecord)
        {
            //Get the data and put into memory as a DB model
            List<ActAdministrationViewModel> actAdministrationViewModelListCurrent = new List<ActAdministrationViewModel>();
            List<ActAdministrationViewModel> actAdministrationViewModelListPending = new List<ActAdministrationViewModel>();

            ActAdministrationViewModel actAdministrationViewModel = new ActAdministrationViewModel();

            if (selectedRecord != null)
            {
                actAdministrationViewModelListCurrent = selectedRecord.Where(r => r.IsCurrent == true).ToList(); // AllRecordsList.Where(r => r.IsCurrent == true).ToList();
                if (GOActAdminDetailsUpdateVM.instance.currentAdminListBox == null)
                    GOActAdminDetailsUpdateVM.instance.currentAdminListBox = new ObservableCollection<OrigAdminDetails>();
                else
                    GOActAdminDetailsUpdateVM.instance.currentAdminListBox.Clear();

                foreach (var actAdm in actAdministrationViewModelListCurrent)
                {
                    List<PortfolioInfoViewModel> port = GetDepartments.GetDepartmentInformation(actAdm.PortfolioID); //GetPortfolios.GetPortfolioInformation(actAdm.PortfolioID);

                    OrigAdminDetails origAdminDetails = new OrigAdminDetails();
                    origAdminDetails.generalFieldsCommentViewModel = new ObservableCollection<GeneralFieldsCommentViewModel>();


                    origAdminDetails.actAdministrationID = actAdm.ActAdministrationID;
                    origAdminDetails.ildNumber = actAdm.ILDNumber;
                    origAdminDetails.portfolioID = actAdm.PortfolioID;
                    origAdminDetails.isCurrent = actAdm.IsCurrent;
                    origAdminDetails.pendingEditID = actAdm.PendingEditID;
                    origAdminDetails.isExcept = actAdm.IsExcept;
                    origAdminDetails.pendingEditType = actAdm.PendingEditType;
                    origAdminDetails.startDate = actAdm.StartDate;
                    origAdminDetails.endDate = actAdm.EndDate;
                    origAdminDetails.department = port[0].Department;
                    origAdminDetails.deptCode = port[0].DeptCode;
                    origAdminDetails.actTitle = cBSelectedActValue.ActTitle; // actList[actSelectedIndex].ActTitle;
                    origAdminDetails.portfolio = actAdm.IsExcept ? port[0].PortfolioName + " - Except:" : (actAdm.ActCommentModel.Count > 0 ? port[0].PortfolioName + " -" : port[0].PortfolioName);
                    origAdminDetails.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(actAdm.PendingEditType)}{(actAdm.PendingEditType == (short)RecordPendingEditType.Delete ? actAdm.ActAdministrationID.ToString() : string.Empty)}"; // string.Empty; //ie A1, M1
                    origAdminDetails.foregroundColour = origAdminDetails.pendingEditType == 3 ? "Red" : string.Empty;
                    origAdminDetails.backgroundColour = origAdminDetails.pendingEditType == 3 ? "Pink" : string.Empty;
                    origAdminDetails.departmentPortfolioID = port[0].DepartmentPortfolioId;
                    origAdminDetails.isDelete = origAdminDetails.pendingEditType == 3 ? "Hidden" : "Visible";

                    foreach (var com in actAdm.ActCommentModel)
                    {
                        GeneralFieldsCommentViewModel generalFieldsCommentViewModel = new GeneralFieldsCommentViewModel();
                        generalFieldsCommentViewModel.actAdministrationId = actAdm.ActAdministrationID;
                        generalFieldsCommentViewModel.actAdministrationComment = com.ActAdministrationComment;
                        generalFieldsCommentViewModel.actAdminCommentId = com.ActAdminCommentId;
                        generalFieldsCommentViewModel.fontBold = com.FontBold;
                        generalFieldsCommentViewModel.bulletChar = com.BulletChar;
                        generalFieldsCommentViewModel.bulletASCII = com.BulletASCII;
                        generalFieldsCommentViewModel.pageBreakBefore = com.PageBreakBefore;
                        generalFieldsCommentViewModel.listSymbolFont = com.ListSymbolFont;
                        generalFieldsCommentViewModel.indentationLeft = com.IndentationLeft;
                        generalFieldsCommentViewModel.indentationRight = com.IndentationRight;
                        generalFieldsCommentViewModel.indentationBy = com.IndentationBy;
                        generalFieldsCommentViewModel.tabHangingIndent = com.TabHangingIndent;
                        generalFieldsCommentViewModel.tabStopPosition = com.TabStopPosition;
                        generalFieldsCommentViewModel.alignmentType = com.AlignmentType;

                        origAdminDetails.generalFieldsCommentViewModel.Add(generalFieldsCommentViewModel);
                    }
                    origAdminDetails.flowDocument = SelectActContents(origAdminDetails.generalFieldsCommentViewModel);

                    GOActAdminDetailsUpdateVM.instance.currentAdminListBox.Add(origAdminDetails);
                }


                actAdministrationViewModelListPending = selectedRecord.Where(r => r.IsCurrent == false).ToList(); //AllRecordsList.Where(r => r.IsCurrent == false).ToList();
                if (GOActAdminDetailsUpdateVM.instance.pendingAdminListBox == null)
                    GOActAdminDetailsUpdateVM.instance.pendingAdminListBox = new ObservableCollection<NewAdminDetails>();
                else
                    GOActAdminDetailsUpdateVM.instance.pendingAdminListBox.Clear();

                foreach (var actAdm in actAdministrationViewModelListPending)
                {
                    List<PortfolioInfoViewModel> port = GetDepartments.GetDepartmentInformation(actAdm.PortfolioID); //GetPortfolios.GetPortfolioInformation(actAdm.PortfolioID);
                                                                                                                     //ActTitleViewModel act = GetActTitles.GetActInformation(actAdm.ILDNumber);

                    NewAdminDetails newAdminDetails = new NewAdminDetails();
                    newAdminDetails.generalFieldsCommentViewModel = new ObservableCollection<GeneralFieldsCommentViewModel>();


                    newAdminDetails.actAdministrationID = actAdm.ActAdministrationID;
                    newAdminDetails.ildNumber = actAdm.ILDNumber;
                    newAdminDetails.portfolioID = actAdm.PortfolioID;
                    newAdminDetails.isCurrent = actAdm.IsCurrent;
                    newAdminDetails.pendingEditID = actAdm.PendingEditID;
                    newAdminDetails.isExcept = actAdm.IsExcept;
                    newAdminDetails.pendingEditType = actAdm.PendingEditType;
                    newAdminDetails.startDate = actAdm.StartDate;
                    newAdminDetails.endDate = actAdm.EndDate;
                    newAdminDetails.department = port[0].Department;
                    newAdminDetails.deptCode = port[0].DeptCode;
                    newAdminDetails.actTitle = cBSelectedActValue.ActTitle; //actList[actSelectedIndex].ActTitle;  //act.ActTitle;
                    newAdminDetails.portfolio = actAdm.IsExcept ? port[0].PortfolioName + " - Except:" : (actAdm.ActCommentModel.Count > 0 ? port[0].PortfolioName + " -" : port[0].PortfolioName);
                    newAdminDetails.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(actAdm.PendingEditType)}{ actAdm.PendingEditID}";  //  currRecordExists != null ? "A" : string.Empty;
                    newAdminDetails.foregroundColour = newAdminDetails.pendingEditType == 3 ? "Red" : string.Empty;
                    newAdminDetails.backgroundColour = newAdminDetails.pendingEditType == 3 ? "Pink" : string.Empty;
                    newAdminDetails.departmentPortfolioID = port[0].DepartmentPortfolioId;


                    foreach (var com in actAdm.ActCommentModel)
                    {
                        GeneralFieldsCommentViewModel generalFieldsCommentViewModel = new GeneralFieldsCommentViewModel();
                        generalFieldsCommentViewModel.actAdministrationId = actAdm.ActAdministrationID;
                        generalFieldsCommentViewModel.actAdministrationComment = com.ActAdministrationComment;
                        generalFieldsCommentViewModel.actAdminCommentId = com.ActAdminCommentId;
                        generalFieldsCommentViewModel.fontBold = com.FontBold;
                        generalFieldsCommentViewModel.bulletChar = com.BulletChar;
                        generalFieldsCommentViewModel.bulletASCII = com.BulletASCII;
                        generalFieldsCommentViewModel.pageBreakBefore = com.PageBreakBefore;
                        generalFieldsCommentViewModel.listSymbolFont = com.ListSymbolFont;
                        generalFieldsCommentViewModel.indentationLeft = com.IndentationLeft;
                        generalFieldsCommentViewModel.indentationRight = com.IndentationRight;
                        generalFieldsCommentViewModel.indentationBy = com.IndentationBy;
                        generalFieldsCommentViewModel.tabHangingIndent = com.TabHangingIndent;
                        generalFieldsCommentViewModel.tabStopPosition = com.TabStopPosition;
                        generalFieldsCommentViewModel.alignmentType = com.AlignmentType;

                        newAdminDetails.generalFieldsCommentViewModel.Add(generalFieldsCommentViewModel);
                    }
                    newAdminDetails.flowDocument = SelectActContents(newAdminDetails.generalFieldsCommentViewModel);

                    GOActAdminDetailsUpdateVM.instance.pendingAdminListBox.Add(newAdminDetails);

                }
            }
        }


        /// <summary>
        /// The comments are sent to the richtextbox helper function to apply the template
        /// </summary>
        /// <param name="recordSelected"></param>
        /// <returns></returns>
        private FlowDocument SelectActContents(ObservableCollection<GeneralFieldsCommentViewModel> recordSelected)
        {
            flowDoc = new FlowDocument();

            foreach (var rec in recordSelected)
            {
                var par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, rec.actAdministrationComment, UIFontSize);
                RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, rec.bulletASCII, rec.indentationLeft, UIFontSize);
            }
            return flowDoc;
        }


        #region wiki
        /// <summary>
        /// Wiki Information
        /// </summary>
        /// <param name="obj"></param>
        private void SelectGetAddNewBtnWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "AddNewBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetCancelBtnWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "CancelBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetBackBtnWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "BackBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetUploadThisActRBWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "UploadThisActRB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetUploadAllActsRBWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "UploadAllActsRB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetUploadBtnWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "UploadBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetOnHoldNORBWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "OnHoldNORB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetOnHoldYESRBWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "OnHoldYESRB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetPrintAllRBWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "PrintAllRB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetPrintThisActRBWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "PrintThisActRB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetPrintNewRBWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "PrintNewRB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetPrintCurrentRBWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "PrintCurrentRB").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetPrintBtnWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "PrintBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetApplicationWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ScreenHelpText);
        }
        #endregion
    }

}
