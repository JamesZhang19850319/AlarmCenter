   M S B A M L   `   `   `  ����   CustomWPFColorPicker4    .CustomWPFColorPicker.AdvancedColorPickerDialog   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    8x,http://schemas.microsoft.com/winfx/2006/xaml    $
��Auto��$
��Auto��  � 	Resources     BrightnessGradient �� 
StartPoint �� EndPoint � ColorInterpolationMode � Offset � Color ) %HorizontalSliderTrackNormalBackground �� Color % !HorizontalSliderTrackNormalBorder  SliderRepeatButtonStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType  SliderThumbBackground " SliderThumbOuterBorderGeometry " HorizontalSliderThumbTopAccent   SliderThumbTopAccentGeometry % !HorizontalSliderThumbBottomAccent #	 SliderThumbBottomAccentGeometry &
 "HorizontalSliderThumbShadeGradient  HorizontalSliderThumbBorder ' #HorizontalSliderThumbTopAccentHover * &HorizontalSliderThumbBottomAccentHover ) %HorizontalSliderThumbTopAccentPressed , (HorizontalSliderThumbBottomAccentPressed  HorizontalSliderThumbStyle J� SnapsToDevicePixels T� X T� Y F�Name �� Triggers P� Property P� Value �� 
TargetName ! SliderThumbBackgroundInverted ) %SliderPointedThumbOuterBorderGeometry * &HorizontalSliderThumbTopAccentInverted ' #SliderPointedThumbTopAccentGeometry - )HorizontalSliderThumbBottomAccentInverted * &SliderPointedThumbBottomAccentGeometry . *HorizontalSliderThumbShadeGradientInverted ' #HorizontalSliderThumbBorderInverted / +HorizontalSliderThumbTopAccentHoverInverted 2 .HorizontalSliderThumbBottomAccentHoverInverted 1 -HorizontalSliderThumbTopAccentPressedInverted   HorizontalSliderUpThumbStyle �� Angle " HorizontalSliderDownThumbStyle ' #VerticalSliderTrackNormalBackground # VerticalSliderTrackNormalBorder    VerticalSliderThumbTopAccent #! VerticalSliderThumbBottomAccent $"  VerticalSliderThumbShadeGradient # VerticalSliderThumbBorder %$ !VerticalSliderThumbTopAccentHover (% $VerticalSliderThumbBottomAccentHover '& #VerticalSliderThumbTopAccentPressed *' &VerticalSliderThumbBottomAccentPressed ( VerticalSliderThumbStyle �� Stretch �� Left
 �� Top K� ZIndex () $VerticalSliderThumbTopAccentInverted +* 'VerticalSliderThumbBottomAccentInverted ,+ (VerticalSliderThumbShadeGradientInverted %, !VerticalSliderThumbBorderInverted -- )VerticalSliderThumbTopAccentHoverInverted 0. ,VerticalSliderThumbBottomAccentHoverInverted // +VerticalSliderThumbTopAccentPressedInverted  0 VerticalSliderLeftThumbStyle !1 VerticalSliderRightThumbStyle 2 SliderStyle � RowDefinitions m�Name m� Fill m� 	Placement �� CornerRadius ��Name ]�Name ]� Thumb  n�Name! ]� IncreaseRepeatButton" �� IncreaseLarge# ]� DecreaseRepeatButton$ �� DecreaseLarge% �� Triggers& � ColumnDefinitions' J� RenderTransformOrigin( �� ScaleX) �� ScaleY* �� AngleX+ �� AngleY 3 DialogCloseButtonStyle, �� StrokeMiterLimit- � MappingMode. �� 	Transform/ u� Matrix
0 � Tag1 �� TextAlignment��%�4  &	        &	 �     &	 �     &	     &	 �    &	 �    &	 >    &	 �    &	 �    &		     &	
 E    &	 �    &	 -    &	     &	 �    &	 $    &	 w    7  7  7  7  7  7 	 7 
 7  7  7  7  7  7  7  7  &	 �    &	 �    &	 b	    &	 �	    &	 �	    &	 8
    &	 t
    &	     &	 \    &	 �    &	     &	 S    7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  &	 �    7  7  7  7  7  7  7 
 7  7  7  7  7  7  7  7  &	 D    &	 Y    &	  �    &	!      &	" S    &	# �    &	$ <    &	% �    &	& �    &	' 3    &	( �    7 " &	) �    &	* %    &	+ x    &	, 
    &	- `    &	. �    &	/     &	0 W    7  7  7 ) 7  7 * 7  7 + 7 , 7  7 - 7 . 7 - 7 . 7 / 7 / &	1 �    7  7  7   7  7 ! 7  7 " 7 # 7  7 $ 7 % 7 $ 7 % 7 & 7 ' &	2 d#    7  7  7  7  7  7  7  7  7  7 ( 7  7  7 0 7 1 &	3 �,    'n� u0    �� $	 0,1&�$	 0,0&�$ ScRgbLinearInterpolation=�.��� $ 0Q�$ sc# 1, 0,0,0��� $	 0.5Q�$ sc# 1, 0.5,0.5,0.5��� $ 1Q�$ sc# 1, 1,1,1���� $ 	#FFF0EDE0���� $	 0,1&�$	 0,0&�.��� $ 	#FFAEB1AF��$	 0.1Q�� $ White��$ .9Q��� " ��.���  �@�OverridesDefaultStyle	 . ��  � ��	 .  ��  � |�	 .  ��  � ��	 �� "
 ��.R��� 
