using FilmCatalogue.Domain.UseCases.Film.Commands;
using FluentValidation;

namespace FilmCatalogue.Persistence.Validation.Contexts.Film
{
    public class DeleteFilmValidator : AbstractValidator<DeleteFilmCommand>
    {
        public DeleteFilmValidator()
        {
            RuleFor(x => x.FilmId)
                .NotEmpty();
        }
    }
}