using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Api.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        ISecurityRepository SecurityRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