������ �� $ #F3F3EF�����M 5,-9 A 1.5 1.5 0 0 0 3.5,-10.5 L -3.5,-10.5 A 1.5 1.5 0 0 0 -5,-9 L -5,9 A 1.5 1.5 0 0 0 -3.5,10.5 L 3.5,10.5 A 1.5 1.5 0 0 0 5,9 Z��� $	 1,1&�$	 0,0&�.��� $ #70D66F��$ 0Q�� $ #46CA44��$ 1Q��-)M 4.5,-8 L 4.5,-10 L -4.5,-10 L -4.5,-8 Z��� $	 1,1&�$	 0,0&�.��� $ #46CA44��$ 0Q�� $ #1B9B1A��$ .8Q��)%M 4.5,8 L 4.5,10 L -4.5,10 L -4.5,8 Z��� $	 1,0&�$	 0,0&�.��� $ 	#40FFFFFF��$ 0Q�� $ 	#00FFFFFF��$ .3Q�� $ 	#00000000��$ .7Q�� $ 	#40000000��$ 1Q��� $	 1,1&�$	 0,0&�.��� $ 	#FFBECBD3��$ 0Q�� $ 	#FF72838D��$ 1Q��� $	 1,1&�$	 0,0&�.��� $ #FBCE7C��$ 0Q�� $ #F7B335��$ 1Q��� $	 1,1&�$	 0,0&�.��� $ #FAC055��$ 0Q�� $ #C7902A��$ .8Q��� $	 1,1&�$	 0,0&�.��� $ #70BA66��$ 0Q�� $ #229412��$ 1Q��� $	 1,1&�$	 0,0&�.��� $ #46A638��$ 0Q�� $ #1B770E��$ .8Q��� " n�.���  �@�OverridesDefaultStyle	 . ��  � ��$	 22����  � ��$	 11����  � ��
	 � � ���  � ��	 �� "
 n�.R���  . z�T� $	 5.5Q�$ 11Q�.+�F� $ 
Background��8��  8�� F� $ 	TopAccent��8�� $��1��8�� F� $ BottomAccent��8�� $��1��8�� F� $ OuterBorder��8�� 8�� $��1��8��  P�  �@J�IsMouseOver . . ��� $ 	TopAccent��8	 	  � ���� $ BottomAccent��8	 
  � ��P�  � ��
 � ���. ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  �@n�
