using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntas.Data.Abstract
{
    public interface ICommentRepository:IEntityRepository<Comment>
    {
    }
}
