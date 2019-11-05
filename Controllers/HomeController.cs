using System.Web;
using System.Web.Mvc;

namespace DevExpressRichEditIssue.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				ViewBag.FileName = Server.MapPath("~/Docs/" + fileName);
			}
			else
			{
				ViewBag.FileName = "";
			}
			return View();
		}
	}
}