/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.ProgressStatus
 * Description : A sub-classed StatusBar control, extended to
 *             : provide a single panel that contains a
 *             : ProgressBar control.
 * Author      : MonoxideC
 * ---------------------------------------------------------------
 */

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Prometheus {
	/// <summary>
	/// A sub-classed StatusBar control, extended to provide a single 
	/// panel that contains a ProgressBar control.
	/// </summary>
	public class ProgressStatus : StatusBar {
		public ProgressBar progressBar = new ProgressBar();
		private int panelWithProgressBar = -1;
		private Thread thAnimation;
    private TD.SandDock.Rendering.RendererBase m_renderer;

    public TD.SandDock.Rendering.RendererBase Renderer
		{
		  get { return m_renderer; }
      set
      {
        m_renderer = value;
        if (m_renderer is TD.SandDock.Rendering.Office2003Renderer)
        {
          TD.SandDock.Rendering.Office2003Renderer renderer = 
            (TD.SandDock.Rendering.Office2003Renderer)m_renderer;
          this.BackColor = renderer.BackgroundColor;
        }
      }
		}

    public void StartAnimation() {
			if (thAnimation.ThreadState != ThreadState.Running)
				thAnimation.Start();
		}
		
		public void StopAnimation() {
			if (thAnimation.ThreadState == ThreadState.Running)
				thAnimation.Suspend();
		}

		private void AnimateBarThread() {
			while (true) {
				progressBar.PerformStep();
				if (progressBar.Value >= progressBar.Maximum) {
				  progressBar.RightToLeft = RightToLeft.Yes;
					progressBar.Step = -progressBar.Step;
				}
				if (progressBar.Value <= progressBar.Minimum) {
				  progressBar.RightToLeft = RightToLeft.Yes;
					progressBar.Step = -progressBar.Step;
				}
				Thread.Sleep(100);
			}
		}

		public ProgressStatus() {
			thAnimation = new Thread(new ThreadStart(AnimateBarThread));
			progressBar.Hide();
			this.Controls.Add(progressBar);    
			this.SizingGrip = false;
			this.DrawItem += new StatusBarDrawItemEventHandler(this.ProgressStatus_DrawItem);
		  //this.Paint += new PaintEventHandler(ProgressStatus_Paint);
		}

	  public int setProgressBar {
			get {
				return panelWithProgressBar;
			}
			set {
				panelWithProgressBar = value;
        if (value != -1)
        {
          this.Panels[panelWithProgressBar].Style = StatusBarPanelStyle.OwnerDraw;
        }
        else
        {
          foreach (StatusBarPanel p in this.Panels)
          {
            p.Style = StatusBarPanelStyle.Text;
          }
        }
			}
		}

		private void ProgressStatus_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent) {
			progressBar.Location = new Point(sbdevent.Bounds.X, sbdevent.Bounds.Y);
			progressBar.Size = new Size(sbdevent.Bounds.Width, sbdevent.Bounds.Height);
			progressBar.Show();
		}
	}
}