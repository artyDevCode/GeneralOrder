   M S B A M L   `   `   `  ����G  CGeneralOrder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GeneralOrderGeneralOrder  5      K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore 5          GeneralOrder.GOImportFile   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:GeneralOrder     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Icore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      ��5
      �� *
UIHeight+��5      �� *	UIWidth+.��5      �� .+�5   
   � $��Center=�6     � ColumnDefinitions  5      �� 5      $��* �6#   �� 5      $	��200 �6#   .+�5      ��  �� Padding$ 1q�6   $��0��6#      WordBlueBrush#��[  64   {�6`   ��  �� Path$ spinnerBool� �� 	Converter )   #GeneralOrder.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=�._�5      � 
������� � RowDefinitions 5      �� 5      $��1* �6(   �� 5      $��1* �6(   �� 5      $��3* �6(   �� 5      $��2* �6(   �� 5      $��2* �6(   �� 5      $��1* �6(   .+�5       �� $��0��6      
HeaderText#��[ 6M     CourRegular#��[ 6q   $3��-Act Administration - General Order File Input��6�   $��Center=�6�   $��	0 20 0 10q�6�   ��6-   �� *
UIFontSize+.  (System.Windows.Interactivity.Interaction   Triggers 5!      /  )System.Windows.Interactivity.EventTrigger  5"   "      	EventName$ 
MouseEnter��61   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 5#   &   6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 6<   �� *getApplicationWikiInformation+5$   #   5&      � 5(      $��0 0 0 0q�6   $��1��6,     5)      �� 5*      $��1* �6/   �� 5+      $��2* �6/   �� 5,      $��1* �6/   .+�5.      ��   RegularText#��[ 6$   $��General Order File��6i   $��0��6�   $��10,4,0,4q�6�   $
��Auto��6�   ��6I   �� *
UIFontSize+6�   �� 5/      -   5         ��Name$ generalOrderFileName��5/   "   $��Center=�6�    �� TextWrapping$
 Wrap=�6�    �� TextAlignment$
 Left=�6�   $��2��51   z   $��1��6�   $��0q�6�   ��5/   @   �� *
UIFontSize+{�6a   �� *enableFileSelect+��50   "   �� *verifyFileSelected+ )   #GeneralOrder.WarningColourConverter  $
 true����51   !   �� *generalOrderFileName+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=���6�   �� *generalOrderPadding+.��6�    52        53   "   $ 	LostFocus��61   .	 54   &     
 6<   �� *getSelectFileInput+55   #   .��59   6   5:      �� 5;      -   5         ��Name$ btnSelectGOFile��5;   �   $��Select file��6�   $��2��6�   ��6"   �� *btnSelectFile+��6D   �� *
UIFontSize+{�6d   �� *enableFileSelect+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+ 5<        5=   "   $ 
MouseEnter��61   .	 5>   &     
 6<   �� *getFileWikiInformation+5?   #   5A      5B      � 5U      $��0 0 0 0q�6   $��3��6,     5V      �� 5W      $��1* �6/   �� 5X      $��2* �6/   .+�5[      �� #��[ 6$   $��	File Type��6i   $��0��6z   $��
10,11,0,11q�6�   ��6I   �� *
UIFontSize+6�   �� 5\      $��1��6J   $��5 0 5 0q�6Z   $��3��6k    6"   �� *generalOrderPadding+._�5]      �   5^   "   �� 5_   &   $��1* �67   �� 5`   &   $��2* �67   .+�5b   "   	�  	� 	GroupName$ BooleanGroup��6.   $��0��6G     WordOrangeBrush#��[ 5d   2     ForegroundVeryDarkBrush#��[ 5e   2   $
��Left=�5f   .   $
��Full��6I   $	��100��6Y   $��Center=�6e   {�5b   W   �� *enableFullRadioButton+ d� 	IsChecked 5c   .   �� *radioFullPartial+ .   (GeneralOrder.RadioButtonCheckedConverter  $ 1��$ TwoWay=���5d   `   �� *
UIFontSize+ 5g   &     5i   *   $ 
MouseEnter��69   .	 5j   .     
 6D   �� *getFullGOWikiInformation+5k   +   5m   #   	� 5p   "   $ BooleanGroup��6.   $��1��6G   #��[ 5r   0   #��[ 5s   2   $��Center=�6i   $
��Left=�5t   .   $��Partial��6I   $	��100��6[   $��20 0 0 0q�6g   {�5p   W   �� *enablePartialRadioButton+ 5q   .   �� *radioFullPartial+   $ 2��$ TwoWay=���5r   ^   �� *
UIFontSize+ 5u   &     5v   *   $ Click��69   .	 5w   .     
 6D   �� *partialRadioButtonClick+5x   +     5y   *   $ 
MouseEnter��69   .	 5z   .     
 6D   �� *getPartialGOWikiInformation+5{   +   5}   #   5~      5      5�      � 5�      $��0 0 0 0q�6   $��4��6,     5�      �� 5�      $��1* �6/   �� 5�      $��3* �6/   .+�5�      �� #��[ 6$   $��Effective Date��6i   $��0��6    �� Padding$ 0q�6�   $��10, 0q�6�   ��6I   �� *
UIFontSize+6�   '  !GeneralOrderCore.DatePickerHelper  5�      $��1��6Y   $��0q�6j   {�61   �� *enableFileSelect+(	  "System.Windows.Controls.DatePicker 	  SelectedDate 6u   �� *GOFileInputDatePicker+��5�      �� 5�   "    �� 
TargetType" 	 62   .R�5�   &   �  �Name$ 	PART_Root��6+   #��z��6>   #��z��5�   6     5�   *   �� 5�   .   $��2* �6?   �� 5�   .   $��* �6?   .+�5�   *   �� $ PART_TextBox��5�   2   $ Center=�5�   2   $��0��6�   $��Center=�6�   {�. $��Center=�6(  $��5q�5�   H   ��5�   I   �� *
UIFontSize+��6i   �� $) #(core:DatePickerHelper.isDateValid)�   $
 true����5�   2   �� *SelectedDate+ �� StringFormat$ {0:dd-MM-yyyy}�� �� RelativeSource ��  �� AncestorType" 	 .��5�   |   5�   +   �� 5�   *   $ PART_Button��6B   $��1��62   $��Select Date��6w   ��6W   �� *
UIFontSize+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+� 5�   *    �Name$ 
PART_Popup��60    � AllowsTransparency . $��Bottom=�5�   0    � 	StaysOpen .   � PlacementTarget 5�   0   ��  �� ElementName$ PART_Button��5�   0   5�   '   5�   #    5�        5�   "   $ 
MouseEnter��61   .	 5�   &     
 6<   �� * getSelectedDateWikiInformation+5�   #   5�      5�      � 5�      $��0 0 0 0q�6   $��5��6,     5�      �� 5�      $��* �6/   �� 5�      $��* �6/   �� 5�      $��* �6/   .+�5�      �� -   5        $ btnImportViewFile��5�   �   $��0��6�   {�6!   �� *enableDiscardViewImportFile+��6S   �� *btnImportViewFile+��6y   �� *
UIFontSize+��6�   �� *btnContentImportViewFile+��6�   �� *generalOrderPadding+��6  �� *generalOrderPadding+ 5�        5�   "   $ 
MouseEnter��61   .	 5�   &     
 6<   �� *getImportFileWikiInformation+5�   #   5�      �� 5�      -   5        $ btnDiscardUncompletedFile��5�   �   $��Discard Incomplete File��6�   $��1��6�   {�6!   �� *enableDiscardIncompletedFile+��6T   �� *
UIFontSize+��6t   �� *btnDiscardIncompleted+��6�   �� *generalOrderPadding+��6  �� *generalOrderPadding+ 5�        5�   "   $ 
MouseEnter��61   .	 5�   &     
 6<   �� *getDiscardFileWikiInformation+5�   #   5�      �� 5�      -   5        $ btnClose��5�   _   $��Close��6q   $��2��6�   ��6"   �� *
btnClose+��6?   �� *
UIFontSize+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+ 5�        5�   "   $ 
MouseEnter��61   .	 5�   &     
 6<   �� *getCloseWikiInformation+5�   #   5�      5�      5�      5�      "
  FontAwesome.WPF.ImageAwesome
  5�      $��0��6    
  Icon$ Refresh=�6/     ForegroundDarkBrush 
  
Foreground# [ 6>   $��Center=�5�   T   $��Center=�6o     
  SpinDuration$  2Q�5�   2   $��40��6C   $��40��6N   y�5�   2   �� $ spinnerBool� '   !GeneralOrder.ImageSourceConverter  $ false��$ TwoWay=�! 
  Spin! 5�   2   �� $ spinnerBool�5�   N      GeneralOrder.Wiki  5�      
�������$��1��6-   $	��Top=�6Y   ��6=   �� *
UIHeight+6Y   5�      5�      5�      