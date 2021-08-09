using GeneralOrderCore;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace GeneralOrderUpdateDetails
{
    public class WindowViewModel : BaseViewModel
    {
        /// <summary>
        /// The window this view model controls
        /// </summary>
        private System.Windows.Window mWindow;
        private FlowDocument _data { get; set; }
        public FlowDocument data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("data");
            }
        }
        private FlowDocument _DocumentTextRange { get; set; }
        public FlowDocument DocumentTextRange
        {
            get { return _DocumentTextRange; }
            set
            {
                _DocumentTextRange = value;
                OnPropertyChanged("DocumentTextRange");
            }
        }

        /// <summary>
        /// The current page of the application
        /// </summary>
        private ApplicationPage _CurrentPage { get; set; }
        public ApplicationPage CurrentPage
        {
            get { return _CurrentPage; }
            set
            {
                _CurrentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 200;
        private GeneralFieldsViewlModel _selectedRecord { get; set; }
        public GeneralFieldsViewlModel selectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;
                OnPropertyChanged("selectedRecord");
            }
        }
        private int _selectedRecordIndex { get; set; } = -1;
        public int selectedRecordIndex
        {
            get { return _selectedRecordIndex; }
            set
            {
                _selectedRecordIndex = value;
                OnPropertyChanged("selectedRecordIndex");
            }
        }
        private bool _isCurrentList { get; set; }
        public bool isCurrentList
        {
            get { return _isCurrentList; }
            set
            {
                _isCurrentList = value;
                OnPropertyChanged("isCurrentList");
            }
        }
        private FlowDocument _loggerFlowDoc { get; set; }
        public FlowDocument loggerFlowDoc
        {
            get { return _loggerFlowDoc; }
            set
            {
                _loggerFlowDoc = value;
                OnPropertyChanged("loggerFlowDoc");
            }
        }

        public static WindowViewModel instance;
        //  public static int ildNumber {get; set;}
        public int UIFontSize { get; set; }
        public int UIWidth { get; set; }
        public int GOActAdminAddHeight { get; set; }
        public int GOAdminDetailsUpdateHeight { get; set; }
        public int GOActAdminIndividualDetailsHeight { get; set; }
        public int UILoggerHeight { get; set; }
        public int UILoggerRTBHeight { get; set; }
        public int UILoggerWidth { get; set; }
        public string generalOrderPadding { get; set; }
        // public string generalOrderMargin { get; set; }
        public int MinFont { get; set; }
        public int MaxFont { get; set; }


        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion
        public WindowViewModel(Window window)
        {
            var retObj = AppConfig.GetAppSettings();

            MinFont = Convert.ToInt16(retObj[0]);
            UIFontSize = Convert.ToInt16(retObj[1]);
            MaxFont = Convert.ToInt16(retObj[2]);

            UIFontSize = UIFontSize > MaxFont ? MaxFont : UIFontSize;
            UIFontSize = UIFontSize < MinFont ? MinFont : UIFontSize;
           
            generalOrderPadding = (UIFontSize - 4).ToString();
            // generalOrderMargin = UIFontSize + " 10 " + UIFontSize + " 10";
            UIWidth = (800 * Convert.ToInt32(UIFontSize + "0")) / 110;
            GOActAdminAddHeight = (550 * Convert.ToInt32(UIFontSize + "0")) / 110;
            GOAdminDetailsUpdateHeight = (640 * Convert.ToInt32(UIFontSize + "0")) / 110;
            GOActAdminIndividualDetailsHeight = (550 * Convert.ToInt32(UIFontSize + "0")) / 110;

            UILoggerWidth = (800 * Convert.ToInt32(UIFontSize + "0")) / 110;
            UILoggerHeight = (100 * Convert.ToInt32(UIFontSize + "0")) / 110;
            UILoggerRTBHeight = (75 * Convert.ToInt32(UIFontSize + "0")) / 110;

            mWindow = window;

            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(WindowClose);

            CurrentPage = ApplicationPage.goDetailsUpdate;

        }



        public void WindowClose()
        {
            mWindow.Close();
        }


    }
}
