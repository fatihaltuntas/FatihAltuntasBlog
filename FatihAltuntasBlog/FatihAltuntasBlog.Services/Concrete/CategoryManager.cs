using AutoMapper;
using FatihAltuntas.Data.Abstract;
using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Entities.Dtos;
using FatihAltuntasBlog.Services.Abstract;
using FatihAltuntasBlog.Services.Utilities;
using FatihAltuntasBlog.Shared.Utilities.Results.Abstract;
using FatihAltuntasBlog.Shared.Utilities.Results.ComplexTypes;
using FatihAltuntasBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Services.Concrete
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdUserName)
        {
            var categoryEntity = _mapper.Map<Category>(categoryAddDto);
            categoryEntity.CreatedByName = createdUserName;
            categoryEntity.ModifiedByName = createdUserName;
            var addedCategoryEntity = await _unitOfWork.Categories.AddAsync(categoryEntity);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto()
            {
                Category = addedCategoryEntity,
                Message = Messages.Category.Add(categoryAddDto.Name),
                ResultStatus = ResultStatus.Success
            }, Messages.Category.Add(categoryAddDto.Name));
        }

        public async Task<IDataResult<int>> Count()
        {
            var count = await _unitOfWork.Categories.CountAsync();
            if (count > -1)
            {
                return new DataResult<int>(ResultStatus.Success, count);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, -1, $"Beklenmeyen bir hata ile karşılaşıldı.");
            }
        }

        public async Task<IDataResult<int>> CountByIsNotDeleted()
        {
            var count = await _unitOfWork.Articles.CountAsync(x => !x.IsDeleted);
            if (count > -1)
            {
                return new DataResult<int>(ResultStatus.Success, count);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, -1, $"Beklenmeyen bir hata ile karşılaşıldı.");
            }
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var categoryEntity = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (categoryEntity != null)
            {
                categoryEntity.IsDeleted = true;
                categoryEntity.ModifiedDate = DateTime.Now;
                categoryEntity.ModifiedByName = modifiedByName;


                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(categoryEntity);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto()
                {
                    Category = deletedCategory,
                    Message = Messages.Category.Delete(categoryEntity.Name),
                    ResultStatus = ResultStatus.Success
                }, Messages.Category.Delete(categoryEntity.Name));
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, new CategoryDto()
            {
                Category = null,
                Message = Messages.Category.NotFound(isOne: true),
                ResultStatus = ResultStatus.Error
            }, Messages.Category.NotFound(isOne: true));
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
            return new DataResult<CategoryDto>(ResultStatus.Error, new CategoryDto()
            {
                Category = null,
                Message = Messages.Category.NotFound(isOne: true),
                ResultStatus = ResultStatus.Error
            }, Messages.Category.NotFound(isOne: true));
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
            return new DataResult<CategoryListDto>(ResultStatus.Error, new CategoryListDto() { Categories = null, ResultStatus = ResultStatus.Error, Message = Messages.Category.NotFound(isOne: false) }, Messages.Category.NotFound(isOne: false));
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
            return new DataResult<CategoryListDto>(ResultStatus.Error, new CategoryListDto() { Categories = null, ResultStatus = ResultStatus.Error, Message = Messages.Category.NotFound(isOne: false) }, Messages.Category.NotFound(isOne: false));
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var isHave = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (isHave)
            {
                var categoryEntity = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(categoryEntity);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            return new DataResult<CategoryUpdateDto>(ResultStatus.Error, null, Messages.Category.NotFound(isOne: true));
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var categoryEntity = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (categoryEntity != null)
            {
                await _unitOfWork.Categories.DeleteAsync(categoryEntity);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Category.HardDelete(categoryEntity.Name));
            }
            return new Result(ResultStatus.Error, Messages.Category.NotFound(isOne: true));
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string updatedUserName)
        {
            var oldCategoryEntity = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var categoryEntity = _mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategoryEntity);
            categoryEntity.ModifiedByName = updatedUserName;

            if (categoryEntity != null)
            {
                var addedCategoryEntity = await _unitOfWork.Categories.UpdateAsync(categoryEntity);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto()
                {
                    Category = addedCategoryEntity,
                    Message = Messages.Category.Update(categoryUpdateDto.Name),
                    ResultStatus = ResultStatus.Success
                }, Messages.Category.Update(categoryUpdateDto.Name));
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, null, Messages.Category.NotFound(isOne: true));
        }
    }
}
