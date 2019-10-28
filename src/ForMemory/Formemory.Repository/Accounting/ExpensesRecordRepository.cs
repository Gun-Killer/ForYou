using System.Linq;
using ForMemory.Domain.Interfaces.Repositories.Accounting;
using ForMemory.Dto.Accounting;
using ForMemory.Entities.Accounting;

namespace Formemory.Repository.Accounting
{
    public class ExpensesRecordRepository : BaseRepository<ExpensesRecordEntity>, IExpensesRecordRepository
    {
        /// <inheritdoc />
        public ExpensesRecordRepository(MyDbContext dbContext) : base(dbContext)
        {

        }

        /// <inheritdoc />
        public ExpensesRecordDto[] Query(int pageIndex, int pageSize)
        {
            return _entities.Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(t => new ExpensesRecordDto
                {
                    Amount = t.Amount,
                    Type = t.Type,
                    Time = t.Time,
                    Remark = t.Remark
                })
                .ToArray();
        }
    }
}