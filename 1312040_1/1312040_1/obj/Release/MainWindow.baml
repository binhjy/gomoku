   M S B A M L   `   `   `  ���� D  @1312040_1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:_1312040_1
_1312040_1      _1312040_1.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:_1312040_1       � Title$  Gomoku��$��413.75��$��795.75��$��784.5��$��470.375�� � 	Resources   _1312040_1.Chess_board   myChessboard  _1312040_1.Xu_li_chat 
 myChat  WatermarkForeground �� Color   BooleanToVisibilityConverter��%K   &	        &	      &	 
     &	          �� $ DarkGray���� .��� $��0,0,2,0q� � RowDefinitions ��  � ColumnDefinitions �� $��0* ��� $	��62* ��� $	��69* �.+��� 
���   �$��2q�$��Stretch=�$��0,2,0,0q�$��Stretch=�$��2��� $��2��.+���  � DataContext# [  ����  �� Path$ 
list_chess����� .R�A�  A� Rows �� $	 row� A� Columns �� $ column����� .R��� ���� *DataContext.ClickCommand+	 �� RelativeSource	 �� 
 �� AncestorType$
 ItemsControlL����� ���� $ 
Background�.���� ���� *MyImageSource+����  �� 
TargetType" �� �� 	Resources   flashAnimation �� TargetProperty W� From	 W� To g� AutoReverse g� Duration g� RepeatBehavior��%Z   &	       �� .��W� $ Opacity�$ 1X�$	 0.2X� . $ 0:0:0.6D�$ Forever�� �� Triggers ��  �� Binding �� *OptionBlink+ �� Value True N� EnterActions ��  ��Name$ flash��#��[  N� ExitActions ��  �� BeginStoryboardName$ flash��� # [ $��2��$��Right=�$	��Top=�$	��304��$��52��.+��� $��	Your name��$
��Left=�$��0,10,0,0q�$	��Top=�$��70���� -    ��Name$ tbx_name��$
��Left=�$��23��$��	70,12,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120������ $ Text_textbox_name��� $
��Left=�$��
207,12,0,0q�$	��Top=�$��75�����	#FFFFFDFD���	#FFC3BEBE$��24������ $ Text_button_Start����� *Start+��  �� 
IsReadOnly . $��Center=�$��2��$��Right=�$��31��$��0,52,0,0q�$
 Wrap=�$��            Chat/History��$	��Top=�$	��304�����	#FFFDFDFD �� 	IsEnabled . ���	#FF2244AA���	#FF1F6AC5 �� SelectionBrush Z� �� $��/image/chat.png��$��2��$��31��$��
0,64,268,0q�$	��Top=�$��19��$��Right=�� # [ $��2��$��Right=�$��Bottom=�$	��304��$��51�����	#FFF0ECEC.+��� 
����������	#FF3073E8$��24,10,124,10q��� ���	#FF3073E8�� $��Center=�$��24,18,124,17q�$��Type your message here...��#��[ y���  �� ElementName$ textBoxSearch��$ Text.IsEmpty�  �� 	Converter#  [ �� -   $��textBoxSearch��$��Center=�$��24,10,124,10q����� $ Text_noi_dung_chat�.������ .���� -   ! ��Name$! bt_send��$
��Send��$
��Left=�$��
186,10,0,0q�$	��Top=�$��74��$��31�����	#FFECB971���	#FFFFF8F8���� $
 Send�� $��2��$��Right=�$��	0,83,0,51q�$	��304��.+��� $��Right=�$
��auto��$	��304�����	#FF3158CD���� " �� Source#" [ $ 	list_chat����� " ��.��� # �� Property# �@��
IsSelected$ �� Value$ �� *Content.IsSelected+% �� Mode$% TwoWay=�	 �� *Self+�� # � ��$ �� & �� 
TargetType"& ��.R��� ���� .R�� $	��281�� �� $	��200 �' z� SharedSizeGroup$' Column1���� $��30 �$' Column2���� $��60 �$' Column3�� �� $��30 ��� $
��auto ��� $��10 �.+��� $��Center=�$
��Bold"�$��0������ $	 Ten��� $��Center=�$��1��$��3������ $ Noi_dung_chat��� $��Center=�$��2������ $ 	Thoi_gian��� $��Center=�$��15��$��1��$��image/clock.png���� $��Center=����	#FF000000$��1��( �� StrokeDashArray$	( 2 5R�) J� SnapsToDevicePixels) . $
��Auto��$��3��$��0,0,0,-1q�$��1��$��2��� $	��Top=�$��Right=�# [  $��2��$��	0,2,304,0q�$	��110��$	��381��.+��� $	��PVP��$��10,10,10,339q����� *Background_PVP+���� *PVP+�� $	��PVC��$��10,75,10,274q����� *Background_PVC+���� *PVC+�� $��New Game��$��10,212,10,137q�* J� RenderTransformOrigin$* 0.489,2.953&����� *Background_NewGame+���� *	NewGame+�� $��CvServer��$��10,147,10,202q����� *Background_CvServer+���� *
CvServer+