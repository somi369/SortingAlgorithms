   M S B A M L   `   `   `  ���� L  HSortingAlgorythms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SortingAlgorythmsSortingAlgorythms  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf "    SortingAlgorythms.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:SortingAlgorythms     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     $	��450��$	��800��  � WindowStartupLocation$  CenterScreen=� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � 	Resources    	MyButton2 �� Property �� Value �� 
TargetType ��Name �� Triggers	 P� Property
 P� Value��%F  &	        �� .���  �@�OverridesDefaultStyle . ��  � �� �	#FF222222��  � ��
 �������  � ��$ 0,10,0,0q���  � �� �� " ��.R�� #��z��.+��� $ MyContentPresenter��#��z��$��Center=�$��Center=� P� 	 � {�
 .  . ���  �@�OverridesDefaultStyle . ��  � �� �	#FF222222��  � ��
 �����P� 	 � {�
 . . ���  �@�OverridesDefaultStyle . ��  � �� �	#FF222222��  � ��
 �����P� 	 �@J�IsMouseOver
 . . ���  � ��
 �������  �@�Cursor$
 Hand��.��� -   ���	#FF222222 � RowDefinitions �� $��* ��� $	��10* �.+�� $��0�� � ColumnDefinitions �� $��3* ��� $��3* ��� $��* �.+��� 
�������$��25(�$��Sorting Algorithms��$��Century Gothic)�$��0���� $��1��.+��  �� $��* ��� $��* �.+��� $��0��
�������$��15(�$��Center=�$��Right=�$��Array size:��$��Century Gothic)��� $��1��$��Center=�$
��Left=�{�.  $��Century Gothic)�����  �� ElementName$ ArraySizeSL�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=��� -   $��ArraySizeSL�� � Minimum$ 10Q� � Maximum$	 100Q�$	��150��$��Center=� �� TickPlacement$
 None=� �� TickFrequency$ 1Q� �� IsSnapToTickEnabled . �� -    ��Name$
 XBTN��$��X��$��70��$��25��$��Right=�$��0q�$��Center=�$��Center=�
�������$��Century Gothic)�$��18(�$��2����Z� � $��1�� �� $��* ��� $��4* �.+��� -    ��Name$ MenuSP��$��0��.+��� -   $ SelectionSortBTN��$��Selection sort��#��[  $��0q�$��Century Gothic)�$��18(��� -   $ BubbleSortBTN��$��Bubble sort��#��[  $��0q�$��Century Gothic)�$��18(��� -   $ QuickSortBTN��$��
Quick sort��#��[  $��0q�$��Century Gothic)�$��18(��� -   $ MergeSortBTN��$��
Merge sort��#��[  $��0q�$��Century Gothic)�$��18(��� -	   $ HeapSortBTN��$��	Heap sort��#��[  $��0q�$��Century Gothic)�$��18(��� $��Timer:��$��Century Gothic)�$��18(�
�������$��Center=�$��Center=�$��0 30 0 0q���Z� �� -
    ��Name$ TimerLBL��$��Century Gothic)�$��18(�
�������$��--:--:--:--��$��Center=�$��Center=���Z� �� -   $ InfoLBL��$��Century Gothic)�$��18(�
�������$��Center=�$��Center=�$��0 30 0 0q���Z� .��-�� .+��  �� $��6* ��� $
��3.5* � �� $��* ��� $��* ��� $��* �.+��� $��0��$��0��$��Century Gothic)�$��18(�
���������Z� .��
Best case:�� -   $ BestCaseLBL��$��0��$��1��$��Century Gothic)�$��18(�
���������Z� .��-�� $��1��$��0��$��Century Gothic)�$��18(�
���������Z� .��
Avg. case:�� -   $ 
AvgCaseLBL��$��1��$��1��$��Century Gothic)�$��18(�
���������Z� .��-�� $��2��$��0��$��Century Gothic)�$��18(�
���������Z� .��Worst case:�� -   $ WorstCaseLBL��$��2��$��1��$��Century Gothic)�$��18(�
���������Z� .��-�� -   $ LiveChartSP��$��1��$��0,0,0,10q�.+�#  LiveCharts.Wpf.CartesianChart  -     Name$ 
LiveCHART1��$��10,10,10.2,10q�$	��404��&   LiveCharts.Wpf.Charts.Base.Chart   	Hoverable .     DisableAnimations . #��� ��   Series �� *liveChartValueSeries+   DataTooltip Z�    AxisY   LiveCharts.Wpf.Axis     Title$ Y��   MinValue$ 0Q�    LabelFormatter  �� *	formatter+