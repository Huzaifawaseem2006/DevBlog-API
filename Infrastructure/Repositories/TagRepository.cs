using DevBlog.Core.Entities;
using DevBlog.Core.Interfaces;
using DevBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;
        public TagRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Tag entity, CancellationToken token)
        {
            await _context.Tags.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync(token);
            }
        }
        public async Task<IEnumerable<Tag>> GetAllAsync(CancellationToken token)
        {
            return await _context.Tags.ToListAsync(token);
        }
        public async Task<Tag> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Tags.FindAsync(new object[] { id }, token);
        }
        public async Task UpdateAsync(Tag entity, CancellationToken token)
        {
            _context.Tags.Update(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(Tag entity, CancellationToken token)
        {
            _context.Tags.Remove(entity);
            await _context.SaveChangesAsync(token);
        }
        public async Task<IEnumerable<Tag>> GetTagsByPostIdAsync(Guid postId, CancellationToken token)
        {
            return await _context.Tags
                .Where(t => t.Posts.Any(p => p.Id == postId))
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Tag>> GetTagsByIdAsync(IEnumerable<Guid> tagIds, CancellationToken token)
        {
            return await _context.Tags
                .Where(t => tagIds.Contains(t.Id))
                .ToListAsync(token);
        }

    }
}
