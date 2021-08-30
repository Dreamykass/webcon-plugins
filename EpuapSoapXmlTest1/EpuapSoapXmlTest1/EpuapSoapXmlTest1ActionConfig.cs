using WebCon.WorkFlow.SDK.Common;
using WebCon.WorkFlow.SDK.ConfigAttributes;

namespace EpuapSoapXmlTest1
{
	public class EpuapSoapXmlTest1ActionConfig : PluginConfiguration
	{
		[ConfigEditableInteger(DisplayName = "Number of seconds after which the main thread will kindly request the launched work thread to finish.",
			DefaultValue = 10)]
		public int SecondsToCancel { get; set; } = 30;

		[ConfigEditableInteger(DisplayName = "Delay after the kind request to finish, that the thread will be less nicely interrupted and forced to finish.",
			DefaultValue = 4)]
		public int SecondsAfterCancelToInterrupt { get; set; } = 2;

		[ConfigEditableText(DisplayName = " webClient.UploadString(")]
		public string WebClientUploadString { get; set; }
	}
}