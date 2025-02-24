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
    /// Interaction logic for BusMaintain.xaml
    /// </summary>
    public partial class BusMaintain : Window
    {
        public BusMaintain()
        {
            InitializeComponent();
            PopulateBookingData();
        }

        private void PopulateBookingData()
        {
            //adds busses to the table
            BusList.Items.Clear();
            using (AppDBcontext BusData = new AppDBcontext())
            {
                var BusDataList = BusData.tblBus.ToList();
                foreach (var Item in BusDataList)
                {
                    BusList.Items.Add(Item);
                }
            }
        }

        private void ClearHeaderFields()
        {
            txtBusName.Text = "";
            txtBusPosition.Text = "";
            txtAvailable.Text = "";
        }

        private void btnViewBook(object sender, RoutedEventArgs e)
        {
            PopulateBookingData();
            ClearHeaderFields();
        }

        private void btnUpdateBook(object sender, RoutedEventArgs e)
        {
            try
            {
                string BusName = txtBusName.Text;
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblBus.Find(BusName);
                    FindID.Position = Convert.ToInt32(txtBusPosition.Text);
                    FindID.Available = Convert.ToBoolean(txtAvailable.Text);
                    DB.tblBus.Update(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully updated", "Bus Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString( ex));
                MessageBox.Show("Could not update data", "Bus Maintainance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            //re opens the analytics menu
            AnalyticsMenu analytics = new AnalyticsMenu();
            analytics.Show();
            Close();
        }

        private void BookingList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var item = (ListView)sender;
                var Bus = (Bus)item.SelectedItem;
                txtBusName.Text = Bus.BusName;
                txtBusPosition.Text = Bus.Position.ToString();
                txtAvailable.Text = Bus.Available.ToString();
            }
            catch (Exception)
            {

            }
        }
    }
}
