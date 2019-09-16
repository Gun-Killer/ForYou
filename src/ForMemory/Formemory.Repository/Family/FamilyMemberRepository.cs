using ForMemory.Entities.Family;

namespace Formemory.Repository.Family
{
    public class FamilyMemberRepository : BaseRepository<FamilyMemberEntity>
    {
        /// <inheritdoc />
        public FamilyMemberRepository(MyDbContext dbContext) : base(dbContext)
        {

        }
    }
}