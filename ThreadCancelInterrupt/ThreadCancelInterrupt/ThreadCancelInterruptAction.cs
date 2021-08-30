using System;
using WebCon.WorkFlow.SDK.ActionPlugins;
using WebCon.WorkFlow.SDK.ActionPlugins.Model;

namespace ThreadCancelInterrupt
{
	public class ThreadCancelInterruptAction : CustomAction<ThreadCancelInterruptActionConfig>
	{
		public override void Run(RunCustomActionParams args)
		{
			void log(string msg) => args.Context.PluginLogger.AppendInfo(msg);

			// execution time of the whole action
			var stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();

			log("Configuration. " +
				$"SecondsToSleepFor: {Configuration.SecondsToSleepFor}; " +
				$"SecondsToRequestToFinish: {Configuration.SecondsToRequestToFinish}; " +
				$"FinishByInterruptDelayInSeconds: {Configuration.InterruptDelayInSeconds};");

			// used to communicate to the work thread that it should finish
			var cancellationTokenSource = new System.Threading.CancellationTokenSource();
			var cancellationToken = cancellationTokenSource.Token;

			var thread = new System.Threading.Thread(() =>
			{
				// ------------ actual work that the action is supposed to do goes here ------------

				// the cancellation token should be checked here, to gracefully finish work and end the thread
				// but for the purpose of the example, it's just a simple sleep

				log($"Work thread is sleeping for {Configuration.SecondsToSleepFor} seconds...");
				System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(Configuration.SecondsToSleepFor));
				log("Work thread has finished sleeping.");
				log($"Work thread; IsCancellationRequested == {cancellationToken.IsCancellationRequested}");

				// ------------ actual work that the action is supposed to do goes here ------------
			});

			thread.Start();

			void logThreadStatus() => log($"{stopwatch.Elapsed.TotalSeconds} has passed since start of action;" +
				$" and thread.isAlive == {thread.IsAlive}");

			// try to join with a timeout; cancel the thread
			thread.Join(System.TimeSpan.FromSeconds(Configuration.SecondsToRequestToFinish));
			logThreadStatus();
			log("Cancelling the work thread.");
			cancellationTokenSource.Cancel();

			// interrupt if still running
			System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(Configuration.InterruptDelayInSeconds));
			logThreadStatus();
			log("Interrupting the work thread.");
			thread.Interrupt();

			// give the work thread some time to actually finish
			System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(Configuration.InterruptDelayInSeconds));
			logThreadStatus();

			// finish the main thread - if the work thread is still running, the action will error out
			stopwatch.Stop();
			log("This action plugin has been running " +
				$"for {stopwatch.Elapsed.TotalSeconds} seconds, " +
				$"or exactly {stopwatch.Elapsed.TotalMilliseconds} milliseconds.");
		}
	}
}