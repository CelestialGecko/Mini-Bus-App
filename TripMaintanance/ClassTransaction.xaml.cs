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

namespace TripMaintanance
{
    /// <summary>
    /// Interaction logic for ClassTransaction.xaml
    /// </summary>
    public partial class ClassTransaction : Window
    {
        private char ComboContent;

        public ClassTransaction()
        {
            InitializeComponent();
        }

        private void btnCreate(object sender, RoutedEventArgs e)
        {
            try
            {
                var ClasID = NewID();
                var TeachID = Convert.ToInt32(txtTeacherID.Text);
                var ClasName = txtClassName.Text;
                var ClasType = ComboContent;
                using (AppDBcontext DB = new AppDBcontext())
                {
                    Class classes = new Class()
                    {
                        ClassID = ClasID,
                        TeacherID = TeachID,
                        ClassName = ClasName,
                        ClassType = ClasType,
                    };
                    DB.tblClass.Add(classes);
                    DB.SaveChanges();
                    MessageBox.Show("Class has been successfully saved", "Class transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainMenu mainMenuOpen = new MainMenu();
                    mainMenuOpen.Show();
                    Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Data has not been saved", "Class transaction", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int NewID()
        {
            int NewClassIDs = 0;
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            string queryString = "SELECT MAX(ClassID) + 1 AS ClassID " + "FROM tblClass";
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
                        string NewClassID = reader[0].ToString();
                        NewClassIDs = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return NewClassIDs;
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SideOfYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            object selectedItem = comboBox.SelectedItem;
            if(selectedItem != null)
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)selectedItem;
                ComboContent = Convert.ToChar(comboBoxItem.Content);
            }
        }
    }
}
