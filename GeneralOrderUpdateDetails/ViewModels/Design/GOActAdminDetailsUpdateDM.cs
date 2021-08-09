using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace GeneralOrderUpdateDetails
{
    /// <summary>
    /// Data Design time class
    /// </summary>
    public class GOActAdminDetailsUpdateDM : BaseViewModel
    {
        #region properties
        public static GOActAdminDetailsUpdateDM Instance => new GOActAdminDetailsUpdateDM();
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

        private string _generalOrderActName { get; set; }
        public string generalOrderActName
        {
            get { return _generalOrderActName; }
            set
            {
                _generalOrderActName = value;
                OnPropertyChanged("generalOrderActName");
            }
        }
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
        public System.Windows.Documents.List list1;

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
       
        public int UIWidth { get; set; }
        public int UIHeight { get; set; }
        public string generalOrderPadding { get; set; }
        public int GOListBoxHeight { get; set; }
        public int GOListViewHeight { get; set; }
        public int GOListBoxWidth { get; set; }

        //Listbox columns
        public int UIFontSize { get; set; } = 10;
       
        #endregion

        public GOActAdminDetailsUpdateDM()
        {
            //Set the UI font and size
            int fontSize = UIFontSize > 9 ? UIFontSize : 10;
            GOListBoxWidth = ((800 * Convert.ToInt32(fontSize + "0")) / 110) - 200;
            generalOrderPadding = (UIFontSize - 4).ToString(); // (UIFontSize / 10) + UIFontSize;
            GOListBoxHeight = (480 * Convert.ToInt32(fontSize + "0")) / 110;
            GOListViewHeight = (210 * Convert.ToInt32(fontSize + "0")) / 110;
            UIWidth = (800 * Convert.ToInt32(fontSize + "0")) / 110;
            UIHeight = (640 * Convert.ToInt32(fontSize + "0")) / 110;
           
            //Set up properties for the textboxes
            generalOrderILD = "11716";
            generalOrderActName = "Transport Integration Act 2010";
            generalOrderActNumber = "06/2010";
            currentAdminListBox = new ObservableCollection<OrigAdminDetails>();
            pendingAdminListBox = new ObservableCollection<NewAdminDetails>();

            //Setup the current list box with data
            currentAdminListBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for public transport1",
                deptCode = "DEDJR",
                startDate = DateTime.Now,
                generalFieldsCommentViewModel = new ObservableCollection<GeneralFieldsCommentViewModel>
                {
                    new GeneralFieldsCommentViewModel
                    {
                         fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 65,
                         actAdministrationComment = "In so far as it relates to the exercise of powers relating to leases and licences under Subdivisions 1 and 2 of Division 9 of Part I in respect of land described as Crown Allotment 22D of section 30, Parish of Melbourne North being the site of the Victorian County Court "
                    },
                     new GeneralFieldsCommentViewModel
                    {
                         fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 66,
                         actAdministrationComment = "In so far as it relates to the land described as Crown Allotment 16 of Section 5, at Elwood, Parish of Prahran being the site of the former Elwood Police Station:"
                    },
                       new GeneralFieldsCommentViewModel
                    {
                           fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Courier New",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 111,
                         indentationLeft = 54,
                         tabStopPosition = 72,
                        actAdminCommentId = 67,
                         actAdministrationComment = "Except Division 6 of Part I, Subdivision 3 of Division 9 of Part I, section 209 and the remainder of the Act where it relates to the sale and alienation of Crown Lands as set out in Administrative Arrangements Order No. 58 (which are administered by the Minister for Finance)"
                    },
                         new GeneralFieldsCommentViewModel
                    {
                               fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Courier New",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 111,
                         indentationLeft = 54,
                         tabStopPosition = 72,
                        actAdminCommentId = 68,
                         actAdministrationComment = "Except sections 201, 201A and 399"
                    },
                          new GeneralFieldsCommentViewModel
                    {
                              fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 69,
                         actAdministrationComment = "Sections 22C - 22E"
                    },
                           new GeneralFieldsCommentViewModel
                    {
                               fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 70,
                         actAdministrationComment = "Sections 201, 201A and 399 in so far as they relate to the land described as Crown Allotment 16 of Section 5, at Elwood, Parish of Prahran being the site of the former Elwood Police Station (in so far as they relate to that land, these provisions are jointly administered with the Minister for Finance)"
                    },
                      new GeneralFieldsCommentViewModel
                    {
                           fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "none",
                         pageBreakBefore = false,
                         tabHangingIndent = 0,
                         alignmentType = 0,
                         bulletASCII = 999,
                         indentationLeft = 0,
                         tabStopPosition = 36,
                        actAdminCommentId = 71,
                         actAdministrationComment = "(The Act is otherwise administered by the Minister for Corrections, the Minister for Creative Industries, the Minister for Energy, Environment and Climate Change, the Minister for Finance, the Minister for Health, the Minister for Ports, the Minister for Roads and Road Safety and the Special Minister of State)"
                    }
                },

                change = "M12345",
                backgroundColour = "white",
                foregroundColour = "#4682B4"

            });
            currentAdminListBox[0].flowDocument = flow(currentAdminListBox[0].generalFieldsCommentViewModel);
            currentAdminListBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for roads and safety2",
                deptCode = "DEDJR",
                startDate = DateTime.Now,

                generalFieldsCommentViewModel = new ObservableCollection<GeneralFieldsCommentViewModel>
                {
                     new GeneralFieldsCommentViewModel
                    {
                         fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 70,
                         actAdministrationComment = "kyt tjrj tr jytf jySections 201, 201A and 399 in so far as they relate to the land described as Crown Allotment 16 of Section 5, at Elwood, Parish of Prahran being the site of the former Elwood Police Station (in so far as they relate to that land, these provisions are jointly administered with the Minister for Finance)"
                    }
                },
                change = "M12345",
                backgroundColour = "#eaf0fb",// "#B0C4DE",
                foregroundColour = "#4682B4" // "White"
            });

            currentAdminListBox[1].flowDocument = flow(currentAdminListBox[1].generalFieldsCommentViewModel);


            //*********************************************************************//

            //Set up the pending lst box with data
            pendingAdminListBox.Add(new NewAdminDetails
            {
                portfolio = "Minister for public transport1",
                deptCode = "DEDJR",
                startDate = DateTime.Now,

                generalFieldsCommentViewModel = new ObservableCollection<GeneralFieldsCommentViewModel>
                {
                    new GeneralFieldsCommentViewModel
                    {
                         fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 46,
                         actAdministrationComment = "Part I (except section 4B), Parts III, IIIA, VIII, and IX "
                    },
                     new GeneralFieldsCommentViewModel
                    {
                         fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 47,
                         actAdministrationComment = "Sections 16, 35, 41-44, 47D, 48-48C, 53-58B and 86-86C "
                    },
                       new GeneralFieldsCommentViewModel
                    {
                           fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 48,
                         actAdministrationComment = "Section 87 in so far as it relates to the effective management of hunting, including preserving good order among hunters of wildlife "
                    },
                         new GeneralFieldsCommentViewModel
                    {
                               fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "none",
                         pageBreakBefore = false,
                         tabHangingIndent = 0,
                         alignmentType = 0,
                         bulletASCII = 999,
                         indentationLeft = 21.2,
                         tabStopPosition = 36,
                        actAdminCommentId = 49,
                         actAdministrationComment = "(These provisions are jointly administered with the Minister for Energy, Environment and Climate Change)"
                    },
                          new GeneralFieldsCommentViewModel
                    {
                              fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 50,
                         actAdministrationComment = "Part IIIB in so far as it relates to the hunting of game "
                    },
                           new GeneralFieldsCommentViewModel
                    {
                               fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 51,
                         actAdministrationComment = "Sections 58C, 58D and 58E "
                    },
                      new GeneralFieldsCommentViewModel
                    {
                           fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "none",
                         pageBreakBefore = false,
                         tabHangingIndent = 0,
                         alignmentType = 0,
                         bulletASCII = 999,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 52,
                         actAdministrationComment = "(The Act is otherwise administered by the Minister for Energy, Environment and Climate Change)"
                    }
                },

                change = "A1",
                backgroundColour = "white",
                foregroundColour = "#008080"
            });

            pendingAdminListBox[0].flowDocument = flow(pendingAdminListBox[0].generalFieldsCommentViewModel);


            pendingAdminListBox.Add(new NewAdminDetails
            {
                portfolio = "Minister for public transport2",
                deptCode = "DPC",
                startDate = DateTime.Now,
                generalFieldsCommentViewModel = new ObservableCollection<GeneralFieldsCommentViewModel>
                {
                    new GeneralFieldsCommentViewModel
                    {
                               fontBold = false,
                          bulletChar = "",
                         indentationBy = 0,
                         indentationRight = 0,
                         listSymbolFont = "Symbol",
                         pageBreakBefore = false,
                         tabHangingIndent = 1,
                         alignmentType = 0,
                         bulletASCII = 183,
                         indentationLeft = 18,
                         tabStopPosition = 36,
                        actAdminCommentId = 51,
                         actAdministrationComment = "TEST Afgfkhg jffk f kgfSections 58C, 58D and 58E "
                    },
                },

                change = "M1",
                backgroundColour = "#ebfbea",
                foregroundColour = "#008080"
            });
            pendingAdminListBox[1].flowDocument = flow(pendingAdminListBox[1].generalFieldsCommentViewModel);


        }

        /// <summary>
        /// Handle the RICH TEXT BOX text
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        private FlowDocument flow(ObservableCollection<GeneralFieldsCommentViewModel> records)
        {
            flowDoc = new FlowDocument();
            //list1 = new System.Windows.Documents.List();

            foreach (var record in records)
            {
                // RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, list1, record);
                var par = RichTextBoxHelper.DisplayTextRTBHelper(flowDoc, record.actAdministrationComment, UIFontSize);
                RichTextBoxHelper.FlowDocBulletType(flowDoc, list1, par, record.bulletASCII, record.indentationLeft, UIFontSize);

            }
            return flowDoc;
        }
    }
}

