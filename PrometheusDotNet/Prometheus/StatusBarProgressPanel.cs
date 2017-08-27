using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace Prometheus
{
	/// <summary>
	/// Status Bar Panel that Displays Progress
	/// </summary>
	public class StatusBarProgressPanel
		: System.Windows.Forms.StatusBarPanel
	{
		#region Member Variables

		private RefreshDelegate _refreshDelegate;

		private bool _drawEventRegistered;
		private ProgressDisplayStyle _animationStyle;
		
		private long _stepSize;
		private long _startPoint;
		private long _endPoint;
		private long _currentPosition;

		//private System.Drawing.Drawing2D.LinearGradientBrush _progressBrush;
		private System.Drawing.TextureBrush _progressBrush;
		private Brush _textBrush;
		private Font _textFont;
		private bool _showText;

		private TimeSpan _animationTick;
		private Thread _animationThread;

		/// <summary>
		/// Flag used by Infiniate Style
		/// </summary>
		private bool _increasing;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Construction / Destruction

		/// <summary>
		/// Creates a new StatusBarProgressPanel
		/// </summary>
		public StatusBarProgressPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			_drawEventRegistered = false;

			_animationStyle = ProgressDisplayStyle.Infinite;

			_currentPosition = 0;
			_stepSize = 10;
			_startPoint = 0;
			_endPoint = 100;
			
			_showText = true;
			_textFont = new Font("Arial", 8);
			_textBrush = SystemBrushes.ControlText;
			
			//_progressBrush = SystemBrushes.Highlight;
			//_progressBrush =  new System.Drawing.Drawing2D.LinearGradientBrush(
			//	new Rectangle(new Point(0,0), new Size(20, 10)), Color.Red, Color.Yellow, LinearGradientMode.Vertical);
			//System.IO.Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Prometheus.bomb.png");
			System.IO.Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Prometheus.progress_bar.gif");
			Image img = new Bitmap(imageStream);
			_progressBrush = new System.Drawing.TextureBrush(img);
			_increasing = true;

			_animationTick = TimeSpan.FromSeconds(0.5);
			InitializeAnimationThread();

			_refreshDelegate = new RefreshDelegate( this.Refresh );
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		#region Properties

		/// <summary>
		/// The method used when drawing the progress bar
		/// </summary>
		[Category("Animation")]
		public ProgressDisplayStyle AnimationStyle
		{
			get
			{
				return _animationStyle;
			}
			set
			{
				_animationStyle = value;
			}
		}

		/// <summary>
		/// Timespan between infinate progress animation changes
		/// </summary>
		[Category("Animation")]
		public TimeSpan AnimationTick
		{
			get
			{
				return _animationTick;
			}
			set
			{
				_animationTick = value;
			}
		}

		/// <summary>
		/// Ammount to move on each progress step
		/// </summary>
		[Category("Measurement")]
		public long StepSize
		{
			get
			{
				return _stepSize;
			}
			set
			{
				_stepSize = value;
			}
		}

		/// <summary>
		/// Start point of progress
		/// </summary>
		[Category("Measurement")]
		public long StartPoint
		{
			get
			{
				return _startPoint;
			}
			set
			{
				_startPoint = value;
			}
		}

		/// <summary>
		/// Point of progress completion
		/// </summary>
		[Category("Measurement")]
		public long EndPoint
		{
			get
			{
				return _endPoint;
			}
			set
			{
				_endPoint = value;
			}
		}

		/// <summary>
		/// Current Position of the Progress Indicator
		/// </summary>
		[Category("Measurement")]
		public long ProgressPosition
		{
			get
			{
				return _currentPosition;
			}
			set
			{
				_currentPosition = value;
			}
		}

		/// <summary>
		/// Brush style of the progress indicator
		/// </summary>
		[Category("Style")]
		public TextureBrush ProgressDrawStyle
		{
			get
			{
				return _progressBrush;
			}
			set
			{
				_progressBrush = value;
			}
		}

		/// <summary>
		/// Brush style of the Text when it is drawn
		/// </summary>
		[Category("Style")]
		public Brush TextDrawStyle
		{
			get
			{
				return _textBrush;
			}
			set
			{
				_textBrush = value;
			}
		}

		/// <summary>
		/// Font style of the Text when it is drawn
		/// </summary>
		[Category("Style")]
		public Font TextFont
		{
			get
			{
				return _textFont;
			}
			set
			{
				_textFont = value;
			}
		}

		/// <summary>
		/// Optionally Display Text value of the Indicator
		/// </summary>
		[Category("Style")]
		public bool ShowText
		{
			get
			{
				return _showText;
			}
			set
			{
				_showText = value;
			}
		}

		/// <summary>
		/// Value indicating the current status of the animation thread
		/// </summary>
		[Category("Animation")]
		public bool IsAnimated
		{
			get
			{
				return _animationThread.IsAlive;
			}
		}

		#endregion

		#region Step

		/// <summary>
		/// Promotes the progress bar by one step
		/// </summary>
		public void Step()
		{
			if ( ! _drawEventRegistered )
			{
				this.Parent.DrawItem += new StatusBarDrawItemEventHandler(Parent_DrawItem);
				_drawEventRegistered = true;
			}

			if ( this.IsAnimated )
			{
				if ( _increasing )
				{
					_currentPosition += _stepSize;

					if (_currentPosition >= _endPoint)
					{
						_increasing = false;
					}
				}
				else
				{
					_currentPosition -= _stepSize;

					if (_currentPosition <= _startPoint)
					{
						_increasing = true;
					}
				}
			}
			else if (_currentPosition < _endPoint)
			{
				_currentPosition += _stepSize;
			}

			this.Parent.Invoke( _refreshDelegate );
		}

		#endregion

		#region Refresh

		/// <summary>
		/// Refreshes the progress bar
		/// </summary>
		public void Refresh()
		{
			this.Parent.Refresh();
		}

		#endregion

		#region Reset

		/// <summary>
		/// Reinitializes the progress bar
		/// </summary>
		public void Reset()
		{
			StopAnimation();
			_currentPosition = _startPoint;
			
			this.Parent.Invoke( _refreshDelegate );
		}

		#endregion

		#region Animation

		/// <summary>
		/// Spawn the progress animation thread
		/// </summary>
		public void StartAnimation()
		{
			StopAnimation();

			_currentPosition = 0;

			InitializeAnimationThread();
			
			_animationThread.Start();
		}

		/// <summary>
		/// Stop the progress animation thread
		/// </summary>
		public void StopAnimation()
		{
			if ( _animationThread.IsAlive )
			{
				_animationThread.Abort();
			}
		}

		/// <summary>
		/// ThreadStart Delegate Handler for infinate progress animation
		/// </summary>
		private void AnimationThreadStartCallback()
		{
			while ( true )
			{
				this.Step();
				if (this._currentPosition == this.EndPoint) {
					this.AnimationStyle = ProgressDisplayStyle.RightToLeft;
				}
				if (this._currentPosition == 0) {
					this.AnimationStyle = ProgressDisplayStyle.LeftToRight;
				}

				Thread.Sleep( _animationTick );
			}
		}

		private void InitializeAnimationThread()
		{
			_animationThread = new Thread(new ThreadStart( this.AnimationThreadStartCallback ));
			_animationThread.IsBackground = true;
			_animationThread.Name = "Progress Bar Animation Thread";
		}

		#endregion

		#region Owner-Draw

		/// <summary>
		/// Owner-Draw Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="sbdevent"></param>
		private void Parent_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
		{
			if ( sbdevent.Panel == this )
			{
				sbdevent.DrawBackground();

				if (_currentPosition != _startPoint)
				{
					if ( ( _currentPosition <= _endPoint ) || (this.AnimationStyle == ProgressDisplayStyle.Infinite) )
					{
						Rectangle bounds = sbdevent.Bounds;
						float percent = ( (float)_currentPosition / ((float)_endPoint - (float)_startPoint) );

						switch ( this.AnimationStyle )
						{
							case ProgressDisplayStyle.LeftToRight :
							{
								bounds.Width = (int) (percent * (float)sbdevent.Bounds.Width);
								break;
							}
							case ProgressDisplayStyle.RightToLeft :
							{
								bounds.Width = (int) (percent * (float)sbdevent.Bounds.Width);
								bounds.X += sbdevent.Bounds.Width - bounds.Width;
								break;
							}
							case ProgressDisplayStyle.BottomToTop :
							{
								bounds.Height = (int) (percent * (float)sbdevent.Bounds.Height);
								bounds.Y += sbdevent.Bounds.Height - bounds.Height;
								break;
							}
							case ProgressDisplayStyle.TopToBottom :
							{
								bounds.Height = (int) (percent * (float)sbdevent.Bounds.Height);
								break;
							}
							case ProgressDisplayStyle.Infinite :
							{
								bounds.Height = (int) (percent * (float)sbdevent.Bounds.Height);
								bounds.Y += (sbdevent.Bounds.Height - bounds.Height) / 2;
								bounds.Width = (int) (percent * (float)sbdevent.Bounds.Width);
								bounds.X += (sbdevent.Bounds.Width - bounds.Width) / 2;
								break;
							}
						}
					
						// draw the progress bar
						sbdevent.Graphics.FillRectangle(_progressBrush, bounds);

						if ( this.ShowText )
						{
							// draw the text on top of the progress bar
							sbdevent.Graphics.DrawString((percent * 100).ToString(), _textFont, _textBrush, sbdevent.Bounds);
						}
					}
				}

			}
		}
	
		#endregion
		
		#region Delegates

		private delegate void RefreshDelegate();

		#endregion
	}

	#region ProgressDisplayStyle

	/// <summary>
	/// Statusbar Progress Display Styles
	/// </summary>
	public enum ProgressDisplayStyle
	{
		/// <summary>
		/// A continually moving animation
		/// </summary>
		Infinite,
		/// <summary>
		/// A progress bar that fills from left to right
		/// </summary>
		LeftToRight,
		/// <summary>
		/// A progress bar that fills from right to left
		/// </summary>
		RightToLeft,
		/// <summary>
		/// A progress bar that fills from bottom to top
		/// </summary>
		BottomToTop,
		/// <summary>
		/// A progress bar that fills from top to bottom
		/// </summary>
		TopToBottom
	}

	#endregion


}
