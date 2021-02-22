﻿namespace CourseCatalog.Domain.Entities
{
    public class CourseEndorsement
    {
        public int CourseEndorsementId { get; set; }
        public int EndorsementId { get; set; }
        public int CourseId { get; set; }
        public Endorsement Endorsement { get; set; }
    }
}