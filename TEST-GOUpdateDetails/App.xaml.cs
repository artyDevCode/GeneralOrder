using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TEST_GOUpdateDetails
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           
            var res = new  GeneralOrderUpdateDetails.MainWindow(6950);
            res.ShowDialog();
        }
       
    }
}
