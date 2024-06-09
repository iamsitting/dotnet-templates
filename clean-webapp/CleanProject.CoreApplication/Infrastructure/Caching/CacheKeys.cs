namespace CleanProject.CoreApplication.Constants;

public static class CacheKeys
{
    public enum Domain
    {
        Templates,
        Books
    }

    public enum CacheType
    {
        All,
        First,
        Id,
        Name
    }
}