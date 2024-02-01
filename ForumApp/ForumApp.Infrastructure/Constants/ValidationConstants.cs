using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Infrastructure.Constants
{
	public class ValidationConstants
	{
		public const int MaxTitleLength = 50;
		public const int MinTitleLength = 10;
		public const int MaxContentLength = 1500;
		public const int MinContentLength = 30;
		public const string RequiredErrorMessage = "The {0} is required!";
		public const string StringLengthErrorMessage = "The {0} field must be between {2} and {1} characters long!";
	}
}
