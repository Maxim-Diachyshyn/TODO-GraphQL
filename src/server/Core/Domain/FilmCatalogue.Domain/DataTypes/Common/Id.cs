using System;

namespace FilmCatalogue.Domain.DataTypes.Common
{
    public struct Id
    {
        private readonly Guid _value;

        public Id (Guid value)
        {
            _value = value;
        }

        public static implicit operator Guid(Id id)
        {
            return id._value;
        }

        public static implicit operator Id(Guid id)
        {
            return new Id(id);
        }
    }
}
