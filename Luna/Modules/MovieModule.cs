using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Luna.Data;
using Luna.Data.DomainClasses;

namespace Luna.Modules
{
    [Name("Movie")]
    public class MovieModule : ModuleBase<SocketCommandContext>
    {
        private readonly MovieRepository _repo;

        public MovieModule(MovieRepository repo)
        {
            _repo = repo;
        }

        [Command("movie")]
        [Summary("Request movie details based on a movie title.")]
        public async Task Movie([Remainder][Summary("title")] string movieTitle)
        {
            Movie movie = await _repo.GetMovieByNameAsync(movieTitle);
            string movieDetails = "";
            if (movie.Response == "True")
            {
                movieDetails = $"Title: {movie.Title}\n" +
                               $"Year: {movie.Year}\n" +
                               $"Released: {movie.Released}\n" +
                               $"Genre: {movie.Genre}\n" +
                               $"Actors: {movie.Actors}\n";
            }
            else
            {
                movieDetails = movieTitle + " could not be found in the database, please try again.";
            }
            await ReplyAsync(movieDetails);
        }

    }
}