IsDragging . . ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF5F4EA�� $ OuterBorder�� � ��	 �	#FFD6D4C6�� $ 	TopAccent�� � ��	 �	#FFD2D0C6�� $ BottomAccent�� � ��	 �	#FFBCBAAE�� $ #F3F3EF���a]M 5,-8 A 1.5 1.5 0 0 0 3.5,-9.5 L -3.5,-9.5 A 1.5 1.5 0 0 0 -5,-8 L -5,5.5 L 0,10.5 L 5,5.5 Z��� $	 1,1&�$	 0,0&�.��� $ #46CA44��$ 1Q�� $ #1B9B1A��$ 0Q��+'M 4.5,-6 L 4.5,-9 L -4.5,-9 L -4.5,-6 Z��� $	 1,1&�$	 0,0&�.��� $ #70D66F��$ 1Q�� $ #46CA44��$ .2Q��62M 4.5,4 L 0,8.5 L -4.5,4 L -4.5,5 L 0,10 L 4.5,5 Z��� $	 1,0&�$	 0,0&�.��� $ 	#40FFFFFF��$ 1Q�� $ 	#00FFFFFF��$ .7Q�� $ 	#00000000��$ .3Q�� $ 	#40000000��$ 0Q��� $	 1,1&�$	 0,0&�.��� $ 	#FFBECBD3��$ 1Q�� $ 	#FF72838D��$ 0Q��� $	 1,1&�$	 0,0&�.��� $ #FAC055��$ 1Q�� $ #C7902A��$ 0Q��� $	 1,1&�$	 0,0&�.��� $ #FBCE7C��$ 1Q�� $ #F7B335��$ .2Q��� $	 1,1&�$	 0,0&�.��� $ #46A638��$ 1Q�� $ #1B770E��$ 0Q��� " n�.���  �@�OverridesDefaultStyle	 . ��  � ��$	 22����  � ��$	 11����  � ��
	 � � ���  � ��	 �� "
 n�.R���  . z�V� ~�X� �� $	 180Q�T� $	 5.5Q�$ 11Q�.+�F� $ 
Background��8��  8�� F� $ 	TopAccent��8�� $��1��8�� F� $ BottomAccent��8�� $��1��8�� F� $ OuterBorder��8�� 8�� $��1��8��  P�  �@J�IsMouseOver . . ��� $ 	TopAccent��8	 	  � ���� $ BottomAccent��8	 
  � ��P�  � ��
 � ���. ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  �@n�
IsDragging . . ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF5F4EA�� $ OuterBorder�� � ��	 �	#FFD6D4C6�� $ 	TopAccent�� � ��	 �#BBB9AD�� $ BottomAccent�� � ��
	 ���� �� " n�.���  �@�OverridesDefaultStyle	 . ��  � ��$	 22����  � ��$	 11����  � ��
	 � � ���  � ��	 �� "
 n�.R���  . z�T� $	 5.5Q�$ 11Q�.+�F� $ 
Background��8��  8�� F� $ 	TopAccent��8�� $��1��8�� F� $ BottomAccent��8�� $��1��8�� F� $ OuterBorder��8�� 8�� $��1��8��  P�  �@J�IsMouseOver . . ��� $ 	TopAccent��8	 	  � ���� $ BottomAccent��8	 
  � ��P�  � ��
 � ���. ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  �@n�
