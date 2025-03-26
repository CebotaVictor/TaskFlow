using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TaskFlow.WebApi.Extensions
{
    public static class RouteConfigure
    {
        public static IEndpointRouteBuilder AddRouteConfig(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapControllerRoute(
                name: "Billing",
                pattern: "{controller=Home}/{action=Biling}/{id?}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "Dashboard",
                pattern: "{controller=Home}/{action=Dashboard}/{id?}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "Dashboard",
                pattern: "{controller=Home}/{action=Template}/{id?}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "Profile",
                pattern: "{controller=Home}/{action=Profile}/{id?}")
                .WithStaticAssets();

            return endpoint;
        }
    }
}
