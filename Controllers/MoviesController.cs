using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Data;
using V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Dto;
using V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        IRepository repository;
        public MoviesController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Movies
        [HttpGet(Name = "GetMovies")]
        public IActionResult Get()
        {
            return Ok(repository.GetMovies());
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Get(int id)
        {

            var movie = repository.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }


        // POST: api/Movies
        [HttpPost]
        public IActionResult Post([FromBody] MovieDto movie)
        {
            if (ModelState.IsValid)
            {
                bool result = repository.AddMovie(movie);
                return Created("AddMovie", movie);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MovieDto movie)
        {
            if (ModelState.IsValid && id == movie.Movie.Id)
            {
                bool result = repository.UpdateMovie(movie);
                if (result)
                {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest(ModelState);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = repository.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            bool result = repository.DeleteMovie(movie);
            if (result)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpGet("actor/{id}")]
       
        public IActionResult MoviesByActor(int id)
        {
            var movies = repository.GetMovieByActor(id);
            if (!movies.Any())
            {
                return NoContent();
            }
            return Ok(movies);
        }

        [HttpGet("genre/{id}")]
         public IActionResult MoviesByGenre(int id)
        {
            bool isValid = Enum.IsDefined(typeof(Genre), id);
            if (!isValid)
            {
                return BadRequest("Invalid Genre");
            }
            
            var movies = repository.GetMoviesByGenre((Genre)id);
            if (!movies.Any())
            {
                return NoContent();
            }
            return Ok(movies);
        }
       
    }
}
