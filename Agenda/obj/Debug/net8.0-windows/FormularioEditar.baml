   M S B A M L   `   `   `  ����A  =Agenda, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AgendaAgenda  5   	       Agenda.FormularioEditar   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Agenda     5   	     � Title$  FormularioEditar��5   	   $	��450��6"   $	��300��6/   ���#CBE7E8 � WindowStartupLocation$ CenterScreen=�6P    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name	 ��Name��%�  '��       '��     '�� �    �� 5
   
   " ��6   .�5      ��  � ��$
 Left=�64   �� 5       � ��$ Center=�62   �� 5       � ��$ 1��6,   �� 5       � �� �#0081A76+   �� 5       � ��$ lobster)�6+   �� 5       � ��$
 bold"�6+   5      �� 5   
   " ��6   .�5      ��  � ��$ 23��6'   �� 5       � ��$	 145��6&   �� 5       � ��$ 3��6,   �� 5       � ��$
 Left=�64   �� 5       � ��$ lobster)�6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� ���#0081A7$��3q�67   $ 5��6K   ���#eff7f6._�5      �� $ PART_ContentHost��6+   5      5      5       5!      �� 5"   
   " ��6   .�5#      ��  � �� 5$      �� 5%      " ��6&   .R�5&      �� $	 border��6!   $ 10��61   ���#f08080$��2q�6X   ���#f08080._�5'      �� $��Center=�6/   $��Center=�6L   5(      5)      5+      5,      5-      .��5.      � 
 � RowDefinitions
 5/   
   �� 50      $	��90* �6   �� 51      $	��51* �6   �� 52      $	��51* �6   �� 53      $	��51* �6   �� 54      $	��51* �6   �� 55      $	��51* �6   �� 56      $	��51* �6   �� 57      $	��51* �6   �� 58      $	��21* �6    � ColumnDefinitions 5:   
   �� 5;      $��5* �6   �� 5<      $	��17* �6   �� 5=      $	��33* �6   .+�5?   
   �� $��0��6   $��4��6!   ���#fafbdc65   �� 5@   
   $)��#Modifique el contacto seleccionado:��6   $��0��6>   $
��Left=�6K   $��Center=�6f   $��4��6�   ���#0081A76�   �� 5A   
   $��Nombre��6   $��1��6!   �� 5B   
   $��1er Apelido��6   $��2��6&   �� 5C   
   $��2do Apelido��6   $��3��6&   �� 5D   
   $��
Comentario��6   $��4��6%   �� 5E   
   $��Teléfono/s��6   $��5��6%   �� 5F   
   $��Email/s��6   $��6��6"   �� 5G   
   -   5         ��Name$ 	IdTextBox��5G      $��1��6%    �� 
IsReadOnly$
 True��62   $��30��6D    �� TextWrapping$
 Wrap=�6O   $ �� HorizontalScrollBarVisibility$ Disabled=�6c   " �� VerticalScrollBarVisibility$
 Auto=�6�    �� TextAlignment$ Center=�6�   $y�	Collapsed=�6�   �� 5H   
   -   5        $ NombreTextBox��5H      $��1��6)   $��2��66   �� 5I   
   -   5        $ Apelido1TextBox��5I      $��2��6+   �� 5J   
   -   5        $ Apelido2TextBox��5J      $��3��6,   �� 5K   
   -   5        $ ComentarioTextBox��5K      $��4��6-   �� 5L   
   -   5        $ TelefonoTextBox��5L      $��5��6+   $
 Wrap=�68    �� AcceptsReturn$
 True��6L   $ Visible=�6a   �� 5M   
   -   5        $ EmailTextBox��5M      $��6��6(   $
 Wrap=�65   $
 True��6I   $ Visible=�6^   �� 5N   
   -   5         ��Name$ EditarButton��5N      $��EDITAR��6'   
�������$��lobster)�6K   $
��Bold"�6`   $��2��6r   $��7��6�   $��Bottom=�6�   $��28��6�   $��80��6�   $��32,0,0,0q�6�   6�   5O      5P      