   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  T PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore -   'GeneralOrderUpdateDetails.GOActAdminAdd   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  3System&clr-namespace:System;assembly=mscorlib  Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  3local'clr-namespace:GeneralOrderUpdateDetails  Ccore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore  ���� *
UIHeight+���� *	UIWidth+  � 	Resources   �� MergedDictionaries �� Source��  �� $T Npack://application:,,,/GeneralOrderUpdateDetails;component/Styles/UIStyle.xaml?�%    .���� .+��  � ColumnDefinitions �� $��* ��� $	��200 �.+���  �� Padding$ 1q�   WordBlueBrush#��[  ._�� 
�������$��0��{���  �� Path$ spinnerBool� �� 	Converter 6  0GeneralOrderUpdateDetails.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=�	 � RowDefinitions	 �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $	��15* ��� $��1* ��� $��1* �.+�� $��0 0 0 0q�$��0��.+���   
HeaderText#��[   CourRegular#��[ $��Center=�$��	0 20 0 10q����� *
UIFontSize+���� *windowTitle+.  (System.Windows.Interactivity.Interaction
   Triggers
 /  )System.Windows.Interactivity.EventTrigger     	EventName$ 
MouseEnter��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *getApplicationWikiInformation+� $��0 0 0 0q�$��1�� �� $��1* ��� $��4* �.+���   RegularText#��[ $	��Act��$��0��$
��10 0q�$
��Auto����  �� DisplayMemberPath$ ActTitle�� �� SelectedValuePath$ ActTitleILDNumber��$��1������ *	actList+���� *generalOrderPadding+���� *
UIFontSize+ �� SelectedIndex �� *actSelectedIndex+$ TwoWay=� �� SelectedItem �� *cBSelectedActValue+$ TwoWay=�
   $ SelectionChanged��.    �� *selectActCB+  $ 
MouseEnter��.    �� *getActWikiInformation+� $��0 0 0 0q�$��2�� �� $��1* ��� $��4* �.+��� #��[ $��	Portfolio��$��0��$
��Auto��$
��10 0q���  �� Text$ -- Select Portfolio --��$ PortfolioName��$ PortfolioId��$��1������ *portfolioList+���� *
UIFontSize+ �� *portfolioSelectedIndex+$ TwoWay=����� *generalOrderPadding+ �� *cBSelectedPortfolioValue+$ TwoWay=�
   $ SelectionChanged��.    �� *selectPortfolioCB+  $ 
MouseEnter��.    �� *getPortfolioWikiInformation+� $��0 0 0 0q�$��3�� �� $��1* ��� $��4* �.+��� #��[ $��
Department��$��0��$
��10 0q�$
��Auto���� $ 
Department��$ DepartmentId��$��1������ *
deptList+���� *generalOrderPadding+���� *
UIFontSize+ �� *deptSelectedIndex+$ TwoWay=� �� *cBSelectedDeptValue+$ TwoWay=�
   $ SelectionChanged��.    �� *selectDeptCB+  $ 
MouseEnter��.    �� *getDepartmentWikiInformation+� $��0 0 0 0q�$��4�� �� $��1* ��� $��4* �.+�� 	 �� $��1* ��� $	��10* �.+��� #��[ $	��Top=�$��Comments��$��0��$��0��$
��10 0q�$
��Auto������ *
UIFontSize+�� $��1��$��0��  WordOrangeBrush#��[ $��0q�$��10 0 0 0q�  ForegroundVeryDarkBrush#��[ $	��Top=�$��Center=����� *
UIFontSize+ d� 	IsChecked �� *CheckBoxIsExcept+$ TwoWay=�{��� *CheckBoxIsExceptEnabled+$ TwoWay=����� *	cbChecked+���� $��	Is Except�� �� Padding$ 2 0 10 0q�$
��Wrap=�
   $ 
MouseEnter��.    �� *getIsExceptRBWikiInformation+*  $GeneralOrderCore.BindableRichTextBox  -     Name$ ActAdminRichTextBox��$��1�� �� 
IsReadOnly .  " �� VerticalScrollBarVisibility$ Visible=�  ForegroundMainBrush#��[    Document �� *	flowDoc+$ TwoWay=����� *generalOrderPadding+���� *actAdminRichTextBoxHeight+ J� InputBindings �� 
 �� Key$	 Tab�� �� Command �� *insertTextRTB+�� $ Return�� �� *insertTextRTBblock+
   $ KeyUp��.    �� *deleteTextRTBblock+  $ 
MouseEnter��.    �� *getCommentsWikiInformation+� $��0 0 0 0q�$��5�� �� $��1* ��� $��3* ��� $��2* �.+��� #��[ $��Effective Date��$��0��$
��10 0q�$
��Auto��'  !GeneralOrderCore.DatePickerHelper  -     Name$ fileImportDatePicker��$��1��(  "System.Windows.Controls.DatePicker   SelectedDateFormat$ Short=����� *
UIFontSize+   SelectedDate �� *effectiveDatePicker+$ TwoWay=�����  �� 
TargetType"  .R��   �Name$  	PART_Root��#��z��#��z�� �� $��2* ��� $��* �.+��� ! ��Name$! PART_TextBox��" �� TextAlignment$" Center=�$��0��$��Center=�{�. $��Center=�$��0q����� *
UIFontSize+���� *SelectedDate+# �� StringFormat$# {0:dd-MM-yyyy}��$ �� RelativeSource$ �� % �� AncestorType"%  .���� & ��Name$& PART_Button��$��1��$��Select Date������ *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+� ' �Name$' 
PART_Popup��( � AllowsTransparency( . $��Bottom=�) � 	StaysOpen) .  * � PlacementTarget* �� + �� ElementName$+ PART_Button��
   $ 
MouseEnter��.    �� *!getEffectiveDateWikiInformation+� $��0 0 0 0q�$��6�� �� $��* ��� $��2* ��� $��* �.+��� $��Save and return��$��0������ *btnBackAndSave+���� *generalOrderPadding+���� *generalOrderPadding+
   $ 
MouseEnter��.    �� *" getBackAndSaveBtnWikiInformation+�� $��Cancel��$��2������ *	btnCancel+���� *generalOrderPadding+���� *generalOrderPadding+
   $ 
MouseEnter��.    �� *getCancelBtnWikiInformation+"	  FontAwesome.WPF.ImageAwesome	  $��0��, 	  Icon$, Refresh=�  ForegroundDarkBrush- 	  
Foreground#- [ $��Center=�$��Center=�. 	  SpinDuration$. 2Q�$��40��$��40��y��� $ spinnerBool� 4
  .GeneralOrderUpdateDetails.ImageSourceConverter
  $ false��$ TwoWay=�/ 	  Spin/ �� $ spinnerBool�$  GeneralOrderUpdateDetails.Wiki  
�������$��1��$	��Top=����� *
UIHeight+