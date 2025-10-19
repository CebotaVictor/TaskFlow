using Microsoft.CodeAnalysis.Scripting;

namespace TaskFlow.WebApi.Extensions
{
    public static class WebOptimizer
    {
        public static IServiceCollection AddWebOptimizerConfig(this IServiceCollection  services)
        {
            services.AddWebOptimizer(pipeline =>
            {
                //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? new NullReferenceException("ASPNETCORE_ENVIRONMENT null").ToString();
                //bool isDevelopment = environment == Environments.Development;
                pipeline.AddCssBundle("/css/bundle.css", "assets/css/nucleo-icons.css", "assets/css/nucleo-svg.css");
                pipeline.AddJavaScriptBundle("/js/core.js", "assets/js/core/bootstrap.bundle.min.js");

                //pipeline.MinifyCssFiles("/assets/css/ProjectBoard.css");
                pipeline.MinifyCssFiles ();
                pipeline.MinifyJsFiles();

            });

            return services;
        }
    }
}
