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

        public SeriesCollection LiveChartValueSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        int SizeOfChartArray;
        public MainWindow()
        {
            InitializeComponent();
            //Livechart
            InitializeLiveChart();
            SizeOfChartArray = 20;
            CreateRandomUniqueValues();
        }
        private void InitializeLiveChart()
        {
            LiveChartValueSeries = new SeriesCollection();
            for (int i = 0; i < 20; i++)
            {
                LiveChartValueSeries.Add(new ColumnSeries { Values = new ChartValues<int> { i + 1 }, Fill = Brushes.Black });
            }
            Labels = new[] { "Label1", "Label2" };
            Formatter = value => value.ToString("N");
            DataContext = this;
        }
        private void ChangeColorToRed(int index)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                ((ColumnSeries)LiveChartValueSeries[index]).Fill = Brushes.Red;
            }));
        }
        private void ChangeColorToGreen(int index)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                ((ColumnSeries)LiveChartValueSeries[index]).Fill = Brushes.LightGreen;
            }));
        }
        private void ChangeColorToBlack(int index)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                ((ColumnSeries)LiveChartValueSeries[index]).Fill = Brushes.Black;
            }));
        }
        private void ChangeColorToBlue(int index)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                ((ColumnSeries)LiveChartValueSeries[index]).Fill = Brushes.Blue;
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
                //Button disable
                Dispatcher.BeginInvoke(new Action(() => { UniqueValuesBTN.IsEnabled = false; }));

                CreateRandomUniqueValues();

                //Button enable
                Dispatcher.BeginInvoke(new Action(() => { UniqueValuesBTN.IsEnabled = true; }));
            }).Start();
        }

        private void CreateRandomUniqueValues()
        {

            new Thread(() =>
            {
                
                // Clear Chart values
                for (int i = 0; i < 20; i++)
                {
                    LiveChartValueSeries[i].Values.Clear();
                }

                //Create an array

                var Numbers = Enumerable.Range(1, 20).ToArray();
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
        private void SortBTN_Click(object sender, RoutedEventArgs e)
        {

            new Thread(() =>
            {
                for (int i = 0; i < LiveChartValueSeries.Count - 1; i++)
                {
                    for (int j = i + 1; j < LiveChartValueSeries.Count; j++)
                    {
                        int a = Convert.ToInt32(LiveChartValueSeries[i].Values[0]);
                        int b = Convert.ToInt32(LiveChartValueSeries[j].Values[0]);

                        Dispatcher.Invoke(new Action(() =>
                        {
                            //ChangeColorToBlue((ColumnSeries)LiveChartValueSeries[i]);
                            //ChangeColorToBlue((ColumnSeries)LiveChartValueSeries[j]);
                        }));
                        Thread.Sleep(100);


                        if (a > b)
                        {

                            LiveChartValueSeries[i].Values.Clear();
                            LiveChartValueSeries[j].Values.Clear();

                            LiveChartValueSeries[i].Values.Add(b);
                            LiveChartValueSeries[j].Values.Add(a);

                            Dispatcher.Invoke(new Action(() =>
                            {
                                ChangeColorToGreen(i);
                                ChangeColorToGreen(j);
                            }));
                            Thread.Sleep(100);
                        }
                        else
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                ChangeColorToRed(i);
                                ChangeColorToRed(j);
                            }));
                            Thread.Sleep(100);
                        }
                        Dispatcher.Invoke(new Action(() =>
                        {
                            ChangeColorToBlack(i);
                            ChangeColorToBlack(j);
                        }));
                        Thread.Sleep(100);
                    }
                }
            }).Start();
        }

        //MergeSort functions
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
                chartValueList = MergeSort(chartValueList, indexList);
                Thread.Sleep(1000);
                for (int i = 0; i < chartValueList.Count; i++)
                {
                    setSeriesElement(i, chartValueList[i]);
                }

            }).Start();
        }
        private List<int> MergeSort(List<int> unsorted, List<int> indexList)
        {
            if (unsorted.Count <= 1)
                return unsorted;

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            List<int> leftIndexes = new List<int>();
            List<int> rightIndexes = new List<int>();


            int middle = unsorted.Count / 2;
            for (int i = 0; i < middle; i++)  //Dividing the unsorted list
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
                    if (left.First() <= right.First())  //Comparing First two elements to see which is smaller
                    {

                        ChangeColorToGreen(leftIndexes.First());
                        ChangeColorToGreen(rightIndexes.First());
                        Thread.Sleep(100);

                        result.Add(left.First());
                        left.Remove(left.First());      //Rest of the list minus the first element

                        indexResult.Add(leftIndexes.First());
                        leftIndexes.Remove(leftIndexes.First());

                        ChangeColorToBlack(indexResult.Last());
                        ChangeColorToBlack(rightIndexes.First());
                        Thread.Sleep(100);

                    }
                    else
                    {
                        ChangeColorToRed(leftIndexes.First());
                        ChangeColorToRed(rightIndexes.First());
                        Thread.Sleep(100);

                        result.Add(right.First());
                        right.Remove(right.First());

                        indexResult.Add(rightIndexes.First());
                        rightIndexes.Remove(rightIndexes.First());

                        ChangeColorToBlack(leftIndexes.First());
                        ChangeColorToBlack(indexResult.Last());
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
                setSeriesElement(indexResult[i],result[i]);
            }
            Thread.Sleep(200);
            return result;
        }


        //Quicksort functions
        private void QuickSortBTN_Click(object sender, RoutedEventArgs e)
        {
            int[] ChartValueArray = new int[SizeOfChartArray];
            for (int i = 0; i < SizeOfChartArray; i++)
            {
                ChartValueArray[i] = getSeriesElement(i);
            }
            new Thread(() =>
            {
                Quick_Sort(ChartValueArray, 0, SizeOfChartArray - 1);
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
            ChangeColorToBlue(left);

            Thread.Sleep(200);

            int pivot = arr[left];

            Thread.Sleep(200);

            ChangeColorToBlack(left);



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
                        ChangeColorToRed(left);
                        ChangeColorToRed(right);

                        Thread.Sleep(200);

                        return right;
                    }

                    ChangeColorToGreen(left);
                    ChangeColorToGreen(right);

                    Thread.Sleep(200);

                    int temp = arr[left];

                    setSeriesElement(left, arr[right]);
                    setSeriesElement(right, temp);

                    arr[left] = arr[right];
                    arr[right] = temp;

                    Thread.Sleep(200);

                    ChangeColorToBlack(left);
                    ChangeColorToBlack(right);
                }
                else
                {
                    return right;
                }
            }
        }

        //Heapsort


        //Bubblesort functions
        private void BubbleSortBTN_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                for (int i = 0; i < SizeOfChartArray; i++)
                {
                    for (int j = 0; j < SizeOfChartArray - i - 1; j++)
                    {
                        //Highlight elements
                        int a = Convert.ToInt32(LiveChartValueSeries[j].Values[0]);
                        int b = Convert.ToInt32(LiveChartValueSeries[j + 1].Values[0]);

                        Dispatcher.Invoke(new Action(() =>
                        {
                            //ChangeColorToBlue((ColumnSeries)LiveChartValueSeries[j]);
                            //ChangeColorToBlue((ColumnSeries)LiveChartValueSeries[j+1]);
                        }));
                        Thread.Sleep(100);

                        //Compare
                        if (a > b)
                        {
                            //Highlight pass
                            LiveChartValueSeries[j].Values.Clear();
                            LiveChartValueSeries[j + 1].Values.Clear();

                            LiveChartValueSeries[j].Values.Add(b);
                            LiveChartValueSeries[j + 1].Values.Add(a);

                            Dispatcher.Invoke(new Action(() =>
                            {
                                ChangeColorToGreen(j);
                                ChangeColorToGreen(j + 1);
                            }));
                            Thread.Sleep(100);
                        }
                        else
                        {
                            //Highlight fail
                            Dispatcher.Invoke(new Action(() =>
                            {
                                ChangeColorToRed(j);
                                ChangeColorToRed(j + 1);
                            }));
                            Thread.Sleep(100);
                        }

                        //Remove highlight
                        Dispatcher.Invoke(new Action(() =>
                        {
                            ChangeColorToBlack(j);
                            ChangeColorToBlack(j + 1);
                        }));
                        Thread.Sleep(100);
                    }
                }
            }).Start();
        }



    }
}