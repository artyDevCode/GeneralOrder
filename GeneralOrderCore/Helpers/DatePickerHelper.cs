using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GeneralOrderCore
{
    public class DatePickerHelper : DatePicker
    {

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void OnStaticPropertyChanged(string propertyName)
        {
            var handler = StaticPropertyChanged;
            if (handler != null)
            {
                handler(null, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static bool _isDateValid = true; // backing field

        public static bool isDateValid
        {
            get { return _isDateValid; }
            set
            {
                _isDateValid = value;
                OnStaticPropertyChanged("isDateValid");
            }
        }


       // public static bool isDateValid { get; set; } = true;
        //Set default date
        private static string previousDate { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
        /// <summary>
        /// Date structure added to an array and is used by DateTime.TryParseExact to validate entry
        /// </summary>
        protected string[] dateFormats = new[]
                {
                    "dd/MM/yyyy", "dd-MM-yyyy", "d/MM/yyyy", "d-MM-yyyy", "d/M/yyyy", "d-M-yyyy"
                };

        protected DateTime resDate;

        /// <summary>
        /// OnPreviewKeyUp will trigger this method before the actual execution. Because there are three controls attached to the datepicker (Button, TextBox and Calendar) a switch statement 
        /// will select the appropriate execution of the code to avaid type conflict.
        /// Button and Calendar will resume normal execution Handled = false
        /// TextBox will be handled by the application on validating entry into the textbox
        /// </summary>
        /// <param name="e">The event occured by losing focus on the button, textbox and calendar</param>
        protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnPreviewLostKeyboardFocus(e);
            //e.GetType();
            switch (e.OriginalSource.GetType().Name)
            {
                case "Button":
                    e.Handled = false;
                    break;
                case "Calendar":
                    e.Handled = false;
                    break;
                case "TextBox":
                    ValidateDate(((System.Windows.Controls.TextBox)e.OriginalSource).Text, e);
                    break;
                default:
                    e.Handled = false;
                    break;
            }
            if (!isDateValid)
            {
                //       ((System.Windows.Controls.DatePicker)e.Source).SelectedDate = DateTime.Now;
                MessageBoxHelper.CatchExceptionMessageBox("Invalid date entered");
                //        isDateValid = true;
            }

        }

        /// <summary>
        /// Will trigger this method everytime a key is pressed on the keyboard.
        /// Only valid character/Numeric types will be allowed. 
        /// </summary>
        /// <param name="e">The key pressed</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {

            base.OnPreviewKeyDown(e);
            var key = (int)e.Key;
            e.Handled = true;
            //Key 34 - 43 is numbers. Key 145 and 143 are / and -
            if ((key > 33 && key < 44) || key == 143 || key == 145 || e.Key == Key.Back)
            {
                //Call the validate method to verify format of date
                ValidateDate(((System.Windows.Controls.TextBox)e.OriginalSource).Text, e);
            }
            else
            {
                //Invalid key pressed
                isDateValid = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of keyboard event KeyboardFocusChangedEventArgs and KeyEventArgs whose base class is KeyboardEventArgs </typeparam>
        /// <param name="date">The date in string format</param>
        /// <param name="e">the event passed to as Generics</param>
        private void ValidateDate<T>(string date, T e) where T : KeyboardEventArgs
        {
            //Validate if the string can be converted to a date
            if (DateTime.TryParseExact(date, dateFormats, null, System.Globalization.DateTimeStyles.None, out resDate))
            {
                isDateValid = true;
                e.Handled = false;
                previousDate = date;
                ((System.Windows.Controls.DatePicker)e.Source).SelectedDate = DateTime.Parse(date); //Pass the value of the date to the Datepicker which in turn will add it to the textbox
                BindableRichTextBox.UIHasChanged = true;
            }
            else //Invalid date format
            {
                isDateValid = false;
                e.Handled = false;  //set this value to true if you want the cursor to return to the datepicker textbox
                BindableRichTextBox.UIHasChanged = false;
            }
        }
    }
}
