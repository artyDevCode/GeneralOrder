   M S B A M L   `   `   `  ����T  PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails  5      K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore 5      ;    5GeneralOrderUpdateDetails.GOActAdminIndividualDetails   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:GeneralOrderUpdateDetails     5      Icore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      ��5	      �� *
UIHeight+��5
      �� *	UIWidth+  � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/GeneralOrderUpdateDetails;component/Styles/UIStyle.xaml?�6%   %    5      .��5      �� .+�5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��200 �6#   .+�5      ��  �� Padding$ 1q�6      WordBlueBrush#��[  6!   ._�5      � 
�������$��0��6*   {�6:   ��  �� Path$ spinnerBool� �� 	Converter 6   0GeneralOrderUpdateDetails.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=�	 � RowDefinitions	 5      �� 5       $��1* �6(   �� 5!      $��1* �6(   �� 5"      $��1* �6(   �� 5#      $��1* �6(   �� 5$      $	��15* �6(   �� 5%      $��1* �6(   �� 5&      $��1* �6(   .+�5(      � $��0 0 0 0q�6   $��0��6-   .+�5)      ��   
HeaderText#��[ 6$     CourRegular#��[ 6i   $��Center=�6�   $��5q�6�   ��6H   �� *
UIFontSize+��6�   �� *windowTitle+.  (System.Windows.Interactivity.Interaction
   Triggers
 5*      /  )System.Windows.Interactivity.EventTrigger  5+   "      	EventName$ 
MouseEnter��61   .  (System.Windows.Interactivity.TriggerBase   Actions. 5,   &   6  0System.Windows.Interactivity.InvokeCommandAction     Command 6<   �� *getApplicationWikiInformation+5-   #   5/      50      � 51      $��0 0 0 0q�6   $��1��6-    52      �� 53      $��2* �6/   �� 54      $��2* �6/   �� 55      $	��15* �6/   �� 56      $��4* �6/   .+�58      �� $��0��6&     RegularText#��[ 6X   $	��Act��6}   $
��10 0q�6�   ��66   �� *
UIFontSize+6�   �� 59      $��1��6$   #��[ 6V   ��65   �� *
UIFontSize+��6{   �� *	actNumber+
 5:   $     5;   &   $ 
MouseEnter��65   . 5<   *      6@   �� *getActNumberWikiInformation+5=   '   5?      �� 5A      $��2��6$   #��[ 6U   ��64   �� *
UIFontSize+��6z   �� $ actTitle��� 5C      $��3��6$   #��[ 6U   ��64   �� *
UIFontSize+��6z   �� $ actDate�5D      � 5F      $��0 0 0 0q�6   $��2��6-    5G      �� 5H      $��1* �6/   �� 5I      $��5* �6/   .+�5K      �� #��[ 6%   $��	Portfolio��6k   $��0��6|   $
��10 0q�6�   ��6J   �� *
UIFontSize+6�   �� 5L      -   5         ��Name$ pofolioComboBox��5L   #    �� DisplayMemberPath$ PortfolioName��5M   $   $��0q�6F    �� SelectedValuePath$ PortfolioId��5O   $   $��1��6D   ��5L   =   �� $ portfolioList���5M   Q   �� *
UIFontSize+ �� SelectedIndex 5N   $   �� $ selectedPortfolioIndex�$ TwoWay=���5O   T   �� *generalOrderPadding+
 5S        5T   "   $ SelectionChanged��61   . 5U   &      6<   �� *portfolioCommand+   CommandParameter 5V   =   ��  �� ElementName$ pofolioComboBox��$ SelectedItem�5W   #     5X   "   $ 
MouseEnter��61   . 5Y   &      6<   �� *getPortfolioWikiInformation+5Z   #   5\      5]      � 5_      $��0 0 0 0q�6   $��3��6-    5`      �� 5a      $��1* �6/   �� 5b      $��4* �6/   �� 5c      $��1* �6/   .+�5e      �� #��[ 6%   $��
Department��6k   $��0��6}   $
��10 0q�6�   $
��Auto��6�   ��6J   �� *
UIFontSize+6�   �� 5f      $��1��6$   #��[ 6V   $
��Auto��6�   ��64   �� *
UIFontSize+��6{   �� *departmentTitle+6�   �� 5g      $��2��6$   $��Center=�6U   #��[ 6r   $
��Auto��6�   ��64   �� *
UIFontSize+��6�   �� *
deptCode+6�   5h      � 5k      $��0 0 0 0q�6   $��4��6-    5l      �� 5m      $��1* �6/   �� 5n      $��5* �6/   .+�5p      � 	 5q      �� 5r   "   $��1* �60   �� 5s   "   $	��10* �60   .+�5u      �� #��[ 6I   $��Comments��6n   $��0��6~   $��0��6�   $	��Top=�6�   $
��10 0q�6�   ��6(   �� *
UIFontSize+6�   �� 5v      $��0��6(   $��1��68     WordOrangeBrush#��[ 5x   2   $��0q�6`   $��10 0 0 0q�6k     ForegroundVeryDarkBrush#��[ 5y   2   $	��Top=�6j   $��Center=�6�   ��5v   E   �� *
UIFontSize+ d� 	IsChecked 6f   �� *CheckBoxIsExcept+$ TwoWay=�{�5w   *   �� *CheckBoxIsExceptEnabled+$ TwoWay=���5x   ~   �� *	cbChecked+��5z   "   �� 5{   &   $��	Is Except��60    �� Padding$ 2 0 10 0q�6A   $
��Wrap=�6U   
 5}   "     5~   &   $ 
MouseEnter��65   . 5   *      6@   �� *getIsExceptRBWikiInformation+5�   '   5�      5�      *  $GeneralOrderCore.BindableRichTextBox  5�      -   5          Name$! individualUploadRichTextBox��5�   3   $��1��5�   4     ForegroundMainBrush#��[ 5�   3   $��0q�6e   " �� VerticalScrollBarVisibility$ Visible=�6q      Document 5�   X   �� $ flowDoc�$ TwoWay=���5�   D   �� *generalOrderPadding+��6l   �� *,*actAdminIndividualDetailsRichTextBoxHeight+
 5�        5�   "   $ KeyUp��61   . 5�   &      6<   �� *deleteTextRTBblock+5�   #     5�   "   $ 
MouseEnter��61   . 5�   &      6<   �� *getCommentsWikiInformation+5�   #    J� InputBindings 5�      �� 5�   "   
 �� Key$	 Tab��5�   %    �� Command 5�   %   �� *insertTextRTB+�� 5�   "   $ Return��5�   %    5�   %   �� *insertTextRTBblock+5�      5�      � 5�      $��0 0 0 0q�6   $��5��6-    5�      �� 5�      $��1* �6/   �� 5�      $��3* �6/   �� 5�      $��2* �6/   .+�5�      �� #��[ 6%   $��Effective Date��6k   $��0��6�   $
��10 0q�6�   $
��Auto��6�   ��6J   �� *
UIFontSize+6�   '  !GeneralOrderCore.DatePickerHelper  5�      $��1��60   (  "System.Windows.Controls.DatePicker   SelectedDate 6A   �� $ effectiveDatePicker�$ TwoWay=���5�      �� 5�   "    �� 
TargetType"  62   .R�5�   &   �  �Name$ 	PART_Root��6+   #��z��6>   #��z��5�   6    5�   *   �� 5�   .   $��2* �6?   �� 5�   .   $��* �6?   .+�5�   *   ��  ��Name$ PART_TextBox��5�   2     �� TextAlignment$  Center=�5�   2   $��0��6i   $��Center=�6y   {�. $��Center=�6�   $��0q�5�   H   ��5�   I   �� *
UIFontSize+��5�   2   �� *SelectedDate+! �� StringFormat$! {0:dd-MM-yyyy}��" �� RelativeSource" �� # �� AncestorType"#  .��5�   |   5�   +   �� 5�   *   $ ��Name$$ PART_Button��6B   $��1��62   $��Select Date��6w   ��6W   �� *
UIFontSize+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+� 5�   *   % �Name$% 
PART_Popup��60   & � AllowsTransparency& . $��Bottom=�5�   0   ' � 	StaysOpen' .  ( � PlacementTarget( 5�   0   �� $ PART_Button��5�   0   5�   '   5�   #   
 5�        5�   "   $ 
MouseEnter��61   . 5�   &      6<   �� *!getEffectiveDateWikiInformation+5�   #     5�   "   $ MouseLeftButtonDown��61   . 5�   &      6<   �� *getSelectedDate+5�   #   5�      5�      � 5�      $��0 0 0 0q�6   $��6��6-    5�      �� 5�      $��* �6/   �� 5�      $��2* �6/   �� 5�      $��* �6/   .+�5�      �� -   5        $$ btnBackAndSave��5�   g   $��Save and return��5�   !   $��0��6;   $	��Top=�6�   ��5�   !   �� *btnBackAndSave+��6F   �� *
UIFontSize+y�6   �� *btnBackAndSaveVisible+��5�   K   �� *generalOrderPadding+��6s   �� *generalOrderPadding+
 5�        5�   "   $ 
MouseEnter��61   . 5�   &      6<   �� *" getBackAndSaveBtnWikiInformation+5�   #   5�      �� 5�      -   5        $$ 	btnCancel��5�   b   $��Cancel��6u   $��2��6�   ��6"   �� *	btnCancel+��6A   �� *
UIFontSize+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+
 5�        5�   "   $ 
MouseEnter��61   . 5�   &      6<   �� *getCancelBtnWikiInformation+5�   #   5�      5�      5�      5�      $	   GeneralOrderUpdateDetails.Wiki	  5�      
�������$��1��6-   $	��Top=�6=   5�      5�      5�      