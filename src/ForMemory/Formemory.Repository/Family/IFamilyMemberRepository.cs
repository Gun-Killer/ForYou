using System;
using ForMemory.Entities.Family;

namespace Formemory.Repository.Family
{
    public interface IFamilyMemberRepository
    {
        void Insert(FamilyMemberEntity entity);

        FamilyMemberEntity QueryById(Guid id);
    }
}