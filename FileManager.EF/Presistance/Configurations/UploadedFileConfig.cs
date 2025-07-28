using FileManager.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Core.Presistance.Configurations;
internal class UploadedFileConfig : IEntityTypeConfiguration<UploadedFile>
{
    public void Configure(EntityTypeBuilder<UploadedFile> builder)
    {
        builder.ToTable("Files");
        builder.Property(x => x.FileName).HasMaxLength(250);
        builder.Property(x => x.StoredFileName).HasMaxLength(250);
        builder.Property(x => x.ContentType).HasMaxLength(50);
        builder.Property(x => x.FileExtension).HasMaxLength(10);
    }
}
