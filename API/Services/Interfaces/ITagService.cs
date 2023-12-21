using System.Linq.Expressions;

namespace API.Services.Interfaces
{
    public interface ITagService
    {
        Task<ICollection<GetTagDto>> GetAllAsync(int page, int take);
        Task<ICollection<GetTagDto>> GetAllByOrderAsync(int page, int take, Expression<Func<Tag, object>>? orderExpression);
        Task<GetTagDto> GetByIdAsync(int id);
        Task CreateAsync(CreateTagDto createTagDto);
        Task UpdateAsync(int id, CreateTagDto createTagDto);
        Task DeleteAsync(int id);
    }
}
