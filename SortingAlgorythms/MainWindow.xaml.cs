using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortingAlgorythms
{
    /// ctrl+k ctrl+f
    /// 
    public partial class MainWindow : Window
    {
        private int[] randomNumbers = new int[20];
        public MainWindow()
        {
            InitializeComponent();
            //----- LVC START-----
            series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(3),
                        new ObservableValue(6),
                        new ObservableValue(7),
                        new ObservableValue(4),
                        new ObservableValue(2)
                    },
                    Fill = Brushes.Black
                }
            };


            Labels = new[] { "Label1", "Label2" };
            Formatter = value => value.ToString("N");
            DataContext = this;
            //----- LVC END -----
        }
        //----- LVC START -----
        public SeriesCollection series { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        //*----- LVC END -----

        private void MergeSortMain()
        {
            //MergeSort(randomNumbers);
        }
        /*
        private int[] MergeSort(int[] array)
        {
            if (array.Length == 1)
            {
                return array;
            }
            //Declare and initialise the new half arrays
            int[] arrayOne;
            int[] arrayTwo;
            if (array.Length % 2 == 0)
            {
                arrayOne = new int[array.Length / 2];
                arrayTwo = new int[array.Length / 2];
            }
            else
            {
                arrayOne = new int[(array.Length / 2) + 1];
                arrayTwo = new int[array.Length / 2];
            }
            for (int i = 0; i < arrayOne.Length; i++)
            {
                arrayOne[i] = array[i];
            }
            for(int i = 0; i < arrayTwo.Length ;i++)
            {
                arrayTwo[i]= array[arrayOne.Length+i];
            }

            arrayOne = MergeSort(arrayOne);
            arrayTwo = MergeSort(arrayTwo);

            arrayOne MergeSort()
            return Merge(arrayOne, arrayTwo);
        }
        */

        private int[] Merge(int[] arrayOne, int[] arrayTwo)
        {
            return arrayOne;
        }
        private void QuickSort()
        {

        }

        private void BubbleSort()
        {

        }

        private void HeapSort()
        {

        }

        private void SwapValues(int a, int b)
        {
            int c = a;
            a = b;
            b = c;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            series[0].Values.Clear();
            Random rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                randomNumbers[i] = rand.Next(1, 20);
                series[0].Values.Add(new ObservableValue(randomNumbers[i]));
            }
            //Array.Sort(randomNumbers);



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Random rand = new Random();
            //int a = rand.Next(0, 20);
            //int b = rand.Next(0, 20);
            //int c = Int16.Parse(series[0].Values[a].ToString());
            //series[0].Values[a] = series[0].Values[b];
            //series[0].Values[b] = c;

            for (int i = 0; i < 20; i++)
            {
                for (int j = i + 1; j < 20; j++)
                {
                    int a = getSeriesElement(i);
                    int b = getSeriesElement(j);
                    if (b < a)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            int c = a;
                            setSeriesElement(i, b);
                            setSeriesElement(j, c);
                            System.Threading.Thread.Sleep(1000);
                        });

                    }
                }

            }

        }
        public int getSeriesElement(int idx)
        {
            int[] seriesNumbers = new int[20];
            series[0].Values.GetEnumerator
            return seriesNumbers[idx];
        }
        public void setSeriesElement(int idx, int value)
        {
            int[] seriesNumbers = new int[20];
            series[0].Values.CopyTo(seriesNumbers, 0);
            seriesNumbers[idx] = value;
            series[0].Values.Clear();
            for (int i = 0; i < 20; i++)
            {
                series[0].Values.Add(new ObservableValue(seriesNumbers[i]));
            }
        }
    }
}