using FilmCatalogue.Api.Web.Rest.Controllers.Film.Commands.Create;
using FilmCatalogue.Api.Web.Rest.Controllers.Film.Commands.Update;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Film.Commands.AddFilm;
using FilmCatalogue.Domain.UseCases.Film.Commands.DeleteFilm;
using FilmCatalogue.Domain.UseCases.Film.Commands.UpdateFilm;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Domain.UseCases.Film.Requests.GetFilmById;
using FilmCatalogue.Domain.UseCases.Film.Requests.GetFilmList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                new GetFilmListRequest()
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FilmModel>> GetByIdAsync(Guid id)
        {
            var film = await _mediator.Send(
                new GetFilmByIdRequest
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

        [HttpPost]
        public async Task<ActionResult<FilmModel>> CreateAsync(CreateModel model)
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
        public async Task<ActionResult<FilmModel>> UpdateAsync(Guid id, UpdateModel model)
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
