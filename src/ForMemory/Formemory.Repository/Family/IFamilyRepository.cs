using System;
using ForMemory.Entities.Family;

namespace Formemory.Repository.Family
{
    public interface IFamilyRepository
    {
        void Insert(FamilyEntity entity);

        FamilyEntity QueryById(Guid id);
    }
}