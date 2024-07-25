using BrainBoost_API.DTOs.Course;
using BrainBoost_API.DTOs.Quiz;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IUnitOfWork UnitOfWork;

        public QuizController(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        [HttpGet("ChangeQuizState/{id:int}")]
        public async Task<IActionResult> ChangeQuizState(int id, [FromQuery] string status)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Student std = UnitOfWork.StudentRepository.Get(c => c.UserId == UserID);

                var enrolledCourse = UnitOfWork.StudentEnrolledCoursesRepository.Get(c => c.StudentId == std.Id && c.CourseId == id);
                enrolledCourse.QuizState = true;
                UnitOfWork.StudentEnrolledCoursesRepository.update(enrolledCourse);
                UnitOfWork.save();

                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetQuizByCourseId/{id:int}")]
        public async Task<IActionResult> GetQuizByCourseId(int id)
        {
            if (ModelState.IsValid)
            {
                Quiz retrievedQuiz = UnitOfWork.QuizRepository.Get(c => c.CourseId == id, "Questions");
                if (retrievedQuiz != null)
                {
                    List<Question> questions = new List<Question>();
                    foreach (var question in retrievedQuiz.Questions)
                    {
                        Question retrievedQuestion = UnitOfWork.QuestionRepository.Get(q=>q.Id == question.QuestionId,"Answers");
                        foreach (var Answer in retrievedQuestion.Answers)
                        {
                            Answer.Question = null;
                        }
                        retrievedQuestion.Quizzes=null;
                        questions.Add(retrievedQuestion);
                    }
                    return Ok(questions);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut("UpdateCourseQuiz/{courseId:int}")]
        public async Task<IActionResult> UpdateCourseQuiz(editedQuizz editedQuizz, int courseId)
        {
            if (ModelState.IsValid)
            {
                Quiz quiz = UnitOfWork.QuizRepository.Get(c => c.CourseId == courseId);
                if (quiz != null)
                {
                    quiz.NumOfQuestions = editedQuizz.Questions.Count;
                    foreach (var Question in editedQuizz.Questions)
                    {
                        Question retrievedQuestion = UnitOfWork.QuestionRepository.Get(q => q.Id == Question.Id, "Answers");
                        if (retrievedQuestion != null)
                        {
                            retrievedQuestion.Content = Question.HeadLine;
                            retrievedQuestion.Degree = Question.Degree;
                            foreach (var item in Question.Choices)
                            {
                                Answer retrievedAnswer = UnitOfWork.AnswerRepository.Get(a => a.Id == item.Id);
                                if (retrievedAnswer != null)
                                {
                                    retrievedAnswer.Content = item.Choice;
                                    retrievedAnswer.IsCorrect = item.isCorrect;
                                }
                                else
                                {
                                    Answer newAnswer = new Answer()
                                    {
                                        Content = item.Choice,
                                        IsCorrect = item.isCorrect,
                                        QuestionId = retrievedQuestion.Id
                                    };
                                    UnitOfWork.AnswerRepository.add(newAnswer);
                                }
                            }
                            UnitOfWork.save();
                        }
                        else
                        {
                            Question newQuestion = new Question()
                            {
                                Content = Question.HeadLine,
                                Degree = Question.Degree,
                                Type = Enums.QuestionType.MultipleChoice
                            };
                            UnitOfWork.QuestionRepository.add(newQuestion);
                            UnitOfWork.save();
                            UnitOfWork.QuizQuestionRepository.add(new QuizQuesitons
                            {
                                QuizId = quiz.Id,
                                QuestionId = newQuestion.Id,
                            });
                            UnitOfWork.save();
                            foreach (var item in Question.Choices)
                            {
                                Answer newAnswer = new Answer()
                                {
                                    Content = item.Choice,
                                    IsCorrect = item.isCorrect,
                                    QuestionId = newQuestion.Id
                                };
                                UnitOfWork.AnswerRepository.add(newAnswer);
                                UnitOfWork.save();
                            }
                        }
                    }
                    return Ok(new 
                    {
                       msg ="done"
                    });
                }
            }
            return BadRequest(ModelState);
        }

    }
}
