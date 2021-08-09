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

namespace GeneralOrderUpdateDetails
{
    /// <summary>
    /// Design time data class
    /// </summary>
    public class GOActAdminIndividualDetailsDM : BaseViewModel
    {
        #region properties
        public static GOActAdminIndividualDetailsDM Instance => new GOActAdminIndividualDetailsDM();
        private string _windowTitle { get; set; } = "Act Administration - Details Upload";
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
        private int _selectedPortfolioIndex { get; set; } = 1;
        public int selectedPortfolioIndex
        {
            get { return _selectedPortfolioIndex; }
            set
            {
                _selectedPortfolioIndex = value;
                OnPropertyChanged("selectedPortfolioIndex");
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
     
        public List list1 { get; set; }

       
        public int UIFontSize { get; set; } = 14;
        public int UIWidth { get; set; }
        public int UIHeight { get; set; }
        
        public string generalOrderPadding { get; set; }
        public int detailsUploadRichTextBoxHeight { get; set; }
        #endregion


        public GOActAdminIndividualDetailsDM()
        {
            //Set up UI font and size
            UIWidth = (800 * Convert.ToInt32(UIFontSize + "0")) / 110;
            UIHeight = (550 * Convert.ToInt32(UIFontSize + "0")) / 110;
            generalOrderPadding = (UIFontSize - 4).ToString();
            //  generalOrderMargin = UIFontSize + " 10 " + UIFontSize + " 10";
            detailsUploadRichTextBoxHeight = (UIHeight / 6) * 4;

            //Set up properties
            actNumber = "54663";
            actTitle = "Transport Integration Act 2010";
            effectiveDatePicker = DateTime.Now;

            portfolioList = new ObservableCollection<PortfolioViewModel> {
                  new PortfolioViewModel { PortfolioId = 10, PortfolioName = "Housing, Disability and Ageing" },
                 new PortfolioViewModel { PortfolioId = 11, PortfolioName = "Creative Industries" },
                 new PortfolioViewModel { PortfolioId = 12, PortfolioName = "Families and Children" }
                 };

            selectedPortfolioIndex = 0;

            departmentTitle = "Department of Ecenomic Development, Jobs, Transport and Resources";
            deptCode = "DEDJTR";
            actDate = "02/08/2016";

            //set up richtextbox text
            Run run = null;
            flowDoc = new FlowDocument();
            System.Windows.Documents.Paragraph par = new System.Windows.Documents.Paragraph();
            list1 = new List();

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



