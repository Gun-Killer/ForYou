using ForMemory.Domain.Interfaces.Repositories.Accounting;
using ForMemory.Domain.Interfaces.Services.Accounting;
using ForMemory.Dto.Accounting;

namespace ForMemory.Service.Accounting
{
    public class ExpensesRecordQueryService : IExpensesRecordQueryService
    {
        private readonly IExpensesRecordRepository _expensesRecordRepository;

        public ExpensesRecordQueryService(IExpensesRecordRepository expensesRecordRepository)
        {
            _expensesRecordRepository = expensesRecordRepository;
        }

        /// <inheritdoc />
        public ExpensesRecordDto[] Query(int pageIndex, int pageSize)
        {
            return _expensesRecordRepository.Query(pageIndex, pageSize);
        }
    }
}