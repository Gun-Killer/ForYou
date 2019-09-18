using ForMemory.Domain.Interfaces.Repositories.Family;
using ForMemory.Domain.Interfaces.Services.Family;
using ForMemory.Dto.Family;
using ForMemory.Entities.Family;

namespace ForMemory.Service.Family
{
    public class FamilyCmdService : IFamilyCmdService
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        /// <inheritdoc />
        public FamilyCmdService(IFamilyRepository familyRepository,
            IFamilyMemberRepository familyMemberRepository)
        {
            _familyRepository = familyRepository;
            _familyMemberRepository = familyMemberRepository;
        }

        /// <inheritdoc />
        public void Register(FamilyRegisterDto registerInfo)
        {
            var newFamily = new FamilyEntity
            {
                Name = registerInfo.Name
            };
            _familyRepository.Insert(newFamily);


            _familyMemberRepository.Insert(new FamilyMemberEntity()
            {
                FamilyId = newFamily.Id,
                Name = registerInfo.Name,
                //Password=registerInfo.Password1// todo
                Phone = registerInfo.Phone,
            });

            _familyRepository.Commit();
        }
    }
}