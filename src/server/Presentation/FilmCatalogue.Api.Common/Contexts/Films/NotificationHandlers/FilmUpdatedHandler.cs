using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Application.UseCases.Films.Requests;
using FilmCatalogue.Application.UseCases.Reviews.Commands;
using FilmCatalogue.Application.UseCases.Reviews.Requests;
using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Domain.DataTypes.Reviews;
using MediatR;
using MediatR.Pipeline;

namespace FilmCatalogue.Api.Common.Contexts.Films.NotificationHandlers
{
    public class FilmUpdatedHandler : 
        IObservable<FilmViewModel>, 
        IRequestPostProcessor<UpdateFilmCommand, Film>,
        IRequestPostProcessor<AddReviewCommand, Review>
    {
        private readonly ISubject<FilmViewModel> _filmStream;
        private readonly IMediator _mediator;

        public FilmUpdatedHandler(ISubject<FilmViewModel> filmStream, IMediator mediator)
        {
            _filmStream = filmStream;
            _mediator = mediator;
        }

        public Task Process(UpdateFilmCommand command, Film response)
        {
            _filmStream.OnNext(new FilmViewModel(response));            
            return Task.CompletedTask;
        }

        public async Task Process(AddReviewCommand request, Review response)
        {
            var film = await _mediator.Send(new GetFilmByIdRequest(request.FilmId));
            var filmViewModel = new FilmViewModel(film);
            var reviews = await _mediator.Send(new GetReviewsRequest(request.FilmId));
            filmViewModel.SetReviews(reviews.Select(x => new ReviewViewModel(x, filmViewModel)));
            _filmStream.OnNext(filmViewModel);
        }

        public IDisposable Subscribe(IObserver<FilmViewModel> observer)
        {
            return _filmStream.Subscribe(observer);
        }

        public IObservable<FilmViewModel> ById(Id id)
        {
            return _filmStream.Where(x => x.Id == id);
        }
    }
}