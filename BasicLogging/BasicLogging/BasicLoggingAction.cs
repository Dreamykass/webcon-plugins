using System;
using WebCon.WorkFlow.SDK.ActionPlugins;
using WebCon.WorkFlow.SDK.ActionPlugins.Model;

namespace BasicLogging
{
	public class BasicLoggingAction : CustomAction<BasicLoggingActionConfig>
	{
		public override void Run(RunCustomActionParams args)
		{
			void log(string msg) => args.Context.PluginLogger.AppendInfo(msg);

			log($"BasicLoggingAction here hello");
			log($"attachments count: {args.Context.CurrentDocument.Attachments.Count}");
			log("now logging all the attachments:");

			foreach (var attachment in args.Context.CurrentDocument.Attachments)
			{
				log($"filename: {attachment.FileName}" +
					$"; extension: {attachment.FileExtension}" +
					$"; content.length (bytes): {attachment.Content.Length}" +
					$"; description: {attachment.Description}");

			}
		}
	}
}