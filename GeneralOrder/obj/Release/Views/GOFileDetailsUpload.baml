   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  G CGeneralOrder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GeneralOrderGeneralOrder K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore &    GeneralOrder.GOFileDetailsUpload   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  3System&clr-namespace:System;assembly=mscorlib  Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  &localclr-namespace:GeneralOrder  Ccore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore  ���� *
UIHeight+���� *	UIWidth+.���� .+��   � ColumnDefinitions  �� $��* ��� $	��200 �.+���  �� Padding$ 1q�   WordBlueBrush#��[  ._�� 
�������$��0��{���  �� Path$ spinnerBool� �� 	Converter )  #GeneralOrder.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=� � RowDefinitions �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $	��15* ��� $��1* ��� $��1* �.+�� $��0 0 0 0q�$��0��.+���   
HeaderText#��[   CourRegular#��[ $5��/Act Administration - File Import Details Upload��$��Center=�$��	0 20 0 10q����� *
UIFontSize+.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ 
MouseEnter��.  (System.Windows.Interactivity.TriggerBase	   Actions.	 6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 �� *getApplicationWikiInformation+� $��0 0 0 0q�$��1��  �� $��1* ��� $��4* �.+���   RegularText#��[ $��GO File��$��0��$��10, 0q�$
��Auto������ *
UIFontSize+�� $��Center=�$��1������ *generalOrderName+���� *
UIFontSize+� $��0 0 0 0q�$��2��  �� $��1* ��� $��4* �.+��� #��[ $��	Portfolio��$��0��$
��10 0q�$
��Auto������ *
UIFontSize+��  �� DisplayMemberPath$ PortfolioName�� �� SelectedValuePath$ PortfolioId��$��1������ *portfolioList+���� *
UIFontSize+ �� SelectedIndex �� *portfolioSelectedIndex+$ TwoWay=����� *generalOrderPadding+ �� SelectedItem �� *cBSelectedPortfolioValue+$ TwoWay=�   $ SelectionChanged��.	   
 �� *selectPortfolioCB+  $ 
MouseEnter��.	   
 �� *getPortfolioWikiInformation+� $��0 0 0 0q�$��3��  �� $��1* ��� $��4* �.+��� #��[ $	��Act��$��0��$
��10 0q�$
��Auto������ *
UIFontSize+�� $ �� IsSynchronizedWithCurrentItem$
 TrueY�$ ActTitle��$ ActTitleILDNumber��$��1������ *	actList+���� *
UIFontSize+ �� *actSelectedIndex+$ TwoWay=����� *generalOrderPadding+ �� *cBSelectedActValue+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   $ SelectionChanged��.	   
 �� *selectActCB+  $ 
MouseEnter��.	   
 �� *getActWikiInformation+� $��0 0 0 0q�$��4��  �� $��1* ��� $��4* �.+��  �� $��1* ��� $	��10* �.+��� #��[ $	��Top=�$��Comments��$��0��$��0��$
��10 0q�$
��Auto������ *
UIFontSize+�� $��0��$��1��  WordOrangeBrush#��[ $��0q�$��10 0 0 0q�  ForegroundVeryDarkBrush#��[ $	��Top=�$��Center=����� *
UIFontSize+ d� 	IsChecked �� *CheckBoxIsExcept+$ TwoWay=�{��� *CheckBoxIsExceptEnabled+$ TwoWay=����� *	cbChecked+���� $��	Is Except�� �� Padding$ 2 0 10 0q�$
��Wrap=�   $ 
MouseEnter��.	   
 �� *getIsExceptRBWikiInformation+*  $GeneralOrderCore.BindableRichTextBox  -     Name$ detailsUploadRichTextBox��$��1��$	��Top=�" �� VerticalScrollBarVisibility$ Visible=�  ForegroundMainBrush#��[    Document �� *	flowDoc+$ TwoWay=����� * detailsUploadRichTextBoxHeight+���� *generalOrderPadding+   $ KeyUp��.	   
 �� *deleteTextRTBblock+  $ 
MouseEnter��.	   
 �� *getCommentsWikiInformation+ J� InputBindings �� 
 �� Key$	 Tab�� �� Command �� *insertTextRTB+�� $ Return�� �� *insertTextRTBblock+� $��0 0 0 0q�$��5��  �� $��1* ��� $��3* ��� $��2* �.+��� #��[ $��Effective Date��$��0��$��10, 0q�$
��Auto������ *
UIFontSize+'  !GeneralOrderCore.DatePickerHelper  -     Name$ fileImportDatePicker��$��1��(  "System.Windows.Controls.DatePicker   SelectedDateFormat$ Short=����� *
UIFontSize+   SelectedDate �� *effectiveDatePicker+$ TwoWay=�����  �� 
TargetType"  .R��  �Name$ 	PART_Root��#��z��#��z��  �� $��2* ��� $��* �.+���  ��Name$ PART_TextBox�� �� TextAlignment$ Center=�$��0��$��Center=�{�. $��Center=�$��0q����� *
UIFontSize+���� *SelectedDate+  �� StringFormat$  {0:dd-MM-yyyy}��! �� RelativeSource! �� " �� AncestorType""  .���� # ��Name$# PART_Button��$��1��$��Select Date������ *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+� $ �Name$$ 
PART_Popup��% � AllowsTransparency% . $��Bottom=�& � 	StaysOpen& .  ' � PlacementTarget' �� ( �� ElementName$( PART_Button��   $ 
MouseEnter��.	   
 �� *!getEffectiveDateWikiInformation+� $��0 0 0 10q�$��6��  �� $��* ��� $��* ��� $��2* ��� $��2* ��� $��* �.+��� -   $# btnImportFile��$
��Save��$��0������ *BackAndSave+���� *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getBackBtnWikiInformation+�� -   $# btnUploadDetails��$��Upload Details��$��2������ *UploadDetails+���� *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *$"getUploadDetailsBtnWikiInformation+�� -   $# btnGenerateNewGO��$��Generate new GO File��$��3������ *GenerateNewGO+���� *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getNewGOBtnWikiInformation+�� -   $# btnClose��$��Close��$��4������ *Close+���� *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getClosekBtnWikiInformation+"	  FontAwesome.WPF.ImageAwesome	  $��0��) 	  Icon$) Refresh=�  ForegroundDarkBrush* 	  
Foreground#* [ $��Center=�$��Center=�+ 	  SpinDuration$+ 2Q�$��40��$��40��y��� $ spinnerBool� '
  !GeneralOrder.ImageSourceConverter
  $ false��$ TwoWay=�, 	  Spin, �� $ spinnerBool�  GeneralOrder.Wiki  
�������$��1��$	��Top=����� *
UIHeight+