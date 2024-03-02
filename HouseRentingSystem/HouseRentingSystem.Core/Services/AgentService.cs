using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
	public class AgentService : IAgentService
	{
		private readonly IRepository repository;
        public AgentService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<bool> ExistsByIdAsync(string userId)
		{
			return await repository.AllReadOnly<Agent>()
				.AnyAsync(a => a.UserId == userId);
		}
	}
}
