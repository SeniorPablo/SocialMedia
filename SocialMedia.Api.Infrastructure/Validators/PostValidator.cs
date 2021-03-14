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
                .WithMessage("La descripción no puede ser nula");

            RuleFor(post => post.Description)
                .Length(10, 500)
                .WithMessage("La longitud de la descripción debe estar entre 10 y 500 caracteres");
                
            RuleFor(post => post.Date)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}
