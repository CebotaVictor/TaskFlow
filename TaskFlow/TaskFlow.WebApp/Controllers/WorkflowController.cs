using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.WebApp.Controllers
{
    public class WorkflowController : Controller
    {
        public WorkflowController() { }

        public ActionResult ProjectBoardView() { return View(); }
        public ActionResult ProjectListView() { return View(); }
        public ActionResult ViewPage() { return View(); }
    }
}
