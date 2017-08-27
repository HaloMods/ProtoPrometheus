/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Core.PrometheusException
 * Description : Extends the System.ApplicationException class to
 *               provide more robust error handling capabilities.
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Diagnostics;
using System.Text;

namespace Prometheus.Core
{
	/// <summary>
	/// Enhanced base for exception handling in Prometheus
	/// </summary>
	public class PrometheusException : System.ApplicationException
	{
		private int m_nCode;

		public PrometheusException(){;}
		
		public PrometheusException(string sMessage, bool bLog) : this(sMessage,
			null, bLog){;}

		public PrometheusException(string sMessage, System.Exception
			oInnerException, bool bLog) : this(null, 0, sMessage,
			oInnerException, bLog){;}

		public PrometheusException(object oSource, int nCode, string sMessage,
			bool bLog) : this(oSource, nCode, sMessage, null,          bLog){;}

		public PrometheusException(object oSource, int nCode, string sMessage,
			System.Exception oInnerException, bool bLog) :
			base(sMessage, oInnerException)
		{
			if (oSource != null)
				base.Source = oSource.ToString();
			Code = nCode;

			// Need to add logic to check what log destination we should logging to
			// ex: File, Remote Database, etc.
			if (bLog)
			{
				// Initialize trace listeners, just in case
				Utility.InitTraceListeners();

				// Write the exception to the listeners
				Dump(Format(oSource, nCode, sMessage,
					oInnerException));
			}
		}

		/// <summary>
		/// Writes the entire message to all trace listeners.
		/// </summary>
		/// <param name="sMessage">Message to dump.</param>
		private void Dump(string sMessage)
		{
			// Write to all trace listeners
			//Trace.WriteLineIf(Config.TraceLevel.TraceError, sMessage);  // Implement this later
			Trace.WriteLine(sMessage); // Do this for now.
		}

		public static string Format(object oSource, int nCode, string
			sMessage, System.Exception oInnerException)
		{
			StringBuilder sNewMessage = new StringBuilder();
			string sErrorStack = null;

			// Get the error stack - If InnerException is null, sErrorStack
			// will be "exception was not chained" (will never be null)
			sErrorStack = BuildErrorStack(oInnerException);
			Trace.AutoFlush = true;

			sNewMessage.Append("[ Exception Summary ] \n")
				.Append(DateTime.Now.ToShortDateString())
				.Append(":")
				.Append(DateTime.Now.ToShortTimeString())
				.Append(" - ")
				.Append(sMessage)
				.Append("\n\n")
				.Append(sErrorStack);

			return sNewMessage.ToString();
		}

		/// <summary>
		/// Takes a first nested exception object and builds an error stack
		/// from its chained contents.
		/// </summary>
		private static string BuildErrorStack(System.Exception
			oChainedException)
		{
			string sErrorStack = null;
			StringBuilder sbErrorStack = new StringBuilder();
			int nErrStackNum = 1;
			System.Exception oInnerException = null;

			if (oChainedException != null)
			{
				sbErrorStack.Append("[ Error Stack ] \n");

				oInnerException = oChainedException;
				while (oInnerException != null)
				{
					sbErrorStack.Append(nErrStackNum)
						.Append(": ")
						.Append(oInnerException.Message)
						.Append("\n");

					oInnerException =
						oInnerException.InnerException;

					nErrStackNum++;
				}

				sbErrorStack.Append("\n----------------------\n")
					.Append("Call Stack\n")
					.Append(oChainedException.StackTrace);

				sErrorStack = sbErrorStack.ToString();
			}
			else
			{
				sErrorStack = "exception was not chained";
			}

			return sErrorStack;
		}

		public int Code
		{
			get {return m_nCode;}
			set {m_nCode = value;}
		}
	}
}
