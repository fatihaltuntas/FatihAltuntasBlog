using FatihAltuntasBlog.Entities.Dtos;
using FatihAltuntasBlog.Shared.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName = "userImages");
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
