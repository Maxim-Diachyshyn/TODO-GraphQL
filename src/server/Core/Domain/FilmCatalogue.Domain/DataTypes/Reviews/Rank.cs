using System;

namespace FilmCatalogue.Domain.DataTypes.Reviews
{
    public class Rank
    {
        private readonly decimal _value;

        public Rank(decimal value)
        {
            if (value < 1 || value > 5)
            {
                //TODO:
                throw new Exception($"{nameof(Rank)} should be in range [1-5]");
            }
            _value = value;
        }

        public static implicit operator decimal(Rank rank)
        {
            return rank._value;
        }

        public static implicit operator Rank(decimal value)
        {
            return new Rank(value);
        }
    }
}