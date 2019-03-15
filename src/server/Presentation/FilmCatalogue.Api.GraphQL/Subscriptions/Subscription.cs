using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Domain.UseCases.Films.Models;
using FilmCatalogue.Domain.UseCases.Reviews.Models;
using FilmCatalogue.Persistence.Notification.Contexts.Films;
using FilmCatalogue.Persistence.Notification.Contexts.Reviews;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.Subscriptions
{
    public class Subscription : ObjectGraphType
    {
        public Subscription(FilmAddedHandler filmAddedHandler, FilmUpdatedHandler filmUpdatedHandler, FilmRemovedHandler filmRemovedHandler, ReviewAddedHandler reviewAddedHandler)
        {
            AddField(new EventStreamFieldType
            {
                Name = "filmAdded",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<Film>(ctx => ctx.Source as Film),
                Subscriber = new EventStreamResolver<Film>(ctx => filmAddedHandler.Observable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "filmUpdated",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<Film>(ctx => ctx.Source as Film),
                Subscriber = new EventStreamResolver<Film>(ctx => filmUpdatedHandler.Observable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "filmDeleted",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<Film>(ctx => ctx.Source as Film),
                Subscriber = new EventStreamResolver<Film>(ctx => filmRemovedHandler.Observable())
            });

            AddField(new EventStreamFieldType
            {
                Name = "reviewAdded",
                Type = typeof(ReviewType),
                Resolver = new FuncFieldResolver<Review>(ctx => ctx.Source as Review),
                Subscriber = new EventStreamResolver<Review>(ctx => reviewAddedHandler.Observable())
            });
        }
    }   
}