using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Application.UseCases.Reviews.Commands;
using FilmCatalogue.Domain.DataTypes.Reviews;
using MediatR.Pipeline;

namespace FilmCatalogue.Api.Common.Contexts.Reviews.NotificationHandlers
{
    public class ReviewAddedHandler : IRequestPostProcessor<AddReviewCommand, Review>
    {
        private readonly ISubject<ReviewViewModel> _reviewStream;

        public ReviewAddedHandler(ISubject<ReviewViewModel> reviewStream)
        {
            _reviewStream = reviewStream;
        }

        public IObservable<ReviewViewModel> Observable()
        {
            return _reviewStream.AsObservable();
        }

        public Task Process(AddReviewCommand command, Review response)
        {
            _reviewStream.OnNext(new ReviewViewModel(response));            
            return Task.CompletedTask;
        }
    }
}