using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace SortingAlgorythms
{
    /// ctrl+k ctrl+f
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Livechart
            InitializeLiveChart();
        }

        //-----------LiveChart START-----------
        public SeriesCollection LiveChartValueSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        private void InitializeLiveChart()
        {
            LiveChartValueSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int>
                    {
                        1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20
                    },
                    Fill = Brushes.Black
                }
            };
            Labels = new[] { "Label1", "Label2" };
            Formatter = value => value.ToString("N");
            DataContext = this;
        }
        //-----------LiveChart END-----------


        //-----------ETC START-----------
        private int[] randomNumbers = new int[20];
        //-----------ETC END-----------

        //Generates random array with unique values
        private void UniqueValuesBTN_Click(object sender, RoutedEventArgs e)
        {
            //Thread UniqueValuesThread = new Thread(AddUniqueValesToChart);
            //UniqueValuesThread.Start();
            new Thread(() =>
            {
                //Button disable
                Dispatcher.BeginInvoke(new Action(() => { UniqueValuesBTN.IsEnabled = false; }));
                
                // Clear Chart values
                LiveChartValueSeries[0].Values.Clear();

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
                    LiveChartValueSeries[0].Values.Add(Numbers[i]);
                }
                //Button enable
                Dispatcher.BeginInvoke(new Action(() => { UniqueValuesBTN.IsEnabled = true; }));
            }).Start();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }


    }
}