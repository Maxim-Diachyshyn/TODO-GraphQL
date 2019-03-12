using Autofac;
using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Api.GraphQL.Inputs;
using FilmCatalogue.Api.GraphQL.Mutations;
using FilmCatalogue.Api.GraphQL.Queries;
using FilmCatalogue.Api.GraphQL.Schemas;
using FilmCatalogue.Api.GraphQL.Subscriptions;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<IdGraphType>();

            builder.RegisterType<FilmType>();
            builder.RegisterType<AddFilmInput>();
            builder.RegisterType<UpdateFilmInput>();

            builder.RegisterType<ReviewType>();
            builder.RegisterType<AddReviewInput>();

            builder.RegisterType<Query>();
            builder.RegisterType<Mutation>();
            builder.RegisterType<Subscription>();

            builder.RegisterType<FilmSchema>();
        }
    }
}
