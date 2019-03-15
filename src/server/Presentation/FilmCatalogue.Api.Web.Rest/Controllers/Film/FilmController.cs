using FilmCatalogue.Api.Web.Rest.Controllers.Films.Commands.Create;
using FilmCatalogue.Api.Web.Rest.Controllers.Films.Commands.Update;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Application.UseCases.Films.Requests;
using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Films;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmCatalogue.Api.Web.Rest.Controllers.Films
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
        public async Task<IEnumerable<Film>> GetListAsync()
        {
            return await _mediator.Send(
                new GetFilmListRequest()
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Film>> GetByIdAsync(Guid id)
        {
            var films = await _mediator.Send(
                new GetFilmListRequest(new Id(id))
            );
            var film = films.SingleOrDefault();
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        [HttpPost]
        public async Task<ActionResult<Film>> CreateAsync(CreateModel model)
        {
            return Ok(
                await _mediator.Send(
                    new AddFilmCommand
                    {
                        Name = model.Name,
                        ShowedDate = model.ShowedDate
                    }
                )
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Film>> UpdateAsync(Guid id, UpdateModel model)
        {
            return Ok(
                await _mediator.Send(
                    new UpdateFilmCommand
                    {
                        FilmId = id,
                        Name = model.Name,
                        ShowedDate = model.ShowedDate
                    }
                )
            );
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Film>> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteFilmCommand
            {
                FilmId = new Id(id)
            });
            return Ok();
        }
    }
}
