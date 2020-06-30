using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using LiveCharts.Definitions.Series;

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
            LiveChartArray = new int[20];

        }

        //-----------LiveChart START-----------
        public SeriesCollection LiveChartValueSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
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
        //-----------LiveChart END-----------


        //-----------ETC START-----------
        private int[] LiveChartArray;
        //-----------ETC END-----------

        //Generates random array with unique values
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

        private void setLiveChartValueSeries()
        {
            new Thread(() =>
            {
                //LiveChartArray = new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
                ColumnSeries NewColumnSeries = new ColumnSeries { Values = new ChartValues<int>(LiveChartArray) };
                NewColumnSeries.Fill = Brushes.Black;
                LiveChartValueSeries[0] = NewColumnSeries;

            }).Start();
        }

        private void getLiveChartValueSeries()
        {
            new Thread(() =>
            {


            }).Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)

        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            setLiveChartValueSeries();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

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
                            ChangeColorToBlue((ColumnSeries)LiveChartValueSeries[i]);
                            ChangeColorToBlue((ColumnSeries)LiveChartValueSeries[j]);
                        }));
                        Thread.Sleep(200);


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
                            Thread.Sleep(200);                            
                        }
                        else
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                ChangeColorToRed((ColumnSeries)LiveChartValueSeries[i]);
                                ChangeColorToRed((ColumnSeries)LiveChartValueSeries[j]);
                            }));
                            Thread.Sleep(200);
                        }
                        Dispatcher.Invoke(new Action(() =>
                        {
                            ChangeColorToBlack((ColumnSeries)LiveChartValueSeries[i]);
                            ChangeColorToBlack((ColumnSeries)LiveChartValueSeries[j]);
                        }));
                        Thread.Sleep(200);
                    }
                }
            }).Start();
        }

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
    }
}