   M S B A M L   `   `   `  ����T  PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails  5      K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore 5      )    #GeneralOrderUpdateDetails.GOListBox   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:GeneralOrderUpdateDetails     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      Ccore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore  5      ��5
      �� *GOListBoxHeight+  � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/GeneralOrderUpdateDetails;component/Styles/UIStyle.xaml?�6%   %    5      .��5      �� 
�������.+�5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    .+�5      �� $��0��6   $��0q�6&   .+�5      �  5      �� 5      $��1* �6(   �� 5       $��* �6(   .+�5"      � $��0��6   $��Center=�6)    � ColumnDefinitions 5#      �� 5$      $��2* �6/   �� 5%      $��5* �6+   �� 5&      $	��10* �6+   �� 5'      $	��31* �6+   �� 5(      $
��6.5* �6+   �� 5)      $��3* �6+     5+       �� 
TargetType �� Property �� Value��%<  '��       '�� �     �� 5,      " ��6$   .�5-   "   ��  � ��$	 2,0q�6;   �� 5.   "    � ��
 �����6?   �� 5/   "    � ��$
 Bold"�6?   50      �� 51      " ��6$   .�52   "   ��  � ��$ 1q�6D   �� 53   "    � ��
 �����6@   �� 54   "    � ��
 �����6?   55      56      .+�57      �� $��0��6!   ._�58      �� 6,   59      �� 5:      $��1��6!   ._�5;      �� ��6(   �� *
UIFontSize+Dept6N   5<      �� 5=      $��2��6!   ._�5>      �� ��6(   �� *
UIFontSize+	Portfolio6S   5?      �� 5@      $��3��6!   ._�5A      �� ��6(   �� *
UIFontSize+
Comments6R   5B      �� 5C      $��4��6!   ._�5D      �� ��6(   �� *
UIFontSize+
Eff Date6R   5E      �� 5F      $��5��6!   ._�5G      �� ��6(   �� *
UIFontSize+Chg6M   5H      5I      � 5K      $��1��6   .+�5L      �� -   5         ��Name$ currentAdminListBox��5L   H   ��.  $��Disabled=�5M   �   ��5L   $   �� *GOListViewHeight+��6e   �� *
UIFontSize+��5M      �� 	 �� Path$	 currentAdminListBox�
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=���5N      )  #GeneralOrderCore.MyTemplateSelector  5O   "      OrigAdminTemplate 5P   &   �� 5Q   *    �� DataType$ local:OrigAdminDetails��67   .R�5R   .   � ��63   �� *DataContext.GOListBoxWidth+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ ListViewL� 5S   2   �� 5T   6   $��2* �6G   �� 5U   6   $��5* �6G   �� 5V   6   $	��10* �6G   �� 5W   6   $	��31* �6G   �� 5X   6   $
��6.5* �6G   �� 5Y   6   $��3* �6G   .+�5]   2   �� $��0��6>   $	��Top=�6N   $
��Left=�6f   $��0q�6�    �� Orientation$ Vertical=�6�   .+�5^   6   �� $��15��5a   5   $��15��6A   
������ 
������ $��0q�6�   $��0q�6�   $��0q�6�   ��5^   =   �� *editSelectedRow+��6a   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5_   5   ��  �� $ FindAncestor=�" �� � DataContext 5`   5   �� *DataContext+ �� " ����5a      �� *none+.��5b   :   �� $��Center=�6@   $P��Jpack://application:,,,/GeneralOrderUpdateDetails;component/Images/edit.png��6]   $��0q�6�   5d   ;   5e   7   � 5f   6    5g   :   �� 5h   >   $��* �6O   .+�5k   :   �� $��0��6A   $��15��5n   9   $��15��6E   
������ 
������ $��0q�6�   $��0q�6�   $��0q�6�   ��5k   Q   �� *deleteSelectedRow+��6w   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5l   9   ��  �� $ FindAncestor=�" �� 5m   9   �� *DataContext+ �� " ����5n   �   �� *none+.��5o   >   �� $T��Npack://application:,,,/GeneralOrderUpdateDetails;component/Images/undelete.png��6D   5p   ;   �� 5q   :   $��0��6A   $��15��5t   9   $��15��6E   
������ 
������ $��0q�6�   $��0q�6�   $��0q�6�   ��5q   Q   �� *deleteSelectedRow+y�6x   �� *
isDelete+��6�   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5r   9   ��  �� $ FindAncestor=�" �� 5s   9   �� *DataContext+ �� " ����5t   �   �� *none+.��5u   >   �� $R��Lpack://application:,,,/GeneralOrderUpdateDetails;component/Images/delete.png��6D   5v   ;   5x   7   5z   3   �� 5}   2    �� Padding$ 2q�6<   $��1��5~   5   $
��Wrap=�5�   5   ��5~   E   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5   5   �� *foregroundColour+��5�   5   �� *backgroundColour+��5�   5   �� *
deptCode+5�   5   �� 5�   2   $ 2q�6<   $��2��5�   5   $
��Wrap=�5�   5   ��5�   E   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5�   5   �� *foregroundColour+��5�   5   �� *backgroundColour+��5�   5   �� *	portfolio+5�   5   *  $GeneralOrderCore.BindableRichTextBox  5�   2   $��3��5�   6   
������ $��0q�6a    �� 
IsReadOnly .    Document 5�   K   �� *flowDocument+��6m   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5�   6   �� *foregroundColour+��6_   �� *backgroundColour+5�   3   �� 5�   2   $ 2q�6<   $��4��5�   5   $
��Wrap=�5�   5   ��5�   E   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5�   5   �� *foregroundColour+��5�   5   �� *backgroundColour+��5�   5   �� *	startDate+5�   5   �� 5�   2   $ 2q�6<   $��5��5�   5   $
��Wrap=�5�   5   ��5�   E   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5�   5   �� *foregroundColour+��5�   5   �� *backgroundColour+��5�   5   �� *change+5�   5   5�   /   5�   +   5�   #   .  (System.Windows.Interactivity.Interaction   Triggers 5�      /  )System.Windows.Interactivity.EventTrigger  5�   "      	EventName$ MouseLeftButtonUp��61   .  (System.Windows.Interactivity.TriggerBase   Actions. 5�   &   6  0System.Windows.Interactivity.InvokeCommandAction     Command 6<   �� *DisplayMessageCommand+   CommandParameter 6f   ��  �� ElementName$ currentAdminListBox��$	 SelectedItem�5�   #   5�      5�      5�      5�      �� 5�      $��1��6      WordOrangeBrush#��[  6"   $��2q�6Q    �� Padding$ 2q�6e   $��0 2 0 2q�6q   ._�5�      �  5�      �� 5�      $��5* �6+   �� 5�      $��* �6+   .+�5�      �� $��0��6@   $��New Pending Record��6P   $ 20 0 0 0q�6j   $��Center=�6}   ��6    �� *
UIFontSize+6}   �� 5�      -   5         ��Name$ 	btnAddNew��5�   [   $��Add New��6n   $��1��6�   $��0q�6�   $��0q�6�   ��6   �� *
UIFontSize+��6=   �� *	btnAddNew+ 5�        5�      $ 
MouseEnter��6-   . 5�   "      68   �� *getAddNewBtnWikiInformation+5�      5�      5�      5�      �� 5�      $��2��6   .+�5�      �  5�      �� 5�      $��1* �6(   �� 5�      $��* �6(   .+�5�      � $��0��6   $��Center=�6)    5�      �� 5�      $��2* �6/   �� 5�      $��5* �6/   �� 5�      $	��10* �6/   �� 5�      $	��31* �6/   �� 5�      $
��6.5* �6/   �� 5�      $��3* �6/     5�      ��%<  '��       '�� �     �� 5�      " ��6$   .�5�   "   ��  � ��$	 2,0q�6;   �� 5�   "    � ��
 �����6?   �� 5�   "    � ��$
 Bold"�6?   5�      �� 5�      " ��6$   .�5�   "   ��  � ��$ 1q�6D   �� 5�   "    � ��
 �����6@   �� 5�   "    � ��
 �����6?   5�      5�      .+�5�      �� $��0��6!   ._�5�      �� ��6(   �� *
UIFontSize+6J   5�      �� 5�      $��1��6!   ._�5�      �� ��6(   �� *
UIFontSize+Dept6N   5�      �� 5�      $��2��6!   ._�5�      �� ��6(   �� *
UIFontSize+	Portfolio6S   5�      �� 5�      $��3��6!   ._�5�      �� ��6(   �� *
UIFontSize+
Comments6R   5�      �� 5�      $��4��6!   ._�5�      �� ��6(   �� *
UIFontSize+
Eff Date6R   5�      �� 5�      $��5��6!   ._�5�      �� ��6(   �� *
UIFontSize+Chg6M   5�      5�      � 5�      $��1��6   .+�5�      �� -   5        $��pendingAdminListBox��5�   G   ��.  ��6#   �� *GOListViewHeight+��6b   �� *
UIFontSize+��5�      �� *pendingAdminListBox+$
 TwoWay=�$ PropertyChanged=� 5�        5�   "   $ MouseLeftButtonUp��61   . 5�   &      6<   �� *DisplayMessageCommand+ 6f   �� $ pendingAdminListBox��$	 SelectedItem�5�   #   ��5        5  "      NewAdminTemplate 5  &   �� 5  *   $ local:NewAdminDetails��67   .R�5  .   � ��63   �� *DataContext.GOListBoxWidth+ �� $ FindAncestor=�$ ListViewL� 5  2   �� 5  6   $��2* �6G   �� 5  6   $��5* �6G   �� 5	  6   $	��10* �6G   �� 5
  6   $	��31* �6G   �� 5  6   $
��6.5* �6G   �� 5  6   $��3* �6G   .+�5  2   �� $��0��6>   $	��Top=�6N   $
��Left=�6f   $��0q�6�   $ Vertical=�6�   $��15��6�   .+�5  6   �� $��15��5  9   $��15��6E   
������ 
������ $��0q�6�   $��0q�6�   $��0q�6�   ��5  =   �� *editSelectedRow+��5  9   ��  �� $ FindAncestor=�" �� 5  9   �� *DataContext+ �� " ����6�   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5  �   �� *none+.��5  :   �� $��Center=�6@   $P��Jpack://application:,,,/GeneralOrderUpdateDetails;component/Images/edit.png��6]   $��0q�6�   5  ;   5  7   �� 5  6   $��15��5  9   $��15��6E   
������ 
������ $��0q�6�   $��0q�6�   $��0q�6�   ��5  =   �� *deleteSelectedRow+��5  9   ��  �� $ FindAncestor=�" �� 5  9   �� *DataContext+ �� " ����6�   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5  �   �� *none+.��5  :   �� $R��Lpack://application:,,,/GeneralOrderUpdateDetails;component/Images/delete.png��6@   $��10��6�   $��10��6�   5  7   5   3   �� 5!  2   $ 2q�6<   $��1��5"  %   $��0q�65   $
��Wrap=�5&  %   ��5"  @   �� *
UILBDept+��5#  %   �� *foregroundColour+��6M   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5$  %   �� *backgroundColour+��5%  %   �� *
deptCode+5&  %   �� 5(  2   $ 2q�6<   $��2��5)  %   $
��Wrap=�5-  %   ��5)  5   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5*  %   �� *foregroundColour+��5+  %   �� *backgroundColour+��5,  %   �� *	portfolio+5-  %     50  2    . $��3��51  1   
������ $��0q�6\    50  L   �� *flowDocument+$
 OneWay=���51  q   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���52  -   �� *foregroundColour+��6V   �� *backgroundColour+53  3   �� 55  2   $ 2q�6<   $��4��56  %   $
��Wrap=�5:  %   ��56  5   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���57  %   �� *foregroundColour+��58  %   �� *backgroundColour+��59  %   �� *	startDate+5:  %   �� 5=  2   $ 2q�6<   $��5��5>  %   $
��Wrap=�5B  %   ��5>  5   �� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL���5?  %   �� *foregroundColour+��5@  %   �� *backgroundColour+��5A  %   �� *change+5B  %   5D  /   5E  +   5G  #   5I     5J     5K     5L     5M     5N     5O     