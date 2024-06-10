using CleanProject.CoreApplication.Domain;

namespace CleanProject.Persistence.EF.Entities;

public sealed class Publisher : IWithKey<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}