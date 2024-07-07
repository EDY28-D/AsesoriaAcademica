using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

    public class ResearchGroup : Entity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
       public virtual ICollection<ResearchLine> ResearchLines { get; private set; } = new HashSet<ResearchLine>();
       public virtual ICollection<Professor> Professors { get; private set; } = new HashSet<Professor>();
    public ResearchGroup(Guid id, string name, string code) : base(id)
        {
            Name = name;
            Code = code;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    public void SetCode(string code)
    {
        Code = code;
    }
}

