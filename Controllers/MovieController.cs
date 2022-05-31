using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MovieController : Controller
    {

        List<Movie> ListaMovies = new List<Movie>();
        public void PrendiDati()
        {
            Movie PrimoMovie = new Movie()
            {
                Id = 1,
                Title = "When Harry Met Sally",
                ReleaseDate = DateTime.Parse("1989 - 2 - 12"),
                Genre = "Romantic Comedy",
                Price = 7.99M,
                Photo = "/img/fondo-pag-speciali.jpg",
                AlternateText = "Pranaya Rout Photo not available"
            };

            ListaMovies.Add(PrimoMovie);

            Movie SecondoMovie = new Movie()
            {
                Id = 2,
                Title = "Second",
                ReleaseDate = DateTime.Parse("1989 - 2 - 12"),
                Genre = "Story",
                Price = 2.99M,
                Photo = "/img/fondo-pag-speciali.jpg",
                AlternateText = "Pranaya Rout Photo not available"
            };

            ListaMovies.Add(SecondoMovie);

            Movie TerzoMovie = new Movie()
            {
                Id = 3,
                Title = "Terzo",
                ReleaseDate = DateTime.Parse("1989 - 2 - 12"),
                Genre = "Comedy",
                Price = 10.99M,
                Photo = "/img/fondo-pag-speciali.jpg",
                AlternateText = "Pranaya Rout Photo not available"
            };

            ListaMovies.Add(TerzoMovie);

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowMovies()
        {
            PrendiDati();
            return View(ListaMovies);
        }

        



    }
}
