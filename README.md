# DevExpressRichEditIssue

This is a repo to reproduce DevExpress RichEdit control state deserialization bug.

The bug is reproducible with attached BadList.docx file.

The error is NullReferenceException with the following stacktrace.
```
   at DevExpress.Web.ASPxRichEdit.Export.JSONImportHelper.HtValue[T](Hashtable ht, Object enumValue)
   at DevExpress.Web.ASPxRichEdit.Export.NumberingListReferenceLevelExporter.FromHashtable(NumberingListReferenceLevel listLevel, Hashtable jsonLevel)
   at DevExpress.Web.ASPxRichEdit.Export.NumberingListExporter.FromHashtable(DocumentModel documentModel, ArrayList jsonNumberingLists, WebCaches webCaches)
   at DevExpress.Web.ASPxRichEdit.Export.DocumentModelExporter.ImportFromHashtable(DocumentModel documentModel, Hashtable jsonDocumentModel, WebCaches webCaches)
   at DevExpress.Web.ASPxRichEdit.Export.JSONImportHelper.ImportModelFromJSON(DocumentModel documentModel, String jsonAsString, WebCaches webCaches)
   at DevExpress.Web.ASPxRichEdit.Internal.RichEditWorkSession.RestoreFromHibernationChamber(HibernationChamber hibernationChamber)
   at DevExpress.Web.Office.Internal.WorkSessionBase.RestoreFromHibernationContainer(Guid workSessionId, WorkSessionStateContainer hibernationContainer)
   at DevExpress.Web.Office.Internal.WorkSessions.RestoreFromHibernationContainer(WorkSessionBase workSession, Guid workSessionId, WorkSessionStateContainer hibernationContainer)
   at DevExpress.Web.Office.Internal.WorkSessionFactoryStateContainerHelper.ProduceWorkSessionFromContainer(Guid workSessionId, WorkSessionStateContainer hibernationContainer)
   at DevExpress.Web.Office.OfficeStateProviderBase.CheckOut(Guid workSessionId)
   at DevExpress.Web.Office.Internal.WorkSessionRepository.CheckOut(Guid workSessionId)
   at DevExpress.Web.Office.Internal.WorkSessions.CheckOutWorkSession(Guid workSessionID)
   at DevExpress.Web.ASPxRichEdit.ASPxRichEdit.get_CurrentSession()
   at DevExpress.Web.ASPxRichEdit.ASPxRichEdit.SyncWorkSessionSettings()
   at DevExpress.Web.ASPxRichEdit.ASPxRichEdit.set_WorkSessionGuid(Guid value)
   at DevExpress.Web.ASPxRichEdit.ASPxRichEdit.LoadWorkSessionIdFromRequest()
   at DevExpress.Web.Mvc.MVCxRichEdit.AssignReadOnlyToWorkSessionClient()
   at DevExpress.Web.ASPxRichEdit.ASPxRichEdit.set_ReadOnly(Boolean value)
   at DevExpress.Web.Mvc.RichEditExtension.AssignInitialProperties()
   at DevExpress.Web.Mvc.ExtensionBase..ctor(SettingsBase settings, ViewContext viewContext, ModelMetadata metadata, Action`1 onBeforeCreateControl)
   at DevExpress.Web.Mvc.RichEditExtension..ctor(RichEditSettings settings, ViewContext viewContext)
```

Since this only happens when using State Provider, `MockOfficeStateProvider` was added. The problem is the same as with `SqlOfficeStateProvider`.

Steps to reproduce:
1. Run website
2. Click 'Bad file' link
3. The page reloads with rich edit containing the 'bad' file
4. Spell checker triggers document state load. (If not spell checker, the same would be triggered by other actions)
5. An exception happens in the DevExpress internals, which then causes 404 errors on calls to `DXS.ashx`
