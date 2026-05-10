using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;

namespace DevBlog.Core.Helpers
{
    public static class TagMapping
    {
        public static TagDetailsDto ToDto(this Tag tag)
        {
            return new TagDetailsDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public static Tag ToTag(this CreateTagDto dto)
        {
            return new Tag
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
        }

        public static Tag ToUpdatedDto(this UpdateTagDto dto, Tag existingTag)
        {
            existingTag.Name = dto.Name;
            return existingTag;
        }
    }
}
