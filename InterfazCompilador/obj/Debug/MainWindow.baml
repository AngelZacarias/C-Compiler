   M S B A M L   `   `   `  ����M  IInterfazCompilador, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:InterfazCompiladorInterfazCompilador  5   	   #    InterfazCompilador.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:InterfazCompilador     5   	   -   5          � Title$  
COMPILADOR��5   	   $��676.667��6   $
��1034��6-    � Icon$ 	icon2.png��6:    � WindowStartupLocation$ CenterScreen=�6K   .��5	      � $	��646��6   $	��Top=�6    � ColumnDefinitions 5
   
   �� 5      $
��495* �6   �� 5      $��3* �6   �� 5      $	��12* �6   �� 5      $��2* �6   �� 5      $
��514* �6   ��5   
   �� 5      .+�5   
   �� -   5         ��Name$ label��5      $��Codigo Fuente��6   $��	10,58,0,0q�67   $��32��6J   $	��Top=�6V   $
��Left=�6n   $	��403��6�   �� 5   
   -   5         ��Name$ TXT_SourceCode��5      $��
10,95,0,10q�6*    �� TextWrapping$
 Wrap=�6>    �� AcceptsReturn .  �� 
AcceptsTab . 	 �� 	IsEnabled	 . $
��Left=�6�   $	��403��6�   &   System.Windows.Controls.DataGrid  5   
   -   5        
  Name$
 DGV_Elements��5         AutoGenerateColumns .  $��427,95,0,10q�6E      SelectionMode$ Single=�6Z   $
��Left=�6�   $	��311��6�   $��5��6�   ��6q   Z�    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5         
Foreground �	#FF0E4A93,  &System.Windows.Controls.DataGridColumn   Header$ ELEMENTO��6>      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6P   ��  �� Path$ Element�6r     5       �	#FF0E4A93$ VALOR��6>    .  6M   �� $ Value�6m     5       �	#FF0E4A93$
 TIPO��6>    .  6L   �� $
 Type�6k   5      � 5    
   $
��Left=�6   $	��100��6/   $��372,117,0,0q�6<   $	��Top=�6Q   $	��100��6i   � 5!   
   $��48��6   $	��Top=�6    ���	#FF080808$��5��6O   .+�5"      �� -   5         ��Name$ BTN_Open��5"      $�� ��6'   
�������$��39��6s   $��40��6~   ��6E   Z� ��5#      �� 5$       �� ImageSource$ 
folder.png��6!   5&      �� 5'      -   5        $ BTN_GO��5'      $�� ��6%   $��40��6E   
������� J� RenderTransformOrigin$ 0.432,0.556&�6{   $��Bottom=�6�   $��43��6�   ��6d   Z� ��5(      �� 5)      $ play.png��6!    j� Stretch$ Uniform=�68   5+      �� 5,      -   5        $ 	LblStatus��5,      $��-��6'   $	��515��63   $
��Bold"�6?   5-      �� 5.   
   -   5         ��Name$ listView��5.      $��4��6%   $
��Left=�65   $	��541��6P   $��
241,95,0,0q�6]   $	��Top=�6q   $	��263��6�    �� View 5/      �� 50      .P�51      �� $��Pasos��6%   52      54      �� 55   
   -	   5        $ 
label_Copy��55      $��Analisis Lexico��6$   $��
427,58,0,0q�6>   $��32��6R   $	��Top=�6^   $
��Left=�6v   $	��311��6�   $ 0.496,0.406&�6�   $��5��6�   �� 56   
   -
   5        $ label_Copy1��56      $��Analisis Lexico��6%   $��
241,58,0,0q�6?   $��32��6S   $	��Top=�6_   $
��Left=�6w   $	��263��6�   $ 0.496,0.406&�6�   $��4��6�   59      5:      