using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Commands
{
    public class DeleteFilmHandler : IRequestHandler<DeleteFilmCommand>
    {
        private readonly IUnitOfWork<FilmEntity> _unitOfWork;

        public DeleteFilmHandler(IUnitOfWork<FilmEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteFilmCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.Remove(request.FilmId);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
