   M S B A M L   `   `   `  ���� T  PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails  K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore )    #GeneralOrderUpdateDetails.GOListBox   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:GeneralOrderUpdateDetails     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  Ccore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore  ���� *GOListBoxHeight+  � 	Resources   �� MergedDictionaries �� Source��  �� $T Npack://application:,,,/GeneralOrderUpdateDetails;component/Styles/UIStyle.xaml?�%    .���� 
�������.+��  � RowDefinitions �� $��* ��� $��* ��� $��* �.+��� $��0��$��0q�.+��  �� $��1* ��� $��* �.+�� $��0��$��Center=� � ColumnDefinitions �� $��2* ��� $��5* ��� $	��10* ��� $	��31* ��� $
��6.5* ��� $��3* �   �� 
TargetType �� Property �� Value��%�   '��       '�� T     �� " ��.���  � ��$	 2,0q���  � ��
 �������  � ��$
 Bold"��� " ��.���  � ��$ 1q���  � ��
 �������  � ��
 �����.+��� $��0��._��� �� $��1��._��� ���� *
UIFontSize+Dept�� $��2��._��� ���� *
UIFontSize+	Portfolio�� $��3��._��� ���� *
UIFontSize+
Comments�� $��4��._��� ���� *
UIFontSize+
Eff Date�� $��5��._��� ���� *
UIFontSize+Chg� $��1��.+��� -    ��Name$ currentAdminListBox����.  $��Disabled=����� *GOListViewHeight+���� *
UIFontSize+���� 	 �� Path$	 currentAdminListBox�
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=���)  #GeneralOrderCore.MyTemplateSelector     OrigAdminTemplate ��  �� DataType$ local:OrigAdminDetails��.R�� ���� *DataContext.GOListBoxWidth+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ ListViewL� �� $��2* ��� $��5* ��� $	��10* ��� $	��31* ��� $
��6.5* ��� $��3* �.+��� $��0��$	��Top=�$
��Left=�$��0q� �� Orientation$ Vertical=�.+��� $��15��$��15��
������ 
������ $��0q�$��0q�$��0q����� *editSelectedRow+���� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL�����  �� $ FindAncestor=�" �� � DataContext �� *DataContext+ �� " ������ *none+.���� $��Center=�$P��Jpack://application:,,,/GeneralOrderUpdateDetails;component/Images/edit.png��$��0q��  �� $��* �.+��� $��0��$��15��$��15��
������ 
������ $��0q�$��0q�$��0q����� *deleteSelectedRow+���� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL�����  �� $ FindAncestor=�" �� �� *DataContext+ �� " ������ *none+.���� $T��Npack://application:,,,/GeneralOrderUpdateDetails;component/Images/undelete.png���� $��0��$��15��$��15��
������ 
������ $��0q�$��0q�$��0q����� *deleteSelectedRow+y��� *
isDelete+���� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL�����  �� $ FindAncestor=�" �� �� *DataContext+ �� " ������ *none+.���� $R��Lpack://application:,,,/GeneralOrderUpdateDetails;component/Images/delete.png����  �� Padding$ 2q�$��1��$
��Wrap=����� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *foregroundColour+���� *backgroundColour+���� *
deptCode+�� $ 2q�$��2��$
��Wrap=����� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *foregroundColour+���� *backgroundColour+���� *	portfolio+*  $GeneralOrderCore.BindableRichTextBox  $��3��
������ $��0q� �� 
IsReadOnly .    Document �� *flowDocument+���� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *foregroundColour+���� *backgroundColour+�� $ 2q�$��4��$
��Wrap=����� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *foregroundColour+���� *backgroundColour+���� *	startDate+�� $ 2q�$��5��$
��Wrap=����� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *foregroundColour+���� *backgroundColour+���� *change+.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ MouseLeftButtonUp��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *DisplayMessageCommand+   CommandParameter ��  �� ElementName$ currentAdminListBox��$	 SelectedItem��� $��1��   WordOrangeBrush#��[  $��2q� �� Padding$ 2q�$��0 2 0 2q�._��  �� $��5* ��� $��* �.+��� $��0��$��New Pending Record��$ 20 0 0 0q�$��Center=����� *
UIFontSize+�� -    ��Name$ 	btnAddNew��$��Add New��$��1��$��0q�$��0q����� *
UIFontSize+���� *	btnAddNew+   $ 
MouseEnter��.    �� *getAddNewBtnWikiInformation+�� $��2��.+��  �� $��1* ��� $��* �.+�� $��0��$��Center=� �� $��2* ��� $��5* ��� $	��10* ��� $	��31* ��� $
��6.5* ��� $��3* �  ��%�   '��       '�� T     �� " ��.���  � ��$	 2,0q���  � ��
 �������  � ��$
 Bold"��� " ��.���  � ��$ 1q���  � ��
 �������  � ��
 �����.+��� $��0��._��� ���� *
UIFontSize+�� $��1��._��� ���� *
UIFontSize+Dept�� $��2��._��� ���� *
UIFontSize+	Portfolio�� $��3��._��� ���� *
UIFontSize+
Comments�� $��4��._��� ���� *
UIFontSize+
Eff Date�� $��5��._��� ���� *
UIFontSize+Chg� $��1��.+��� -   $��pendingAdminListBox����.  ���� *GOListViewHeight+���� *
UIFontSize+���� *pendingAdminListBox+$
 TwoWay=�$ PropertyChanged=�   $ MouseLeftButtonUp��.    �� *DisplayMessageCommand+ �� $ pendingAdminListBox��$	 SelectedItem���     NewAdminTemplate �� $ local:NewAdminDetails��.R�� ���� *DataContext.GOListBoxWidth+ �� $ FindAncestor=�$ ListViewL� �� $��2* ��� $��5* ��� $	��10* ��� $	��31* ��� $
��6.5* ��� $��3* �.+��� $��0��$	��Top=�$
��Left=�$��0q�$ Vertical=�$��15��.+��� $��15��$��15��
������ 
������ $��0q�$��0q�$��0q����� *editSelectedRow+����  �� $ FindAncestor=�" �� �� *DataContext+ �� " ������ *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *none+.���� $��Center=�$P��Jpack://application:,,,/GeneralOrderUpdateDetails;component/Images/edit.png��$��0q�$��10��$��10���� $��15��$��15��
������ 
������ $��0q�$��0q�$��0q����� *deleteSelectedRow+����  �� $ FindAncestor=�" �� �� *DataContext+ �� " ������ *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *none+.���� $R��Lpack://application:,,,/GeneralOrderUpdateDetails;component/Images/delete.png��$��10��$��10���� $ 2q�$��1��$��0q�$
��Wrap=����� *
UILBDept+���� *foregroundColour+���� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *backgroundColour+���� *
deptCode+�� $ 2q�$��2��$
��Wrap=����� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *foregroundColour+���� *backgroundColour+���� *	portfolio+   . $��3��
������ $��0q� �� *flowDocument+$
 OneWay=����� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *foregroundColour+���� *backgroundColour+�� $ 2q�$��4��$
��Wrap=����� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *foregroundColour+���� *backgroundColour+���� *	startDate+�� $ 2q�$��5��$
��Wrap=����� *DataContext.UIFontSize+ �� $ FindAncestor=�$ ListViewL����� *foregroundColour+���� *backgroundColour+���� *change+