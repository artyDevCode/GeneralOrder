using ApplicationLogger;
using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;


namespace GeneralOrder
{

    /// <summary>
    /// Class for importing General Orders
    /// </summary>
    public class GOImportFileVM : BaseViewModel
    {
        #region commands
        public ICommand getFilePathWikiInformation { get; set; }
        public ICommand getDiscardFileWikiInformation { get; set; }
        public ICommand getImportFileWikiInformation { get; set; }
        public ICommand getSelectedDateWikiInformation { get; set; }
        public ICommand getFullGOWikiInformation { get; set; }
        public ICommand getPartialGOWikiInformation { get; set; }
        public ICommand getCloseWikiInformation { get; set; }
        public ICommand getFileWikiInformation { get; set; }
        public ICommand btnSelectFile { get; set; }
        public ICommand btnImportViewFile { get; set; }
        public ICommand btnDiscardIncompleted { get; set; }
        public ICommand btnClose { get; set; }
        public ICommand partialRadioButtonClick { get; set; }
        public ICommand getApplicationWikiInformation { get; set; }

        public ICommand getSelectFileInput { get; set; }
        //  public ICommand datePickerTextChanged { get; set; }
        #endregion

        #region properties
        public ScreenHelpViewModel helperResults { get; set; }

        private string _btnContentImportViewFile { get; set; } = "Import File";
        public string btnContentImportViewFile
        {
            get { return _btnContentImportViewFile; }
            set
            {
                _btnContentImportViewFile = value;
                OnPropertyChanged("btnContentImportViewFile");
            }
        }

        private bool _verifyFileSelected { get; set; } = true;
        public bool verifyFileSelected
        {
            get { return _verifyFileSelected; }
            set
            {
                _verifyFileSelected = value;
                OnPropertyChanged("verifyFileSelected");
            }
        }

        private string _generalOrderFileName { get; set; } = string.Empty;
        public string generalOrderFileName
        {
            get { return _generalOrderFileName; }
            set
            {
                _generalOrderFileName = value;
                OnPropertyChanged("generalOrderFileName");
            }
        }
        private string _radioFullPartial { get; set; } = "1";
        public string radioFullPartial
        {
            get { return _radioFullPartial; }
            set
            {
                _radioFullPartial = value;
                OnPropertyChanged("radioFullPartial");
            }
        }
        private bool _enablePartialRadioButton { get; set; }
        public bool enablePartialRadioButton
        {
            get { return _enablePartialRadioButton; }
            set
            {
                _enablePartialRadioButton = value;
                OnPropertyChanged("enablePartialRadioButton");
            }
        }
        private bool _enableDiscardIncompletedFile { get; set; }
        public bool enableDiscardIncompletedFile
        {
            get { return _enableDiscardIncompletedFile; }
            set
            {
                _enableDiscardIncompletedFile = value;
                OnPropertyChanged("enableDiscardIncompletedFile");
            }
        }
        private bool _enableFileSelect { get; set; }
        public bool enableFileSelect
        {
            get { return _enableFileSelect; }
            set
            {
                _enableFileSelect = value;
                OnPropertyChanged("enableFileSelect");
            }
        }
        private bool _enableFullRadioButton { get; set; }
        public bool enableFullRadioButton
        {
            get { return _enableFullRadioButton; }
            set
            {
                _enableFullRadioButton = value;
                OnPropertyChanged("enableFullRadioButton");
            }
        }
        private bool _spinnerBool { get; set; }
        public bool spinnerBool
        {
            get { return _spinnerBool; }
            set
            {
                _spinnerBool = value;
                OnPropertyChanged("spinnerBool");
            }
        }
        private FlowDocument _wikiFlowDoc { get; set; }
        public FlowDocument wikiFlowDoc
        {
            get { return _wikiFlowDoc; }
            set
            {
                if (value != _wikiFlowDoc)
                {
                    _wikiFlowDoc = value;
                    OnPropertyChanged("wikiFlowDoc");
                }
            }
        }
      
