using BrainBoost_API.DTOs.video;
using BrainBoost_API.Hubs;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHubContext<CommentHub> commentsHub;

        public CommentController(IUnitOfWork unitOfWork , IHubContext<CommentHub> commentsHub)
        {
            this.unitOfWork = unitOfWork;
            this.commentsHub = commentsHub;
        }

        [HttpPost("addComment")]
        public async Task<IActionResult> addComment(addCommentDTO comment)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Student std = unitOfWork.StudentRepository.Get(c => c.UserId == UserID , "AppUser");
                comment com = unitOfWork.CommentRepository.addComment(std, comment);
                unitOfWork.CommentRepository.add(com);
                unitOfWork.save();
                await commentsHub.Clients.All.SendAsync("ReceiveComment", new
                {
                    UserName = std.AppUser.UserName,
                    CommentContent = com.Content,
                    CommentDate = com.CommentDate,
                    UserPhoto = std.PictureUrl

                });
                var comments = unitOfWork.CommentRepository.GetList(c => c.VideoId == comment.VideoId);
                var commentDTO = unitOfWork.CommentRepository.getComments(comments).ToList();
                return Ok(commentDTO);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("getComment/{id:int}")]
        public async Task<IActionResult> getComment(int id)
        {
            if (ModelState.IsValid)
            {
                var comments = unitOfWork.CommentRepository.GetList(c => c.VideoId == id);
                var commentDTO = unitOfWork.CommentRepository.getComments(comments).ToList();

                return Ok(commentDTO);
            }
            return BadRequest(ModelState);
        }

    }
}
