   M S B A M L   `   `   `  ����G  CGeneralOrder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GeneralOrderGeneralOrder  5      K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore 5
      (    "GeneralOrder.GOImportFileSelectAct   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:GeneralOrder     5      Icore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore     5
      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5        � WindowStartupLocation$  CenterScreen=�5      
������� � Title$ Act Title Check��5      $	��300��5      $	��600��6   .��5      � $��15q�6    � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5      $��50 �6   �� 5      $��30 �6   .+�5   
   ��    RegularText#��[  6   $��0��6Y   $��0��6f   $��0q�6v    �� TextAlignment$
 Left=�6�    �� Padding$ 0q�6�   $
��Wrap=�5      $����An Act Title has not been matched against the Database. Please type the Act Title in the text box and an autocomplete list box will appear. Select the appropriate Act Title.  ��6(   ��5   9   �� *
UIFontSize+5   (   � 5   
   $��0 0 0 0q�6   $��1��6     5      �� 5      $��1* �6#   �� 5      $��7* �6#   .+�5!      �� #��[  6   $��Verify Act: ��6_   $��0��6s   ��6>   �� *
UIFontSize+6s   �� 5"      #��[  6   
���  ��$��Center=�5#      $��1��68   ��5"   ?   �� *
UIFontSize+��6q   �� *verifyActText+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5#   8   5$      � 5%   
   $��0 0 0 0q�6   $��2��6     5&      �� 5'      $��1* �6#   �� 5(      $��7* �6#   .+�5*      �� #��[  6   $	��Act��5+      $��0��6(   $
��10 0q�68   $��25��6F   ��5*   >   �� *
UIFontSize+5+   F   %  GeneralOrderCore.ComboBoxHelper  5-      $ �� IsSynchronizedWithCurrentItem$ falseY�6B   	 �� DisplayMemberPath$	 ActTitle��5.   $   
 �� 
IsEditable
 .   �� MaxDropDownHeight$	 200��5/   \    �� IsTextSearchEnabled .   �� SelectedValuePath$ ActTitleILDNumber��51   $   $��1��6J   ��5-   "   �� *	actList+��5.   B   �� *
UIFontSize+ �� SelectedIndex 5/   $   �� *actSelectedIndex+$ TwoWay=� �� IsDropDownOpen 50   @   �� *cBShowItems+$ TwoWay=�$ PropertyChanged=���51   [   �� *generalOrderPadding+ �� SelectedItem 52   $   �� *cBSelectedActValue+$ TwoWay=�$ PropertyChanged=�.  (System.Windows.Interactivity.Interaction   Triggers 53      /  )System.Windows.Interactivity.EventTrigger  54         	EventName$ SelectionChanged��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 55      6  0System.Windows.Interactivity.InvokeCommandAction     Command 60   �� *selectActCB+56      58      �� 59      -   5        $��	txtAnswer��59   !   $��0q�6   $��0q�62    �� TextAlignment$
 Left=�6>   $��Center=�6S   
�������
�������$��2q�5;   E   $��1��6Y   $��25��5<      ��5:      �� *
UIFontSize+��5;   i   �� *selectTextBoxText+$ TwoWay=�$ PropertyChanged=�.��5<   '    5=        5>      $ TextChanged��6%   . 5?         60   �� *selectActTextBox+5@      .��5A   *   5B      5C      �� 5E   
   -   5         ��Name$ btnClose��5E   n   $��4��6   $��0��6   $��Save and Close��6�   ��61   �� *
btnClose+��6N   �� *
UIFontSize+��5F      �� *generalOrderPadding+��6>   �� *generalOrderPadding+5G      5I      5J      