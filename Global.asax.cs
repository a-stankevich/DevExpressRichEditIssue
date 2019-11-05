using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DevExpressRichEditIssue.StateProvider;

namespace DevExpressRichEditIssue
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			DevExpress.Web.Office.DocumentManager.StateProvider = new MockOfficeStateProvider();
		}
	}
}
