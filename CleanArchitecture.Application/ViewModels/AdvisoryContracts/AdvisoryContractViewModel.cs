using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.AdvisoryContracts;

public sealed class AdvisoryContractViewModel
{
    public Guid AdvisoryContractId { get; set; }
    public Guid ProfessorId { get;  set; }

    public Guid StudentId { get; set; }

    public Guid ResearchLineId { get;  set; }
    public string ThesisTopic { get; set; }
    public string Message { get;  set; }
    public string Status { get;  set; }
    public StudentViewModel Student { get; set; }
    public ProfessorViewModel Professor { get;  set; }
    public ResearchLineViewModel ResearchLine { get; set; }


    public static AdvisoryContractViewModel FromAdvisoryContract(AdvisoryContract AdvisoryContract)
    {
        return new AdvisoryContractViewModel
        {
           AdvisoryContractId = AdvisoryContract.Id,
           ProfessorId = AdvisoryContract.ProfessorId,
           StudentId = AdvisoryContract.StudentId,
           ResearchLineId = AdvisoryContract.ResearchLineId,
           ThesisTopic = AdvisoryContract.ThesisTopic,
           Message = AdvisoryContract.Message,
            Student = StudentViewModel.FromStudent(AdvisoryContract.Student),
            Professor=ProfessorViewModel.FromProfessor(AdvisoryContract.Professor),
            ResearchLine=ResearchLineViewModel.FromResearchLine(AdvisoryContract.ResearchLine),
        };
    }
}