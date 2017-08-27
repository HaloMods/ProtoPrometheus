/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.RichTextBoxTraceListener
 * Description : Trace Listener class that writes color coded
 *               category-based output to a RichTextBox control.
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Prometheus
{
	/// <summary>
	/// Trace Listener class that writes color coded
	/// category-based output to a RichTextBox control.
	/// </summary>
	public class RichTextBoxTraceListener : TraceListener
	{
    public static bool EnableWindowOutput = false;
    private const int WM_VSCROLL = 0x115;
    private const int SB_BOTTOM = 7;
    protected RichTextBox m_rtBox;

		public RichTextBoxTraceListener(string ListenerName, ref RichTextBox rtBox)
			: base(ListenerName)
		{
			m_rtBox = rtBox;
			if ((object)rtBox == null)
				throw new Exception("Cannot create RichTextBoxListener: Control is null");
		}
		
    //
    [System.Security.SuppressUnmanagedCodeSecurity] 
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

		public override void Write(string s, string category)
		{
      if(EnableWindowOutput == true) //for debugging massive text output
      {
        try  
        {
          // ToDo: Perhaps initialize custom category colors from a different collection
          // when the object is constructed.
          Color f = Color.Black; // Default color is black
          category = category.ToLower(); // Convert to lower case for consistency
			
          // Set text color based on category name
          if (category == "info") f = Color.Blue;
          if (category == "warning") f = Color.Orange;
          if (category == "error") f = Color.Red;
			
          const int MaxLines = 10;
          string[] Lines = m_rtBox.Lines;

          int StartIndex = Lines.Length - MaxLines;

          if(StartIndex > 0)
          {
            //clip the extra line(s)
            string[] Output = new string[MaxLines];

            for(int i=0; i<MaxLines; i++)
              Output[i] = Lines[StartIndex + i];

            m_rtBox.Clear();
            m_rtBox.Lines = Lines;
          }

          /*
  rivate void Debug_Messages(string msg)
      {
        //richTextBox.AppendText("\n" + msg);
        richTextBox.Select(richTextBox.TextLength, 0);
        richTextBox.SelectedText = msg;
      }
    
       maybe try rtb.Focus(); rtb.AppendText(msg); rtb.ScrollToCaret(); rtb.Focus();
           */

          // Write the item
          m_rtBox.SelectionStart = m_rtBox.Text.Length;
          m_rtBox.SelectionColor = f;
          string outputText = DateTime.Now + " : " + s;
          m_rtBox.SelectedText = outputText;
          m_rtBox.Select(m_rtBox.Text.Length, 0);
          //m_rtBox.AppendText(outputText);
          //m_rtBox.Select(m_rtBox.Text.Length, 0);
          SendMessage(this.m_rtBox.Handle, WM_VSCROLL, SB_BOTTOM, 0);
        }
        catch (Exception ex) 
        {
          throw new Exception("Could not write to RichBoxTextListener (" + this.Name + ")", ex);
        }
      }
		}
		public override void Write(string s)
		{
			this.Write(s, "");
		}
		public override void WriteLine(string s, string category)
		{
			this.Write(s + "\r\n", category);
		}
		public override void WriteLine(string s)
		{
			this.WriteLine(s, "");
		}
		
	}
}
