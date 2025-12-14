using DealershipAPI.Hubs;
using DealershipAPI.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace DealershipAPI.Services
{
    public class RegistrationCheckerService : BackgroundService
    {
        private readonly ICarRepository _repo;
        private readonly IHubContext<RegistrationHub> _hubContext;

        public RegistrationCheckerService(
            ICarRepository repository,
            IHubContext<RegistrationHub> hubContext)
        {
            _repo = repository;
            _hubContext = hubContext;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var cars = _repo.GetAllCars();

                var status = cars.Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    IsExpired = c.RegistrationExpiry < DateTime.UtcNow
                });

                await _hubContext.Clients.All.SendAsync(
                    "ReceiveRegistrationStatus",
                    status,
                    cancellationToken: stoppingToken
                );

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }


    }
}
