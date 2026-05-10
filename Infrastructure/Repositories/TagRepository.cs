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
        public async Task AddAsync(Tag entity)
        {
            await _context.Tags.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }
        public async Task<Tag> GetByIdAsync(Guid id)
        {
            return await _context.Tags.FindAsync(id);
        }
        public async Task UpdateAsync(Tag entity)
        {
            _context.Tags.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tag entity)
        {
            _context.Tags.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Tag>> GetTagsByPostIdAsync(Guid postId)
        {
            return await _context.Tags
                .Where(t => t.Posts.Any(p => p.Id == postId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsByIdAsync(IEnumerable<Guid> tagIds)
        {
            return await _context.Tags
                .Where(t => tagIds.Contains(t.Id))
                .ToListAsync();
        }

    }
}
