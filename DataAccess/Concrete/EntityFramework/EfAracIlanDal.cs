using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
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
    public class EfAracIlanDal : EfEntityRepositoryBase<Ilan, AracContext>, IAracIlanDal
    {
        public async Task<List<AracIlanDto>> GetAllIlanDetails(Expression<Func<Ilan,bool>>? expr=null)
        {
            using (AracContext context = new AracContext())
            {

                IQueryable<Ilan> ilanlar = expr==null ? context.Set<Ilan>() : context.Set<Ilan>().Where(expr.Compile()).AsQueryable();
                IQueryable<AracIlanDto> result = from ilan in ilanlar
                             join user in context.Set<User>()
                             on ilan.UserId equals user.Id
                             join arac in context.Set<Arac>()
                             on ilan.AracId equals arac.Id
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
                             join cekisTipi in context.Set<CekisTipi>()
                             on arac.CekisTipiId equals cekisTipi.Id
                             join kasaTipi in context.Set<KasaTipi>()
                             on arac.KasaTipiId equals kasaTipi.Id
                             join resim in context.Set<AracResim>()
                             on arac.Id equals resim.AracId into resimler
                             where ilan.IsActive == true
                             select new AracIlanDto
                             {
                                 Ilan = new IlanDto
                                 {
                                     Ad = user.FirstName,
                                     Soyad = user.LastName,
                                     Aciklama = ilan.Aciklama,
                                     Il = ilan.Il,
                                     Ilce = ilan.Ilce,
                                     Mahalle = ilan.Mahalle,
                                     Tarih = ilan.Tarih,
                                     TelefonNo = ilan.TelefonNo
                                 },
                                 Arac = new AracDto
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
                                     Fiyat = arac.Fiyat
                                 },
                                 Resimler = resimler.Select(x=>x.ImagePath).ToList()
                             };
                return await result.ToListAsync();
            }
        }

        public async Task<AracIlanDto?> GetIlanDetailById(int id)
        {
            using (AracContext context = new AracContext())
            {
                var result = await (from ilan in context.Set<Ilan>()
                             join user in context.Set<User>()
                             on ilan.UserId equals user.Id
                             join arac in context.Set<Arac>()
                             on ilan.AracId equals arac.Id
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
                             join cekisTipi in context.Set<CekisTipi>()
                             on arac.CekisTipiId equals cekisTipi.Id
                             join kasaTipi in context.Set<KasaTipi>()
                             on arac.KasaTipiId equals kasaTipi.Id
                             join resim in context.Set<AracResim>()
                             on arac.Id equals resim.AracId into resimler
                             where ilan.Id == id && ilan.IsActive == true
                             select new AracIlanDto
                             {
                                 Ilan = new IlanDto
                                 {
                                     Ad = user.FirstName,
                                     Soyad = user.LastName,
                                     Aciklama = ilan.Aciklama,
                                     Il = ilan.Il,
                                     Ilce = ilan.Ilce,
                                     Mahalle = ilan.Mahalle,
                                     Tarih = ilan.Tarih,
                                     TelefonNo = ilan.TelefonNo
                                 },
                                 Arac = new AracDto
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
                                     Fiyat = arac.Fiyat
                                 },
                                 Resimler = resimler.Select(x=>x.ImagePath).ToList()
                             }).FirstOrDefaultAsync();
                return result;
            }
        }
    }
}
