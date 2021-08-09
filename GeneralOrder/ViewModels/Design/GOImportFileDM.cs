using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace GeneralOrder
{
    public class GOImportFileDM : BaseViewModel
    {
        #region properties
        public static GOImportFileDM Instance => new GOImportFileDM();


        private DateTime _GOFileInputDatePicker { get; set; }
        public DateTime GOFileInputDatePicker
        {
            get { return _GOFileInputDatePicker; }
            set
            {
                _GOFileInputDatePicker = value;
                OnPropertyChanged("GOFileInputDatePicker");
            }
        }
        private string _generalOrderFileName { get; set; }
        public string generalOrderFileName
        {
            get { return _generalOrderFileName; }
            set
            {
                _generalOrderFileName = value;
                OnPropertyChanged("generalOrderFileName");
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
        #endregion

        #region UISetup
        public int UIFontSize { get; set; } = 10;
        public int UIWidth { get; set; }
        public int UIHeight { get; set; }
        public string generalOrderPadding { get; set; }
        public string generalOrderMargin { get; set; }
        #endregion
        public GOImportFileDM()
        {
            //wikiFlowDoc = new FlowDocument();
            

            generalOrderFileName = "Supplement_to_the_General_Order_dated_15_September_2016.doc";
            GOFileInputDatePicker = DateTime.Now;
            UIWidth = (800 * Convert.ToInt32(UIFontSize + "0")) / 110;
            UIHeight = (320 * Convert.ToInt32(UIFontSize + "0")) / 110;
            generalOrderPadding = UIFontSize.ToString();
            generalOrderMargin = UIFontSize + " 10 " + UIFontSize + " 10";

        }
    }
}
