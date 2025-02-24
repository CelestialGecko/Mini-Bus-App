using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TripMaintanance
{
    class AppDBcontext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string DBpath = Root.Path;
            string cs = string.Format("Data Source={0}", DBpath);
            optionsBuilder.UseSqlite(cs);
        }
        public DbSet<Teacher> tblTeacher { get; set; }
        public DbSet<Student> tblStudent { get; set; }
        public DbSet<Class> tblClass { get; set; }
        public DbSet<BusComments> tblBusComments { get; set; }
        public DbSet<Bus> tblBus { get; set; }
        public DbSet<Booking> tblBooking { get; set; }

    }
}
