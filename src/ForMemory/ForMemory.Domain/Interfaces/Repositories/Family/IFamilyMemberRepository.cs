using System;
using ForMemory.Entities.Family;

namespace ForMemory.Domain.Interfaces.Repositories.Family
{
    public interface IFamilyMemberRepository
    {
        void Insert(FamilyMemberEntity entity);

        FamilyMemberEntity QueryById(Guid id);
    }
}