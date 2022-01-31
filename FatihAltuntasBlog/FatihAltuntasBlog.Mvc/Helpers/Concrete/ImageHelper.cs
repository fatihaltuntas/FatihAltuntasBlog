using FatihAltuntasBlog.Entities.Dtos;
using FatihAltuntasBlog.Mvc.Helpers.Abstract;
using FatihAltuntasBlog.Shared.Utilities.Extensions;
using FatihAltuntasBlog.Shared.Utilities.Results.Abstract;
using FatihAltuntasBlog.Shared.Utilities.Results.ComplexTypes;
using FatihAltuntasBlog.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Mvc.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string imgFolder = "img";
        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public IDataResult<ImageDeletedDto> Delete(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}/", pictureName);
            if (File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto()
                {
                    Extension = fileInfo.Extension,
                    FullName = pictureName,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Success, imageDeletedDto);
            }
            else
                return new DataResult<ImageDeletedDto>(ResultStatus.Error,null,"Böyle bir görsel bulunmamaktadır");
            
        }

        public async Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName = "userImages")
        {
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime datetime = DateTime.Now;
            string newFileName = $"{userName}-{datetime.FullDateTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return new DataResult<ImageUploadedDto>(ResultStatus.Success, new ImageUploadedDto()
            {
                FullName = $"{folderName}/{newFileName}",
                OldName = oldFileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = pictureFile.Length
            });
        }
    }
}
