using FatihAltuntas.Data.Abstract;
using FatihAltuntas.Data.Concrete;
using FatihAltuntas.Data.Concrete.EntityFramework.Contexts;
using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Services.Abstract;
using FatihAltuntasBlog.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyService(this IServiceCollection serviceCollection,string connectionString)
        {
            serviceCollection.AddDbContext<FatihAltuntasBlogContext>(option => option.UseSqlServer(connectionString));
            serviceCollection.AddIdentity<User, Role>(opt=> {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 5;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;

                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<FatihAltuntasBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            return serviceCollection;
        }
    }
}
