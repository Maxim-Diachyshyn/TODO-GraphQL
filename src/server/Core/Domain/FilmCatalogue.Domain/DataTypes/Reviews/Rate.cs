using System;

namespace FilmCatalogue.Domain.DataTypes.Reviews
{
    public class Rate
    {
        private readonly int _value;

        public Rate(int value)
        {
            if (value < 1 || value > 5)
            {
                //TODO:
                throw new Exception();
            }
            _value = value;
        }

        public static implicit operator int(Rate rank)
        {
            return rank._value;
        }

        public static implicit operator Rate(int value)
        {
            return new Rate(value);
        }
    }
}