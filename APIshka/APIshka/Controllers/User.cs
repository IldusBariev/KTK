using Microsoft.AspNetCore.Mvc;
using APIshka.DbContexts;
using APIshka.Request;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace APIshka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class User : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public User(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("login")] 
        public IActionResult Authorization(UserLoginRequest request)
        {
            string username = request.Username;
            string password = request.Password;

            var idenity = GetIdentity(username, password);

            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Имя пользователя не может быть пустым");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Пароль не может быть пустым");
            }


            if (idenity == null)
            {
                return NotFound("Не правильный логин или пароль");
            }

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: idenity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),

                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

            var encodedJwt  = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
            };

            return Ok(response);
        }


        //Для генерации токенов
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("id", user.UserId.ToString(), ClaimTypes.NameIdentifier),
                    new Claim("username", user.Username, ClaimsIdentity.DefaultNameClaimType)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, 
                    "Token", 
                    ClaimsIdentity.DefaultNameClaimType, 
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }


    }
}
