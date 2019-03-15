using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Application.UseCases.Films.Requests;
using FilmCatalogue.Domain.DataTypes.Films;
using MediatR;
using MediatR.Pipeline;

namespace FilmCatalogue.Persistence.Notification.Contexts.Films
{
    public class FilmRemovedHandler : IPipelineBehavior<DeleteFilmCommand, Unit>
    {
        private readonly ISubject<Film> _filmStream;
        private readonly IMediator _mediator;

        public FilmRemovedHandler(ISubject<Film> filmStream, IMediator mediator)
        {
            _filmStream = filmStream;
            _mediator = mediator;
        }

        public IObservable<Film> Observable()
        {
            return _filmStream.AsObservable();
        }

        public async Task<Unit> Handle(DeleteFilmCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            var films = await _mediator.Send(
                new GetFilmListRequest(request.FilmId)
            );
            var result = await next();
            _filmStream.OnNext(films.Single());            
            return result;
        }
    }
}