using FilmCatalogue.Domain.DataTypes.Common;

namespace FilmCatalogue.Api.Common.Contexts.Common.ViewModels
{
    public class BlobViewModel
    {
        public string Type { get; set; }
        public string Base64 { get; set; }

        public BlobViewModel(Blob blob)
        {
            Type = blob.Type;
            Base64 = blob.Base64;
        }
    }
}