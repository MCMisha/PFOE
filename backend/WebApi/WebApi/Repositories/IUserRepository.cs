namespace DefaultNamespace;

public interface IUserRepository
{
    User GetUserByUserName(string userName);
}