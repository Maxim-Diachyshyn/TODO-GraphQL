using System;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Application.UseCases.Reviews.Commands;
using FilmCatalogue.Domain.DataTypes.Reviews;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Commands
{
    public class AddReviewHandler : IRequestHandler<AddReviewCommand, Review>
    {
        private readonly FilmDbContext _context;

        public AddReviewHandler(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<Review> Handle(AddReviewCommand command, CancellationToken cancellationToken)
        {
            var entity = new ReviewEntity
            {
                FilmId = command.FilmId,
                Comment = command.Comment,
                AddedAt = DateTime.UtcNow,
                Rating = command.Rate
            };
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }
    }
}