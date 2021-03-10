﻿using AutoMapper;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraft;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftByCourseId;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftProgram;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftRequirement;
using CourseCatalog.App.Features.Drafts.Commands.DeleteDraft;
using CourseCatalog.App.Features.Drafts.Commands.DeleteDraftProgram;
using CourseCatalog.App.Features.Drafts.Commands.DeleteRequirement;
using CourseCatalog.App.Features.Drafts.Commands.PublishDraft;
using CourseCatalog.App.Features.Drafts.Commands.UpdateDraft;
using CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail;
using CourseCatalog.App.Filters;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/drafts")]
    public class DraftsController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly CourseDbContext _context;
        private readonly IPublishCourseService _publishCourseService;
        private readonly IMapper _mapper;

        public DraftsController(IMediator mediator, CourseDbContext context, IPublishCourseService publishCourseService, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _publishCourseService = publishCourseService;
            _mapper = mapper;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var list = await DataSourceLoader.LoadAsync(_context.DraftsView, loadOptions);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("{draftId}")]
        public async Task<IHttpActionResult> Get(int draftId)
        {
            var dto = await _mediator.Send(new GetDraftDetailQuery(draftId));
            return Ok(dto);
        }

        [HttpPost, Route, CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> UpdateDraft([FromBody] UpdateDraftCommand updateDraftCommand)
        {
            var dto = await _mediator.Send(updateDraftCommand);
            return Ok(dto);
        }

        [HttpPost, Route("create"), CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> CreateDraft([FromBody] CreateDraftCommand createDraftCommand)
        {
            var dto = await _mediator.Send(createDraftCommand);
            return Ok(dto);
        }

        [HttpPost, Route("{draftId}"), CustomAuthorize(Roles = "CourseAdmin, TeacherCertAdmin, CareerTechAdmin, Admin")]
        public async Task<IHttpActionResult> Delete(int draftId)
        {
            var dto = await _mediator.Send(new DeleteDraftCommand(draftId));
            return Ok(dto);
        }

        [HttpPost, Route("createendorsement"), CustomAuthorize(Roles = "CourseAdmin, TeacherCertAdmin, Admin")]
        public async Task<IHttpActionResult> CreateDraftEndorsement([FromBody] CreateDraftEndorsementCommand createRequirementCommand)
        {
            var dto = await _mediator.Send(createRequirementCommand);
            return Ok(dto);
        }

        [HttpPost, CustomAuthorize(Roles = "CourseAdmin, TeacherCertAdmin, Admin")]
        [Route("{draftId}/endorsements/{endorsementId}")]
        public async Task<IHttpActionResult> DeleteDraftEndorsement(int draftId, int endorsementId)
        {
            await _mediator.Send(new DeleteRequirementCommand(draftId, endorsementId));
            return Ok();
        }

        [HttpPost, Route("assignprogram"), CustomAuthorize(Roles = "CourseAdmin, CareerTechAdmin, Admin")]
        public async Task<IHttpActionResult> AssignProgram([FromBody] CreateDraftProgramCommand createDraftProgramCommand)
        {
            var dto = await _mediator.Send(createDraftProgramCommand);
            return Ok(dto);
        }

        [HttpPost, CustomAuthorize(Roles = "CourseAdmin, TeacherCertAdmin, CareerTechAdmin, Admin")]
        [Route("{draftId}/programs/{programId}")]
        public async Task<IHttpActionResult> RemoveProgram(int draftId, int programId)
        {
            await _mediator.Send(new DeleteDraftProgramCommand(draftId, programId));
            return Ok();
        }

        [HttpPost, Route("{courseId}/create"), CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> CreateDraft(int courseId)
        {
            var dto = await _mediator.Send(new CreateDraftByCourseIdCommand(courseId));
            return Ok(dto);
        }

        [HttpPost, Route("publish/{draftId}"), CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> Post(int draftId)
        {
            var dto = await _mediator.Send(new PublishDraftCommand(draftId));
            return Ok(dto);
        }

    }
}
