using Microsoft.AspNetCore.Mvc;
using APIshka.DbContexts;
using APIshka.Request;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using APIshka.Entities;
using System.Text;

namespace APIshka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Users : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public Users(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("login")] 
        public IActionResult AuthorizationAsync(UserLoginRequest request)
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

        [Authorize]
        [HttpPost("addUser")]
        public async Task<IActionResult> AddNewUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || 
                string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Username or password empty");
            }

            var isUsernameExsist = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (isUsernameExsist != null)
                return BadRequest("Username is exsist");

            await _dbContext.Users.AddAsync(new User(
                username,
                HashPassword(password)));
            await _dbContext.SaveChangesAsync();

            return Created();
        }

        //Для генерации токенов
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Username == username && u.PasswordHash == HashPassword(password));
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString(), ClaimTypes.NameIdentifier),
                    new Claim(ClaimTypes.Name, user.Username, ClaimsIdentity.DefaultNameClaimType)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, 
                    "Token", 
                    ClaimsIdentity.DefaultNameClaimType, 
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }

        //Происходит хэширование пароля
        private static string HashPassword(string password)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password))
                );
        }

    }
}
