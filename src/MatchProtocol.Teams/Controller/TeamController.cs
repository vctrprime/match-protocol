using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchProtocol.Teams.Controller
{
    [ApiController]
    [Route("[controller]")]
    
    public class TeamController : ControllerBase
    {
        [HttpGet("A")]
        [Authorize("clientIdPolicy")]
        public IEnumerable<Team> GetA()
        {
            var u = Request.HttpContext.User;
            return new List<Team>
            {
                new Team
                {
                    Id = 1,
                    Name = "Милан"
                },
                new Team
                {
                    Id = 2,
                    Name = "Наполи"
                }
            };
        }
        
        [HttpGet("B")]
        [Authorize("anotherServicePolicy")]
        public IEnumerable<Team> GetB()
        {
            var u = Request.HttpContext.User;
            return new List<Team>
            {
                new Team
                {
                    Id = 1,
                    Name = "Интер"
                },
                new Team
                {
                    Id = 2,
                    Name = "Ювентус"
                }
            };
        }
        
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}