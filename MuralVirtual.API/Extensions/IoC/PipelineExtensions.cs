namespace MuralVirtual.API.Extensions.IoC;

public static class PipelineExtensions
{
    public static void AddApiDI(this IServiceCollection services)
    {
        #region Default
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        #endregion Default

        #region AutoMapper
        services.AddAutoMapper(typeof(Program));
        #endregion AutoMapper
    }
}
