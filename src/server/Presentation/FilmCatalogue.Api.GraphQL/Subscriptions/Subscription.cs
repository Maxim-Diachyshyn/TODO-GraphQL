using FilmCatalogue.Api.Common.Contexts.Films.NotificationHandlers;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Reviews.NotificationHandlers;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Domain.DataTypes.Reviews;
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
                Resolver = new FuncFieldResolver<FilmViewModel>(ctx => ctx.Source as FilmViewModel),
                Subscriber = new EventStreamResolver<FilmViewModel>(ctx => filmAddedHandler.Observable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "filmUpdated",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<FilmViewModel>(ctx => ctx.Source as FilmViewModel),
                Subscriber = new EventStreamResolver<FilmViewModel>(ctx => filmUpdatedHandler.Observable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "filmDeleted",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<FilmViewModel>(ctx => ctx.Source as FilmViewModel),
                Subscriber = new EventStreamResolver<FilmViewModel>(ctx => filmRemovedHandler.Observable())
            });

            AddField(new EventStreamFieldType
            {
                Name = "reviewAdded",
                Type = typeof(ReviewType),
                Resolver = new FuncFieldResolver<ReviewViewModel>(ctx => ctx.Source as ReviewViewModel),
                Subscriber = new EventStreamResolver<ReviewViewModel>(ctx => reviewAddedHandler.Observable())
            });
        }
    }   
}