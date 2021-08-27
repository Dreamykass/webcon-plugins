﻿using System;
using WebCon.WorkFlow.SDK.ActionPlugins;
using WebCon.WorkFlow.SDK.ActionPlugins.Model;

namespace DiceRollForNextStep
{
	public class DiceRollForNextStepAction : CustomAction<DiceRollForNextStepActionConfig>
	{
		public override void Run(RunCustomActionParams args)
		{
			void log(string msg) => args.Context.PluginLogger.AppendInfo(msg);

			log("DiceRollForNextStepAction");
			log($"current workflow id: {args.Context.CurrentWorkflowID}");
			log($"current process id: {args.Context.CurrentProcessID}");
			log($"current document step id: {args.Context.CurrentDocument.StepID}");
			log($"success path id: {Configuration.SuccessPathId}");
			log($"fail path id: {Configuration.FailPathId}");

			var roll = new Random().Next(1, 7); // [1,2,3,4,5,6]
			log($"rolled: {roll}");

			if (roll == 6)
			{
				log("rolled a 6, going to success step");
				//MoveDocumentToNextStep2(args.Context.CurrentDocument, Configuration.SuccessPathId);
				MoveDocumentToNextStep(args.Context.CurrentWorkflowID, Configuration.SuccessPathId);
			}
			else
			{
				log("didn't roll a 6, going to fail step");
				//MoveDocumentToNextStep2(args.Context.CurrentDocument, Configuration.FailPathId);
				MoveDocumentToNextStep(args.Context.CurrentWorkflowID, Configuration.FailPathId);
			}

		}

		private void MoveDocumentToNextStep(int workflowId, int pathId)
		{
			var document = WebCon.WorkFlow.SDK.Documents.DocumentsManager.GetDocumentByID(workflowId, true);
			WebCon.WorkFlow.SDK.Documents.DocumentsManager.MoveDocumentToNextStep(
				new WebCon.WorkFlow.SDK.Documents.Model.MoveDocumentToNextStepParams(document, pathId));
		}

		private void MoveDocumentToNextStep2(WebCon.WorkFlow.SDK.Documents.Model.ExistingDocumentData existingDocumentData, int pathId)
		{
			WebCon.WorkFlow.SDK.Documents.DocumentsManager.MoveDocumentToNextStep(
				new WebCon.WorkFlow.SDK.Documents.Model.MoveDocumentToNextStepParams(existingDocumentData, pathId));
		}
	}
}