using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIshka
{
    public class AuthOptions
    {
        public const string ISSUER = "http://localhost:5155"; // издатель токена
        public const string AUDIENCE = "http://localhost:5034"; // потребитель токена
        const string KEY = "gfhdfgreitpkljfdlsbvc4234bfdhf435FDLKFJGLRKJmvcdflkgjfdglk";   // ключ для шифрации
        public const int LIFETIME = 15; // время жизни токена - 15 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}
