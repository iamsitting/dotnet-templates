namespace CleanProject.CoreApplication.Constants;

public static class SystemRoles
{
    public const string Developer = "Developer";
    public const string Admin = "Admin";

    public static string[] AsArray() => [Developer, Admin];
}
