using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace RazorPagesMovie.PagesMovies
{
    public class IndexModel : PageModel
    {
        private readonly MvcMovieContext _context;

        public IndexModel(MvcMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        public SelectList Ratings { get; set; }
        [BindProperty(SupportsGet = true)]
        public string MovieRating { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            IQueryable<string> ratingQuery = from m in _context.Movie
                                             orderby m.Rating
                                             select m.Rating;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            if (!string.IsNullOrEmpty(MovieRating))
            {
                movies = movies.Where(y => y.Rating == MovieRating);
            }
            Ratings = new SelectList(await ratingQuery.Distinct().ToListAsync());
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
