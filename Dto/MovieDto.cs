using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_Ef_CodeFirst_identity_JWTToken.Dto
{
    public class MovieDto
    {
        public Movie Movie { get; set; }
        public IEnumerable<int> Actors { get; set; }
    }
}
