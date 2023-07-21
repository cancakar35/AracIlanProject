using Business.Abstract;
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
    public class RenkService : OzellikService<Renk>
    {
        public RenkService(IRenkDal repository) : base(repository)
        {
            
        }
    }
}
