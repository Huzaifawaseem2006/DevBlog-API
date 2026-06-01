using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;
using DevBlog.Core.Helpers;
using DevBlog.Core.Interfaces;



namespace DevBlog.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        public PostService(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Result<PostDetailsDto>> GetPostByIdAsync(Guid id, CancellationToken token)
        {
            var post = await _postRepository.GetByIdAsync(id, token);
            if (post == null)
            {
                return Result<PostDetailsDto>.Fail($"Post with id {id} not found.");
            }
            return Result<PostDetailsDto>.Ok(PostMapping.ToPostDetailsDto(post));
        }

        public async Task<Result<IEnumerable<PostDetailsDto>>> GetAllPostsAsync(CancellationToken token)
        {
            var posts = await _postRepository.GetAllAsync(token);
            return Result<IEnumerable<PostDetailsDto>>.Ok(posts.Select(PostMapping.ToPostDetailsDto));
        }

        public async Task<Result<PostDetailsDto>> CreatePostAsync(CreatePostDto createPostDto, Guid authorId, CancellationToken token)
        {
            var post = PostMapping.ToPost(createPostDto, authorId);
            var tags = await _tagRepository.GetTagsByIdAsync(createPostDto.TagIds, token);
            post.Tags = tags.ToList();
            await _postRepository.AddAsync(post, token);
            var savedPost = await _postRepository.GetByIdAsync(post.Id, token);
            var postDetailsDto = PostMapping.ToPostDetailsDto(savedPost);

            return Result<PostDetailsDto>.Ok(postDetailsDto);

        }

        public async Task<Result<bool>> UpdatePostAsync(UpdatePostDto post, Guid postId, Guid authorId, CancellationToken token)
        {
            var existingPost = await _postRepository.GetByIdAsync(postId, token);
            if (existingPost == null)
            {
                return Result<bool>.Fail($"Post with id {postId} not found.");
            }
            if (existingPost.AuthorId != authorId)
            {
                return Result<bool>.Fail("You are not authorized to update this post.");
            }
            var tags = await _tagRepository.GetTagsByIdAsync(post.TagIds, token);
            existingPost.Tags = tags.ToList();
            var updatedPost = PostMapping.ToPost(post, existingPost);
            await _postRepository.UpdateAsync(updatedPost, token);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> DeletePostAsync(Guid id, Guid authorId, bool isAdmin, CancellationToken token)
        {
            var post = await _postRepository.GetByIdAsync(id, token);
            if (post == null)
            {
                return Result<bool>.Fail($"Post with id {id} not found.");
            }
            if (post.AuthorId != authorId && !isAdmin)
            {
                return Result<bool>.Fail("You are not authorized to delete this post.");
            }

            await _postRepository.DeleteAsync(post, token);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<IEnumerable<PostDetailsDto>>> GetPostByAuthorIdAsync(Guid authorId, CancellationToken token)
        {
            var posts = await _postRepository.GetPostsByAuthorAsync(authorId, token);
            return Result<IEnumerable<PostDetailsDto>>.Ok(posts.Select(PostMapping.ToPostDetailsDto));
        }

        public async Task<Result<PagedResult<PostDetailsDto>>> GetAllPostsPaginatedAsync(int pageNumber, int pageSize, CancellationToken token)
        {
            var pagedPosts = await _postRepository.GetAllPostsPaginatedAsync(pageNumber, pageSize, token);
            var postDetailsDtos = pagedPosts.Items.Select(PostMapping.ToPostDetailsDto).ToList();
            return Result<PagedResult<PostDetailsDto>>.Ok(new PagedResult<PostDetailsDto>
            {
                Items = postDetailsDtos,
                TotalRecords = pagedPosts.TotalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = pagedPosts.TotalPages
            });
        }

        public async Task<Result<IEnumerable<PostDetailsDto>>> SearchPostsAsync(string searchTerm, CancellationToken token)
        {
            var posts = await _postRepository.SearchPostsAsync(searchTerm, token);
            return Result<IEnumerable<PostDetailsDto>>.Ok(posts.Select(PostMapping.ToPostDetailsDto));
        }
    }
}
