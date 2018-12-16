using System;

namespace FilmCatalogue.Domain.DataTypes
{
    public struct Id
    {
        private readonly Guid _value;

        public Id (Guid value)
        {
            if (value == null || value == Guid.Empty)
            {
                throw new Exception();
            }
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
    }
}
