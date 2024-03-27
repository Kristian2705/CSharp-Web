using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Statistics;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository repository;

        public StatisticService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<StatisticServiceModel> TotalAsync()
        {
            int totalHouses = await repository.AllReadOnly<House>()
                .CountAsync();

            int totalRents = await repository.AllReadOnly<House>()
                .Where(h => h.RenterId != null)
                .CountAsync();

            return new StatisticServiceModel()
            {
                TotalHouses = totalHouses,
                TotalRents = totalRents
            };
        }
    }
}
