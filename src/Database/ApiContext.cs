// ReSharper disable UnusedAutoPropertyAccessor.Global

using API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}