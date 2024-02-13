using Bogus;
using SuperDbApp.Model;

namespace SuperDbApp.Data
{
    internal class DemoDatenGenerator
    {
        public static IEnumerable<Product> GetDemoProducts(int amount = 100)
        {
            var productId = 1;

            // Bogus-Generator für Produkt-Daten
            var productFaker = new Faker<Product>("de")
                .UseSeed(3)
                .RuleFor(p => p.Id, _ => productId++)
                .RuleFor(p => p.Name, f => $"{f.Commerce.Color()} {f.Commerce.ProductAdjective()} {f.Commerce.ProductName()}")
                .RuleFor(p => p.Material, f => f.Commerce.ProductMaterial())
                .RuleFor(p => p.Price, f => f.Random.Decimal(10, 100))
                .RuleFor(p => p.Factory, f => new Factory
                {
                    Id = f.Random.Int(1, 10),
                    Adress = f.Address.StreetAddress(),
                    Country = f.Address.Country()
                });

            // Generiere 'amount' Produkte mit zufälligen Werten
            return productFaker.Generate(amount);
        }
    }
}
