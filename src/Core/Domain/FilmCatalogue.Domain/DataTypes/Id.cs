using System;
using FluentValidation;

namespace FilmCatalogue.Domain.DataTypes
{
    public struct Id
    {
        private readonly Guid _value;

        public Id (Guid value)
        {
            _value = value;
        }

        public Id New()
        {           
            return new Id(Guid.NewGuid());
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
