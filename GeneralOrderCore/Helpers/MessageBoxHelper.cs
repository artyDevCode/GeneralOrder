using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeneralOrderCore
{
    /// <summary>
    /// Message box helpers to provide different variations of messages to the calling application
    /// </summary>
    public class MessageBoxHelper
    {
        public static bool DisplayMessageBox(string title, string message)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return false;

            }
        }

        public static void InfoMessageBox(string message)
        {
            var result = MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void CatchExceptionMessageBox(string message)
        {
          
            var result = MessageBox.Show(message, "Contact IT", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
