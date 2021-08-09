using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GeneralOrder
{
    /// <summary>
    /// This IS NOT USED
    /// </summary>
    public class GOImportFileSelectActVM : BaseViewModel
    {
        private Window window;
        public static GOImportFileSelectActVM instance;
        //public string searchActTitle { get; set; }
        public ICommand selectActTextBox { get; set; }
        public ICommand selectActCB { get; set; }
        public ICommand btnClose { get; set; }
        private ActTitleViewModel _selectText { get; set; }
        public ActTitleViewModel selectText
        {
            get { return _selectText; }
            set
            {
                _selectText = value;
                OnPropertyChanged("selectText");
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

        private bool _cBShowItems { get; set; }
        public bool cBShowItems
        {
            get { return _cBShowItems; }
            set
            {
                _cBShowItems = value;
                OnPropertyChanged("cBShowItems");
            }
        }


        //verifyActText
        private string _verifyActText { get; set; }
        public string verifyActText
        {
            get { return _verifyActText; }
            set
            {
                _verifyActText = value;
                OnPropertyChanged("verifyActText");
            }
        }
        private string _selectTextBoxText { get; set; }
        public string selectTextBoxText
        {
            get { return _selectTextBoxText; }
            set
            {
                _selectTextBoxText = value;
                OnPropertyChanged("selectTextBoxText");
            }
        }

        private static List<ActTitleViewModel> workingkActList { get; set; }


        public GOImportFileSelectActVM(Window win)
        {
            selectActTextBox = new RelayCommand(SelectActTextBoxContents);
            selectActCB = new RelayCommand(SelectActContents);
            btnClose = new RelayCommand(CloseExec);
            selectText = new ActTitleViewModel();
            window = win;
        }

        private void SelectActTextBoxContents()
        {

            if (selectTextBoxText.Length == 0) //reset the actlist if its a new search
            {
                actList = workingkActList;
                cBShowItems = false;
            }

            var split = System.Text.RegularExpressions.Regex.Replace(selectTextBoxText, @"Act+\s+[0-9]{4}", string.Empty).Trim().ToString().Split(' ').ToList();
            foreach (var res in split)
            {
                actList = workingkActList.Where(r => r.ActTitle.ToLower().Contains(res.ToLower())).ToList();
                cBShowItems = true;
            }


            //var res = DatabaseEntity.GetActTitles.GetActTitle();
            //var split = System.Text.RegularExpressions.Regex.Replace(selectText.ActTitle, @"Act+\s+[0-9]{4}", string.Empty).Trim().ToString().Split(' ').ToList();
            //foreach (var re in split)
            //{
            //    var tt = res.Where(r => r.ActTitle.Contains(re)).ToList();
            //    if (tt != null)
            //        foreach (var tt1 in tt)
            //        {
            //            if (actList.FirstOrDefault(r => r.ActTitleILDNumber == tt1.ActTitleILDNumber) == null)
            //                actList.Add(new ActTitleViewModel
            //                {
            //                    ActNumber = tt1.ActNumber,
            //                    ActTitle = tt1.ActTitle,
            //                    ActTitleILDNumber = tt1.ActTitleILDNumber
            //                });
            //        }
            //}
        }

        public void StartActWindow()
        {

            actList = new List<ActTitleViewModel>();
            actList = DatabaseEntity.GetActTitles.GetActTitle().ToList();
            workingkActList = actList;
            cBShowItems = false;
        }


        private void SelectActContents()
        {
            if (cBSelectedActValue != null)
            {
                selectText = cBSelectedActValue;
                selectTextBoxText = cBSelectedActValue.ActTitle;
            }
            cBShowItems = false;

        }
        private void CloseExec()
        {
            if (workingkActList.Where(r => r.ActTitle.ToLower() == selectTextBoxText.ToLower()).FirstOrDefault() != null)
                window.Close();
             else
                MessageBoxHelper.InfoMessageBox("The Act you have entered does not exist");
        }
    }
}
