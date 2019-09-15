using System;
using ForMemory.Entities.Interfaces;

namespace ForMemory.Entities.Family
{
    public class FamilyMemberEntity : IEntity
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 签名密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 密码签名类型
        /// </summary>
        public string PType { get; set; }

        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid FamilyId { get; set; }
    }
}