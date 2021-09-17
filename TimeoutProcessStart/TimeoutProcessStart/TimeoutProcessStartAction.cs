using System;
using WebCon.WorkFlow.SDK.ActionPlugins;
using WebCon.WorkFlow.SDK.ActionPlugins.Model;

namespace TimeoutProcessStart
{
	public class TimeoutProcessStartAction : CustomAction<TimeoutProcessStartActionConfig>
	{

		public override void RunWithoutDocumentContext(RunCustomActionWithoutContextParams args)
		{
			void log(string msg) => args.Context.PluginLogger.AppendInfo(msg);

			log("TimeoutProcessStartAction hello");


			var docTypeId = Configuration.DocTypeID;
			var workFlowId = Configuration.useCurrentWorkflowID ? args.Context.CurrentWorkflowID : Configuration.WorkFlowID;
			var newDocParams = new WebCon.WorkFlow.SDK.Documents.Model.GetNewDocumentParams(docTypeId, workFlowId);
			newDocParams.CompanyID = Configuration.CompanyID;

			var newDocData = WebCon.WorkFlow.SDK.Documents.DocumentsManager.GetNewDocument(newDocParams);
			log("created a new doc");

			var path = Configuration.PathID;

			//newDocData.Attachments.AddNew("New", System.Text.Encoding.ASCII.GetBytes("ok"));


			var newWorkflowParams = new WebCon.WorkFlow.SDK.Documents.Model.StartNewWorkFlowParams(newDocData, path);

			var userSearchParams = WebCon.WorkFlow.SDK.Tools.Users.Model.UserSearchParameters.FromDisplayName(Configuration.UserSearchDisplayName);
			var bpsAccount = WebCon.WorkFlow.SDK.Tools.Users.UserDataProvider.Validate(userSearchParams);
			var userInfo = new WebCon.WorkFlow.SDK.Common.Model.UserInfo(bpsAccount.BpsID, bpsAccount.DisplayName);
			newWorkflowParams.AssignedPersons.Add(userInfo);

			WebCon.WorkFlow.SDK.Documents.DocumentsManager.StartNewWorkFlow(newWorkflowParams);
			log("started the workflow");
		}
	}
}