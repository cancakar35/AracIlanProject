using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CekisTipiService : OzellikService<CekisTipi>
    {
        public CekisTipiService(ICekisTipiDal repository) : base(repository)
        {
            
        }
    }
}
