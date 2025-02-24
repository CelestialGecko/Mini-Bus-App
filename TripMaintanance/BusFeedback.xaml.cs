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
    /// Interaction logic for BusFeedback.xaml
    /// </summary>
    public partial class BusFeedback : Window
    {
        public BusFeedback()
        {
            InitializeComponent();
        }

        private int NewID()
        {
            int NewCommentID = 0;
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            string queryString = "SELECT MAX(CommentID) + 1 AS CommentID " + "FROM tblBusComment";
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
                        string CommentID = reader[0].ToString();
                        NewCommentID = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return NewCommentID;
        }

        private void btnCreate(object sender, RoutedEventArgs e)
        {
            var ComID = NewID();
            var Busnam = txtBusName.Text;
            var Buscom = txtBusComment.Text;
            using (AppDBcontext DB = new AppDBcontext())
            {
                BusComments busComments = new BusComments()
                {
                    CommentID = ComID,
                    BusName = Busnam,
                    BusComment = Buscom
                };
                DB.tblBusComments.Add(busComments);
                DB.SaveChanges();
                MessageBox.Show("Comment has been successfully saved", "Comments form", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            try
            {

            }
            catch (Exception)
            {
                MessageBox.Show("Data has not been saved", "Comment form", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
