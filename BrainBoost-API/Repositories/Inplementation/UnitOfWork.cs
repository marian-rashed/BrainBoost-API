using AutoMapper;
using BrainBoost_API.Repositories.Interfaces;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext Context;
        public IVideoRepository VideoRepository { get; private set; }
        public IQuizRepository QuizRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public ICourseRepository CourseRepository { get; private set; }
        public ITeacherRepository TeacherRepository { get; private set; }
        public IReviewRepository ReviewRepository { get; private set; }
        public ISubscriptionRepository SubscriptionRepository { get; private set; }
        public IFacebookUserRepository FacebookUserRepository { get; private set; }
        public IPlanRepository PlanRepository { get; private set; }
        public IEnrollmentRepository EnrollmentRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IAnswerRepository AnswerRepository { get; private set; }
        public IQuestionRepository QuestionRepository { get; private set; }
        public IStudentEnrolledCoursesRepository StudentEnrolledCoursesRepository { get; private set; }
        public IVideoStateRepository VideoStateRepository { get; set; }
        public IEarningsRepository EarningsRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IWhatToLearnRepository WhatToLearnRepository { get; set; }
        public IAdminRepository AdminRepository { get; private set; }
        public IQuizQuestionRepository QuizQuestionRepository { get ; set ; }

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            this.Context = context;
            VideoRepository = new VideoRepository(context);
            QuizRepository = new QuizRepository(context, mapper);
            StudentRepository = new StudentRepository(context);
            CourseRepository = new CourseRepository(context, mapper);
            TeacherRepository = new TeacherRepository(context);
            ReviewRepository = new ReviewRepository(context);
            SubscriptionRepository = new SubscriptionRepository(context);
            FacebookUserRepository = new FacebookUserRepository(context);
            PlanRepository = new PlanRepository(context);
            EnrollmentRepository = new EnrollmentRepository(context);
            CategoryRepository = new CategoryRepository(context);
            AnswerRepository = new AnswerRepository(context);
            QuestionRepository = new QuestionRepository(context);
            StudentEnrolledCoursesRepository = new StudentEnrolledCoursesRepository(context);
            WhatToLearnRepository=new WhatToLearnRepository(context);
            VideoStateRepository = new VideoStateRepository(context, mapper);
            EarningsRepository = new EarningsRepository(context, mapper);
            CommentRepository = new CommentRepository(context, mapper);
            AdminRepository = new AdminRepository(context);
            QuizQuestionRepository = new QuizQuestionRepository(context);
        }

        public void save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
