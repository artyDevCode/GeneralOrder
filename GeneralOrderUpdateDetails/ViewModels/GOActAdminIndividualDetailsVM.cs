using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace GeneralOrderUpdateDetails
{
    /// <summary>
    /// Edit individual selected records
    /// </summary>
    public class GOActAdminIndividualDetailsVM : BaseViewModel
    {
        #region properties
        private string _windowTitle { get; set; } = "Act Administration";
        public string windowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged("windowTitle");
            }
        }
        private string _actNumber { get; set; }
        public string actNumber
        {
            get { return _actNumber; }
            set
            {
                _actNumber = value;
                OnPropertyChanged("actNumber");
            }
        }
        private string _actTitle { get; set; }
        public string actTitle
        {
            get { return _actTitle; }
            set
            {
                _actTitle = value;
                OnPropertyChanged("actTitle");
            }
        }
        private string _actDate { get; set; }
        public string actDate
        {
            get { return _actDate; }
            set
            {
                _actDate = value;
                OnPropertyChanged("actDate");
            }
        }
        private string _deptCode { get; set; }
        public string deptCode
        {
            get { return _deptCode; }
            set
            {
                _deptCode = value;
                OnPropertyChanged("deptCode");
            }
        }
        private string _IsRecordCurrent { get; set; }
        public string IsRecordCurrent
        {
            get { return _IsRecordCurrent; }
            set
            {
                _IsRecordCurrent = value;
                OnPropertyChanged("IsRecordCurrent");
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
        private string _departmentTitle { get; set; }
        public string departmentTitle
        {
            get { return _departmentTitle; }
            set
            {
                _departmentTitle = value;
                OnPropertyChanged("departmentTitle");
            }
        }
        private DateTime _effectiveDatePicker { get; set; } = DateTime.Now;
        public DateTime effectiveDatePicker
        {
            get { return _effectiveDatePicker; }
            set
            {
                _effectiveDatePicker = value;
                OnPropertyChanged("effectiveDatePicker");
            }
        }
        private BindableRichTextBox _individualUploadRichTextBox { get; set; } //needed to add the binding in code behind as you cannot bind x:name
        public BindableRichTextBox individualUploadRichTextBox
        {
            get { return _individualUploadRichTextBox; }
            set
            {
                _individualUploadRichTextBox = value;
                OnPropertyChanged("individualUploadRichTextBox");
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


        private static GeneralFieldsViewlModel recordSelected { get; set; }
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
        private string previousAct { get; set; }
        private string previousPortfolio { get; set; }
        public int UIFontSize { get; set; } = WindowViewModel.instance.UIFontSize;
        public int UIWidth { get; set; } = WindowViewModel.instance.UIWidth;
        public int UIHeight { get; set; } = WindowViewModel.instance.GOActAdminIndividualDetailsHeight;
        //  public string generalOrderMargin { get; set; } = WindowViewModel.instance.generalOrderMargin;
        public string generalOrderPadding { get; set; } = WindowViewModel.instance.generalOrderPadding;
        public int actAdminIndividualDetailsRichTextBoxHeight { get; set; }
        #endregion


        #region commands
        public ICommand cbChecked { get; set; }
        public ICommand getApplicationWikiInformation { get; set; }
        public ICommand getActNumberWikiInformation { get; set; }
        public ICommand getPortfolioWikiInformation { get; set; }
        public ICommand getCommentsWikiInformation { get; set; }
        public ICommand getEffectiveDateWikiInformation { get; set; }

        public ICommand getBackAndSaveBtnWikiInformation { get; set; }
        public ICommand getCancelBtnWikiInformation { get; set; }
        public ICommand getSelectedDate { get; set; }


        public System.Windows.Documents.List list1;
        public ICommand portfolioCommand { get; set; }
        public ICommand btnCancel { get; set; }
        public ICommand btnBackAndSave { get; set; }
        public ICommand insertTextRTB { get; set; }
        public ICommand insertTextRTBblock { get; set; }
        public ICommand deleteTextRTBblock { get; set; }
        #endregion


        /// <summary>
        /// Conrtuctor
        /// </summary>
        public GOActAdminIndividualDetailsVM()
        {
            //Set the UI size/font properties
            actAdminIndividualDetailsRichTextBoxHeight = (UIHeight / 6) * 4;

            if (WindowViewModel.instance.isCurrentList)
                windowTitle = "Act Administration - Current record";
            else
                windowTitle = "Act Administration - Pending record";
            wikiFlowDoc = new FlowDocument();

            #region relays
            cbChecked = new RelayCommand(SelectCBChecked);
            btnBackAndSave = new RelayCommand(selectBtnBackAndSave);
            getSelectedDate = new RelayCommand(SelectGetSelectedDate);
            getApplicationWikiInformation = new RelayCommand(SelectGetApplicationWikiInformation);
            getActNumberWikiInformation = new RelayCommand(selectGetActNumberWikiInformation);
            getPortfolioWikiInformation = new RelayCommand(selectGetPortfolioWikiInformation);
            getCommentsWikiInformation = new RelayCommand(selectGetCommentsWikiInformation);
            getEffectiveDateWikiInformation = new RelayCommand(selectGetEffectiveDateWikiInformation);

            getBackAndSaveBtnWikiInformation = new RelayCommand(selectGetBackAndSaveBtnWikiInformation);
            getCancelBtnWikiInformation = new RelayCommand(selectGetCancelBtnWikiInformation);
            btnCancel = new RelayCommand(selectBtnCancel);
            portfolioCommand = new RelayCommand(selectPortfolioCommand);
            insertTextRTB = new RelayCommand(selectInsertTextRTB);
            insertTextRTBblock = new RelayCommand(SelectInsertTextRTBblock);
            deleteTextRTBblock = new RelayCommand(SelectDeleteTextRTBblock);
            #endregion 

            recordSelected = (WindowViewModel.instance.selectedRecord as GeneralFieldsViewlModel).CopyGeneralFields();
            IsRecordCurrent = recordSelected.isCurrent ? "Visible" : "Hidden";
            helperResults = DatabaseEntity.ApplicationPage.goActAdminIndividualDetails.GetHelpInfo();
            CheckBoxIsExcept = recordSelected.isExcept;
            CheckBoxIsExceptEnabled = BindableRichTextBox.UIHasChanged;

            if (recordSelected != null)
            {
                actNumber = recordSelected.ildNumber.ToString();
                actTitle = recordSelected.actTitle;
                effectiveDatePicker = recordSelected.startDate != null ? (DateTime)recordSelected.startDate : DateTime.Now;

                portfolioList = GetPortfolios.GetPortfolio();
                selectedPortfolioIndex = portfolioList.FindIndex(r => r.PortfolioName == recordSelected.portfolio);

                departmentTitle = recordSelected.department;
                deptCode = recordSelected.deptCode;
                actDate = recordSelected.startDate.ToString("dd/MM/yyyy");
                SelectActContents();
                previousPortfolio = recordSelected.portfolio;
                previousAct = recordSelected.actTitle;
            }
            BindableRichTextBox.UIFontSize = UIFontSize;

            //Set the default layout of the RTB
            var par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, "", UIFontSize);
            RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, 0, 1, UIFontSize);
        }

        /// <summary>
        /// Sets the ISExcept to true or false
        /// </summary>
        /// <param name="obj"></param>
        private void SelectCBChecked(object obj)
        {
            //Write to the application logger window
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{CheckBoxIsExcept} --- Is Except = {DateTime.Now}");
            actTitle = recordSelected.actTitle;
            recordSelected.isExcept = CheckBoxIsExcept;
            BindableRichTextBox.UIHasChanged = true;
        }

        /// <summary>
        /// Select the date in the UI
        /// </summary>
        /// <param name="obj"></param>
        private void SelectGetSelectedDate(object obj)
        {
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Selected Date: {effectiveDatePicker.Date}");
        }


        /// <summary>
        /// Select the portfolio in the UI
        /// </summary>
        /// <param name="obj"></param>
        private void selectPortfolioCommand(object obj)
        {
            var data = obj as PortfolioViewModel;
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Selected Portfolio: {data.PortfolioName} ");
            var port = GetPortfolios.GetPortfolioInformation(data.PortfolioId).FirstOrDefault();
            recordSelected.portfolioID = port.PortfolioId;
            recordSelected.portfolio = port.PortfolioName;
            recordSelected.departmentPortfolioID = port.DepartmentPortfolioId;
            recordSelected.department = port.Department;
            previousPortfolio = port.PortfolioName;
            BindableRichTextBox.UIHasChanged = true;
            //}
        }

        /// <summary>
        /// Cancel the edit
        /// </summary>
        /// <param name="obj"></param>
        private void selectBtnCancel(object obj)
        {
            if (PromptSaveRecToDB())
            {
                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Cancel pressed");
                WindowViewModel.instance.CurrentPage = ApplicationPage.goDetailsUpdate;
            }
        }



        /// <summary>
        /// Prompts the user to save the current record before displaying the newly selected record
        /// This can occur if the user cancels without saving changes
        /// </summary>
        private bool PromptSaveRecToDB()
        {
            if (BindableRichTextBox.UIHasChanged)
                if (MessageBoxHelper.DisplayMessageBox("Verify", "There has been changes made. Do you want to save changes?"))
                {
                    if (DatePickerHelper.isDateValid)
                    {
                        //Write to the application logger window
                        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Changes saved to Act {previousAct} and Porfolio {previousPortfolio}");
                        selectBtnBackAndSave();
                        BindableRichTextBox.UIHasChanged = false;
                    }
                    else
                    {
                        MessageBoxHelper.CatchExceptionMessageBox("Effective Date is incorrect");
                        return false;
                    }
                }
                else
                    BindableRichTextBox.UIHasChanged = false;
            return true;
        }


        /// <summary>
        /// Handle the saving of the record.
        /// </summary>
        private void selectBtnBackAndSave()
        {
            if (DatePickerHelper.isDateValid)
            {
                if (recordSelected.isCurrent)
                {
                    GeneralFieldsViewlModel record = null;
                    int returnVal = 0;
                    //prompt the user if the record to be saved has a pending record
                    var res = GOActAdminDetailsUpdateVM.instance.pendingAdminListBox //GOActAdminDetailsUpdateVM.pendingAdminListBox
                         .Where(r => r.ildNumber == recordSelected.ildNumber && r.isCurrent == false && r.departmentPortfolioID == recordSelected.departmentPortfolioID && r.endDate == null)
                        .FirstOrDefault();
                    if (res != null)
                    {
                        if (MessageBoxHelper.DisplayMessageBox("Warning", "There is an associated pending record. Click 'YES' to override pending record or click 'NO' to replace the current record?"))
                        {
                            //Update the existing Pending record
                            record = new GeneralFieldsViewlModel
                            {
                                actAdministrationID = res.actAdministrationID,
                                actTitle = recordSelected.actTitle,
                                pendingEditType = (short)RecordPendingEditType.Add,
                                change = string.Empty,
                                department = recordSelected.department,
                                deptCode = recordSelected.deptCode,
                                endDate = recordSelected.endDate,
                                flowDocument = flowDoc,
                                ildNumber = recordSelected.ildNumber,
                                isCurrent = false,
                                pendingEditID = recordSelected.pendingEditID,
                                isExcept = CheckBoxIsExcept,
                                portfolio = recordSelected.portfolio,
                                portfolioID = recordSelected.portfolioID,
                                departmentPortfolioID = recordSelected.departmentPortfolioID,
                                startDate = effectiveDatePicker.Date,
                                generalFieldsCommentViewModel = flowDoc.LoopThroughFlowDoc(res.actAdministrationID)

                            };
                            if (record.generalFieldsCommentViewModel.Count > 0)
                                if (record.generalFieldsCommentViewModel[0].actAdministrationComment != string.Empty)
                                {
                                    returnVal = GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct(record);
                                }
                        }
                        else
                        {
                            var currRecord = GOActAdminDetailsUpdateVM.instance.currentAdminListBox
                                .Where(r => r.actAdministrationID == recordSelected.actAdministrationID)
                                .FirstOrDefault();
                            if (currRecord != null)  //Set the record to archive
                            {
                                currRecord.isCurrent = false;
                                currRecord.endDate = DateTime.Now;
                                //if (currRecord.generalFieldsCommentViewModel.Count > 0)
                                //    if (currRecord.generalFieldsCommentViewModel[0].actAdministrationComment != string.Empty)
                                //    {
                                        returnVal = GOUpdateDetailsImportedFiles.GOUpdatePartialRecordAct(currRecord);
                                  //  }
                            }

                            //create a new current record
                            record = new GeneralFieldsViewlModel
                            {
                                actAdministrationID = 0,
                                actTitle = recordSelected.actTitle,
                                pendingEditType = (short)RecordPendingEditType.Delete,
                                change = string.Empty, //cnt,  
                                department = recordSelected.department,
                                deptCode = recordSelected.deptCode,
                                endDate = null,
                                flowDocument = flowDoc,
                                ildNumber = recordSelected.ildNumber,
                                isCurrent = true,
                                pendingEditID = returnVal,
                                isExcept = CheckBoxIsExcept,
                                portfolio = recordSelected.portfolio,
                                portfolioID = recordSelected.portfolioID,
                                departmentPortfolioID = recordSelected.departmentPortfolioID,
                                startDate = effectiveDatePicker.Date,
                                generalFieldsCommentViewModel = flowDoc.LoopThroughFlowDoc(0)

                            };
                            if (record.generalFieldsCommentViewModel.Count > 0)
                                if (record.generalFieldsCommentViewModel[0].actAdministrationComment != string.Empty)
                                {
                                    returnVal = GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct(record);
                               
                                }
                            //Modify the non-current pending record pendingEditID to reflect the new current record
                            res.pendingEditID = returnVal;
                            returnVal = GOUpdateDetailsImportedFiles.GOUpdatePartialRecordAct(res);
                        }
                    }
                    else
                    {

                        //Update the current record for audit as delete
                        var currRecord = GOActAdminDetailsUpdateVM.instance.currentAdminListBox
                            .Where(r => r.actAdministrationID == recordSelected.actAdministrationID).FirstOrDefault();
                        if (currRecord != null)
                        {
                            //currRecord.isCurrent = true;
                            //currRecord.endDate = null;
                            //currRecord.pendingEditType = (short)RecordPendingEditType.Delete;
                            //currRecord.foregroundColour = "Red";
                            //currRecord.backgroundColour = "Pink";

                            ////
                            currRecord.isCurrent = true;
                            currRecord.isExcept = CheckBoxIsExcept;
                            currRecord.endDate = null; // DateTime.Now;
                            currRecord.pendingEditID = currRecord.actAdministrationID; //0
                            currRecord.pendingEditType = (short)RecordPendingEditType.Delete;
                            currRecord.change = $"{PendingEditTypeHelper.GetPendingEditTypeText(currRecord.pendingEditType)}{currRecord.actAdministrationID}";  //"D";
                            currRecord.foregroundColour = "Red";
                            currRecord.backgroundColour = "Pink";
                            /////
                        }

                        //if (currRecord.generalFieldsCommentViewModel.Count > 0)
                        //    if (currRecord.generalFieldsCommentViewModel[0].actAdministrationComment != null)
                        //    {
                                returnVal = GOUpdateDetailsImportedFiles.GOUpdatePartialRecordAct(currRecord);
                         //   }

                        //create a new record in non current for audit porposes
                        record = new GeneralFieldsViewlModel
                        {
                            actAdministrationID = 0,
                            actTitle = recordSelected.actTitle,
                            pendingEditType = (short)RecordPendingEditType.Add,
                            change = string.Empty,
                            department = recordSelected.department,
                            deptCode = recordSelected.deptCode,
                            endDate = null,
                            flowDocument = flowDoc,
                            ildNumber = recordSelected.ildNumber,
                            isCurrent = false,
                            pendingEditID = recordSelected.actAdministrationID,
                            isExcept = CheckBoxIsExcept,
                            portfolio = recordSelected.portfolio,
                            portfolioID = recordSelected.portfolioID,
                            departmentPortfolioID = recordSelected.departmentPortfolioID,
                            startDate = effectiveDatePicker.Date,
                            generalFieldsCommentViewModel = flowDoc.LoopThroughFlowDoc(0)

                        };
                        if (record.generalFieldsCommentViewModel.Count > 0)
                            if (record.generalFieldsCommentViewModel[0].actAdministrationComment != string.Empty)
                            {
                                returnVal = GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct(record);
                            }
                    }
                }

                else
                {

                    NewAdminDetails recordsPendingAdminListBox = new NewAdminDetails
                    {
                        actAdministrationID = recordSelected.actAdministrationID,
                        actTitle = recordSelected.actTitle,
                        pendingEditType = (short)RecordPendingEditType.Add,
                        change = string.Empty,
                        department = recordSelected.department,
                        deptCode = recordSelected.deptCode,
                        endDate = recordSelected.endDate,
                        flowDocument = flowDoc,
                        ildNumber = recordSelected.ildNumber,
                        isCurrent = false,
                        pendingEditID = recordSelected.actAdministrationID,
                        isExcept = CheckBoxIsExcept,
                        portfolio = recordSelected.portfolio,
                        portfolioID = recordSelected.portfolioID,
                        departmentPortfolioID = recordSelected.departmentPortfolioID,
                        startDate = effectiveDatePicker.Date,
                        generalFieldsCommentViewModel = flowDoc.LoopThroughFlowDoc(recordSelected.actAdministrationID)
                    };

                    //Upload to database
                    if (recordsPendingAdminListBox.generalFieldsCommentViewModel.Count > 0)
                        if (recordsPendingAdminListBox.generalFieldsCommentViewModel[0].actAdministrationComment != string.Empty)
                        {
                            var ActID = GOUpdateDetailsImportedFiles.GOUpdateFullRecordAct(recordsPendingAdminListBox);
                            recordsPendingAdminListBox.actAdministrationID = recordsPendingAdminListBox.actAdministrationID == 0 ? ActID : recordsPendingAdminListBox.actAdministrationID;

                            GOActAdminDetailsUpdateVM.instance.pendingAdminListBox.Add(recordsPendingAdminListBox);
                        }
                }

                WindowViewModel.instance.CurrentPage = ApplicationPage.goDetailsUpdate;
            }
            else
                MessageBoxHelper.CatchExceptionMessageBox("Effective Date is incorrect");
        }


        /// <summary>
        /// Handle the rich textbox flow document
        /// </summary>
        private void SelectDeleteTextRTBblock()
        {
            //Get the contents of the flow doc
            TextRange tr = new TextRange(flowDoc.ContentStart, flowDoc.ContentEnd);
            CheckBoxIsExceptEnabled = tr.Text.Replace("\t\r\n", string.Empty).Length > 0 ? true : false;
            CheckBoxIsExcept = false;
        }

        /// <summary>
        /// User input in the richtextbox
        /// </summary>
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

            TextPointer caretPos = individualUploadRichTextBox.CaretPosition;
            Block flowBlock = individualUploadRichTextBox.Document.Blocks.Where(x => x.ContentStart.CompareTo(caretPos) == -1 && x.ContentEnd.CompareTo(caretPos) == 1).FirstOrDefault();
            if (flowBlock != null)
                flowDoc.Blocks.InsertAfter(flowBlock, list1);
            else
                flowDoc.Blocks.Add(list1);

            individualUploadRichTextBox.CaretPosition = list1.ContentStart;

            selectInsertTextRTB();
        }

        /// <summary>
        /// When a user tabs inside the RichTextBox
        /// </summary>
        private void selectInsertTextRTB()
        {
            CheckBoxIsExceptEnabled = true;

            //If its a blank RichTextBox and you press tab, create a new block
            if (individualUploadRichTextBox.Document.Blocks.Count() == 0)
            {
                list1 = new System.Windows.Documents.List();
                var par = new System.Windows.Documents.Paragraph();
                par.Margin = new Thickness(0, 0, 0, 0); //Dont add the margin to the paragraph but rather to the list item.
                par.Padding = new Thickness(0, 0, 0, 0);

                par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                //    par.FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
                par.TextAlignment = TextAlignment.Left;
                par.FontFamily = new System.Windows.Media.FontFamily("Courier");
                par.FontSize = UIFontSize; // 10;
                list1.Margin = new Thickness(99, 0, 0, 0);
                list1.MarkerStyle = TextMarkerStyle.None;
                list1.ListItems.Add(new ListItem(par));
                flowDoc.Blocks.Add(list1);
            }
            individualUploadRichTextBox.InsertTextRTB(UIFontSize);
        }

        private void SelectActContents()
        {
            flowDoc = new FlowDocument();
            //list1 = new System.Windows.Documents.List();

            CheckBoxIsExceptEnabled = recordSelected.generalFieldsCommentViewModel.Count() > 0 ? true : false;
            foreach (var rec in recordSelected.generalFieldsCommentViewModel)
            {
                // RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, list1, rec);
                var par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, rec.actAdministrationComment, UIFontSize);
                RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, rec.bulletASCII, rec.indentationLeft, UIFontSize);
            }
        }

        #region wiki
        private void selectGetCancelBtnWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "CancelBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void selectGetBackAndSaveBtnWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "BackAndSaveBtn").Select(r => r.ControlHelpText).FirstOrDefault());
        }


        private void selectGetEffectiveDateWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "EffectiveDate").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void selectGetCommentsWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "Comments").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void selectGetPortfolioWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "Portfolio").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void selectGetActNumberWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ControlHelperVM.Where(r => r.ControlName == "ActNumber").Select(r => r.ControlHelpText).FirstOrDefault());
        }

        private void SelectGetApplicationWikiInformation(object obj)
        {
            wikiFlowDoc.WikiFlowDocParser(helperResults.ScreenHelpText);
        }

        #endregion

        //NOT CURRENTLY USED AS ONHOLD HAS BEEN REMOVED SO ONLY BACKANDSAVE IS ENABLED
        //private void SelectSave()
        //{

        //    //check for record that aleady has onHold

        //    List<GeneralFieldsViewlModel> newComments = new List<GeneralFieldsViewlModel>();
        //    GeneralFieldsViewlModel newComment = new GeneralFieldsViewlModel();
        //    var meta = new DocViewModel();

        //    //get the original record, not a copy
        //    recordSelected = WindowViewModel.instance.selectedRecord as GeneralFieldsViewlModel;
        //    // recordSelected.onHold =  RadioOnHold == "NO" ? false : true;    //Remove the onHold feature 11/01/2018

        //    //This is a new record that was inserted into the nonCurrent list and changes were made.  there could be multiple of these new recs
        //    if (recordSelected.actAdministrationID == 0 && recordSelected.pendingEditID == 0)
        //    {
        //        WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- NON CURRENT LIST -- Saving a GO record"));
        //        recordSelected.pendingEditID = recordSelected.actAdministrationID;
        //        recordSelected.pendingEditType = (short)RecordPendingEditType.Modify; //recordSelected.pendingEditType;
        //        recordSelected.change = $"{0}{1}", PendingEditTypeHelper.GetPendingEditTypeText(recordSelected.pendingEditType), recordSelected.actAdministrationID); //"M" + recordSelected.actAdministrationID;
        //        recordSelected.generalFieldsCommentViewModel = flowDoc.LoopThroughFlowDoc(recordSelected.actAdministrationID); //Create a new comments from flowdoc. This mean delete the original and create a new array in db          
        //        recordSelected.startDate = effectiveDatePicker.Date;
        //        recordSelected.flowDocument = flowDoc;
        //    }
        //    else
        //    {
        //        //find the record in current list by comparing the actAdministrationID == pendingEditID. This is because it could be a NEW Added reecord
        //        OrigAdminDetails recordscurrentAdminListBox = GOActAdminDetailsUpdateVM.currentAdminListBox.Where(r => r.actAdministrationID == recordSelected.pendingEditID).FirstOrDefault();
        //        NewAdminDetails recordsnotCurrentAdminListBox = GOActAdminDetailsUpdateVM.notCurrentAdminListBox.Where(r => r.actAdministrationID == recordSelected.actAdministrationID && r.pendingEditID == recordSelected.pendingEditID).FirstOrDefault();

        //        //if (!recordSelected.onHold && recordSelected.pendingEditID != 0) //is the record not on hold and its a copy of a current list rec
        //        //    //If there is a record already with onHold = false
        //        //    recordSelected.onHold = GOActAdminDetailsUpdateVM.notCurrentAdminListBox.FirstOrDefault(r => r.onHold == false && r.pendingEditID != 0 && r != recordSelected) != null ? true : false;  //Remove the onHold feature 11/01/2018

        //        if (recordsnotCurrentAdminListBox == null)
        //        {
        //            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- NON Current List -- Create a GO record", DateTime.Now));

        //            recordsnotCurrentAdminListBox = new NewAdminDetails
        //            {
        //                actAdministrationID = recordSelected.actAdministrationID,
        //                actTitle = recordSelected.actTitle,
        //                pendingEditType = (short)RecordPendingEditType.Modify, // recordSelected.pendingEditType,
        //                change = $"{0}{1}", PendingEditTypeHelper.GetPendingEditTypeText(recordSelected.pendingEditType), recordSelected.actAdministrationID), //"M" + recordSelected.actAdministrationID, 
        //                department = recordSelected.department,
        //                deptCode = recordSelected.deptCode,
        //                // effectiveDate = recordSelected.effectiveDate,
        //                endDate = recordSelected.endDate,
        //                flowDocument = flowDoc,
        //                ildNumber = recordSelected.ildNumber,
        //                departmentPortfolioID = recordSelected.departmentPortfolioID,
        //                isCurrent = recordSelected.isCurrent,
        //                //   onHold = recordSelected.onHold,  //Remove the onHold feature 11/01/2018
        //                pendingEditID = recordSelected.pendingEditID,
        //                isExcept = recordSelected.isExcept,
        //                portfolio = recordSelected.portfolio,
        //                portfolioID = recordSelected.portfolioID,
        //                startDate = effectiveDatePicker.Date, // recordSelected.startDate,
        //                generalFieldsCommentViewModel = flowDoc.LoopThroughFlowDoc(recordSelected.actAdministrationID)
        //            };

        //            if (recordscurrentAdminListBox != null)
        //            {
        //                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- DELETE -- Tag the Current List record RED", DateTime.Now));

        //                recordscurrentAdminListBox.foregroundColour = "Red";
        //                recordscurrentAdminListBox.backgroundColour = "Pink";
        //                recordscurrentAdminListBox.isCurrent = false;
        //                //  recordscurrentAdminListBox.onHold =  true;    //Remove the onHold feature 11/01/2018
        //                recordscurrentAdminListBox.endDate = DateTime.Now;
        //                recordscurrentAdminListBox.pendingEditType = (short)RecordPendingEditType.Delete;
        //                recordscurrentAdminListBox.change = $"{0}{1}", PendingEditTypeHelper.GetPendingEditTypeText(recordscurrentAdminListBox.pendingEditType), recordscurrentAdminListBox.actAdministrationID); //"D" + recordscurrentAdminListBox.actAdministrationID;
        //                recordscurrentAdminListBox.pendingEditID = 0;

        //                //If it comes from an existing rec, get the ID
        //                recordsnotCurrentAdminListBox.pendingEditID = recordscurrentAdminListBox.pendingEditID;
        //            }
        //            GOActAdminDetailsUpdateVM.notCurrentAdminListBox.Add(recordsnotCurrentAdminListBox);

        //        }
        //        else
        //        {
        //            if (recordscurrentAdminListBox != null)
        //            {
        //                recordSelected.change = $"{0}{1}", PendingEditTypeHelper.GetPendingEditTypeText(recordscurrentAdminListBox.pendingEditType), recordscurrentAdminListBox.actAdministrationID); //"M" + recordscurrentAdminListBox.actAdministrationID;
        //                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- DELETE -- Tag the Current List record RED", DateTime.Now));

        //                if (!recordSelected.onHold)
        //                {
        //                    recordscurrentAdminListBox.foregroundColour = "Red";
        //                    recordscurrentAdminListBox.backgroundColour = "Pink";
        //                    recordscurrentAdminListBox.isCurrent = false;
        //                    // recordscurrentAdminListBox.onHold = true;  //Remove the onHold feature 11/01/2018
        //                    recordscurrentAdminListBox.endDate = DateTime.Now;
        //                    recordscurrentAdminListBox.pendingEditType = (short)RecordPendingEditType.Delete;
        //                    recordscurrentAdminListBox.change = $"{0}{1}", PendingEditTypeHelper.GetPendingEditTypeText(recordscurrentAdminListBox.pendingEditType), recordscurrentAdminListBox.actAdministrationID); //"D" + recordscurrentAdminListBox.actAdministrationID;
        //                    recordscurrentAdminListBox.pendingEditID = 0;
        //                }
        //                else
        //                {
        //                    WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- UNDELETE -- Remove Tag from the Current List record", DateTime.Now));

        //                    //reset
        //                    recordscurrentAdminListBox.foregroundColour = string.Empty;
        //                    recordscurrentAdminListBox.backgroundColour = string.Empty;
        //                    recordscurrentAdminListBox.endDate = null;
        //                    recordscurrentAdminListBox.pendingEditType = (short)RecordPendingEditType.None;
        //                    recordscurrentAdminListBox.change = $"{0}{1}", PendingEditTypeHelper.GetPendingEditTypeText(recordscurrentAdminListBox.pendingEditType), recordscurrentAdminListBox.actAdministrationID); //recordscurrentAdminListBox.actAdministrationID.ToString();
        //                    recordscurrentAdminListBox.pendingEditID = 0;
        //                }
        //            }
        //            else
        //            {
        //                WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{0} --- CREATE -- Tag the NON Current List record 'A0'", DateTime.Now));

        //                recordSelected.change = $"{0}{1}", PendingEditTypeHelper.GetPendingEditTypeText(recordSelected.pendingEditType), recordSelected.pendingEditID); // "A0";
        //            }



        //            recordsnotCurrentAdminListBox.pendingEditType = (short)RecordPendingEditType.Add; //recordSelected.pendingEditType;
        //            recordsnotCurrentAdminListBox.generalFieldsCommentViewModel = flowDoc.LoopThroughFlowDoc(recordsnotCurrentAdminListBox.actAdministrationID); //tempArrayNotCurrentAdminListBox;
        //            recordsnotCurrentAdminListBox.startDate = effectiveDatePicker.Date;
        //            recordsnotCurrentAdminListBox.flowDocument = flowDoc;
        //            GOUpdateDetailsImportedFiles.GenOrderUpdateActs(recordsnotCurrentAdminListBox);
        //        }
        //    }
        //    WindowViewModel.instance.CurrentPage = ApplicationPage.goDetailsUpdate;

        //  //  GOActAdminDetailsUpdateVM.TidyUILists();


        //}
    }
}

