using ForMemory.Dto.Family;

namespace ForMemory.Domain.Interfaces.Services.Family
{
    public interface IFamilyCmdService
    {
        void Register(FamilyRegisterDto registerInfo);
    }
}