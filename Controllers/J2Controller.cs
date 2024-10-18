using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HTTP5125Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J2Controller : ControllerBase
    {
        public static readonly Dictionary<string, int> PepperToSHU = new() {
            { "Poblano", 1500 },
            { "Mirasol", 6000 },
            { "Serrano", 15500 },
            { "Cayenne", 40000 },
            { "Thai", 75000 },
            { "Habanero", 125000 }
        };

        /// <summary>
        /// Calculate the total spiciness from ingredients
        /// </summary>
        /// <param name="ingredients">
        /// {ingredients} is a string of ingredients delimited by ','
        /// </param>
        /// <returns>
        /// Total spiciness in integer
        /// </returns>
        /// <example>
        /// GET: https://localhost:xx/api/j2/chilipeppers?ingredients=Poblano,Cayenne,Thai,Poblano -> 118000
        /// </example>
        [HttpGet(template: "ChiliPeppers")]
        public int ChiliPeppers(string ingredients)
        {
            int totalSpiciness = 0;

            foreach (string ingredient in ingredients.Split(','))
            {
                totalSpiciness += PepperToSHU[ingredient];
            }

            return totalSpiciness;
        }

        public class FergusonballPlayer
        {
            [Range(0, int.MaxValue, ErrorMessage = "Only positive integer allowed.")]
            public required int NumberOfPoints { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "Only positive integer allowed.")]
            public required int NumberOfFouls { get; set; }
        }

        public class FergusonballTeam
        {
            public required List<FergusonballPlayer> Players { get; set; }
        }

        /// <summary>
        /// Calculate the number of gold players inside a team and determine if the team is gold team
        /// </summary>
        /// <param name="team">
        /// {team} is the team to be determined, consists of players
        /// </param>
        /// <returns>
        /// A string consists of number of gold players and plus sign if the team is gold team
        /// </returns>
        /// <example>
        /// POST: https://localhost:xx/api/j2/fergusonballratings
        /// HEADERS: Content-Type: application/json
        /// BODY: "{ "players": [ { "numberOfPoints": 12, "numberOfFouls": 4 }, { "numberOfPoints": 10, "numberOfFouls": 3 }, { "numberOfPoints": 9, "numberOfFouls": 1 } ] }"
        /// -> "3+"
        /// curl -H "Content-Type: application/json" -d "{ \"players\": [ { \"numberOfPoints\": 12, \"numberOfFouls\": 4 }, { \"numberOfPoints\": 10, \"numberOfFouls\": 3 }, { \"numberOfPoints\": 9, \"numberOfFouls\": 1 } ] }" "https://localhost:xx/api/j2/fergusonballratings"
        /// </example>
        [HttpPost(template: "FergusonballRatings")]
        [Consumes("application/json")]
        public string FergusonballRatings([FromBody] FergusonballTeam team)
        {
            int goldPlayers = 0;

            foreach (FergusonballPlayer player in team.Players)
            {
                int starRating = player.NumberOfPoints * 5 - player.NumberOfFouls * 3;

                if (starRating > 40)
                {
                    goldPlayers++;
                }
            }

            return $"{goldPlayers}{(goldPlayers == team.Players.Count ? "+" : "")}";
        }
    }
}
