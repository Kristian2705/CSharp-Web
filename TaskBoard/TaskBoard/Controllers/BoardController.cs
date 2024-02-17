using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly TaskBoardDbContext context;
        public BoardController(TaskBoardDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var boards = await context.Boards
                .Select(b => new BoardViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new TaskViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.Owner.UserName
                    })
                    .ToList()
                })
                .ToListAsync();

            return View(boards);
        }
    }
}
