using CleanProject.Domain;
using User = CleanProject.Persistence.EF.Entities.Identity.User;

namespace CleanProject.Persistence.Extension;

public static class Mapper
{
    public static AppUser ToUser(this User user)
    {
        return new AppUser()
        {
            Id = user.Id,
            Email = user.Email,
        };
    }
}