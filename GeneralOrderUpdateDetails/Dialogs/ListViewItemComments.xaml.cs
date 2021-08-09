using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeneralOrderUpdateDetails 
{
    /// <summary>
    /// Interaction logic for ListViewItemComments.xaml
    /// </summary>
    public partial class ListViewItemComments : Window
    {
        public ListViewItemComments()
        {
            InitializeComponent();
            this.DataContext = new ListViewItemCommentsVM();
        }

        public ListViewItemComments(string comment)
        {
            InitializeComponent();
            this.DataContext = new ListViewItemCommentsVM(comment, this);
        }
        //public bool? DialogResult { get ; set ; }
        //public UserControl Owner { get ; set ; }

        //public void Close()
        //{

        //}

        //public bool? ShowDialog()
        //{
        //    return true;
        //}
    }
}
