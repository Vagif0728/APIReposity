using API.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Services.Implementations
{
    public class TagService: ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateTagDto createTagDto)
        {
            await _repository.AddAsync(new Tag { Name = createTagDto.Name });
            await _repository.SaveChanceAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not Found");

            _repository.Delete(tag);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<GetTagDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<GetTagDto> tagDtos = new List<GetTagDto>();
            foreach (Tag category in tags)
            {
                tagDtos.Add(new GetTagDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return tagDtos;
        }
        public async Task<ICollection<GetTagDto>> GetAllByOrderAsync(int page, int take, Expression<Func<Tag, object>>? orderExpression)
        {
            ICollection<Tag> tags = await _repository.GetAllByOrderAsync(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<GetTagDto> tagDtos = new List<GetTagDto>();
            foreach (Tag category in tags)
            {
                tagDtos.Add(new GetTagDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return tagDtos;
        }

        public async Task<GetTagDto> GetByIdAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Not Found");

            return new GetTagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public async Task UpdateAsync(int id, CreateTagDto createTagDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not Found");

            tag.Name = createTagDto.Name;

            _repository.Update(tag);
            await _repository.SaveChanceAsync();
        }
    }
}
