using System;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System.Diagnostics;

namespace Prometheus.Core.Render
{
	/// <summary>
	/// Summary description for InputManager.
	/// </summary>
	public class InputManager
	{
    private Device m_keyboard = null;
    private Device m_mouse = null;
    KeyboardState m_KeyState = null;
    MouseState m_MouseState;
    private bool m_bLeftMouseBtn = false;
    private bool m_bMiddleMouseBtn = false;
    private bool m_bRightMouseBtn = false;

    /// <summary>
    /// This class is responsible for checking the keyboard state each frame and mapping the
    /// inputs to "state variables", such as "CameraForward".  Since the state is encapsulated
    /// inside of this class, we plan on allowing the user to map the keys to the functions they want.
    /// </summary>
    public InputManager()
		{
		}

    #region Camera Keys
    public bool CameraForward
    {
      get{return(m_KeyState[Key.W]);}
    }
    public bool CameraRight
    {
      get{return(m_KeyState[Key.D]);}
    }
    public bool CameraBack
    {
      get{return(m_KeyState[Key.S]);}
    }
    public bool CameraLeft
    {
      get{return(m_KeyState[Key.A]);}
    }
    public bool CameraUp
    {
      get{return(m_KeyState[Key.R]);}
    }
    public bool CameraDown
    {
      get{return(m_KeyState[Key.F]);}
    }
    #endregion

    public bool IncreaseGamma
    {
      get{return(m_KeyState[Key.NumPadPlus]);}
    }
    public bool DecreaseGamma
    {
      get{return(m_KeyState[Key.NumPadMinus]);}
    }

    #region Edit Keys
    public bool StartSelectionBox
    {
      get{return(m_KeyState[Key.Z]);}
    }
    public bool EditVerticalPlacement
    {
      get{return(m_KeyState[Key.LeftControl]);}
    }

    public bool ApplyLightmapPaint
    {
      get
      {
        return(m_KeyState[Key.P]&&m_bLeftMouseBtn);
      }
    }
    #endregion

    public int MouseX
    {
      get{return(m_MouseState.X);}
    }
    public int MouseY
    {
      get{return(m_MouseState.Y);}
    }
    public int MouseZ
    {
      get{return(m_MouseState.Z);}
    }
    public bool CameraActive
    {
      get
      {
        return(m_bLeftMouseBtn&&!ApplyLightmapPaint);
      }
    }
    public bool EditActive
    {
      get{return(m_bLeftMouseBtn);}
    }

    public void KeyboardInit(Control parent)
    {
      m_keyboard = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Keyboard);
      m_keyboard.SetCooperativeLevel(parent, CooperativeLevelFlags.Background |
        CooperativeLevelFlags.NonExclusive);
      m_keyboard.Acquire();
    }
    public void MouseInit(Control parent)
    {
      m_mouse = new Device(SystemGuid.Mouse);
      m_mouse.SetCooperativeLevel(parent, CooperativeLevelFlags.Background |
        CooperativeLevelFlags.NonExclusive);
      m_mouse.Acquire();
    }
    public void Update()
    {
      m_KeyState = m_keyboard.GetCurrentKeyboardState();
      m_MouseState = m_mouse.CurrentMouseState;

      //get state of mouse buttons
      byte[] buttons = m_MouseState.GetMouseButtons();
      m_bLeftMouseBtn = (buttons[0] == 128);
      m_bRightMouseBtn = (buttons[1] == 128);
      m_bMiddleMouseBtn = (buttons[2] == 128);
      //Trace.WriteLine("left button = " + m_bLeftMouseBtn.ToString());
    }
    public void Dispose()
    {
      if(m_keyboard != null) m_keyboard.Unacquire();
      if(m_mouse != null) m_mouse.Unacquire();
    }
	}
}
