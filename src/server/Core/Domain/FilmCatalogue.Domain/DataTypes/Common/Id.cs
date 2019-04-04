using System;

namespace FilmCatalogue.Domain.DataTypes.Common
{
    public class Id
    {
        private readonly Guid _value;

        public Id (Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new Exception($"{nameof(Id)} should not be empty");
            }
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
