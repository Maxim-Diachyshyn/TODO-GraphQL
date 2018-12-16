using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.DTO;
using FilmCatalogue.Domain.Repositories.Film.Commands;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using FilmCatalogue.Persistence.EntityFramework.DTO;
using FilmCatalogue.Persistence.EntityFramework.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Commands
{
    public class AddFilmHandler : IRequestHandler<AddFilm, IIdAccessor>
    {
        private readonly FilmDbContext _context;
        private readonly IProjection<FilmEntity, IdAccessor> _projection;

        public AddFilmHandler(FilmDbContext context, IProjection<FilmEntity, IdAccessor> projection)
        {
            _context = context;
            _projection = projection;
        }

        public async Task<IIdAccessor> Handle(AddFilm command, CancellationToken cancellationToken)
        {
            var entry = _context.Add(
                new FilmEntity
                {
                    Name = command.Name,
                    ShowedDate = command.ShowedDate,
                    AddedAt = command.AddedAt,
                    ProfileViews = 0
                }
            );
            await _context.SaveChangesAsync(cancellationToken);

            return _projection.GetExpression().Compile()(entry.Entity);
        }
    }
}
