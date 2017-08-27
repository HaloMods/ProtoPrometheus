using System.Collections;

namespace Core.Util
{
	/// <summary>
	/// Used to get a list of the last
	/// 'MAX_ELEMENTS' actions that the
	/// user performed to return to the
	/// debug servers or to where ever
	/// we send the crash data too.
	/// 
	/// Since N- doesn't seem to want to
	/// include a pdb file in the release
	/// to get debug stack info we use this,
	/// so probably encapsulate your
	/// "ActionStack.Push" function 
	/// in a #if RELEASE macro
	/// </summary>
	public class ActionStack
	{
		/// <summary>
		/// Max elements in the stack allowed
		/// </summary>
		public const int MAX_ELEMENTS = 20;

		/// <summary>
		/// Internal stack
		/// </summary>
		private static Stack Stack = new Stack(MAX_ELEMENTS);

		/// <summary>
		/// Its a pesudo static class,
		/// so do any crap here that
		/// needs to be done at runtime
		/// </summary>
		static ActionStack() {}

		/// <summary>
		/// Adds an event to the stack
		/// Put this in the very beginning of the GUI function
		/// Ex: 
		/// ActionStack.Push("MdxControl", "Click");
		/// ActionStack.Push("MdxControl", "MouseEnter");
		/// </summary>
		/// <param name="obj">name of the GUI object</param>
		/// <param name="ui_event">obj's ui event</param>
		public static void Push(string obj, string ui_event)
		{
			Stack.Push(obj + " : " + ui_event);
		}

		/// <summary>
		/// Takes the stack and makes
		/// a single string out of all
		/// the elements. Appends "\r\n"
		/// to each element in the return
		/// value
		/// </summary>
		/// <returns>Single string of the stack</returns>
		public static new string ToString()
		{
			string value = string.Empty;
			//foreach(string s in Stack)
			//	value += v + "\r\n";

			return value;
		}

//		public static void DevExpressClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
//		{
//		}

		/// <summary>
		/// Adds 'WindowsControlClick' to obj's Click event and to
		/// any other controls in obj
		/// </summary>
		/// <param name="obj">control to attach to</param>
		public static void AttachWinClickListener(System.Windows.Forms.Control obj)
		{
			obj.Click += new System.EventHandler(WindowsControlClick);

			foreach(System.Windows.Forms.Control c in obj.Controls)
				AttachWinClickListener(c);
		}

		public static void WindowsControlClick(object sender, System.EventArgs e)
		{
			if( !(sender is System.Windows.Forms.Control) ) return;
			System.Windows.Forms.Control control = sender as System.Windows.Forms.Control;
			Push(control.Name, "Click");
		}
	}
}