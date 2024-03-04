namespace Shared;

public interface IUserService<TUser,TLoginRequest,TLoginReponse> where TUser : class where TLoginRequest : class where TLoginReponse : class
{
    public Task<PageReponse<TUser>> GetUsers(int index, int count);
    public Task<TUser> GetUserById(long id);
    public Task<TUser> PostUser(TUser user);
    public Task<TLoginReponse?> Login(TLoginRequest loginRequest);
    public Task<TUser> PutUser(long id, TUser user);
    public Task<bool> DeleteUser(long id);
}