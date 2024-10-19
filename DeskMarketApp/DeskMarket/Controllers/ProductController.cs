namespace DeskMarket.Controllers
{
    using DeskMarket.Data;
    using DeskMarket.Data.Models;
    using DeskMarket.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Security.Claims;
    using static DeskMarket.Common.ErrorMessages;
    using static DeskMarket.Common.ValidationConstants;

    public class ProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProductController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserId = GetCurrentUserId();

            var model = await dbContext.Products
                .Where(p => p.IsDeleted == false)
                .Include(p => p.ProductsClients)
                .AsNoTracking()
                .Select(p => new ProductInfoViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    ImageUrl = String.IsNullOrEmpty(p.ImageUrl) ? String.Empty : p.ImageUrl,
                    Price = p.Price,
                    IsSeller = currentUserId == p.SellerId,
                    HasBought = p.ProductsClients.Any(pr => pr.ClientId == currentUserId),
                })
                .ToListAsync();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new ProductAddViewModel();
            model.Categories = await GetCategories();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddViewModel model)
        {
            string currentUserId = GetCurrentUserId();

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (DateTime.TryParseExact(model.AddedOn, RequiredDateFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out var addedOnParsedDate) == false)
            {
                ModelState.AddModelError(nameof(model.AddedOn), ErrorDateFormat);

                return View(model);
            };

            Product product = new Product()
            {
                ProductName = model.ProductName,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                SellerId = currentUserId,
                AddedOn = addedOnParsedDate,
                CategoryId = model.CategoryId,
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string currentUserId = GetCurrentUserId();

            var model = await dbContext.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.ProductsClients.Any(pr => pr.ClientId == currentUserId))
                .AsNoTracking()
                .Select(p => new ProductInfoViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    ImageUrl = String.IsNullOrEmpty(p.ImageUrl) ? String.Empty : p.ImageUrl,
                    Price = p.Price,
                    IsSeller = currentUserId == p.SellerId,
                    HasBought = p.ProductsClients.Any(pr => pr.ClientId == currentUserId),
                })
                .ToListAsync();

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            Product? product = await dbContext.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductsClients)
                .FirstOrDefaultAsync();

            if (product == null || product.IsDeleted)
            {
                return BadRequest();
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (product.ProductsClients.Any(pc => pc.ClientId == currentUserId) == false
                && product.SellerId != currentUserId)
            {
                product.ProductsClients.Add(new ProductClient()
                {
                    ProductId = product.Id,
                    ClientId = currentUserId
                });

                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Cart));
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            Product? product = await dbContext.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductsClients)
                .FirstOrDefaultAsync();

            if (product == null || product.IsDeleted)
            {
                return BadRequest();
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            ProductClient? productClient = dbContext.ProductsClients
                .FirstOrDefault(pc => pc.ClientId == currentUserId && pc.ProductId == product.Id);

            if (productClient != null)
            {
                product.ProductsClients.Remove(productClient);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            string currentUserId = GetCurrentUserId();

            var model = await dbContext.Products
                .Where(p => p.Id == id && p.IsDeleted == false)
                .Select(p =>
                    new ProductDetailsViewModel()
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Description = p.Description,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl ?? string.Empty,
                        AddedOn = p.AddedOn.ToString(RequiredDateFormat),
                        CategoryName = p.Category.Name,
                        Seller = p.Seller.UserName!,
                        HasBought = p.ProductsClients.Any(pr => pr.ClientId == currentUserId),
                    })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //if product id doesn't exist return to all
            var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            //if product exists but current logged in user is not authorized to edit it
            string currentUserId = GetCurrentUserId();
            if (product.SellerId != currentUserId || product.IsDeleted)
            {
                return RedirectToAction(nameof(Index));
            }

            //get product to edit
            var model = await dbContext.Products
                .Where(pr => pr.Id == id && pr.IsDeleted == false)
                .AsNoTracking()
                .Select(p => new ProductEditViewModel()
                {

                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    SellerId = p.SellerId,
                    AddedOn = p.AddedOn.ToString(RequiredDateFormat),
                    CategoryId = p.CategoryId,
                })
                .FirstOrDefaultAsync();

            model!.Categories = await GetCategories();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            if (DateTime.TryParseExact(model.AddedOn, RequiredDateFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out var addedOnParsedDate) == false)
            {
                ModelState.AddModelError(nameof(model.AddedOn), ErrorDateFormat);
                model.Categories = await GetCategories();
                return View(model);
            };

            var product = await dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return BadRequest();
            }

            product.Id = id;
            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Price = model.Price;
            product.AddedOn = addedOnParsedDate;
            product.CategoryId = model.CategoryId;

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Product", new { id = id });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(ProductDeleteViewModel model, int id)
        {
            var productToDelete = await dbContext.Products
                .Where(pr => pr.Id == id && pr.IsDeleted == false)
                .AsNoTracking()
                .Select(pr => new ProductDeleteViewModel()
                {
                    Id = pr.Id,
                    ProductName = pr.ProductName,
                    SellerId = pr.SellerId,
                    Seller = pr.Seller.UserName!,
                })
                .FirstOrDefaultAsync();

            return View(productToDelete);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteViewModel model)
        {
            Product? product = await dbContext.Products
                .Where(pr => pr.Id == model.Id && pr.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (product != null)
            {
                product.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        private Task<List<Category>> GetCategories()
        {
            return dbContext.Categories.ToListAsync();
        }
    }
}
