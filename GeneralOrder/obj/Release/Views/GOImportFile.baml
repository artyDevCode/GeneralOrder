   M S B A M L   `   `   `  ���� G  CGeneralOrder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GeneralOrderGeneralOrder  K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore     GeneralOrder.GOImportFile   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:GeneralOrder     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Icore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  ���� *
UIHeight+���� *	UIWidth+.���� .+�� $��Center=�  � ColumnDefinitions  �� $��* ��� $	��200 �.+���  �� Padding$ 1q�$��0��   WordBlueBrush#��[  {���  �� Path$ spinnerBool� �� 	Converter )   #GeneralOrder.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=�._�� 
������� � RowDefinitions �� $��1* ��� $��1* ��� $��3* ��� $��2* ��� $��2* ��� $��1* �.+��� $��0��  
HeaderText#��[   CourRegular#��[ $3��-Act Administration - General Order File Input��$��Center=�$��	0 20 0 10q����� *
UIFontSize+.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ 
MouseEnter��.  (System.Windows.Interactivity.TriggerBase	   Actions.	 6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 �� *getApplicationWikiInformation+� $��0 0 0 0q�$��1��  �� $��1* ��� $��2* ��� $��1* �.+���   RegularText#��[ $��General Order File��$��0��$��10,4,0,4q�$
��Auto������ *
UIFontSize+�� -    ��Name$ generalOrderFileName��$��Center=� �� TextWrapping$
 Wrap=� �� TextAlignment$
 Left=�$��2��$��1��$��0q����� *
UIFontSize+{��� *enableFileSelect+���� *verifyFileSelected+ )   #GeneralOrder.WarningColourConverter  $
 true������ *generalOrderFileName+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=����� *generalOrderPadding+.��   $ 	LostFocus��.	   
 �� *getSelectFileInput+.���� -    ��Name$ btnSelectGOFile��$��Select file��$��2������ *btnSelectFile+���� *
UIFontSize+{��� *enableFileSelect+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getFileWikiInformation+� $��0 0 0 0q�$��3��  �� $��1* ��� $��2* �.+��� #��[ $��	File Type��$��0��$��
10,11,0,11q����� *
UIFontSize+�� $��1��$��5 0 5 0q�$��3�� �� *generalOrderPadding+._��   �� $��1* ��� $��2* �.+�	�  	� 	GroupName$ BooleanGroup��$��0��  WordOrangeBrush#��[   ForegroundVeryDarkBrush#��[ $
��Left=�$
��Full��$	��100��$��Center=�{��� *enableFullRadioButton+ d� 	IsChecked �� *radioFullPartial+ .   (GeneralOrder.RadioButtonCheckedConverter  $ 1��$ TwoWay=����� *
UIFontSize+   $ 
MouseEnter��.	   
 �� *getFullGOWikiInformation+	� $ BooleanGroup��$��1��#��[ #��[ $��Center=�$
��Left=�$��Partial��$	��100��$��20 0 0 0q�{��� *enablePartialRadioButton+ �� *radioFullPartial+   $ 2��$ TwoWay=����� *
UIFontSize+   $ Click��.	   
 �� *partialRadioButtonClick+  $ 
MouseEnter��.	   
 �� *getPartialGOWikiInformation+� $��0 0 0 0q�$��4��  �� $��1* ��� $��3* �.+��� #��[ $��Effective Date��$��0�� �� Padding$ 0q�$��10, 0q����� *
UIFontSize+'  !GeneralOrderCore.DatePickerHelper  $��1��$��0q�{��� *enableFileSelect+(	  "System.Windows.Controls.DatePicker 	  SelectedDate �� *GOFileInputDatePicker+����  �� 
TargetType" 	 .R��  �Name$ 	PART_Root��#��z��#��z��  �� $��2* ��� $��* �.+��� $ PART_TextBox��$ Center=�$��0��$��Center=�{�. $��Center=�$��5q����� *
UIFontSize+���� $) #(core:DatePickerHelper.isDateValid)�   $
 true������ *SelectedDate+ �� StringFormat$ {0:dd-MM-yyyy}�� �� RelativeSource ��  �� AncestorType" 	 .���� $ PART_Button��$��1��$��Select Date������ *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+�  �Name$ 
PART_Popup�� � AllowsTransparency . $��Bottom=� � 	StaysOpen .   � PlacementTarget ��  �� ElementName$ PART_Button��   $ 
MouseEnter��.	   
 �� * getSelectedDateWikiInformation+� $��0 0 0 0q�$��5��  �� $��* ��� $��* ��� $��* �.+��� -   $ btnImportViewFile��$��0��{��� *enableDiscardViewImportFile+���� *btnImportViewFile+���� *
UIFontSize+���� *btnContentImportViewFile+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getImportFileWikiInformation+�� -   $ btnDiscardUncompletedFile��$��Discard Incomplete File��$��1��{��� *enableDiscardIncompletedFile+���� *
UIFontSize+���� *btnDiscardIncompleted+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getDiscardFileWikiInformation+�� -   $ btnClose��$��Close��$��2������ *
btnClose+���� *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getCloseWikiInformation+"
  FontAwesome.WPF.ImageAwesome
  $��0�� 
  Icon$ Refresh=�  ForegroundDarkBrush 
  
Foreground# [ $��Center=�$��Center=�  
  SpinDuration$  2Q�$��40��$��40��y��� $ spinnerBool� '   !GeneralOrder.ImageSourceConverter  $ false��$ TwoWay=�! 
  Spin! �� $ spinnerBool�   GeneralOrder.Wiki  
�������$��1��$	��Top=����� *
UIHeight+