   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      T PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails 5	      K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore 5
      -   'GeneralOrderUpdateDetails.GOActAdminAdd   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      3System&clr-namespace:System;assembly=mscorlib  5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      3local'clr-namespace:GeneralOrderUpdateDetails  5	      Ccore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore  5
      ��5      �� *
UIHeight+��5      �� *	UIWidth+  � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/GeneralOrderUpdateDetails;component/Styles/UIStyle.xaml?�6%   %    5      .��5      �� .+�5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��200 �6#   .+�5       ��  �� Padding$ 1q�6      WordBlueBrush#��[  6!   ._�5!      � 
�������$��0��6*   {�6:   ��  �� Path$ spinnerBool� �� 	Converter 6  0GeneralOrderUpdateDetails.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=�	 � RowDefinitions	 5"      �� 5#      $��1* �6(   �� 5$      $��1* �6(   �� 5%      $��1* �6(   �� 5&      $��1* �6(   �� 5'      $	��15* �6(   �� 5(      $��1* �6(   �� 5)      $��1* �6(   .+�5,      � $��0 0 0 0q�6   $��0��6-   .+�5-      ��   
HeaderText#��[ 6%     CourRegular#��[ 6k   $��Center=�6�   $��	0 20 0 10q�6�   ��6K   �� *
UIFontSize+��6�   �� *windowTitle+.  (System.Windows.Interactivity.Interaction
   Triggers
 5.      /  )System.Windows.Interactivity.EventTrigger  5/   "      	EventName$ 
MouseEnter��61   .  (System.Windows.Interactivity.TriggerBase   Actions. 50   &   6  0System.Windows.Interactivity.InvokeCommandAction     Command 6<   �� *getApplicationWikiInformation+51   #   53      54      � 56      $��0 0 0 0q�6   $��1��6,    57      �� 58      $��1* �6/   �� 59      $��4* �6/   .+�5;      ��   RegularText#��[ 6%   $	��Act��6J   $��0��6U   $
��10 0q�6e   $
��Auto��6s   �� 5<       �� DisplayMemberPath$ ActTitle��5=   $    �� SelectedValuePath$ ActTitleILDNumber��5?   $   $��1��6J   ��5<   $   �� *	actList+��6E   �� *generalOrderPadding+��5=   B   �� *
UIFontSize+ �� SelectedIndex 5>   $   �� *actSelectedIndex+$ TwoWay=� �� SelectedItem 5@   $   �� *cBSelectedActValue+$ TwoWay=�
 5A        5B   "   $ SelectionChanged��61   . 5C   &      6<   �� *selectActCB+5D   #     5E   "   $ 
MouseEnter��61   . 5F   &      6<   �� *getActWikiInformation+5G   #   5I      5J      � 5L      $��0 0 0 0q�6   $��2��6,    5M      �� 5N      $��1* �6/   �� 5O      $��4* �6/   .+�5Q      �� #��[ 6$   $��	Portfolio��6I   $��0��6[   $
��Auto��6k   $
��10 0q�6x   �� 5R       �� Text$ -- Select Portfolio --��6K   $ PortfolioName��5S   $   $ PortfolioId��5U   $   $��1��6D   ��5R   %   �� *portfolioList+��5S   G   �� *
UIFontSize+ 5T   $   �� *portfolioSelectedIndex+$ TwoWay=���5U   V   �� *generalOrderPadding+ 5V   $   �� *cBSelectedPortfolioValue+$ TwoWay=�
 5W        5X   "   $ SelectionChanged��61   . 5Y   &      6<   �� *selectPortfolioCB+5Z   #     5[   "   $ 
MouseEnter��61   . 5\   &      6<   �� *getPortfolioWikiInformation+5]   #   5_      5`      � 5d      $��0 0 0 0q�6   $��3��6,    5e      �� 5f      $��1* �6/   �� 5g      $��4* �6/   .+�5i      �� #��[ 6%   $��
Department��6J   $��0��6\   $
��10 0q�6l   $
��Auto��6{   �� 5j      $ 
Department��5k   $   $ DepartmentId��5m   $   $��1��6E   ��5j   %   �� *
deptList+��6G   �� *generalOrderPadding+��5k   D   �� *
UIFontSize+ 5l   $   �� *deptSelectedIndex+$ TwoWay=� 5n   $   �� *cBSelectedDeptValue+$ TwoWay=�
 5o        5p   "   $ SelectionChanged��61   . 5q   &      6<   �� *selectDeptCB+5r   #     5s   "   $ 
MouseEnter��61   . 5t   &      6<   �� *getDepartmentWikiInformation+5u   #   5w      5x      � 5|      $��0 0 0 0q�6   $��4��6,    5}      �� 5~      $��1* �6/   �� 5      $��4* �6/   .+�5�      � 	 5�      �� 5�   "   $��1* �60   �� 5�   "   $	��10* �60   .+�5�      �� #��[ 6)   $	��Top=�6N   $��Comments��6f   $��0��6�   $��0��6�   $
��10 0q�6�   $
��Auto��6�   ��6v   �� *
UIFontSize+6�   �� 5�      $��1��6(   $��0��65     WordOrangeBrush#��[ 5�   2   $��0q�6`   $��10 0 0 0q�6k     ForegroundVeryDarkBrush#��[ 5�   2   $	��Top=�6j   $��Center=�6�   ��5�   E   �� *
UIFontSize+ d� 	IsChecked 6f   �� *CheckBoxIsExcept+$ TwoWay=�{�5�   *   �� *CheckBoxIsExceptEnabled+$ TwoWay=���5�   ~   �� *	cbChecked+��5�   "   �� 5�   &   $��	Is Except��60    �� Padding$ 2 0 10 0q�6A   $
��Wrap=�6U   
 5�   "     5�   &   $ 
MouseEnter��65   . 5�   *      6@   �� *getIsExceptRBWikiInformation+5�   '   5�      5�      *  $GeneralOrderCore.BindableRichTextBox  5�      -   5          Name$ ActAdminRichTextBox��5�   3   $��1��5�   5    �� 
IsReadOnly .  " �� VerticalScrollBarVisibility$ Visible=�6�     ForegroundMainBrush#��[ 5�   b      Document 5�   P   �� *	flowDoc+$ TwoWay=���5�   Y   �� *generalOrderPadding+��5�   4   �� *actAdminRichTextBoxHeight+ J� InputBindings 5�      �� 5�   "   
 �� Key$	 Tab��5�   %    �� Command 5�   %   �� *insertTextRTB+�� 5�   "   $ Return��5�   %    5�   %   �� *insertTextRTBblock+
 5�        5�   "   $ KeyUp��61   . 5�   &      6<   �� *deleteTextRTBblock+5�   #     5�   "   $ 
MouseEnter��61   . 5�   &      6<   �� *getCommentsWikiInformation+5�   #   5�      5�      � 5�      $��0 0 0 0q�6   $��5��6,    5�      �� 5�      $��1* �6/   �� 5�      $��3* �6/   �� 5�      $��2* �6/   .+�5�      �� #��[ 6%   $��Effective Date��6J   $��0��6`   $
��10 0q�6p   $
��Auto��6   '  !GeneralOrderCore.DatePickerHelper  5�      -   5          Name$ fileImportDatePicker��5�   0   $��1��6N   (  "System.Windows.Controls.DatePicker   SelectedDateFormat$ Short=�6^   ��6y   �� *
UIFontSize+   SelectedDate 6�   �� *effectiveDatePicker+$ TwoWay=���5�      �� 5�   "    �� 
TargetType"  62   .R�5�   &   �   �Name$  	PART_Root��6+   #��z��6>   #��z��5�   6    5�   *   �� 5�   .   $��2* �6?   �� 5�   .   $��* �6?   .+�5�   *   �� ! ��Name$! PART_TextBox��5�   2   " �� TextAlignment$" Center=�5�   2   $��0��6i   $��Center=�6y   {�. $��Center=�6�   $��0q�5�   H   ��5�   I   �� *
UIFontSize+��5�   2   �� *SelectedDate+# �� StringFormat$# {0:dd-MM-yyyy}��$ �� RelativeSource$ �� % �� AncestorType"%  .��5�   |   5�   +   �� 5�   *   & ��Name$& PART_Button��6B   $��1��62   $��Select Date��6w   ��6W   �� *
UIFontSize+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+� 5�   *   ' �Name$' 
PART_Popup��60   ( � AllowsTransparency( . $��Bottom=�5�   0   ) � 	StaysOpen) .  * � PlacementTarget* 5�   0   �� + �� ElementName$+ PART_Button��5�   0   5�   '   5�   #   
 5�        5�   "   $ 
MouseEnter��61   . 5�   &      6<   �� *!getEffectiveDateWikiInformation+5�   #   5�      5�      � 5�      $��0 0 0 0q�6   $��6��6,    5�      �� 5�      $��* �6/   �� 5�      $��2* �6/   �� 5�      $��* �6/   .+�5�      �� $��Save and return��6F   $��0��6`   ��6!   �� *btnBackAndSave+��6p   �� *generalOrderPadding+��6�   �� *generalOrderPadding+
 5�        5�   "   $ 
MouseEnter��61   . 5�   &      6<   �� *" getBackAndSaveBtnWikiInformation+5�   #   5�      �� 5�      $��Cancel��6B   $��2��6S   ��6"   �� *	btnCancel+��6c   �� *generalOrderPadding+��6�   �� *generalOrderPadding+
 5�        5�   "   $ 
MouseEnter��61   . 5�   &      6<   �� *getCancelBtnWikiInformation+5�   #   5�      5      5     5     "	  FontAwesome.WPF.ImageAwesome	  5     $��0��6   , 	  Icon$, Refresh=�6/     ForegroundDarkBrush- 	  
Foreground#- [ 6>   $��Center=�5  T   $��Center=�6o   . 	  SpinDuration$. 2Q�5  2   $��40��6C   $��40��6N   y�5  2   �� $ spinnerBool� 4
  .GeneralOrderUpdateDetails.ImageSourceConverter
  $ false��$ TwoWay=�/ 	  Spin/ 5  2   �� $ spinnerBool�5  N   $  GeneralOrderUpdateDetails.Wiki  5     
�������$��1��6,   $	��Top=�6X   ��6<   �� *
UIHeight+6X   5
     5     5     