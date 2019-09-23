using System;
using ForMemory.Domain.Interfaces.Repositories.Accounting;
using ForMemory.Domain.Interfaces.Services.Accounting;
using ForMemory.Dto.Accounting;
using ForMemory.Entities.Accounting;
using Microsoft.Extensions.Logging;

namespace ForMemory.Service.Accounting
{
    public class ExpensesRecordCmdService : IExpensesRecordCmdService
    {
        private readonly IExpensesRecordRepository _expensesRecordRepository;
        private readonly ILogger<ExpensesRecordCmdService> _logger;
        public ExpensesRecordCmdService(IExpensesRecordRepository expensesRecordRepository,
            ILogger<ExpensesRecordCmdService> logger)
        {
            _expensesRecordRepository = expensesRecordRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public void AddRecord(ExpensesRecordAddDto recordInfo)
        {
            _expensesRecordRepository.Insert(new ExpensesRecordEntity()
            {
                Amount = recordInfo.Amount,
                Type = recordInfo.Type,
                AccountId = recordInfo.AccountId,
                Time = DateTime.Now
            });
            _expensesRecordRepository.Commit();

            _logger.LogInformation("save end");
        }
    }
}