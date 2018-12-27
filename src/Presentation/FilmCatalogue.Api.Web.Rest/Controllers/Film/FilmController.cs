using FilmCatalogue.Domain;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Film.Commands;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Domain.UseCases.Film.Requests;
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
                new GetFilmList()
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FilmModel>> GetByIdAsync(Guid id)
        {
            var film = await _mediator.Send(
                new GetFilm
                {
                    Id = id
                }
            );
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        [HttpPost("{id:guid}")]
        public async Task<ActionResult<FilmModel>> CreateAsync(CreateModel model)
        {
            await _mediator.Send((AddFilmCommand)model);
            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FilmModel>> UpdateAsync(UpdateModel model)
        {
            await _mediator.Send((UpdateFilmCommand)model);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FilmModel>> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteFilmCommand
            {
                FilmId = new Id(id)
            });
            return Ok();
        }
    }
}
