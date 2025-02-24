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
using System.Windows.Shapes;

namespace TripMaintanance
{
    /// <summary>
    /// Interaction logic for AnalyticsMenu.xaml
    /// </summary>
    public partial class AnalyticsMenu : Window
    {
        public AnalyticsMenu()
        {
            InitializeComponent();
            PopulateBookingData();
            PopulateCommentData();
        }

        private void ClearHeaderFields()
        {
            txtBookingNumber.Text = "";
            txtClassID.Text = "";
            txtBusName.Text = "";
            txtDateOfTrip.Text = "";
            txtTimeOfTrip.Text = "";
        }

        private void PopulateBookingData()
        {
            using (AppDBcontext BookingData = new AppDBcontext())
            {
                var BookingDataList = BookingData.tblBooking.ToList();
                foreach (var Item in BookingDataList)
                {
                    BookingList.Items.Add(Item);
                }
            }
        }

        private void PopulateCommentData()
        {
            using (AppDBcontext CommentData = new AppDBcontext())
            {
                var CommentsDataList = CommentData.tblBusComments.ToList();
                foreach (var Item in CommentsDataList)
                {
                    CommentsList.Items.Add(Item);
                }
            }
        }

        private void btnViewBook(object sender, RoutedEventArgs e)
        {
            ClearHeaderFields();
            BookingList.Items.Clear();
            PopulateBookingData();
        }

        private void btnApproveBook(object sender, RoutedEventArgs e)
        {
            int BookingID1 = Convert.ToInt32(txtBookingNumber.Text);
            try
            {
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblBooking.Find(BookingID1);
                    FindID.Approved = true;
                    DB.tblBooking.Update(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Trip has been approved", "Booking Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not update data", "Booking Maintainance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdateBook(object sender, RoutedEventArgs e)
        {
            int BookingID1 = Convert.ToInt32(txtBookingNumber.Text);
            try
            {
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblBooking.Find(BookingID1);
                    FindID.ClassID = Convert.ToInt32(txtClassID.Text);
                    FindID.BusName = Convert.ToString(txtBusName.Text);
                    FindID.DateOfTrip = Convert.ToDateTime(txtDateOfTrip.Text);
                    FindID.TimeOfTrip = Convert.ToDateTime(txtTimeOfTrip.Text);
                    DB.tblBooking.Update(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully updated", "Booking Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not update data", "Booking Maintainance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteBook(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this Booking?",
"Booking Maintenance", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int BookingID1 = Convert.ToInt32(txtBookingNumber.Text);
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblBooking.Find(BookingID1);
                    DB.tblBooking.Remove(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully deleted", "Booking Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearHeaderFields();
                    BookingList.Items.Clear();
                    PopulateBookingData();
                }
            }
            else
            {
                MessageBox.Show("Data has not been deleted", "Booking Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BookingList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (ListView)sender;
            var booking = (Booking)item.SelectedItem;
            txtBookingNumber.Text = booking.BookingNumber.ToString();
            txtClassID.Text = booking.ClassID.ToString();
            txtBusName.Text = booking.BusName;
            txtDateOfTrip.Text = booking.DateOfTrip.ToString();
            txtTimeOfTrip.Text = booking.TimeOfTrip.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TripStats tripStats = new TripStats();
            tripStats.Show();
            Close();
        }

        private void Button_Bus(object sender, RoutedEventArgs e)
        {
            BusMaintain busMaintain = new BusMaintain();
            busMaintain.Show();
            Close();
        }
    }
}
