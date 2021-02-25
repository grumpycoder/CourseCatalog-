﻿using MediatR;

namespace MyDemo.Api.Features.Programs.Commands.CreateProgram
{
    public class CreateProgramCommand : IRequest<int>
    {
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public bool? TraditionalForMales { get; set; }
        public bool? TraditionalForFemales { get; set; }

        public int ProgramTypeId { get; set; }
        public int ClusterId { get; set; }
    }
}
