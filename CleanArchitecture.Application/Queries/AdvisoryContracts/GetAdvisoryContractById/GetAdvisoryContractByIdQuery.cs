using System;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using MediatR;

namespace CleanArchitecture.Application.Queries.AdvisoryContracts.GetAdvisoryContractById;

public sealed record GetAdvisoryContractByIdQuery(Guid AdvisoryContractId) : IRequest<AdvisoryContractViewModel?>;