namespace TemplateProject.Entities;

public interface IBaseEntity
{
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ArchivedOn { get; set; }
}

public interface IEntity : IBaseEntity
{
    public Guid Id { get; set; }
}