using FilmCatalogue.Domain.UseCases.Film.Commands.AddFilm;
using FluentValidation;

namespace FilmCatalogue.Persistence.Validation.Contexts.Film
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