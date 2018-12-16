using FilmCatalogue.Domain.Contexts.Time.Requests;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.Time.Requests
{
    public class GetCurrentTimeHandler : IRequestHandler<GetCurrentTime, DateTime>
    {
        public Task<DateTime> Handle(GetCurrentTime request, CancellationToken cancellationToken)
        {
            return Task.FromResult(DateTime.UtcNow);
        }
    }
}
