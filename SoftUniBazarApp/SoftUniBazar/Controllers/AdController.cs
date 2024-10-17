using SoftUniBazar.Data.Models;
using System.Security.Claims;

namespace SoftUniBazar.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SoftUniBazar.Data;
    using SoftUniBazar.Models;
    using static SoftUniBazar.Common.ValidationConstants;

    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext context;

        public AdController(BazarDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await context.Ads
                .Where(ad => !ad.IsDeleted)
                .AsNoTracking()
                .Select(ad => new AdInfoViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Description = ad.Description,
                    Category = ad.Category.Name,
                    CreatedOn = ad.CreatedOn.ToString(RequiredDateFormat),
                    ImageUrl = ad.ImageUrl,
                    Owner = ad.Owner.UserName,
                    Price = ad.Price,
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string currentUserId = await GetCurrentUserId();

            var model = await context.Ads
                .Include(ad => ad.AdsBuyers)
                .AsNoTracking()
                .Where(ab => ab.AdsBuyers.Any(ad => ad.BuyerId == currentUserId)
                             && !ab.IsDeleted)
                .Select(ad => new AdInfoViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Description = ad.Description,
                    Category = ad.Category.Name,
                    CreatedOn = ad.CreatedOn.ToString(RequiredDateFormat),
                    ImageUrl = ad.ImageUrl,
                    Owner = ad.Owner.UserName,
                    Price = ad.Price,
                })
                .ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var ad = await context.Ads.FindAsync(id);

            if (ad == null)
            {
                return RedirectToAction(nameof(All));
            }

            string currentUserId = await GetCurrentUserId();

            var seminarParticipant = await context.AdsBuyers
                .Where(ad => ad.AdId == id && ad.BuyerId == currentUserId)
                .FirstOrDefaultAsync();

            if (seminarParticipant == null)
            {
                await context.AdsBuyers.AddAsync(new AdBuyer()
                {
                    AdId = id,
                    BuyerId = currentUserId,
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var ad = await context.Ads.FindAsync(id);

            if (ad == null)
            {
                return RedirectToAction(nameof(All));
            }

            string currentUserId = await GetCurrentUserId();

            var seminarParticipant = await context.AdsBuyers
                .Where(ad => ad.AdId == id && ad.BuyerId == currentUserId)
                .FirstOrDefaultAsync();

            if (seminarParticipant != null)
            {
                context.AdsBuyers.Remove(seminarParticipant);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AdAddViewModel();

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            string currentUserId = await GetCurrentUserId();

            var newAd = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreatedOn = DateTime.Now,
                OwnerId = currentUserId,
            };

            await context.Ads.AddAsync(newAd);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await context.Ads
                .Where(ad => ad.Id == id && !ad.IsDeleted)
                .Select(ad => new AdEditViewModel()
                {
                    Name = ad.Name,
                    Description = ad.Description,
                    ImageUrl = ad.ImageUrl,
                    Price = ad.Price,
                    CategoryId = ad.CategoryId,
                    OwnerId = ad.OwnerId,
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            string currentUserId = await GetCurrentUserId();
            if (model.OwnerId != currentUserId)
            {
                return RedirectToAction(nameof(All));
            }

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdEditViewModel model, int id)
        {
            var modelToEdit = await context.Ads
                .Where(ad => ad.Id == id && !ad.IsDeleted)
                .FirstOrDefaultAsync();

            //check if seminar with id exists
            if (modelToEdit == null)
            {
                return BadRequest();
            }

            string currentUserId = await GetCurrentUserId();

            modelToEdit.Name = model.Name;
            modelToEdit.Description = model.Description;
            modelToEdit.ImageUrl = model.ImageUrl;
            modelToEdit.Price = model.Price;
            modelToEdit.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private async Task<string> GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        private async Task<List<Category>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
