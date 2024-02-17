using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskBoardDbContext context;
        public HomeController(TaskBoardDbContext _context)
        {
            context = _context;
        }


        public async Task<IActionResult> Index()
        {
            var taskBoards = context
                .Boards
                .Select(b => b.Name)
                .Distinct();

            var tasksCounts = new List<HomeBoardModel>();

            foreach (var boardName in taskBoards)
            {
                var tasksInBoard = context.Tasks.Where(t => t.Board.Name == boardName).Count();
                tasksCounts.Add(new HomeBoardModel()
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoard
                });
            }

            var userTasksCount = -1;

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userTasksCount = context.Tasks.Where(t => t.OwnerId == currentUserId).Count();
            }

            var homeModel = new HomeViewModel()
            {
                AllTasksCount = context.Tasks.Count(),
                BoardsWithTasksCount = tasksCounts,
                UserTasksCount = userTasksCount
            };

            return View(homeModel);
        }
    }
}