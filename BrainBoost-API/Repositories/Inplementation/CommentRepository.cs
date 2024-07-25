using AutoMapper;
using BrainBoost_API.DTOs.Review;
using BrainBoost_API.DTOs.video;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Interfaces;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class CommentRepository : Repository<comment>, ICommentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CommentRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<GetCommentDTO> getComments(IEnumerable<comment> comments)
        {
            var com = mapper.Map<IEnumerable<GetCommentDTO>>(comments).ToList();
            return com;
        }
        public comment addComment(Student student, addCommentDTO comment)
        {
            comment c = new comment 
                          { 
                            CommentDate = DateTime.Now,
                            StudentPhoto=student.PictureUrl,
                            StudentName=student.AppUser.UserName,
                            studentId=student.Id,   
                            Content=comment.Content,
                            VideoId=comment.VideoId,
                          };
            return c;
        }
    }
}
