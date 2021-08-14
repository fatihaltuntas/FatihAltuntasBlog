﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntas.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IArticleRepository Articles { get; } //unitOfWork.Articles
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IRoleRepository Roles { get; }
        IUserRepository User { get; }
        //_UbitOfWork.Categories.AddAsync(category);
        //_unitOfWork.Users.AddAsync(user)
        //_unitOfWork.SaveAsync()

        Task<int> SaveAsync();
    }
}