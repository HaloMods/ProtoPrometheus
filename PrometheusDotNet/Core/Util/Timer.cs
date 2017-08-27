using System;
using System.Collections;

namespace Prometheus.Core
{
	/// <summary>
	/// In Progress - Will be used to easily globally measure execution
	/// time of certain events.
	/// </summary>
	public class Timer
	{
    private static Hashtable m_trackers = new Hashtable();
    public static Hashtable Trackers
    {
      get { return m_trackers; }
    }
    public static void AddTracker(string name)
    {
      if (m_trackers.Contains(name)) m_trackers.Remove(name);
      m_trackers.Add(name, new TimerTracker(name));
    }
    public static void Remove(string name)
    {
      m_trackers.Remove(name);
    }
	}
  public class TimerTracker
  {
    public DateTime StartTime;
    public DateTime EndTime;
    public TimeSpan Total
    {
      get { return EndTime.Subtract(StartTime); }
    }
    public string Name;
    public TimerTracker(string name)
    {
      Name = name;
      Start();
    }
    public void Start()
    {
      StartTime = DateTime.Now;
    }
    public TimeSpan Stop()
    {
      EndTime = DateTime.Now;
      return Total;
    }
  }
}
