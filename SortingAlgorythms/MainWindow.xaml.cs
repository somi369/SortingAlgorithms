using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SortingAlgorythms
{
    public partial class MainWindow : Window
    {
        public SeriesCollection LiveChartValueSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        int SizeOfChartArray;
        public MainWindow()
        {
            InitializeComponent();

            SizeOfChartArray = 40;

            InitializeLiveChart();

            CreateRandomUniqueValues();
        }
        private void InitializeLiveChart()
        {
            LiveChartValueSeries = new SeriesCollection();
            for (int i = 0; i < SizeOfChartArray; i++)
            {
                LiveChartValueSeries.Add(new ColumnSeries { Values = new ChartValues<int> { i + 1 }, Fill = Brushes.Black });
            }
            Labels = new[] { "Label1", "Label2" };
            Formatter = value => value.ToString("N");
            DataContext = this;
        }
        private void ChangeColor(int index, System.Windows.Media.Brush color)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                ((ColumnSeries)LiveChartValueSeries[index]).Fill = color;
            }));
        }
        
        private void setEnableForAllButtons(bool state)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                StackPanel0.IsEnabled = state;
            }));
        }

        private int getSeriesElement(int index)
        {
            int result = Convert.ToInt32(LiveChartValueSeries[index].Values[0]);
            return result;
        }
        private void setSeriesElement(int index, int value)
        {
            LiveChartValueSeries[index].Values.Clear();
            LiveChartValueSeries[index].Values.Add(value);
        }
        private void UniqueValuesBTN_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                CreateRandomUniqueValues();
            }).Start();
        }
        private void CreateRandomUniqueValues()
        {

            new Thread(() =>
            {

                // Clear Chart values
                for (int i = 0; i < SizeOfChartArray; i++)
                {
                    LiveChartValueSeries[i].Values.Clear();
                }

                //Create an array

                var Numbers = Enumerable.Range(1, SizeOfChartArray).ToArray();
                var Rnd = new Random();

                // Shuffle the array
                for (int i = 0; i < Numbers.Length; ++i)
                {
                    int RandomIndex = Rnd.Next(Numbers.Length);
                    int Temp = Numbers[RandomIndex];
                    Numbers[RandomIndex] = Numbers[i];
                    Numbers[i] = Temp;
                }

                // Change Chart values
                for (int i = 0; i < Numbers.Length; ++i)
                {
                    LiveChartValueSeries[i].Values.Add(Numbers[i]);
                }


            }).Start();
        }
        //Selection sort
        private void SelectionSortBTN_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                setEnableForAllButtons(false);
                SelectionSort();
                setEnableForAllButtons(true);
            }).Start();
        }
        private void SelectionSort()
        {
            for (int i = 0; i < LiveChartValueSeries.Count - 1; i++)
            {
                for (int j = i + 1; j < LiveChartValueSeries.Count; j++)
                {
                    int a = Convert.ToInt32(LiveChartValueSeries[i].Values[0]);
                    int b = Convert.ToInt32(LiveChartValueSeries[j].Values[0]);
                    if (a > b)
                    {
                        LiveChartValueSeries[i].Values.Clear();
                        LiveChartValueSeries[j].Values.Clear();

                        LiveChartValueSeries[i].Values.Add(b);
                        LiveChartValueSeries[j].Values.Add(a);

                        ChangeColor(i, Brushes.LightGreen);
                        ChangeColor(j, Brushes.LightGreen);
                        Thread.Sleep(100);
                    }
                    else
                    {
                        ChangeColor(i, Brushes.Red);
                        ChangeColor(j, Brushes.Red);
                        Thread.Sleep(100);
                    }
                    ChangeColor(i, Brushes.Black);
                    ChangeColor(j, Brushes.Black);
                    Thread.Sleep(100);
                }
            }
        }
        //Bubble sort
        private void BubbleSortBTN_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                setEnableForAllButtons(false);
                BubbleSort();
                setEnableForAllButtons(true);
            }).Start();
        }
        private void BubbleSort()
        {
            for (int i = 0; i < SizeOfChartArray; i++)
            {
                for (int j = 0; j < SizeOfChartArray - i - 1; j++)
                {
                    int a = Convert.ToInt32(LiveChartValueSeries[j].Values[0]);
                    int b = Convert.ToInt32(LiveChartValueSeries[j + 1].Values[0]);
                    Thread.Sleep(100);

                    if (a > b)
                    {
                        LiveChartValueSeries[j].Values.Clear();
                        LiveChartValueSeries[j + 1].Values.Clear();

                        LiveChartValueSeries[j].Values.Add(b);
                        LiveChartValueSeries[j + 1].Values.Add(a);

                        ChangeColor(j, Brushes.LightGreen);
                        ChangeColor(j + 1, Brushes.LightGreen);
                        Thread.Sleep(100);
                    }
                    else
                    {
                        ChangeColor(j, Brushes.Red);
                        ChangeColor(j + 1, Brushes.Red);
                        Thread.Sleep(100);
                    }
                    ChangeColor(j, Brushes.Black);
                    ChangeColor(j + 1, Brushes.Black);

                }
            }
        }
        //Quick sort
        private void QuickSortBTN_Click(object sender, RoutedEventArgs e)
        {
            int[] ChartValueArray = new int[SizeOfChartArray];
            for (int i = 0; i < SizeOfChartArray; i++)
            {
                ChartValueArray[i] = getSeriesElement(i);
            }
            new Thread(() =>
            {
                setEnableForAllButtons(false);
                Quick_Sort(ChartValueArray, 0, SizeOfChartArray - 1);
                setEnableForAllButtons(true);
            }).Start();
        }
        private void Quick_Sort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    Quick_Sort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(arr, pivot + 1, right);
                }
            }

        }
        private int Partition(int[] arr, int left, int right)
        {
            ChangeColor(left, Brushes.Blue);
            Thread.Sleep(100);

            int pivot = arr[left];
            
            ChangeColor(left, Brushes.Black);
            Thread.Sleep(100);

            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right])
                    {
                        return right;
                    }

                    ChangeColor(left, Brushes.LightGreen);
                    ChangeColor(right, Brushes.LightGreen);
                    Thread.Sleep(100);

                    int temp = arr[left];

                    setSeriesElement(left, arr[right]);
                    setSeriesElement(right, temp);

                    arr[left] = arr[right];
                    arr[right] = temp;
                    
                    ChangeColor(left, Brushes.Black);
                    ChangeColor(right, Brushes.Black);
                    Thread.Sleep(100);
                }
                else
                {
                    ChangeColor(left, Brushes.Red);
                    ChangeColor(right, Brushes.Red);
                    Thread.Sleep(100);

                    ChangeColor(left, Brushes.Black);
                    ChangeColor(right, Brushes.Black);
                    Thread.Sleep(100);

                    return right;
                }
            }
        }
        //Merge sort
        private void MergeSortBTN_Click(object sender, RoutedEventArgs e)
        {
            List<int> chartValueList = new List<int>();
            for (int i = 0; i < SizeOfChartArray; i++)
            {
                chartValueList.Add(getSeriesElement(i));
            }

            List<int> indexList = new List<int>();
            for (int i = 0; i < SizeOfChartArray; i++)
            {
                indexList.Add(i);
            }
            new Thread(() =>
            {
                setEnableForAllButtons(false);
                chartValueList = MergeSort(chartValueList, indexList);
                Thread.Sleep(100);
                for (int i = 0; i < chartValueList.Count; i++)
                {
                    setSeriesElement(i, chartValueList[i]);
                }
                setEnableForAllButtons(true);

            }).Start();
        }
        private List<int> MergeSort(List<int> unsorted, List<int> indexList)
        {
            if (unsorted.Count <= 1)
            {
                
                return unsorted;
            }

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            List<int> leftIndexes = new List<int>();
            List<int> rightIndexes = new List<int>();

            int middle = unsorted.Count / 2;
            for (int i = 0; i < middle; i++)
            {
                left.Add(unsorted[i]);
                leftIndexes.Add(indexList[i]);
            }
            for (int i = middle; i < unsorted.Count; i++)
            {
                right.Add(unsorted[i]);
                rightIndexes.Add(indexList[i]);

            }

            left = MergeSort(left, leftIndexes);
            right = MergeSort(right, rightIndexes);
            return Merge(left, right, leftIndexes, rightIndexes);
        }
        private List<int> Merge(List<int> left, List<int> right, List<int> leftIndexes, List<int> rightIndexes)
        {
            List<int> result = new List<int>();
            List<int> indexResult = new List<int>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left.First() <= right.First())
                    {

                        ChangeColor(leftIndexes.First(), Brushes.Red);
                        ChangeColor(rightIndexes.First(), Brushes.Red);
                        Thread.Sleep(100);

                        result.Add(left.First());
                        left.Remove(left.First());

                        indexResult.Add(leftIndexes.First());
                        leftIndexes.Remove(leftIndexes.First());

                        ChangeColor(indexResult.Last(), Brushes.Black);
                        ChangeColor(rightIndexes.First(), Brushes.Black);
                        Thread.Sleep(100);

                    }
                    else
                    {
                        ChangeColor(leftIndexes.First(), Brushes.LightGreen);
                        ChangeColor(rightIndexes.First(), Brushes.LightGreen);
                        Thread.Sleep(100);

                        result.Add(right.First());
                        right.Remove(right.First());

                        indexResult.Add(rightIndexes.First());
                        rightIndexes.Remove(rightIndexes.First());

                        ChangeColor(leftIndexes.First(), Brushes.Black);
                        ChangeColor(indexResult.Last(), Brushes.Black);
                        Thread.Sleep(100);

                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());

                    indexResult.Add(leftIndexes.First());
                    leftIndexes.Remove(leftIndexes.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());
                    right.Remove(right.First());

                    indexResult.Add(rightIndexes.First());
                    rightIndexes.Remove(rightIndexes.First());
                }
            }

            for (int i = 0; i < indexResult.Count; i++)
            {
                setSeriesElement(indexResult[i], result[i]);
            }
            Thread.Sleep(100);
            return result;
        }
        //Heap sort -> need color when comparing
        private void HeapSortBTN_Click(object sender, RoutedEventArgs e)
        {
            int[] ChartValueArray = new int[SizeOfChartArray];
            for (int i = 0; i < SizeOfChartArray; i++)
            {
                ChartValueArray[i] = getSeriesElement(i);
            }
            new Thread(() =>
            {
                setEnableForAllButtons(false);
                HeapSort(ChartValueArray);
                setEnableForAllButtons(true);
            }).Start();
        }
        private void HeapSort(int[] array)
        {
            var length = array.Length;
            for (int i = length / 2 - 1; i >= 0; i--)
            {
                Heapify(array, length, i);
            }
            for (int i = length - 1; i >= 0; i--)
            {
                int temp = array[0];

                setSeriesElement(0, array[i]);
                setSeriesElement(i, temp);
                Thread.Sleep(100);

                array[0] = array[i];
                array[i] = temp;

                Heapify(array, i, 0);
            }
        }
        private void Heapify(int[] array, int length, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < length && array[left] > array[largest])
            {
                largest = left;
            }
            if (right < length && array[right] > array[largest])
            {
                largest = right;
            }
            if (largest != i)
            {
                int swap = array[i];

                setSeriesElement(i, array[largest]);
                setSeriesElement(largest, swap);
                Thread.Sleep(100);

                array[i] = array[largest];
                array[largest] = swap;
                Heapify(array, length, largest);
            }
        }
    }
}