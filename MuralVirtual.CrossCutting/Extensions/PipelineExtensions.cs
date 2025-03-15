using Microsoft.Extensions.DependencyInjection;
using MuralVirtual.Application.Services;
using MuralVirtual.Domain.Interfaces.Application;
using MuralVirtual.Domain.Interfaces.Infrastructure.Repositories;
using MuralVirtual.Infrastructure.Repositories;

namespace MuralVirtual.CrossCutting.Extensions;

public static class PipelineExtensions
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        #region Auth
        services.AddScoped<IAuthServices, AuthServices>();
        #endregion Auth

    }

    public static void AddInfrastructureDI(this IServiceCollection services)
    {
        #region Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        #endregion Repositories
    }
}
