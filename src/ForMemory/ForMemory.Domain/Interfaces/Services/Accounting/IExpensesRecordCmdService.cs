using ForMemory.Dto.Accounting;

namespace ForMemory.Domain.Interfaces.Services.Accounting
{
    public interface IExpensesRecordCmdService
    {
        void AddRecord(ExpensesRecordAddDto recordInfo);
    }
}