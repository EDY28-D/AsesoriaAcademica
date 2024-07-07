using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class StudentViewModelSortProvider : ISortingExpressionProvider<StudentViewModel, Student>
{
    private static readonly Dictionary<string, Expression<Func<Student, object>>> s_expressions = new()
    {
        { "id", Student => Student.Id },
        { "code", Student => Student.Code },
        { "userId", Student => Student.UserId}

    };

    public Dictionary<string, Expression<Func<Student, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}