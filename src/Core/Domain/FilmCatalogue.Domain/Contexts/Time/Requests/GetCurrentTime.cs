using MediatR;
using System;

namespace FilmCatalogue.Domain.Contexts.Time.Requests
{
    public class GetCurrentTime : IRequest<DateTime>
    {
    }
}
