using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<Data.ApplicationDbContext> options)
            : base(options)
        {


        }
        

           public DbSet<Student> students { get; set; }
    }

    
}
