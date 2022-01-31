using FatihAltuntas.Data.Abstract;
using FatihAltuntas.Data.Concrete.EntityFramework.Contexts;
using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Shared.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntas.Data.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<Category> GetById(int categoryId)
        {
           return await FatihAltuntasBlogContext.Categories.SingleOrDefaultAsync(x => x.Id == categoryId);
        }
        private FatihAltuntasBlogContext FatihAltuntasBlogContext
        {
            get {
                return _context as FatihAltuntasBlogContext;
            }
        }
    }
}
