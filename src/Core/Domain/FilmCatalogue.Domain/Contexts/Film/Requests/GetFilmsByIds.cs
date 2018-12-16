﻿using FilmCatalogue.Domain.DataTypes;
using MediatR;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.Repositories.Film.Requests
{
    public class GetFilmsByIds<T> : IRequest<IEnumerable<T>>
    {
        public IEnumerable<Id> FilmIds { get; set; }
    }
}
