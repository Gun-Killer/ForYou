using System;

namespace ForMemory.Dto.Accounting
{
    public class ExpensesRecordDto
    {
        public decimal Amount { get; set; }

        public string Remark { get; set; }

        public int Type { get; set; }

        public DateTime Time { get; set; }
    }
}