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

        //----- LVC START -----
        public SeriesCollection LiveChartValueSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        //*----- LVC END -----
        public MainWindow()
        {
            InitializeComponent();
            //----- LVC START-----
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
            //----- LVC END -----
        }

        //Generates random array with unique values
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            LiveChartValueSeries[0].Values.Clear();

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

            // Now your array is randomized and you can simply print them in order
            for (int i = 0;i < Numbers.Length;++i)
            {
                LiveChartValueSeries[0].Values.Add(Numbers[i]);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

        }

    }
}