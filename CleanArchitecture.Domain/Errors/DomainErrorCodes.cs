namespace CleanArchitecture.Domain.Errors
{
    public static class DomainErrorCodes
    {
        public static class User
        {
            // User Validation
            public const string EmptyId = "USER_EMPTY_ID";
            public const string EmptyFirstName = "USER_EMPTY_FIRST_NAME";
            public const string EmptyLastName = "USER_EMPTY_LAST_NAME";
            public const string EmailExceedsMaxLength = "USER_EMAIL_EXCEEDS_MAX_LENGTH";
            public const string FirstNameExceedsMaxLength = "USER_FIRST_NAME_EXCEEDS_MAX_LENGTH";
            public const string LastNameExceedsMaxLength = "USER_LAST_NAME_EXCEEDS_MAX_LENGTH";
            public const string InvalidEmail = "USER_INVALID_EMAIL";
            public const string InvalidRole = "USER_INVALID_ROLE";

            // User Password Validation
            public const string EmptyPassword = "USER_PASSWORD_MAY_NOT_BE_EMPTY";
            public const string ShortPassword = "USER_PASSWORD_MAY_NOT_BE_SHORTER_THAN_6_CHARACTERS";
            public const string LongPassword = "USER_PASSWORD_MAY_NOT_BE_LONGER_THAN_50_CHARACTERS";
            public const string UppercaseLetterPassword = "USER_PASSWORD_MUST_CONTAIN_A_UPPERCASE_LETTER";
            public const string LowercaseLetterPassword = "USER_PASSWORD_MUST_CONTAIN_A_LOWERCASE_LETTER";
            public const string NumberPassword = "USER_PASSWORD_MUST_CONTAIN_A_NUMBER";
            public const string SpecialCharPassword = "USER_PASSWORD_MUST_CONTAIN_A_SPECIAL_CHARACTER";

            // General
            public const string AlreadyExists = "USER_ALREADY_EXISTS";
            public const string PasswordIncorrect = "USER_PASSWORD_INCORRECT";
        }

        public static class Tenant
        {
            // Tenant Validation
            public const string EmptyId = "TENANT_EMPTY_ID";
            public const string EmptyName = "TENANT_EMPTY_NAME";
            public const string NameExceedsMaxLength = "TENANT_NAME_EXCEEDS_MAX_LENGTH";

            // General
            public const string AlreadyExists = "TENANT_ALREADY_EXISTS";
        }

        public static class ResearchGroup
        {
            // Tenant Validation
            public const string EmptyId = "RESEARCHGROUP_EMPTY_ID";
            public const string EmptyName = "RESEARCHGROUP_EMPTY_NAME";
            public const string NameExceedsMaxLength = "RESEARCHGROUP_NAME_EXCEEDS_MAX_LENGTH";

            // General
            public const string AlreadyExists = "RESEARCHGROUP_ALREADY_EXISTS";
        }

        public static class ResearchLine
        {
            // Tenant Validation
            public const string EmptyId = "RESEARCHLINE_EMPTY_ID";
            public const string EmptyName = "RESEARCHLINE_EMPTY_NAME";
            public const string NameExceedsMaxLength = "RESEARCHLINE_NAME_EXCEEDS_MAX_LENGTH";

            // General
            public const string AlreadyExists = "RESEARCHLINE_ALREADY_EXISTS";
        }

        public static class Student
        {
            // Student Validation
            public const string EmptyId = "STUDENT_EMPTY_ID";
            public const string EmptyCode = "STUDENT_EMPTY_CODE";
            public const string CodeExceedsMaxLength = "STUDENT_CODE_EXCEEDS_MAX_LENGTH";

            // General
            public const string AlreadyExists = "STUDENT_ALREADY_EXISTS";
        }

        public static class Professor
        {
            // Professor Validation
            public const string EmptyId = "PROFESSOR_EMPTY_ID";
            public const string EmptyCode = "PROFESSOR_EMPTY_CODE";
            public const string CodeExceedsMaxLength = "PROFESSOR_CODE_EXCEEDS_MAX_LENGTH";

            // General
            public const string AlreadyExists = "PROFESSOR_ALREADY_EXISTS";
        }

        public static class Appointment
        {
            // Appointment Validation
            public const string EmptyId = "APPOINTMENT_EMPTY_ID";
            public const string EmptyProfessorId = "APPOINTMENT_EMPTY_PROFESSOR_ID";
            public const string EmptyStudentId = "APPOINTMENT_EMPTY_STUDENT_ID";
            public const string EmptyCalendarId = "APPOINTMENT_EMPTY_CALENDAR_ID";

            // General
            public const string InsufficientPermissions = "INSUFFICIENT_PERMISSIONS";
            public const string AppointmentAlreadyExists = "APPOINTMENT_ALREADY_EXISTS";
            public const string NotFound = "APPOINTMENT_NOT_FOUND";
            public const string AlreadyExists = "APPOINTMENT_ALREADYEXISTS";
            
        }
        public static class AdvisoryContract
        {
            // Professor Validation
            public const string EmptyId = "ADVISORYCONTRACT_EMPTY_ID";
            public const string EmptyMessage = "ADVISORYCONTRACT_EMPTY_MESSAGE";
            public static string EmptyThesisTopic = "ADVISORYCONTRAC_EMPTY_THESISTOPIC";
            public const string ThesisTopicExceedsMaxLength = "ADVISORYCONTRACT_CODE_EXCEEDS_MAX_LENGTH";

            // General
            public const string AlreadyExists = "ADVISORYCONTRACT_ALREADY_EXISTS";
        }
    }
    
    

}
