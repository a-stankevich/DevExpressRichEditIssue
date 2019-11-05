using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.Office;
using DevExpress.Web.Office.Internal;

namespace DevExpressRichEditIssue.StateProvider
{
	public class MockOfficeStateStorage : IOfficeStateStorageRemote
	{
		private static readonly Dictionary<string, string> _sessionStates = new Dictionary<string, string>();
		private static readonly Dictionary<string, string> _documentSessions = new Dictionary<string, string>();
		private static readonly Dictionary<string, string> _sharedKeyValue = new Dictionary<string, string>();

		private static void log(string workSessionId = null)
		{
			var method = new System.Diagnostics.StackFrame(1).GetMethod().Name;
			var msg = "Storage." + method;
			if (!string.IsNullOrEmpty(workSessionId))
			{
				msg += " " + workSessionId;
			}
			System.Diagnostics.Trace.WriteLine(msg);
		}

		public bool AddCheckedOut(string workSessionId, string documentId)
		{
			log(workSessionId);
			return true;
		}

		public bool CheckIn(string workSessionId, string documentId, string workSessionState)
		{
			log(workSessionId);
			_sessionStates[workSessionId] = workSessionState;
			return true;
		}

		public bool CheckOut(string workSessionId, out string workSessionState)
		{
			log(workSessionId);
			var result = _sessionStates.TryGetValue(workSessionId, out workSessionState);
			if (result)
			{
				// this will crash here if deserialization fails
				WorkSessionFactoryStateContainerHelper.ProduceWorkSessionFromContainer(Guid.Empty, workSessionState);
			}
			return result;
		}

		public string FindWorkSessionId(string documentId)
		{
			log();
			if (_documentSessions.ContainsKey(documentId))
			{
				return _documentSessions[documentId];
			}
			return null;
		}

		public string Get(string key)
		{
			log();
			if (_sharedKeyValue.ContainsKey(key))
			{
				return _sharedKeyValue[key];
			}
			return null;
		}

		public bool HasWorkSessionId(string workSessionId)
		{
			log(workSessionId);
			return _sessionStates.ContainsKey(workSessionId);
		}

		public void Remove(string workSessionId)
		{
			log(workSessionId);
			_sessionStates.Remove(workSessionId);
		}

		public void Set(string key, string value)
		{
			log();
			_sharedKeyValue[key] = value;
		}

		public void UndoCheckOut(string workSessionId)
		{
			log(workSessionId);
			// don't care
		}
	}
}