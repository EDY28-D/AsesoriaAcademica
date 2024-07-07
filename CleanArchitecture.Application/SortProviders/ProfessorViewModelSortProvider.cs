using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class ProfessorViewModelSortProvider : ISortingExpressionProvider<ProfessorViewModel, Professor>
{
    private static readonly Dictionary<string, Expression<Func<Professor, object>>> s_expressions = new()
    {
        { "id", Professor => Professor.Id },
        { "isCoordinator", Professor => Professor.IsCoordinator},
        { "userId", Professor => Professor.UserId},
        { "researchGroupId", Professor => Professor.ResearchGroupId}

    };

    public Dictionary<string, Expression<Func<Professor, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}