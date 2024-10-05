using CinemaApp.Web.ViewModels.Cinema;
using System.ComponentModel.DataAnnotations;
using static CinemaApp.Common.EntityValidationConstants.Movie;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class AddMovieToCinemaInputModel
    {
        [Required]
        public string MovieId { get; set; } = null!;

        [Required]
        [MaxLength(TitleMaxLength)]

        public string MovieTitle { get; set; } = null!;

        public IList<CinemaCheckBoxItemInputModel> Cinemas { get; set; }
            = new List<CinemaCheckBoxItemInputModel>();

    }
}
