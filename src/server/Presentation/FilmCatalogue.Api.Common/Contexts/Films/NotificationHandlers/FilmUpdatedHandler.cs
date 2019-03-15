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
    public class FilmUpdatedHandler : IRequestPostProcessor<UpdateFilmCommand, Film>
    {
        private readonly ISubject<FilmViewModel> _filmStream;

        public FilmUpdatedHandler(ISubject<FilmViewModel> filmStream)
        {
            _filmStream = filmStream;
        }

        public IObservable<FilmViewModel> Observable()
        {
            return _filmStream.AsObservable();
        }

        public Task Process(UpdateFilmCommand command, Film response)
        {
            _filmStream.OnNext(new FilmViewModel(response));            
            return Task.CompletedTask;
        }
    }
}