using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace dataprovider.Models
{
    public class ProvContext : DbContext
    {
        public ProvContext(DbContextOptions<ProvContext> options) : base(options)
        {

        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<TbBsPplb> TbBsPplb { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.HasDefaultSchema("ProvContext");
        //}
    }
}
