using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Interfaces;
using SocialMedia.Api.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitOfWork.PostRepository.GetAll();
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
                throw new Exception("El usuario no existe");
            }
            if(entity.Description.Contains("Sexo"))
            {
                throw new Exception("Contenido no permitido");
            }
            await _unitOfWork.PostRepository.Add(entity);
        }

        public async Task<bool> UpdatePost(Post entity)
        {
            await _unitOfWork.PostRepository.Update(entity);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
