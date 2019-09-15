using System;

namespace ForMemory.Entities.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid Id { get; set; }
    }
}