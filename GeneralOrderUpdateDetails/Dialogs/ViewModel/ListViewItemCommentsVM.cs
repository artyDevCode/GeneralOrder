using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GeneralOrderUpdateDetails
{
    public class ListViewItemCommentsVM
    {
        private System.Windows.Window cWindow;
        public string displayComment { get; set; }
        public ICommand CancelCommand { get; }
        
        public ListViewItemCommentsVM()
        {


        }

        public ListViewItemCommentsVM(string comment, Window window)
        {
            CancelCommand = new RelayCommand(selectCancelCommand);
            cWindow = window;
            displayComment = comment;

        }

        private void selectCancelCommand(object obj)
        {
            cWindow.Close();
        }
    }
}
