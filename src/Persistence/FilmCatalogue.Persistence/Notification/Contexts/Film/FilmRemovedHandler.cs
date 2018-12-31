using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.UseCases.Film.Commands.DeleteFilm;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Domain.UseCases.Film.Requests.GetFilmById;
using MediatR;
using MediatR.Pipeline;

namespace FilmCatalogue.Persistence.Notification.Contexts.Film
{
    public class FilmRemovedHandler : IPipelineBehavior<DeleteFilmCommand, Unit>
    {
        private readonly ISubject<FilmModel> _filmStream;
        private readonly IMediator _mediator;

        public FilmRemovedHandler(ISubject<FilmModel> filmStream, IMediator mediator)
        {
            _filmStream = filmStream;
            _mediator = mediator;
        }

        public IObservable<FilmModel> Observable()
        {
            return _filmStream.AsObservable();
        }

        public async Task<Unit> Handle(DeleteFilmCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            var film = await _mediator.Send(
                new GetFilmByIdRequest
                {
                    Id = request.FilmId
                }
            );
            var result = await next();
            _filmStream.OnNext(film);            
            return result;
        }
    }
}