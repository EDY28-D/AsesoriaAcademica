namespace CleanArchitecture.Domain.Constants;

public static class MaxLengths
{
    public static class User
    {
        public const int Email = 320;
        public const int FirstName = 100;
        public const int LastName = 100;
        public const int Password = 128;
    }

    public static class Tenant
    {
        public const int Name = 255;
    }
    public static class ResearchGroup
    {
        public const int Name = 250;
        public const int Code = 9;
    }
    public static class ResearchLine
    {
        public const int Name = 250;
        public const int Code = 9;
    }
    public static class Student
    {
        public const int Code = 9;
    }
    public static class Professor
    {
        public const int Code = 9;
    }
    public static class Appointment
    {
        public const int Code = 9;
    }
    public static class AdvisoryContract
    {
        public const int Status = 20;
    }
}