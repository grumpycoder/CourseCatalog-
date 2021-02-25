﻿using AutoMapper;
using MediatR;
using MyDemo.Api.Application.Contracts;
using MyDemo.Api.Domain.Entities;
using MyDemo.Api.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemo.Api.Features.Programs.Commands.CreateProgram
{
    public class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IProgramRepository _programRepository;

        public CreateProgramCommandHandler(IMapper mapper, IProgramRepository programRepository)
        {
            _mapper = mapper;
            _programRepository = programRepository;
        }
        public async Task<int> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
        {
            var program = await _programRepository.GetProgramByProgramCode(request.ProgramCode);
            if (program != null)
            {
                throw new BadRequestException(
                    $"Duplicate Program Code. Existing program already contains Program Code {request.ProgramCode}");
            }

            program = _mapper.Map<Program>(request);

            program = await _programRepository.AddAsync(program);

            return program.ProgramId;
        }
    }
}
