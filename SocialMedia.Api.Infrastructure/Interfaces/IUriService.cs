using SocialMedia.Api.Core.QueryFilters;
using System;

namespace SocialMedia.Api.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}
