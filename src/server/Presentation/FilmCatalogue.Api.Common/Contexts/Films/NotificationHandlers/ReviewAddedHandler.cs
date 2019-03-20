using System;
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
    public class ReviewAddedHandler : IObservable<ReviewViewModel>, IRequestPostProcessor<AddReviewCommand, Review>
    {
        private readonly ISubject<FilmViewModel> _filmStream;
        private readonly ISubject<ReviewViewModel> _reviewStream;
        private readonly IMediator _mediator;

        public ReviewAddedHandler(ISubject<FilmViewModel> filmStream, ISubject<ReviewViewModel> reviewStream, IMediator mediator)
        {
            _filmStream = filmStream;
            _reviewStream = reviewStream;
            _mediator = mediator;
        }

        public async Task Process(AddReviewCommand request, Review response)
        {
            _reviewStream.OnNext(new ReviewViewModel(response));
            var films = await _mediator.Send(new GetFilmListRequest(request.FilmId));
            var film = films.Single();
            var filmViewModel = new FilmViewModel(film);
            var reviews = await _mediator.Send(new GetReviewsRequest(request.FilmId));
            filmViewModel.SetReviews(reviews.Select(x => new ReviewViewModel(x)));
            _filmStream.OnNext(filmViewModel);
        }

        public IDisposable Subscribe(IObserver<ReviewViewModel> observer)
        {
            return _reviewStream.Subscribe(observer);
        }
    }
}