using SocialMedia.Api.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Api.Core.Interfaces.Services
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security security);
    }
}
