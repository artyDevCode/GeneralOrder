   M S B A M L   `   `   `  ����T  PGeneralOrderUpdateDetails, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GeneralOrderUpdateDetailsGeneralOrderUpdateDetails  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   CourThin  CourRegular  CourBold  	BaseStyle  �� 
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
IsReadOnly+ � AllowsTransparency, �� IsDropDownOpen- � ActualWidth. �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation/   DirectionalNavigation�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      1local'clr-namespace:GeneralOrderUpdateDetails  5      1system&clr-namespace:System;assembly=mscorlib  5      %�/  &	        &	 9     &	 t     &	 �     7   &	 �     7   &	 7    &	 Q    &	 k    &	 �    &		 �    &	
 �    &	 �    '�� �    7  7  &	 y    &	 �    7  &	 �    &	 �    7  &	 �    &	     7  &	     &	 =    7  &	 U    &	 t    7  &	 �    &	 �    7  &	 �    &	 �    7  &	 �    &	     7  &	 1    &	 P    7  &	 h    &	 �    7  &	  �    &	! �    7   &	" �    &	# �    7 " &	$     &	% ,    7 $ &	& D    &	' c    7 & &	( {    &	) �    &	* �    &	+ �    &	, �    &	-      &	. A    &	/ b    &	0 �    &	1 �    &	2 �    &	3 �    &	4     &	5 $    &	6 E    &	7 �    &	8 �    &	9 �    &	: �    &	;     &	< -    &	= N    &	> o    &	? �    &	@ �    &	A �    &	B �    &	C 	    &	D �	    &	E �
    &	F W    7  7 # 7  7  7  '	� p    7  7 # 7  7  7  &	G �    7  7 # 7  7  7  7  7  7  '�� �    7 G &	H      7  7  7  &	I q    7 H 7  &	J     7  7  7  7  &	K     7  7  7  7  '�� M    7  7  7  7  7  7  7   7  &	L     &	M (    &	N F    &	O d    &	P �    &	Q �    7 L 7 M 7 M 7 O 7 O 7 P 7 N '�� #    7  7  7  7 Q 7 L 7 N *�5      %!pack://appliction;,,,/Fonts/#COUR)�6E   *�5      '#pack://appliction;,,,/Fonts/#COURDB)�6J   *�5	      '#pack://appliction;,,,/Fonts/#COURBI)�6G   �� 5      "  ��6   .�5   
   ��  � ��8   6'   5      �� 5      "  ��6'   .�5   
   ��  � ��8   6'   5      X�5      10Q�6+   X�5      11Q�6+   X�5      12Q�6+   X�5      14Q�6+   X�5      16Q�6+   X�5      20Q�6+   X�5      25Q�6+   �� 5#      "  ��6   .�5$   
   ��  � ��$ Center=�67   �� 5%   
    � ��8   6'   �� 5&   
    � ��8  6'   5'      ��5,      #efefef��6-   �� 5-      8   63   ��5/      #bdbdbd��6,   �� 50      8   62   ��52      #fafafa��61   �� 53      8   67   ��55      #686868��6,   �� 56      8   62   ��58      #000000��60   �� 59      8   66   ��5;      #ffffff��6-   �� 5<      8   63   ��5>      #bdbdbd��6,   �� 5?      8   62   ��5A      #3099c5��6&   �� 5B      8   6,   ��5D      #45b6e5��6+   �� 5E      8   61   ��5G      #ecf6fb��6/   �� 5H      8   65   ��5J      #0c6991��6*   �� 5K      8   60   ��5M      #ffa800��6(   �� 5N      8   6.   ��5P      #ff4747��6%   �� 5Q      8   6+   ��5S      #00c541��6'   �� 5T      8   6-   ��5Z      	#FFE8EDF9��6+   ��5[      	#FFC5CBF9��65   ��5\      	#FF7381F9��64   ��5^      	#FFE8EDF9��69   ��5_      	#FFC5CBF9��68   ��5`      	#FF888888��67   ��5b      	#FFC5CBF9��67   ��5c      	#FFDDDDDD��66   ��5e      	White��6-   ��5f      	#FF7381F9��62   ��5g      	#FF211AA9��60   ��5i      	#FF3843C4��65   ��5j      	#FF211AA9��63   ��5m      	#FF444444��6*   ��5n      /+sc#1, 0.004391443, 0.002428215, 0.242281124��6P   ��5q      	#FFCCCCCC��60   ��5r      	#FF888888��61   ��5s      	#FF444444��6/   ��5u      	#FF888888��67   ��5v      	#FF444444��66   ��5x      	#FFAAAAAA��68   ��5y      	#FF888888��67   ��5{      	Black��67   ��5~      	#FFC5CBF9��6.   ��5      	Black��6:   ��5�      	#FFC5CBF9��64   ��5�      	#FF3843C4��63   �� 5�      $ 0.5,1&�5�      $ 0.5,0&�5�      .��5�   
   � # � 0 6   $ 0Q�5�      � 5�   
   # � 1 6   $	 0.5Q�5�      � 5�   
   # � 0 6   $ 1Q�5�      5�      �� 5�      $	 0,0&�5�      $	 1,0&�5�      ��5�      � � 5�      $ 	#000000FF��6   $ 0Q�5�      � 5�      $ 	#600000FF��6   $	 0.4Q�5�      � 5�      $ 	#600000FF��6   $	 0.6Q�5�      � 5�      $ 	#000000FF��6   $ 1Q�5�      5�      5�      �� 5�      "  �6!   .�5�   
   ��  �@�WindowStyle$
 None=�6(   �� 5�   
    �@�AllowsTransparency . 6/   �� 5�   
    �@�
ResizeMode$ CanMinimize=�6'   �� 5�   
    � ��
 �����6'   �� 5�   
    � ��
 � � �6(   �� 5�   
    � ��$ 5q�6,   �� 5�   
    � ��
 �  ��6'   5�      �� 5�      "  	�6!   8   .�5�   
   ��  � ��8  6'   �� 5�   
    � ��8  6'   �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��8  6%   �� 5�   
    � ��8  6'   �� 5�   
    � ��$ 50 10q�6$   �� 5�   
    � ��$
 0 10q�6#   5�      �� 5�      "  	�6   8   .�5�   
   ��  � ��8  6'   �� 5�   
    � ��8  6'   �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��8  6%   �� 5�   
    � ��8  6'   �� 5�   
    � ��$ 50 10q�6$   �� 5�   
    � ��$
 0 10q�6#   5�      �� 5�      "  ��6"   8   .�5�   
   ��  � ��8  6'   �� 5�   
    � ��8  6'   �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��8  6%   �� 5�   
    � ��8  6'   �� 5�   
    � ��$ 50 10q�6$   �� 5�   
    � ��$
 0 10q�6#   �� 5�   
    � �� 5�      �� 5�      "	 ��6"   .R�5�      �� $
 border��6   $ 10��5�      #��z��5�      #��z��5�      #��z��5�       . ._�5�      � .+�5�      �� #��z��6(   |�.  #��z��5�   $   #��z��5�   $   #��z��5�   $   #��z��5�   $   # z 5�   $   #��z��5�   $   5�      5�       5�      :� 5�      $ 
MouseEnter��6'   .[�5�      �� .��5�   "   �� .��5�   &   �� 8  $ 0:0:0.3D�6T   $ border��6g   $ Background.Color�6�   5�   #   5�      5�      :� 5�      $ 
MouseLeave��6'   .[�5�      �� .��5�   "   �� .��5�   &   �� 8  $ 0:0:0.3D�6V   $ border��6i   $ Background.Color�6�   5�   #   5�      5�      P� 5�       � {� .  . �5�      �� $ border��6;   8   � ��6O   5�      5�      5�      5�      �� 5     "  ��6   8   5     �� 5     "  ��6)   8   .�5  
   ��   �@  IsHitTestVisibleInChrome . 6B   �� 5  
    � ��
 ���� 6'   �� 5  
    � ��$ 2q�6$   �� 5  
    � ��$ Stretch=�6.   �� 5  
    � ��8  6'   �� 5  
    �@�LayoutTransform 5     �� 5     $	 1.5Q�6!   5     �� 5  
    � �� 5     �� 5      "	 ��6"   .R�5!     �� # z��6   #��z��6A   ._�5"     �� $��Center=�6$   $��Center=�6?   #��z��6\   5#     5$     5&      5(  
   P� 5)      �@J�IsMouseOver . . �5*     ��  � ��8  6/   5+     5.     �� 51     "  ��6&   8    53  
   P� 54      �@J�IsMouseOver . . �55     ��  � ��
 �  ��6/   �� 56      � ��8  6/   57     5:     �� 5A     "  ��6    8   .�5B  
   ��  � ��8  6'   �� 5C  
    � ��8  6%   �� 5D  
    � ��8  6'   �� 5E  
    � ��$ Center=�6.   �� 5F  
    � ��$ Center=�60   �� 5G  
    � ��$	 0 6q�6#   5H     �� 5K     "  ��6    8   .�5L  
   ��  � ��8  6'   �� 5M  
    � ��8  6%   �� 5N  
    � ��8  6'   �� 5O  
    � ��$ Center=�6.   �� 5P  
    � ��$
 Left=�60   �� 5Q  
    �@��TextAlignment$
 Left=�6*   �� 5R  
    � ��$	 0 6q�6#   5S     �� 5V     "  ��6   8   .�5X  
   ��  � ��8  6%   �� 5Y  
    � ��8  6'   �� 5Z  
    � ��$ 10q�6$   �� 5[  
    � ��$ 0 5 0 5q�6#   �� 5\  
    � ��8  6(   �� 5]  
    � ��$ 0 0 0 1q�6,   �� 5^  
    � ��$
 Left=�60   �� 5_  
    � ��$ Bottom=�6.   �� 5`  
    �@��TextAlignment$ Center=�6*   �� 5a  
    � ��8  6'   �� 5b  
    � ��8  6'   �� 5d  
    � �� 5e     �� 5f     "	 ��6"   .R�5h     � .+�5j     �� $
 border��6!   #��z��5k  !   #��z��5l  !   #��z��5m  !    . ._�5o     �� $ PART_ContentHost��6+   |�.  $��Hidden=�6W   $��Hidden=�6~   5p     �� 5r     $ placeholder��5t  $    .  #��z 5s  $   8�� # z��5v  $   $��Center=�5w  $   #��z��5x  $   8�� ��5{     �� 5|  "   "  ��6(   .�5}  &   ��  � y�$ 	Collapsed=�6C    5~  &   �� 5  *    66   �� *Text+  �� *TemplatedParent+!  6�   .a�5�  .   ��  � y�$ Visible=�6K   5�  +   5�  #   5�     5�      5�     P� 5�      � {� .  . �5�     �� $ border��68    �@J�Opacity$
 0.56Q�6L   5�     P� 5�      �@J�IsMouseOver . . �5�     �� $ border��6<    � �� �	#FF7EB4EA6P   5�     P� 5�      �@J�IsKeyboardFocused . . �5�     �� $ border��6<    � �� �	#FF569DE56P   5�     5�     5�     5�     �� 5�     $ #e3e9ef��68   �� 5�     $
 #fff��6<   �� 5�     $
 #888��6>   �� 5�     $
 #eee��6>   �� 5�     $
 #888��6:   �� 5�     "	 d�6;   .R�5�  
   � " 5�     �� 5�     �� 5�     $��20 �6#   .+�5�     �� $��Border��6)   $��2��6   8��  $ 0��5�     $��
1, 1, 1, 1q�6    8�� 5�     �� 5�     $��ButtonBorder��6L   $��1��6   $��
1, 1, 1, 1q�6%   ���#444$ 
0, 0, 0, 0��5�     $��
0, 0, 0, 0q�6)   8�� 5�     F� 5�     $��Arrow��6   $��1��6    =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=�5�     ���#444$��Center=�5�     5�      5�  
   P� 5�      �@J�IsMouseOver . . �5�     �� $ ButtonBorder��65    � ��
 �����6O   5�     P� 5�      �@d�	IsChecked$
 TrueY�68   . �5�     �� $ ButtonBorder��65    � ��
 �����6O   �� 5�     $ Arrow��6/    � �� �	#FF8D979E6B   5�     P� 5�      � {� .  . �5�     �� $ Border��65   8   � ��6I   �� 5�     $ ButtonBorder��65   8   � ��6O   �� 5�     $ ButtonBorder��67   8   � ��6Q   �� 5�     8   � ��6;   �� 5�     $ Arrow��6/    � �� �#9996B   5�     5�     �� 5�     "  ��6   .�5�  
   ��  �@J�SnapsToDevicePixels . 6:   �� 5�  
    �@�OverridesDefaultStyle . 6C   �� 5�  
    � ��$
 Auto=�6G   �� 5�  
    � ��$
 Auto=�6E   �� 5�  
    � �� . 6:   �� 5�  
    � ��8   61   �� 5�  
    � ��8  63   �� 5�  
    � ��8  63   �� 5�  
    �@�FocusVisualStyle 6>   Z� �� 5�  
    � �� 5�     �� 5�     "	 ��6"   .R�5�     � .+�5�     d� $��ToggleButton��6'   $��2��6;   $# Press=�5�     |�.  8�� $ 5�     �� $% IsDropDownOpen�  �� *TemplatedParent+$& TwoWay=�5�     �� 5�     $��ContentSite��6+   $��5, 3, 23, 3q�6>    .  $
��Left=�5�     $��Center=�6:   #��z' 5�     #��z( 5�     #��z��5�     �� 5�     $��PART_EditableTextBox��6"   $��3, 3, 23, 3q�6>   #* z) 5�     $y�Hidden=�5�     
������ $
��Left=�5�     $��Center=�61   |�. .��5�  (   ��5�     �� 5�  "   "	 ��62   .R�5�  &   �� $��PART_ContentHost��6-   |�.  6E   5�  #   .��5�  0   5�     � 5�     $��Popup��6    $��Bottom=�6-   |�.  + . #��z, 5�     $��Slide=�5�     .��5�     � $��DropDown��6#    . #��z- 5�     #��z. 5�     .+�5�  "   �� $��DropDownBorder��6)   
�������$��
0, 1, 0, 0q�6R   $ 0��5�     $��1,1,1,1q�6*   8�� 5�     �� 5�  "   $��4q�6/    . .��5�  &   �� $/ 	Contained=�65   5�  #   5�     5�     5�      5�     P� 5�      �@��HasItems .  . �5�     �� $ DropDownBorder��6K    � ��$ 95��6g   5�     P� 5�      � {� .  . �5      �� 8   � ��6G   5     P� 5      �@��
IsGrouping . . �5     ��  � �� .  6N   5     P� 5      �@��
IsEditable . . �5     ��  �@ 	IsTabStop .  6M   �� 5     $ PART_EditableTextBox��6E    � y�$ Visible=�6g   �� 5     $ ContentSite��6E    � y�$ Hidden=�6^   5	     5     5     5     5     