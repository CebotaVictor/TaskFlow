using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TaskFlow.WebApi.Extensions
{
    public static class RouteConfigure
    {
        public static IEndpointRouteBuilder AddRouteConfig(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapControllerRoute(
                name: "Profile",
                pattern: "{controller=Home}/{action=Profile}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Register}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "Register",
                pattern: "{controller=Auth}/{action=Login}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "ProjectBoardView",
                pattern: "{controller=Workflow}/{action=ProjectBoardView}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "ProjectBoardView",
                pattern: "{controller=Workflow}/{action=ProjectBoardView}/{Id}")
                .WithStaticAssets();


            endpoint.MapControllerRoute(
                name: "ProjectListView",
                pattern: "{controller=Workflow}/{action=ProjectListView}")
                .WithStaticAssets();

            endpoint.MapControllerRoute(
                name: "ViewPage",
                pattern: "{controller=Workflow}/{action=ViewPage}")
                .WithStaticAssets();

            return endpoint;
        }
    }
}
