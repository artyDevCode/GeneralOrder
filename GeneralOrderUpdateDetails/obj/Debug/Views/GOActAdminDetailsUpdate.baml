   M S B A M L   `   `   `  ����T  PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails  5      7    1GeneralOrderUpdateDetails.GOActAdminDetailsUpdate   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:GeneralOrderUpdateDetails     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5	      ��5
      �� *
UIHeight+��5      �� *	UIWidth+  � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/GeneralOrderUpdateDetails;component/Styles/UIStyle.xaml?�6%   %    5      .��5      �� $	��Top=�6   .+�5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��200 �6#   .+�5      ��  �� Padding$ 1q�6      WordBlueBrush#��[  6"   ._�5      � 
�������{�6*   ��  �� Path$ spinnerBool� �� 	Converter 6   0GeneralOrderUpdateDetails.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=�	 � RowDefinitions	 5       �� 5!      $��1* �6(   �� 5"      $��1* �6(   �� 5#      $��1* �6(   �� 5$      $��1* �6(   .+�5&      � $��0 0 0 0q�6   $��0��6-   .+�5'      ��   
HeaderText#��[ 6%     CourRegular#��[ 6i   $)��#Act Administration - Details Update��6�   $��Center=�6�   $��0 10 0 5q�6�   ��6I   �� *
UIFontSize+.  (System.Windows.Interactivity.Interaction
   Triggers
 5(      /  )System.Windows.Interactivity.EventTrigger  5)   "      	EventName$ 
MouseEnter��61   .  (System.Windows.Interactivity.TriggerBase   Actions. 5*   &   6  0System.Windows.Interactivity.InvokeCommandAction     Command 6<   �� *getApplicationWikiInformation+5+   #   5-      5.      � 5/      $��0 0 0 0q�6   $��1��6-    50      �� 51      $��2* �6/   �� 52      $��2* �6/   �� 53      $	��15* �6/   �� 54      $��4* �6/   .+�56      ��   RegularText#��[ 6$   $��Bottom=�6i   $	��Act��6�   $��0��6�   $��5 2 2 5q�6�   ��6I   �� *
UIFontSize+6�   �� 57      -   5         ��Name$ txtgeneralOrderILD��57   "   $��1��6�   $��0q�6�   $��2 2 2 5q�6�   ��6>   �� *
UIFontSize+��6^   �� *generalOrderILD+6�   �� 59       �� DisplayMemberPath$ ActTitle��5:   $    �� SelectedValuePath$ ActTitleILDNumber��5<   $   $��2��6J   $��0q�6Z   $��2q�6f   ��59   $   �� *	actList+��5:   A   �� *
UIFontSize+ �� SelectedIndex 5;   $   �� *actSelectedIndex+$ TwoWay=� �� SelectedItem 5=   $   �� *cBSelectedActValue+$ TwoWay=�
 5>        5?   "   $ SelectionChanged��61   . 5@   &      6<   �� *selectActCB+5A   #     5B   "   $ 
MouseEnter��61   . 5C   &      6<   �� *getActWikiInformation+5D   #   5F      �� 5G      -   5        $ generalOrderActNumber��5G   "   $��3��6�   $��0q�6�   $��2 2 2 5q�6�   ��6A   �� *
UIFontSize+��6a   �� *generalOrderActNumber+6�   5H      )   #GeneralOrderUpdateDetails.GOListBox  5J      $��0 0 0 2q�6'   $��2��68   � 5K      $��0 0 0 0q�6   $��3��6-    5L      �� 5M      $��5* �6/   �� 5N      $��2* �6/   �� 5O      $��2* �6/   .+�5R      �� $��2q�6!   $��2q�6,   $��0��6@   $ 0q�6Q   #��[  6]   ._�5S      �  5T   "   �� 5U   &   $��3* �67   �� 5V   &   $��7* �67   .+�5X   "   � $��0��6'   	 5Y   &   �� 5Z   *   $��1* �68   �� 5[   *   $��4* �68   �� 5\   *   $��4* �68   .+�5^   &   �� -   5         ��Name$ generateActReport��5^   �   $��1��6-   $��
Act Report��6�   ��6:   �� *
UIFontSize+��6Z   �� *btnGenerateActReport+��5_   2   �� *generalOrderPadding+��6Z   �� *generalOrderPadding+
 5`   *     5a   .   $ 
