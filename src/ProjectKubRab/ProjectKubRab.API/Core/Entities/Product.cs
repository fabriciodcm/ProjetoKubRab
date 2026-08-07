using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectKubRab.API.Core.Entities
{
    public class Product
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0.0m;
        public DateOnly DateAdded { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public override bool Equals(object? obj)
        {
            if (obj is Product other)
            {
                return Name.Equals(other.Name) && DateAdded.Equals(other.DateAdded);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DateAdded);
        }

        public override string ToString()
        {
            return $"Product: {Name}, Date: {DateAdded}, Price: {Price.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"))}";
        }
    }
}
  