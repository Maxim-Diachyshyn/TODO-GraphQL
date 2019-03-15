using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Domain.DataTypes.Films;
using MediatR;
using MediatR.Pipeline;

namespace FilmCatalogue.Persistence.Notification.Contexts.Films
{
    public class FilmAddedHandler : IRequestPostProcessor<AddFilmCommand, Film>
    {
        private readonly ISubject<Film> _filmStream;

        public FilmAddedHandler(ISubject<Film> filmStream)
        {
            _filmStream = filmStream;
        }

        public IObservable<Film> Observable()
        {
            return _filmStream.AsObservable();
        }

        public Task Process(AddFilmCommand command, Film response)
        {
            _filmStream.OnNext(response);            
            return Task.CompletedTask;
        }
    }
}