MouseEnter��6=   . 5b   2      6H   �� *getPrintBtnWikiInformation+5c   /   5e   '   �� 5f   &   -   5        $ generateActAdminReport��5f   �   $��2��6-   $��Admin Report��6�   ��6:   �� *btnGenerateActAdminReport+��6h   �� *
UIFontSize+��5g   1   �� *generalOrderPadding+��6Y   �� *generalOrderPadding+
 5h   *     5i   .   $ 
MouseEnter��6=   . 5j   2      6H   �� *getPrintBtnWikiInformation+5k   /   5m   '   5n   #   � 5p   "   $��1��6'   	 5q   &   �� 5r   *   $��1* �68   �� 5s   *   $��1* �68   .+�5u   &   � $��1��6+   	 5v   *   �� 5w   .   $��1* �6<   �� 5x   .   $��1* �6<    5z   *   �� 5{   .   $��5* �6?   �� 5|   .   $��3* �6?   .+�5~   *   	� $��1��66   $��0��6F    	� 	GroupName$ GroupRadioActReport��6s   $��0 0 2 0q�5   �     WordOrangeBrush#��[ 5�   2   $��Center=�6`   $��Center=�6{     ForegroundVeryDarkBrush#��[ 5�   2   $��0q�6h   ��5~   S   �� *
UIFontSize+ d� 	IsChecked 6�   �� *RadioActReport+ ;   5GeneralOrderUpdateDetails.RadioButtonCheckedConverter  $ 2��$ TwoWay=���5�   .   �� 5�   2   $��This Act��6<    �� Padding$ 2 0 0 0q�6L   $
��Wrap=�6_   
 5�   .     5�   2   $ 
MouseEnter��6A   . 5�   6      6L   �� *" getPrintThisActRBWikiInformation+5�   3   5�   +   	� 5�   *   $��0��67   $��0��6G   $! GroupRadioActCommentsReport��6t   #��[ 5�   2   $��0q�6`   $��0q�6k   #��[ 5�   2   $��Center=�6j   $
��Left=�6�   ��5�   T   �� *
UIFontSize+ 6�   �� *RadioCommentsReport+   $ 2��$ TwoWay=���5�   .   �� 5�   2   $��Current comments only��6<   $ 2 0 0 0q�6Y   $
��Wrap=�6l   
 5�   .     5�   2   $ 
MouseEnter��6A   . 5�   6      6L   �� *" getPrintCurrentRBWikiInformation+5�   3   5�   +   	� 5�   *   $��1��67   $��1��6G   $ GroupRadioActReport��6t   #��[ 5�   2   $��0 0 2 0q�6`   $��0q�6q   #��[ 5�   2   $��Center=�6h   $��Center=�6�   ��5�   T   �� *
UIFontSize+ 6�   �� *RadioActReport+   $ 1��$ TwoWay=���5�   .   �� 5�   2   $��	All Acts ��6<   $ 2 0 0 0q�6M   $
��Wrap=�6`   
 5�   .     5�   2   $ 
MouseEnter��6A   . 5�   6      6L   �� *getPrintAllRBWikiInformation+5�   3   5�   +   	� 5�   *   $��0��67   $��1��6G   $! GroupRadioActCommentsReport��6t   #��[ 5�   2   $��0q�6`   $��0q�6k   #��[ 5�   2   $��Center=�6i   $
��Left=�6�   ��5�   T   �� *
UIFontSize+ 6�   �� *RadioCommentsReport+   $ 1��$ TwoWay=���5�   .   �� 5�   2   $��With new comments��6<   $ 2 0 0 0q�6U   $
��Wrap=�6h   
 5�   .     5�   2   $ 
MouseEnter��6A   . 5�   6      6L   �� *getPrintNewRBWikiInformation+5�   3   5�   +   5�   '   � 5�   &   $��1��6+    5�   *   �� 5�   .   $��1* �6?   �� 5�   .   $��1* �6?   .+�5�   &   	� $��0��62   $ GroupRadioAdminReport��6b   $��1��6�   #��[ 5�   2   $��0q�6`   $��0q�6k   #��[ 5�   2   $��Center=�6j   $
��Left=�6�   ��5�   B   �� *
UIFontSize+ 6�   �� *RadioActAdminReport+   $ 1��$ TwoWay=���5�   *   �� 5�   2   $��Act Administrators��6<   $	��Top=�6V   $ 2 0 0 0q�6o   $
��Wrap=�6�   
 5�   *     5�   .   $ 
MouseEnter��6=   . 5�   2      6H   �� *!getPrintActAdminWikiInformation+5�   /   5�   '   	� 5�   &   $��1��62   $��1��6b   $ GroupRadioAdminReport��6o   #��[ 5�   2   $��0q�6a   $��0q�6l   #��[ 5�   2   $��Center=�6i   $
��left=�6�   ��5�   B   �� *
UIFontSize+ 6�   �� *RadioActAdminReport+   $ 2��$ TwoWay=���5�   *   �� 5�   2   $��Administered Acts��6<   $��Center=�6U   $ 2 0 0 0q�6p   $��NoWrap=�6�   
 5�   *     5�   .   $ 
MouseEnter��6=   . 5�   2      6H   �� *)'getPrintAdministeredActsWikiInformation+5�   /   5�   '   5�   #   5�   #   5�      5�      �� 5�      $��1��6!   $	��Top=�62   $��2q�6J   $��2q�6^   #��[  6i   ._�5�      �  5�   "   �� 5�   &   $��* �67   �� 5�   &   $��* �67   .+�5�   "   �� -   5        $ UploadDetails��5�   ~   $��0��6)   $��Upload��6�   ��69   �� *
UIFontSize+��6Y   �� *btnUploadDetails+��5�   )   �� *generalOrderPadding+
 5�   &     5�   *   $ 
