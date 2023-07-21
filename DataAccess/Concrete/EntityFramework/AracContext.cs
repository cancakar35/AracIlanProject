using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class AracContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=CAN-DESKTOP;Database=CarDB;Trusted_Connection=true;encrypt=false;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }

        DbSet<User> Users { get; set; }
        DbSet<OperationClaim> OperationClaims { get; set; }
        DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        DbSet<Arac> Araclar { get; set; }
        DbSet<AracResim> AracResimleri { get; set;}
        DbSet<CekisTipi> CekisTipleri { get; set; }
        DbSet<Ilan> Ilanlar { get; set; }
        DbSet<KasaTipi> KasaTipleri { get; set; }
        DbSet<Kategori> Kategoriler { get; set; }
        DbSet<Marka> Markalar { get; set; }
        DbSet<Renk> Renkler { get; set; }
        DbSet<VitesTipi> VitesTipleri { get; set; }
        DbSet<YakitTipi> YakitTipleri { get; set; }
    }
}
