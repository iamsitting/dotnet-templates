namespace CleanProject.CoreApplication.Domain;

public interface IUserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}