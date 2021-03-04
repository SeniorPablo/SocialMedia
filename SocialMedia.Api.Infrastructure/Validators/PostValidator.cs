using FluentValidation;
using SocialMedia.Api.Core.DTOs;
using System;

namespace SocialMedia.Api.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(10, 500);

            RuleFor(post => post.Date)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}
