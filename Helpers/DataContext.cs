using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAuth.API.Models;

namespace UserAuth.API.Helpers
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options) {}
        public DbSet<User> Users { get; set; }
    }
}