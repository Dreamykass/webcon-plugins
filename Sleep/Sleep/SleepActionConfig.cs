using WebCon.WorkFlow.SDK.Common;
using WebCon.WorkFlow.SDK.ConfigAttributes;

namespace Sleep
{
	public class SleepActionConfig : PluginConfiguration
	{
		[ConfigEditableInteger(DisplayName = "Number of seconds to sleep for.")]
		public int Seconds { get; set; } = 30;
	}
}