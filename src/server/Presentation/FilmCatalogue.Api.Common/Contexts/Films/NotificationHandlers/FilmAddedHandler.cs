using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Domain.DataTypes.Films;
using MediatR.Pipeline;

namespace FilmCatalogue.Api.Common.Contexts.Films.NotificationHandlers
{
    public class FilmAddedHandler : IObservable<FilmViewModel>, IRequestPostProcessor<AddFilmCommand, Film>
    {
        private readonly ISubject<FilmViewModel> _filmStream;

        public FilmAddedHandler(ISubject<FilmViewModel> filmStream)
        {
            _filmStream = filmStream;
        }

        public IDisposable Subscribe(IObserver<FilmViewModel> observer)
        {
            return _filmStream.Subscribe(observer);
        }

        public Task Process(AddFilmCommand command, Film response)
        {
            _filmStream.OnNext(new FilmViewModel(response));            
            return Task.CompletedTask;
        }
    }
}