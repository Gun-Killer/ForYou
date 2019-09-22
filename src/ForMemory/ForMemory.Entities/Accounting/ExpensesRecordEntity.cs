using System;
using ForMemory.Entities.Interfaces;

namespace ForMemory.Entities.Accounting
{
    public class ExpensesRecordEntity : IEntity
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid FamilyId { get; set; }

        public Guid FamilyMemberId { get; set; }

        public DateTime Time { get; set; }

        public decimal Amount { get; set; }

        public int Type { get; set; }

        public string Remark { get; set; }
    }
}