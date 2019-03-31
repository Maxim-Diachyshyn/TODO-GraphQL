using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Application.UseCases.Films.Requests;
using FilmCatalogue.Application.UseCases.Reviews.Commands;
using FilmCatalogue.Domain.DataTypes.Reviews;
using MediatR;
using MediatR.Pipeline;

namespace FilmCatalogue.Api.Common.Contexts.Reviews.NotificationHandlers
{
    public class ReviewAddedHandler : IObservable<ReviewViewModel>, IRequestPostProcessor<AddReviewCommand, Review>
    {
        private readonly ISubject<ReviewViewModel> _reviewStream;
        private readonly IMediator _mediator;

        public ReviewAddedHandler(ISubject<ReviewViewModel> reviewStream, IMediator mediator)
        {
            _reviewStream = reviewStream;
            _mediator = mediator;
        }

        public IObservable<ReviewViewModel> Observable()
        {
            return _reviewStream.AsObservable();
        }

        public async Task Process(AddReviewCommand command, Review response)
        {
            var film = await _mediator.Send(new GetFilmByIdRequest(command.FilmId));
            _reviewStream.OnNext(new ReviewViewModel(response, new FilmViewModel(film)));            
        }

        public IDisposable Subscribe(IObserver<ReviewViewModel> observer)
        {
            return _reviewStream.Subscribe(observer);
        }
    }
}