using ForMemory.Entities.Accounting;

namespace ForMemory.Domain.Interfaces.Repositories.Accounting
{
    public interface IExpensesRecordRepository
    {
        void Insert(ExpensesRecordEntity entity);

        int Commit();
    }
}