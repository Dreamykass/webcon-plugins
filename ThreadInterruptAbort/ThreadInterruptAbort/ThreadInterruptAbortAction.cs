using WebCon.WorkFlow.SDK.ActionPlugins;
using WebCon.WorkFlow.SDK.ActionPlugins.Model;

namespace ThreadInterruptAbort
{
	public class ThreadInterruptAbortAction : CustomAction<ThreadInterruptAbortActionConfig>
	{
		public override void Run(RunCustomActionParams args)
		{
			void log(string msg) => args.Context.PluginLogger.AppendInfo(msg);

			var stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();

			log("Configuration. " +
				$"SecondsToSleepFor: {Configuration.SecondsToSleepFor}; " +
				$"SecondsToRequestToFinish: {Configuration.SecondsToRequestToFinish}; " +
				$"FinishByInterruptDelayInSeconds: {Configuration.InterruptDelayInSeconds}; " +
				$"FinishByAbortDelayInSeconds: {Configuration.AbortDelayInSeconds};");

			var cancellationTokenSource = new System.Threading.CancellationTokenSource();
			var cancellationToken = cancellationTokenSource.Token;

			var thread = new System.Threading.Thread(() =>
			{
				// ------------ actual work that the action is supposed to do goes here ------------

				// the cancellation token should be checked here, to gracefully finish work and end the thread
				// but for the purpose of the example, it's just a simple sleep

				log($"Work thread is sleeping for {Configuration.SecondsToSleepFor} seconds...");
				System.Threading.Thread.Sleep(Configuration.SecondsToSleepFor * 1000);
				log("Work thread has finished sleeping.");
				log($"Work thread; IsCancellationRequested == {cancellationToken.IsCancellationRequested}");

				// ------------ actual work that the action is supposed to do goes here ------------
			});

			thread.Start();

			void logThreadStatus() => log($"{stopwatch.Elapsed.TotalSeconds} has passed since start of action;" +
				$" and thread.isAlive == {thread.IsAlive}");

			System.Threading.Thread.Sleep(Configuration.SecondsToRequestToFinish * 1000);
			logThreadStatus();
			log("Cancelling the work thread.");
			cancellationTokenSource.Cancel();

			System.Threading.Thread.Sleep(Configuration.InterruptDelayInSeconds * 1000);
			logThreadStatus();
			log("Interrupting the work thread.");
			thread.Interrupt();

			System.Threading.Thread.Sleep(Configuration.AbortDelayInSeconds * 1000);
			logThreadStatus();
			log("Aborting the work thread.");
			thread.Interrupt();

			stopwatch.Stop();
			log("This action plugin has been running " +
				$"for {stopwatch.Elapsed.TotalSeconds} seconds, " +
				$"or exactly {stopwatch.Elapsed.TotalMilliseconds} milliseconds.");
		}
	}
}