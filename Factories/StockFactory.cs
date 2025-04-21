using System.Text;
using Bogus;
using Microsoft.Extensions.Primitives;
using ProductApi.Models;

namespace ProductApi.Factories;

public class StockFactory
{
    private static readonly Faker Faker = new Faker();
    /// <summary>
    /// Creates a random stock
    /// </summary>
    /// <returns></returns>
    public static Stock Create(int? id = null)
    {
        var location = Faker.Lorem.Words(Faker.Random.Int(1,5));
        StringBuilder st = new StringBuilder();
        st.Append(location[0]);
        for (int i = 1; i < location.Length; i++)
        {
            st.Append($" {location[i]}");
        }
        return new Stock()
        {
            ProductId = id ?? Faker.Random.Int(1,100000), 
            Quantity = Faker.Random.Int(1,100000), 
            Location = st.ToString()
        };
    }
}