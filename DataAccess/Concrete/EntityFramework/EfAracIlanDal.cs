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
        public async Task<List<IlanDto>> GetAllIlanDetails(Expression<Func<Ilan,bool>>? expr=null)
        {
            using (AracContext context = new AracContext())
            {

                IQueryable<Ilan> ilanlar = expr==null ? context.Set<Ilan>() : context.Set<Ilan>().Where(expr.Compile()).AsQueryable();
                IQueryable<IlanDto> result = from ilan in ilanlar
                             join user in context.Set<User>()
                             on ilan.UserId equals user.Id
                             where ilan.IsActive == true
                             select new IlanDto
                             {
                                 Ad = user.FirstName,
                                 Soyad = user.LastName,
                                 Aciklama = ilan.Aciklama,
                                 Il = ilan.Il,
                                 Ilce = ilan.Ilce,
                                 Mahalle = ilan.Mahalle,
                                 Tarih = ilan.Tarih,
                                 TelefonNo = ilan.TelefonNo
                             };
                return await result.ToListAsync();
            }
        }

        public async Task<IlanDto?> GetIlanDetailById(int id)
        {
            using (AracContext context = new AracContext())
            {
                var result = await (from ilan in context.Set<Ilan>()
                             join user in context.Set<User>()
                             on ilan.UserId equals user.Id
                             where ilan.Id == id && ilan.IsActive == true
                             select new IlanDto
                             {
                                 Ad = user.FirstName,
                                 Soyad = user.LastName,
                                 Aciklama = ilan.Aciklama,
                                 Il = ilan.Il,
                                 Ilce = ilan.Ilce,
                                 Mahalle = ilan.Mahalle,
                                 Tarih = ilan.Tarih,
                                 TelefonNo = ilan.TelefonNo
                             }).FirstOrDefaultAsync();
                return result;
            }
        }
    }
}
