using System;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Domain.DataTypes.Common;

namespace FilmCatalogue.Api.Common.Contexts.Films.Inputs
{
    public class AddFilmInput
    {
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public string Photo { get; set; }

        public AddFilmCommand ToCommand()
        {
            return new AddFilmCommand
            {
                Name = Name,
                ShowedDate = ShowedDate,
                Photo = !string.IsNullOrEmpty(Photo)
                    ? new Blob(Photo)
                    : null
            };
        }
    }
}