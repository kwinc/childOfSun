   M S B A M L   `   `   `  ���� ?  ;V4.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:V4._0V4._0      
V4._0.vids   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:V4._0       � WindowStartupLocation$  CenterScreen=� � Title$� �Электронно-учебное пособие по дисциплине 'Информационная Безопасность'�� � 
ResizeMode$ NoResize=�$	��450��$	��800�� � Icon$ /Res/icon.ico�� J� InputBindings ��  �� Command$
 Help��
 �� Key$ F1�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$ ApplicationCommands.Help��.��� ���#90bcbd.+��� $��Управление�����#FFF$��16(�$
��Left=�$	��240��$	��302��$��Center=�$��10,0,0,0q�.��� .+��� $��Выбор видео:��$
��Left=�$��	10,10,0,0q�$	��Top=�
�������$��16(��� -   	 ��Name$		 cb1��$��0,46,0,0q�$	��Top=�$��16(�
���   �$��Center=�$	��218���� -   
 ��Name$
 btnAdd��$��Добавить��$��Center=�$��0,78,0,0q�$	��Top=����#fc9880���#FFF$��Comic Sans MS)�$��24(�$	��218���� -   $
 	btnRemove��$#��Удалить текущую��$��Center=�$��	0,120,0,0q�$	��Top=�$	��218�����#fc9880���#FFF$��Comic Sans MS)�$��24(��� -   $#��Скачать текущую��$��Center=�$��	0,162,0,0q�$	��Top=�$	��218�����#fc9880���#FFF$��Comic Sans MS)�$��24(��� -   $��На главную��$��Center=�$��	0,204,0,0q�$	��Top=�$	��218�����#fc9880���#FFF$��Comic Sans MS)�$��24(��� $6��0Предварительный просмотр:��$
��Left=�$��
250,10,0,0q�$	��Top=�
�������$��16(�� $��255,55,28,33q� � RowDefinitions �� $��* ��� $
��Auto �.+�r� -   $��mePlayer�� r� Source$P Jhttp://hubblesource.stsci.edu/sources/video/clips/details/images/hst_1.mpg?� r� LoadedBehavior$ Manual=��� $��1��.+��� -   $��	lblStatus�����#FFF$�� ��$��Center=�$��5q�� $��Center=�.+��� -	   $��btnPlay�����#fc9880���#FFF$��Comic Sans MS)�$��16(�.��Воспроизвести�� -
   $��btnPause�����#fc9880���#FFF$��Comic Sans MS)�$��16(�$	��5,0q�.��
Пауза�� -   $��btnStop�����#fc9880���#FFF$��Comic Sans MS)�$��16(�.��
Стоп