namespace CleanProject.CoreApplication.Domain;

public interface IBook
{
    public string Author { get; set; }
    public string Title { get; set; }
    public int YearPublished { get; set; }
}