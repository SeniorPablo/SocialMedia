using SocialMedia.Api.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Api.Core.Interfaces.Repository
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}
