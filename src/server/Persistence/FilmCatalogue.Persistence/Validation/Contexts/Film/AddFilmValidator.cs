using FilmCatalogue.Domain.UseCases.Films.Commands;
using FluentValidation;

namespace FilmCatalogue.Persistence.Validation.Contexts.Films
{
    public class AddFilmValidator : AbstractValidator<AddFilmCommand>
    {
        public AddFilmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.ShowedDate)
                .NotEmpty();
        }
    }
}