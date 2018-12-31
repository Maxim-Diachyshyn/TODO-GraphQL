using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FilmCatalogue.Domain.UseCases.Film.Commands.UpdateFilm;
using FilmCatalogue.Domain.UseCases.Film.Models;
using MediatR.Pipeline;

namespace FilmCatalogue.Persistence.Notification.Contexts.Film
{
    public class FilmUpdatedHandler : IRequestPostProcessor<UpdateFilmCommand, FilmModel>
    {
        private readonly ISubject<FilmModel> _filmStream;

        public FilmUpdatedHandler(ISubject<FilmModel> filmStream)
        {
            _filmStream = filmStream;
        }

        public IObservable<FilmModel> Observable()
        {
            return _filmStream.AsObservable();
        }

        public Task Process(UpdateFilmCommand command, FilmModel response)
        {
            _filmStream.OnNext(response);            
            return Task.CompletedTask;
        }
    }
}