IsDragging . . ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF5F4EA�� $ OuterBorder�� � ��	 �	#FFD6D4C6�� $ 	TopAccent�� � ��	 �	#FFD2D0C6�� $ BottomAccent�� � ��
	 ���� �� $ 	#FFF0EDE0���� $	 1,0&�$	 0,0&�.��� $ 	#FFAEB1AF��$	 0.1Q�� $ White��$ .9Q��� $	 0,1&�$	 1,0&�.��� $ #70D66F��$ 0Q�� $ #46CA44��$ 1Q��� $	 0,1&�$	 1,0&�.��� $ #46CA44��$ 0Q�� $ #1B9B1A��$ .8Q��� $	 0,0&�$	 1,0&�.��� $ 	#40FFFFFF��$ 0Q�� $ 	#00FFFFFF��$ .3Q�� $ 	#00000000��$ .7Q�� $ 	#40000000��$ 1Q��� $
 0,.5&�$	 1,0&�.��� $ 	#FFBECBD3��$ 0Q�� $ 	#FF72838D��$ 1Q��� $	 0,1&�$	 1,0&�.��� $ #FBCE7C��$ 0Q�� $ #F7B335��$ 1Q��� $	 0,1&�$	 1,0&�.��� $ #FAC055��$ 0Q�� $ #C7902A��$ .8Q��� $	 0,1&�$	 1,0&�.��� $ #70BA66��$ 0Q�� $ #229412��$ 1Q��� $	 0,1&�$	 1,0&�.��� $ #46A638��$ 0Q�� $ #1B770E��$ .8Q��� " n�.���  �@�OverridesDefaultStyle	 . ��  � ��$	 22����  � ��$	 11����  � ��
	 � � ���  � ��	 �� "
 n�.R���  . z�V� ~�X� �� $	 -90Q�T� $ 11Q�$	 5.5Q�.+�F� $
��8.75��$��8.792�����	#FFA5452A$
 Fill=����	#FFA5452A,����`�� X4��8T笪�@��c��"��� X4��	$ -4.25��$ -10.209��F� $ OuterBorder��$��Stretch=�$��Stretch=�$	��9.5��$��9.625��8��  $��1��$ -1������0���  @͕�9  `<W0D <�����? `� `� o��YUU@$L�-k�?���� �@Zb  `� `� +( &?zd�bW #@u�G��B0?�1��#@ `� `� Q�� �p�"	@#@׈<� ���  `� `� 	$ -4.625��$ -10.75����Z�  P�  �@J�IsMouseOver . P�  � ��
 � ���P�  �@n�
