   M S B A M L   `   `   `  ����D  @ProgTZApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ProgTZApp	ProgTZApp  5   	       ProgTZApp.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ProgTZApp     5   	     � Title$  	ExitGames��5   	   $	��480��6   $	��800��6(   $	��480��64   $	��800��6D   $	��480��6S   $	��800��6c   .��5	      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5   
   �� -   5        $��BorderWaveTop��5      $ ��images/border-wave-top.png��6%   $��Stretch=�6I   $��Stretch=�6e   $��1��6�   �� 5   
   -   5        $��	LogoImage��5      $��images/logo.png��6!   $��Center=�6:   $��Center=�6U   $��1��6r   $	��130��6   $	��130��6�   $	��130��6�   $	��130��6�   $��5q�6�   �� 5   
   -   5        $��LoginTextBlock��5      $��2��6*   $
��auto��67   $
��auto��6E   $��Bottom=�6R   $��Center=�6m   $ ��Введите логин:��6�   $��5q�6�   �� 5   
   -   5         ��Name$ BorderLoginTextBox��5      $��1q�5      
������ 
������� �� CornerRadius$ 10��5!      $
��auto��5"      $
��auto��5#      $	��220��5$      $��22��5%      $��Center=�5&      $��Center=�5'      $��3��5(      $��5q�5)      ._�5*      �� -   5         ��Name$ LoginTextBox��5*      $
��auto��6,   $
��auto��6:   $��20��6G   $	��200��6V   $��20��6e   $	��200��6t   $��Center=�6�   $��Right=�6�   $��0q�6�   
������ 
������ 6�   5+      �� 5-   
   -   5        $��PasswordTextBlock��5-      $��4��6-   $
��auto��6:   $
��auto��6H   $��Bottom=�6U   $��Center=�6p   $"��Введите пароль:��6�   $��5q�6�   �� 5/   
   -   5        $ BorderPasswordTextBox��50      $��1q�51      
�������$ 10��53      $
��auto��54      $
��auto��55      $	��220��56      $��22��57      $��Center=�58      $��Center=�59      $��5��5:      $��5q�5;      ._�5<      G� -   5        $��PassPasswordBox��5<      $
��auto��61   $
��auto��6?   $��20��6L   $	��200��6[   $��20��6j   $	��200��6y   $��Center=�6�   $��Right=�6�   $��0q�6�   
������ 
������ 6�   5=      �� 5?   
   -	   5        $ BorderLoginButton��5@      $��1q�5A      ���#00ad39$ 15��5C      $
��auto��5D      $
��auto��5E      $	��120��5F      $��35��5G      $��Center=�5H      $��Center=�5I      $��6��5J      $��25q�5K      ._�5L      �� -
   5        $��LoginButton��5L      $
��auto��6(   $
��auto��66   $��30��6C   $	��100��6R   $��Center=�6a   $��Center=�6|   $��
Войти��6�   ���#00ad39$��0q�6�   
������ 
�������$��15(�6�   $��	UltraBold"�6  6$  5M      �� 5O   
   -   5        $��	LoginIcon��5O      $��images/login-icon.png��6!   $��Center=�6@   $��Center=�6[   $��3��6x   $��15��6�   $��15��6�   $��15��6�   $��15��6�   $��	0 0 200 0q�6�   �� 5P   
   -   5        $��PassIcon��5P      $��images/password-icon.png��6    $��Center=�6B   $��Center=�6]   $��5��6z   $��15��6�   $��15��6�   $��15��6�   $��15��6�   $��	0 0 200 0q�6�   5Q      5R      