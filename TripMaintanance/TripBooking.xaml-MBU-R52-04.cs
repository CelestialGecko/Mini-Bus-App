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
using System.Globalization;

namespace TripMaintanance
{
    /// <summary>
    /// Interaction logic for TripBooking.xaml
    /// </summary>
    
    public partial class TripBooking : Window
    {

        public DateTime NewTripTime;
        public DateTime? NewTripDate;
        
        
        public TripBooking()
        {
            InitializeComponent();
        }

        private void TimeChanged(object sender, SelectionChangedEventArgs e)
        {
            string TripTime;
            ComboBox comboBox = (ComboBox)sender;
            object selectedItem = comboBox.SelectedItem;
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)selectedItem;
                TripTime = Convert.ToString(comboBoxItem.Content);
            }
            if (TripTime == "09:15")
            {
                NewTripTime = DateTime.ParseExact("09:15", "HH:mm", CultureInfo.InvariantCulture);
            }
            else if(TripTime == "11:15")
            {
                NewTripTime = DateTime.ParseExact("11:15", "HH:mm", CultureInfo.InvariantCulture);
            }
            else
            {
                NewTripTime = DateTime.ParseExact("13:15", "HH:mm", CultureInfo.InvariantCulture);
            }
        }

        private int NewID()
        {
            int NewBookingNumbers = 0;
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            string queryString = "SELECT MAX(BookingNumber) + 1 AS BookingNumber " + "FROM tblBooking";
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0]);
                        string NewBookingNumber = reader[0].ToString();
                        NewBookingNumbers = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return NewBookingNumbers;
        }

        private void btnCreate(object sender, RoutedEventArgs e)
        {
            try
            {
                var BookNumID = NewID();
                var ClasID = Convert.ToInt32(txtClassID.Text);
                var BussName = txtBusName.Text;
                var TripTim = NewTripTime;
                var TripDat = Convert.ToDateTime(NewTripDate);

                string Day = Convert.ToString(TripDat).Replace("/", "-");
                Day = Day.Substring(6, 4) + Day.Substring(2, 4) + Day.Substring(0, 2) + Day.Substring(10, 9);

                bool Taken = false;
                string _db = Root.Path;
                string cs = string.Format("Data source={0}", _db);
                string queryString = "SELECT BusName, BookingNumber FROM tblBooking WHERE BusName = 'YT14' AND DateOfTrip = '" + Day + "' AND TimeOfTrip = '" + TripTim + "' AND Approved = 1";
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    SqliteCommand command = new SqliteCommand(queryString, connection);
                    connection.Open();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                }

                using (AppDBcontext DB = new AppDBcontext())
                {
                    Booking booking = new Booking()
                    {
                        BookingNumber = BookNumID,
                        ClassID = ClasID,
                        BusName = BussName,
                        TimeOfTrip = TripTim,
                        DateOfTrip = TripDat,
                        Approved = false
                    };
                    DB.tblBooking.Add(booking);
                    DB.SaveChanges();
                    MessageBox.Show("Booking has been successfully saved", "Booking form", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Data has not been saved", "Booking form", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //NewTripDate = DatePicked.DisplayDate;
            NewTripDate = DatePicked.SelectedDate;
        }
    }
}
