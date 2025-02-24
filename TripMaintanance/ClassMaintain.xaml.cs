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
    /// Interaction logic for ClassMaintain.xaml
    /// </summary>
    public partial class ClassMaintain : Window
    {
        public char CurrentTeacherType { get; set; }

        public List<int> ClasIDLis = new List<int>();
        public List<string> ClasNameLis = new List<string>();

        public ClassMaintain()
        {
            InitializeComponent();
            PopulateClassData();
            NewID();
        }

        private void PopulateClassData()
        {
            using (AppDBcontext ClassData = new AppDBcontext())
            {
                var ClassDataList = ClassData.tblClass.ToList();
                foreach (var Item in ClassDataList)
                {
                    ClassList.Items.Add(Item);
                }
            }
        }

        private void NewID()
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
                        string NewClassID = reader[0].ToString();
                        NewClassIDs = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            txtClassID.Text = NewClassIDs.ToString();
        }

        private void ClearHeaderFields()
        {
            NewID();
            txtTeacherID.Text = "";
            txtClassName.Text = "";
            txtClassType.Text = "";
        }

        private void StudentButton(object sender, RoutedEventArgs e)
        {
            var studentMaintainOpen = new StudentMaintain();
            studentMaintainOpen.CurrentTeacherType = CurrentTeacherType;
            studentMaintainOpen.Show();
            Close();
        }

        private void TeacherButton(object sender, RoutedEventArgs e)
        {
            TeacherMaintain teacherMaintainOpen = new TeacherMaintain();
            teacherMaintainOpen.Show();
            Close();
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete " + txtClassName.Text + " Class?",
"Class Maintenance", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int ClassID1 = Convert.ToInt32(txtClassID.Text);
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblClass.Find(ClassID1);
                    DB.tblClass.Remove(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully deleted", "Class Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearHeaderFields();
                    ClassList.Items.Clear();
                    PopulateClassData();
                }
            }
            else
            {
                MessageBox.Show("Data has not been deleted", "Class Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnUpdate(object sender, RoutedEventArgs e)
        {
            int ClassID1 = Convert.ToInt32(txtClassID.Text);
            try
            {
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblClass.Find(ClassID1);
                    FindID.TeacherID = Convert.ToInt32(txtTeacherID.Text);
                    FindID.ClassName = txtClassName.Text;
                    FindID.ClassType = Convert.ToChar(txtClassType.Text);
                    DB.tblClass.Update(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully updated", "Class Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not update data", "Class Maintainance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnInsert(object sender, RoutedEventArgs e)
        {
            try
            {
                var ClasID = Convert.ToInt32(txtClassID.Text);
                var TeachID = Convert.ToInt32(txtTeacherID.Text);
                var ClasName = txtClassName.Text;
                var ClasType = Convert.ToChar(txtClassType.Text);
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
                    MessageBox.Show("Data has been successfully saved", "Class Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearHeaderFields();
                    ClassList.Items.Clear();
                    PopulateClassData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Data has not been saved", "Class Maintenance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnView(object sender, RoutedEventArgs e)
        {
            ClearHeaderFields();
            ClassList.Items.Clear();
            PopulateClassData();
        }

        private void ClassList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (ListView)sender;
            var classes = (Class)item.SelectedItem;
            txtClassID.Text = classes.ClassID.ToString();
            txtTeacherID.Text = classes.TeacherID.ToString();
            txtClassName.Text = classes.ClassName;
            txtClassType.Text = classes.ClassType.ToString();
        }

        private void SetListItems()
        {
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            string queryString = "SELECT ClassID, ClassName From tblClass";
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
                        ClasIDLis.Add(Convert.ToInt32(reader["ClassID"]));
                        ClasNameLis.Add(Convert.ToString(reader["ClassName"]));
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
            int right = ClasNameLis.Count - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                int result = string.Compare(ClasNameLis[mid], SearchName);

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
                LeftArray[i] = ClasNameLis[left + i];
                LeftID[i] = ClasIDLis[left + i];
            }
            for (int j = 0; j < RightSize; ++j)
            {
                RightArray[j] = ClasNameLis[mid + 1 + j];
                RightID[j] = ClasIDLis[mid + 1 + j];
            }

            int k = left;
            int l = 0;
            int m = 0;
            while (l < LeftSize && m < RightSize)
            {
                if (LeftArray[l].CompareTo(RightArray[m]) <= 0)
                {
                    ClasNameLis[k] = LeftArray[l];
                    ClasIDLis[k] = LeftID[l];
                    l++;
                }
                else
                {
                    ClasNameLis[k] = RightArray[m];
                    ClasIDLis[k] = RightID[m];
                    m++;
                }
                k++;
            }

            while (l < LeftSize)
            {
                ClasNameLis[k] = LeftArray[l];
                ClasIDLis[k] = LeftID[l];
                l++;
                k++;
            }

            while (m < RightSize)
            {
                ClasNameLis[k] = RightArray[m];
                ClasIDLis[k] = RightID[m];
                m++;
                k++;
            }
        }

        private void DisplayListItems()
        {
            ClassList.Items.Clear();
            foreach (var item in ClasIDLis)
            {
                using (AppDBcontext db = new AppDBcontext())
                {
                    var FindID = db.tblClass.Find(item);
                    ClassList.Items.Add(FindID);
                }
            }
        }

        private void btnSearch(object sender, RoutedEventArgs e)
        {
            ClasNameLis.Clear();
            ClasIDLis.Clear();
            SetListItems();
            SortListItems(0, ClasNameLis.Count - 1);
            int Location = SearchListItems(txtSearch.Text);
            if(Location == -1)
            {
                MessageBox.Show("Could not find name", "Class Maintainance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Location = ClasIDLis[Location];
                ClasIDLis.Clear();
                ClasIDLis.Add(Location);
                DisplayListItems();
            }
        }

        private void btnSort(object sender, RoutedEventArgs e)
        {
            ClasNameLis.Clear();
            ClasIDLis.Clear();
            SetListItems();
            SortListItems(0, ClasNameLis.Count - 1);
            DisplayListItems();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            if (CurrentTeacherType == 'H')
            {
                ITeach.Visibility = Visibility.Visible;
                TTeach.Visibility = Visibility.Visible;
                bTeach.Visibility = Visibility.Visible;
            }
        }
    }
}
