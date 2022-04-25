using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_blog.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_blog.Services.Context
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> UserInfo {get; set;}
        public DbSet<BlogitemModel> BlogInfo {get; set;}
        public DataContext(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

