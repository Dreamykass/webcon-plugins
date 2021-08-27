using WebCon.WorkFlow.SDK.Common;
using WebCon.WorkFlow.SDK.ConfigAttributes;

namespace ThreadInterruptAbort
{
	public class ThreadInterruptAbortActionConfig : PluginConfiguration
	{
		[ConfigEditableInteger(DisplayName = "Number of seconds to sleep for, on the launched work thread.")]
		public int SecondsToSleepFor { get; set; } = 30;

		[ConfigEditableInteger(DisplayName = "Number of seconds after which the main thread will kindly request the launched work thread to finish.")]
		public int SecondsToRequestToFinish { get; set; } = 30;

		[ConfigEditableInteger(DisplayName = "Delay after the kind request to finish, that the thread will be less nicely interrupted and forced to finish.")]
		public int InterruptDelayInSeconds { get; set; } = 30;

		[ConfigEditableInteger(DisplayName = "Delay after the less nice interruoption, that the thread will be very violently aborted.")]
		public int AbortDelayInSeconds { get; set; } = 30;


	}
}