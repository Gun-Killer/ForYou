using ForMemory.Dto.Accounting;
using ForMemory.Entities.Accounting;

namespace ForMemory.Domain.Interfaces.Repositories.Accounting
{
    public interface IExpensesRecordRepository
    {
        void Insert(ExpensesRecordEntity entity);

        ExpensesRecordDto[] Query(int pageIndex, int pageSize);
        int Commit();
    }
}