using Business.Abstract;
using Business.Constants;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AracResimManager : IAracResimService
    {
        private readonly IAracResimDal _aracResimDal;
        private readonly IFileHelper _fileHelper;

        public AracResimManager(IAracResimDal aracResimDal, IFileHelper fileHelper)
        {
            _aracResimDal = aracResimDal;
            _fileHelper = fileHelper;
        }

        public async Task<IResult> Add(IFormFileCollection fileCollection, int aracId)
        {
            if (fileCollection == null)
            {
                return new ErrorResult("Lütfen en az bir resim yükleyiniz.");
            }
            if (fileCollection.Count > 5)
            {
                return new ErrorResult("5'ten fazla resim yüklenemez.");
            }
            AracResim newResim;
            string[] supportedTypes = { ".png", ".jpg", ".jpeg", ".webp" };
            if (fileCollection.Any(file => file == null || !supportedTypes.Contains(Path.GetExtension(file.FileName))))
            {
                return new ErrorResult("Lütfen desteklenen resim dosyalarını yükleyiniz. (png, jpg, webp)");
            }
            foreach (IFormFile file in fileCollection)
            {
                string? fileName = await _fileHelper.Upload(file, PathConstants.ImagePaths);
                if (fileName != null)
                {
                    newResim = new AracResim
                    {
                        AracId = aracId,
                        ImagePath = fileName,
                        CreatedAt = DateTime.Now
                    };
                    await _aracResimDal.Add(newResim);
                }
            }
            return new SuccessResult();
        }

        public async Task<IResult> Delete(int id)
        {
            AracResim? aracResim = await _aracResimDal.Get(x => x.Id == id);
            if (aracResim == null || aracResim.ImagePath == null)
            {
                return new ErrorDataResult<AracResim>("Resim bulunamadı!");
            }
            _fileHelper.Delete(PathConstants.ImagePaths + aracResim.ImagePath);
            await _aracResimDal.Delete(aracResim);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<AracResim>>> GetAll(Expression<Func<AracResim, bool>>? expr = null)
        {
            return new SuccessDataResult<List<AracResim>>(await _aracResimDal.GetAll());
        }

        public async Task<IDataResult<List<AracResim>>> GetByAracId(int aracId)
        {
            return new SuccessDataResult<List<AracResim>>(await _aracResimDal.GetAll(x=>x.AracId == aracId));
        }

        public async Task<IDataResult<AracResim>> GetById(int id)
        {
            AracResim? aracResim = await _aracResimDal.Get(x=>x.Id == id);
            if (aracResim == null)
            {
                return new ErrorDataResult<AracResim>("Resim bulunamadı!");
            }
            return new SuccessDataResult<AracResim>(aracResim);
        }

        public async Task<IResult> Update(IFormFile file, int id)
        {
            AracResim? aracResim = await _aracResimDal.Get(x => x.Id == id);
            if (aracResim == null || aracResim.ImagePath == null)
            {
                return new ErrorDataResult<AracResim>("Resim bulunamadı!");
            }
            string? filePath = await _fileHelper.Update(file, aracResim.ImagePath, PathConstants.ImagePaths);
            if (filePath == null)
            {
                return new ErrorResult("Güncelleme başarısız.");
            }
            aracResim.ImagePath = filePath;
            await _aracResimDal.Update(aracResim);
            return new SuccessResult();
        }
    }
}
