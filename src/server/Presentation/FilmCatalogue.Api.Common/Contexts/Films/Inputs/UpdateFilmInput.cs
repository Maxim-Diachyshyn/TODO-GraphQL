using System;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Domain.DataTypes.Common;

namespace FilmCatalogue.Api.Common.Contexts.Films.Inputs
{
    public class UpdateFilmInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public string Photo { get; set; }

        public UpdateFilmCommand ToCommand()
        {
            return new UpdateFilmCommand
            {
                FilmId = Id,
                Name = Name,
                ShowedDate = ShowedDate,
                Photo = !string.IsNullOrEmpty(Photo)
                    ? new Blob(Photo)
                    : null
            };
        }
    }
}