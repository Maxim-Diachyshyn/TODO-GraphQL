using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Application.UseCases.Films.Requests;
using FilmCatalogue.Domain.DataTypes.Common;
using MediatR;

namespace FilmCatalogue.Api.Common.Contexts.Films.NotificationHandlers
{
    public class FilmRemovedHandler : IObservable<FilmViewModel>, IPipelineBehavior<DeleteFilmCommand, Unit>
    {
        private readonly ISubject<FilmViewModel> _filmStream;        
        private readonly IMediator _mediator;

        public FilmRemovedHandler(ISubject<FilmViewModel> filmStream, IMediator mediator)
        {
            _filmStream = filmStream;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteFilmCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            //TODO: this data should be loaded to command body
            var film = await _mediator.Send(
                new GetFilmByIdRequest(request.FilmId)
            );
            var result = await next();
            _filmStream.OnNext(new FilmViewModel(film));      
            return result;
        }

        public IDisposable Subscribe(IObserver<FilmViewModel> observer)
        {
            return _filmStream.Subscribe(observer);
        }

        public IObservable<FilmViewModel> ById(Id id)
        {
            return _filmStream.Where(x => x.Id == id);
        }
    }
}