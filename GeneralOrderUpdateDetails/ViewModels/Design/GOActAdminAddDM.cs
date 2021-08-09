using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace GeneralOrderUpdateDetails
{

    /// <summary>
    /// Class for design time data
    /// </summary>
    public class GOActAdminAddDM : BaseViewModel
    {
        #region properties
        public static GOActAdminAddDM Instance => new GOActAdminAddDM();

      

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
        private int _deptSelectedIndex { get; set; } = 1;
        public int deptSelectedIndex
        {
            get { return _deptSelectedIndex; }
            set
            {
                _deptSelectedIndex = value;
                OnPropertyChanged("deptSelectedIndex");
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
        private int _portfolioSelectedIndex { get; set; } = 1;
        public int portfolioSelectedIndex
        {
            get { return _portfolioSelectedIndex; }
            set
            {
                _portfolioSelectedIndex = value;
                OnPropertyChanged("portfolioSelectedIndex");
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

        public int UIFontSize { get; set; } = 10;
        public int UIWidth { get; set; } 
        public int UIHeight { get; set; }

        public string generalOrderPadding { get; set; }
        private int actAdminRichTextBoxHeight { get; set; }
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public GOActAdminAddDM()
        {
            //Set the UI font and size
            UIWidth = (800 * Convert.ToInt32(UIFontSize + "0")) / 110;
            UIHeight = (550 * Convert.ToInt32(UIFontSize + "0")) / 110;
            generalOrderPadding = (UIFontSize - 4).ToString();
            actAdminRichTextBoxHeight =  (UIHeight / 6) * 4;
        
            //Set up the combo boxes
            portfolioList = new ObservableCollection<PortfolioViewModel> {
                 new PortfolioViewModel { PortfolioId = 10, PortfolioName = "Housing, Disability and Ageing" },
                 new PortfolioViewModel { PortfolioId = 11, PortfolioName = "Creative Industries" },
                 new PortfolioViewModel { PortfolioId = 12, PortfolioName = "Families and Children" }
                 };

            portfolioSelectedIndex = 1;


            effectiveDatePicker = DateTime.Now;
            actList = new ObservableCollection<ActTitleViewModel> {
                 new ActTitleViewModel {  ActTitle = "Administration and Probate Act 1958",  ActTitleILDNumber = 9988 },
                 new ActTitleViewModel {  ActTitle = "Adoption Act 1984",  ActTitleILDNumber = 9987 },
                 new ActTitleViewModel {  ActTitle = "Commercial Arbitration Act 2011",  ActTitleILDNumber = 9222 },
                 new ActTitleViewModel {  ActTitle = "Commonwealth Places(Administration of Laws) Act 1970",  ActTitleILDNumber = 2343 },
                 new ActTitleViewModel {  ActTitle = "Confiscation Act 1997",  ActTitleILDNumber = 3454 },
                 };
            actSelectedIndex = 3;

            deptList = new ObservableCollection<DepartmentViewModel> {
                 new DepartmentViewModel { DepartmentId = 10, Department = "Department of Peremier and Cabinet" },
                 new DepartmentViewModel { DepartmentId = 11, Department = "Department of Justice" },
                 new DepartmentViewModel { DepartmentId = 12, Department = "Department of Education" }
                 };

            deptSelectedIndex = 1;

            //Set up the richtextbox text
            Run run = null;
            flowDoc = new FlowDocument();


            System.Windows.Documents.Paragraph par = new System.Windows.Documents.Paragraph();
            list1 = new System.Windows.Documents.List();

            par.Margin = new Thickness(0, 0, 0, 0);
            par.Padding = new Thickness(0, 0, 0, 0);

            par.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
            par.TextAlignment = TextAlignment.Left;
            par.FontFamily = new System.Windows.Media.FontFamily("Courier");
            par.FontSize = UIFontSize;
            run = new Run("ed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae " +
               "ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut " +
               "odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor " +
               "sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, " +
               "quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit " +
               "esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur" +
               "ed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis" +
               " et quasi architecto beatae vitae dicta sunt explicabo.Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui " +
               "ratione voluptatem sequi nesciunt.Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore" +
               " et dolore magnam aliquam quaerat voluptatem.Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur ? Quis autem " +
               "vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur");
            par.Inlines.Add(run);


            list1.Margin = new Thickness(21, 0, 0, 0);
            list1.MarkerStyle = TextMarkerStyle.Disc;
            list1.ListItems.Add(new ListItem(par));
            flowDoc.Blocks.Add(list1);

        }

    }
}
