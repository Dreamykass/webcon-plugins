using WebCon.WorkFlow.SDK.Common;
using WebCon.WorkFlow.SDK.ConfigAttributes;

namespace DiceRollForNextStep
{
	public class DiceRollForNextStepActionConfig : PluginConfiguration
	{
		[ConfigEditableText(DisplayName = "Success Path ID (to go to, if we roll a 6)", IsRequired = true)]
		public int SuccessPathId { get; set; }

		[ConfigEditableText(DisplayName = "Fail Path ID (to go to, if we don't roll a 6)", IsRequired = true)]
		public int FailPathId { get; set; }
	}
}