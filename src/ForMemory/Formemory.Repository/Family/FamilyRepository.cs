using ForMemory.Entities.Family;

namespace Formemory.Repository.Family
{
    public class FamilyRepository : BaseRepository<FamilyEntity>
    {
        /// <inheritdoc />
        public FamilyRepository(MyDbContext dbContext) : base(dbContext)
        {

        }
    }
}