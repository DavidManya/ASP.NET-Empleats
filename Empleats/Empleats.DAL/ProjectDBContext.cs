using Common.Lib.Core;
using Microsoft.EntityFrameworkCore;
using Empleats.Lib.Models;

namespace Empleats.Lib.DAL
{
    public class ProjectDBContext : DbContext
    {
        public DbSet<Empleat> Empleats { get; set; }

        public ProjectDBContext(DbContextOptions<ProjectDBContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Entity>()
                   .Ignore(x => x.CurrentValidation);
        }
    }
}
