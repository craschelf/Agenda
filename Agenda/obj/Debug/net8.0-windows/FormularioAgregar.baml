   M S B A M L   `   `   `  ����A  =Agenda, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AgendaAgenda  5   	       Agenda.Formulario   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Agenda     5   	     � Title$  
Formulario��5   	   $	��450��6   $	��300��6)    � WindowStartupLocation$ CenterScreen=�65    � 	Resources 5	       �� 
TargetType �� Property �� Value��%_  '��       '�� �     �� 5
   
   " ��6   .�5      ��  � ��$
 Left=�64   �� 5       � ��$ Center=�62   �� 5       � ��$ 1��6,   5      �� 5   
   " ��6   .�5      ��  � ��$ 25��6'   �� 5       � ��$	 145��6&   �� 5       � ��$ 3��6,   �� 5       � ��$
 Left=�64   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $	��90* �6   �� 5      $	��51* �6   �� 5      $	��51* �6   �� 5      $	��51* �6   �� 5      $	��51* �6   �� 5      $	��51* �6   �� 5      $	��51* �6   �� 5      $	��51* �6   �� 5       $	��21* �6    � ColumnDefinitions 5"   
   �� 5#      $��5* �6   �� 5$      $	��17* �6   �� 5%      $	��33* �6   .+�5'   
   �� $2��,Introduzca los datos del contacto a agregar:��6   $��0��6G   $
��Left=�6T   $��Center=�6o   $��4��6�   �� 5(   
   -   5         ��Name$ 	IdTextBox��5(      $��1��6%   	 �� 
IsReadOnly$
	 True��62   $��30��6D   
 �� TextWrapping$

 Wrap=�6O   $ �� HorizontalScrollBarVisibility$ Disabled=�6c   " �� VerticalScrollBarVisibility$
 Auto=�6�    �� TextAlignment$ Center=�6�   $y�	Collapsed=�6�   �� 5)   
   $��Nombre��6   $��1��6!   �� 5*   
   $��1er Apelido��6   $��2��6&   �� 5+   
   $��2do Apelido��6   $��3��6&   �� 5,   
   $��
Comentario��6   $��4��6%   �� 5-   
   $��Teléfono/s��6   $��5��6%   �� 5.   
   $��Email/s��6   $��6��6"   �� 5/   
   -   5        $ NombreTextBox��5/      $��1��6)   �� 50   
   -   5        $ Apelido1TextBox��50      $��2��6+   �� 51   
   -   5        $ Apelido2TextBox��51      $��3��6,   �� 52   
   -   5        $ ComentarioTextBox��52      $��4��6-   �� 53   
   -   5        $ TelefonoTextBox��53      $��5��6+   $

 Wrap=�68    �� AcceptsReturn$
 True��6L   $ Visible=�6a   �� 54   
   -   5        $ EmailTextBox��54      $��6��6(   $

 Wrap=�65   $
 True��6I   $ Visible=�6^   �� 55   
   -   5         ��Name$ GuardarButton��55      $��Guardar��6(   $��2��6:   $��7��6J   $��26��6W   $��80��6c   $��32,0,0,0q�6n   6�   56      57      