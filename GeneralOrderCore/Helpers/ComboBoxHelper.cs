using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GeneralOrderCore
{
    public class ComboBoxHelper : ComboBox
    {

       // private static List<ActTitleViewModel> ActList {get; set; }
    

        //protected override void OnGotFocus(RoutedEventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    ActList = new List<ActTitleViewModel>();
        //    ActList = ItemsSource.Cast<ActTitleViewModel>().ToList();
        //}
        //protected override void OnPreviewKeyUp(KeyEventArgs e)
        //{
        //    base.OnPreviewKeyUp(e);
        //    e.Handled = true;

        //    if (this.Text.Length == 0)
        //    {
        //        ItemsSource = ActList;
        //    }
        //    ///var list = ItemsSource.Cast<ActTitleViewModel>().ToList();
        //    /// var l = Items;
        //    var split = System.Text.RegularExpressions.Regex.Replace(this.Text.ToString(), @"Act+\s+[0-9]{4}", string.Empty).Trim().ToString().Split(' ').ToList();
        //    foreach (var res in split)
        //    {
        //        ItemsSource = ActList.Where(r => r.ActTitle.Contains(res)).ToList();
        //        //if (tt != null)
        //        //    foreach (var tt1 in tt)
        //        //    {
        //        //        if (actList.FirstOrDefault(r => r.ActTitleILDNumber == tt1.ActTitleILDNumber) == null)
        //        //            actList.Add(new ActTitleViewModel
        //        //            {
        //        //                ActNumber = tt1.ActNumber,
        //        //                ActTitle = tt1.ActTitle,
        //        //                ActTitleILDNumber = tt1.ActTitleILDNumber
        //        //            });
        //        //    }
        //    }
        //}

       

        //protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        //{
        //    base.OnSelectionChanged(e);
        //    var a = this.SelectedItem;
        //}
    }
}
