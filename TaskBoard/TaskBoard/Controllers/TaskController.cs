using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private TaskBoardDbContext context;

        public TaskController(TaskBoardDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TaskFormViewModel();
            model.Boards = await GetBoards();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormViewModel model)
        {
            if(!(await GetBoards()).Any(b => b.Id == model.Id))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            if(!ModelState.IsValid)
            {
                model.Boards = await GetBoards();

                return View(model);
            }

            var entity = new Data.Models.Task()
            {
                BoardId = model.BoardId,
                CreatedOn = DateTime.Now,
                Description = model.Description,
                OwnerId = GetUserId(),
                Title = model.Title
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var task = await context.Tasks
                .Where(b => b.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Board = t.Board.Name,
                    Description = t.Description,
                    Id = t.Id,
                    CreatedOn = t != null && t.CreatedOn.HasValue 
                        ? t.CreatedOn.Value.ToString("dd/MM/yyyy HH:mm") 
                        : "",
                    Owner = t.Owner.UserName,
                    Title = t.Title
                })
                .FirstOrDefaultAsync();

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await context.Tasks.FindAsync(id);

            if(task == null)
            {
                return BadRequest();
            }

            if(task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new TaskFormViewModel()
            {
                BoardId = task.BoardId,
                Description = task.Description,
                Id = task.Id,
                Boards = await GetBoards()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskFormViewModel model, int id)
        {
            var task = await context.Tasks.FindAsync(id);

            if(task == null)
            {
                return BadRequest();
            }

            if (task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            if (!(await GetBoards()).Any(b => b.Id == model.Id))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Boards = await GetBoards();

                return View(model);
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;

            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Board");
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var task = await context.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            if (task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel model)
        {
            var task = await context.Tasks.FindAsync(model.Id);

            if (task == null)
            {
                return BadRequest();
            }

            if (task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Board");
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private async Task<IEnumerable<TaskBoardModel>> GetBoards()
        {
            var boards = await context.Boards
                .Select(b => new TaskBoardModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToListAsync();

            return boards;
        }
    }
}
