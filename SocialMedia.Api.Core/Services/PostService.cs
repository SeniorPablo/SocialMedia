using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Interfaces.Repository;
using SocialMedia.Api.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task<Post> GetPost(int id)
        {
            return await _postRepository.GetPost(id);
        }

        public async Task AddPost(Post entity)
        {
            var user = await _userRepository.GetUser(entity.UserId);
            if(user == null)
            {
                throw new Exception("El usuario no existe");
            }
            if(entity.Description.Contains("Sexo"))
            {
                throw new Exception("Contenido no permitido");
            }
            await _postRepository.AddPost(entity);
        }

        public async Task<bool> UpdatePost(Post entity)
        {
            return await _postRepository.UpdatePost(entity);
        }

        public async Task<bool> DeletePost(int id)
        {
            return await _postRepository.DeletePost(id);
        }
    }
}
