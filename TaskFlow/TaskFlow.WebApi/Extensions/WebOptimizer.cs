namespace TaskFlow.WebApi.Extensions
{
    public static class WebOptimizer
    {
        public static IServiceCollection AddWebOptimizerConfig(this IServiceCollection  services)
        {
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddCssBundle("/css/bundle.css", "assets/css/nucleo-icons.css", "assets/css/nucleo-svg.css");
                pipeline.AddJavaScriptBundle("/js/bundle.js", "assets/js/material-dashboard.min.js");
                pipeline.MinifyCssFiles();
                pipeline.MinifyJsFiles();
            });
            return services;
        }
    }
}
