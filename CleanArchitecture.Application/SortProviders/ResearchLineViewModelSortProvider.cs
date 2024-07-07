using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class ResearchLineViewModelSortProvider : ISortingExpressionProvider<ResearchLineViewModel, ResearchLine>
{
    private static readonly Dictionary<string, Expression<Func<ResearchLine, object>>> s_expressions = new()
    {
        { "id", ResearchLine => ResearchLine.Id },
        { "name", ResearchLine => ResearchLine.Name }
        
    };

    public Dictionary<string, Expression<Func<ResearchLine, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}