        /// <summary>
        /// A flag indicating if the application is processing a long running method such as importing a file
        /// </summary>
        private bool _importFileIsRunning { get; set; }
        public bool importFileIsRunning
        {
            get { return _importFileIsRunning; }
            set
            {
                if (value != _importFileIsRunning)
                {
                    _importFileIsRunning = value;
                    OnPropertyChanged("importFileIsRunning");
                }
            }
        }
        /// <summary>
        /// Effective Date picker
        /// </summary>
        private DateTime _GOFileInputDatePicker { get; set; } = DateTime.Now;
        public DateTime GOFileInputDatePicker
        {
            get { return _GOFileInputDatePicker; }
            set
            {
                if (value != _GOFileInputDatePicker)
                {
                    _GOFileInputDatePicker = value;
                    OnPropertyChanged("GOFileInputDatePicker");
                    SelectGetSelectedDate();
                }
            }
        }

        private GeneralOrderImportHeader res = null;
        #endregion\
        #region UI
        public int UIFontSize { get; set; } = WindowViewModel.instance.UIFontSize;
        public int UIWidth { get; set; } = WindowViewModel.instance.UIWidth;
        public int UIHeight { get; set; } = WindowViewModel.instance.GOImportFileHeight;
        public string generalOrderMargin { get; set; } = WindowViewModel.instance.generalOrderMargin;
        public string generalOrderPadding { get; set; } = WindowViewModel.instance.generalOrderPadding;
        #endregion

       /// <summary>
       /// File picker to display the filename in the logger
       /// </summary>
        public void SelectFileExec()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
         
            if ((bool)dialog.ShowDialog())
            {
                generalOrderFileName = dialog.FileName;  //dialog.SafeFileName; 
                                                         // generalOrderFileNamePath = dialog.FileName;
                                                         //Write to the application logger window
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Selected file:  {dialog.FileName}");
                VerifyFileInput(); 
            }
        }

