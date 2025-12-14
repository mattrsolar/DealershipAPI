using DealershipAPI.Models;

namespace DealershipAPI.Repositories
{
    public interface ICarRepository
    {
        IReadOnlyList<Car> GetAllCars();
    }
}
