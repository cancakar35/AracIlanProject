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

        DbSet<Arac> Araclar { get; set; }
        DbSet<AracResim> AracResimleri { get; set;}
        DbSet<Cekis> Cekisler { get; set; }
        DbSet<Ilan> Ilanlar { get; set; }
        DbSet<KasaTipi> KasaTipleri { get; set; }
        DbSet<Kategori> Kategoriler { get; set; }
        DbSet<Marka> Markalar { get; set; }
        DbSet<Motosiklet> Motosikletler { get; set; }
        DbSet<Otomobil> Otomobiller { get; set; }
        DbSet<Renk> Renkler { get; set; }
        DbSet<VitesTipi> VitesTipleri { get; set; }
        DbSet<YakitTipi> YakitTipleri { get; set; }
    }
}
