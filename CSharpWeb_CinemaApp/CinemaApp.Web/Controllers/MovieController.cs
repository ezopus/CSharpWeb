using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CinemaApp.Web.Controllers
{
	public class MovieController : Controller
	{
		private readonly CinemaDbContext dbContext;

		public MovieController(CinemaDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult Index()
		{
			IEnumerable<Movie> movies = this.dbContext
				.Movies
				.ToList();

			return View(movies);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return this.View();
		}

		[HttpPost]
		public IActionResult Create(AddMovieInputModel inputModel)
		{
			bool isReleaseDateValid = DateTime
				.TryParseExact(inputModel.ReleaseDate, "dd/MM/yyyy",
				CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

			if (!isReleaseDateValid)
			{
				this.ModelState.AddModelError(nameof(inputModel.ReleaseDate), "The release date must be in the following format: dd/MM/yyyy");
			}

			if (!this.ModelState.IsValid)
			{
				//render the same form with user entered values + model errors
				return this.View(inputModel);
			}

			Movie movie = new Movie()
			{
				Title = inputModel.Title,
				Genre = inputModel.Genre,
				ReleaseDate = releaseDate,
				Description = inputModel.Description,
				Director = inputModel.Director,
				Duration = inputModel.Duration
			};

			this.dbContext.Movies.Add(movie);
			this.dbContext.SaveChanges();

			return this.RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Details(string id)
		{
			bool isIdValid = Guid.TryParse(id, out Guid guidId);

			if (!isIdValid)
			{
				return this.RedirectToAction(nameof(Index));
			}

			Movie movie = this.dbContext.Movies.FirstOrDefault(m => m.Id == guidId);

			if (movie == null)
			{
				return this.RedirectToAction(nameof(Index));
			}

			return this.View(movie);
		}
	}
}
