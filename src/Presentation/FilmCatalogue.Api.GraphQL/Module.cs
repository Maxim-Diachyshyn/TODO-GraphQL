using Autofac;
using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Api.GraphQL.Mutations;
using FilmCatalogue.Api.GraphQL.Queries;
using FilmCatalogue.Api.GraphQL.Schemas;

namespace FilmCatalogue.Api.GraphQL
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<FilmType>();
            builder.RegisterType<AddFilmInput>();

            builder.RegisterType<Query>();

            builder.RegisterType<Mutation>();

            builder.RegisterType<FilmSchema>();
        }
    }
}
