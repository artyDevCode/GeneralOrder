   M S B A M L   `   `   `  ���� T  PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   CourThin  CourRegular  CourBold  	BaseStyle  �� 
TargetType �� Property �� Value  BaseTextBlockStyle  
FontSize10  
FontSize11  
FontSize12  
FontSize14 	 
FontSize16 
 
FontSize20  
FontSize25  BackgroundDarkBrush  ForegroundLightBrush  BackgroundLight  BackgroundLightBrush �� Color  BackgroundDark  BackgroundVeryLight  BackgroundVeryLightBrush  ForegroundMain  ForegroundMainBrush  ForegroundVeryDark  ForegroundVeryDarkBrush  ForegroundLight  ForegroundDark  ForegroundDarkBrush  WordBlue  WordBlueBrush  WordLightBlue  WordLightBlueBrush  WordVeryLightBlue  WordVeryLightBlueBrush   WordDarkBlue ! WordDarkBlueBrush " 
WordOrange # WordOrangeBrush $ WordRed % WordRedBrush & 	WordGreen ' WordGreenBrush ( WindowColor ) ContentAreaColorLight * ContentAreaColorDark + DisabledControlLightColor , DisabledControlDarkColor - DisabledForegroundColor . SelectedBackgroundColor / SelectedUnfocusedColor 0 ControlLightColor 1 ControlMediumColor 2 ControlDarkColor 3 ControlMouseOverColor 4 ControlPressedColor 5 
GlyphColor 6 GlyphMouseOver 7 BorderLightColor 8 BorderMediumColor 9 BorderDarkColor : PressedBorderLightColor ; PressedBorderDarkColor < DisabledBorderLightColor = DisabledBorderDarkColor > DefaultBorderBrushDarkColor ? HeaderTopColor "@ DatagridCurrentCellBorderColor A SliderTrackDarkColor B NavButtonFrameColor C MenuPopupBrush �� EndPoint �� 
StartPoint � Color � Offset $D  ProgressBarIndicatorAnimatedFill E CustomWindow F RadioButton �� BasedOn G GeneralButton	 �� 
TargetType
 ��Name �� CornerRadius J� SnapsToDevicePixels �� Triggers :� RoutedEvent	 �� To g� Duration �� 
TargetName �� TargetProperty �� From P� Property P� Value �� 
TargetName H WindowControlButton'   !System.Windows.Shell.WindowChrome �� ScaleX �� Padding �� Triggers I WindowCloseButton J 
HeaderText K RegularText ��Name ��Name J� IsHitTestVisible
 � Tag �� Padding �� Binding  �� RelativeSource! �� Value L ComboBoxNormalBorderBrush !M ComboBoxNormalBackgroundBrush #N ComboBoxDisabledForegroundBrush #O ComboBoxDisabledBackgroundBrush P ComboBoxDisabledBorderBrush  Q ComboBoxToggleButtonTemplate" � ColumnDefinitions# �� 	ClickMode$ d� 	IsChecked% �� Path& �� Mode' �� SelectionBoxItem( �� SelectionBoxItemTemplate) �� 
IsReadOnly* �� 
IsReadOnly+ � AllowsTransparency, �� IsDropDownOpen- � ActualWidth. �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation/   DirectionalNavigation�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     1local'clr-namespace:GeneralOrderUpdateDetails  1system&clr-namespace:System;assembly=mscorlib  %M  &	        &	 +     &	 X     &	 �     7   &	 �     7   &	 �     &	 �     &	 �     &	 �     &		 �     &	
 �     &	     '��     7  7  &	 b    &	 s    7  &	 }    &	 �    7  &	 �    &	 �    7  &	 �    &	 �    7  &	 �    &	 �    7  &	 �    &	 �    7  &	     &	     7  &	     &	 0    7  &	 :    &	 K    7  &	 U    &	 f    7  &	  p    &	! �    7   &	" �    &	# �    7 " &	$ �    &	% �    7 $ &	& �    &	' �    7 & &	( �    &	) �    &	*     &	+     &	, (    &	- ;    &	. N    &	/ a    &	0 t    &	1 �    &	2 �    &	3 �    &	4 �    &	5 �    &	6 �    &	7     &	8 *    &	9 =    &	: P    &	; c    &	< v    &	= �    &	> �    &	? �    &	@ �    &	A �    &	B �    &	C �    &	D Q    &	E �    &	F �    7  7 # 7  7  7  '	� j    7  7 # 7  7  7  &	G 
    7  7 # 7  7  7  7  7  7  '�� K
    7 G &	H Z
    7  7  7  &	I �    7 H 7  &	J     7  7  7  7  &	K �    7  7  7  7  '�� `    7  7  7  7  7  7  7   7  &	L �    &	M �    &	N �    &	O �    &	P     &	Q     7 L 7 M 7 M 7 O 7 O 7 P 7 N '�� �    7  7  7  7 Q 7 L 7 N *�%!pack://appliction;,,,/Fonts/#COUR)�*�'#pack://appliction;,,,/Fonts/#COURDB)�*�'#pack://appliction;,,,/Fonts/#COURBI)��� "  ��.���  � ��8   �� "  ��.���  � ��8   X�10Q�X�11Q�X�12Q�X�14Q�X�16Q�X�20Q�X�25Q��� "  ��.���  � ��$ Center=���  � ��8   ��  � ��8  ��#efefef���� 8   ��#bdbdbd���� 8   ��#fafafa���� 8   ��#686868���� 8   ��#000000���� 8   ��#ffffff���� 8   ��#bdbdbd���� 8   ��#3099c5���� 8   ��#45b6e5���� 8   ��#ecf6fb���� 8   ��#0c6991���� 8   ��#ffa800���� 8   ��#ff4747���� 8   ��#00c541���� 8   ��	#FFE8EDF9����	#FFC5CBF9����	#FF7381F9����	#FFE8EDF9����	#FFC5CBF9����	#FF888888����	#FFC5CBF9����	#FFDDDDDD����	White����	#FF7381F9����	#FF211AA9����	#FF3843C4����	#FF211AA9����	#FF444444����/+sc#1, 0.004391443, 0.002428215, 0.242281124����	#FFCCCCCC����	#FF888888����	#FF444444����	#FF888888����	#FF444444����	#FFAAAAAA����	#FF888888����	Black����	#FFC5CBF9����	Black����	#FFC5CBF9����	#FF3843C4���� $ 0.5,1&�$ 0.5,0&�.��� # � 0 $ 0Q�� # � 1 $	 0.5Q�� # � 0 $ 1Q��� $	 0,0&�$	 1,0&���� � $ 	#000000FF��$ 0Q�� $ 	#600000FF��$	 0.4Q�� $ 	#600000FF��$	 0.6Q�� $ 	#000000FF��$ 1Q��� "  �.���  �@�WindowStyle$
 None=���  �@�AllowsTransparency . ��  �@�
