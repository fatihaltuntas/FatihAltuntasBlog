using FatihAltuntasBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Services.Abstract
{
    public interface ICommentManager
    {
        Task<IDataResult<int>> Count();
        Task<IDataResult<int>> CountByIsNotDeleted();
    }
}
