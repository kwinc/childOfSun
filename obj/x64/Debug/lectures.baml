   M S B A M L   `   `   `  ����E  AMoonPdfLib, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:MoonPdfLib;assembly=MoonPdfLib
MoonPdfLib  5   	   ? ;V4.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:V4._0V4._0 5   	      V4._0.lectures   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <mpp,clr-namespace:MoonPdfLib;assembly=MoonPdfLib     5   	   %localclr-namespace:V4._0     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � Title$� �Электронно-учебное пособие по дисциплине 'Информационная Безопасность'��5
   	    � 
ResizeMode$ NoResize=�6X   $	��872��6n   $
��1083��6{    � Icon$ /Res/icon.ico��6�    J� InputBindings 5      �� 5   
    �� Command$
 Help��6   
 �� Key$ F1��6$    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.Help��5      6<   .��5      � ���#90bcbd.+�5   
      MoonPdfLib.MoonPdfPanel  -   5        $��pdfPanel��5      $��Bottom=�6+   
�������	   ViewType$	 
SinglePage=�6Z   
   PageRowDisplay$
 ContinuousPageRows=�6p      
PageMargin$ 0,2,4,2q�6�    J� 	AllowDrop . $��378,38,35,37q�6�   �� 5   
   $6��0Предварительный просмотр:��6   $
��Left=�64   $��
378,10,0,0q�6O   $	��Top=�6c   
�������$��16(�6�   �� 5   
   $
��Left=�6   $��Center=�6.   $��Управление��6I   ���#FFF$��16(�6o   $��10,0,0,0q�6}   $	��404��6�   .��5      � .+�5      �� $��Выбор лекции:��6   
�������$��16(�6C   $��10,31,207,315q�6Q   �� 5      -   5         ��Name$	 cb1��5      $��16(�6(   
���   �$��
10,66,10,0q�6q   $	��Top=�6�   �� 5      -   5         ��Name$ btAdd��5      ���#fc9880���#FFF$��Comic Sans MS)�6p   $��Добавить��6�   $��24(�6�   $��10,98,10,223q�6�   �� 5      -   5        $ btRemove��5      ���#fc9880���#FFF$��Comic Sans MS)�6p   $#��Удалить текущую��6�   $��24(�6�   $��10,158,10,164q�6�   �� 5      -   5        ���#fc9880���#FFF$��Comic Sans MS)�5   p   $#��Скачать текущую��6�   $��24(�6�   $��10,217,10,106q�6�   �� 5      -   5        ���#fc9880���#FFF$��Comic Sans MS)�5   p   $��На главную��6�   $��24(�6�   $��10,275,10,47q�6�   5      5       5!      5"      