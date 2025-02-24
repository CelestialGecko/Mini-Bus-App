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
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MathNet.Numerics.Statistics;

namespace TripMaintanance
{
    /// <summary>
    /// Interaction logic for TripStats.xaml
    /// </summary>
    public partial class TripStats : Window
    {
        //An array that stores the bus names and bus count to be displayed on a pie chart
        public string[] BusNames = new string[12];
        public int[] BusCount = new int[12];
        //defines 2 series collections that are used to display the information on the 2 graphs
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollectionBar { get; set; }
        //An array that stores the content for the labels on the bar chart and a formatter for the bar chart to format the values on the y axis
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        //stores the class names and the number of times a class appears in the bookings area, the array only holds the top 6 classes
        public string[] ClassName = new string[6];
        public int[] ClassCount = new int[6];

        public TripStats()
        {
            InitializeComponent();
            GetBookingsData();
            InitializeBarChart();
            InitializeDonutChart();
            //sets DataContext to the current instance of the class
            DataContext = this;
            //caculates and displays the confidence interval
            ConfidenceInterval();
            TopBus.Content += " " + BusNames[0];
            TopClass.Content += " " + ClassName[0];
        }

        private void GetBookingsData()
        {
            int i = 0;
            string _db = Root.Path;
            string cs = $"Data source={_db}";
            //Uses a query that counts the number of times a class appears and then orders them by desc to get the class that appears the most
            string classQuery = "SELECT t2.ClassName, COUNT(t1.ClassID) AS count FROM tblBooking AS t1 JOIN tblClass AS t2 ON t1.ClassID = t2.ClassID GROUP by t2.ClassID ORDER by count DESC LIMIT 6";
            //counts all the busses that appear in bookings and orders them by desc
            string busQuery = "SELECT BusName, COUNT(*) AS count FROM tblBooking GROUP by BusName ORDER by count DESC";

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                try
                {
                    connection.Open();

                    // runs class query
                    SqliteCommand classCommand = new SqliteCommand(classQuery, connection);
                    SqliteDataReader classReader = classCommand.ExecuteReader();
                    while (classReader.Read())
                    {
                        ClassName[i] = Convert.ToString(classReader["ClassName"]);
                        ClassCount[i] = Convert.ToInt32(classReader["count"]);
                        i++;
                    }
                    classReader.Close();
                    i = 0;
                    // runs bus query
                    SqliteCommand busCommand = new SqliteCommand(busQuery, connection);
                    SqliteDataReader busReader = busCommand.ExecuteReader();
                    while (busReader.Read())
                    {
                        BusNames[i] = Convert.ToString(busReader["BusName"]);
                        BusCount[i] = Convert.ToInt32(busReader["count"]);
                        i++;
                    }
                    busReader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void InitializeBarChart()
        {
            //adds the data to the class bar chart
            SeriesCollectionBar = new SeriesCollection { new ColumnSeries { Title = ClassName[0], Values = new ChartValues<double> { ClassCount[0] } } };
            for (int j = 1; j < 6; j++)
            {
                SeriesCollectionBar.Add(new ColumnSeries { Title = ClassName[j], Values = new ChartValues<double> { ClassCount[j] } });
            }
            //formats the information for the bar chart
            Labels = new[] { " " };
            Formatter = value => value.ToString("N");
        }
        private void InitializeDonutChart()
        {
            //adds the data to the bus donut chart
            SeriesCollection = new SeriesCollection { new PieSeries { Title = BusNames[0], Values = new ChartValues<ObservableValue> { new ObservableValue(BusCount[0]) }, DataLabels = true } };
            for (int j = 1; j < BusNames.Length; j++)
            {
                SeriesCollection.Add(new PieSeries { Title = BusNames[j], Values = new ChartValues<ObservableValue> { new ObservableValue(BusCount[j]) }, DataLabels = true });
            }
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            //re opens the analytics menu
            AnalyticsMenu analytics = new AnalyticsMenu();
            analytics.Show();
            Close();
        }

        private void ConfidenceInterval()
        {
            //runs each step for calculating the interval and creates and report with it
            int sum = Sum(BusCount);
            double mean = CalculateMean(BusCount, sum); ;
            double SD = CalculateSD(BusCount, mean); ;
            double P = Convert.ToDouble(Ptxt.Text);
            double Tvalue = GetStudentT(P, BusCount.Length - 1);
            GetConfidenceInterval(BusCount, mean, SD, Tvalue);
        }

        private static double GetStudentT(double P, double V)
        {
            //returns the T value that used to calculate the interval
            return MathNet.Numerics.Distributions.StudentT.InvCDF(0, 1, V, P);
        }

        private static int Sum(int[] BusData)
        {
            //calculates the sum of bookings
            int sum = 0;
            foreach (var data in BusData)
            {
                sum += data;
            }
            return sum;
        }

        private static double CalculateMean(int[] BusData, int sum)
        {
            //calculates the mean
            return sum / BusData.Length;
        }

        private static double CalculateSD(int[] BusData, double mean)
        {
            //caluclates the standard deviation SX
            double SD = 0;
            foreach (var data in BusData)
            {
                SD += Math.Pow(data - mean, 2);
            }
            SD = Math.Sqrt(SD / (BusData.Length - 1));
            return SD;
        }

        private void GetConfidenceInterval(int[] BusData, double mean, double SD, double T)
        {
            //puts the mean, SD and T value in a formula to ge the 2 intervals
            double Lower;
            double Upper;
            int Count = 0;
            int i = 0;
            Lower = Math.Round(mean - T * (SD / Math.Sqrt(BusData.Length)));
            Upper = Math.Round(mean + T * (SD / Math.Sqrt(BusData.Length)));
            Intervals.Content = $"({Lower}, {Upper})";
            Advice.Content = "";
            //creates advice for busses outside this region
            foreach (int value in BusData)
            {
                if (value < Lower)
                {
                    Advice.Content += $"{BusNames[i]} may need to be checked as it{Environment.NewLine}is currently not being used enough{Environment.NewLine}";
                    Count++;
                }
                else if(value > Upper)
                {
                    Advice.Content += $"{BusNames[i]} is being used significantly{Environment.NewLine}more than the other busses{Environment.NewLine}";
                    Count++;
                }
                i++;
            }
            if (Count == 0)
            {
                Advice.Content = "Busses all seem fine";
            }
        }

        private void btnCal(object sender, RoutedEventArgs e)
        {
            ConfidenceInterval();
        }
    }
}
