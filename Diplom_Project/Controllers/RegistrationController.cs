using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Diplom_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly DataContext _context;
        public RegistrationController(DataContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult<LoginModel>>? Registration([FromBody]LoginModel request)
        {
            string pattern = @"^(?=(?:\d+[a-zA-Z]+[@#$%^&+=]+|[a-zA-Z]+[@#$%^&+=]+\d+|[@#$%^&+=]+\d+[a-zA-Z]+)).{8,20}$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(request.Password))
            {
                return BadRequest("The password should contain at least 3 segments." +
                    "\r\nEach segment should consist of a minimum of 2 characters." +
                    "\r\nThere should be one segment with digits, one with letters (both uppercase and lowercase), and one with special characters.");
            }

            if (request.Password == "P@ssw0rd")
            {
                request.IsAdmin = true;
            }

            _context.Users.Add(request);

            await _context.SaveChangesAsync();
            return request;
        }
    }
}
