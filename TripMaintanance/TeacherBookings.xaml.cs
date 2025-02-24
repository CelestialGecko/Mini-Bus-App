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

namespace TripMaintanance
{
    /// <summary>
    /// Interaction logic for TeacherBookings.xaml
    /// </summary>
    public partial class TeacherBookings : Window
    {
        public string CurrentIDs { get; set; }

        public TeacherBookings()
        {
            InitializeComponent();
        }

        private void PopulateBookingData()
        {
            using (AppDBcontext BookingData = new AppDBcontext())
            {
                BookingList.Items.Clear();
                var BookingDataList = BookingData.tblBooking.ToList();
                var ClassDataList = BookingData.tblClass.ToList();
                MessageBox.Show(CurrentIDs);
                foreach (var CItem in ClassDataList)
                {
                    if (CurrentIDs == Convert.ToString(CItem.TeacherID))
                    {
                        foreach (var BItem in BookingDataList)
                        {
                            if (CItem.ClassID == BItem.ClassID)
                            {
                                BookingList.Items.Add(BItem);
                            }
                        }
                    }
                }
            }
        }

        private void BookingList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var item = (ListView)sender;
                var Bookings = (Booking)item.SelectedItem;
                txtBookingNumber.Text = Bookings.BookingNumber.ToString();
                txtClassID.Text = Bookings.ClassID.ToString();
                txtBusName.Text = Bookings.BusName;
                txtDateOfTrip.Text = Bookings.DateOfTrip.ToString();
                txtTimeOfTrip.Text = Bookings.TimeOfTrip.ToString();
            }
            catch (Exception)
            {

            }
        }

        private void ClearHeaderFields()
        {
            txtBookingNumber.Text = "";
            txtClassID.Text = "";
            txtBusName.Text = "";
            txtDateOfTrip.Text = "";
            txtTimeOfTrip.Text = "";
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void btnUpdateBook(object sender, RoutedEventArgs e)
        {
            try
            {
                int BookingID1 = Convert.ToInt32(txtBookingNumber.Text);
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblBooking.Find(BookingID1);
                    FindID.ClassID = Convert.ToInt32(txtClassID.Text);
                    FindID.BusName = Convert.ToString(txtBusName.Text);
                    FindID.DateOfTrip = Convert.ToDateTime(txtDateOfTrip.Text);
                    FindID.TimeOfTrip = Convert.ToDateTime(txtTimeOfTrip.Text);
                    FindID.Approved = false;
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

        private void btnViewBook(object sender, RoutedEventArgs e)
        {
            PopulateBookingData();
            ClearHeaderFields();
        }
    }
}
