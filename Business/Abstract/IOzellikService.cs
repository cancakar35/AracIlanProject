using Core.Entities.Abstract;
using Core.Utilities.Results;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOzellikService<T> where T : class, IOzellik, new()
    {
        Task<IDataResult<List<T>>> GetAll();
        Task<IDataResult<string>> GetById(int id);
    }
}
