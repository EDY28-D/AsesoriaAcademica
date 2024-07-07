using System;

namespace CleanArchitecture.Domain.Entities
{
    public class Student : Entity
    {
        public string Code { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; } = null!;

        public Student(Guid id, Guid userId, string code) : base(id)
        {
            Code = code;
            UserId = userId;
        }

        public void SetCode(string code)
        {
            Code = code;
        }

        public void SetUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
