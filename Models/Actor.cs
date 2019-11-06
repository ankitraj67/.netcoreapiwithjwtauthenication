using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date Of Birth")]
        [DateValidator]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
