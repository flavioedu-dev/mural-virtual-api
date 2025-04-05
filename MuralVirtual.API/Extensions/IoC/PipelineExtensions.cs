using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MuralVirtual.API.Filters;
using MuralVirtual.API.Middlewares;
using MuralVirtual.CrossCutting.Extensions;
using MuralVirtual.Domain.Configurations;
using MuralVirtual.Infrastructure.Repositories;

namespace MuralVirtual.API.Extensions.IoC;

public static class PipelineExtensions
{
    public static void AddApiDI(this IServiceCollection services, IConfiguration configuration)
    {
        #region AutoMapper
        services.AddAutoMapper(typeof(Program));
        #endregion AutoMapper

        #region FluentValidation
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        services.AddFluentValidationAutoValidation();
        #endregion FluentValidation

        #region Context
        services.AddDbContextPool<MuralVirtualDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("MuralVirtualDB"), options => options.MigrationsAssembly("MuralVirtual.API"))
        );
        #endregion Context
    }

    public static void AddDI(this IServiceCollection services, IConfiguration configuration)
    {
        #region Default
        services.AddControllers(options =>
            options.Filters.Add(typeof(CustomErrorResponse))
        ).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        #endregion Default

        services.AddApiDI(configuration);
        services.AddApplicationDI();
        services.AddInfrastructureDI();

        #region Configurations
        services.Configure<PasswordEncryptionOptions>(configuration.GetSection("PasswordEncryption"));
        #endregion Configurations
    }

    public static void AddMiddlewares(this WebApplication app)
    {
        app.GlobalErrorHandling();
    }
}
