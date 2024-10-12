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

            if (DateTime.TryParseExact(model.ReleasedOn, ReleasedOnFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out var releasedOn) == false)
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

            return RedirectToAction(nameof(MyZone));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //if game id doesn't exist return to all
            var game = dbContext.Games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return RedirectToAction(nameof(All));
            }

            //if game exists but current logged in user is not authorized to edit game
            string currentUserId = GetCurrentUserId();
            if (game != null && game.PublisherId != currentUserId)
            {
                return RedirectToAction(nameof(All));
            }

            //get game to edit
            var model = await dbContext
                .Games
                .Where(g => g.Id == id)
                .AsNoTracking()
                .Select(g => new GameEditModel()
                {
                    Description = g.Description,
                    GenreId = g.GenreId,
                    ImageUrl = g.ImageUrl,
                    ReleasedOn = g.ReleasedOn.ToString(ReleasedOnFormat),
                    Title = g.Title
                })
                .FirstOrDefaultAsync();

            model.Genres = await GetGenres();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GameEditModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (DateTime.TryParseExact(model.ReleasedOn, ReleasedOnFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out var releasedOn) == false)
            {
                ModelState.AddModelError(nameof(model.ReleasedOn), "Invalid date format!");

                return View(model);
            };

            var game = await dbContext.Games.FindAsync(id);

            if (game == null)
            {
                throw new ArgumentException("Game id invalid.");
            }

            game.Id = model.Id;
            game.Title = model.Title;
            game.Description = model.Description;
            game.GenreId = model.GenreId;
            game.ImageUrl = model.ImageUrl ?? string.Empty;
            game.PublisherId = GetCurrentUserId() ?? string.Empty;
            game.ReleasedOn = releasedOn;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> MyZone()
        {
            string currentUsedId = GetCurrentUserId();

            var model = await dbContext.Games
                .Where(g => g.IsDeleted == false)
                .Where(g => g.GamersGames.Any(gr => gr.GamerId == currentUsedId))
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
        public async Task<IActionResult> AddToMyZone(int id)
        {
            Game? game = await dbContext.Games
                .Where(g => g.Id == id)
                .Include(g => g.GamersGames)
                .FirstOrDefaultAsync();

            if (game == null || game.IsDeleted)
            {
                throw new ArgumentException("Invalid id.");
            }

            string currentUsedId = GetCurrentUserId() ?? string.Empty;

            if (game.GamersGames.Any(gr => gr.GamerId == currentUsedId) == false)
            {
                game.GamersGames.Add(new GamerGame()
                {
                    GameId = game.Id,
                    GamerId = currentUsedId
                });

                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(MyZone));
            }
            else
            {
                return RedirectToAction(nameof(All));
            }
        }
        [HttpGet]
        public async Task<IActionResult> StrikeOut(int id)
        {
            Game? game = await dbContext.Games
                .Where(g => g.Id == id)
                .Include(g => g.GamersGames)
                .FirstOrDefaultAsync();

            if (game == null || game.IsDeleted)
            {
                throw new ArgumentException("Invalid id.");
            }

            string currentUsedId = GetCurrentUserId() ?? string.Empty;

            GamerGame? gamerGame = dbContext.GamersGames
                    .FirstOrDefault(gr => gr.GamerId == currentUsedId);

            if (gamerGame != null)
            {
                game.GamersGames.Remove(gamerGame);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MyZone));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await dbContext.Games
                .Where(g => g.Id == id)
                .Where(g => g.IsDeleted == false)
                .Select(g =>
                new GameDetailsViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    Genre = g.Genre.Name,
                    ImageUrl = g.ImageUrl ?? string.Empty,
                    Publisher = g.Publisher.UserName ?? string.Empty,
                    ReleasedOn = g.ReleasedOn.ToString(ReleasedOnFormat),
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(GameDeleteViewModel model, int id)
        {
            var gameToDelete = await dbContext.Games
                .Where(g => g.Id == id && g.IsDeleted == false)
                .AsNoTracking()
                .Select(g => new GameDeleteViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Publisher = g.Publisher.UserName ?? string.Empty,
                })
                .FirstOrDefaultAsync();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(GameDeleteViewModel model)
        {
            Game? game = await dbContext.Games
                .Where(g => g.Id == model.Id && g.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (game != null)
            {
                game.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MyZone));
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
