namespace SE_training.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly IDatabaseContext _context;

    public UserRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<(Status status, UserDTO user)> CreateAsync(CreateUserDTO user)
    {   
        UserDTO? exists = null;
        if (user.Email != null)
        {
            exists = await ReadAsync(user.Email);
        }
        if (exists != null)
        {
            return (Status.Conflict, exists);
        }

        var entity = new User() 
        {
            Name = user.Name,
            Email = user.Email
        };  
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        var details = new UserDTO(entity.Id, entity.Name, entity.Email);
        return (Created, details);
    }

    public async Task<UserDTO> ReadAsync(int userId)
    {
        var userDTO = await _context.Users
                            .Where(u => u.Id == userId)
                            .Select(u => new UserDTO(u.Id, u.Name, u.Email))
                            .SingleOrDefaultAsync();
        return userDTO;
    }

    public async Task<UserDTO> ReadAsync(string userEmail)
    {
        var users = from u in _context.Users
                    where u.Email == userEmail
                    select new UserDTO(u.Id, u.Name, u.Email);

        return await users.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<UserDTO>> ReadAllAsync()
    {
        var users = (await _context.Users
                            .Select(u => new UserDTO(u.Id, u.Name, u.Email))
                            .ToListAsync()).AsReadOnly();

        return users;
    }
}