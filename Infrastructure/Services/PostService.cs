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

        public async Task<PostDetailsDto> GetPostByIdAsync(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                throw new Exception($"Post with id {id} not found.");
            }
            return PostMapping.ToPostDetailsDto(post);
        }

        public async Task<IEnumerable<PostDetailsDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts.Select(PostMapping.ToPostDetailsDto);
        }

        public async Task<PostDetailsDto> CreatePostAsync(CreatePostDto createPostDto, Guid authorId)
        {
            var post = PostMapping.ToPost(createPostDto, authorId);
            var tags = await _tagRepository.GetTagsByIdAsync(createPostDto.TagIds);
            post.Tags = tags.ToList();
            await _postRepository.AddAsync(post);
            var savedPost = await _postRepository.GetByIdAsync(post.Id);
            var postDetailsDto = PostMapping.ToPostDetailsDto(savedPost);

            return postDetailsDto;

        }

        public async Task UpdatePostAsync(UpdatePostDto post, Guid postId, Guid authorId)
        {
            var existingPost = await _postRepository.GetByIdAsync(postId);
            if (existingPost == null)
            {
                throw new Exception($"Post with id {postId} not found.");
            }
            if (existingPost.AuthorId != authorId)
            {
                throw new Exception("You are not authorized to update this post.");
            }
            var tags = await _tagRepository.GetTagsByIdAsync(post.TagIds);
            existingPost.Tags = tags.ToList();
            var updatedPost = PostMapping.ToPost(post, existingPost);
            await _postRepository.UpdateAsync(updatedPost);
        }

        public async Task DeletePostAsync(Guid id, Guid authorId,bool isAdmin)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                throw new Exception($"Post with id {id} not found.");
            }
            if (post.AuthorId != authorId && !isAdmin)
            {
                throw new Exception("You are not authorized to delete this post.");
            }
          
            await _postRepository.DeleteAsync(post);
        }

        public async Task<IEnumerable<PostDetailsDto>> GetPostByAuthorIdAsync(Guid authorId)
        {
            var posts = await _postRepository.GetPostsByAuthorAsync(authorId);
            return posts.Select(PostMapping.ToPostDetailsDto);
        }

        public async Task<PagedResult<PostDetailsDto>> GetAllPostsPaginatedAsync(int pageNumber, int pageSize)
        {
            var pagedPosts = await _postRepository.GetAllPostsPaginatedAsync(pageNumber, pageSize);
            var postDetailsDtos = pagedPosts.Items.Select(PostMapping.ToPostDetailsDto).ToList();
            return new PagedResult<PostDetailsDto>
            {
                Items = postDetailsDtos,
                TotalRecords = pagedPosts.TotalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = pagedPosts.TotalPages
            };
        }
    }
}
