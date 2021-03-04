using Microsoft.EntityFrameworkCore;
using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Interfaces.Repository;
using SocialMedia.Api.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContext context) : base(context) { }
        public async Task<IEnumerable<Post>> GetPostsByUser(int id)
        {
            return await _entities.Where(p => p.UserId == id).ToListAsync();
        }
    }
}
