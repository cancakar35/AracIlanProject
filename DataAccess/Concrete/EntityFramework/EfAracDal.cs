using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAracDal : EfEntityRepositoryBase<Arac, AracContext>, IAracDal
    {
        public async Task<List<AracDto>> GetAllAracDetails(Expression<Func<Arac, bool>>? expr=null)
        {
            using (AracContext context = new AracContext())
            {
                IQueryable<Arac> araclar = expr == null ? context.Set<Arac>() : context.Set<Arac>().Where(expr.Compile()).AsQueryable();
                var result = from arac in araclar
                             join kategori in context.Set<Kategori>()
                             on arac.KategoriId equals kategori.Id
                             join marka in context.Set<Marka>()
                             on arac.MarkaId equals marka.Id
                             join renk in context.Set<Renk>()
                             on arac.RenkId equals renk.Id
                             join vitesTipi in context.Set<VitesTipi>()
                             on arac.VitesTipiId equals vitesTipi.Id
                             join yakitTipi in context.Set<YakitTipi>()
                             on arac.YakitTipiId equals yakitTipi.Id
                             join cekisTipi in context.Set<CekisTipleri>()
                             on arac.CekisTipiId equals cekisTipi.Id
                             join kasaTipi in context.Set<KasaTipi>()
                             on arac.KasaTipiId equals kasaTipi.Id
                             select new AracDto
                             {
                                 Kategori = kategori.Name,
                                 Marka = marka.Name,
                                 Model = arac.Model,
                                 Seri = arac.Seri,
                                 UretimYili = arac.UretimYili,
                                 Renk = renk.Name,
                                 MotorGucuHP = arac.MotorGucuHP,
                                 MotorHacmiCC = arac.MotorHacmiCC,
                                 KiloMetre = arac.KiloMetre,
                                 VitesTipi = vitesTipi.Name,
                                 YakitTipi = yakitTipi.Name,
                                 CekisTipi = cekisTipi.Name,
                                 KasaTipi = kasaTipi.Name,
                                 Sifir = arac.Sifir,
                             };
                return await result.ToListAsync();
            }
        }

        public async Task<AracDto?> GetAracDetails(int id)
        {
            using (AracContext context = new AracContext())
            {
                var result = await (from arac in context.Set<Arac>()
                                    join kategori in context.Set<Kategori>()
                                    on arac.KategoriId equals kategori.Id
                                    join marka in context.Set<Marka>()
                                    on arac.MarkaId equals marka.Id
                                    join renk in context.Set<Renk>()
                                    on arac.RenkId equals renk.Id
                                    join vitesTipi in context.Set<VitesTipi>()
                                    on arac.VitesTipiId equals vitesTipi.Id
                                    join yakitTipi in context.Set<YakitTipi>()
                                    on arac.YakitTipiId equals yakitTipi.Id
                                    join cekisTipi in context.Set<CekisTipleri>()
                                    on arac.CekisTipiId equals cekisTipi.Id
                                    join kasaTipi in context.Set<KasaTipi>()
                                    on arac.KasaTipiId equals kasaTipi.Id
                                    where arac.Id == id
                                    select new AracDto
                                    {
                                        Kategori = kategori.Name,
                                        Marka = marka.Name,
                                        Model = arac.Model,
                                        Seri = arac.Seri,
                                        UretimYili = arac.UretimYili,
                                        Renk = renk.Name,
                                        MotorGucuHP = arac.MotorGucuHP,
                                        MotorHacmiCC = arac.MotorHacmiCC,
                                        KiloMetre = arac.KiloMetre,
                                        VitesTipi = vitesTipi.Name,
                                        YakitTipi = yakitTipi.Name,
                                        CekisTipi = cekisTipi.Name,
                                        KasaTipi = kasaTipi.Name,
                                        Sifir = arac.Sifir,
                                    }).SingleOrDefaultAsync();
                return result;
            }
        }
    }
}
