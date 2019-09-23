using ForMemory.Domain.Interfaces.Repositories.Accounting;
using ForMemory.Entities.Accounting;

namespace Formemory.Repository.Accounting
{
    public class ExpensesRecordRepository : BaseRepository<ExpensesRecordEntity>, IExpensesRecordRepository
    {
        /// <inheritdoc />
        public ExpensesRecordRepository(MyDbContext dbContext) : base(dbContext)
        {

        }
    }
}