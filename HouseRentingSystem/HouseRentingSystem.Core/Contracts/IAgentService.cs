namespace HouseRentingSystem.Core.Contracts
{
	public interface IAgentService
	{
		Task<bool> ExistsByIdAsync(string userId);
	}
}
