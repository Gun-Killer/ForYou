using System;
using ForMemory.Entities.Family;

namespace ForMemory.Domain.Interfaces.Repositories.Family
{
    public interface IFamilyRepository
    {
        void Insert(FamilyEntity entity);

        FamilyEntity QueryById(Guid id);
    }
}