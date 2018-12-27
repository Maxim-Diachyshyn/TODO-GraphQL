using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.UseCases.Film.Commands;
using FilmCatalogue.Domain.UseCases.Film.Models;
using MediatR;

namespace FilmCatalogue.Persistence.Notification.Contexts.Film
{
    public class FilmAddedHandler : 
        IRequestHandler<AddFilmCommand, FilmModel>
    {
        private readonly ISubject<FilmModel> _filmStream;
        private readonly IRequestHandler<AddFilmCommand, FilmModel> _addFilmHandler;

        public FilmAddedHandler(ISubject<FilmModel> filmStream, IRequestHandler<AddFilmCommand, FilmModel> addFilmHandler)
        {
            _filmStream = filmStream;
            _addFilmHandler = addFilmHandler;
        }

        public async Task<FilmModel> Handle(AddFilmCommand request, CancellationToken cancellationToken)
        {
            var newFilm = await _addFilmHandler.Handle(request, cancellationToken);            
            _filmStream.OnNext(newFilm);            
            return newFilm;
        }

        public IObservable<FilmModel> Observable()
        {
            return _filmStream.AsObservable();
        }
    }
}