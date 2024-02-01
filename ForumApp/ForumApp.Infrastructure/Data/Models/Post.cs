using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForumApp.Infrastructure.Constants.ValidationConstants;

namespace ForumApp.Infrastructure.Data.Models
{
	public class Post
	{
		[Key]
        public int Id { get; set; }
		[Required]
		[MaxLength(MaxTitleLength)]
		public string Title { get; set; } = string.Empty;
		[Required]
		[MaxLength(MaxContentLength)]
		public string Content { get; set; } = string.Empty;
	}
}
