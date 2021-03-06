using FatihAltuntas.Data.Abstract;
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
    public class EFCommentRepository : EfEntityRepositoryBase<Comment>,ICommentRepository
    {
        public EFCommentRepository(DbContext context) : base(context) 
        {
        }
    }
}
