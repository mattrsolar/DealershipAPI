using DealershipAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DealershipAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _rep;

        public CarsController(ICarRepository repository)
        {
            _rep = repository;
        }


        [HttpGet]
        public IActionResult GetCars([FromQuery] string? make)
        {
            var cars = _rep.GetAllCars();

            if (!string.IsNullOrEmpty(make))
            {
                cars = cars
                    .Where(c => c.Make.Equals(make, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }   

            return Ok(cars);
        }

    }
}
