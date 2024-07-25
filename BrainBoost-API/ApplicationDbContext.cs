using BrainBoost_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BrainBoost_API
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<subscription> Subscriptions { get; set; }
        public DbSet<FacebookUser> FacebookUsers { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<WhatToLearn> WhatToLearn { get; set; }
        public DbSet<VideoState> videoStates { get; set; }
        public DbSet<Earnings> Earnings { get; set; }
        public DbSet<comment> Comments { get; set; }
        public DbSet<StudentEnrolledCourses> StudentEnrolledCourses { get; set; }
        public DbSet<StudentSavedCourses> StudentSavedCourses { get; set; }
        public DbSet<Admin> Admin { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Question>()
            //.HasMany(q => q.Answers)
            //.WithOne(a => a.Question)
            //.HasForeignKey(a => a.QuestionId);
            //builder.Entity<Question>()
            //.HasOne(q => q.TrueAnswer)
            //.WithMany()
            //.HasForeignKey(q => q.TrueAnswerId)
            //.OnDelete(DeleteBehavior.NoAction);
            //////not returning

            //foreach (var model in builder.Model.GetEntityTypes())
            //{
            //    builder.Entity(model.Name).Property<bool>("IsDeleted").HasDefaultValue(false);

            //}
            builder.Entity<Teacher>().HasIndex(Teacher => Teacher.UserId).IsUnique();
            foreach (var model in builder.Model.GetEntityTypes())
            {
                var isDeletedProperty = model.FindProperty("IsDeleted");
                if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    isDeletedProperty.SetDefaultValue(false);
                }
            }
            builder.Entity<Answer>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<ApplicationRole>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<ApplicationUser>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Category>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Course>().HasQueryFilter(e => !e.IsDeleted && e.IsApproved);
            builder.Entity<Enrollment>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<FacebookUser>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Plan>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Question>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Quiz>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<QuizQuesitons>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Review>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Student>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<StudentEnrolledCourses>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<StudentSavedCourses>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<subscription>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Teacher>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Video>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<WhatToLearn>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Earnings>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<comment>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Admin>().HasQueryFilter(e => !e.IsDeleted);
            //builder.Entity<Course>().HasQueryFilter(e => e.IsApproved);



        }
    }
}
