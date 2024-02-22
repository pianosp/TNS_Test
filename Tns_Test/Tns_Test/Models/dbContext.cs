using System;
using Microsoft.EntityFrameworkCore;

namespace Tns_Test.Models
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options): base(options){}
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}

