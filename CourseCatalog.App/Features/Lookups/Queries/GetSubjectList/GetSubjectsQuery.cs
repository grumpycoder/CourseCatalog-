﻿using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetSubjectList
{
    public class GetSubjectListQuery : IRequest<List<Subject>>
    {

    }
}