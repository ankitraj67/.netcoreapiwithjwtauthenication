using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Dto;
using V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Data
{
    public interface IRepository
    {
       

        IEnumerable<Movie> GetMovies();
        bool AddActor(Actor actor);
        IEnumerable<Actor> GetActors();
        Actor GetActor(int id);
        bool updateActor(Actor actor);
        bool DeleteActor(Actor actor);
        Movie GetMovie(int id);
        bool AddMovie(MovieDto movie);
        bool UpdateMovie(MovieDto movie);
        bool DeleteMovie(Movie movie);
       IEnumerable<Movie> GetMovieByActor(int actorId);
        IEnumerable<Movie> GetMoviesByGenre(Genre genreId);
        IEnumerable<Actor> GetActorByMovie(int movieId);
    }
}
