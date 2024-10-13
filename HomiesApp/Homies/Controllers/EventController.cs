using Homies.Data.Models;
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
            var isStartValid = DateTime.TryParseExact(model.Start, RequiredDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime startParsed);
            var isEndValid = DateTime.TryParseExact(model.Start, RequiredDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime endParsed);

            if (!isStartValid)
            {
                ModelState
                    .AddModelError(nameof(model.Start), $"Invalid date format. Please use: {RequiredDateFormat}");
            }
            if (!isStartValid)
            {
                ModelState
                    .AddModelError(nameof(model.End), $"Invalid date format. Please use: {RequiredDateFormat}");
            }


            if (!ModelState.IsValid)
            {
                model.Types = await GetEventTypes();

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
        public async Task<IActionResult> Details(int id)
        {
            var e = await dbContext.Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    CreatedOn = e.CreatedOn.ToString(RequiredDateFormat),
                    Start = e.Start.ToString(RequiredDateFormat),
                    End = e.End.ToString(RequiredDateFormat),
                    Organiser = e.Organiser.UserName,
                    Type = e.Type.Name,
                    TypeId = e.TypeId,
                })
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }


            return View(e);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await dbContext.Events.FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganiserId != GetCurrentUserId())
            {
                return Unauthorized();
            }

            var model = new EventInfoViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString(RequiredDateFormat),
                End = e.End.ToString(RequiredDateFormat),
                TypeId = e.TypeId,
                Organiser = e.OrganiserId
            };

            model.Types = await GetEventTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventInfoViewModel model, int id)
        {
            var e = await dbContext.Events.FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganiserId != GetCurrentUserId())
            {
                return Unauthorized();
            }

            var isStartValid = DateTime.TryParseExact(model.Start, RequiredDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime startParsed);
            var isEndValid = DateTime.TryParseExact(model.Start, RequiredDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime endParsed);

            if (!isStartValid)
            {
                ModelState
                    .AddModelError(nameof(model.Start), $"Invalid date format. Please use: {RequiredDateFormat}");
            }
            if (!isStartValid)
            {
                ModelState
                    .AddModelError(nameof(model.End), $"Invalid date format. Please use: {RequiredDateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = await GetEventTypes();

                return View(model);
            }

            e.Start = startParsed;
            e.End = endParsed;
            e.Name = model.Name;
            e.Description = model.Description;
            e.TypeId = model.TypeId;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var ev = await dbContext.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (ev == null)
            {
                return BadRequest();
            }

            string userId = GetCurrentUserId();

            if (!ev.EventsParticipants.Any(p => p.HelperId == userId))
            {
                ev.EventsParticipants.Add(new EventParticipant()
                {
                    HelperId = userId,
                    EventId = id,
                });

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var ev = await dbContext.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            string userId = GetCurrentUserId();

            var eventToLeave = ev.EventsParticipants.FirstOrDefault(e => e.HelperId == userId);

            if (eventToLeave == null)
            {
                return BadRequest();
            }

            ev.EventsParticipants.Remove(eventToLeave);

            await dbContext.SaveChangesAsync();


            return RedirectToAction(nameof(All));
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        private async Task<ICollection<TypeViewModel>> GetEventTypes()
        {
            return await dbContext.Types
                .Select(t => new TypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }
    }
}
