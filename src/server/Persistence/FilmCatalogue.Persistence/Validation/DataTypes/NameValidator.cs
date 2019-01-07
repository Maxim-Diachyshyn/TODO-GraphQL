using FilmCatalogue.Domain.DataTypes;
using FluentValidation;

namespace FilmCatalogue.Persistence.Validation.DataTypes
{
    public class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}