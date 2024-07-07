using System;

namespace CleanArchitecture.Domain.Constants;

public static class Ids
{
    public static class Seed
    {
        public static readonly Guid UserId = new("7e3892c0-9374-49fa-a3fd-53db637a40ae");
        public static readonly Guid TenantId = new("b542bf25-134c-47a2-a0df-84ed14d03c4a");
        public static readonly Guid ResearchGroupId = new("b542bf25-134c-47a2-a0df-84ed14d03c41");
        public static readonly Guid ResearchLineId = new("b542bf25-134c-47a2-a0df-84ed14d03c42");
        public static readonly Guid StudentId = new("b542bf25-134c-47a2-a0df-84ed14d03c62");
        public static readonly Guid ProfessorId = new("b542bf25-134c-47a2-a0df-84ed14d03c66");
        public static readonly Guid AppointmentId = new("b542bf25-134c-47a2-a0df-84ed14d03c69");
        public static readonly Guid AdvisoryContractId = new("b542bf25-134c-47a2-a0df-84ed14d03c63");
    }
}