using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Dto;
using V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Data
{
    public class MovieRepository : IRepository
    {
        MovieContext context;
        public MovieRepository(MovieContext context)
        {
            this.context = context;
        }

        public bool AddActor(Actor actor)
        {
            try
            {
                context.Actors.Add(actor);
                var result = context.SaveChanges();
                if (result >0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddMovie(MovieDto movie)
        {
            try
            {
                context.Movies.Add(movie.Movie);
                foreach (var actorId in movie.Actors)
                {
                    context.MovieActors.Add(new MovieActor
                    {
                        Movie = movie.Movie,
                        Actor = context.Actors.Find(actorId)
                    });
                    context.SaveChanges();
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteActor(Actor actor)
        {
            try
            {
                context.Actors.Remove(actor);
                int result = context.SaveChanges();
                if (result>0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Actor GetActor(int id)
        {
            return context.Actors.Find(id);
        }

        public IEnumerable<Actor> GetActors()
        {
            return context.Actors.ToList();
        }

        public Movie GetMovie(int id)
        {
            return context.Movies.Find(id);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return context.Movies.Include(m => m.MovieActors);
        }

        public bool updateActor(Actor actor)
        {
            try
            {
                context.Actors.Update(actor);
                int result = context.SaveChanges();
                if (result >0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool UpdateMovie(MovieDto movie)
        {
            try
            {
                context.Movies.Update(movie.Movie);
                context.SaveChanges();
                var movieactors = context.MovieActors.
                    Where(ma => ma.Movie == movie.Movie);
                context.MovieActors.RemoveRange(movieactors);

                foreach (var actorId in movie.Actors)
                {
                    context.MovieActors.Add(new MovieActor
                    {
                        Movie = movie.Movie,
                        Actor = context.Actors.Find(actorId)
                    });
                    context.SaveChanges();
                }
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public bool DeleteMovie(Movie movie)
        {

            try
            {
                context.Movies.Remove(movie);
                var movieActors = context.MovieActors
                    .Where(ma => ma.Movie == movie);
                context.MovieActors.RemoveRange(movieActors);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Movie> GetMovieByActor(int actorId)
        {
            var movies = from m in context.Movies
                         join ma in context.MovieActors on m.Id
                         equals ma.MovieId
                         where ma.ActorId == actorId
                         select m;
            return movies;
            
        }

        public IEnumerable<Movie> GetMoviesByGenre(Genre genreId)
        {
            return context.Movies.Where(m => m.Genre == (Genre)genreId);
        }

        public IEnumerable<Actor> GetActorByMovie(int movieId)
        {
            var actors = from m in context.Actors
                         join ma in context.MovieActors on m.Id
                         equals ma.ActorId
                         where ma.ActorId == movieId
                         select m;
            return actors;
        }
    }
}
