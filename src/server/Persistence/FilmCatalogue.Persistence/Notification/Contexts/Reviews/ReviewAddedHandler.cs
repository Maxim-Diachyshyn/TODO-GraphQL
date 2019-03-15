using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FilmCatalogue.Domain.UseCases.Reviews.Commands;
using FilmCatalogue.Domain.UseCases.Reviews.Models;
using MediatR.Pipeline;

namespace FilmCatalogue.Persistence.Notification.Contexts.Reviews
{
    public class ReviewAddedHandler : IRequestPostProcessor<AddReviewCommand, Review>
    {
        private readonly ISubject<Review> _reviewStream;

        public ReviewAddedHandler(ISubject<Review> reviewStream)
        {
            _reviewStream = reviewStream;
        }

        public IObservable<Review> Observable()
        {
            return _reviewStream.AsObservable();
        }

        public Task Process(AddReviewCommand command, Review response)
        {
            _reviewStream.OnNext(response);            
            return Task.CompletedTask;
        }
    }
}