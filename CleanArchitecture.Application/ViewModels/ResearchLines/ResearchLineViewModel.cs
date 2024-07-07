using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.ResearchLines;

public sealed class ResearchLineViewModel
{
    public Guid Id { get; set; }
    public Guid ResearchGroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public static ResearchLineViewModel FromResearchLine(ResearchLine ResearchLine)
    {
        return new ResearchLineViewModel
        {
            Id = ResearchLine.Id,
            ResearchGroupId=ResearchLine.ResearchGroupId,
            Name = ResearchLine.Name,
            Code = ResearchLine.Code,
        };
    }
}