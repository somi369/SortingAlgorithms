using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;

namespace SortingAlgorythms
{
    public partial class MainWindow : Window
    {

        public SeriesCollection LiveChartValueSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }

        int SizeOfArray;

        public MainWindow()
        {
            InitializeComponent();
            //Livechart
            InitializeLiveChart();
            SizeOfArray = 20;
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

        private void UniqueValuesBTN_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                //Button disable
                Dispatcher.BeginInvoke(new Action(() => { UniqueValuesBTN.IsEnabled = false; }));

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
                //Button enable
                Dispatcher.BeginInvoke(new Action(() => { UniqueValuesBTN.IsEnabled = true; }));
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
                                ChangeColorToGreen((ColumnSeries)LiveChartValueSeries[i]);
                                ChangeColorToGreen((ColumnSeries)LiveChartValueSeries[j]);
                            }));
                            Thread.Sleep(100);
                        }
                        else
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                ChangeColorToRed((ColumnSeries)LiveChartValueSeries[i]);
                                ChangeColorToRed((ColumnSeries)LiveChartValueSeries[j]);
                            }));
                            Thread.Sleep(100);
                        }
                        Dispatcher.Invoke(new Action(() =>
                        {
                            ChangeColorToBlack((ColumnSeries)LiveChartValueSeries[i]);
                            ChangeColorToBlack((ColumnSeries)LiveChartValueSeries[j]);
                        }));
                        Thread.Sleep(100);
                    }
                }
            }).Start();
        }

        //MergeSort

        private void MergeSortBTN_Click(object sender, RoutedEventArgs e)
        {
            int[] ArrayOfValues = new int[SizeOfArray];

            for (int i = 0; i < SizeOfArray; i++)
            {
                ArrayOfValues[i] = (int)LiveChartValueSeries[i].Values[0];
            }
            mergeSort(ArrayOfValues);
        }
        public static int[] mergeSort(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];
            //As this is a recursive algorithm, we need to have a base case to 
            //avoid an infinite recursion and therfore a stackoverflow
            if (array.Length <= 1)
                return array;
            // The exact midpoint of our array  
            int midPoint = array.Length / 2;
            //Will represent our 'left' array
            left = new int[midPoint];

            //if array has an even number of elements, the left and right array will have the same number of 
            //elements
            if (array.Length % 2 == 0)
                right = new int[midPoint];
            //if array has an odd number of elements, the right array will have one more element than left
            else
                right = new int[midPoint + 1];
            //populate left array
            for (int i = 0; i < midPoint; i++)
                left[i] = array[i];
            //populate right array   
            int x = 0;
            //We start our index from the midpoint, as we have already populated the left array from 0 to midpont
            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }
            //Recursively sort the left array
            left = mergeSort(left);
            //Recursively sort the right array
            right = mergeSort(right);
            //Merge our two sorted arrays
            result = merge(left, right);
            return result;
        }

        //This method will be responsible for combining our two sorted arrays into one giant array
        public static int[] merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
            //
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            //while either array still has an element
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                //if both arrays have elements  
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    //If item on left array is less than item on right array, add that item to the result array 
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    // else the item in the right array wll be added to the results array
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                //if only the left array still has elements, add all its items to the results array
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                //if only the right array still has elements, add all its items to the results array
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }


        //Quicksort

        //Heapsort


        //Bubble


        private void ChangeColorToRed(ColumnSeries x)
        {

            x.Fill = Brushes.Red;
        }
        private void ChangeColorToGreen(ColumnSeries x)
        {
            x.Fill = Brushes.LightGreen;
        }
        private void ChangeColorToBlack(ColumnSeries x)
        {
            x.Fill = Brushes.Black;
        }
        private void ChangeColorToBlue(ColumnSeries x)
        {
            x.Fill = Brushes.Blue;
        }

        private void BubbleSortBTN_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                for (int i = 0; i < SizeOfArray; i++)
                {
                    for (int j = 0; j < SizeOfArray - i-1; j++)
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
                            LiveChartValueSeries[j+1].Values.Clear();

                            LiveChartValueSeries[j].Values.Add(b);
                            LiveChartValueSeries[j+1].Values.Add(a);

                            Dispatcher.Invoke(new Action(() =>
                            {
                                ChangeColorToGreen((ColumnSeries)LiveChartValueSeries[j]);
                                ChangeColorToGreen((ColumnSeries)LiveChartValueSeries[j+1]);
                            }));
                            Thread.Sleep(100);
                        }
                        else
                        {
                            //Highlight fail
                            Dispatcher.Invoke(new Action(() =>
                            {
                                ChangeColorToRed((ColumnSeries)LiveChartValueSeries[j]);
                                ChangeColorToRed((ColumnSeries)LiveChartValueSeries[j+1]);
                            }));
                            Thread.Sleep(100);
                        }

                        //Remove highlight
                        Dispatcher.Invoke(new Action(() =>
                        {
                            ChangeColorToBlack((ColumnSeries)LiveChartValueSeries[j]);
                            ChangeColorToBlack((ColumnSeries)LiveChartValueSeries[j+1]);
                        }));
                        Thread.Sleep(100);
                    }
                }
            }).Start();
        }
    }
}