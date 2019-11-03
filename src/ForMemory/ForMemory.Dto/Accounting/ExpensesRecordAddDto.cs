using System;

namespace ForMemory.Dto.Accounting
{
    public class ExpensesRecordAddDto
    {
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
        /// <summary>
        /// 1 income
        /// 2 expense
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public string Remark { get; set; }
    }
}