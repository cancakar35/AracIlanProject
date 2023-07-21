using Business.Abstract;
using Core.DataAccess;
using Core.Entities.Abstract;
using Core.Utilities.Results;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OzellikService<T> : IOzellikService<T> where T : class, IOzellik, IEntity, new()
    {
        private readonly IEntityRepository<T> _entityRepository;

        public OzellikService(IEntityRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<IDataResult<List<T>>> GetAll()
        {
            try
            {
                return new SuccessDataResult<List<T>>(await _entityRepository.GetAll());
            }
            catch
            {
                return new ErrorDataResult<List<T>>("İşlem başarısız.");
            }
        }

        public async Task<IDataResult<string>> GetById(int id)
        {
            try
            {
                var result = await _entityRepository.Get(x => x.Id == id);
                if (result == null)
                {
                    return new ErrorDataResult<string>("Bulunamadı!");
                }
                return new SuccessDataResult<string>(data:result.Name);
            }
            catch
            {
                return new ErrorDataResult<string>("İşlem başarısız.");
            }
        }
    }
}
