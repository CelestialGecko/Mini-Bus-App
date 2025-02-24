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
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace TripMaintanance
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>

    public partial class MainMenu : Window
    {
        public string CurrentTeacher { get; set; }

        public string CurrentID { get; set; }

        public char CurrentTeacherType { get; set; }

        public MainMenu()
        {
            InitializeComponent();
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AnalyticsButton(object sender, RoutedEventArgs e)
        {
            AnalyticsMenu analyticsMenu = new AnalyticsMenu();
            analyticsMenu.ShowDialog();
        }

        private void CreateButton(object sender, RoutedEventArgs e)
        {
            ClassTransaction classTransactionOpen = new ClassTransaction();
            classTransactionOpen.ShowDialog();
        }

        private void BookButton(object sender, RoutedEventArgs e)
        {
            TripBooking tripBooking = new TripBooking();
            tripBooking.ShowDialog();
        }

        private void MaintainButton(object sender, RoutedEventArgs e)
        {
            var classMaintain = new ClassMaintain();
            classMaintain.CurrentTeacherType = CurrentTeacherType;
            classMaintain.ShowDialog();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Label1.Content = $"{CurrentTeacher} {CurrentID}";
            if (CurrentTeacherType == 'H')
            {
                Label1.Content += " HR";
                SetButtonVisibility(AnaButton, AnaIcon, Visibility.Visible, true);
                SetButtonVisibility(BookingsButton, BookingsIcon, Visibility.Collapsed, false);
            }
            DateTime today = DateTime.Now.Date;
            // Determine the century doomsday for the 21st century (2000s)
            int centuryDoomsday = 3;
            //gets the current date in the right format
            DateTime TodaysDate = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);
            //determines the doomsday of the year
            int year = TodaysDate.Year;
            int yearDoomsday;
            if (year % 4 == 0)
            {
                yearDoomsday = 4; // The doomsday for leap years is always the 4th of July
            }
            else
            {
                yearDoomsday = (year / 12 + year % 12 + (year % 12) / 4) % 7;
            }
            // Calculate the difference between the target date and the year's doomsday
            int difference = (TodaysDate.Day - yearDoomsday) % 7;
            if (difference < 0)
            {
                difference += 7;
            }
            // Find the day of the week
            int dayOfWeek = (centuryDoomsday + difference) % 7;
            string[] daysOfWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            string dayOfWeekName = daysOfWeek[dayOfWeek];
            //Works out monday and friday
            int Sub1 = SubtractMon(dayOfWeekName);
            int Sub2 = SubtractFri(dayOfWeekName);
            //subtracts the nessisary days to get the numerical values for friday and monday
            DateTime Monday = TodaysDate.AddDays(Sub1);
            DateTime Friday = TodaysDate.AddDays(Sub2);
            //formats the string for Monday and friday to be compatible with the sql query
            string Mondays = Convert.ToString(Monday).Replace("/", "-");
            string Fridays = Convert.ToString(Friday).Replace("/", "-");
            Mondays = Mondays.Substring(6, 4) + Mondays.Substring(2, 4) + Mondays.Substring(0, 2) + Mondays.Substring(10, 9);
            Fridays = Fridays.Substring(6, 4) + Fridays.Substring(2, 4) + Fridays.Substring(0, 2) + Fridays.Substring(10, 9);
            //creates lists to store the values gathered from the query
            List<int> classIDs = new List<int>();
            List<string> classNames = new List<string>();
            List<string> busNames = new List<string>();
            List<DateTime> dateOfTrips = new List<DateTime>();
            List<DateTime> timeOfTrips = new List<DateTime>();
            //gets the db root and formats the SQL query
            string _db = Root.Path;
            string cs = $"Data source={_db}";
            string queryString = $"SELECT ClassName, BusName, DateOfTrip, TimeOfTrip FROM tblBooking, tblClass WHERE DateOfTrip >= '{Mondays}' AND DateOfTrip <= '{Fridays}' AND tblClass.ClassID = tblBooking.ClassID AND Approved = 1";
            //exercutes the query on a different method
            ExecuteSqlQuery(cs, queryString, reader =>
            {
                classNames.Add(reader["ClassName"].ToString());
                busNames.Add(reader["BusName"].ToString());
                dateOfTrips.Add(Convert.ToDateTime(reader["DateOfTrip"]));
                timeOfTrips.Add(Convert.ToDateTime(reader["TimeOfTrip"]));
            });
            //creates an array that stores the 3 different times that trips can occur
            DateTime[] tripTimes = { new DateTime(today.Year, today.Month, today.Day, 9, 15, 0), new DateTime(today.Year, today.Month, today.Day, 11, 15, 0), new DateTime(today.Year, today.Month, today.Day, 13, 15, 0) };
            //creates an 2D array for all the label objects
            Label[,] labels = {
    { C1R1, C2R1, C3R1 },
    { C1R2, C2R2, C3R2 },
    { C1R3, C2R3, C3R3 },
    { C1R4, C2R4, C3R4 },
    { C1R5, C2R5, C3R5 }
};
            //loops through each row of the two-dimensional labels array
            for (int row = 0; row < labels.GetLength(0); row++)
            {
                //loops through each row of the two-dimensional labels array
                for (int col = 0; col < labels.GetLength(1); col++)
                {
                    //get the label at the current row and column
                    Label currentLabel = labels[row, col];
                    // Clear the content and tooltip of the current label
                    currentLabel.Content = "";
                    currentLabel.ToolTip = default;
                    //loops through every class name
                    for (int i = 0; i < classNames.Count; i++)
                    {
                        DateTime currentDay = Monday.AddDays(row);
                        //checks if the current day matches the day of the trip
                        if (currentDay.Day == dateOfTrips[i].Day)
                        {
                            DateTime currentTime = tripTimes[col];
                            //checks if the current time matches the time of the trip
                            if (currentTime.Hour == timeOfTrips[i].Hour && currentTime.Minute == timeOfTrips[i].Minute)
                            {
                                currentLabel.Content += $"{classNames[i]} {busNames[i]}{Environment.NewLine}";
                                //runs a function to get the students in the current class and adds them to the tool tip
                                List<string> students = GetStudentsFromClass(cs, classNames[i]);
                                currentLabel.ToolTip += $"Students for {classNames[i]}:{Environment.NewLine}";
                                currentLabel.ToolTip += string.Join(Environment.NewLine, students) + Environment.NewLine;
                            }
                        }
                    }
                }
            }
        }
        //makes the button and icon visable depending on what value is passed as isEnabled
        private void SetButtonVisibility(Button button, UIElement icon, Visibility visibility, bool isEnabled)
        {
            button.Visibility = icon.Visibility = visibility;
            button.IsEnabled = icon.IsEnabled = isEnabled;
        }
        //exercutes an sql query to get the information used to create the time table
        private void ExecuteSqlQuery(string connectionString, string query, Action<SqliteDataReader> processResult)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                try
                {
                    SqliteCommand command = new SqliteCommand(query, connection);
                    connection.Open();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //proccesses the results from the query and adds them to their specified list
                        processResult(reader);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //exercutes an sql query to get all the student names for a given class, this returns the students list
        private List<string> GetStudentsFromClass(string connectionString, string className)
        {
            List<string> students = new List<string>();
            string query = $"SELECT StudentFirstName, StudentSecondName FROM tblStudent, tblClass WHERE tblClass.ClassID = tblStudent.ClassID AND ClassName = '{className}'";
            ExecuteSqlQuery(connectionString, query, reader =>
            {
                students.Add($"{reader["StudentFirstName"]} {reader["StudentSecondName"]}");
            });
            return students;
        }

        private void FeedbackButton(object sender, RoutedEventArgs e)
        {
            BusFeedback busFeedback = new BusFeedback();
            busFeedback.ShowDialog();
        }
        //returns the day for monday
        private static int SubtractMon(string day)
        {
            switch (day)
            {
                case "Sunday": return 1;
                case "Monday": return 0;
                case "Tuesday": return -1;
                case "Wednesday": return -2;
                case "Thursday": return -3;
                case "Friday": return -4;
                case "Saturday": return -5;
                default: return 0;
            }
        }
        //returns the day for friday
        private static int SubtractFri(string day)
        {
            switch (day)
            {
                case "Sunday": return 5;
                case "Monday": return 4;
                case "Tuesday": return 3;
                case "Wednesday": return 2;
                case "Thursday": return 1;
                case "Friday": return 0;
                case "Saturday": return -1;
                default: return 0;
            }
        }

        private void BtnBookingsButton(object sender, RoutedEventArgs e)
        {
            var teacherBookings = new TeacherBookings();
            teacherBookings.CurrentIDs = CurrentID;
            teacherBookings.ShowDialog();
        }
    }
}
