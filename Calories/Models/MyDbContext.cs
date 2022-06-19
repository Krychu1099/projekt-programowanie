using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Calories.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CaloriesModel> Calories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=D:\Users\Krystian\source\repos\Calories\Calories\Db.db");
    }
}
