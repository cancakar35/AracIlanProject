using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMarkaService
    {
        Task<IDataResult<List<Marka>>> GetMarkaList();
        Task<IDataResult<Marka>> GetMarkaById(int id);
    }
}
