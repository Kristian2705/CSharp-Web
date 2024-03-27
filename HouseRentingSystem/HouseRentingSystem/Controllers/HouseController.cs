using HouseRentingSystem.Attributes;
using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : BaseController
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;
        private readonly ILogger logger;

        public HouseController(
            IHouseService _houseService, 
            IAgentService _agentService,
            ILogger<HouseController> _logger)
        {
            houseService = _houseService;
            agentService = _agentService;
            logger = _logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllHousesQueryModel query)
        {
            var model = await houseService.AllAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = model.TotalHousesCount;
            query.Houses = model.Houses;
            query.Categories = await houseService.AllCategoriesNameAsync();

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var userId = User.Id();
            IEnumerable<HouseServiceModel> model;

            if(await agentService.ExistsByIdAsync(userId))
            {
                int agentId = await agentService.GetAgentByIdAsync(userId) ?? 0;
                model = await houseService.AllHousesByAgentIdAsync(agentId);
            }
            else
            {
                model = await houseService.AllHousesByUserIdAsync(userId);
            }

			return View(model);
		}

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if(await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await houseService.HouseDetailsByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        [MustBeAgent]
        public async Task<IActionResult> Add()
        {
            var model = new HouseFormModel()
            {
                Categories = await houseService.AllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [MustBeAgent]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if(await houseService.CategoryExistsAsync(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exits");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategoriesAsync();

                return View(model);
            }

            int? agentId = await agentService.GetAgentByIdAsync(User.Id());

            int newHouseId = await houseService.CreateAsync(model, agentId ?? 0);

            return RedirectToAction(nameof(Details), new { id = newHouseId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if(await houseService.HasAgentWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var model = await houseService.GetHouseFormModelByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel house)
        {
            if (await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            if(await houseService.CategoryExistsAsync(house.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(house.CategoryId), "Category does not exits");
            }

            if (!ModelState.IsValid)
            {
                house.Categories = await houseService.AllCategoriesAsync();

                return View(house);
            }

            await houseService.EditAsync(id, house);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
			if(await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if(await houseService.HasAgentWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var house = await houseService.HouseDetailsByIdAsync(id);

            var model = new HouseDetailsViewModel()
            {
                Id = id,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                Title = house.Title
            };

            return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel house)
        {
            if (await houseService.ExistsAsync(house.Id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithIdAsync(house.Id, User.Id()) == false)
            {
                return Unauthorized();
            }

            await houseService.DeleteAsync(house.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            if(await houseService.IsRentedAsync(id))
            {
                return BadRequest();
            }

            await houseService.RentAsync(id, User.Id());

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if(await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            try
            {
                await houseService.LeaveAsync(id, User.Id());
            }
            catch(UnauthorizedAccessException uae)
            {
                logger.LogError(uae, "HouseController/Leave");
                return Unauthorized();
            }

            return RedirectToAction(nameof(Mine));
        }
    }
}
