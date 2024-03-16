namespace Shared;

/// <summary>
/// Represents a service interface for user-related operations.
/// </summary>
/// <typeparam name="TUser">The type representing a user.</typeparam>
/// <typeparam name="TLoginRequest">The type representing a login request.</typeparam>
/// <typeparam name="TLoginResponse">The type representing a login response.</typeparam>
public interface IUserService<TUser,TLoginRequest,TLoginReponse> where TUser : class where TLoginRequest : class where TLoginReponse : class
{
    public Task<PageReponse<TUser>> GetUsers(int index, int count);
    public Task<TUser?> GetUserById(long id);
    public Task<TUser?> PostUser(TUser user);
    public Task<TLoginReponse?> Login(TLoginRequest loginRequest);
    public Task<TUser?> PutUser(long id, TUser user);
    public Task<bool> DeleteUser(long id);
}