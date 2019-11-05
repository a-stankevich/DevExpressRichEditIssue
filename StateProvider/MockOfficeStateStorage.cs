using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.Office;

namespace DevExpressRichEditIssue.StateProvider
{
	public class MockOfficeStateStorage : IOfficeStateStorageRemote
	{
		private static readonly Dictionary<string, string> _sessionStates = new Dictionary<string, string>();
		private static readonly Dictionary<string, string> _documentSessions = new Dictionary<string, string>();
		private static readonly Dictionary<string, string> _sharedKeyValue = new Dictionary<string, string>();

		public bool AddCheckedOut(string workSessionId, string documentId)
		{
			return true;
		}

		public bool CheckIn(string workSessionId, string documentId, string workSessionState)
		{
			_sessionStates[workSessionId] = workSessionState;
			return true;
		}

		public bool CheckOut(string workSessionId, out string workSessionState)
		{
			return _sessionStates.TryGetValue(workSessionId, out workSessionState);
		}

		public string FindWorkSessionId(string documentId)
		{
			if (_documentSessions.ContainsKey(documentId))
			{
				return _documentSessions[documentId];
			}
			return null;
		}

		public string Get(string key)
		{
			if (_sharedKeyValue.ContainsKey(key))
			{
				return _sharedKeyValue[key];
			}
			return null;
		}

		public bool HasWorkSessionId(string workSessionId)
		{
			return _sessionStates.ContainsKey(workSessionId);
		}

		public void Remove(string workSessionId)
		{
			_sessionStates.Remove(workSessionId);
		}

		public void Set(string key, string value)
		{
			_sharedKeyValue[key] = value;
		}

		public void UndoCheckOut(string workSessionId)
		{
			// don't care
		}
	}
}