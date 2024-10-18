using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HTTP5125Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J1Controller : ControllerBase
    {

        /// <summary>
        /// Calculate the final score for a robot droid when delivering packages while avoiding obstacles
        /// </summary>
        /// <param name="collisions">
        /// {collisions} is the number of collisions with an obstacle
        /// </param>
        /// <param name="deliveries">
        /// {deliveries} is the number of packages delivered
        /// </param>
        /// <returns>
        /// The final score in integer
        /// </returns>
        /// <example>
        /// POST: https://localhost:xx/api/j1/delivedroid
        /// HEADERS: Content-Type: application/x-www-form-urlencoded
        /// BODY: Collisions=2&Deliveries=5
        /// -> 730
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "Collisions=2&Deliveries=5" "https://localhost:xx/api/j1/delivedroid"
        /// </example>
        [HttpPost(template: "Delivedroid")]
        [Consumes("application/x-www-form-urlencoded")]
        public int Delivedroid(
            [FromForm][Range(0, int.MaxValue, ErrorMessage = "Only positive integer allowed.")] int collisions,
            [FromForm][Range(0, int.MaxValue, ErrorMessage = "Only positive integer allowed.")] int deliveries)
        {
            int score = deliveries * 50 - collisions * 10 + (deliveries > collisions ? 500 : 0);
            return score;
        }

        /// <summary>
        /// 2022 CCC J1, caculate how many cupcakes will be left over if each students (28) gets one cupcake
        /// </summary>
        /// <param name="regulars">
        /// {regulars} is the number of regular boxes of cupcakes which holds 8 cupcakes
        /// </param>
        /// <param name="smalls">
        /// {smalls} is the number of small boxes of cupcakes which holds 3 cupcakes
        /// </param>
        /// <returns>
        /// The number of cupcakes left overed in integer
        /// </returns>
        /// <example>
        /// POST: https://localhost:xx/api/j1/cupcakeparty
        /// HEADERS: Content-Type: application/x-www-form-urlencoded
        /// BODY: Regulars=2&Smalls=5
        /// -> 3
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "Regulars=2&Smalls=5" "https://localhost:xx/api/j1/cupcakeparty"
        /// </example>
        [HttpPost(template: "CupcakeParty")]
        [Consumes("application/x-www-form-urlencoded")]
        public int CupcakeParty(
            [FromForm][Range(0, int.MaxValue, ErrorMessage = "Only positive integer allowed.")] int regulars,
            [FromForm][Range(0, int.MaxValue, ErrorMessage = "Only positive integer allowed.")] int smalls)
        {
            int leftOver = regulars * 8 + smalls * 3 - 28;
            return leftOver;
        }
    }
}
