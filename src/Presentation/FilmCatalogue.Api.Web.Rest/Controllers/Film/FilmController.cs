using FilmCatalogue.Domain.Contexts.Film.Commands;
using FilmCatalogue.Domain.Contexts.Film.Models;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.Repositories.Film.Commands;
using FilmCatalogue.Domain.Repositories.Film.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmCatalogue.Api.Web.Rest.Controllers.Film
{
    [FilmRoute]
    [ApiController]
    public class FilmController : Controller
    {
        private readonly IMediator _mediator;

        public FilmController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<FilmModel>> GetListAsync()
        {
            return await _mediator.Send(
                new GetFilmList<FilmModel>()
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FilmModel>> GetByIdAsync(Guid id)
        {
            var films = await _mediator.Send(
                new GetFilmsByIds<FilmModel>
                {
                    FilmIds = new List<Id> { new Id(id) }
                }
            );
            if (!films.Any())
            {
                return NotFound();
            }
            return Ok(films.Single());
        }

        [HttpPost("{id:guid}")]
        public async Task<ActionResult<FilmModel>> CreateAsync(CreateModel model)
        {
            await _mediator.Send((AddFilm)model);
            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FilmModel>> UpdateAsync(UpdateModel model)
        {
            await _mediator.Send((UpdateFilm)model);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FilmModel>> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteFilm
            {
                FilmId = new Id(id)
            });
            return Ok();
        }
    }
}
