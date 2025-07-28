using FileManager.Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.EF.Presistance;
public class ApplicationDbcontext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbcontext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<UploadedFile> Files { get; set; }
}
