using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;
public class ResearchLine : Entity
    {
        public string Name { get; private set; }
        public Guid ResearchGroupId { get; private set; }
        public string Code { get; private set; }
        public virtual ResearchGroup ResearchGroup { get; private set; } = null!;

        public ResearchLine(Guid id, string name, Guid researchGroupId,string code) : base(id)
        {
         Name = name;
         ResearchGroupId = researchGroupId;
         Code = code;
        }

        public void SetName(string name)
        {
         Name = name;
        }

        public void SetResearchGroupId(Guid researchGroupId)
        {
            ResearchGroupId = researchGroupId;
        }
        public void SetCode(string code)
        {
            Code = code;
        }
    }

