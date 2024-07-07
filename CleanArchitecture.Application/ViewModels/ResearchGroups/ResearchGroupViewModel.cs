using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.ResearchGroups;

public sealed class ResearchGroupViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static ResearchGroupViewModel FromResearchGroup(ResearchGroup ResearchGroup)
    {
        return new ResearchGroupViewModel
        {
            Id = ResearchGroup.Id,
            Name = ResearchGroup.Name,
           //Users = ResearchGroup.Users.Select(UserViewModel.FromUser).ToList()
        };
    }
}