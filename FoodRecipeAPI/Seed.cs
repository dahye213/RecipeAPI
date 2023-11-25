using FoodRecipeAPI.Data;
using FoodRecipeAPI.Models;
using System.Text.Json;

namespace FoodRecipeAPI
{
    // This class is to store data from public API to local database. 
    public class Seed
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DataContext _dataContext;

        public Seed(IHttpClientFactory httpClientFactory, DataContext dataContext)
        {
            _httpClientFactory = httpClientFactory;
            _dataContext = dataContext;
        }

        public async Task RetrieveAndStoreDataAsync()
        {
            string[] queries = {"pasta","soup","pizza","risotto","taco","sushi","rice","cake"};
            foreach (var query in queries)
            {
                var client = _httpClientFactory.CreateClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://recipe-by-api-ninjas.p.rapidapi.com/v1/recipe?query={query}"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "f29187cb44mshbd0fbe22456d613p1268dcjsnf6b282594085" },
                        { "X-RapidAPI-Host", "recipe-by-api-ninjas.p.rapidapi.com" },
                    },
                };

                try
                {
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();

                        // Deserialzie the JSON response into a C# object 

                        var recipes = JsonSerializer.Deserialize<List<Recipe>>(body, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });

                        if (recipes != null)
                        {
                            foreach (var recipe in recipes)
                            {
                                _dataContext.Recipes.Add(recipe);
                            }
                            await _dataContext.SaveChangesAsync();
                            Console.WriteLine($"Processed recipes for query: {query}");
                        }
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching recipe {query}: {ex.Message}");
                }
                
            }
            
        }
    }
}
