using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Infrastructure.Data.Common
{
	public class Repository : IRepository
	{
		private readonly DbContext context;
        public Repository(HouseRentingDbContext _context)
        {
			context = _context;
        }
        public IQueryable<T> All<T>() where T : class => DbSet<T>();

		public IQueryable<T> AllReadOnly<T>() where T : class => DbSet<T>().AsNoTracking();

		private DbSet<T> DbSet<T>() where T : class => context.Set<T>();
	}
}
