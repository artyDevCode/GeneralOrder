   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  G CGeneralOrder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GeneralOrderGeneralOrder K GGeneralOrderCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCoreGeneralOrderCore %   GeneralOrder.GODepartmentForAct   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  3System&clr-namespace:System;assembly=mscorlib  Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  &localclr-namespace:GeneralOrder  Ccore8clr-namespace:GeneralOrderCore;assembly=GeneralOrderCore  ���� *
UIHeight+���� *	UIWidth+.���� .+��   � ColumnDefinitions  �� $��* ��� $	��200 �.+���  �� Padding$ 1q�   WordBlueBrush#��[  ._�� 
�������$��0��{���  �� Path$ spinnerBool� �� 	Converter )  #GeneralOrder.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=� � RowDefinitions �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $	��15* ��� $��1* �.+�� $��0 0 0 0q�$��0��.+���   
HeaderText#��[   CourRegular#��[ $4��.Act Administration - Select Department for Act��$��Center=�$��	0 20 0 10q����� *
UIFontSize+.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ 
MouseEnter��.  (System.Windows.Interactivity.TriggerBase	   Actions.	 6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 �� *getApplicationWikiInformation+� $��0 0 0 0q�$��1��  �� $��1* ��� $��4* �.+���   RegularText#��[ $��	Portfolio��$��0��$
��10 0q����� *
UIFontSize+��  �� DisplayMemberPath$ PortfolioName�� �� SelectedValuePath$ PortfolioId��$��1������ *portfolioList+���� *
UIFontSize+ �� SelectedIndex �� *portfolioSelectedIndex+$ TwoWay=� �� SelectedItem �� *cBSelectedPortfolioValue+$ TwoWay=�   $ SelectionChanged��.	   
 �� *selectPortfolioCB+  $ 
MouseEnter��.	   
 �� *getPortfolioWikiInformation+� $��0 0 0 0q�$��2��  �� $��1* ��� $��4* �.+��� #��[ $	��Act��$��0��$
��10 0q�$
��Auto������ *
UIFontSize+�� $ ActTitle��$ ActTitleILDNumber��$��1������ *	actList+���� *
UIFontSize+ �� *actSelectedIndex+$ TwoWay=� �� *cBSelectedActValue+$ TwoWay=�   $ SelectionChanged��.	   
 �� *selectActCB+  $ 
MouseEnter��.	   
 �� *getActWikiInformation+� $��0 0 0 0q�$��3��  �� $��1* ��� $��4* �.+��� #��[ $��
Department��$��0��$
��10 0q�$
��Auto������ *
UIFontSize+�� $ 
Department��$ DepartmentTitle��$��1������ *
deptList+���� *
UIFontSize+ �� *deptSelectedIndex+$ TwoWay=����� *generalOrderPadding+ �� *cBSelectedDeptValue+$ TwoWay=�   $ SelectionChanged��.	   
 �� *selectDeptCB+  $ 
MouseEnter��.	   
 �� *getDepartmentWikiInformation+� $��0 0 0 0q�$��4��  �� $��1* ��� $��4* �.+��  �� $��1* ��� $	��10* �.+��� #��[ $	��Top=�$��Comments��$��0��$��0��$
��10 0q�$
��Auto������ *
UIFontSize+*  $GeneralOrderCore.BindableRichTextBox  -     Name$! departmentForActRichTextBox��$��1�� �� 
IsReadOnly . " �� VerticalScrollBarVisibility$ Visible=�  ForegroundMainBrush#��[    Document �� *	flowDoc+$ TwoWay=����� *#!departmentForActRichTextBoxHeight+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getCommentsWikiInformation+ J� InputBindings �� 
 �� Key$	 Tab�� �� Command �� *insertTextRTB+�� $ Return�� �� *insertTextRTBblock+� $��0 0 0 0q�$��5��  �� $��* ��� $��2* ��� $��* �.+��� -    ��Name$ btnImportFile��$��Save and return��$��0������ *BackAndSave+���� *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getBackAndSaveWikiInformation+�� -   $ btnCaeeencelk��$��Close��$��4������ *Close+���� *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+   $ 
MouseEnter��.	   
 �� *getCloseWikiInformation+"  FontAwesome.WPF.ImageAwesome  $��0��   Icon$ Refresh=�  ForegroundDarkBrush   
Foreground# [ $��Center=�$��Center=�   SpinDuration$ 2Q�$��40��$��40��y��� $ spinnerBool� '  !GeneralOrder.ImageSourceConverter  $ false��$ TwoWay=�   Spin �� $ spinnerBool�	  GeneralOrder.Wiki	  
�������$��1��$	��Top=����� *
UIHeight+