   M S B A M L   `   `   `  ����?  ;V4.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:V4._0.Res.studentWindowsV4._0.Res.studentWindows  5   	   $    V4._0.Res.studentWindows.testS   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:V4._0.Res.studentWindows     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$� �Электронно-учебное пособие по дисциплине 'Информационная Безопасность'��5	   	    � 
ResizeMode$ NoResize=�6X   $	��374��6n   $	��800��6{    � Icon$ /Res/icon.ico��6�    J� InputBindings 5
      �� 5   
    �� Command$
 Help��6   
 �� Key$ F1��6$    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.Help��5      6<   .��5      � ���#90bcbd.+�5   
   �� $��Управление��6   ���#FFF$��16(�69   $	��211��6G   $	��Top=�6T   $��10,10,484,0q�6l   .��5      � .+�5      �� $��Выбор теста:��6   $
��Left=�6/   $��	10,10,0,0q�6J   $	��Top=�6]   $��16(�6u   
�������6�   �� 5      -   5        	 ��Name$		 cb1��5      $��
10,46,13,0q�6(   $	��Top=�6<   6T   �� 5      -   5        ���#fc9880���#FFF$��Comic Sans MS)�5   @   $��На главную��6p   $��10,135,13,0q�6�   $	��Top=�6�   $��24(�6�   �� 5      -   5        
 ��Name$
 lbres1��5      $�� ��6(   $
��Left=�63   $	��Top=�6N   $��16(�6f   
�������$��	10,78,0,0q�6�   $	��271��6�   $��52��6�   5      5      �� 5   
   -   5         ��Name$ rtbQuestion��5      $
��Left=�6)   $��
353,18,0,0q�6D   $	��Top=�6X   $��16(�6p   
�������$��72��6�   $	��401��6�   $
��Wrap=�6�   �� 5   
   $��Visible=�6   $��Disabled=�6?   $��353,95,46,73q�6f   .��5      � .+�5      	� -   5         	�Name$
 rba1��5      ���#FFF 	� 	GroupName$	 ans��6>   $�� ��6N   $
��Left=�6Y   $��16(�6t   $	��Top=�6�   $��	10,10,0,0q�6�   	� 5      -   5        $
 rba2��5      ���#FFF$	 ans��6>   $�� ��6N   $
��Left=�6Y   $��16(�6t   $	��Top=�6�   $��	10,66,0,0q�6�   	� 5       -   5        $
 rba3��5       ���#FFF$	 ans��6>   $�� ��6N   $
��Left=�6Y   $��16(�6t   $	��Top=�6�   $��
10,118,0,0q�6�   5!      5"      �� 5#   
   -	   5         ��Name$ btnNext��5#   S   ���#fc9880���#FFF$��Comic Sans MS)�68   $��
Далее��6z   $
��Left=�6�   $��483,290,0,0q�6�   $	��Top=�6�   $��Normal"�6�   $��24(�6�   $	��271��6�   5&      5'      