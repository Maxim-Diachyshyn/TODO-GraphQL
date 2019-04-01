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
    public class AddFilmHandler : IRequestHandler<AddFilmCommand, Film>
    {
        private readonly IUnitOfWork<FilmEntity> _unitOfWork;

        public AddFilmHandler(IUnitOfWork<FilmEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Film> Handle(AddFilmCommand command, CancellationToken cancellationToken)
        {
            var entity = _unitOfWork.Add(
                new FilmEntity
                {
                    Name = command.Name,
                    ShowedDate = command.ShowedDate,
                    Photo = command.Photo?.Data,
                    PhotoType = command.Photo?.Type
                }
            );
            await _unitOfWork.SaveChangesAsync();
            return entity.ToModel();
        }
    }
}
