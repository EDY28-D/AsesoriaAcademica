using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;


    public sealed class AdvisoryContractViewModelSortProvider : ISortingExpressionProvider<AdvisoryContractViewModel, AdvisoryContract>
    {
        private static readonly Dictionary<string, Expression<Func<AdvisoryContract, object>>> s_expressions = new()
        {
            { "id", contract => contract.Id },
            { "professorId", contract => contract.ProfessorId },
            { "studentId", contract => contract.StudentId },
            { "researchLineId", contract => contract.ResearchLineId },
            { "thesisTopic", contract => contract.ThesisTopic },
            { "message", contract => contract.Message },
            { "status", contract => contract.Status }
        };

        public Dictionary<string, Expression<Func<AdvisoryContract, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
