using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Application.UseCases.Films.Requests;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Requests
{
    public class GetFilmByIdHandler : IRequestHandler<GetFilmByIdRequest, Film>
    {
        private readonly IQueryBuilder<IFilmRequest, FilmEntity> _builder;

        public GetFilmByIdHandler(IQueryBuilder<IFilmRequest, FilmEntity> builder)
        {
            _builder = builder;
        }

        public async Task<Film> Handle(GetFilmByIdRequest request, CancellationToken cancellationToken)
        {
            var filmEntity = await _builder.Build(request).SingleAsync();
            return filmEntity.ToModel();
        }
    }
}