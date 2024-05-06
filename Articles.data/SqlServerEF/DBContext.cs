using Articles.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.data.SqlServerEF
{
 public class DBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ASUSX1502\\MSSQLSERVER01;Database=ArticlesDB;Trusted_Connection=True");
        }
        public DbSet<Student> Students { get; set; }

        public DbSet<category>Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<AuthorPost> AuthorPost { get; set; }



    }
}
