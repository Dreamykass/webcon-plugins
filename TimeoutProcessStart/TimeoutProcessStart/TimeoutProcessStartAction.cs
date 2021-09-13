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


			var docTypeId = 1; // 0 errors out as invalid document type
			var workFlowId = args.Context.CurrentWorkflowID;
			var newDocData = WebCon.WorkFlow.SDK.Documents.DocumentsManager.GetNewDocument(
				new WebCon.WorkFlow.SDK.Documents.Model.GetNewDocumentParams(docTypeId, workFlowId)
			);
			var path = 0;

			log("created a new doc");

			WebCon.WorkFlow.SDK.Documents.DocumentsManager.StartNewWorkFlow(
				new WebCon.WorkFlow.SDK.Documents.Model.StartNewWorkFlowParams(
					newDocData, path
				)
			);

			log("started the workflow");
		}
	}
}