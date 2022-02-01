using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Mvc.Areas.Admin.Models;
using FatihAltuntasBlog.Services.Abstract;
using FatihAltuntasBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IArticleManager _articleManager;
        private readonly ICommentManager _commentManager;
        public HomeController(UserManager<User> userManager, ICategoryManager categoryManager, IArticleManager articleManager, ICommentManager commentManager)
        {
            _userManager = userManager;
            _categoryManager = categoryManager;
            _articleManager = articleManager;
            _commentManager = commentManager;
        }



        public async Task<IActionResult> Index()
        {
            var userCountResult = await _userManager.Users.CountAsync();
            var categoryCountResult = await _categoryManager.CountByIsNotDeleted();
            var articleCountResult = await _articleManager.CountByIsNotDeleted();
            var commentCountResult = await _commentManager.CountByIsNotDeleted();
            var articles = await _articleManager.GetAll();
            if(userCountResult > -1 && categoryCountResult.ResultStatus == ResultStatus.Success && articleCountResult.ResultStatus == ResultStatus.Success && commentCountResult.ResultStatus == ResultStatus.Success && articles.ResultStatus == ResultStatus.Success)
            {
                return View(new DashboardViewModel()
                {
                    Articles = articles.Data,
                    ArticlesCount = articleCountResult.Data,
                    CategoriesCount = categoryCountResult.Data,
                    CommentsCount = commentCountResult.Data,
                    UsersCount = userCountResult
                });
            }
            return View();
        }
    }
}
