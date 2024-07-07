using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class ResearchGroupViewModelSortProvider : ISortingExpressionProvider<ResearchGroupViewModel, ResearchGroup>
{
    private static readonly Dictionary<string, Expression<Func<ResearchGroup, object>>> s_expressions = new()
    {
        { "id", ResearchGroup => ResearchGroup.Id },
        { "name", ResearchGroup => ResearchGroup.Name }
        
    };

    public Dictionary<string, Expression<Func<ResearchGroup, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}