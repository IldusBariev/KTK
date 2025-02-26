namespace APIshka.Helpers;

public class PasswordHasherHelper
{
    public string HashPassword(string value) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(value);

    public bool VerifyPasswordHash(string pass, string passHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(pass, passHash);
   
}
