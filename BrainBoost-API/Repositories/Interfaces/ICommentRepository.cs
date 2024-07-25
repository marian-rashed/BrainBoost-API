using BrainBoost_API.DTOs.video;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;

namespace BrainBoost_API.Repositories.Interfaces
{
    public interface ICommentRepository:IRepository<comment>
    {
        IEnumerable<GetCommentDTO> getComments(IEnumerable<comment> comments);
        comment addComment(Student student,addCommentDTO comment);
    }
}
