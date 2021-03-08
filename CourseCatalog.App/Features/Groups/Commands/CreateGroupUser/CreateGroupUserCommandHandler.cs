﻿using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Groups.Commands.CreateGroupUser
{
    public class CreateGroupUserCommandHandler : IRequestHandler<CreateGroupUserCommand, CreateGroupUserCommandDto>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public CreateGroupUserCommandHandler(IMapper mapper, IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<CreateGroupUserCommandDto> Handle(CreateGroupUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.IdentityGuid);

            if (user == null) throw new NotFoundException(nameof(User), request.IdentityGuid);

            var group = await _groupRepository.GetGroupByIdWithUsers(request.GroupId);

            if (group == null) throw new NotFoundException(nameof(Group), request.GroupId);

            var groupUser = new UserGroup { GroupId = request.GroupId, UserId = user.Id };

            group.Users.Add(groupUser);

            await _groupRepository.UpdateAsync(group);

            var dto = _mapper.Map<CreateGroupUserCommandDto>(groupUser);

            return dto;
        }
    }
}