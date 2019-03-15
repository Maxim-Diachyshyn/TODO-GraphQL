namespace FilmCatalogue.Domain.DataTypes.Reviews
{
    public class Rank
    {
        private readonly decimal _value;

        public Rank(decimal value)
        {
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