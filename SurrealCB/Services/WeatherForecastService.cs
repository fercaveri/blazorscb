using System;
using System.Linq;
using System.Threading.Tasks;
using SurrealCB.Data.Model;

namespace SurrealCB.Server
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<object[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new object
            {
            }).ToArray());
        }
    }
}
