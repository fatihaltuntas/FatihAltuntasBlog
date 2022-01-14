﻿using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FatihAltuntasBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using FatihAltuntasBlog.Shared.Utilities.Extensions;
using AutoMapper;
using System.Text.Json;
using FatihAltuntasBlog.Mvc.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;

namespace FatihAltuntasBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IWebHostEnvironment env, IMapper mapper)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
        }

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
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto() { 
                Users = users,
                ResultStatus = ResultStatus.Success
            },new JsonSerializerOptions {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto.UserName,userAddDto.PictureFile);
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
                var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel { 
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
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                bool isUploadedNewPicture = false;
                var oldUser = await _userManager.FindByIdAsync(dto.Id.ToString());
                var oldUserImage = oldUser.Picture;

                if(dto.PictureFile != null)
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

        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }

    }
}