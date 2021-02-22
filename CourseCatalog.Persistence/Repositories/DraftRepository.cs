﻿using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.Persistence.Repositories
{
    public class DraftRepository : BaseRepository<Draft>, IDraftRepository
    {
        public DraftRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Draft> GetDraftByCourseNumber(string courseNumber)
        {
            var draft = await _dbContext.Drafts.FirstOrDefaultAsync(c => c.CourseNumber == courseNumber);

            return draft;
        }

        public async Task<Draft> GetDraftWithDetails(int draftId)
        {
            var draft = await _dbContext.Drafts
                .Include(c => c.CourseLevel)
                .Include(c => c.Subject)
                .Include(c => c.HighGrade)
                .Include(c => c.LowGrade)
                .Include(c => c.GradeScale)
                .Include(c => c.ScedCategory)
                .FirstOrDefaultAsync(x => x.DraftId == draftId);

            return draft;
        }
    }
}