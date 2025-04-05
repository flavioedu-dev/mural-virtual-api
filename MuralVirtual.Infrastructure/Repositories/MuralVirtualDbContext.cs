using Microsoft.EntityFrameworkCore;
using MuralVirtual.Domain.Entities;

namespace MuralVirtual.Infrastructure.Repositories;

public class MuralVirtualDbContext : DbContext
{
    public MuralVirtualDbContext(DbContextOptions<MuralVirtualDbContext> options)
        : base(options) { }

    public DbSet<User> Users {  get; set; } 
}