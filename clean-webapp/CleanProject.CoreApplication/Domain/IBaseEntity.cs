namespace CleanProject.CoreApplication.Domain;

public interface IBaseEntity
{
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
}

public interface IWithKey<TKey>
{
    public TKey Id { get; set; }
}