   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      G CGeneralOrder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GeneralOrderGeneralOrder 5	      K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore 5
      %   GeneralOrder.GODepartmentForAct   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      3System&clr-namespace:System;assembly=mscorlib  5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      &localclr-namespace:GeneralOrder  5	      Ccore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore  5
      ��5      �� *
UIHeight+��5      �� *	UIWidth+.��5      �� .+�5   
   �   � ColumnDefinitions  5      �� 5      $��* �6#   �� 5      $	��200 �6#   .+�5      ��  �� Padding$ 1q�6      WordBlueBrush#��[  6"   ._�5      � 
�������$��0��6+   {�6;   ��  �� Path$ spinnerBool� �� 	Converter )  #GeneralOrder.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=� � RowDefinitions 5      �� 5      $��1* �6(   �� 5      $��1* �6(   �� 5      $��1* �6(   �� 5       $��1* �6(   �� 5!      $	��15* �6(   �� 5"      $��1* �6(   .+�5$      � $��0 0 0 0q�6   $��0��6-   .+�5%      ��   
HeaderText#��[ 6%     CourRegular#��[ 6i   $4��.Act Administration - Select Department for Act��6�   $��Center=�6�   $��	0 20 0 10q�6�   ��6I   �� *
UIFontSize+.  (System.Windows.Interactivity.Interaction   Triggers 5&      /  )System.Windows.Interactivity.EventTrigger  5'   "      	EventName$ 
MouseEnter��61   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 5(   &   6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 6<   �� *getApplicationWikiInformation+5)   #   5+      5,      � 5.      $��0 0 0 0q�6   $��1��6-     5/      �� 50      $��1* �6/   �� 51      $��4* �6/   .+�53      ��   RegularText#��[ 6$   $��	Portfolio��6i   $��0��6z   $
��10 0q�6�   ��6I   �� *
UIFontSize+6�   �� 54       �� DisplayMemberPath$ PortfolioName��55   $    �� SelectedValuePath$ PortfolioId��57   $   $��1��6D   ��54   $   �� *portfolioList+��55   F   �� *
UIFontSize+ �� SelectedIndex 56   $   �� *portfolioSelectedIndex+$ TwoWay=� �� SelectedItem 58   $   �� *cBSelectedPortfolioValue+$ TwoWay=� 59        5:   "   $ SelectionChanged��61   .	 5;   &     
 6<   �� *selectPortfolioCB+5<   #     5=   "   $ 
MouseEnter��61   .	 5>   &     
 6<   �� *getPortfolioWikiInformation+5?   #   5A      5B      � 5D      $��0 0 0 0q�6   $��2��6-     5E      �� 5F      $��1* �6/   �� 5G      $��4* �6/   .+�5I      �� #��[ 6%   $	��Act��6j   $��0��6u   $
��10 0q�6�   $
��Auto��6�   ��6J   �� *
UIFontSize+6�   �� 5J      $ ActTitle��5K   $   $ ActTitleILDNumber��5M   $   $��1��6J   ��5J   $   �� *	actList+��5K   A   �� *
UIFontSize+ 5L   $   �� *actSelectedIndex+$ TwoWay=� 5N   $   �� *cBSelectedActValue+$ TwoWay=� 5O        5P   "   $ SelectionChanged��61   .	 5Q   &     
 6<   �� *selectActCB+5R   #     5S   &   $ 
MouseEnter��65   .	 5T   *     
 6@   �� *getActWikiInformation+5U   '   5W      5X      � 5Z      $��0 0 0 0q�6   $��3��6-     5[      �� 5\      $��1* �6/   �� 5]      $��4* �6/   .+�5_      �� #��[ 6%   $��
Department��6j   $��0��6|   $
��10 0q�6�   $
��Auto��6�   ��6J   �� *
UIFontSize+6�   �� 5`      $ 
Department��5a   $   $ DepartmentTitle��5c   $   $��1��6H   ��5`   $   �� *
deptList+��5a   C   �� *
UIFontSize+ 5b   $   �� *deptSelectedIndex+$ TwoWay=���5c   X   �� *generalOrderPadding+ 5d   $   �� *cBSelectedDeptValue+$ TwoWay=� 5e        5f   "   $ SelectionChanged��61   .	 5g   &     
 6<   �� *selectDeptCB+5h   #     5i   &   $ 
MouseEnter��65   .	 5j   *     
 6@   �� *getDepartmentWikiInformation+5k   '   5m      5n      � 5p      $��0 0 0 0q�6   $��4��6-     5q      �� 5r      $��1* �6/   �� 5s      $��4* �6/   .+�5u      �  5v      �� 5w   "   $��1* �60   �� 5x   "   $	��10* �60   .+�5{      �� #��[ 6)   $	��Top=�6N   $��Comments��6f   $��0��6�   $��0��6�   $
��10 0q�6�   $
��Auto��6�   ��6v   �� *
UIFontSize+6�   5|      *  $GeneralOrderCore.BindableRichTextBox  5}      -   5          Name$! departmentForActRichTextBox��5}   3   $��1��5~   3    �� 
IsReadOnly . " �� VerticalScrollBarVisibility$ Visible=�6V     ForegroundMainBrush#��[ 5�   4      Document 5}   X   �� *	flowDoc+$ TwoWay=���5   3   �� *#!departmentForActRichTextBoxHeight+��6j   �� *generalOrderPadding+ 5�        5�   "   $ 
MouseEnter��61   .	 5�   &     
 6<   �� *getCommentsWikiInformation+5�   #    J� InputBindings 5�      �� 5�   "   
 �� Key$	 Tab��5�   %    �� Command 5�   %   �� *insertTextRTB+�� 5�   "   $ Return��5�   %    5�   %   �� *insertTextRTBblock+5�      5�      � 5�      $��0 0 0 0q�6   $��5��6-     5�      �� 5�      $��* �6/   �� 5�      $��2* �6/   �� 5�      $��* �6/   .+�5�      �� -   5         ��Name$ btnImportFile��5�   c   $��Save and return��6z   $��0��6�   ��6!   �� *BackAndSave+��6C   �� *
UIFontSize+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+ 5�        5�   "   $ 
MouseEnter��61   .	 5�   &     
 6<   �� *getBackAndSaveWikiInformation+5�   #   5�      �� 5�      -   5        $ btnCaeeencelk��5�   ]   $��Close��6t   $��4��6�   ��6"   �� *Close+��6=   �� *
UIFontSize+��6�   �� *generalOrderPadding+��6�   �� *generalOrderPadding+ 5�        5�   "   $ 
MouseEnter��61   .	 5�   &     
 6<   �� *getCloseWikiInformation+5�   #   5�      5�      5�      5�      "  FontAwesome.WPF.ImageAwesome  5�      $��0��6      Icon$ Refresh=�6/     ForegroundDarkBrush   
Foreground# [ 6>   $��Center=�5�   T   $��Center=�6o      SpinDuration$ 2Q�5�   2   $��40��6C   $��40��6N   y�5�   2   �� $ spinnerBool� '  !GeneralOrder.ImageSourceConverter  $ false��$ TwoWay=�   Spin 5�   2   �� $ spinnerBool�5�   N   	  GeneralOrder.Wiki	  5�      
�������$��1��6H   $	��Top=�6X   ��6   �� *
UIHeight+6X   5�      5�      5�      