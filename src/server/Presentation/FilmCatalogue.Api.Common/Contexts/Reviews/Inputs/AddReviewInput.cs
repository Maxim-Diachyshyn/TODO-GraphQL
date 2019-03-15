using System;
using FilmCatalogue.Application.UseCases.Reviews.Commands;

namespace FilmCatalogue.Api.Common.Contexts.Reviews.Inputs
{
    public class AddReviewInput
    {
        public string FilmId { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }

        public AddReviewCommand ToCommand()
        {
            return new AddReviewCommand(
                filmId: Guid.Parse(FilmId),
                comment: Comment,
                rate: Rate
            );
        }
    }
}