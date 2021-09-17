using WebCon.WorkFlow.SDK.Common;
using WebCon.WorkFlow.SDK.ConfigAttributes;

namespace TimeoutProcessStart
{
	public class TimeoutProcessStartActionConfig : PluginConfiguration
	{
		//[ConfigEditableText(DisplayName = "Source form field ID")]
		//public string SourceFormFieldID { get; set; }

		//[ConfigEditableText(DisplayName = "Destination form field ID")]
		//public string DestinationFormFieldID { get; set; }

		//[ConfigEditableText(DisplayName = "Price form field ID", Description = "ID of the form field which contains price of the product")]
		//public string PriceFormFieldID { get; set; }

		//[ConfigEditableInteger(DisplayName = "Price")]
		//public int Price { get; set; }

		[ConfigEditableInteger(DisplayName = "DocType ID", IsRequired = true)]
		public int DocTypeID { get; set; }

		[ConfigEditableInteger(DisplayName = "Company ID", IsRequired = true)]
		public int CompanyID { get; set; }

		[ConfigEditableInteger(DisplayName = "Path ID", IsRequired = true)]
		public int PathID { get; set; }

		[ConfigEditableBool(DisplayName = "Use current Workflow ID")]
		public bool useCurrentWorkflowID { get; set; }

		[ConfigEditableInteger(DisplayName = "Workflow ID")]
		public int WorkFlowID { get; set; }

		[ConfigEditableText(DisplayName = "UserSearch DisplayName")]
		public string UserSearchDisplayName { get; set; }
	}
}