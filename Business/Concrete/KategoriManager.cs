using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class KategoriManager : IKategoriService
    {
        private readonly IKategoriDal _kategoriDal;

        public KategoriManager(IKategoriDal kategoriDal)
        {
            _kategoriDal = kategoriDal;
        }

        public async Task<IDataResult<Kategori>> GetKategoriById(int id)
        {
            try
            {
                Kategori? kategori = await _kategoriDal.Get(x => x.Id == id);
                if (kategori == null)
                {
                    return new ErrorDataResult<Kategori>("Kategori bulunamadı");
                }
                return new SuccessDataResult<Kategori>(kategori);
            }
            catch
            {
                return new ErrorDataResult<Kategori>("İşlem başarısız.");
            }
        }

        public async Task<IDataResult<List<Kategori>>> GetKategoriList()
        {
            try
            {
                return new SuccessDataResult<List<Kategori>>(await _kategoriDal.GetAll());
            }
            catch
            {
                return new ErrorDataResult<List<Kategori>>("İşlem başarısız.");
            }
        }
    }
}
