using System;
using ForMemory.Entities.Interfaces;

namespace ForMemory.Entities.Accounting
{
    public class AccountEntity : IEntity
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}