   M S B A M L   `   `   `  ����L  HCommercialCompany, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CommercialCompany.WPFCommercialCompany.WPF  5   	   #    CommercialCompany.WPF.Profile   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CommercialCompany.WPF     5   	     � Title$  Профиль��5   	   $	��550��6   $	��800��6&    � Icon$ /Resources/icon.ico��62   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      $	��370 �6   6+    � ColumnDefinitions 5   
   �� 5      $	��180 �6   6-   �� 5      6!   .+�5   
   �� -   5        $��profilePhoto��5      $��0��6$   $��
10,10,22,0q�61    J� RenderTransformOrigin$ -0.94,0.398&�6E   �� 5   
   -   5        $��profileType��5      $
��TYPE��6$   $��1��63   $��30��6C   $	��100��6O   $��102,45,410,67q�6[   �� 5   
   -   5        $��profileName��5      $
��NAME��6#   $��0��62   $��1��6?   $��30��6O   $��22,10,381,102q�6[   �� 5   
   -   5        $��butBack��5      $��
Выход��6!   $��2��61   $��20,340,22,5q�6>   6S   �� 5   
   $��Имя:��6   $
��Left=�6   $��
163,10,0,0q�6:   $	��Top=�6N   $��2��6f   $
��Bold"�6z   �� 5   
   $&�� Тип пользователя:��6   $
��Left=�6,   $��
163,45,0,0q�6G   $	��Top=�6[   $��2��6s   $
��Bold"�6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGrid��5         CanUserAddRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6;   $
��Left=�6K   $	��330��6f   $��20,5,0,0q�6s   $��1��6�   $	��Top=�6�   $	��749��6�   $��2��6�   ��5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5      -   5        5      5!      �� 5#   
   $��Таблица:��6   $
��Left=�6#   $��163,106,0,0q�6>   $	��Top=�6S   $��2��6k   $	��119��6   $
��Bold"�6�   �� 5$   
   -   5        $��tableComboBox��5$      $��1��6(   $
��Left=�68   $��
49,106,0,0q�6S   $	��Top=�6g   $	��127��6   $��26��6�   6�   �� 5%   
   -   5        $��Добавить��5%      $��1��6$   $
��Left=�64   $��202,106,0,0q�6O   $	��Top=�6d   $��93��6|   $��26��6�   6�   �� 5&   
   -	   5        $��Удалить��5&      $��1��6#   $
��Left=�63   $��398,106,0,0q�6N   $	��Top=�6c   $��93��6{   $��26��6�   6�   �� 5'   
   -
   5        $��Обновить��5'      $��1��6$   $
��Left=�64   $��496,106,0,0q�6O   $	��Top=�6d   $��93��6|   $��26��6�   6�   �� 5(   
   -   5        $ ��Редактировать��5(      $��1��6)   $
��Left=�69   $��300,106,0,0q�6T   $	��Top=�6i   $��93��6�   $��26��6�   6�   �� 5)   
   -   5         ��Name$ searchTextBox��5)      $��1��6)   $
��Left=�69   $��
363,10,0,0q�6T   	 �� TextWrapping$
	 Wrap=�6h   $	��Top=�6|   $	��170��6�   $��20��6�   �� 5*   
   -   5        
 ��Name$
 searchButton��5*      $��1��6'   $��
Поиск��67   $
��Left=�6G   $��
538,10,0,0q�6b   $	��Top=�6v   $��49��6�   6�   5+      5,      