using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.Office;

namespace DevExpressRichEditIssue.StateProvider
{
	public class MockOfficeStateProvider : OfficeStateProviderBase
	{
		public MockOfficeStateProvider() : base(new MockOfficeStateStorage())
		{
		}
	}
}