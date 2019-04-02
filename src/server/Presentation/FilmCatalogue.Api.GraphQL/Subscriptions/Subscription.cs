using System;
using System.Reactive.Linq;
using FilmCatalogue.Api.Common.Contexts.Films.NotificationHandlers;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Api.GraphQL.GraphTypes;
using GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace FilmCatalogue.Api.GraphQL.Subscriptions
{
    public class Subscription : ObjectGraphType
    {
        public Subscription(IHttpContextAccessor accessor)
        {
            AddField(new EventStreamFieldType
            {
                Name = "filmAdded",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<FilmViewModel>(ctx => ctx.Source as FilmViewModel),
                Subscriber = new EventStreamResolver<FilmViewModel>(ctx => accessor.GetService<FilmAddedHandler>().AsObservable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "filmUpdated",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<FilmViewModel>(ctx => ctx.Source as FilmViewModel),
                Subscriber = new EventStreamResolver<FilmViewModel>(ctx => accessor.GetService<FilmUpdatedHandler>().AsObservable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "filmDeleted",
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<FilmViewModel>(ctx => ctx.Source as FilmViewModel),
                Subscriber = new EventStreamResolver<FilmViewModel>(ctx => accessor.GetService<FilmRemovedHandler>().AsObservable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "filmDeletedById",
                Arguments = new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                Type = typeof(FilmType),
                Resolver = new FuncFieldResolver<FilmViewModel>(ctx => ctx.Source as FilmViewModel),
                Subscriber = new EventStreamResolver<FilmViewModel>(ctx => accessor.GetService<FilmRemovedHandler>().ById(ctx.GetArgument<Guid>("id")))
            });
        }
    }   
}