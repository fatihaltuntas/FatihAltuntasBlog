using FatihAltuntas.Data.Abstract;
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
    public class CommentManager : ICommentManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<int>> Count()
        {
            var count = await _unitOfWork.Comments.CountAsync();
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
            var count = await _unitOfWork.Comments.CountAsync(x=> !x.IsDeleted);
            if (count > -1)
            {
                return new DataResult<int>(ResultStatus.Success, count);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, -1, $"Beklenmeyen bir hata ile karşılaşıldı.");
            }
        }
    }
}
