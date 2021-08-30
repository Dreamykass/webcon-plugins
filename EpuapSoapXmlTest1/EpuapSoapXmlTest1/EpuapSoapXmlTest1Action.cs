using System;
using WebCon.WorkFlow.SDK.ActionPlugins;
using WebCon.WorkFlow.SDK.ActionPlugins.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using XMLSerializable.Models.Faults;
using XMLSerializable.Models.RequestCode;
using XMLSerializable;

namespace EpuapSoapXmlTest1
{
	public class EpuapSoapXmlTest1Action : CustomAction<EpuapSoapXmlTest1ActionConfig>
	{
		public override void Run(RunCustomActionParams args)
		{
			void log(string msg) => args.Context.PluginLogger.AppendInfo(msg);

			// execution time of the whole action
			var stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();

			log("EpuapSoapXmlTest1Action");

			// used to communicate to the work thread that it should finish
			var cancellationTokenSource = new System.Threading.CancellationTokenSource();
			var cancellationToken = cancellationTokenSource.Token;

			var thread = new System.Threading.Thread(() =>
			{
				// ------------ actual work that the action is supposed to do goes here ------------

				Envelope env = new Envelope("id");

				XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
				ns.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
				ns.Add("sig", "http://signing2.zp.epuap.gov.pl");

				XmlSerializer mySerializer = new XmlSerializer(typeof(Envelope));
				// To write to a file, create a StreamWriter object.  
				//StreamWriter myWriter = new StreamWriter("myFileName.xml");
				//mySerializer.Serialize(myWriter, env, ns);
				//myWriter.Close();

				XmlSerializer xmlSerializer = new XmlSerializer(env.GetType());

				string requestString = null;

				using (StringWriter textWriter = new StringWriter())
				{
					xmlSerializer.Serialize(textWriter, env, ns);
					requestString = textWriter.ToString();
				}

				log($"requestString: {requestString}");

				Fault fault = null;

				// sending WS-Security request
				using (WebClient webClient = new WebClient())
				{
					//webClient.Proxy = new System.Net.WebProxy("http://localhost:8888");
					webClient.Encoding = Encoding.UTF8;
					if (!string.IsNullOrEmpty(env.Body.RequestCode.SOAPAction))
					{
						webClient.Headers.Add("SOAPAction", env.Body.RequestCode.SOAPAction);
					}
					webClient.Headers[HttpRequestHeader.ContentType] = "text/xml";

					string response = null;
					try
					{
						//wybierz gdzie wysłać
						response = webClient.UploadString(Configuration.WebClientUploadString, requestString);
					}
					catch (WebException ex)
					{
						if (ex.Response != null)
						{
							var responseStream = ex.Response.GetResponseStream();
							if (responseStream != null)
							{
								using (var reader = new StreamReader(responseStream))
								{

								}
							}
						}

					}

					if (!string.IsNullOrEmpty(response))
					{
					}
					else
					{
					}
				}

				// ------------ actual work that the action is supposed to do goes here ------------
			});

			thread.Start();

			void logThreadStatus() => log($"{stopwatch.Elapsed.TotalSeconds} has passed since start of action;" +
				$" and thread.isAlive == {thread.IsAlive}");

			// try to join with a timeout; cancel the thread
			thread.Join(System.TimeSpan.FromSeconds(Configuration.SecondsToCancel));
			logThreadStatus();
			log("Cancelling the work thread.");
			cancellationTokenSource.Cancel();

			// interrupt if still running
			System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(Configuration.SecondsAfterCancelToInterrupt));
			logThreadStatus();
			log("Interrupting the work thread.");
			thread.Interrupt();

			// give the work thread some time to actually finish
			System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(Configuration.SecondsAfterCancelToInterrupt));
			logThreadStatus();

			// finish the main thread - if the work thread is still running, the action will error out
			stopwatch.Stop();
			log("This action plugin has been running " +
				$"for {stopwatch.Elapsed.TotalSeconds} seconds, " +
				$"or exactly {stopwatch.Elapsed.TotalMilliseconds} milliseconds.");
		}
	}
}