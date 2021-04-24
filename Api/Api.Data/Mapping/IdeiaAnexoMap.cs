﻿using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class IdeiaAnexoMap : IEntityTypeConfiguration<IdeiaAnexoEntity>
    {
        public void Configure(EntityTypeBuilder<IdeiaAnexoEntity> builder)
        {
            builder.ToTable("IdeiaAnexo");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DesAnexo).HasColumnType("LONGTEXT");
            builder.Property(p => p.IndTipoArquivo).HasMaxLength(1);
            builder.Property(p => p.DesNomeOriginal).HasMaxLength(500);
            builder.Property(p => p.IdeiaId);
        }
    }
}
