using DevBlog.Core.Interfaces;
using DevBlog.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using DevBlog.Core.Entities;
namespace DevBlog.Infrastructure.Services
{
    public class LikeService : ILikeService
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public LikeService(IPostRepository postRepository, UserManager<ApplicationUser> userManager)
        {
            _postRepository = postRepository;
            _userManager = userManager;
        }
        public async Task<Result<bool>> LikePostAsync(Guid postId, Guid userId)
        {
            if (postId == Guid.Empty || userId == Guid.Empty)
            {
                return Result<bool>.Fail("Post ID and User ID cannot be empty.");
            }

            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                return Result<bool>.Fail($"Post with id {postId} not found.");
            }

            if (post.LikedByUsers.Any(u => u.Id == userId))
            {
                return Result<bool>.Fail("You have already liked this post.");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result<bool>.Fail($"User with id {userId} not found.");
            }

            await _postRepository.LikePostAsync(post, user);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> UnlikePostAsync(Guid postId, Guid userId)
        {
            if (postId == Guid.Empty || userId == Guid.Empty)
            {
                return Result<bool>.Fail("Post ID and User ID cannot be empty.");
            }
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                return Result<bool>.Fail($"Post with id {postId} not found.");
            }
            if (!post.LikedByUsers.Any(u => u.Id == userId))
            {
                return Result<bool>.Fail("You have not liked this post.");
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result<bool>.Fail($"User with id {userId} not found.");
            }
            await _postRepository.UnlikePostAsync(post, user);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<int>> GetLikesCountAsync(Guid postId)
        {
            if (postId == Guid.Empty)
            {
                return Result<int>.Fail("Post ID cannot be empty.");
            }
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                return Result<int>.Fail($"Post with id {postId} not found.");
            }
            int likesCount = post.LikedByUsers.Count;
            return Result<int>.Ok(likesCount);

        }
    }
}
