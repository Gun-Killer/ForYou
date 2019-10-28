using ForMemory.Dto.Accounting;

namespace ForMemory.Domain.Interfaces.Services.Accounting
{
    public interface IExpensesRecordQueryService
    {
        ExpensesRecordDto[] Query(int pageIndex, int pageSize);
    }
}