   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	   J FTripMaintanance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:TripMaintananceTripMaintanance 5   	      TripMaintanance.TripStats   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	   /localclr-namespace:TripMaintanance     5   	     � WindowStyle$
  None=�5      $��Axalp Grotesk)�6-    � Title$ 	TripStats��5	   	   $	��480��6   $	��740��6(   .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��2.5* �6   6.   �� 5      $	��180 �6   6-    � RowDefinitions 5   
   �� 5      $��* �6   6)   .+�5   
   �� ���	#FF171717.+�5      �  5      �� 5      $��* �6'   63   �� 5      $
��1.5* �6'   66    5      �� 5      $	��100 �6$   63   �� 5      $��40 �6$   62   �� 5      $	��290 �6$   63   �� 5       $��40 �6$   62   .+�5$      �� $��0��6   
�������$
��Bold"�68   $��68(�5%      $��3��6   .��62   	Analytics6=   �� 5'      $��1��6   
�������$
��Bold"�68   $��22(�5(      $��3��6   .��62   Most popular bus6D   �� 5*      $��1��6   $��1��6%   
�������$
��Bold"�6H   $��22(�5+      $��3��6   .��62   Class with the most bookings6P      LiveCharts.Wpf.PieChart  5-      -   5        $��Chart��5-      $��2��6,   &    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�5.         InnerRadius$ 80Q�6/   $	��0 0q�6@      Series 5-   9   �� *SeriesCollection+   ChartLegend 5/      "   LiveCharts.Wpf.DefaultLegend  50         
BulletSize$ 10Q�6,   
�������6Q   	   DataTooltip	 52      #   LiveCharts.Wpf.DefaultTooltip  53      
   
BulletSize$
 10Q�6-   
���   �6R   55      #   LiveCharts.Wpf.CartesianChart  56      $ Right=�6L   $��2��6c   $��1��57       56   %   �� *SeriesCollectionBar+   AxisX 59         LiveCharts.Wpf.Axis     Title$ 
Class name��6#      
Foreground
 �����   Labels 6I   �� *Labels+6e      AxisY 5<        $ Bookings��6#   
 �����   LabelFormatter 6G   �� *	Formatter+6n    5>        5?      $ 10Q�6,   
�������6Q   5A      �� 5C      -   5        $��3��5C      $��5, 10, 5, 10q�6&   ���	#FF383838���	#FF383838
�������.��5D   S   Exit5F      5H      5I      �� 5L   
   
���   �$��1��6(   .+�5M      �  5N      �� 5O      $��40 �6$   62   �� 5P      $
��auto �6$   64   �� 5Q      $
��auto �6$   64   �� 5R      $��40 �6$   62   �� 5S      $
��auto �6$   64   �� 5T      $
��auto �6$   64   �� 5U      $
��auto �6$   64   �� 5V      $��40 �6$   62   �� 5W      $
��auto �6$   64    5Z      �� 5[      $��* �6'   �� 5\      $
��1.5* �6'   .+�5_      �� $��0��6   
�������$
��Bold"�68   $��22(�5`      $��2��6   .��62   Summary data6@   �� 5b      -   5         ��Name$ TopBus��5b   8   $��1��6   
�������$��20(�5c      $��2��6   .��62   	Top bus6<   �� 5e      -   5        $ TopClass��5e   8   $��2��6   
�������$��20(�5f      $��2��6   .��62   	Top class6>   �� 5h      $��3��6   
�������$
��Bold"�68   $��22(�5i      $��2��6   .��62   Find an issue6A   �� 5k      $��4��6   
�������$��20(�5l      .��6   	Enter P6'   �� 5n      -   5         ��Name$
 Ptxt��5o   ;   $��4��5n      $��1��6'   $��5q�67   ���	#FF383838���	#FF383838
�������$
��0.99��5o   I   $��18(�6U   �� 5q      -   5        $��re calculate��5q      $��5��60   $��2��6L   $��10, 10, 10, 10q�5r      $	��Top=�6)   $	��150��6A   ���	#FF383838���	#FF383838
�������5s   @   �� 5u      -   5        $ 	Intervals��5u   8   $��6��6   
�������$��20(�5v      $��2��6   $��40, 0q�62   .��6A   
(NA, NA)6K   �� 5x      $��7��6   
�������$
��Bold"�68   $��22(�5y      $��2��6   .��62   
Reports:6<   �� 5{      -   5        $ Advice��5{   8   $��8��6   
�������$��12(�5|      $��2��6   .��62   Busses all seem fine6H   5~      5      5�      5�      