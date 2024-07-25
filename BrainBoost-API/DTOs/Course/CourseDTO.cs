using BrainBoost_API.DTOs.Answer;
using BrainBoost_API.DTOs.Question;
using BrainBoost_API.DTOs.Quiz;

namespace BrainBoost_API.DTOs.Course
{
    public class CourseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int TeacherId { get; set; }
        public string Language { get; set; }
        public string Level { get; set; }
        public string CategoryName { get; set; }
        public insertedQuiz Quiz { get; set; }
        public List<string> WhatToLearn { get; set; }
        public string? CertificateHeadline { get; set; }
        public string? CertificateAppreciationParagraph { get; set; }
    }
}
