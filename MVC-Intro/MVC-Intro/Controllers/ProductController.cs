using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVC_Intro.Models;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Intro.Controllers
{
	public class ProductController : Controller
	{
		private ICollection<ProductViewModel> _products = new List<ProductViewModel>()
		{
			new ProductViewModel()
			{
				Id = 1,
				Name = "Cheese",
				Price = 7.00
			},
			new ProductViewModel()
			{
				Id = 2,
				Name = "Ham",
				Price = 5.50
			},
			new ProductViewModel()
			{
				Id = 3,
				Name = "Bread",
				Price = 1.50
			},
		};
		public IActionResult Index()
		{
			return View();
		}
		[ActionName("My-Products")]
		public IActionResult All(string keyword)
		{
			if(keyword != null)
			{
				var foundProducts = _products.Where(x => x.Name.ToLower().Contains(keyword.ToLower())).ToList();
				return View(foundProducts);
			}
			return View(_products);
		}

		public IActionResult ById(int id)
		{
			var product = _products.FirstOrDefault(x => x.Id == id);
			if(product == null)
			{
				return BadRequest();
			}
			return View(product);
		}

		public IActionResult AllAsJson()
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true,
			};
			return Json(_products, options);
		}

		public IActionResult AllAsText()
		{
			var text = new StringBuilder();
			foreach (var item in _products)
			{
				text.AppendLine($"Product {item.Id}: {item.Name} - {item.Price:f2} lv.");
			}
			return Content(text.ToString().Trim());
		}

		public IActionResult AllAsTextFile()
		{
			var sb = new StringBuilder();
			foreach (var item in _products)
			{
				sb.AppendLine($"Product {item.Id}: {item.Name} - {item.Price:f2} lv.");
			}

			Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");

			return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
		}
	}
}
