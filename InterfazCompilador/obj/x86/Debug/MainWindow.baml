   M S B A M L   `   `   `  ����M  IInterfazCompilador, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:InterfazCompiladorInterfazCompilador  5   	   #    InterfazCompilador.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:InterfazCompilador     5   	   -   5          � Title$  
COMPILADOR��5   	   $��676.667��6   $
��1034��6-    � Icon$ 	icon2.png��6:    � WindowStartupLocation$ CenterScreen=�6K   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��255* �6   �� 5      �� 5      $
��257* �6   ��5   
   �� 5      .+�5   
   �� -   5         ��Name$ label��5      $��Codigo Fuente��6   $��	10,58,2,0q�67   $��32��6J   $	��Top=�6V   �� 5   
   -   5         ��Name$ TXT_SourceCode��5      $��	10,95,2,0q�6*    �� TextWrapping$
 Wrap=�6=    �� AcceptsReturn .  �� 
AcceptsTab . 	 �� 	IsEnabled	 . $	��323��6�   $	��Top=�6�   &   System.Windows.Controls.DataGrid  5   
   -   5        
  Name$
 DGV_Elements��5      $��1��6)      AutoGenerateColumns .  $��
10,0,10,10q�6R      SelectionMode$ Single=�6f   $��3��6}   $	��213��6�   $��Bottom=�6�   ��6�   Z�    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5         
Foreground �	#FF0E4A93,  &System.Windows.Controls.DataGridColumn   Header$ ELEMENTO��6>      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6P   ��  �� Path$ Element�6r     5       �	#FF0E4A93$ VALOR��6>    .  6M   �� $ Value�6m     5       �	#FF0E4A93$
 TIPO��6>    .  6L   �� $
 Type�6k   5      �� 5   
   -   5        $ TXT_Copy��5      $��	0,95,10,0q�6$   $
 Wrap=�67   $�� ��6K    .  . $��Right=�6z   $	��503��6�   $	��323��6�   $	��Top=�6�   $��2��6�   �� 5   
   -   5        $ label1��5      $��Información��6    $��29��66   $��	1,58,10,0q�6B   $	��Top=�6U   $��2��6m   � 5    
   $
��Left=�6   $	��100��6/   $��372,117,0,0q�6<   $	��Top=�6Q   $	��100��6i   � 5!   
   $��48��6   $	��Top=�6    ���	#FF080808$��3��6O   .+�5"      �� -   5         ��Name$ BTN_Open��5"      $�� ��6'   
�������$��39��6s   $��40��6~   ��6E   Z� ��5#      �� 5$       �� ImageSource$ 
folder.png��6!   5&      �� 5'      -   5        $ BTN_GO��5'      $�� ��6%   $��40��6E   
������� J� RenderTransformOrigin$ 0.432,0.556&�6{   $��Bottom=�6�   $��43��6�   ��6d   Z� ��5(      �� 5)      $ play.png��6!    j� Stretch$ Uniform=�68   5+      5,      5/      50      