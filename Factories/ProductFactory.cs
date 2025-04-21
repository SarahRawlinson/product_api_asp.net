using Bogus;
using ProductApi.Models;

namespace ProductApi.Factories;

public class ProductFactory
{
    private static readonly Faker Faker = new Faker();
    /// <summary>
    /// Creates a random product
    /// </summary>
    /// <returns></returns>
    public static Product Create()
    {
        return new Product
        {
            Name = Faker.Lorem.Letter(Faker.Random.Number(5,50)),
            Price = Faker.Random.Number(1, 10)
        };
    }
}