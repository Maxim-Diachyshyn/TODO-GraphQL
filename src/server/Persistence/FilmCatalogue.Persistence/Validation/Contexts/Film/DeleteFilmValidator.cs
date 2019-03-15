using FilmCatalogue.Domain.UseCases.Films.Commands;
using FluentValidation;

namespace FilmCatalogue.Persistence.Validation.Contexts.Films
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