using GameZone.Data;
using GameZone.Data.Models;
using GameZone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using static GameZone.Common.ValidationConstants.Game;

namespace GameZone.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly GameZoneDbContext dbContext;

        public GameController(GameZoneDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await dbContext.Games
                .Where(g => g.IsDeleted == false)
                .Select(g => new GameInfoViewModel()
                {
                    Id = g.Id,
                    Genre = g.Genre.Name,
                    ImageUrl = g.ImageUrl,
                    Publisher = g.Publisher.UserName ?? string.Empty,
                    ReleasedOn = g.ReleasedOn.ToString(ReleasedOnFormat),
                    Title = g.Title

                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new GameViewModel();
            model.Genres = await dbContext
                .Genres
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GameViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            DateTime releasedOn;

            if (DateTime.TryParseExact(model.ReleasedOn, ReleasedOnFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out releasedOn) == false)
            {
                ModelState.AddModelError(nameof(model.ReleasedOn), "Invalid date format!");

                return View(model);
            };



            Game game = new Game()
            {
                Description = model.Description,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                PublisherId = GetCurrentUserId() ?? string.Empty,
                ReleasedOn = releasedOn,
                Title = model.Title
            };

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await dbContext
                .Games
                .Where(g => g.Id == id)
                .AsNoTracking()
                .Select(g => new GameViewModel()
                {
                    Description = g.Description,
                    GenreId = g.GenreId,
                    ImageUrl = g.ImageUrl,
                    ReleasedOn = g.ReleasedOn.ToString(ReleasedOnFormat),
                    Title = g.Title
                })
                .FirstOrDefaultAsync();

            model.Genres = await GetGenres();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GameViewModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            DateTime releasedOn;

            if (DateTime.TryParseExact(model.ReleasedOn, ReleasedOnFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out releasedOn) == false)
            {
                ModelState.AddModelError(nameof(model.ReleasedOn), "Invalid date format!");

                return View(model);
            };

            Game? editedGame = await dbContext.Games.FindAsync(id);

            editedGame.Description = model.Description;
            editedGame.GenreId = model.GenreId;
            editedGame.ImageUrl = model.ImageUrl;
            editedGame.PublisherId = GetCurrentUserId() ?? string.Empty;
            editedGame.ReleasedOn = releasedOn;
            editedGame.Title = model.Title;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> MyZone()
        {
            return View(new List<GameViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> AddToMyZone(int id)
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> StrikeOut(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            return View();
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private Task<List<Genre>> GetGenres()
        {
            return dbContext.Genres.ToListAsync();
        }
    }
}
