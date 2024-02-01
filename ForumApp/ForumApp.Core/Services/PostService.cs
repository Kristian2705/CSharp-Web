using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumApp.Infrastructure.Data.Models;
using System.Runtime.CompilerServices;
using System.Reflection.PortableExecutable;

namespace ForumApp.Core.Services
{
	public class PostService : IPostService
	{
		private ForumAppDbContext context;
        public PostService(ForumAppDbContext _context)
        {
            context = _context;
        }

        public async Task AddPostAsync(PostModel model)
        {
			var post = new Post()
			{
				Title = model.Title,
				Content = model.Content,
			};
            await context.AddAsync(post);
			await context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
			var entity = await context.Posts.FindAsync(id);

			context.Remove(entity);

			await context.SaveChangesAsync();
        }

        public async Task EditPostAsync(PostModel model)
        {
            var entity = await context.FindAsync<Post>(model.Id);

			if (entity == null)
			{
				throw new ApplicationException("Invalid post!");
			}

			entity.Title = model.Title;
			entity.Content = model.Content;

			await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostModel>> GetAllPostsAsync()
		{
			return await context.Posts.Select(p => new PostModel()
			{
				Id = p.Id,
				Title = p.Title,
				Content = p.Content
			})
			.AsNoTracking()
			.ToListAsync();
		}

        public async Task<PostModel?> GetPostById(int id)
        {
            return await context.Posts
				.Where(p => p.Id == id)
				.Select(p => new PostModel()
				{
					Id = p.Id,
					Title = p.Title,
					Content = p.Content
				})
				.FirstOrDefaultAsync();
        }
    }
}
