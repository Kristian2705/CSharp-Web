using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Controllers
{
	/// <summary>
	/// Post controller
	/// </summary>
	public class PostController : Controller
	{
		private IPostService service;
        public PostController(IPostService _service)
        {
			service = _service;
        }
		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var posts = await service.GetAllPostsAsync();
			return View(posts);
		}

		[HttpGet]
		public IActionResult AddPost()
		{
			var model = new PostModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddPost(PostModel model)
		{
			if(!ModelState.IsValid)
			{
				return View(model);
			}

			await service.AddPostAsync(model);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> EditPost(int id)
		{
			PostModel? model = await service.GetPostById(id);

			if(model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> EditPost(PostModel model)
		{
			if(!ModelState.IsValid)
			{
				return View(model);
			}

			await service.EditPostAsync(model);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> DeletePost(int id)
		{
			await service.DeletePostAsync(id);
			return RedirectToAction(nameof(Index));
        }
	}
}
