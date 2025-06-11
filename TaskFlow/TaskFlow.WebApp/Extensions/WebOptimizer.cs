using Microsoft.CodeAnalysis.Scripting;

namespace TaskFlow.WebApi.Extensions
{
    public static class WebOptimizer
    {
        public static IServiceCollection AddWebOptimizerConfig(this IServiceCollection  services)
        {
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddCssBundle("/css/bundle.css", "assets/css/nucleo-icons.css", "assets/css/nucleo-svg.css");
                pipeline.AddJavaScriptBundle("/js/core.js", "assets/js/core/bootstrap.bundle.min.js");

                pipeline.MinifyCssFiles();
                pipeline.MinifyJsFiles();
            });

            return services;
        }
    }
}
