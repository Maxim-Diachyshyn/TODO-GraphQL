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
    public class FilmUpdatedHandler : IObservable<FilmViewModel>, IRequestPostProcessor<UpdateFilmCommand, Film>
    {
        private readonly ISubject<FilmViewModel> _filmStream;

        public FilmUpdatedHandler(ISubject<FilmViewModel> filmStream)
        {
            _filmStream = filmStream;
        }

        public Task Process(UpdateFilmCommand command, Film response)
        {
            _filmStream.OnNext(new FilmViewModel(response));            
            return Task.CompletedTask;
        }

        public IDisposable Subscribe(IObserver<FilmViewModel> observer)
        {
            return _filmStream.Subscribe(observer);
        }
    }
}