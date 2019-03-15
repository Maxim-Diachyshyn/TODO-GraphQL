using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Application.UseCases.Films.Requests;
using FilmCatalogue.Application.UseCases.Reviews.Commands;
using FilmCatalogue.Application.UseCases.Reviews.Requests;
using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Reviews;
using MediatR;
using MediatR.Pipeline;

namespace FilmCatalogue.Api.Common.Contexts.Films.NotificationHandlers
{
    public class ReviewAddedHandler : IRequestPostProcessor<AddReviewCommand, Review>
    {
        private readonly ISubject<FilmViewModel> _filmStream;
        private readonly IMediator _mediator;

        public ReviewAddedHandler(ISubject<FilmViewModel> filmStream, IMediator mediator)
        {
            _filmStream = filmStream;
            _mediator = mediator;
        }

        public async Task Process(AddReviewCommand request, Review response)
        {
            var films = await _mediator.Send(new GetFilmListRequest(request.FilmId));
            var film = films.Single();
            var filmViewModel = new FilmViewModel(film);
            var reviews = await _mediator.Send(new GetReviewsRequest(request.FilmId));
            filmViewModel.SetReviews(reviews.Select(x => new ReviewViewModel(x)));
            _filmStream.OnNext(filmViewModel);
        }
    }
}