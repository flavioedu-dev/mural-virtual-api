using Microsoft.EntityFrameworkCore;
using MuralVirtual.Infrastructure.Repositories;

namespace MuralVirtual.API.Extensions.IoC;

public static class PipelineExtensions
{
    public static void AddApiDI(this IServiceCollection services, IConfiguration configuration)
    {
        #region Default
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        #endregion Default

        #region AutoMapper
        services.AddAutoMapper(typeof(Program));
        #endregion AutoMapper

        #region Context
        services.AddDbContextPool<MuralVirtualDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("MuralVirtualDB"), options => options.MigrationsAssembly("MuralVirtual.API"))
        );
        #endregion Context
    }
}
