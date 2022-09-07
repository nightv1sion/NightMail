using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
    }
}
