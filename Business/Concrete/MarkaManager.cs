using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MarkaManager : IMarkaService
    {
        private readonly IMarkaDal _markaDal;

        public MarkaManager(IMarkaDal markaDal)
        {
            _markaDal = markaDal;
        }

        public async Task<IDataResult<Marka>> GetMarkaById(int id)
        {
            try
            {
                Marka? marka = await _markaDal.Get(x => x.Id == id);
                if (marka == null)
                {
                    return new ErrorDataResult<Marka>("Marka bulunamadı");
                }
                return new SuccessDataResult<Marka>(marka);
            }
            catch
            {
                return new ErrorDataResult<Marka>("İşlem başarısız.");
            }
        }

        public async Task<IDataResult<List<Marka>>> GetMarkaList()
        {
            try
            {
                return new SuccessDataResult<List<Marka>>(await _markaDal.GetAll());
            }
            catch
            {
                return new ErrorDataResult<List<Marka>>("İşlem başarısız.");
            }
        }
    }
}
