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

namespace TripMaintanance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnterInput_Click(object sender, RoutedEventArgs e)
        {
            var Username1 = txtInputUsername.Text;
            var Password1 = txtInputPassword.Text;
            char TeacherType = default;
            string currentID = "NA";
            string CurrentTeacher = "Unnamed";
            if (Username1 == "" || Password1 == "")
            {
                MessageBox.Show("empty feilds, please type a username and password", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                using (AppDBcontext Context = new AppDBcontext())
                {
                    bool userfound = Context.tblTeacher.Any(Teacher => Teacher.Username == Username1 && Teacher.Password == Password1);
                    if (userfound)
                    {
                        MessageBox.Show("You have been logged in successfully", "Login complete", MessageBoxButton.OK, MessageBoxImage.Information);

                        string _db = Root.Path;
                        string cs = string.Format("Data source={0}", _db);
                        string queryString = "SELECT TeacherFirstName, TeacherSecondName, TeacherID, TeacherType FROM tblTeacher WHERE Username = '" + Username1 + "' AND Password = '" + Password1 + "'";
                        using (SqliteConnection connection = new SqliteConnection(cs))
                        {
                            SqliteCommand command = new SqliteCommand(queryString, connection);
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
                            catch(Exception ex)
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
                    else
                    {
                        MessageBox.Show("Invalid username or password, if you are having account issues then please get in touch with HR", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        //Process.Start("https://www.youtube.com/channel/UCkTDEsKefNriv5w_sSNKRsw");
                    }
                }
            }
        }

        private void CloseLogin_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
