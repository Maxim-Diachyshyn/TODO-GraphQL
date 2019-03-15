using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Application.UseCases.Films.Commands;
using MediatR.Pipeline;

namespace FilmCatalogue.Persistence.Notification.Contexts.Films
{
    public class FilmUpdatedHandler : IRequestPostProcessor<UpdateFilmCommand, Film>
    {
        private readonly ISubject<Film> _filmStream;

        public FilmUpdatedHandler(ISubject<Film> filmStream)
        {
            _filmStream = filmStream;
        }

        public IObservable<Film> Observable()
        {
            return _filmStream.AsObservable();
        }

        public Task Process(UpdateFilmCommand command, Film response)
        {
            _filmStream.OnNext(response);            
            return Task.CompletedTask;
        }
    }
}