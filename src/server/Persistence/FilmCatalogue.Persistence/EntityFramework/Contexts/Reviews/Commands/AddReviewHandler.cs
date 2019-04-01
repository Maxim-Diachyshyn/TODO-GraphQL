using System;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Application.UseCases.Reviews.Commands;
using FilmCatalogue.Domain.DataTypes.Reviews;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Commands
{
    public class AddReviewHandler : IRequestHandler<AddReviewCommand, Review>
    {
        private readonly IUnitOfWork<ReviewEntity> _unitOfWork;

        public AddReviewHandler(IUnitOfWork<ReviewEntity> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> Handle(AddReviewCommand command, CancellationToken cancellationToken)
        {
            var entity = _unitOfWork.Add(new ReviewEntity
            {
                FilmId = command.FilmId,
                Comment = command.Comment,
                AddedAt = DateTime.UtcNow,
                Rating = command.Rate
            });
            await _unitOfWork.SaveChangesAsync();
            return entity.ToModel();
        }
    }
}