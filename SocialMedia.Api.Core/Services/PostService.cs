using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Exceptions;
using SocialMedia.Api.Core.Interfaces;
using SocialMedia.Api.Core.Interfaces.Services;
using SocialMedia.Api.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Post> GetPosts(PostQueryFilter filters)
        {
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
            return posts;
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
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdatePost(Post entity)
        {
            _unitOfWork.PostRepository.Update(entity);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
