﻿namespace CourseCatalog.Domain.Entities
{
    public class ProgramDraft
    {
        public int ProgramDraftId { get; set; }
        public int DraftId { get; set; }
        public int ProgramId { get; set; }
        public Draft Draft { get; set; }
        public Program Program { get; set; }
    }
}