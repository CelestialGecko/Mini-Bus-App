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

        public string[] BusQueue;
        public int[] BusPositionQueue;
        private int Front = 1;
        private int Rear = default;
        public int CurrentBus;

        public TripBooking()
        {
            InitializeComponent();
            CreateQueue();
        }

        public void RemoveFromQueue()
        {
            if (Rear != Front - 1)
            {
                CurrentBus = BusPositionQueue[Front - 1];
                for (int counter = 0; counter < Rear; counter++)
                {
                    if (counter == Rear - 1)
                    {
                        BusPositionQueue[counter] = default;//sets the rear pointer to null when counter = 4
                    }
                    else
                    {
                        BusPositionQueue[counter] = BusPositionQueue[counter + 1];//moves the items in the queue to a slot in front
                    }
                }
            }
        }
        
        public string CreateQuery()
        {
            //initialize the Query string with the update statement and case statement
            string Query = "UPDATE tblBus SET Position = CASE";
            //loop through the queue and add each bus name and position to the query string
            for (int i = 0; i < Rear; i++)
            {
                //add the bus name and its corresponding position to the query string
                Query += $" WHEN BusName = '{BusQueue[i]}' THEN {BusPositionQueue[i]}";
            }
            //close the case statement and return the completed query string
            Query += " END";
            return Query;
        }

        public void AddQueue()
        {
            //adds current bus to the rear of the queue
            BusPositionQueue[Rear - 1] = CurrentBus;
        }

        public void SaveQueue()
        {
            // construct connection string to SQLite database
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            // get query string to update bus positions in the database
            string QueryString = CreateQuery();
            // open connection to database and execute query
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(QueryString, connection))
                {
                    try
                    {
                        // execute query and get number of rows updated
                        int RowsUpdated = command.ExecuteNonQuery();

                        // show message box indicating failure of update
                        if (!(RowsUpdated > 0))
                        {
                            MessageBox.Show("Update failed.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // show error message box if query execution throws an exception
                        MessageBox.Show(Convert.ToString(ex));
                    }
                }
            }
        }

        public void CreateQueue()
        {
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            // Query to get the number of available buses
            string countQueryString = "SELECT count(*) AS RecordCount FROM tblBus WHERE Available = 1";
            // Connects to database and executes query to get number of available buses
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                SqliteCommand countCommand = new SqliteCommand(countQueryString, connection);
                try
                {
                    connection.Open();
                    // Gets the number of available buses
                    Rear = Convert.ToInt32(countCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
                // Initializes arrays for bus names and positions
                BusQueue = new string[Rear];
                BusPositionQueue = new int[Rear];
            }
            // Query to get the bus names and positions of available buses
            string busQueryString = "SELECT BusName, Position FROM tblBus WHERE Available = 1";
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                int i = 0;
                SqliteCommand busCommand = new SqliteCommand(busQueryString, connection);
                try
                {
                    connection.Open();
                    SqliteDataReader reader = busCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        // Populates the bus queue and position queue arrays
                        BusQueue[i] = Convert.ToString(reader["BusName"]);
                        BusPositionQueue[i] = Convert.ToInt32(reader["Position"]);
                        i++;
                    }
                    reader.Close();
                    // Displays advice on which bus to use first
                    Advisetxt.Content = "I would advise using bus " + BusQueue[BusPositionQueue[0] - 1];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
            }
        }

        private void TimeChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected item in the combo box
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            // Extract the time from the selected item and convert to DateTime
            string tripTime = selectedItem.Content.ToString();
            NewTripTime = DateTime.ParseExact(tripTime, "HH:mm", CultureInfo.InvariantCulture);
        }

        private int NewID()
        {
            //gets the db path and sets the query
            int NewBookingNumbers = 0;
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            string queryString = "SELECT MAX(BookingNumber) + 1 AS BookingNumber FROM tblBooking";
            //connects to data base
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                //executes the query
                SqliteCommand command = new SqliteCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //writes the new ID to a variable
                        string NewBookingNumber = reader[0].ToString();
                        NewBookingNumbers = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
            }
            //returns the new ID
            return NewBookingNumbers;
        }

        private void btnCreate(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get values from the text boxes
                var BookNumID = NewID();
                var ClasID = Convert.ToInt32(txtClassID.Text);
                var BussName = txtBusName.Text;
                var TripTim = NewTripTime;
                var TripDat = Convert.ToDateTime(NewTripDate);
                // Format date and time for the SQL query
                string Day = TripDat.ToString("yyyy-MM-dd");
                string Time = TripTim.ToString("HH:mm");
                // Set default values
                bool Taken = false;
                string TakenBy = default;
                // Get path and connection string for the query
                string _db = Root.Path;
                string cs = string.Format("Data source={0}", _db);
                string queryString = "SELECT ClassName FROM tblBooking, tblClass WHERE BusName = @BussName AND tblClass.ClassID = tblBooking.ClassID AND strftime('%Y-%m-%d', DateOfTrip) = @Day AND strftime('%H:%M', TimeOfTrip) = @Time AND Approved = 1";
                // Connect to the database
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    // Execute the query on the database
                    SqliteCommand command = new SqliteCommand(queryString, connection);
                    command.Parameters.AddWithValue("@BussName", BussName);
                    command.Parameters.AddWithValue("@Day", Day);
                    command.Parameters.AddWithValue("@Time", Time);
                    connection.Open();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // If there is a record that is with the same conditions as the booking then the bus is taken
                        Taken = true;
                        TakenBy = Convert.ToString(reader["ClassName"]);
                    }
                }
                if (Taken == true)
                {
                    // Display an error that tells the teacher the class that has booked this slot
                    MessageBox.Show(TakenBy + " class has already booked this bus", "Booking form", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // Add the information from the transaction form to the table if the slot is available
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
                        if(BussName == BusQueue[BusPositionQueue[0] - 1])
                        {
                            RemoveFromQueue();
                            AddQueue();
                            SaveQueue();
                        }
                        MessageBox.Show("Booking has been successfully saved", "Booking form", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // If an unexpected error occurs while saving record
                MessageBox.Show(Convert.ToString(ex), "Booking form", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //sets the date of the trip
            NewTripDate = DatePicked.SelectedDate;
        }
    }
}
