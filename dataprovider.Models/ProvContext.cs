using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace dataprovider.Models
{
  public class ProvContext : DbContext
  {
    //protected ProvContext()
    //{
    //}

    public ProvContext(DbContextOptions options) : base(options)
    {
    }
    public virtual DbSet<TbBsPplb> TbBsPplb { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //  optionsBuilder.UseMySql("server=localhost;port=3306;uid=sa;pwd=zhq092012;database=MyDataDB");
    //}
  }
}
