   M S B A M L   `   `   `  ����?  ;V4.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:V4._0V4._0  5   	       V4._0.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:V4._0     5   	     � Title$�  �Электронно-учебное пособие по дисциплине 'Информационная Безопасность'��5   	    � 
ResizeMode$ NoResize=�6X   $	��450��6n   $	��800��6{    � Icon$ /Res/icon.ico��6�    � WindowStartupLocation$ CenterScreen=�6�    J� InputBindings 5	      �� 5
   
    �� Command$
 Help��6   
 �� Key$ F1��6$    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.Help��5      6<   .��5      � ���#90bcbd.+�5   
   �� $��Логин:��6   ���#FFFFFF$
��Left=�66   $��146,157,0,0q�6Q   $	��Top=�6f   $��16(�6~   $��Normal$�6�   �� 5   
   $��Пароль:��6   ���#FFFFFF$
��Left=�67   $��136,185,0,0q�6R   $	��Top=�6g   $��16(�6   �� 5   
   -   5        	 ��Name$	 textBox_login��5      $
��Left=�6)   $��209,161,0,0q�6D   
 �� TextWrapping$

 Wrap=�6Y   $�� ��6m   $	��Top=�6u   $	��460��6�   $��16(�6�   G� 5   
   -   5         G�Name$ textBox_password��5      $
��Left=�60   $��209,189,0,0q�6K   $	��Top=�6`   $	��460��6x   $��16(�6�   �� 5   
   -   5        ���#fc9880���#FFFFFF$��
Войти��5   ;   $��Center=�6K   $	��Top=�6h   $��16(�6�   $	��120��6�   $��34��6�    �� 	IsDefault . $��	0,217,0,0q�6�   �� 5   
   -   5        ���#fc9880���#FFFFFF$��Справка��5   ;   $��16(�6M    . $��465,217,0,0q�6�   $
��Left=�6�   $	��120��6�   $��34��6�   $	��Top=�6�   5      5      