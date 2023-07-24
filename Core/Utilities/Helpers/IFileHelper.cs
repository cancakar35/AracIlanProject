using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public interface IFileHelper
    {
        Task<string?> Upload(IFormFile file, string root);
        Task<string?> Update(IFormFile file, string filePath, string root);
        void Delete(string filePath);
    }
}
