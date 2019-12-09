using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dataprovider.EF
{
    public partial class MyDataDBContext : DbContext
    {
        public MyDataDBContext()
        {
        }

        public MyDataDBContext(DbContextOptions<MyDataDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbBsPplb> TbBsPplb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;uid=root;pwd=zhq092012;port=3306;database=MyDataDB;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbBsPplb>(entity =>
            {
                entity.ToTable("tb_bs_pplb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Ctime)
                    .HasColumnName("ctime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");
            });
        }
    }
}
