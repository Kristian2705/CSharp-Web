using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Infrastructure.Data.Configuration
{
	public class PostConfiguration : IEntityTypeConfiguration<Post>
	{
		private Post[] initialPosts =
		{
			new Post()
			{
				Id = 1,
				Title = "New Fortnite Update",
				Content = "Added new weapons and skins"
			},
			new Post()
			{
				Id = 2,
				Title = "Brawl Stars balance changes",
				Content = "Buffed and nerfed several brawlers"
			},
			new Post()
			{
				Id = 3,
				Title = "New DLCs in ETS2",
				Content = "Added the new Italy DLC"
			}
		};
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			builder.HasData(initialPosts);
		}
	}
}
