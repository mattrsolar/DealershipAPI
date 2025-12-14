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
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Car>>(json)!;
        }
    }
}
