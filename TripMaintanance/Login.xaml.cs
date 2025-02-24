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
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.IO;

namespace TripMaintanance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        //uses a defualt of 3 attempts
        private int attemptsLeft = 3;

        private void TryLogin(string username, string password)
        {
            //checks if the user has run out of attempts
            MessageBox.Show(Root.Path);
            if (attemptsLeft > 0)
            {
                try
                {
                    using (AppDBcontext context = new AppDBcontext())
                    {
                        bool userFound = context.tblTeacher.Any(teacher => teacher.Username == username && teacher.Password == password);
                        if (userFound)
                        {
                            //if the user logs in correctly
                            MessageBox.Show("You have been logged in successfully", "Login complete", MessageBoxButton.OK, MessageBoxImage.Information);
                            OpenMainMenu(username, password);
                        }
                        else
                        {
                            //reduces the attempt count by 1 and displays an error message depending on attempt count
                            attemptsLeft--;
                            if (attemptsLeft > 0)
                            {
                                MessageBox.Show($"Invalid username or password. You have {attemptsLeft} attempts left.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                MessageBox.Show("You have exceeded the maximum number of login attempts. Please contact HR for assistance.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("You have exceeded the maximum number of login attempts. Please contact HR for assistance.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //used to open the main menu and get information about the teacher who has just logged in
        private void OpenMainMenu(string Username, string Password)
        {
            char TeacherType = default;
            string currentID = "NA";
            string CurrentTeacher = "Unnamed";
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            //formats the query to get the full name, id and type of a teacher
            string queryString = "SELECT TeacherFirstName, TeacherSecondName, TeacherID, TeacherType FROM tblTeacher WHERE Username = @username AND Password = @password";
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                //uses parameterized query
                command.Parameters.AddWithValue("@username", Username);
                command.Parameters.AddWithValue("@password", Password);
                try
                {
                    connection.Open();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        currentID = reader["TeacherID"].ToString();
                        CurrentTeacher = (reader["TeacherFirstName"].ToString()) + " " + (reader["TeacherSecondName"].ToString());
                        TeacherType = Convert.ToChar(reader["TeacherType"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            var MainMenuOpen = new MainMenu();
            MainMenuOpen.CurrentTeacher = CurrentTeacher;
            MainMenuOpen.CurrentID = currentID;
            MainMenuOpen.CurrentTeacherType = TeacherType;
            MainMenuOpen.Show();
            Close();
        }

        private void EnterInput_Click(object sender, RoutedEventArgs e)
        {
            string username = txtInputUsername.Text;
            string password = txtInputPassword.Text;
            //checks if feilds are empty
            if (username == "" || password == "")
            {
                MessageBox.Show("Empty fields. Please enter a username and password.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                TryLogin(username, password);
            }
        }

        private void CloseLogin_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
