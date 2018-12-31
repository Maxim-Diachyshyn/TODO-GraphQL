using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Persistence.Notification.Contexts.Film;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.Subscriptions
{
    public class Subscription : ObjectGraphType
    {
        public Subscription(FilmAddedHandler filmAddedHandler, FilmUpdatedHandler filmUpdatedHandler, FilmRemovedHandler filmRemovedHandler)
        {
            AddField(new EventStreamFieldType
            {
                Name = "filmAdded",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<FilmModel>(ctx => ctx.Source as FilmModel),
                Subscriber = new EventStreamResolver<FilmModel>(ctx => filmAddedHandler.Observable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "filmUpdated",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<FilmModel>(ctx => ctx.Source as FilmModel),
                Subscriber = new EventStreamResolver<FilmModel>(ctx => filmUpdatedHandler.Observable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "filmRemoved",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<FilmModel>(ctx => ctx.Source as FilmModel),
                Subscriber = new EventStreamResolver<FilmModel>(ctx => filmRemovedHandler.Observable())
            });
        }
    }   
}