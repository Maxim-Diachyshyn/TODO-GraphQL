using System;

namespace TODOGraphQL.Domain.DataTypes.Common
{
    public class Id
    {
        private readonly Guid _value;

        public Id (Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new Exception($"{nameof(value)} should not be empty");
            }
            _value = value;
        }

        public static implicit operator Guid(Id id) => id._value;
        public static implicit operator Id(Guid id) => new Id(id);
        public override string ToString() => _value.ToString();
    }
}
