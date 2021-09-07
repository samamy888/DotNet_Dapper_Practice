using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice.EF
{
    public class EFContext : DbContext
    {
        public DbSet<TestModel> EFTable { get; set; }
        private string connectionString { get; }


        public EFContext(IConfiguration config)
        {
            string DataSource = config["EFDB"];
            connectionString = @$"Data Source={DataSource}";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(connectionString);



    }
}
