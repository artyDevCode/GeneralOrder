   M S B A M L   `   `   `  ���� T  PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails  7    1GeneralOrderUpdateDetails.GOActAdminDetailsUpdate   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:GeneralOrderUpdateDetails     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  ���� *
UIHeight+���� *	UIWidth+  � 	Resources   �� MergedDictionaries �� Source��  �� $T Npack://application:,,,/GeneralOrderUpdateDetails;component/Styles/UIStyle.xaml?�%    .���� $	��Top=�.+��  � ColumnDefinitions �� $��* ��� $	��200 �.+���  �� Padding$ 1q�   WordBlueBrush#��[  ._�� 
�������{���  �� Path$ spinnerBool� �� 	Converter 6   0GeneralOrderUpdateDetails.ChangeBooleanConverter   �� ConverterParameter$ false�� �� Mode$ TwoWay=�	 � RowDefinitions	 �� $��1* ��� $��1* ��� $��1* ��� $��1* �.+�� $��0 0 0 0q�$��0��.+���   
HeaderText#��[   CourRegular#��[ $)��#Act Administration - Details Update��$��Center=�$��0 10 0 5q����� *
UIFontSize+.  (System.Windows.Interactivity.Interaction
   Triggers
 /  )System.Windows.Interactivity.EventTrigger     	EventName$ 
MouseEnter��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *getApplicationWikiInformation+� $��0 0 0 0q�$��1�� �� $��2* ��� $��2* ��� $	��15* ��� $��4* �.+���   RegularText#��[ $��Bottom=�$	��Act��$��0��$��5 2 2 5q����� *
UIFontSize+�� -    ��Name$ txtgeneralOrderILD��$��1��$��0q�$��2 2 2 5q����� *
UIFontSize+���� *generalOrderILD+��  �� DisplayMemberPath$ ActTitle�� �� SelectedValuePath$ ActTitleILDNumber��$��2��$��0q�$��2q����� *	actList+���� *
UIFontSize+ �� SelectedIndex �� *actSelectedIndex+$ TwoWay=� �� SelectedItem �� *cBSelectedActValue+$ TwoWay=�
   $ SelectionChanged��.    �� *selectActCB+  $ 
MouseEnter��.    �� *getActWikiInformation+�� -   $ generalOrderActNumber��$��3��$��0q�$��2 2 2 5q����� *
UIFontSize+���� *generalOrderActNumber+)   #GeneralOrderUpdateDetails.GOListBox  $��0 0 0 2q�$��2��� $��0 0 0 0q�$��3�� �� $��5* ��� $��2* ��� $��2* �.+��� $��2q�$��2q�$��0��$ 0q�#��[  ._��  �� $��2* ��� $��4* �.+�� $��0��	 �� $��1* ��� $��4* ��� $��4* �.+��� -    ��Name$ generateActReport��$��1��$��
Act Report������ *
UIFontSize+���� *btnGenerateActReport+���� *generalOrderPadding+���� *generalOrderPadding+
   $ 
MouseEnter��.    �� *getPrintBtnWikiInformation+�� -   $ generateActAdminReport��$��2��$��Admin Report������ *btnGenerateActAdminReport+���� *
UIFontSize+���� *generalOrderPadding+���� *generalOrderPadding+
   $ 
MouseEnter��.    �� *getPrintBtnWikiInformation+� $��1��	 �� $��1* ��� $��1* �.+�� $��1��	 �� $��1* ��� $��1* � �� $��2* ��� $��1* �.+�	� $��1��$��0�� 	� 	GroupName$ GroupRadioActReport��$��0 0 2 0q�  WordOrangeBrush#��[ $��Center=�$��Right=�  ForegroundVeryDarkBrush#��[ $��0q����� *
UIFontSize+ d� 	IsChecked �� *RadioActReport+ ;   5GeneralOrderUpdateDetails.RadioButtonCheckedConverter  $ 2��$ TwoWay=����� $��This Act�� �� Padding$ 2 0 0 0q�$
��Wrap=�
   $ 
MouseEnter��.    �� *" getPrintThisActRBWikiInformation+	� $��0��$��0��$! GroupRadioActCommentsReport��#��[ $��0q�$��0q�#��[ $��Center=�$
��Left=����� *
UIFontSize+ �� *RadioCommentsReport+   $ 2��$ TwoWay=����� $��Current comments only��$ 2 0 0 0q�$
��Wrap=�
   $ 
MouseEnter��.    �� *" getPrintCurrentRBWikiInformation+	� $��1��$��1��$ GroupRadioActReport��#��[ $��0 0 2 0q�$��0q�#��[ $��Center=�$��Right=����� *
UIFontSize+ �� *RadioActReport+   $ 1��$ TwoWay=����� $��	All Acts ��$ 2 0 0 0q�$
��Wrap=�
   $ 
MouseEnter��.    �� *getPrintAllRBWikiInformation+	� $��0��$��1��$! GroupRadioActCommentsReport��#��[ $��0q�$��0q�#��[ $��Center=�$
��Left=����� *
UIFontSize+ �� *RadioCommentsReport+   $ 1��$ TwoWay=����� $��With new comments��$ 2 0 0 0q�$
��Wrap=�
   $ 
MouseEnter��.    �� *getPrintNewRBWikiInformation+� $��1�� �� $��1* ��� $��1* �.+�	� $��0��$ GroupRadioAdminReport��$��1��#��[ $��0q�$��0q�#��[ $��Center=�$
��Left=����� *
UIFontSize+ �� *RadioActAdminReport+   $ 1��$ TwoWay=����� $��Act Administrators��$��Center=�$ 2 0 0 0q�$
��Wrap=�
   $ 
MouseEnter��.    �� *!getPrintActAdminWikiInformation+	� $��1��$��1��$ GroupRadioAdminReport��#��[ $��0q�$��0q�#��[ $��Center=�$
��left=����� *
UIFontSize+ �� *RadioActAdminReport+   $ 2��$ TwoWay=����� $��Administered Acts��$��Center=�$ 2 0 0 0q�$��NoWrap=�
   $ 
MouseEnter��.    �� *)'getPrintAdministeredActsWikiInformation+�� $��1��$	��Top=�$��2q�$��0q�#��[  ._��  �� $��* ��� $��* �.+��� -   $ UploadDetails��$��0��$��Upload������ *
UIFontSize+���� *btnUploadDetails+���� *generalOrderPadding+
   $ 
MouseEnter��.    �� *getUploadBtnWikiInformation+� $��1��	 �� $��1* ��� $��1* �.+�	� $��0��$  BooleanGroupRadioUploadAct��#��[ $��Center=�$
��left=�#��[ $��0q�$��0q����� *
UIFontSize+ �� *RadioUploadAct+   $ 2��$ TwoWay=����� $��This Act��$ 2 0 0 0q�$
��Wrap=�
   $ 
MouseEnter��.    �� *getThisActWikiInformation+	� $��1��$  BooleanGroupRadioUploadAct��#��[ $��Center=�$
��left=�#��[ $��0q�$��0q����� *
UIFontSize+ �� *RadioUploadAct+   $ 1��$ TwoWay=����� $��All Acts��$ 2 0 0 0q�$
��Wrap=�
   $ 
MouseEnter��.    �� *getAllActsWikiInformation+�� $��2��.+�� 	 �� $��* ��� $��* �.+��� -   $ btnDiscardPending��$��0��$��Discard Pending������ *
UIFontSize+���� *btnDiscardPending+���� *generalOrderPadding+���� *generalOrderPadding+
   $ 
MouseEnter��.    �� *getBackBtnWikiInformation+�� -   $ 	btnCancel��$��1��$��Close������ *
UIFontSize+���� *	btnCancel+���� *generalOrderPadding+���� *generalOrderPadding+
   $ 
MouseEnter��.    �� *getCancelBtnWikiInformation+"  FontAwesome.WPF.ImageAwesome  $��0��   Icon$ Refresh=�  ForegroundDarkBrush   
Foreground# [    SpinDuration$ 2Q�$��40��$��40��y��� $ spinnerBool� 4	   .GeneralOrderUpdateDetails.ImageSourceConverter	  $ false��$ TwoWay=�   Spin �� $ spinnerBool�$
   GeneralOrderUpdateDetails.Wiki
  
�������$��1��$	��Top=����� *
UIHeight+