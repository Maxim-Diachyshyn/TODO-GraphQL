using FilmCatalogue.Domain.DataTypes;
using FluentValidation;

namespace FilmCatalogue.Persistence.Validation.DataTypes
{
    public class PageModelValidator : AbstractValidator<PageModel>
    {
        public PageModelValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0u);
            RuleFor(x => x.Count)
                .GreaterThan(0u);
        }
    }
}