using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Commands
{
    public class UpdateFilmHandler : IRequestHandler<UpdateFilmCommand, Film>
    {
        private readonly IUnitOfWork<FilmEntity> _unitOfWork;

        public UpdateFilmHandler(IUnitOfWork<FilmEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Film> Handle(UpdateFilmCommand command, CancellationToken cancellationToken)
        {
             var filmEntity = _unitOfWork.Update(command.FilmId, e => 
            {
                e.Name = command.Name;
                e.ShowedDate = command.ShowedDate;
                e.Photo = command.Photo?.Data;
                e.PhotoType = command.Photo?.Type;
            });

            await _unitOfWork.SaveChangesAsync();

            return filmEntity.ToModel();
        }
    }
}
