using System;

namespace ForMemory.Domain.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid Id { get; set; }
    }
}