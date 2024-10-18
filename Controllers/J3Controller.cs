using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace HTTP5125Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J3Controller : ControllerBase
    {
        /// <summary>
        /// Convert the single line instructions into human readable outputs
        /// </summary>
        /// <param name="instructions">
        /// {instructions} is the single line instructions in string
        /// </param>
        /// <returns>
        /// List of human readable instructions
        /// </returns>
        /// <example>
        /// POST: https://localhost:xx/api/j3/harptuning
        /// HEADERS: Content-Type: application/json
        /// BODY: "AFB+8HC-4"
        /// -> ["AFB tighten 8","HC loosen 4"]
        /// curl -H "Content-Type: application/json" -d "\"AFB+8HC-4\"" "https://localhost:xx/api/j3/harptuning"
        /// </example>
        [HttpPost(template: "HarpTuning")]
        [Consumes("application/json")]
        public List<string> HarpTuning([FromBody] string instructions)
        {
            byte[] instructionsBytes = Encoding.ASCII.GetBytes(instructions);
            Nullable<byte> prevByte = null;
            List<string> outputs = [];

            foreach (byte b in instructionsBytes)
            {
                switch (b)
                {
                    // For uppercase letters
                    case >= 65 and <= 90:
                        // If reading the first byte or prev byte is digit
                        if (prevByte == null || (prevByte >= 48 && prevByte <= 57))
                        {
                            // Add a new line to output
                            outputs.Add("");
                        }

                        // Append the char to the last line of output
                        outputs[^1] += Convert.ToChar(b);
                        break;
                    // For "+" or "-"
                    case 43 or 45:
                        // Append " tighten " if b == "+" else " loosen " to the last line of output
                        outputs[^1] += (b == 43 ? " tighten " : " loosen ");
                        break;
                    // For digits
                    case >= 48 and <= 57:
                        // Append the char to the last line of output
                        outputs[^1] += Convert.ToChar(b);
                        break;
                    default:
                        break;
                }

                prevByte = b;
            }

            return outputs;
        }
    }
}
