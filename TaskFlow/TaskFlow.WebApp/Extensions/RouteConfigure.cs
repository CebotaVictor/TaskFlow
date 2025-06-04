using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TaskFlow.WebApi.Extensions
{
    public static class RouteConfigure
    {
        public static IEndpointRouteBuilder AddRouteConfig(this IEndpointRouteBuilder endpoint)
        {

            endpoint.MapControllerRoute(
                name: "Billing",
                pattern: "{controller=Home}/{action=Biling}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "Dashboard",
                pattern: "{controller=Home}/{action=Dashboard}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "Dashboard",
                pattern: "{controller=Home}/{action=Template}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "Profile",
                pattern: "{controller=Home}/{action=Profile}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "Register",
                pattern: "{controller=Auth}/{action=Register}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "Register",
                pattern: "{controller=Auth}/{action=Login}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "ProjectView",
                pattern: "{controller=Workflow}/{action=ProjectBoardView}")
                .WithStaticAssets();


            endpoint.MapControllerRoute(
                name: "ProjectView",
                pattern: "{controller=Workflow}/{action=ProjectListView}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "ProjectView",
                pattern: "{controller=Workflow}/{action=ViewPage}")
                .WithStaticAssets();

            return endpoint;
        }
    }
}
