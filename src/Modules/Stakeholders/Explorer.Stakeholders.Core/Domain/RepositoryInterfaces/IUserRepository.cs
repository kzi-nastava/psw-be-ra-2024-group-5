namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

public interface IUserRepository
{
    bool Exists(string username);
    User? GetActiveByName(string username);
    User Create(User user);
    long GetPersonId(long userId);
    public bool UserExistsById(long userId);

    Result<object> GetUserById(long userId);

}