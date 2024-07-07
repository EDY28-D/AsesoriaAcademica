namespace CleanArchitecture.Domain.Errors;

public static class ErrorCodes
{
    public const string CommitFailed = "COMMIT_FAILED";
    public const string ObjectNotFound = "OBJECT_NOT_FOUND";
    public const string InsufficientPermissions = "UNAUTHORIZED";
    public const string InvalidUserRole = "INVALID_USER_ROLE";
}