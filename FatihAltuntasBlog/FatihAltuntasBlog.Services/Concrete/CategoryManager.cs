using AutoMapper;
using FatihAltuntas.Data.Abstract;
using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Entities.Dtos;
using FatihAltuntasBlog.Services.Abstract;
using FatihAltuntasBlog.Shared.Utilities.Results.Abstract;
using FatihAltuntasBlog.Shared.Utilities.Results.ComplexTypes;
using FatihAltuntasBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdUserName)
        {
            var categoryEntity = _mapper.Map<Category>(categoryAddDto);
            categoryEntity.CreatedByName = createdUserName;
            categoryEntity.ModifiedByName = createdUserName;
            await _unitOfWork.Categories.AddAsync(categoryEntity).ContinueWith(x => _unitOfWork.SaveAsync());

            return new Result(ResultStatus.Success, $"{categoryAddDto.Name} başarıyla oluşturuldu");
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var categoryEntity = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (categoryEntity != null)
            {
                categoryEntity.IsDeleted = true;
                categoryEntity.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(categoryEntity).ContinueWith(x => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{categoryEntity.Name} başarıyla silindi");
            }
            return new Result(ResultStatus.Error, "Silme işlemi gerçekleştirilemedi");
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var categoryEntity = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (categoryEntity != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto()
                {
                    Category = categoryEntity,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, null, "Kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categoryListEntity = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categoryListEntity.Any())
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto()
                {
                    Categories = categoryListEntity,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, null, "Kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categoryEntityList = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categoryEntityList.Any())
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto()
                {
                    Categories = categoryEntityList,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, null, "Kategori bulunamadı");
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var categoryEntity = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (categoryEntity != null)
            {
                await _unitOfWork.Categories.DeleteAsync(categoryEntity);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "Kategori başarıyla silindi");
            }
            return new Result(ResultStatus.Error, "Kategori silinemedi");
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string updatedUserName)
        {
            var categoryEntity = _mapper.Map<Category>(categoryUpdateDto);
            categoryEntity.ModifiedByName = updatedUserName;

            if (categoryEntity != null)
            {
                await _unitOfWork.Categories.UpdateAsync(categoryEntity).ContinueWith(x => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellendi");
            }
            return new Result(ResultStatus.Error, "Kategori güncellenemedi");
        }
    }
}
