namespace API_Dto;

public interface IUserService
{
    public Task<PageReponseDto<UserDto>> GetUsers(int index, int count);
    public Task<UserDto> GetUserById(long id);
    public Task<UserDto> PostUser(UserDto user);
    public Task<LoginResponseDto?> Login(LoginRequestDto loginRequest);
    public Task<UserDto> PutUser(long id, UserDto user);
    public Task<bool> DeleteUser(long id);
}