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
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace TripMaintanance
{
    /// <summary>
    /// Interaction logic for TeacherMaintain.xaml
    /// </summary>
    public partial class TeacherMaintain : Window
    {
        public List<int> TeachIDLis = new List<int>();
        public List<string> TeachNameLis = new List<string>();

        public char CurrentTeacherType { get; set; }

        public TeacherMaintain()
        {
            InitializeComponent();
            PopulateTeacherData();
            NewID();
        }

        private void PopulateTeacherData()
        {
            using (AppDBcontext TeacherData = new AppDBcontext())
            {
                var TeacherDataList = TeacherData.tblTeacher.ToList();
                foreach (var Item in TeacherDataList)
                {
                    TeacherList.Items.Add(Item);
                }
            }
        }

        private void NewID()
        {
            int NewTeacherIDs = 0;
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            string queryString = "SELECT MAX(TeacherID) + 1 AS TeacherID " + "FROM tblTeacher";
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
                        string NewTeacherID = reader[0].ToString();
                        NewTeacherIDs = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            txtTeacherID.Text = NewTeacherIDs.ToString();
        }

        private void ClearHeaderFields()
        {
            NewID();
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtPassword.Text = "";
            txtUsername.Text = "";
            txtTeacherType.Text = "";
        }

        private void btnView(object sender, RoutedEventArgs e)
        {
            ClearHeaderFields();
            TeacherList.Items.Clear();
            PopulateTeacherData();
        }

        private void btnInsert(object sender, RoutedEventArgs e)
        {
            try
            {
                var TeachID = Convert.ToInt32(txtTeacherID.Text);
                var FirstName = txtFirstName.Text;
                var SecondName = txtSecondName.Text;
                var UserName = txtUsername.Text;
                var PassWord = txtPassword.Text;
                var TypeTeacher = txtTeacherType.Text;
                using (AppDBcontext DB = new AppDBcontext())
                {
                    Teacher teacher = new Teacher()
                    {
                        TeacherID = TeachID,
                        TeacherFirstName = FirstName,
                        TeacherSecondName = SecondName,
                        Username = UserName,
                        Password = PassWord,
                        TeacherType = TypeTeacher
                    };
                    DB.tblTeacher.Add(teacher);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully saved", "Teacher Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearHeaderFields();
                    TeacherList.Items.Clear();
                    PopulateTeacherData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Data has not been saved", "Teacher Maintenance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate(object sender, RoutedEventArgs e)
        {
            int TeacherID1 = Convert.ToInt32(txtTeacherID.Text);
            try
            {
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblTeacher.Find(TeacherID1);
                    FindID.Username = txtUsername.Text;
                    FindID.Password = txtPassword.Text;
                    FindID.TeacherFirstName = txtFirstName.Text;
                    FindID.TeacherSecondName = txtSecondName.Text;
                    FindID.TeacherType = txtTeacherType.Text;
                    DB.tblTeacher.Update(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully updated", "Teacher Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not update data", "Teacher Maintainance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete Teacher " + txtFirstName.Text + " " + txtSecondName.Text + "?",
    "Teacher Maintenance", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int TeacherID1 = Convert.ToInt32(txtTeacherID.Text);
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblTeacher.Find(TeacherID1);
                    DB.tblTeacher.Remove(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully deleted", "Teacher Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearHeaderFields();
                    TeacherList.Items.Clear();
                    PopulateTeacherData();
                }
            }
            else
            {
                MessageBox.Show("Data has not been deleted", "Teacher Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TeacherList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (ListView)sender;
            var teacher = (Teacher)item.SelectedItem;
            txtTeacherID.Text = teacher.TeacherID.ToString();
            txtFirstName.Text = teacher.TeacherFirstName;
            txtSecondName.Text = teacher.TeacherSecondName;
            txtUsername.Text = teacher.Username;
            txtPassword.Text = teacher.Password;
            txtTeacherType.Text = teacher.TeacherType;
        }

        private void ClassButton(object sender, RoutedEventArgs e)
        {
            ClassMaintain classMaintainOpen = new ClassMaintain();
            classMaintainOpen.Show();
            Close();
        }

        private void StudentButton(object sender, RoutedEventArgs e)
        {
            StudentMaintain studentMaintainOpen = new StudentMaintain();
            studentMaintainOpen.Show();
            Close();
        }

        private void SetListItems()
        {
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            string queryString = "SELECT TeacherID, TeacherFirstName From tblTeacher";
            //connects to the data base and exercutes the query
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                try
                {
                    SqliteCommand command = new SqliteCommand(queryString, connection);
                    connection.Open();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //adds the values for each column to a given list depending on what column name is used
                        TeachIDLis.Add(Convert.ToInt32(reader["TeacherID"]));
                        TeachNameLis.Add(Convert.ToString(reader["TeacherFirstName"]));
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    //ensures that the program does not crash if an issue occurs connecting to the data base
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public int SearchListItems(string SearchName)
        {
            int left = 0;
            int right = TeachNameLis.Count - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                int result = string.Compare(TeachNameLis[mid], SearchName);

                if (result == 0)
                {
                    return mid;
                }
                else if (result < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return -1;
        }

        private void SortListItems(int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                SortListItems(left, mid);
                SortListItems(mid + 1, right);
                Merge(left, mid, right);
            }
        }

        public void Merge(int left, int mid, int right)
        {
            int LeftSize = mid - left + 1;
            int RightSize = right - mid;

            string[] LeftArray = new string[LeftSize];
            int[] LeftID = new int[LeftSize];
            string[] RightArray = new string[RightSize];
            int[] RightID = new int[RightSize];

            for (int i = 0; i < LeftSize; ++i)
            {
                LeftArray[i] = TeachNameLis[left + i];
                LeftID[i] = TeachIDLis[left + i];
            }
            for (int j = 0; j < RightSize; ++j)
            {
                RightArray[j] = TeachNameLis[mid + 1 + j];
                RightID[j] = TeachIDLis[mid + 1 + j];
            }

            int k = left;
            int l = 0;
            int m = 0;
            while (l < LeftSize && m < RightSize)
            {
                if (LeftArray[l].CompareTo(RightArray[m]) <= 0)
                {
                    TeachNameLis[k] = LeftArray[l];
                    TeachIDLis[k] = LeftID[l];
                    l++;
                }
                else
                {
                    TeachNameLis[k] = RightArray[m];
                    TeachIDLis[k] = RightID[m];
                    m++;
                }
                k++;
            }

            while (l < LeftSize)
            {
                TeachNameLis[k] = LeftArray[l];
                TeachIDLis[k] = LeftID[l];
                l++;
                k++;
            }

            while (m < RightSize)
            {
                TeachNameLis[k] = RightArray[m];
                TeachIDLis[k] = RightID[m];
                m++;
                k++;
            }
        }

        private void DisplayListItems()
        {
            TeacherList.Items.Clear();
            foreach (var item in TeachIDLis)
            {
                using (AppDBcontext db = new AppDBcontext())
                {
                    var FindID = db.tblTeacher.Find(item);
                    TeacherList.Items.Add(FindID);
                }
            }
        }

        private void btnSearch(object sender, RoutedEventArgs e)
        {
            TeachNameLis.Clear();
            TeachIDLis.Clear();
            SetListItems();
            SortListItems(0, TeachNameLis.Count - 1);
            int Location = SearchListItems(txtSearch.Text);
            if (Location == -1)
            {
                MessageBox.Show("Could not find name", "Teacher Maintainance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Location = TeachIDLis[Location];
                TeachIDLis.Clear();
                TeachIDLis.Add(Location);
                DisplayListItems();
            }
        }

        private void btnSort(object sender, RoutedEventArgs e)
        {
            TeachNameLis.Clear();
            TeachIDLis.Clear();
            SetListItems();
            SortListItems(0, TeachNameLis.Count - 1);
            DisplayListItems();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            if (CurrentTeacherType == 'H')
            {
                IClass.Visibility = Visibility.Visible;
                TClass.Visibility = Visibility.Visible;
                BClass.Visibility = Visibility.Visible;
            }
        }
    }
}
