   M S B A M L   `   `   `  ���� T  PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails  K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore ;    5GeneralOrderUpdateDetails.GOActAdminIndividualDetails   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:GeneralOrderUpdateDetails     Icore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  ���� *
UIHeight+���� *	UIWidth+  � 	Resources   �� MergedDictionaries �� Source��  �� $T Npack://application:,,,/GeneralOrderUpdateDetails;component/Styles/UIStyle.xaml?�%    .���� .+��  � ColumnDefinitions �� $��* ��� $	��200 �.+���  �� Padding$ 1q�   WordBlueBrush#��[  ._�� 
�������$��0��{���  �� Path$ spinnerBool� �� 	Converter 6   0GeneralOrderUpdateDetails.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=�	 � RowDefinitions	 �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $	��15* ��� $��1* ��� $��1* �.+�� $��0 0 0 0q�$��0��.+���   
HeaderText#��[   CourRegular#��[ $��Center=�$��5q����� *
UIFontSize+���� *windowTitle+.  (System.Windows.Interactivity.Interaction
   Triggers
 /  )System.Windows.Interactivity.EventTrigger     	EventName$ 
MouseEnter��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *getApplicationWikiInformation+� $��0 0 0 0q�$��1�� �� $��2* ��� $��2* ��� $	��15* ��� $��4* �.+��� $��0��  RegularText#��[ $	��Act��$
��10 0q����� *
UIFontSize+�� $��1��#��[ ���� *
UIFontSize+���� *	actNumber+
   $ 
MouseEnter��.    �� *getActNumberWikiInformation+�� $��2��#��[ ���� *
UIFontSize+���� $ actTitle��� $��3��#��[ ���� *
UIFontSize+���� $ actDate�� $��0 0 0 0q�$��2�� �� $��1* ��� $��5* �.+��� #��[ $��	Portfolio��$��0��$
��10 0q����� *
UIFontSize+�� -    ��Name$ pofolioComboBox�� �� DisplayMemberPath$ PortfolioName��$��0q� �� SelectedValuePath$ PortfolioId��$��1������ $ portfolioList����� *
UIFontSize+ �� SelectedIndex �� $ selectedPortfolioIndex�$ TwoWay=����� *generalOrderPadding+
   $ SelectionChanged��.    �� *portfolioCommand+   CommandParameter ��  �� ElementName$ pofolioComboBox��$ SelectedItem�  $ 
MouseEnter��.    �� *getPortfolioWikiInformation+� $��0 0 0 0q�$��3�� �� $��1* ��� $��4* ��� $��1* �.+��� #��[ $��
Department��$��0��$
��10 0q�$
��Auto������ *
UIFontSize+�� $��1��#��[ $
��Auto������ *
UIFontSize+���� *departmentTitle+�� $��2��$��Center=�#��[ $
��Auto������ *
UIFontSize+���� *
deptCode+� $��0 0 0 0q�$��4�� �� $��1* ��� $��5* �.+�� 	 �� $��1* ��� $	��10* �.+��� #��[ $��Comments��$��0��$��0��$	��Top=�$
��10 0q����� *
UIFontSize+�� $��0��$��1��  WordOrangeBrush#��[ $��0q�$��10 0 0 0q�  ForegroundVeryDarkBrush#��[ $	��Top=�$��Center=����� *
UIFontSize+ d� 	IsChecked �� *CheckBoxIsExcept+$ TwoWay=�{��� *CheckBoxIsExceptEnabled+$ TwoWay=����� *	cbChecked+���� $��	Is Except�� �� Padding$ 2 0 10 0q�$
��Wrap=�
   $ 
MouseEnter��.    �� *getIsExceptRBWikiInformation+*  $GeneralOrderCore.BindableRichTextBox  -     Name$! individualUploadRichTextBox��$��1��  ForegroundMainBrush#��[ $��0q�" �� VerticalScrollBarVisibility$ Visible=�   Document �� $ flowDoc�$ TwoWay=����� *generalOrderPadding+���� *,*actAdminIndividualDetailsRichTextBoxHeight+
   $ KeyUp��.    �� *deleteTextRTBblock+  $ 
MouseEnter��.    �� *getCommentsWikiInformation+ J� InputBindings �� 
 �� Key$	 Tab�� �� Command �� *insertTextRTB+�� $ Return�� �� *insertTextRTBblock+� $��0 0 0 0q�$��5�� �� $��1* ��� $��3* ��� $��2* �.+��� #��[ $��Effective Date��$��0��$
��10 0q�$
��Auto������ *
UIFontSize+'  !GeneralOrderCore.DatePickerHelper  $��1��(  "System.Windows.Controls.DatePicker   SelectedDate �� $ effectiveDatePicker�$ TwoWay=�����  �� 
TargetType"  .R��  �Name$ 	PART_Root��#��z��#��z�� �� $��2* ��� $��* �.+���  ��Name$ PART_TextBox��  �� TextAlignment$  Center=�$��0��$��Center=�{�. $��Center=�$��0q����� *
UIFontSize+���� *SelectedDate+! �� StringFormat$! {0:dd-MM-yyyy}��" �� RelativeSource" �� # �� AncestorType"#  .���� $ ��Name$$ PART_Button��$��1��$��Select Date������ *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+� % �Name$% 
PART_Popup��& � AllowsTransparency& . $��Bottom=�' � 	StaysOpen' .  ( � PlacementTarget( �� $ PART_Button��
   $ 
MouseEnter��.    �� *!getEffectiveDateWikiInformation+  $ MouseLeftButtonDown��.    �� *getSelectedDate+� $��0 0 0 0q�$��6�� �� $��* ��� $��2* ��� $��* �.+��� -   $$ btnBackAndSave��$��Save and return��$��0��$	��Top=����� *btnBackAndSave+���� *
UIFontSize+y��� *btnBackAndSaveVisible+���� *generalOrderPadding+���� *generalOrderPadding+
   $ 
MouseEnter��.    �� *" getBackAndSaveBtnWikiInformation+�� -   $$ 	btnCancel��$��Cancel��$��2������ *	btnCancel+���� *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+
   $ 
MouseEnter��.    �� *getCancelBtnWikiInformation+$	   GeneralOrderUpdateDetails.Wiki	  
�������$��1��$	��Top=