namespace CleanProject.CoreApplication.Infrastructure;

public interface IAppLogger<T>
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception ex);
}