IsDragging . P�  � {� .  . ��� $ OuterBorder�� � ��	 �	#FFD6D4C6�� $	 0,1&�$	 1,0&�.��� $ #46CA44��$ 1Q�� $ #1B9B1A��$ 0Q��� $	 0,1&�$	 1,0&�.��� $ #70D66F��$ 1Q�� $ #46CA44��$ .2Q��� $	 0,0&�$	 1,0&�.��� $ 	#40FFFFFF��$ 1Q�� $ 	#00FFFFFF��$ .7Q�� $ 	#00000000��$ .3Q�� $ 	#40000000��$ 0Q��� $	 0,1&�$	 1,0&�.��� $ 	#FFBECBD3��$ 1Q�� $ 	#FF72838D��$ 0Q��� $	 0,1&�$	 1,0&�.��� $ #FAC055��$ 1Q�� $ #C7902A��$ 0Q��� $	 0,1&�$	 1,0&�.��� $ #FBCE7C��$ 1Q�� $ #F7B335��$ .2Q��� $	 0,1&�$	 1,0&�.��� $ #46A638��$ 1Q�� $ #1B770E��$ 0Q��� " n�.���  �@�OverridesDefaultStyle	 . ��  � ��$	 22����  � ��$	 11����  � ��
	 � � ���  � ��	 �� "
 n�.R���  . z�V� ~�X� �� $ 90Q�T� $ 11Q�$	 5.5Q�.+�F� $ 
Background��8��  8�� F� $ 	TopAccent��8�� $��1��8�� F� $ BottomAccent��8�� $��1��8�� F� $ OuterBorder��8�� 8�� $��1��8��  P�  �@J�IsMouseOver . . ��� $ 	TopAccent��8	 	  � ���� $ BottomAccent��8	 
  � ��P�  � ��
 � ���. ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  �@n�
IsDragging . . ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF5F4EA�� $ OuterBorder�� � ��	 �	#FFD6D4C6�� $ 	TopAccent�� � ��	 �#BBB9AD�� $ BottomAccent�� � ��
	 ���� �� " n�.���  �@�OverridesDefaultStyle	 . ��  � ��$	 22����  � ��$	 11����  � ��
	 � � ���  � ��	 �� "
 n�.R���  . z�V� ~�X� �� $	 -90Q�T� $ 11Q�$	 5.5Q�.+�F� $ 
Background��8��  8�� F� $ 	TopAccent��8�� $��1��8�� F� $ BottomAccent��8�� $��1��8�� F� $ OuterBorder��8�� 8�� $��1��8��  P�  �@J�IsMouseOver . . ��� $ 	TopAccent��8	 	  � ���� $ BottomAccent��8	 
  � ��P�  � ��
 � ���. ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  �@n�
IsDragging . . ��� $ 	TopAccent��8	   � ���� $ BottomAccent��8	   � ��P�  � {� .  . ��� $ 
Background�� � ��	 �	#FFF5F4EA�� $ OuterBorder�� � ��	 �	#FFD6D4C6�� $ 	TopAccent�� � ��	 �	#FFD2D0C6�� $ BottomAccent�� � ��
	 ���� �� " ��.���  �@��IsPressAndHoldEnabled	 .  ��  � ��
	 ���� ��  � ��	 �	#FFA1A192��  � ��	 �� "
 ��.R���  . #��z��#��z��#��z��._��  �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�m� $ TopTick��$y�	Collapsed=�$��4��$��0��# z��$	 Top=�m� $ 
BottomTick��$y�	Collapsed=�$��4��$��2��# z��$ Bottom=��� $	��5,0q�$��Center=�$	��4.0��$��1��8��  8�� $��1q�$ 1��._��� $��-6,-1q�.+��� $ PART_SelectionRange��$y�Hidden=�$	��4.0��#��� ��#��� ��$	��1.0��]� $ 
PART_Track��$��1�� n� $  Thumb��8�� ! �� 8�� #��Z" # �� 8�� #��Z$  P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$	 Visible=��� $ Thumb��8	   � ��P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$	 Visible=��� $ Thumb��8	   � ��P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$	 Visible=��� $ 
BottomTick�� � y�$	 Visible=�P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$	 Visible=�P�  �@J�IsKeyboardFocused . . ��� $ Thumb�� � ��
	 � ���% P�  �@��Orientation$ Vertical=�. ���  � ��	 �� "
 ��.R���  . #��z��#��z��#��z��$��9��._�� & �� $
��Auto ��� #��z��$
��Auto ��� $
��Auto �.+�m� $ LeftTick��$y�	Collapsed=�$��4��$��0��# z��$
 Left=�m� $ 	RightTick��$y�	Collapsed=�$��4��$��2��# z��$ Right=��� $��Right=�$��0,0,10,0q�$��3��$��1��8�� 8�� $��1q�$ 1��$' 0.5,0.5&�$��1��z�V� .~��� $( 1Q�$) 1Q��� $* 0Q�$+ 0Q��� $ 0Q�T� $ 0Q�$ 0Q�._��� $��-1,-6q�.+��� $ PART_SelectionRange��$y�Hidden=�$	��4.0��#��� ��#��� ��$	��1.0��]� $ 
PART_Track��$��1�� n� $  Thumb��8��	 ! �� 8��
 #��Z" # �� 8�� #��Z$  P�  �@��TickPlacement$ TopLeft=�. ��� $ LeftTick�� � y�$	 Visible=��� $ Thumb��8	   � ��P�  �@��TickPlacement$ BottomRight=�. ��� $ 	RightTick�� � y�$	 Visible=��� $ Thumb��8	   � ��P�  �@��TickPlacement$
 Both=�. ��� $ LeftTick�� � y�$	 Visible=��� $ 	RightTick�� � y�$	 Visible=�P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$	 Visible=�P�  �@J�IsKeyboardFocused . . ��� $ Thumb�� � ��
	 � ����� " ��.���  � ��$	 20����  � ��$	 20����  � ��	 �� .R��� .���� .+�F� $��1.000000�����	#ff919191$, 1.000000Q�������!N;� ��!؄؄!?� !��g5 !�&" ! � ؄ � N;� � � �g5 � � U &"  � �g5  � �?�  � �؄ � �!U �!�g5 ��!N;� ���� $- Absolute=�$ 399.912598,306.483398&�$ 399.912598,235.869263&���� $ 0.000000Q�$ 	#ffffffff��� $ 1.000000Q�$ 	#ff000000��. u� $B/ <1.000000,0.000000,-0.000000,-1.000000,-390.192383,309.083008x�F� $ 
crossLine1��$��1.500000�����	#ff919191$, 1.000000Q�"����[� �E �cj� �K� �[� ��� F� $ 
crossLine2��$��1.500000�����	#ff919191$, 1.000000Q�"���У�E '�E ќ<� �^� ѣ�E ��  P�  �@J�IsMouseOver . . ��� $ 
crossLine1�� � ��	 �#FF0000�� $ 
crossLine2�� � ��	 �#FF0000�� " n�.���  � ��	 �� .R��� $��Stretch=�$��Stretch=�$ 2��
������ ._��� #��z0 $
��Auto��
���   �$��12(�$��Arial)�$
��Bold"�$��1,0,0,0q�$
1 Left=�$
��Left=�$��Center=�.���� 2 �� Orientation$2 Vertical=�.+��� -   3 ��Name$3 	Container��
�������$��1q�
�������$��12q�$ 3��4 J� BitmapEffect4 F� 5 F� Color$5 Black��6 F� 	Direction$	6 320Q�7 F� ShadowDepth$7 2Q�8 F� Softness$	8 0.5Q�9 F� Opacity$	9 0.5Q�._��� .+��� $��25��$��0,0,0,0q�$ 7 7 0 0�� 4 DialogTitleBarBGBrush#��� 4 y��� *TitleBarVisibility+._�� -   : �Name$: 
titlePanel��.+��� $��Advanced Color Palette��$��Center=�$��5,0,0,0q�n� -   $  	moveThumb��$��0q�$��Stretch=�; � Cursor$; 	ScrollAll��0 �� *Title+�� -   #��� 3 $��Right=�$��Center=�$��0q�$	��5,0q�
������ $
; Hand���� ���#ffffff$��0��$��0,0,0,0q�$	��Top=�$��1��y��� *TitleBarVisibility+�� ���#cfd8da$��0��$��0,0,0,0q�$	��Top=�$��1��y��� *TitleBarVisibility+�� < �� Padding$	< 0,0q�._���  5 FrameBKPanel#��� 5 ._��� .+�� 
������ .+��� $��0,0,0,0q�$��0q�$��Center=�$��Center=�._��� $2 Vertical=�$	��225��$
��Auto�����	#FFFFFFFF$��0,0,0,8q�$��Center=�$��Center=�.+��� $2 
Horizontal=�.+��� -   $3 borderColorChart��$��	10,10,0,0q�$	��Top=�$	��174��$	��138��._�� .+��� -   $
��None=�$��colorChart.PNG��A� -   = A�Name$= colorMarker��$��12��$��12��$��2��
���**���� ._��� $2 
Horizontal=�.+��� -   $3 brightnessSliderBorder��$��8,10,0,0q�$��15��$	��138��$	��Top=�#���   �� -	   > ��Name$> brightnessSlider��$��0,5,0,0q�? �� Orientation$? Vertical=�$	��9.5��$	��148��@ �� IsMoveToPointEnabled@ . |�.  A � Minimum$	A 0.0Q�B � Maximum$	B 1.0Q�$��Center=�#��� 2 $��Center=��� $2 
Horizontal=�$��62��.+��� -
   $3 currentColorBorder��$
��Left=�$��	10,0,25,0q�$��Bottom=�$��82��$��50��$��2q�
���   ����	#FF808080�� $2 Vertical=�$��90��.+��� -   C ��Name$C OK��$��Right=�$��	20,10,0,0q�$��Bottom=�$��70��$��23��$��Ok���� -   $C Cancel��$��Right=�$��20,5,0,0q�$��Bottom=�$��70��$��23��$��Cancel��