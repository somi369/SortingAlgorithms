using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;
using System.Collections.Generic;

namespace SortingAlgorythms
{
    public partial class MainWindow : Window
    {
        //LiveCharts
        public int sizeOfChartArray;
        public int[] arrayCopy;
        public SeriesCollection liveChartValueSeries { get; set; }
        public string[] labels { get; set; }
        public Func<int, string> formatter { get; set; }
        //Timer
        public DispatcherTimer dispatcherTimer;
        public DateTime startDate;
        //Contructor
        public MainWindow()
        {
            InitializeComponent();
            //LiveCharts
            sizeOfChartArray = 10;
            arrayCopy = new int[10];
            InitializeLiveChart();
            CreateRandomUniqueValues();
            //Timer            
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherTimer.Tick += DispatcherTimerTicker;
            startDate = new DateTime();
        }

        private void DispatcherTimerTicker(object sender, EventArgs e)
        {
            DateTime dateTimeCurrent = DateTime.Now;
            TimeSpan dateDiff = dateTimeCurrent - startDate;
            TimerLBL.Content =
                dateDiff.Hours.ToString() + ':' +
                dateDiff.Minutes.ToString() + ':' +
                dateDiff.Seconds.ToString() + ':' +
                dateDiff.Milliseconds.ToString();
        }
        private void InitializeLiveChart()
        {

            liveChartValueSeries = new SeriesCollection();
            for (int i = 0; i < sizeOfChartArray; i++)
            {
                liveChartValueSeries.Add(new ColumnSeries { Values = new ChartValues<int> { i + 1 }, Fill = Brushes.Black });
            }
            labels = new[] { "Label1", "Label2" };
            formatter = value => value.ToString("N");
            DataContext = this;
        }
        private void ChangeColor(int index, Brush color)
        {
            Dispatcher.Invoke(new Action(() =>
                {
                    ((ColumnSeries)liveChartValueSeries[index]).Fill = color;
                }));
        }
        private void SetEnableForAllButtons(bool state)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                MenuSP.IsEnabled = state;
                ArraySizeSL.IsEnabled = state;
            }));
        }
        private void SetRunTimeLabels(int idx)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                switch (idx)
                {
                    case 0:
                        InfoLBL.Content = "Selection runtime:";
                        BestCaseLBL.Content = "n^2";
                        AvgCaseLBL.Content = "n^2";
                        WorstCaseLBL.Content = "n^2";
                        break;
                    case 1:
                        InfoLBL.Content = "Bubble runtime:";
                        BestCaseLBL.Content = "n";
                        AvgCaseLBL.Content = "n^2";
                        WorstCaseLBL.Content = "n^2";
                        break;
                    case 2:
                        InfoLBL.Content = "Quick sort runtime:";
                        BestCaseLBL.Content = "nlogn";
                        AvgCaseLBL.Content = "nlogn";
                        WorstCaseLBL.Content = "n^2";
                        break;
                    case 3:
                        InfoLBL.Content = "Merge runtime:";
                        BestCaseLBL.Content = "nlogn";
                        AvgCaseLBL.Content = "nlogn";
                        WorstCaseLBL.Content = "nlogn";
                        break;
                    case 4:
                        InfoLBL.Content = "Heap runtime";
                        BestCaseLBL.Content = "nlogn";
                        AvgCaseLBL.Content = "nlogn";
                        WorstCaseLBL.Content = "nlogn";
                        break;
                    default:
                        InfoLBL.Content = "-";
                        BestCaseLBL.Content = "-";
                        AvgCaseLBL.Content = "-";
                        WorstCaseLBL.Content = "-";
                        break;
                }
            }));
        }
        private int GetSeriesElement(int index)
        {
            int result = Convert.ToInt32(liveChartValueSeries[index].Values[0]);
            return result;
        }
        private void SetSeriesElement(int index, int value)
        {
            liveChartValueSeries[index].Values.Clear();
            liveChartValueSeries[index].Values.Add(value);
        }
        private void ResetToUnordered()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                liveChartValueSeries.Clear();
            }));

            for (int i = 0; i < sizeOfChartArray; ++i)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    liveChartValueSeries.Add(new ColumnSeries { Values = new ChartValues<int> { arrayCopy[i] }, Fill = Brushes.Black });
                }));
            }
        }
        private void CreateRandomUniqueValues()
        {

            new Thread(() =>
            {

                // Clear Chart values
                Dispatcher.Invoke(new Action(() =>
                {
                    liveChartValueSeries.Clear();
                }));
                arrayCopy = new int[sizeOfChartArray];

                //Create an array

                var Numbers = Enumerable.Range(1, sizeOfChartArray).ToArray();
                var Rnd = new Random();

                // Shuffle the array
                for (int i = 0; i < Numbers.Length; ++i)
                {
                    int RandomIndex = Rnd.Next(Numbers.Length);
                    int Temp = Numbers[RandomIndex];
                    Numbers[RandomIndex] = Numbers[i];
                    Numbers[i] = Temp;
                }
                arrayCopy = Numbers;

                // Change Chart values
                for (int i = 0; i < sizeOfChartArray; ++i)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        liveChartValueSeries.Add(new ColumnSeries { Values = new ChartValues<int> { Numbers[i] }, Fill = Brushes.Black });
                    }));
                }



            }).Start();
        }
        private void ArraySizeSL_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            sizeOfChartArray = (int)ArraySizeSL.Value;
            CreateRandomUniqueValues();
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void XBTN_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
        //Selection sort
        private void SelectionSortBTN_Click(object sender, RoutedEventArgs e)
        {
            SetRunTimeLabels(0);
            ResetToUnordered();
            new Thread(() =>
            {
                startDate = DateTime.Now;
                dispatcherTimer.Start();
                SetEnableForAllButtons(false);
                SelectionSort();
                SetEnableForAllButtons(true);
                dispatcherTimer.Stop();
            }).Start();
        }
        private void SelectionSort()
        {
            for (int i = 0; i < liveChartValueSeries.Count - 1; i++)
            {
                for (int j = i + 1; j < liveChartValueSeries.Count; j++)
                {
                    int a = Convert.ToInt32(liveChartValueSeries[i].Values[0]);
                    int b = Convert.ToInt32(liveChartValueSeries[j].Values[0]);
                    if (a > b)
                    {
                        liveChartValueSeries[i].Values.Clear();
                        liveChartValueSeries[j].Values.Clear();

                        liveChartValueSeries[i].Values.Add(b);
                        liveChartValueSeries[j].Values.Add(a);

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
            SetRunTimeLabels(1);
            ResetToUnordered();
            new Thread(() =>
                {
                    startDate = DateTime.Now;
                    dispatcherTimer.Start();
                    SetEnableForAllButtons(false);
                    BubbleSort();
                    SetEnableForAllButtons(true);
                    dispatcherTimer.Stop();
                }).Start();
        }
        private void BubbleSort()
        {
            for (int i = 0; i < sizeOfChartArray; i++)
            {
                for (int j = 0; j < sizeOfChartArray - i - 1; j++)
                {
                    int a = Convert.ToInt32(liveChartValueSeries[j].Values[0]);
                    int b = Convert.ToInt32(liveChartValueSeries[j + 1].Values[0]);
                    Thread.Sleep(100);

                    if (a > b)
                    {
                        liveChartValueSeries[j].Values.Clear();
                        liveChartValueSeries[j + 1].Values.Clear();

                        liveChartValueSeries[j].Values.Add(b);
                        liveChartValueSeries[j + 1].Values.Add(a);

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
            SetRunTimeLabels(2);
            ResetToUnordered();
            int[] ChartValueArray = new int[sizeOfChartArray];
            for (int i = 0; i < sizeOfChartArray; i++)
            {
                ChartValueArray[i] = GetSeriesElement(i);
            }
            new Thread(() =>
            {
                startDate = DateTime.Now;
                dispatcherTimer.Start();
                SetEnableForAllButtons(false);
                Quick_Sort(ChartValueArray, 0, sizeOfChartArray - 1);
                SetEnableForAllButtons(true);
                dispatcherTimer.Stop();
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

                    SetSeriesElement(left, arr[right]);
                    SetSeriesElement(right, temp);

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
            SetRunTimeLabels(3);
            ResetToUnordered();
            List<int> chartValueList = new List<int>();
            for (int i = 0; i < sizeOfChartArray; i++)
            {
                chartValueList.Add(GetSeriesElement(i));
            }

            List<int> indexList = new List<int>();
            for (int i = 0; i < sizeOfChartArray; i++)
            {
                indexList.Add(i);
            }
            new Thread(() =>
            {
                startDate = DateTime.Now;
                dispatcherTimer.Start();
                SetEnableForAllButtons(false);
                chartValueList = MergeSort(chartValueList, indexList);
                Thread.Sleep(100);
                for (int i = 0; i < chartValueList.Count; i++)
                {
                    SetSeriesElement(i, chartValueList[i]);
                }
                SetEnableForAllButtons(true);
                dispatcherTimer.Stop();

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
                SetSeriesElement(indexResult[i], result[i]);
            }
            Thread.Sleep(100);
            return result;
        }
        //Heap sort
        private void HeapSortBTN_Click(object sender, RoutedEventArgs e)
        {
            SetRunTimeLabels(4);
            ResetToUnordered();
            int[] ChartValueArray = new int[sizeOfChartArray];
            for (int i = 0; i < sizeOfChartArray; i++)
            {
                ChartValueArray[i] = GetSeriesElement(i);
            }
            new Thread(() =>
            {
                startDate = DateTime.Now;
                dispatcherTimer.Start();
                SetEnableForAllButtons(false);
                HeapSort(ChartValueArray);
                SetEnableForAllButtons(true);
                dispatcherTimer.Stop();
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

                SetSeriesElement(0, array[i]);
                SetSeriesElement(i, temp);
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
                ChangeColor(left, Brushes.LightGreen);
                ChangeColor(largest, Brushes.LightGreen);
                Thread.Sleep(100);

                ChangeColor(left, Brushes.Black);
                ChangeColor(largest, Brushes.Black);
                Thread.Sleep(100);

                largest = left;
            }
            if (right < length && array[right] > array[largest])
            {
                ChangeColor(left, Brushes.Red);
                ChangeColor(largest, Brushes.Red);
                Thread.Sleep(100);

                ChangeColor(left, Brushes.Black);
                ChangeColor(largest, Brushes.Black);
                Thread.Sleep(100);

                largest = right;
            }
            if (largest != i)
            {
                int swap = array[i];


                ChangeColor(left, Brushes.LightGreen);
                ChangeColor(largest, Brushes.LightGreen);
                Thread.Sleep(100);


                ChangeColor(left, Brushes.Black);
                ChangeColor(largest, Brushes.Black);
                Thread.Sleep(100);


                SetSeriesElement(i, array[largest]);
                SetSeriesElement(largest, swap);
                Thread.Sleep(100);

                array[i] = array[largest];
                array[largest] = swap;
                Heapify(array, length, largest);
            }
        }

        
    }
}