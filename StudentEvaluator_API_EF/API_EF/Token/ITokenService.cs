using EF_Entities;

namespace API_EF.Token;

public interface ITokenService
{
    /// <summary>
    /// Creates a JWT token for the given user.
    /// </summary>
    /// <param name="user">The user to create the token for.</param>
    /// <returns>The JWT token.</returns>
    string CreateToken(TeacherEntity user);
}