using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.UseCases.Film.Commands;
using FilmCatalogue.Domain.UseCases.Film.Models;
using MediatR;
using MediatR.Pipeline;

namespace FilmCatalogue.Persistence.Notification.Contexts.Film
{
    public class FilmAddedHandler : IRequestPostProcessor<AddFilmCommand, FilmModel>
    {
        private readonly ISubject<FilmModel> _filmStream;

        public FilmAddedHandler(ISubject<FilmModel> filmStream)
        {
            _filmStream = filmStream;
        }

        public IObservable<FilmModel> Observable()
        {
            return _filmStream.AsObservable();
        }

        public Task Process(AddFilmCommand command, FilmModel response)
        {
            _filmStream.OnNext(response);            
            return Task.CompletedTask;
        }
    }
}