using ForMemory.Domain.Interfaces.Repositories.Family;
using ForMemory.Entities.Family;

namespace ForMemory.Repository.Family
{
    public class FamilyRepository : BaseRepository<FamilyEntity>, IFamilyRepository
    {
        /// <inheritdoc />
        public FamilyRepository(MyDbContext dbContext) : base(dbContext)
        {

        }
    }
}