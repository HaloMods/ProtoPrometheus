/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * File        : Console.cs
 * Author      : MonoxideC
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System.Collections;
using System.Diagnostics;

namespace Prometheus.Core.Render
{
	/// <summary>
	/// Provides a means of outputting text to the render window.
	/// </summary>
	public class RenderConsole
	{
    private static StringQueue strings = new StringQueue(5);
    private static int fadeOutDelay = 150;
    private static int fadeCounter = 0;
    private static bool inputMode;
    private static string inputText = "";

    public static string InputText
    {
      get { return inputText; }
      set { inputText = value; }
    }

    public static bool InputMode
    {
      get { return inputMode; }
      set { inputMode = value; }
    }
 
    /// <summary>
    /// Gets the number of lines diaplyed at once in the console.
    /// </summary>
    public static int Length
    {
      get { return strings.MaxLength; }
    }

    public static void Enter()
    {
      inputText = "";
    }
    
    public static void BackSpace()
    {
      inputText = inputText.Substring(0, inputText.Length-1);
    }

    /// <summary>
    /// Sets the number of frames that it takes for a string to fade from the console.
    /// </summary>
    public static int FadeOutDelay
    {
      get { return fadeOutDelay; }
      set { fadeOutDelay = value; }
    }

    public static bool HasLines
    {
      get { return (strings.Count > 0); }
    }

    public static void WriteLine(string s)
    {
      strings.Add(s);
    }

    /// <summary>
    /// Gets all of the lines to display from the console.
    /// </summary>
    public static string[] GetFrame()
    {
      fadeCounter++;
      if (fadeCounter >= fadeOutDelay)
      {
        fadeCounter = 0;
        strings.Pop();
      }
      string[] result = new string[strings.Count];
      for (int x=0; x<strings.Count; x++)
      {
        result[x] = strings[(strings.Count-1)-x];
      }
      return result;
    }
	}

  /// <summary>
  /// A sized collection of strings.
  /// </summary>
  public class StringQueue : CollectionBase
  {
    private int maxLength;

    /// <summary>
    /// The maximum number of lines that will be stored before pushing out the first entry.
    /// </summary>
    public int MaxLength
    {
      get { return maxLength; }
      set { maxLength = value; }
    }

    public void Add(string s)
    {
      if (InnerList.Count >= MaxLength) Pop();
      InnerList.Add(s);
    }

    public string this[int index]
    {
      get { return (string)(InnerList[index]); }
    }

    public StringQueue(int length)
    {
      maxLength = length;
    }

    /// <summary>
    /// Removes the first entry.
    /// </summary>
    public void Pop()
    {
      InnerList.RemoveAt(0);
    }
  }

  public class ConsoleTraceListener : TraceListener
  {
    private string[] categories;

    public ConsoleTraceListener(params string[] categories)
    {
      this.categories = categories;
    }

    public override void Write(string message)
    {
      this.Write(message, "");
    }

    public override void Write(string message, string category)
    {
      foreach (string s in categories)
      {
        if (s == category)
        {
          RenderConsole.WriteLine(message);          
          return;
        }
      }
    }

    public override void WriteLine(string message)
    {
      this.WriteLine(message, "");
    }

    public override void WriteLine(string message, string category)
    {
      
      this.Write(message);
    }
  }
}
