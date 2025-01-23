using System.Text.RegularExpressions;
using OrderMs.Common.Exceptions;

namespace OrderMs.Domain.Entities.ValueObjects
{
    public partial class OrderTotalCost
    {
        private const string Pattern = @"^\d+\.\d{2}$";
        private OrderTotalCost(decimal value) => Value = value;

        public static OrderTotalCost Create(decimal value)
        {
            try
            {
                //if (value == default) throw new NullAttributeException("Total cost is required");
                //if (!TotalCostRegex().IsMatch(value)) throw new InvalidAttributeException("Client ci is invalid");
                if (value < 0) throw new InvalidAttributeException("Total cost is invalid");

                return new OrderTotalCost(value);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public decimal Value { get; init; } //*init no permite setear desde afuera, solo desde el constructor

        [GeneratedRegex(Pattern)]
        private static partial Regex TotalCostRegex();
    }
}