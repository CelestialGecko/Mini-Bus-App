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
    /// Interaction logic for StudentMaintain.xaml
    /// </summary>
    public partial class StudentMaintain : Window
    {
        public List<int> StudIDLis = new List<int>();
        public List<string> StudNameLis = new List<string>();

        public char CurrentTeacherType { get; set; }

        public StudentMaintain()
        {
            InitializeComponent();
            PopulateStudentData();
            NewID();
        }

        private void PopulateStudentData()
        {
            using (AppDBcontext StudentData = new AppDBcontext())
            {
                var StudentDataList = StudentData.tblStudent.ToList();
                foreach (var Item in StudentDataList)
                {
                    StudentList.Items.Add(Item);
                }
            }
        }

        private void NewID()
        {
            int NewStudentIDs = 0;
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            string queryString = "SELECT MAX(StudentID) + 1 AS StudentID " + "FROM tblStudent";
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
                        string NewStudentID = reader[0].ToString();
                        NewStudentIDs = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            txtStudentID.Text = NewStudentIDs.ToString();
        }

        private void ClearHeaderFields()
        {
            NewID();
            txtClassID.Text = "";
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtStudentType.Text = "";
        }

        private void btnView(object sender, RoutedEventArgs e)
        {
            ClearHeaderFields();
            StudentList.Items.Clear();
            PopulateStudentData();
        }

        private void btnInsert(object sender, RoutedEventArgs e)
        {
            try
            {
                var StudID = Convert.ToInt32(txtStudentID.Text);
                var ClasID = Convert.ToInt32(txtClassID.Text);
                var FirstName = txtFirstName.Text;
                var SecondName = txtSecondName.Text;
                var TypeTeacher = txtStudentType.Text;
                using (AppDBcontext DB = new AppDBcontext())
                {
                    Student Student = new Student()
                    {
                        StudentID = StudID,
                        ClassID = ClasID,
                        StudentFirstName = FirstName,
                        StudentSecondName = SecondName,
                        StudentType = TypeTeacher
                    };
                    DB.tblStudent.Add(Student);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully saved", "Student Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearHeaderFields();
                    StudentList.Items.Clear();
                    PopulateStudentData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Data has not been saved", "Student Maintenance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate(object sender, RoutedEventArgs e)
        {
            int StudentID1 = Convert.ToInt32(txtStudentID.Text);
            try
            {
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblStudent.Find(StudentID1);
                    FindID.ClassID = Convert.ToInt32(txtClassID.Text);
                    FindID.StudentFirstName = txtFirstName.Text;
                    FindID.StudentSecondName = txtSecondName.Text;
                    FindID.StudentType = txtStudentType.Text;
                    DB.tblStudent.Update(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully updated", "Student Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not update data", "Student Maintainance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete Student " + txtFirstName.Text + " " + txtSecondName.Text + "?",
"Student Maintenance", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int StudentID1 = Convert.ToInt32(txtStudentID.Text);
                using (AppDBcontext DB = new AppDBcontext())
                {
                    var FindID = DB.tblStudent.Find(StudentID1);
                    DB.tblStudent.Remove(FindID);
                    DB.SaveChanges();
                    MessageBox.Show("Data has been successfully deleted", "Student Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearHeaderFields();
                    StudentList.Items.Clear();
                    PopulateStudentData();
                }
            }
            else
            {
                MessageBox.Show("Data has not been deleted", "Student Maintenance", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TeacherButton(object sender, RoutedEventArgs e)
        {
            TeacherMaintain teacherMaintainOpen = new TeacherMaintain();
            teacherMaintainOpen.Show();
            Close();
        }

        private void ClassButton(object sender, RoutedEventArgs e)
        {
            var classMaintainOpen = new ClassMaintain();
            classMaintainOpen.CurrentTeacherType = CurrentTeacherType;
            classMaintainOpen.Show();
            Close();
        }

        private void StudentList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var item = (ListView)sender;
                var student = (Student)item.SelectedItem;
                txtStudentID.Text = student.StudentID.ToString();
                txtClassID.Text = student.ClassID.ToString();
                txtFirstName.Text = student.StudentFirstName;
                txtSecondName.Text = student.StudentSecondName;
                txtStudentType.Text = student.StudentType;
            }
            catch (Exception)
            {

            }
        }

        private void SetListItems()
        {
            string _db = Root.Path;
            string cs = string.Format("Data source={0}", _db);
            string queryString = "SELECT StudentID, StudentFirstName From tblStudent";
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
                        StudIDLis.Add(Convert.ToInt32(reader["StudentID"]));
                        StudNameLis.Add(Convert.ToString(reader["StudentFirstName"]));
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
            int right = StudNameLis.Count - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                int result = string.Compare(StudNameLis[mid], SearchName);

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
                LeftArray[i] = StudNameLis[left + i];
                LeftID[i] = StudIDLis[left + i];
            }
            for (int j = 0; j < RightSize; ++j)
            {
                RightArray[j] = StudNameLis[mid + 1 + j];
                RightID[j] = StudIDLis[mid + 1 + j];
            }

            int k = left;
            int l = 0;
            int m = 0;
            while (l < LeftSize && m < RightSize)
            {
                if (LeftArray[l].CompareTo(RightArray[m]) <= 0)
                {
                    StudNameLis[k] = LeftArray[l];
                    StudIDLis[k] = LeftID[l];
                    l++;
                }
                else
                {
                    StudNameLis[k] = RightArray[m];
                    StudIDLis[k] = RightID[m];
                    m++;
                }
                k++;
            }

            while (l < LeftSize)
            {
                StudNameLis[k] = LeftArray[l];
                StudIDLis[k] = LeftID[l];
                l++;
                k++;
            }

            while (m < RightSize)
            {
                StudNameLis[k] = RightArray[m];
                StudIDLis[k] = RightID[m];
                m++;
                k++;
            }
        }

        private void DisplayListItems()
        {
            StudentList.Items.Clear();
            foreach (var item in StudIDLis)
            {
                using (AppDBcontext db = new AppDBcontext())
                {
                    var FindID = db.tblStudent.Find(item);
                    StudentList.Items.Add(FindID);
                }
            }
        }

        private void btnSearch(object sender, RoutedEventArgs e)
        {
            StudNameLis.Clear();
            StudIDLis.Clear();
            SetListItems();
            SortListItems(0, StudNameLis.Count - 1);
            int Location = SearchListItems(txtSearch.Text);
            if (Location == -1)
            {
                MessageBox.Show("Could not find name", "Student Maintainance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Location = StudIDLis[Location];
                StudIDLis.Clear();
                StudIDLis.Add(Location);
                DisplayListItems();
            }
        }

        private void btnSort(object sender, RoutedEventArgs e)
        {
            StudNameLis.Clear();
            StudIDLis.Clear();
            SetListItems();
            SortListItems(0, StudNameLis.Count - 1);
            DisplayListItems();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            if (CurrentTeacherType == 'H')
            {
                IStud.Visibility = Visibility.Visible;
                TStud.Visibility = Visibility.Visible;
                bStud.Visibility = Visibility.Visible;
            }
        }
    }
}
