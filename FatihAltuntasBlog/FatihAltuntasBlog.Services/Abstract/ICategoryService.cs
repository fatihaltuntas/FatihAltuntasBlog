using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Entities.Dtos;
using FatihAltuntasBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> Get(int categoryId);
        Task<IDataResult<CategoryListDto>> GetAll();
        Task<IResult> Add(CategoryAddDto categoryAddDto, string createdUserName);
        Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string updatedUserName);
        Task<IResult> Delete(int categoryId, string modifiedByName);
        Task<IResult> HardDelete(int categoryId);
        Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();
    }
}
