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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Services.Concrete
{
    public class ArticleManager : IArticleManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var articleEntity = _mapper.Map<Article>(articleAddDto);
            articleEntity.CreatedByName = createdByName;
            articleEntity.ModifiedByName = createdByName;
            articleEntity.Id = 1;
            await _unitOfWork.Articles.AddAsync(articleEntity);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.Add(articleAddDto.Title));
        }

        public async Task<IDataResult<int>> Count()
        {
            var count = await _unitOfWork.Articles.CountAsync();
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

        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var articleEntity = await _unitOfWork.Articles.GetAsync(x => x.Id == articleId);
            if (articleEntity != null)
            {
                articleEntity.IsDeleted = true;
                articleEntity.ModifiedByName = modifiedByName;
                articleEntity.ModifiedDate = DateTime.Now;
                await _unitOfWork.Articles.UpdateAsync(articleEntity);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Article.Delete(articleEntity.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(true));
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var articleEntity = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId, a => a.User, a => a.Category);
            if (articleEntity != null)
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto()
                {
                    Article = articleEntity,
                    ResultStatus = ResultStatus.Success
                });
            return new DataResult<ArticleDto>(ResultStatus.Error, null, Messages.Article.NotFound(true));

        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articleEntityList = await _unitOfWork.Articles.GetAllAsync(null, a => a.User, a => a.Category);
            if (articleEntityList.Any())
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto()
                {
                    Articles = articleEntityList,
                    ResultStatus = ResultStatus.Success
                });
            return new DataResult<ArticleListDto>(ResultStatus.Error, null, Messages.Article.NotFound(false));
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var articleEntityList = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId, x => x.User, x => x.Category);
            if (articleEntityList.Any())
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto()
                {
                    Articles = articleEntityList,
                    ResultStatus = ResultStatus.Success
                });
            return new DataResult<ArticleListDto>(ResultStatus.Error, null, Messages.Article.NotFound(false));
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
        {
            var articleEntityList = await _unitOfWork.Articles.GetAllAsync(x => !x.IsDeleted, x => x.User, x => x.Category);
            if (articleEntityList.Any())
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto()
                {
                    Articles = articleEntityList,
                    ResultStatus = ResultStatus.Success
                });
            return new DataResult<ArticleListDto>(ResultStatus.Error, null, Messages.Article.NotFound(false));
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articleEntityList = await _unitOfWork.Articles.GetAllAsync(x => !x.IsDeleted && x.IsActive, x => x.User, x => x.Category);
            if (articleEntityList.Any())
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto()
                {
                    Articles = articleEntityList,
                    ResultStatus = ResultStatus.Success
                });
            return new DataResult<ArticleListDto>(ResultStatus.Error, null, Messages.Article.NotFound(false));
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var articleEntity = await _unitOfWork.Articles.GetAsync(x => x.Id == articleId);
            if (articleEntity != null)
            {
                await _unitOfWork.Articles.DeleteAsync(articleEntity); ;
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Article.HardDelete(articleEntity.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(true));
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var articleEntity = _mapper.Map<Article>(articleUpdateDto);
            articleEntity.ModifiedByName = modifiedByName;
            await _unitOfWork.Articles.UpdateAsync(articleEntity);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.Update(articleUpdateDto.Title));
        }
    }
}
