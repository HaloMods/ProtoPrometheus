/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.PollableThreadManager
 * Description : Manages the process of creating a separate thread
 *             : that can be periodically polled to check for
 *             : completion of an operation.
 * Author      : MonoxideC
 * ---------------------------------------------------------------
 */

using System;
using System.Threading;

namespace Prometheus {
	/// <summary>
	/// Manages the process of creating a separate thread that can be
	/// periodically polled to check for completion of an operation.
	/// </summary>
	public class PollableThreadManager {
		
		public enum AsyncReturn : int {
			Unknown = -2,
			Running = -1,
			CompletedUnsuccessfully = 0,
			CompletedSuccessfully = 1
		}

		public delegate void ThreadMethod();

		private DateTime m_startTime;

		public float RunningTime {
			get { 
				if ((object)m_startTime == null) return 0;
				TimeSpan ts = DateTime.Now.Subtract(m_startTime);
				return (float)ts.TotalSeconds;
			}
		}

		private ThreadMethod m_doExecution;

		/// <summary>
		/// Begin the async operation.  Wait 1 second for the 
		/// operation to complete before returning.
		/// </summary>
		/// <returns>The status of the operation after 1 second.</returns>
		public AsyncReturn BeginExecution() {
			return BeginExecution(1000);
		}

		/// <summary>
		/// Begin the async operation.  Wait the specified number of
		/// milliseconds for the operation to complete before returning.
		/// </summary>
		/// <param name="MillisecondsToWait">0 indicates no wait.  -1 indicates infinite wait.</param>
		/// <returns>The status of the operation after the specified number of milliseconds.</returns>
		public AsyncReturn BeginExecution(int MillisecondsToWait) {
			mbError = false;

			// Start the thread
			ThreadStart startThread = new ThreadStart(m_doExecution);
			mExecution = new Thread(startThread);
			mExecution.Start();			

			// Give it a chance to complete before we go on
			return WaitForCompletion(MillisecondsToWait);
		}

		/// <summary>
		/// Wait the specified number of milliseconds for the 
		/// operation to complete then return the current status
		/// of the operation.
		/// </summary>
		/// <param name="MillisecondsToWait">0 indicates no wait.  -1 indicates infinite wait.</param>
		/// <returns>The status of the operation after the specified number of milliseconds.</returns>
		public AsyncReturn WaitForCompletion(int MillisecondsToWait) {
			// If we don't have a thread, we have problem
			if (mExecution == null) 
				return AsyncReturn.Unknown;

			// If the thread is either stopped or not running
			// yet, wait for it to complete
			if (0 == (mExecution.ThreadState & 
				(ThreadState.Stopped | ThreadState.Unstarted))) {
				if (!mExecution.Join(MillisecondsToWait)) {
					// We didn't finish yet, let the caller know
					return AsyncReturn.Running;
				}
			}

			// We have an error in execution
			if (mbError) 
				return AsyncReturn.CompletedUnsuccessfully;

			// Success!!!
			return AsyncReturn.CompletedSuccessfully;
		}
		
		private bool mbError = false;

		/// <summary>
		/// Our actual thread of execution
		/// </summary>
		private Thread mExecution;

		/// <summary>
		/// Exception thrown during execution (if any).
		/// </summary>
		private Exception mException = null;
		/// <summary>
		/// Exception is thrown during execution, if any.
		/// </summary>
		public Exception ExceptionThrown {
			get { return mException; }
		}

		/// <summary>
		/// The ID provided by the caller
		/// </summary>
		private int m_id;
		/// <summary>
		/// The ID provided by the caller
		/// </summary>
		public int ID {
			get { return m_id; }
		}
	
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="ToFactor">The value to factor.</param>
		public PollableThreadManager(int ID, ThreadMethod DoExecution) {
			m_startTime = DateTime.Now;
			m_id = ID;
			m_doExecution = DoExecution;
		}
	}
}