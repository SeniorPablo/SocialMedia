﻿using Microsoft.Extensions.Options;
using SocialMedia.Api.Core.Custom;
using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Exceptions;
using SocialMedia.Api.Core.Interfaces;
using SocialMedia.Api.Core.Interfaces.Services;
using SocialMedia.Api.Core.QueryFilters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var posts = _unitOfWork.PostRepository.GetAll();

            if(filters.UserId != null)
            {
                posts = posts.Where(p => p.UserId == filters.UserId);
            }

            if (filters.Date != null)
            {
                posts = posts.Where(p => p.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }

            if (filters.Description != null)
            {
                posts = posts.Where(p => p.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            var pagedPosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task AddPost(Post entity)
        {
            var user = await _unitOfWork.UserRepository.GetById(entity.UserId);
            if(user == null)
            {
                throw new BussinesException("El usuario no existe");
            }

            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(entity.UserId);
            if(userPost.Count() < 10)
            {
                var lastPost = userPost.OrderByDescending(u => u.Date).FirstOrDefault();
                if((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BussinesException("No estás habilitado para publicar");
                }
            }

            if(entity.Description.Contains("Sexo"))
            {
                throw new BussinesException("Contenido no permitido");
            }
            await _unitOfWork.PostRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post entity)
        {
            var post = await _unitOfWork.PostRepository.GetById(entity.Id);
            post.ImageUrl = entity.ImageUrl;
            post.Description = entity.Description;
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
