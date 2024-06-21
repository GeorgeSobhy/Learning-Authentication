using jwt.Models;
using jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IAuth _auth;
        public authController(IAuth auth)
        {
            _auth = auth;
        }
        [Authorize]
        [HttpPost("Register")]
        public async Task<IActionResult> register(RegisterDTO DTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            if (await _auth.Register(DTO.FullName, DTO.Username, DTO.Password) == false)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            return NoContent();
                

        }
        [HttpPost("Login")]
        public async Task<IActionResult> login(LoginDTO DTO)
        {
            if (DTO.userName == null || DTO.password == null)
                return BadRequest("UserName or Password are wrong or both of them");

            return Ok(await _auth.login(DTO.userName, DTO.password));
        }
    }
}
