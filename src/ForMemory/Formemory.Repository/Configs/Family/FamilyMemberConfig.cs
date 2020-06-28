using ForMemory.Entities.Family;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForMemory.Repository.Configs.Family
{
    public class FamilyMemberConfig : IEntityTypeConfiguration<FamilyMemberEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<FamilyMemberEntity> builder)
        {
            builder.ToTable("family_member");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).HasMaxLength(255);
            builder.Property(t => t.Password).HasMaxLength(255);
            builder.Property(t => t.Salt).HasMaxLength(255);
            builder.Property(t => t.PType).HasMaxLength(255);
            builder.Property(t => t.Phone).HasMaxLength(255);


            builder.HasIndex(t => t.Phone).IsUnique();
        }
    }
}