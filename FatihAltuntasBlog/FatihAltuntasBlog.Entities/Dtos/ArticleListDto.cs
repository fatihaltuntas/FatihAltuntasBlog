using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Entities.Dtos
{
    public class ArticleListDto:DtoGetBase
    {
        public IList<Article> Articles { get; set; }
        //public override ResultStatus ResultStatus { get; set; } = ResultStatus.Success
    }
}
