using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GeneralOrderCore
{
    //Models for the RichTextBox current and partial views
    public class OrigAdminDetails : GeneralFieldsViewlModel
    {
    }

    public class NewAdminDetails : GeneralFieldsViewlModel
    {

    }

    public class MyTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// This property is set from XAML.
        /// </summary>
        public DataTemplate NewAdminTemplate { get; set; }

        /// <summary>
        /// This property is set from XAML.
        /// </summary>
        public DataTemplate OrigAdminTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is NewAdminDetails)
            {
                return NewAdminTemplate;
            }

            if (item is OrigAdminDetails)
            {
                return OrigAdminTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
