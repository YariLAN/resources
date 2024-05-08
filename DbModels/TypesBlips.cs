using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DbModels
{
    public class TypesBlips
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Blips> Blips { get; set; }
    }

    public class TypesBlipsConfiguration : IEntityTypeConfiguration<TypesBlips>
    {
        public void Configure(EntityTypeBuilder<TypesBlips> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasMaxLength(50);

            builder
                .HasMany(x => x.Blips)
                .WithOne(x => x.TypeBlip)
                .HasForeignKey(x => x.TypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
