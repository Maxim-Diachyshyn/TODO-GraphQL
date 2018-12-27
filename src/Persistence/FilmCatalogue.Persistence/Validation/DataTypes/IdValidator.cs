using System;
using FilmCatalogue.Domain.DataTypes;
using FluentValidation;

namespace FilmCatalogue.Persistence.Validation.DataTypes
{
    public class IdValidator : AbstractValidator<Id>
    {
        public IdValidator()
        {
            RuleFor(x => (Guid)x)
                .NotEmpty();
        }
    }
}