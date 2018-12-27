namespace FilmCatalogue.Api.Web.Rest.Models
{
    public class PageModel
    {
        public uint Page { get; set; }
        public uint Count { get; set; }

        public static implicit operator Domain.DataTypes.PageModel(PageModel model)
        {
            return new Domain.DataTypes.PageModel
            {
                Page = model.Page,
                Count = model.Count
            };
        }
    }
}
