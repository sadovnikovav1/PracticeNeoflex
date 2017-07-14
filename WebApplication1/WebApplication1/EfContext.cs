using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class EfContext : DbContext
    {
        public EfContext(string connectionString)
            : base(connectionString)
        { }

        public EfContext()
            : base("WebSocketChat_database")
        { }

        public DbSet<User> Users { get; set; }
    }
}