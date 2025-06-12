using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Services;

namespace Book_Keep.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ScopeService(this IServiceCollection service)
        {
            // Helpers
            service.AddScoped<ExcelHelper>();
            service.AddScoped<TimeHelper>();
            // Service
            service.AddScoped<BookService>();
            // Queries
            service.AddScoped<BookQueries>();
            return service;
        }
    }
}
