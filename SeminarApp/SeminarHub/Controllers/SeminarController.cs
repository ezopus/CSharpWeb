using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Common;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;
using static SeminarHub.Common.ValidationConstants;


namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext context;

        public SeminarController(SeminarHubDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await context.Seminars
                .Where(s => !s.IsDeleted)
                .AsNoTracking()
                .Select(s => new SeminarInfoViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    DateAndTime = s.DateAndTime.ToString(RequiredDateFormat),
                    Category = s.Category.Name,
                    Organizer = s.Organizer.UserName,
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string currentUserId = await GetCurrentUserId();

            var model = await context.Seminars
                .Include(s => s.SeminarsParticipants)
                .AsNoTracking()
                .Where(s => s.SeminarsParticipants.Any(sp => sp.ParticipantId == currentUserId)
                            && !s.IsDeleted)
                .Select(s => new SeminarInfoViewModel()
                {
                    Id = s.Id,
                    Category = s.Category.Name,
                    DateAndTime = s.DateAndTime.ToString(RequiredDateFormat),
                    Lecturer = s.Lecturer,
                    Organizer = s.Organizer.UserName,
                    Topic = s.Topic
                })
                .ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> Join(int id)
        {
            var seminar = await context.Seminars.FindAsync(id);

            if (seminar == null)
            {
                return RedirectToAction(nameof(All));
            }

            string currentUserId = await GetCurrentUserId();

            var seminarParticipant = await context.SeminarsParticipants
                .Where(sp => sp.SeminarId == id && sp.ParticipantId == currentUserId)
                .FirstOrDefaultAsync();

            if (seminarParticipant == null)
            {
                await context.SeminarsParticipants.AddAsync(new SeminarParticipant()
                {
                    SeminarId = id,
                    ParticipantId = currentUserId,
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }

        public async Task<IActionResult> Leave(int id)
        {
            var seminar = await context.Seminars.FindAsync(id);

            if (seminar == null)
            {
                return RedirectToAction(nameof(All));
            }

            string currentUserId = await GetCurrentUserId();

            var seminarParticipant = await context.SeminarsParticipants
                .Where(sp => sp.SeminarId == id && sp.ParticipantId == currentUserId)
                .FirstOrDefaultAsync();

            if (seminarParticipant != null)
            {
                context.SeminarsParticipants.Remove(seminarParticipant);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await context.Seminars
                .Where(s => s.Id == id && !s.IsDeleted)
                .AsNoTracking()
                .Select(s => new SeminarDetailsViewModel()
                {
                    Id = s.Id,
                    Category = s.Category.Name,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Duration = s.Duration,
                    DateAndTime = s.DateAndTime.ToString(RequiredDateFormat),
                    Details = s.Details,
                    Organizer = s.Organizer.UserName,
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SeminarAddViewModel();

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            string currentUserId = await GetCurrentUserId();

            var isDateValid = DateTime.TryParseExact(model.DateAndTime,
                RequiredDateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateParsed
            );

            if (!isDateValid)
            {
                model.Categories = await GetCategories();
                ModelState.AddModelError(nameof(model.DateAndTime), ErrorMessages.ErrorDateFormat);
                return View(model);
            }

            var newSeminar = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                CategoryId = model.CategoryId,
                DateAndTime = dateParsed,
                Details = model.Details,
                OrganizerId = currentUserId,
                Duration = model.Duration,
                IsDeleted = false,
            };

            await context.Seminars.AddAsync(newSeminar);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await context.Seminars
                .Where(s => s.Id == id && !s.IsDeleted)
                .AsNoTracking()
                .Select(s => new SeminarEditViewModel()
                {
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    CategoryId = s.CategoryId,
                    DateAndTime = s.DateAndTime.ToString(RequiredDateFormat),
                    Details = s.Details,
                    OrganizerId = s.OrganizerId,
                    Duration = s.Duration,
                })
                .FirstOrDefaultAsync();

            //check if seminar with id exists
            if (model == null)
            {
                return BadRequest();
            }

            //check if user is authorized to edit seminar
            string currentUserId = await GetCurrentUserId();
            if (model.OrganizerId != currentUserId)
            {
                return RedirectToAction(nameof(All));
            }

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeminarEditViewModel model, int id)
        {
            var modelToEdit = await context.Seminars
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefaultAsync();

            //check if seminar with id exists
            if (modelToEdit == null)
            {
                return BadRequest();
            }

            var isDateValid = DateTime.TryParseExact(model.DateAndTime,
                RequiredDateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateParsed
            );

            if (!isDateValid)
            {
                model.Categories = await GetCategories();
                ModelState.AddModelError(nameof(model.DateAndTime), ErrorMessages.ErrorDateFormat);
                return View(model);
            }

            string currentUserId = await GetCurrentUserId();

            modelToEdit.Topic = model.Topic;
            modelToEdit.Lecturer = model.Lecturer;
            modelToEdit.Duration = model.Duration;
            modelToEdit.CategoryId = model.CategoryId;
            modelToEdit.Details = model.Details;
            modelToEdit.DateAndTime = dateParsed;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await context.Seminars
                .Where(s => s.Id == id)
                .AsNoTracking()
                .Select(s => new SeminarDeleteViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime,
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelToDelete = await context.Seminars.FindAsync(id);

            if (modelToDelete != null)
            {
                modelToDelete.IsDeleted = true;
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));
        }
        private async Task<string> GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private async Task<List<Category>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
