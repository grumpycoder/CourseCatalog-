﻿using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetProgramList
{
    public class GetProgramListQuery : IRequest<List<ProgramListDto>>
    {
    }
}
