using ForMemory.Domain.Interfaces.Repositories.Family;
using ForMemory.Entities.Family;

namespace Formemory.Repository.Family
{
    public class FamilyMemberRepository : BaseRepository<FamilyMemberEntity>, IFamilyMemberRepository
    {
        /// <inheritdoc />
        public FamilyMemberRepository(MyDbContext dbContext) : base(dbContext)
        {

        }
    }
}