   M S B A M L   `   `   `  ����J  FTripMaintanance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:TripMaintananceTripMaintanance  5   	   !    TripMaintanance.BusFeedback   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:TripMaintanance     5   	     � WindowStyle$
  None=�5       � Title$ BusFeedback��5   	   $	��440��6   $	��530��6*   .��5	      � ���	#FF171717 � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��100 �6   6+   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��30 �6   6*   �� 5      $	��170 �6   6+   �� 5      $
��auto �6   6,   .+�5   
   �� -   5        $��
txtBusName��5      $��2��6   $��0��61   $��
5, 5, 5, 5q�5   	   ���	#FF383838���	#FF383838
������� �� SelectionBrush �	#FF383838$��3��5   7   $
��Bold"�5   	   6   �� 5   
   $��0��6   
�������$
��Bold"�60   $��72(�5      $��3��6   .��62   New comment6?   �� 5!   
   $��1��6   
�������$
��Bold"�60   .��6B   Write the bus name here:6\   �� 5#   
   $��3��6   
�������$
��Bold"�60   .��6B   Write comment in here:6Z   �� 5%   
   -   5        $��txtBusComment��5%      $��4��6   $��0��64   $��
5, 5, 5, 5q�5&   	   ���	#FF383838���	#FF383838
������� �	#FF383838$��3��5'   7   $
��Bold"�5(   	   6   �� 5*   
   -   5        $��5��5*      $��0��6   $��5, 10, 5, 3q�6.   ���	#FF383838���	#FF383838
�������.��5+   S   Submit5-      �� 5.   
   -   5        $��5��5.      $��1��6   $��5, 10, 5, 3q�6.   ���	#FF383838���	#FF383838
�������.��5/   S   Exit51      52      53      