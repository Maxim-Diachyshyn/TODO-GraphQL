using System;
using System.IO;

namespace FilmCatalogue.Domain.DataTypes
{
    public class Blob
    {
        public string Type { get; }
        public byte[] Data { get; }
        public string Base64 { get; }

        public Blob(string base64)
        {
            var index1 = base64.IndexOf("data:") + "data:".Length;
            Type = base64.Substring(index1, base64.IndexOf(";") - index1);
            Base64 = base64.Substring(base64.IndexOf("base64,") + "base64,".Length);
            Data = Convert.FromBase64String(Base64);
        }

        public Blob(string type, byte[] data)
        {
            Type = type;
            Data = data;
            Base64 = $"data:{type};base64,{Convert.ToBase64String(Data)}";
        }
    }
}