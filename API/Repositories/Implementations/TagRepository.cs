using API.Repositories.Interfaces;

namespace API.Repositories.Implementations
{
    public class TagRepository: Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context): base(context) { }
    }
}
