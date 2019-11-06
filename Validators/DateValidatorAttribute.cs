using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

    namespace V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Models
    {
        public class DateValidatorAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value,
                ValidationContext validationContext)
            {
                var instance = validationContext.ObjectInstance;
                if (instance is Movie)
                {
                    var movie = instance as Movie;
                    if (movie.ReleaseDate >= DateTime.Now)
                    {
                        return new ValidationResult("Relese Date Should be in the past",
                            new[] { nameof(movie.ReleaseDate) });
                    }
                }
                else if (instance is Actor)
                {
                    var actor = instance as Actor;
                    if (actor.DateOfBirth >= DateTime.Now)
                    {
                        return new ValidationResult("Date of Birth should be in the past",
                            new[] { nameof(actor.DateOfBirth) });
                    }
                }
                return ValidationResult.Success;
            }
        }
    }

