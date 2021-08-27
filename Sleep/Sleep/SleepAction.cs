using System;
using WebCon.WorkFlow.SDK.ActionPlugins;
using WebCon.WorkFlow.SDK.ActionPlugins.Model;

namespace Sleep
{
	public class SleepAction : CustomAction<SleepActionConfig>
	{
		public override void Run(RunCustomActionParams args)
		{
			void log(string msg) => args.Context.PluginLogger.AppendInfo(msg);

			log($"Sleeping for {Configuration.Seconds} seconds...");
			System.Threading.Thread.Sleep(Configuration.Seconds * 1000);
			log("Finished sleeping.");
		}
	}
}