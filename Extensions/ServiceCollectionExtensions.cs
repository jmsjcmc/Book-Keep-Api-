using Book_Keep.Helpers;

namespace Book_Keep.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ScopeService(this IServiceCollection service)
        {
            // Helpers
            service.AddScoped<ExcelHelper>();
            service.AddScoped<TimeHelper>();
            return service;
        }
    }
}
