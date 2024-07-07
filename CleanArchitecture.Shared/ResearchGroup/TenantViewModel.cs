using System;

namespace CleanArchitecture.Shared.Tenants;

public sealed record ResarchGroupViewModel(
    Guid Id,
    string Name);