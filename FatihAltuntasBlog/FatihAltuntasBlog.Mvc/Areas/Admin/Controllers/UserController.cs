using AutoMapper;
using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Entities.Dtos;
using FatihAltuntasBlog.Mvc.Areas.Admin.Models;
using FatihAltuntasBlog.Shared.Utilities.Extensions;
using FatihAltuntasBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public UserController(UserManager<User> userManager, IWebHostEnvironment env, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            ImageDelete("fatihaltuntas1-388-16-28-22-17-9-2021.png");
            return View(new UserListDto()
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View("UserLogin");
        }
        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Eposta adresiniz yada parolanız yanlış");
                        return View("UserLogin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eposta adresiniz yada parolanız yanlış");
                    return View("UserLogin");
                }
            }
            else
            {
                return View("UserLogin");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto()
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            }, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }
        [Authorize]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto.UserName, userAddDto.PictureFile);
                var user = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto()
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{user.UserName} adlı kullanıcı adına sahip, kullanıcı başarıyla kaydedildi.",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxModel);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                {
                    UserAddDto = userAddDto,
                    UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                });

            }
            var userAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
            });
            return Json(userAddAjaxModelStateErrorModel);
        }
        [Authorize(Roles = "Admin,Editor")]
        public async Task<string> ImageUpload(string userName, IFormFile pictureFile)
        {
            string wwwroot = _env.WebRootPath;
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime datetime = DateTime.Now;
            string fileName = $"{userName}-{datetime.FullDateTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return fileName;
        }
        [Authorize]
        [Authorize(Roles = "Admin,Editor")]
        public bool ImageDelete(string fileName)
        {
            string wwwroot = _env.WebRootPath;
            var fileToDeleteUrl = Path.Combine($"{wwwroot}/img", fileName);
            if (System.IO.File.Exists(fileToDeleteUrl))
            {
                System.IO.File.Delete(fileToDeleteUrl);
                return true;
            }
            else
                return false;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                bool isUploadedNewPicture = false;
                var oldUser = await _userManager.FindByIdAsync(dto.Id.ToString());
                var oldUserImage = oldUser.Picture;

                if (dto.PictureFile != null)
                {
                    dto.Picture = await ImageUpload(dto.UserName, dto.PictureFile);
                    isUploadedNewPicture = true;
                }

                var updatedUser = _mapper.Map<UserUpdateDto, User>(dto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);

                if (result.Succeeded)
                {
                    if (isUploadedNewPicture)
                        this.ImageDelete(oldUserImage);

                    var userUpdateAjaxModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto = new UserDto()
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{dto.UserName} adlı kullanıcı başarıyla güncellendi",
                            User = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", dto)
                    });
                    return Json(userUpdateAjaxModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var userUpdateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto = dto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", dto)
                    });
                    return Json(userUpdateErrorViewModel);
                }
            }
            else
            {
                var userUpdateModelStateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                {
                    UserUpdateDto = dto,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", dto)
                });
                return Json(userUpdateModelStateErrorViewModel);
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto()
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı başarıyla silinmiştir.",
                    User = user
                });
                return Json(deletedUser);
            }
            string errorMessages = "";
            foreach (var error in result.Errors)
            {
                errorMessages += $"*{error.Description}\n";
            }

            var deletedUserErrorModel = JsonSerializer.Serialize(new UserDto
            {
                ResultStatus = ResultStatus.Error,
                Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı silirinken bazı hatalar oluştu.\n{errorMessages}",
                User = user
            });
            return Json(deletedUserErrorModel);
        }
        [Authorize(Roles = "Admin")]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
