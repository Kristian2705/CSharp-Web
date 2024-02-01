using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForumApp.Infrastructure.Constants.ValidationConstants;

namespace ForumApp.Core.Models
{
	public class PostModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(MaxTitleLength, 
			MinimumLength = MinTitleLength, 
			ErrorMessage = StringLengthErrorMessage)]
		public string Title { get; set; } = string.Empty;
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(MaxContentLength, 
			MinimumLength = MinContentLength, 
			ErrorMessage = StringLengthErrorMessage)]
		public string Content { get; set; } = string.Empty;

	}
}