MouseEnter��69   . 5�   .      6D   �� *getUploadBtnWikiInformation+5�   +   5�   #   � 5�   "   $��1��6'   	 5�   &   �� 5�   *   $��1* �68   �� 5�   *   $��1* �68   .+�5�   &   	� $��0��62   $  BooleanGroupRadioUploadAct��6_   #��[ 5�   2   $��Center=�6`   $
��left=�6{   #��[ 5�   2   $��0q�6i   $��0q�6u   ��5�   ?   �� *
UIFontSize+ 6�   �� *RadioUploadAct+   $ 2��$ TwoWay=���5�   *   �� 5�   .   $��This Act��68   $ 2 0 0 0q�6I   $
��Wrap=�6\   
 5�   *     5�   .   $ 
MouseEnter��6=   . 5�   2      6H   �� *getThisActWikiInformation+5�   /   5�   '   	� 5   &   $��1��62   $  BooleanGroupRadioUploadAct��6_   #��[ 5  2   $��Center=�6`   $
��left=�6{   #��[ 5  2   $��0q�6i   $��0q�6u   ��5   ?   �� *
UIFontSize+ 6�   �� *RadioUploadAct+   $ 1��$ TwoWay=���5  *   �� 5  .   $��All Acts��68   $ 2 0 0 0q�6I   $
��Wrap=�6\   
 5  *     5  .   $ 
MouseEnter��6=   . 5	  2      6H   �� *getAllActsWikiInformation+5
  /   5  '   5  #   5     5     �� 5     $��2��6%   .+�5     � 	 5  "   �� 5  &   $��* �64   �� 5  &   $��* �64   .+�5  "   �� -   5        $ btnDiscardPending��5  |   $��0��6I   $��Discard Pending��6�   ��6)   �� *
UIFontSize+��6V   �� *btnDiscardPending+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+
 5  &     5  *   $ 
MouseEnter��69   . 5  .      6D   �� *getBackBtnWikiInformation+5  +   5  #   �� 5  "   -   5        $ 	btnCancel��5  t   $��1��6I   $��Close��6�   ��6)   �� *
UIFontSize+��6V   �� *	btnCancel+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+
 5  &     5   *   $ 
MouseEnter��69   . 5!  .      6D   �� *getCancelBtnWikiInformation+5"  +   5$  #   5%     5&     5(     5+     5-     "  FontAwesome.WPF.ImageAwesome  5.     $��0��6      Icon$ Refresh=�6/     ForegroundDarkBrush   
Foreground# [ 6>      SpinDuration$ 2Q�51  2   $��40��6C   $��40��6N   y�5/  2   �� $ spinnerBool� 4	   .GeneralOrderUpdateDetails.ImageSourceConverter	  $ false��$ TwoWay=�   Spin 50  2   �� $ spinnerBool�51  N   $
   GeneralOrderUpdateDetails.Wiki
  52     
�������$��1��6H   $	��Top=�6X   ��6   �� *
UIHeight+6X   55     57     59     