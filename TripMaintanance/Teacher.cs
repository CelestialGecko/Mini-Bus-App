using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace TripMaintanance
{
    class Teacher
    {
        [Key]
        public int TeacherID { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherSecondName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string TeacherType { get; set; }
    }
    class Student
    {
        [Key]
        public int StudentID { get; set; }

        public int ClassID { get; set; }

        public string StudentFirstName { get; set; }

        public string StudentSecondName { get; set; }

        public string StudentType { get; set; }
    }
    class Class
    {
        [Key]
        public int ClassID { get; set; }

        public int TeacherID { get; set; }

        public string ClassName { get; set; }

        public char ClassType { get; set; }
    }
    class BusComments
    {
        [Key]
        public int CommentID { get; set; }

        public string BusName { get; set; }

        public string BusComment { get; set; }
    }
    class Bus
    {
       [Key]
        public string BusName { get; set; }

        public bool Available { get; set; }

        public int Position { get; set; }
    }
    class Booking
    {
        [Key]
        public int BookingNumber { get; set; }

        public int ClassID { get; set; }

        public string BusName { get; set; }

        public DateTime DateOfTrip { get; set; }

        public DateTime TimeOfTrip { get; set; }

        public bool Approved { get; set; }
    }
    public class Root
    {
        public static readonly string Path = $@"{Directory.GetCurrentDirectory()}\Trip.db";
    }
}
