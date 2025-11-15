public interface IRoleEntityFactory
{
    Task CreateForUserAsync(User user);
}