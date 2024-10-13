using System.Globalization;

namespace Homies.Controllers
{
    using Homies.Data;
    using Homies.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using static Homies.Common.ValidationConstants;

    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext dbContext;

        public EventController(HomiesDbContext context)
        {
            this.dbContext = context;
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var events = await dbContext.Events
                .Select(e => new EventInfoViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Organiser = e.Organiser.UserName,
                    Start = e.Start.ToString(RequiredDateFormat),
                    Type = e.Type.Name,
                })
                .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string currentUserId = GetCurrentUserId();
            var model = await dbContext.Events
                .Include(e => e.EventsParticipants)
                .Where(e => e.EventsParticipants.Any(ep => ep.HelperId == currentUserId))
                .AsNoTracking()
                .Select(e => new EventInfoViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Organiser = e.Organiser.UserName,
                    Start = e.Start.ToString(RequiredDateFormat),
                    Type = e.Type.Name,
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var newEvent = new EventAddViewModel();
            newEvent.Types = await GetEventTypes();

            return View(newEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isStartValid = DateTime.TryParseExact(model.Start, RequiredDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime startParsed);
            var isEndValid = DateTime.TryParseExact(model.Start, RequiredDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime endParsed);

            if (!isStartValid || !isEndValid)
            {
                return View(model);
            }

            string currentUserId = GetCurrentUserId();

            var newEvent = new Data.Models.Event()
            {
                Name = model.Name,
                Description = model.Description,
                Start = startParsed,
                End = endParsed,
                CreatedOn = DateTime.Now,
                OrganiserId = currentUserId,
                TypeId = model.TypeId,
            };

            await dbContext.Events.AddAsync(newEvent);

            await dbContext.SaveChangesAsync();


            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Join(int id)
        {
            return RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        public IActionResult Leave(int id)
        {
            return RedirectToAction(nameof(All));
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        private Task<List<Data.Models.Type>> GetEventTypes()
        {
            return dbContext.Types.ToListAsync();
        }
    }
}
