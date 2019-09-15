using System;
using ForMemory.Entities.Interfaces;

namespace ForMemory.Entities.Family
{
    /// <summary>
    /// 
    /// </summary>
    public class Family: IEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        public Guid Id { get; set; }
    }
}