ResizeMode$ CanMinimize=���  � ��
 �������  � ��
 � � ���  � ��$ 5q���  � ��
 �  ���� "  	�8   .���  � ��8  ��  � ��8  ��  � ��$ 0q���  � ��8  ��  � ��8  ��  � ��$ 50 10q���  � ��$
 0 10q��� "  	�8   .���  � ��8  ��  � ��8  ��  � ��$ 0q���  � ��8  ��  � ��8  ��  � ��$ 50 10q���  � ��$
 0 10q��� "  ��8   .���  � ��8  ��  � ��8  ��  � ��$ 0q���  � ��8  ��  � ��8  ��  � ��$ 50 10q���  � ��$
 0 10q���  � �� �� "	 ��.R��� $
 border��$ 10��#��z��#��z��#��z�� . ._�� .+��� #��z��|�.  #��z��#��z��#��z��#��z��# z #��z�� :� $ 
MouseEnter��.[��� .���� .���� 8  $ 0:0:0.3D�$ border��$ Background.Color�:� $ 
MouseLeave��.[��� .���� .���� 8  $ 0:0:0.3D�$ border��$ Background.Color�P�  � {� .  . ��� $ border��8   � ���� "  ��8   �� "  ��8   .���   �@  IsHitTestVisibleInChrome . ��  � ��
 ���� ��  � ��$ 2q���  � ��$ Stretch=���  � ��8  ��  �@�LayoutTransform �� $	 1.5Q���  � �� �� "	 ��.R��� # z��#��z��._��� $��Center=�$��Center=�#��z�� P�  �@J�IsMouseOver . . ���  � ��8  �� "  ��8    P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��8  �� "  ��8   .���  � ��8  ��  � ��8  ��  � ��8  ��  � ��$ Center=���  � ��$ Center=���  � ��$	 0 6q��� "  ��8   .���  � ��8  ��  � ��8  ��  � ��8  ��  � ��$ Center=���  � ��$
 Left=���  �@��TextAlignment$
 Left=���  � ��$	 0 6q��� "  ��8   .���  � ��8  ��  � ��8  ��  � ��$ 10q���  � ��$ 0 5 0 5q���  � ��8  ��  � ��$ 0 0 0 1q���  � ��$
 Left=���  � ��$ Bottom=���  �@��TextAlignment$ Center=���  � ��8  ��  � ��8  ��  � �� �� "	 ��.R�� .+��� $
 border��#��z��#��z��#��z�� . ._��� $ PART_ContentHost��|�.  $��Hidden=�$��Hidden=��� $ placeholder�� .  #��z 8�� # z��$��Center=�#��z��8�� ���� "  ��.���  � y�$ 	Collapsed=� ��  �� *Text+  �� *TemplatedParent+!  .a���  � y�$ Visible=� P�  � {� .  . ��� $ border�� �@J�Opacity$
 0.56Q�P�  �@J�IsMouseOver . . ��� $ border�� � �� �	#FF7EB4EAP�  �@J�IsKeyboardFocused . . ��� $ border�� � �� �	#FF569DE5�� $ #e3e9ef���� $
 #fff���� $
 #888���� $
 #eee���� $
 #888���� "	 d�.R�� " �� �� $��20 �.+��� $��Border��$��2��8��  $ 0��$��
1, 1, 1, 1q�8�� �� $��ButtonBorder��$��1��$��
1, 1, 1, 1q����#444$ 
0, 0, 0, 0��$��
0, 0, 0, 0q�8�� F� $��Arrow��$��1��=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=����#444$��Center=� P�  �@J�IsMouseOver . . ��� $ ButtonBorder�� � ��
 �����P�  �@d�	IsChecked$
 TrueY�. ��� $ ButtonBorder�� � ��
 ������� $ Arrow�� � �� �	#FF8D979EP�  � {� .  . ��� $ Border��8   � ���� $ ButtonBorder��8   � ���� $ ButtonBorder��8   � ���� 8   � ���� $ Arrow�� � �� �#999�� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � ��8   ��  � ��8  ��  � ��8  ��  �@�FocusVisualStyle Z� ��  � �� �� "	 ��.R�� .+�d� $��ToggleButton��$��2��$# Press=�|�.  8�� $ �� $% IsDropDownOpen�  �� *TemplatedParent+$& TwoWay=��� $��ContentSite��$��5, 3, 23, 3q� .  $
��Left=�$��Center=�#��z' #��z( #��z���� $��PART_EditableTextBox��$��3, 3, 23, 3q�#* z) $y�Hidden=�
������ $
��Left=�$��Center=�|�. .������ "	 ��.R��� $��PART_ContentHost��|�.  .��� $��Popup��$��Bottom=�|�.  + . #��z, $��Slide=�.��� $��DropDown�� . #��z- #��z. .+��� $��DropDownBorder��
�������$��
0, 1, 0, 0q�$ 0��$��1,1,1,1q�8�� �� $��4q� . .���� $/ 	Contained=� P�  �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P�  � {� .  . ��� 8   � ��P�  �@��
IsGrouping . . ���  � �� .  P�  �@��
IsEditable . . ���  �@ 	IsTabStop .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=