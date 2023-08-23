using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly DataContext _context;
        public RegistrationController(DataContext context) => _context = context;

        [HttpPost]
        public async Task<LoginModel> Registration([FromBody]LoginModel request)
        {
            if (request.Password == "admin")
            {
                request.IsAdmin = true;
            }
            _context.Users.Add(request);

            await _context.SaveChangesAsync();
            return request;
        }
    }
}
