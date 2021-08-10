using FatihAltuntas.Data.Abstract;
using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Shared.Data.Abstract;
using FatihAltuntasBlog.Shared.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntas.Data.Concrete.EntityFramework.Repositories
{
    public class EfArticleRepository : EfEntityRepositoryBase<Article>, IArticleRepository
    {
        public EfArticleRepository(DbContext context) : base(context)
        {
        }
    }
}
