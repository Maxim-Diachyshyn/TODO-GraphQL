using FilmCatalogue.Domain.DataTypes;
using FluentValidation;
using MediatR;
using System;

namespace FilmCatalogue.Domain.UseCases.Film.Commands
{
    public class DeleteFilmCommand : IRequest
    {
        public Id FilmId { get; set; }
    }
}
