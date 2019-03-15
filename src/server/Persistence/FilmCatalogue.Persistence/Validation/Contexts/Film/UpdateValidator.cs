using FilmCatalogue.Domain.UseCases.Films.Commands;
using FluentValidation;

namespace FilmCatalogue.Persistence.Validation.Contexts.Films
{
    public class UpdateValidator : AbstractValidator<UpdateFilmCommand>
    {
        public UpdateValidator()
        {
            RuleFor(x => x.FilmId)
                .NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.ShowedDate)
                .NotEmpty();
        }
    }
}