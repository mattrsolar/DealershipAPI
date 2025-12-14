using DealershipAPI.Models;
using System.Text.Json;

namespace DealershipAPI.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly string _filePath;

        public CarRepository()
        {
            _filePath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "cars.json"
            );
        }

        public IReadOnlyList<Car> GetAllCars()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException($"File not found: {_filePath}");
            var json = File.ReadAllText(_filePath);

            var cars = JsonSerializer.Deserialize<List<Car>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            if (cars == null)
                throw new Exception("Unable to deserialize the cars.");

            return cars;
        }
    }
}
