using ForMemory.Entities.Family;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForMemory.Repository.Configs.Family
{
    public class FamilyConfig : IEntityTypeConfiguration<FamilyEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<FamilyEntity> builder)
        {
            builder.ToTable("family");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).HasMaxLength(255);

            builder.HasIndex(t => t.Name).IsUnique();
        }
    }
}