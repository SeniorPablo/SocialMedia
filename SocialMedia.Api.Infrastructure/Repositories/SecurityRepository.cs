using Microsoft.EntityFrameworkCore;
using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Interfaces.Repository;
using SocialMedia.Api.Infrastructure.Data;
using System.Threading.Tasks;

namespace SocialMedia.Api.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext context) : base(context) { }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(s => s.User == login.User && s.Password == login.Password);
        }
    }
}