        /// <summary>
        /// This method will determine if the application already has imported a General Order Document. If it has an existing imported document, it will alow the user to view or discard the file. 
        /// If it does not have a file already uploaded, it will let the user select a file to import.
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
        /// <returns></returns>
        public async void ImportViewFileExec()
        {
            if (DatePickerHelper.isDateValid) //Verify the date field is valid
            {
                if (enableFileSelect) //If there is no current document already imported
                {
                    if (VerifyFileInput()) //There has to be a file selected
                    {
                        if (MessageBoxHelper.DisplayMessageBox($"Verify", $"Are you sure you want to import this file? \n{generalOrderFileName} \nEffective Date: {GOFileInputDatePicker} \nFile Type: {(radioFullPartial == "1" ? "Full" : "Partial")}"))
                        {
                            //Write to the application logger window
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Start importing File Name:  {generalOrderFileName}  Effective Date: {GOFileInputDatePicker} , File Type: {(radioFullPartial == "1" ? "Full" : "Partial")}");
                            //Writing logger information into the Database
                            WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                            {
                                AdditionalUserInfo = "GOImportFile",
                                LogDetail = $"File Name:  { generalOrderFileName}  Effective Date: {GOFileInputDatePicker} , File Type: {(radioFullPartial == "1" ? "Full" : "Partial")}",
                                LogDetail_Additional = "Records loaded are current records where EndDate is Null. ",
                                LogTime = DateTime.Now,
                                UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                                Workstation = System.Environment.MachineName
                            }, "GeneralOrder", false);

                            //DatabaseEntity.GeneralOrderMetadataViewModel data = null;
                            //Run async
                            await RunCommandAsync(() => importFileIsRunning, async () =>
                            {
                                spinnerBool = importFileIsRunning;
                                //read the document and return an object of its contents
                                var retData = await System.Threading.Tasks.Task.Run(() => DocReader.WordDocReader(generalOrderFileName));

                                //Reruns a Tuple, 1st item will contain a flag to determine if the document imported has errors
                                if (!retData.Item1) //If the read document has no errors then proceed to upload
                                {
                                    //Convert the returned object to a JSON string
                                    var resMetaResult = Newtonsoft.Json.JsonConvert.SerializeObject(retData.Item2);
                                    //Send the JSON object to DLL to import into DB, flag set to FULL or PARTIAL and the Effective Date
                                    var passed = await System.Threading.Tasks.Task.Run(() => GeneralOrderImportHeaderDC.GenOrderImport(resMetaResult, Convert.ToBoolean(radioFullPartial == "1"), GOFileInputDatePicker));
                                    //Once uploaded, set the UI to view data
                                    enableDiscardIncompletedFile = true;
                                    enableFileSelect = false;
                                    btnContentImportViewFile = "View File";
                                    enableFullRadioButton = false;
                                    enablePartialRadioButton = false;
                                    //Write to the application logger window
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Finished importing File Name:  {generalOrderFileName}  Effective Date: {GOFileInputDatePicker} , File Type: {(radioFullPartial == "1" ? "Full" : "Partial")}");
                                }
                                else
                                {
                                    //The file read had errors, go back to the main screen so the user can select another file of exit to fix the errors in the selected file.
                                    btnContentImportViewFile = "Import File";
                                    enablePartialRadioButton = true;
                                    enableFullRadioButton = true;
                                    enableDiscardIncompletedFile = false;
                                    enableFileSelect = true;
                                    //Write to the application logger window
                                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Error with importing the General Order document or the document doesnt exist");
                                }
                            });
                            spinnerBool = importFileIsRunning;
                        }
                    }                  
                }
                else
                {
                    //this gets the instance of the main window and opens the Add Details upload page
                    //Write to the application logger window
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- View current file Name:  {generalOrderFileName}  Effective Date: {GOFileInputDatePicker} , File Type: {(radioFullPartial == "1" ? "Full" : "Partial")}");
                    await RunCommandAsync(() => importFileIsRunning, async () =>
                    {
                        spinnerBool = importFileIsRunning;
                        //get the already imported file from the database
                        await System.Threading.Tasks.Task.Run(() => WindowViewModel.instance.resMeta = GetGeneralOrder.GetGeneralOrderDoc(true));

                        if (WindowViewModel.instance.resMeta != null)
                            WindowViewModel.instance.CurrentPage = ApplicationPage.goFileDetailsUpload;
                        else
                            //Write to the application logger window
                            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- No current records found");
                    });
                    spinnerBool = importFileIsRunning;
                }
            }
            else
            {
                MessageBoxHelper.CatchExceptionMessageBox("Effective Date is incorrect");            
            }
        }


        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
        /// <returns></returns>
        public void CloseExec()
        {
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- Closed....");
            WindowViewModel.instance.WindowClose(); //Window global method.
        }


        /// <summary>
        /// Loads the records from the database where processed datetime == null. (current reocrds) 
        /// </summary>
        public async void startLoad()
        {
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- Start loading Database records");
          

            spinnerBool = importFileIsRunning;
            //gets the records from the Database
            await RunCommandAsync(() => importFileIsRunning, async () =>
           {
               res = await System.Threading.Tasks.Task.Run(() => GetGeneralOrder.CheckGeneralOrderProcessedDate());
           });

            //Populates the UI with the selected record information
            if (res != null)
            {
                //Write to the application logger window
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{ DateTime.Now} --- Loaded current record - { res.GeneralOrderImportFile}");
                radioFullPartial = res.GeneralOrderFull ? "1" : "2";
                //  generalOrderFileNamePath = res.GeneralOrderImportFile;
                GOFileInputDatePicker = res.GeneralOrderEffectiveDate;
                generalOrderFileName = res.GeneralOrderImportFile;
            }
            else
                //Write to the application logger window
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- No records found");

            spinnerBool = false; // importFileIsRunning;
            //Changes the UI based on the record details
            toggeleUI();
        }

        //This modifies the UI based on the Radio Button that is checked or the values that it receives from the Dartabase
        private void toggeleUI()
        {
            if (res != null)
            {
                btnContentImportViewFile = "View File";
                enablePartialRadioButton = false;
                enableFullRadioButton = false;
                enableDiscardIncompletedFile = true;
                enableFileSelect = false;
            }
            else
            {
                btnContentImportViewFile = "Import File";
                enablePartialRadioButton = true;
                enableFullRadioButton = true;
                enableDiscardIncompletedFile = false;
                enableFileSelect = true;
            }

        }


        /// <summary>
        /// Constructor.
        /// Set the Relay commands
        /// </summary>
        public GOImportFileVM()
        {

            wikiFlowDoc = new FlowDocument();

            #region relaycommands
            btnDiscardIncompleted = new RelayCommand(SelectBtnDiscardIncompleted);
            getSelectFileInput = new RelayCommand(SelectGetSelectFileInput);
            //  radioFullPartial = "1";
            getFilePathWikiInformation = new RelayCommand(SelectGetFilePathWikiInformation);
            getFileWikiInformation = new RelayCommand(SelectFileGetWikiInformation);
            btnSelectFile = new RelayCommand(SelectFileExec);
            btnImportViewFile = new RelayCommand(ImportViewFileExec);
            btnClose = new RelayCommand(CloseExec);
            getFullGOWikiInformation = new RelayCommand(SelectGetFullGOWikiInformation);
            getSelectedDateWikiInformation = new RelayCommand(SelectGetSelectedDateWikiInformation);
            getPartialGOWikiInformation = new RelayCommand(SelectGetPartialGOWikiInformation);
            getCloseWikiInformation = new RelayCommand(SelectGetCloseWikiInformation);
            getDiscardFileWikiInformation = new RelayCommand(SelectGetDiscardFileWikiInformation);
            getImportFileWikiInformation = new RelayCommand(SelectedGetImportFileWikiInformation);
            partialRadioButtonClick = new RelayCommand(SelectPartialRadioButtonClick);
            getApplicationWikiInformation = new RelayCommand(SelectGetApplicationWikiInformation);
            #endregion

            helperResults = DatabaseEntity.ApplicationPage.goImportFile.GetHelpInfo();
            if (helperResults == null)
            {
                helperResults = new ScreenHelpViewModel();
                helperResults.ControlHelperVM = new List<ControlHelperViewModel>();
            }
            startLoad();
        }

        private void SelectGetSelectFileInput()
        {            
            VerifyFileInput();
        }

        /// <summary>
        /// This willcheck to see if the selected file exists. This occurs when a user enters the path and file name instead of using the file picker
        /// </summary>
        /// <returns></returns>
        private bool VerifyFileInput()
        {
            if (generalOrderFileName != string.Empty)
            {
                if (!File.Exists(generalOrderFileName))
                {
                    MessageBoxHelper.InfoMessageBox("File not found");
                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- File not found {generalOrderFileName}");
                    verifyFileSelected = false;
                    return false;
                }
                else
                {
                    verifyFileSelected = true;
                    return true;
                    //switch (Path.GetExtension(generalOrderFileName))
                    //{
                    //    case ".doc":
                    //    case ".docx":
                    //        verifyFileSelected = true;
                    //        return true;
                    //    default:
                    //        MessageBoxHelper.InfoMessageBox("Incorrect file type. Please select doc or docx");
                    //        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- Incorrect file type {1}", DateTime.Now, Path.GetExtension(generalOrderFileName)));
                    //        verifyFileSelected = false;
                    //        return false;
                    //}
                }
            }
            else
            {
                verifyFileSelected = false;
                return false;
            }
        }

        /// <summary>
        /// When the user click the discard button, it will remove the record from the database
        /// </summary>
        private async void SelectBtnDiscardIncompleted()
        {
            if (MessageBoxHelper.DisplayMessageBox("Warning", "Are you sure you want to discard this record?"))
            {
                //This will discard any files that have been imported but not yet uploaded to ActAdin
                //Write to the application logger window
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Discard process started  file Name:  {generalOrderFileName}  Effective Date: {GOFileInputDatePicker} , File Type: {(radioFullPartial == "1" ? "Full" : "Partial")}");
                await RunCommandAsync(() => importFileIsRunning, async () =>
                        {
                            spinnerBool = importFileIsRunning;
                            var passed = await System.Threading.Tasks.Task.Run(() => GeneralOrderImportHeaderDC.DiscardGenOrderImport());
                            if (passed)
                                res = null;
                        });
                spinnerBool = importFileIsRunning;
                //Write to the application logger window
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Discard process finished file Name:  {generalOrderFileName}  Effective Date: {GOFileInputDatePicker} , File Type: {(radioFullPartial == "1" ? "Full" : "Partial")}");
                generalOrderFileName = string.Empty;
                // generalOrderFileNamePath = string.Empty;
                GOFileInputDatePicker = DateTime.Now;
                toggeleUI();
                radioFullPartial = "1";
            }
        }

        /// <summary>
        /// When the user clicks on the DatePicker
        /// </summary>
        private void SelectGetSelectedDate()
        {
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Selected Date = {GOFileInputDatePicker}");
        }

        /// <summary>
        /// When a user check the Radio Button PARTIAL
        /// </summary>
        private void SelectPartialRadioButtonClick()
        {
            toggeleUI();
        }

        /// <summary>
        /// Wiki information displayed on hover of an object
        /// </summary>
        #region wiki
        private void SelectGetApplicationWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ScreenHelpText);
        }

        private void SelectGetCloseWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "CloseBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetDiscardFileWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "DiscardFileBtn").Select(r => r.ControlHelpText).FirstOrDefault());
            //"This will discard any information that is saved in the database.";
        }

        private void SelectedGetImportFileWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "ImportViewFileBtn").Select(r => r.ControlHelpText).FirstOrDefault());
            //"This will trigger the import of the selected General Order file";
        }

        private void SelectGetSelectedDateWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "effectiveDatePicker").Select(r => r.ControlHelpText).FirstOrDefault());
            //"Select the Effective Date of the General Order you have/will select";
        }

        private void SelectGetPartialGOWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "PartialGORadioBtn").Select(r => r.ControlHelpText).FirstOrDefault());
            //"This will let the system know that you are uploading a PARTIAL General Order Document";
        }

        private void SelectGetFullGOWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "FullGORadioBtn").Select(r => r.ControlHelpText).FirstOrDefault());
            //"This will let the system know that you are uploading a FULL General Order Document";
        }

        private void SelectGetFilePathWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "FilePath").Select(r => r.ControlHelpText).FirstOrDefault());
            //"Displays the File Path of the file you selected from you computer";
        }

        private void SelectFileGetWikiInformation()
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "FileSelectBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        #endregion

        /// <summary>
        /// This method will load the document data extracted from General Order docx and insert it into a template and save as a new docx.
        /// The reason we copy to a template is because the original docx from parliament website is unformated, without a template. This makes it very hard to parse to the WPF Richtexbox
        /// and maintain the style.
        /// </summary>
        /// <param name="data"></param>
        //public void pushDataIntoTemplate(DatabaseEntity.GeneralOrderMetadataViewModel data)
        //{
        //    Object oMissing = System.Reflection.Missing.Value;

        //    Object oTemplatePath = @"C:\Users\arty\Documents\Visual Studio 2017\Projects\General Order Projects\Word Docs\GeneralOrderTitle.docx";

        //    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
        //    Document wordDoc = new Document();

        //    wordDoc = wordApp.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

        //    foreach (Field myMergeField in wordDoc.Fields)
        //    {
        //        Range rngFieldCode = myMergeField.Code;

        //        String fieldText = rngFieldCode.Text;

        //        // ONLY GETTING THE MAILMERGE FIELDS

        //        if (fieldText.StartsWith(" MERGEFIELD"))
        //        {

        //            // THE TEXT COMES IN THE FORMAT OF

        //            // MERGEFIELD  MyFieldName  \\* MERGEFORMAT

        //            // THIS HAS TO BE EDITED TO GET ONLY THE FIELDNAME "MyFieldName"

        //            Int32 endMerge = fieldText.IndexOf("\\");

        //            Int32 fieldNameLength = fieldText.Length - endMerge;

        //            String fieldName = fieldText.Substring(11, endMerge - 11);

        //            // GIVES THE FIELDNAMES AS THE USER HAD ENTERED IN .dot FILE

        //            fieldName = fieldName.Trim();

        //            // **** FIELD REPLACEMENT IMPLEMENTATION GOES HERE ****//

        //            // THE PROGRAMMER CAN HAVE HIS OWN IMPLEMENTATIONS HERE

        //            if (fieldName == "AdministrationOfActsTitle")
        //            {

        //                myMergeField.Select();

        //                wordApp.Selection.TypeText("ADMINISTRATION OF ACTS");

        //            }
        //        }
        //    }
        //    wordDoc.SaveAs(@"C:\Users\arty\Documents\Visual Studio 2017\Projects\General Order Projects\GeneralOrder.docx");
        //    wordApp.Documents.Open(@"C:\Users\arty\Documents\Visual Studio 2017\Projects\General Order Projects\GeneralOrder.docx");
        //    wordApp.Application.Quit();
        //}
    }
}
