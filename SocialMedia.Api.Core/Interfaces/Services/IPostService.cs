using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Core.Interfaces.Services
{
    public interface IPostService
    {
        IEnumerable<Post> GetPosts(PostQueryFilter filters);
        Task<Post> GetPost(int id);
        Task AddPost(Post entity);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}
