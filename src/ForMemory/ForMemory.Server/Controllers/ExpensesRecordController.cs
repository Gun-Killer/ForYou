using ForMemory.Domain.Interfaces.Services.Accounting;
using ForMemory.Dto.Accounting;
using Microsoft.AspNetCore.Mvc;

namespace ForMemory.Server.Controllers
{
    [Route("api/[controller]")]
    public class ExpensesRecordController : Controller
    {
        private readonly IExpensesRecordCmdService _expensesRecordCmdService;

        public ExpensesRecordController(IExpensesRecordCmdService expensesRecordCmdService)
        {
            _expensesRecordCmdService = expensesRecordCmdService;
        }

        [HttpPost, Route("addrecord")]
        public string AddRecord(ExpensesRecordAddDto record)
        {
            _expensesRecordCmdService.AddRecord(record);
            return "ok";
        }
    }
}
