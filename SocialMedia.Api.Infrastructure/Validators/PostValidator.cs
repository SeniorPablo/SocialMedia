using FluentValidation;
using SocialMedia.Api.Core.DTOs;

namespace SocialMedia.Api.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(10, 15);
        }
    }
}
